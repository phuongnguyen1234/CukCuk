﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CukCuk.Backend.Infrastructure.Repository
{
    using CukCuk.Backend.Core.Entities;
    using CukCuk.Backend.Core.Interfaces.Database;
    using CukCuk.Backend.Core.Interfaces.Repository;
    using CukCuk.Backend.Core.DTOs;
    using Dapper;
    using System.Data;
    using System.Data.Common;
    using CukCuk.Backend.Infrastructure.Extensions;

    /// <summary>
    /// Repo của InventoryItem
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class InventoryItemRepository : BaseRepository<InventoryItem>, IInventoryItemRepository
    {
        public InventoryItemRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        /// <summary>
        /// Tạo món ăn cùng với các thông tin liên quan trong một transaction
        /// </summary>
        public async Task<Guid> CreateWithRelationsAsync(InventoryItem item, IEnumerable<Guid> additionIds, IEnumerable<Guid> kitchenIds)
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection) await dbConnection.OpenAsync(); else connection.Open();
            
            using var tran = connection.BeginTransaction();
            try
            {
                // 1. Tạo món ăn
                var id = await CreateAsync(item, connection, tran);

                // 2. Thêm sở thích phục vụ
                if (additionIds != null && additionIds.Any())
                {
                    var sqlAdd = @"INSERT INTO `item_addition` (`inventory_item_id`, `inventory_item_addition_id`) VALUES (@InventoryItemId, @InventoryItemAdditionId)";
                    await connection.ExecuteAsync(sqlAdd, additionIds.Distinct().Select(aid => new { InventoryItemId = id, InventoryItemAdditionId = aid }), transaction: tran);
                }

                // 3. Thêm nơi chế biến
                if (kitchenIds != null && kitchenIds.Any())
                {
                    var sqlKitchen = @"INSERT INTO `item_kitchen` (`inventory_item_id`, `kitchen_id`) VALUES (@InventoryItemId, @KitchenId)";
                    await connection.ExecuteAsync(sqlKitchen, kitchenIds.Distinct().Select(kid => new { InventoryItemId = id, KitchenId = kid }), transaction: tran);
                }

                tran.Commit();
                return id;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật món ăn và đồng bộ các thông tin liên quan trong một transaction
        /// </summary>
        public async Task<bool> UpdateWithRelationsAsync(InventoryItem item, IEnumerable<Guid> addAdditions, IEnumerable<Guid> removeAdditions, IEnumerable<Guid> addKitchens, IEnumerable<Guid> removeKitchens)
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection) await dbConnection.OpenAsync(); else connection.Open();

            using var tran = connection.BeginTransaction();
            try
            {
                // 1. Cập nhật thông tin món
                var updated = await UpdateAsync(item, connection, tran);
                if (!updated)
                {
                    tran.Rollback();
                    return false;
                }

                // 2. Xử lý sở thích (Thêm/Xóa)
                if (addAdditions != null && addAdditions.Any())
                {
                    var sqlAdd = @"INSERT INTO `item_addition` (`inventory_item_id`, `inventory_item_addition_id`) VALUES (@InventoryItemId, @InventoryItemAdditionId)";
                    await connection.ExecuteAsync(sqlAdd, addAdditions.Select(aid => new { InventoryItemId = item.InventoryItemId, InventoryItemAdditionId = aid }), transaction: tran);
                }
                if (removeAdditions != null && removeAdditions.Any())
                {
                    var sqlRemove = @"DELETE FROM `item_addition` WHERE `inventory_item_id` = @InventoryItemId AND `inventory_item_addition_id` IN @Ids";
                    await connection.ExecuteAsync(sqlRemove, new { InventoryItemId = item.InventoryItemId, Ids = removeAdditions }, transaction: tran);
                }

                // 3. Xử lý nơi chế biến (Thêm/Xóa)
                if (addKitchens != null && addKitchens.Any())
                {
                    var sqlAddK = @"INSERT INTO `item_kitchen` (`inventory_item_id`, `kitchen_id`) VALUES (@InventoryItemId, @KitchenId)";
                    await connection.ExecuteAsync(sqlAddK, addKitchens.Select(kid => new { InventoryItemId = item.InventoryItemId, KitchenId = kid }), transaction: tran);
                }
                if (removeKitchens != null && removeKitchens.Any())
                {
                    var sqlRemoveK = @"DELETE FROM `item_kitchen` WHERE `inventory_item_id` = @InventoryItemId AND `kitchen_id` IN @Ids";
                    await connection.ExecuteAsync(sqlRemoveK, new { InventoryItemId = item.InventoryItemId, Ids = removeKitchens }, transaction: tran);
                }

                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteWithRelationsAsync(Guid id)
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection) await dbConnection.OpenAsync(); else connection.Open();

            using var tran = connection.BeginTransaction();
            try
            {
                // 1. Xóa các liên kết sở thích
                var sqlAdditions = @"DELETE FROM `item_addition` WHERE `inventory_item_id` = @Id";
                await connection.ExecuteAsync(sqlAdditions, new { Id = id }, transaction: tran);

                // 2. Xóa các liên kết nơi chế biến
                var sqlKitchens = @"DELETE FROM `item_kitchen` WHERE `inventory_item_id` = @Id";
                await connection.ExecuteAsync(sqlKitchens, new { Id = id }, transaction: tran);

                // 3. Xóa món ăn chính
                var deleted = await DeleteAsync(id, connection, tran);

                tran.Commit();
                return deleted;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Lấy danh sách InventoryItem có phân trang và lọc
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<PagedResult<InventoryItem>> GetPagedAsync(int page, int pageSize, IEnumerable<FilterCriteria>? filters = null, string? search = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 20;

            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            // Sử dụng Alias 'i' cho bảng chính và JOIN các bảng liên quan để hỗ trợ lọc theo tên
            var sqlFrom = $"`{TableName}` i " +
                          $"LEFT JOIN `inventory_item_category` c ON i.`inventory_item_category_id` = c.`inventory_item_category_id` " +
                          $"LEFT JOIN `unit` u ON i.`unit_id` = u.`unit_id` " +
                          $"LEFT JOIN `inventory_item_type` t ON i.`inventory_item_type_id` = t.`inventory_item_type_id`";

            var whereClauses = new List<string>();
            var parameters = new DynamicParameters();

            // Xây dựng điều kiện WHERE từ filters
            if (filters != null)
            {
                int i = 0;
                foreach (var f in filters)
                {
                    if (string.IsNullOrWhiteSpace(f.Column))
                        continue;

                    // Tách giá trị theo dấu phẩy để hỗ trợ lọc nhiều giá trị (OR logic)
                    // Ví dụ: Name chứa "Bánh" hoặc "Trà"
                    var values = f.Value?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? Array.Empty<string>();
                    if (!values.Any()) continue;

                    // Chuẩn hóa tên cột về snake_case để đảm bảo khớp với tên cột trong DB (kể cả khi FE gửi camelCase)
                    var columnSnakeCase = f.Column.ToSnakeCase();

                    // Ánh xạ tên cột filter sang cột trong bảng tương ứng (xử lý Alias)
                    string colSql;
                    if (columnSnakeCase.Equals("inventory_item_category_name", StringComparison.OrdinalIgnoreCase))
                    {
                        colSql = "c.`inventory_item_category_name`";
                    }
                    else if (columnSnakeCase.Equals("unit_name", StringComparison.OrdinalIgnoreCase))
                    {
                        colSql = "u.`unit_name`";
                    }
                    else if (columnSnakeCase.Equals("inventory_item_type_name", StringComparison.OrdinalIgnoreCase))
                    {
                        colSql = "t.`inventory_item_type_name`";
                    }
                    else
                    {
                        colSql = $"i.`{columnSnakeCase}`"; // Mặc định là bảng chính
                    }

                    // Xử lý đặc biệt cho các cột boolean
                    var isBooleanColumn = columnSnakeCase.StartsWith("is_") || columnSnakeCase.Contains("_is_") || 
                                          columnSnakeCase.Equals("inventory_item_allow_price_override", StringComparison.OrdinalIgnoreCase);
                    // Xử lý cho các cột tiền tệ/số để đảm bảo so sánh đúng kiểu dữ liệu (tránh so sánh chuỗi)
                    var isNumericColumn = columnSnakeCase.Equals("inventory_item_price", StringComparison.OrdinalIgnoreCase) ||
                                           columnSnakeCase.Equals("inventory_item_cost_price", StringComparison.OrdinalIgnoreCase);

                    var subClauses = new List<string>();

                    if (isNumericColumn)
                    {
                        // Logic dành riêng cho các cột kiểu số (giá, giá vốn)
                        bool isAndLogic = f.Operation == FilterOperation.NotEqual;

                        for (int j = 0; j < values.Length; j++)
                        {
                            var paramName = $"p{i}_{j}";
                            var val = values[j];
                            // Sử dụng InvariantCulture để đảm bảo parse đúng số bất kể locale của server
                            if (decimal.TryParse(val, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var decimalVal))
                            {
                                subClauses.Add($"{colSql} {GetSqlOperator(f.Operation)} @{paramName}");
                                parameters.Add(paramName, decimalVal, DbType.Decimal);
                            }
                        }
                        if (subClauses.Any())
                        {
                            var joinOperator = isAndLogic ? " AND " : " OR ";
                            whereClauses.Add($"({string.Join(joinOperator, subClauses)})");
                        }
                    }
                    else if (isBooleanColumn && f.Operation == FilterOperation.Equals)
                    {
                        // Logic dành riêng cho các cột boolean
                        for (int j = 0; j < values.Length; j++)
                        {
                            var paramName = $"p{i}_{j}";
                            var val = values[j];
                            object? dbVal = null;

                            if (val.Equals("Có", StringComparison.OrdinalIgnoreCase) || val.Equals("true", StringComparison.OrdinalIgnoreCase) || val.Equals("1"))
                            {
                                dbVal = 1;
                            }
                            else if (val.Equals("Không", StringComparison.OrdinalIgnoreCase) || val.Equals("false", StringComparison.OrdinalIgnoreCase) || val.Equals("0"))
                            {
                                dbVal = 0;
                            }

                            if (dbVal != null)
                            {
                                parameters.Add(paramName, dbVal);
                                subClauses.Add($"{colSql} = @{paramName}");
                            }
                        }
                        if (subClauses.Any())
                        {
                            whereClauses.Add($"({string.Join(" OR ", subClauses)})");
                        }
                    }
                    else // Mặc định xử lý như kiểu chuỗi cho các cột còn lại
                    {
                        // Với phép phủ định (Khác, Không chứa), ta dùng AND (VD: Khác A AND Khác B)
                        // Với phép khẳng định (Bằng, Chứa...), ta dùng OR (VD: Bằng A OR Bằng B)
                        bool isAndLogic = f.Operation == FilterOperation.NotContains || f.Operation == FilterOperation.NotEqual;

                        for (int j = 0; j < values.Length; j++)
                        {
                            var paramName = $"p{i}_{j}";
                            var val = values[j];

                            switch (f.Operation)
                            {
                                case FilterOperation.Contains:
                                    subClauses.Add($"{colSql} LIKE @{paramName}");
                                    parameters.Add(paramName, $"%{val}%", DbType.String);
                                    break;
                                case FilterOperation.NotContains:
                                    subClauses.Add($"{colSql} NOT LIKE @{paramName}");
                                    parameters.Add(paramName, $"%{val}%", DbType.String);
                                    break;
                                case FilterOperation.StartsWith:
                                    subClauses.Add($"{colSql} LIKE @{paramName}");
                                    parameters.Add(paramName, $"{val}%", DbType.String);
                                    break;
                                case FilterOperation.EndsWith:
                                    subClauses.Add($"{colSql} LIKE @{paramName}");
                                    parameters.Add(paramName, $"%{val}", DbType.String);
                                    break;
                                case FilterOperation.Equals:
                                    subClauses.Add($"{colSql} = @{paramName}");
                                    parameters.Add(paramName, val, DbType.String);
                                    break;
                                case FilterOperation.NotEqual:
                                    subClauses.Add($"{colSql} <> @{paramName}");
                                    parameters.Add(paramName, val, DbType.String);
                                    break;
                                default:
                                    // Trường hợp này không nên xảy ra với cột chuỗi (ví dụ: > , <)
                                    // nhưng nếu có, nó sẽ được coi là so sánh chuỗi.
                                    subClauses.Add($"{colSql} {GetSqlOperator(f.Operation)} @{paramName}");
                                    parameters.Add(paramName, val, DbType.String);
                                    break;
                            }
                        }

                        if (subClauses.Any())
                        {
                            var joinOperator = isAndLogic ? " AND " : " OR ";
                            whereClauses.Add($"({string.Join(joinOperator, subClauses)})");
                        }
                    }
                    i++;
                }
            }

            // Xử lý tìm kiếm chung (Search Box) - Tìm theo Code HOẶC Name
            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchParamName = "SearchParam";
                parameters.Add(searchParamName, $"{search.Trim()}%");
                whereClauses.Add($"(LOWER(i.`inventory_item_name`) LIKE LOWER(@{searchParamName}) OR LOWER(i.`inventory_item_code`) LIKE LOWER(@{searchParamName}))");
            }

            var whereSql = whereClauses.Any() ? "WHERE " + string.Join(" AND ", whereClauses) : string.Empty;

            // Đếm tổng số
            var countSql = $"SELECT COUNT(*) FROM {sqlFrom} {whereSql}";
            var total = await connection.QuerySingleAsync<long>(countSql, parameters);

            // Lấy danh sách có phân trang
            var offset = (page - 1) * pageSize;
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", pageSize);

            // Chỉ select i.* để trả về đúng entity InventoryItem, tránh lỗi mapping trùng tên cột từ bảng khác
            var sql = $"SELECT i.* FROM {sqlFrom} {whereSql} ORDER BY i.`inventory_item_name` ASC LIMIT @Offset, @PageSize";
            var items = await connection.QueryAsync<InventoryItem>(sql, parameters);

            return new PagedResult<InventoryItem>(items, page, pageSize, total);
        }

        private static string GetSqlOperator(FilterOperation op) => op switch
        {
            FilterOperation.Equals => "=",
            FilterOperation.NotEqual => "<>",
            FilterOperation.GreaterThan => ">",
            FilterOperation.GreaterThanOrEqual => ">=",
            FilterOperation.LessThan => "<",
            FilterOperation.LessThanOrEqual => "<=",
            FilterOperation.Contains => "LIKE",
            FilterOperation.NotContains => "NOT LIKE",
            FilterOperation.StartsWith => "LIKE",
            FilterOperation.EndsWith => "LIKE",
            _ => throw new NotSupportedException($"Filter operation {op} is not supported.")
        };

        /// <summary>
        /// Kiểm tra tồn tại của InventoryItem theo code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="excludeId"></param>
        /// <returns></returns>
        public async Task<bool> ExistsByCodeAsync(string code, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(code)) return false;

            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            var table = $"`{TableName}`";
            var sql = $@"
                SELECT COUNT(1)
                FROM {table}
                WHERE LOWER(`inventory_item_code`) = LOWER(@Code)";

            if (excludeId.HasValue && excludeId != Guid.Empty)
            {
                sql += " AND `inventory_item_id` <> @ExcludeId";
            }

            var count = await connection.QuerySingleAsync<long>(sql, new { Code = code.Trim(), ExcludeId = excludeId });
            return count > 0;
        }

        #region Item Additions

        /// <summary>
        /// Lấy danh sách addition của InventoryItem
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<InventoryItemAddition>> GetAdditionsForItemAsync(Guid inventoryItemId)
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            var sql = @"
                SELECT iia.*
                FROM `inventory_item_addition` AS iia
                INNER JOIN `item_addition` AS ia ON iia.`inventory_item_addition_id` = ia.`inventory_item_addition_id`
                WHERE ia.`inventory_item_id` = @InventoryItemId";

            var additions = await connection.QueryAsync<InventoryItemAddition>(sql, new { InventoryItemId = inventoryItemId });
            return additions;
        }
        #endregion

        #region Item Kitchens

        /// <summary>
        /// Lấy danh sách kitchen của InventoryItem
        /// </summary>
        public async Task<IEnumerable<Kitchen>> GetKitchensForItemAsync(Guid inventoryItemId)
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            var sql = @"
                SELECT k.*
                FROM `kitchen` AS k
                INNER JOIN `item_kitchen` AS ik ON k.`kitchen_id` = ik.`kitchen_id`
                WHERE ik.`inventory_item_id` = @InventoryItemId";

            var kitchens = await connection.QueryAsync<Kitchen>(sql, new { InventoryItemId = inventoryItemId });
            return kitchens;
        }
        #endregion
    }

    
}
