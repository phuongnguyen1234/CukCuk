using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Infrastructure.Repository
{
    using System.Data.Common;
    using CukCuk.Backend.Core.Entities;
    using CukCuk.Backend.Core.Interfaces.Database;
    using CukCuk.Backend.Core.Interfaces.Repository;
    using Dapper;

    /// <summary>
    /// Repo của InventoryItemCategory
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class InventoryItemCategoryRepository : BaseRepository<InventoryItemCategory>, IInventoryItemCategoryRepository
    {
        public InventoryItemCategoryRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        /// <summary>
        /// Kiểm tra tồn tại của mã nhóm
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
                WHERE LOWER(`inventory_item_category_code`) = LOWER(@Code)";

            if (excludeId.HasValue && excludeId != Guid.Empty)
            {
                sql += " AND `inventory_item_category_id` <> @ExcludeId";
            }

            var count = await connection.QuerySingleAsync<long>(sql, new { Code = code.Trim(), ExcludeId = excludeId });
            return count > 0;
        }
    }
}
