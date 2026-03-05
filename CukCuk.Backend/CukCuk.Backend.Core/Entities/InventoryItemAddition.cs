using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Entities
{
    /// <summary>
    /// Sở thích phục vụ (ví dụ: thêm phô mai, topping...).
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class InventoryItemAddition
    {
        /// <summary>
        /// Khóa chính của addition.
        /// </summary>
        public Guid InventoryItemAdditionId { get; set; }

        /// <summary>
        /// Tên sở thích.
        /// </summary>
        public string InventoryItemAdditionName { get; set; } = null!;

        /// <summary>
        /// Giá cộng thêm (nếu có).
        /// </summary>
        public decimal InventoryItemAdditionPrice { get; set; }
    }
}
