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
    /// Repo của InventoryItemAddition
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class InventoryItemAdditionRepository : BaseRepository<InventoryItemAddition>, IInventoryItemAdditionRepository
    {
        public InventoryItemAdditionRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public override async Task<IEnumerable<InventoryItemAddition>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            var table = $"`{TableName}`";
            var sql = $"SELECT * FROM {table} ORDER BY `inventory_item_addition_name` ASC";
            return await connection.QueryAsync<InventoryItemAddition>(sql);
        }
    }
}
