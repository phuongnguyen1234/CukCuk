using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.DTOs
{
    /// <summary>
    /// DTO của InventoryItemCategory
    /// </summary>
    /// Created by Phuong 27/02/2026
    public class InventoryItemCategoryDTO
    {
        /// <summary>
        /// Khóa chính nhóm thực đơn.
        /// </summary>
        public Guid? InventoryItemCategoryId { get; set; }

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
