﻿
using CukCuk.Backend.Core.Entities;
using CukCuk.Backend.Core.DTOs;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CukCuk.Backend.Core.Interfaces.Service
{
    /// <summary>
    /// interface service của inventory item
    /// </summary>
    public interface IInventoryItemService
    {
        /// <summary>
        /// Lấy món theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InventoryItemResponseDTO?> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy danh sách món có phân trang và lọc
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<PagedResult<InventoryItemResponseDTO>> GetPagedAsync(int page, int pageSize, IEnumerable<FilterCriteria>? filters = null, string? search = null);

        /// <summary>
        /// Thêm món mới
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="imageStream"></param>
        /// <param name="imageFileName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> CreateWithImageAsync(InventoryItemRequestDTO dto, Stream? imageStream, string? imageFileName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sửa món
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <param name="imageStream"></param>
        /// <param name="imageFileName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UpdateWithImageAsync(Guid id, InventoryItemRequestDTO dto, Stream? imageStream, string? imageFileName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Xóa món
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);

        // // Quản lý sở thích phục vụ của món
        // Task<IEnumerable<Guid>> GetAdditionIdsAsync(Guid inventoryItemId);

        // /// <summary>
        // /// Thêm danh sách sở thích cho món
        // /// </summary>
        // /// <param name="inventoryItemId"></param>
        // /// <param name="additionIds"></param>
        // /// <returns></returns>
        // Task<bool> AddAdditionsAsync(Guid inventoryItemId, IEnumerable<Guid> additionIds);

        // /// <summary>
        // /// Xóa danh sách sở thích cho món
        // /// </summary>
        // /// <param name="inventoryItemId"></param>
        // /// <param name="additionIds"></param>
        // /// <returns></returns>
        // Task<bool> RemoveAdditionsAsync(Guid inventoryItemId, IEnumerable<Guid> additionIds);

        // /// <summary>
        // /// Xóa sở thích cho món
        // /// </summary>
        // /// <param name="inventoryItemId"></param>
        // /// <param name="additionId"></param>
        // /// <returns></returns>
        // Task<bool> RemoveAdditionAsync(Guid inventoryItemId, Guid additionId);

        /// <summary>
        /// Tạo code theo tên món
        /// </summary>
        /// <param name="name">Tên món</param>
        /// <returns>Code gợi ý</returns>
        Task<string> GenerateNewCodeAsync(string name);
    }
}
