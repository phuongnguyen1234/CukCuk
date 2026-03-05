using CukCuk.Backend.Core.Entities;

namespace CukCuk.Backend.Core.Interfaces.Repository
{
    /// <summary>
    /// interface repo của InventoryItemCategory
    /// </summary>
    /// Created by Phuong 26/02/2026
    public interface IInventoryItemCategoryRepository : IBaseRepository<InventoryItemCategory>
    {
        /// <summary>
        /// Kiểm tra code đã tồn tại
        /// </summary>
        /// <param name="code"></param>
        /// <param name="excludeId"></param>
        /// <returns></returns>
        Task<bool> ExistsByCodeAsync(string code, Guid? excludeId = null);
    }
}
