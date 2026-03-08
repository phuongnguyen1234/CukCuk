﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CukCuk.Backend.Infrastructure.Repository
{
    using CukCuk.Backend.Core.Interfaces.Database;
    using CukCuk.Backend.Core.Interfaces.Repository;
    using CukCuk.Backend.Infrastructure.Extensions;
    using Dapper;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;

    /// <summary>
    /// Lớp base repository sử dụng Dapper để thực hiện các thao tác CRUD cơ bản trên database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// Created by Phuong 25/02/2026
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IDbConnectionFactory _connectionFactory;

        protected BaseRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Tên bảng trong database. Mặc định là tên class T chuyển sang snake_case. Override trong repository cụ thể nếu tên bảng khác.
        /// </summary>
        protected virtual string TableName => typeof(T).Name.ToSnakeCase();

        /// <summary>
        /// Tên cột ID chính. Mặc định là "Id" hoặc "{ClassName}Id". Override trong repository cụ thể nếu tên cột khác.
        /// </summary>
        protected virtual string KeyName => $"{typeof(T).Name}Id";

        // Hàm tiện ích để đặt tên bảng và cột trong MySQL với dấu backticks để tránh lỗi với từ khóa hoặc tên có dấu cách
        private static string MysqlQuote(string identifier) => $"`{identifier}`";

        /// <summary>
        /// Lấy object theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            var table = MysqlQuote(TableName);
            var key = MysqlQuote(KeyName.ToSnakeCase());
            var sql = $"SELECT * FROM {table} WHERE {key} = @Id";
            return await connection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
        }

        /// <summary>
        /// Lấy tất cả object
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            var table = MysqlQuote(TableName);
            var sql = $"SELECT * FROM {table}";
            return await connection.QueryAsync<T>(sql);
        }

        /// <summary>
        /// Thêm mới object
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task<Guid> CreateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            return await CreateAsync(entity, connection, null);
        }

        protected virtual async Task<Guid> CreateAsync(T entity, IDbConnection connection, IDbTransaction? transaction)
        {
            var idProp = typeof(T).GetProperty(KeyName, BindingFlags.Public | BindingFlags.Instance);
            Guid id;
            if (idProp != null && (idProp.PropertyType == typeof(Guid) || idProp.PropertyType == typeof(Guid?)) && idProp.CanWrite)
            {
                var current = idProp.GetValue(entity);
                id = current is Guid g && g != Guid.Empty ? g : Guid.NewGuid();
                idProp.SetValue(entity, id);
            }
            else
            {
                id = Guid.NewGuid();
            }

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.CanRead && p.CanWrite)
                                 .ToArray();

            if (!props.Any())
                throw new InvalidOperationException($"Type {typeof(T).FullName} has no readable/writable properties to insert.");

            // Column names must use snake_case to match database schema, parameter names keep CLR property names
            var columns = props.Select(p => MysqlQuote(p.Name.ToSnakeCase()));
            var parameters = props.Select(p => "@" + p.Name);

            var columnList = string.Join(", ", columns);
            var paramList = string.Join(", ", parameters);

            var table = MysqlQuote(TableName);
            var sql = $"INSERT INTO {table} ({columnList}) VALUES ({paramList})";

            var affected = await connection.ExecuteAsync(sql, entity, transaction: transaction);
            return affected > 0 ? id : Guid.Empty;
        }

        /// <summary>
        /// Cập nhật object
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            return await UpdateAsync(entity, connection, null);
        }

        protected virtual async Task<bool> UpdateAsync(T entity, IDbConnection connection, IDbTransaction? transaction)
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.CanRead && p.CanWrite && !string.Equals(p.Name, KeyName, StringComparison.OrdinalIgnoreCase))
                                 .ToArray();

            if (!props.Any())
                throw new InvalidOperationException($"Type {typeof(T).FullName} has no updatable properties.");

            // Set columns to snake_case, parameters use CLR property names
            var setClause = string.Join(", ", props.Select(p => $"{MysqlQuote(p.Name.ToSnakeCase())} = @{p.Name}"));
            var table = MysqlQuote(TableName);
            var key = MysqlQuote(KeyName.ToSnakeCase());
            var sql = $"UPDATE {table} SET {setClause} WHERE {key} = @{KeyName}";

            var affected = await connection.ExecuteAsync(sql, entity, transaction: transaction);
            return affected > 0;
        }

        /// <summary>
        /// Xóa object theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is DbConnection dbConnection)
                await dbConnection.OpenAsync();
            else
                connection.Open();

            return await DeleteAsync(id, connection, null);
        }

        protected virtual async Task<bool> DeleteAsync(Guid id, IDbConnection connection, IDbTransaction? transaction)
        {
            var table = MysqlQuote(TableName);
            var key = MysqlQuote(KeyName.ToSnakeCase());
            var sql = $"DELETE FROM {table} WHERE {key} = @Id";
            var affected = await connection.ExecuteAsync(sql, new { Id = id }, transaction: transaction);
            return affected > 0;
        }
    }
}
