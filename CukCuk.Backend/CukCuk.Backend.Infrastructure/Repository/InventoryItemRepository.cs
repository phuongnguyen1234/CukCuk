using System;
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

                    // Ánh xạ tên cột filter sang cột trong bảng tương ứng (xử lý Alias)
                    string colSql;
                    if (f.Column.Equals("inventory_item_category_name", StringComparison.OrdinalIgnoreCase))
                    {
                        colSql = "c.`inventory_item_category_name`";
                    }
                    else if (f.Column.Equals("unit_name", StringComparison.OrdinalIgnoreCase))
                    {
                        colSql = "u.`unit_name`";
                    }
                    else if (f.Column.Equals("inventory_item_type_name", StringComparison.OrdinalIgnoreCase))
                    {
                        colSql = "t.`inventory_item_type_name`";
                    }
                    else
                    {
                        colSql = $"i.`{f.Column}`"; // Mặc định là bảng chính
                    }

                    // Xử lý đặc biệt cho các cột boolean
                    bool isBooleanColumn = f.Column.StartsWith("is_") || f.Column.Contains("_is_");
                    if (isBooleanColumn && f.Operation == FilterOperation.Equals)
                    {
                        var boolClauses = new List<string>();
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
                                boolClauses.Add($"{colSql} = @{paramName}");
                            }
                        }
                        if (boolClauses.Any())
                        {
                            // Với nhiều giá trị boolean (ít xảy ra), dùng OR
                            whereClauses.Add($"({string.Join(" OR ", boolClauses)})");
                        }
                    }
                    else // Xử lý cho các kiểu dữ liệu khác (string, number)
                    {
                        var subClauses = new List<string>();
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
                                    parameters.Add(paramName, $"%{val}%");
                                    break;
                                case FilterOperation.NotContains:
                                    subClauses.Add($"{colSql} NOT LIKE @{paramName}");
                                    parameters.Add(paramName, $"%{val}%");
                                    break;
                                case FilterOperation.StartsWith:
                                    subClauses.Add($"{colSql} LIKE @{paramName}");
                                    parameters.Add(paramName, $"{val}%");
                                    break;
                                case FilterOperation.EndsWith:
                                    subClauses.Add($"{colSql} LIKE @{paramName}");
                                    parameters.Add(paramName, $"%{val}");
                                    break;
                                case FilterOperation.Equals:
                                    subClauses.Add($"{colSql} = @{paramName}");
                                    parameters.Add(paramName, val);
                                    break;
                                case FilterOperation.NotEqual:
                                    subClauses.Add($"{colSql} <> @{paramName}");
                                    parameters.Add(paramName, val);
                                    break;
                                // Các trường hợp còn lại cho number
                                default:
                                    subClauses.Add($"{colSql} {GetSqlOperator(f.Operation)} @{paramName}");
                                    parameters.Add(paramName, val);
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
                parameters.Add(searchParamName, $"%{search.Trim()}%");
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
        /// Thêm các addition vào InventoryItem
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <param name="additionIds"></param>
        /// <returns></returns>
        public async Task AddItemAdditionsAsync(Guid inventoryItemId, IEnumerable<Guid> additionIds)
        {
            if (additionIds == null) return;

            var ids = additionIds.Where(id => id != Guid.Empty).Distinct().ToArray();
            if (!ids.Any()) return;

            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            using var tran = connection.BeginTransaction();
            try
            {
                var sql = @"
                    INSERT INTO `item_addition` (`inventory_item_id`, `inventory_item_addition_id`)
                    VALUES (@InventoryItemId, @InventoryItemAdditionId)";

                foreach (var addId in ids)
                {
                    await connection.ExecuteAsync(sql, new
                    {
                        InventoryItemId = inventoryItemId,
                        InventoryItemAdditionId = addId
                    }, transaction: tran);
                }

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Xóa các addition khỏi InventoryItem
        /// </summary>
        public async Task RemoveItemAdditionsAsync(Guid inventoryItemId, IEnumerable<Guid> additionIds)
        {
            if (additionIds == null) return;

            var ids = additionIds.Where(id => id != Guid.Empty).Distinct().ToArray();
            if (!ids.Any()) return;

            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            var sql = @"DELETE FROM `item_addition`
                        WHERE `inventory_item_id` = @InventoryItemId
                          AND `inventory_item_addition_id` IN @Ids";
            await connection.ExecuteAsync(sql, new { InventoryItemId = inventoryItemId, Ids = ids });
        }

        /// <summary>
        /// Xóa tất cả addition khỏi InventoryItem
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        public async Task RemoveAllItemAdditionsAsync(Guid inventoryItemId)
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            var sql = @"DELETE FROM `item_addition` WHERE `inventory_item_id` = @Id";
            await connection.ExecuteAsync(sql, new { Id = inventoryItemId });
        }

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
        /// Thêm các kitchen vào InventoryItem
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <param name="kitchenIds"></param>
        /// <returns></returns>
        public async Task AddItemKitchensAsync(Guid inventoryItemId, IEnumerable<Guid> kitchenIds)
        {
            if (kitchenIds == null) return;

            var ids = kitchenIds.Where(id => id != Guid.Empty).Distinct().ToArray();
            if (!ids.Any()) return;

            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            using var tran = connection.BeginTransaction();
            try
            {
                var sql = @"
                    INSERT INTO `item_kitchen` (`inventory_item_id`, `kitchen_id`)
                    VALUES (@InventoryItemId, @KitchenId)";

                foreach (var kId in ids)
                {
                    await connection.ExecuteAsync(sql, new
                    {
                        InventoryItemId = inventoryItemId,
                        KitchenId = kId
                    }, transaction: tran);
                }

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Xóa các kitchen khỏi InventoryItem
        /// </summary>
        public async Task RemoveItemKitchensAsync(Guid inventoryItemId, IEnumerable<Guid> kitchenIds)
        {
            if (kitchenIds == null) return;

            var ids = kitchenIds.Where(id => id != Guid.Empty).Distinct().ToArray();
            if (!ids.Any()) return;

            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            var sql = @"DELETE FROM `item_kitchen`
                        WHERE `inventory_item_id` = @InventoryItemId
                          AND `kitchen_id` IN @Ids";
            await connection.ExecuteAsync(sql, new { InventoryItemId = inventoryItemId, Ids = ids });
        }

        /// <summary>
        /// Xóa tất cả kitchen khỏi InventoryItem
        /// </summary>
        public async Task RemoveAllItemKitchensAsync(Guid inventoryItemId)
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            var sql = @"DELETE FROM `item_kitchen` WHERE `inventory_item_id` = @Id";
            await connection.ExecuteAsync(sql, new { Id = inventoryItemId });
        }

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
