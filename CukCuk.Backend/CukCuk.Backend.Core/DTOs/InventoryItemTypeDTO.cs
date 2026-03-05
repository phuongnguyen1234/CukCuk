using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.DTOs
{
    /// <summary>
    /// DTO của InventoryItemType
    /// </summary>
    /// Created by Phuong 28/02/2026
    public class InventoryItemTypeDTO
    {
        /// <summary>
        /// Khóa chính loại món.
        /// </summary>
        public Guid? InventoryItemTypeId { get; set; }

        /// <summary>
        /// Tên loại món.
        /// </summary>
        public string InventoryItemTypeName { get; set; } = null!;
    }
}