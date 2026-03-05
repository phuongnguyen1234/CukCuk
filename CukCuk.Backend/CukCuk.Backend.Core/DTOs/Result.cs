using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.DTOs
{
    /// <summary>
    /// DTO trả về của controller
    /// </summary>
    /// Created by Phuong 26/02/2026
    public class Result
    {
        /// <summary>
        /// Có thành công không
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Thông báo
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int? StatusCode { get; set; }

        public Result() { }
        public Result(bool success, string? message, int? statusCode)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
