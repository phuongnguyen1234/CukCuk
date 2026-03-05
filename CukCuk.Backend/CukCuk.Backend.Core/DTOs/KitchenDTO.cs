using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Entities
{
    /// <summary>
    /// Nơi chế biến (Bếp 1, Bar...).
    /// </summary>
    /// Created by Phuong 28/02/2026
    public class KitchenDTO
    {
        /// <summary>
        /// Khóa chính nơi chế biến.
        /// </summary>
        public Guid? KitchenId { get; set; }

        /// <summary>
        /// Tên nơi chế biến.
        /// </summary>
        public string KitchenName { get; set; } = null!;
    }
}
