using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Entities
{
    /// <summary>
    /// Bảng liên kết giữa InventoryItem và InventoryItemAddition.
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class ItemAddition
    {
        /// <summary>
        /// Id món chính.
        /// </summary>
        public Guid InventoryItemId { get; set; }

        /// <summary>
        /// Id sở thích phục vụ.
        /// </summary>
        public Guid InventoryItemAdditionId { get; set; }
    }
}
