using CukCuk.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Interfaces.Repository
{
    /// <summary>
    /// interface repo của InventoryItemAddition
    /// </summary>
    /// Created by Phuong 26/02/2026
    public interface IInventoryItemAdditionRepository : IBaseRepository<InventoryItemAddition>
    {
        /// <summary>
        /// Kiểm tra tên đã tồn tại
        /// </summary>
        /// <param name="name">tên cần kiểm tra</param>
        /// <param name="excludeId">id cần loại trừ (dùng khi update để ko check trùng với chính nó)</param>
        /// <returns>true nếu tồn tại, false nếu chưa tồn tại</returns>
        Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null);
    }
}
