using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Entities
{
    /// <summary>
    /// Loại món: Món ăn, Đồ uống, Combo, Buffet...
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class InventoryItemType
    {
        /// <summary>
        /// Khóa chính loại món.
        /// </summary>
        public Guid InventoryItemTypeId { get; set; }

        /// <summary>
        /// Tên loại món.
        /// </summary>
        public string InventoryItemTypeName { get; set; } = null!;
    }
}
