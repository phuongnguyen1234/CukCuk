using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Entities
{
    /// <summary>
    /// Nhóm thực đơn (Khai vị, Món chính, Nước ép...).
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class InventoryItemCategory
    {
        /// <summary>
        /// Khóa chính nhóm thực đơn.
        /// </summary>
        public Guid InventoryItemCategoryId { get; set; }

        /// <summary>
        /// Mã nhóm.
        /// </summary>
        public string InventoryItemCategoryCode { get; set; } = null!;

        /// <summary>
        /// Tên nhóm.
        /// </summary>
        public string InventoryItemCategoryName { get; set; } = null!;
    }
}
