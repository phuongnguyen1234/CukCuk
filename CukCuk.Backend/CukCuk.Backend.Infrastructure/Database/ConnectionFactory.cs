using CukCuk.Backend.Core.Interfaces.Database;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Infrastructure.Database
{
    /// <summary>
    /// Lớp ConnectionFactory để tạo kết nối đến cơ sở dữ liệu MySQL
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class ConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Tạo một kết nối đến cơ sở dữ liệu MySQL
        /// </summary>
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
