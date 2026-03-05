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
    /// Repo của Unit
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class UnitRepository : BaseRepository<Unit>, IUnitRepository
    {
        public UnitRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }
    

    public async Task<bool> ExistsByNameAsync(string unitName, Guid? excludeId = null)
    {
        if (string.IsNullOrWhiteSpace(unitName)) return false;

        using var connection = _connectionFactory.CreateConnection();
        if (connection is DbConnection dbConnection)
            await dbConnection.OpenAsync();
        else
            connection.Open();

        var table = $"`{TableName}`";
        var sql = $@"
            SELECT COUNT(1)
            FROM {table}
            WHERE LOWER(`unit_name`) = LOWER(@UnitName)";

        if (excludeId.HasValue && excludeId != Guid.Empty)
        {
            sql += " AND `unit_id` <> @ExcludeId";
        }

        var count = await connection.QuerySingleAsync<long>(sql, new { UnitName = unitName.Trim(), ExcludeId = excludeId });
        return count > 0;
    }


}}
