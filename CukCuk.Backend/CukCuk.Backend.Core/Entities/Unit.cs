using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Entities
{
    /// <summary>
    /// Đơn vị tính của món (Đĩa, Bát, Cốc...).
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class Unit
    {
        /// <summary>
        /// Khóa chính đơn vị.
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Tên đơn vị.
        /// </summary>
        public string UnitName { get; set; } = null!;

        /// <summary>
        /// Mô tả đơn vị
        /// </summary>
        public string UnitDescription { get; set; } = string.Empty;
    }
}
