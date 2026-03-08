﻿﻿﻿using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace CukCuk.Backend.Core.Interfaces.Repository
{
    /// <summary>
    /// interface repo của InventoryItem
    /// </summary>
    /// Created by Phuong 26/02/2026
    public interface IInventoryItemRepository : IBaseRepository<InventoryItem>
    {
        /// <summary>
        /// Lấy dữ liệu có phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<PagedResult<InventoryItem>> GetPagedAsync(int page, int pageSize, IEnumerable<FilterCriteria>? filters = null, string? search = null);

        /// <summary>
        /// Kiểm tra code đã tồn tại
        /// </summary>
        /// <param name="code"></param>
        /// <param name="excludeId"></param>
        /// <returns></returns>
        Task<bool> ExistsByCodeAsync(string code, Guid? excludeId = null);

        Task<Guid> CreateWithRelationsAsync(InventoryItem item, 
        IEnumerable<Guid> additionIds, 
        IEnumerable<Guid> kitchenIds);

        Task<bool> UpdateWithRelationsAsync(InventoryItem item, 
        IEnumerable<Guid> addAdditions, 
        IEnumerable<Guid> removeAdditions, 
        IEnumerable<Guid> addKitchens, 
        IEnumerable<Guid> removeKitchens);

        Task<bool> DeleteWithRelationsAsync(Guid id);
        // Quản lý sở thích phục vụ của món

        /// <summary>
        /// Lấy danh sách chi tiết sở thích của món
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        Task<IEnumerable<InventoryItemAddition>> GetAdditionsForItemAsync(Guid inventoryItemId);

        // Quản lý nơi chế biến (Kitchen) của món

        /// <summary>
        /// Lấy danh sách bếp của món
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        Task<IEnumerable<Kitchen>> GetKitchensForItemAsync(Guid inventoryItemId);
    }
}
