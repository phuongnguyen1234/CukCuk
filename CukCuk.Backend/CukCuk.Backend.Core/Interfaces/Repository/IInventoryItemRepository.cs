﻿using CukCuk.Backend.Core.DTOs;
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

        // Quản lý sở thích phục vụ của món

        /// <summary>
        /// Thêm danh sách id sở thích cho món
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <param name="additionIds"></param>
        /// <returns></returns>
        Task AddItemAdditionsAsync(Guid inventoryItemId, IEnumerable<Guid> additionIds);

        /// <summary>
        /// Xóa sở thích cho món
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <param name="additionIds"></param>
        /// <returns></returns>
        Task RemoveItemAdditionsAsync(Guid inventoryItemId, IEnumerable<Guid> additionIds);

        /// <summary>
        /// Lấy danh sách chi tiết sở thích của món
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        Task<IEnumerable<InventoryItemAddition>> GetAdditionsForItemAsync(Guid inventoryItemId);

        /// <summary>
        /// Xóa tất cả sở thích của món
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        Task RemoveAllItemAdditionsAsync(Guid inventoryItemId);

        // Quản lý nơi chế biến (Kitchen) của món

        /// <summary>
        /// Thêm danh sách id bếp cho món
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <param name="kitchenIds"></param>
        /// <returns></returns>
        Task AddItemKitchensAsync(Guid inventoryItemId, IEnumerable<Guid> kitchenIds);

        /// <summary>
        /// Xóa bếp khỏi món
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <param name="kitchenIds"></param>
        /// <returns></returns>
        Task RemoveItemKitchensAsync(Guid inventoryItemId, IEnumerable<Guid> kitchenIds);

        /// <summary>
        /// Xóa tất cả bếp của món
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        Task RemoveAllItemKitchensAsync(Guid inventoryItemId);

        /// <summary>
        /// Lấy danh sách bếp của món
        /// </summary>
        /// <param name="inventoryItemId"></param>
        /// <returns></returns>
        Task<IEnumerable<Kitchen>> GetKitchensForItemAsync(Guid inventoryItemId);
    }
}
