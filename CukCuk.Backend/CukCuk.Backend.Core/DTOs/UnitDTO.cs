using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.DTOs
{
    /// <summary>
    /// DTO của Unit
    /// </summary>
    /// Created by Phuong 26/02/2026
    public class UnitDTO
    {
        /// <summary>
        /// Khóa chính đơn vị.
        /// </summary>
        public Guid? UnitId { get; set; }

        /// <summary>
        /// Tên đơn vị.
        /// </summary>
        public string UnitName { get; set; } = null!;

        /// <summary>
        /// Mô tả đơn vị
        /// </summary>
        public string? UnitDescription { get; set; } = string.Empty;
    }
}