﻿using System;
using CukCuk.Backend.Core.Entities;
using CukCuk.Backend.Core.DTOs;

namespace CukCuk.Backend.Core.Mappers
{
    /// <summary>
    /// Mapper 2 chiều của item
    /// </summary>
    /// Created by Phuong 26/02/2026
    public static class InventoryItemMapper
    {
        /// <summary>
        /// RequestDTO -> entity (dùng cho Create)
        /// </summary>
        public static InventoryItem ToEntity(InventoryItemRequestDTO dto)
        {
            if (dto == null) return null;

            return new InventoryItem
            {
                InventoryItemId = dto.InventoryItemId ?? Guid.NewGuid(),
                InventoryItemCategoryId = dto.InventoryItemCategoryId,
                UnitId = dto.UnitId,
                InventoryItemTypeId = dto.InventoryItemTypeId,
                InventoryItemName = dto.InventoryItemName,
                InventoryItemCode = dto.InventoryItemCode,
                InventoryItemLangName = dto.InventoryItemLangName,
                InventoryItemCourse = dto.InventoryItemCourse,
                InventoryItemIsFeatured = dto.InventoryItemIsFeatured ?? false,
                InventoryItemPrice = dto.InventoryItemPrice,
                InventoryItemIsMarketPrice = dto.InventoryItemIsMarketPrice ?? false,
                InventoryItemAllowPriceOverride = dto.InventoryItemAllowPriceOverride ?? false,
                InventoryItemCostPrice = dto.InventoryItemCostPrice,
                InventoryItemDescription = dto.InventoryItemDescription ?? string.Empty,
                InventoryItemImage = dto.InventoryItemImage,
                InventoryItemIsOnSale = dto.InventoryItemIsOnSale ?? false
            };
        }

        /// <summary>
        /// Map giá trị từ RequestDTO lên entity hiện có (dùng cho update). Không ghi lên trường Image ở đây
        /// vì image thường được xử lý riêng (save/delete). Không cập nhật Id.
        /// </summary>
        public static void UpdateFromDto(InventoryItem entity, InventoryItemRequestDTO dto)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            entity.InventoryItemCategoryId = dto.InventoryItemCategoryId;
            entity.UnitId = dto.UnitId;
            entity.InventoryItemTypeId = dto.InventoryItemTypeId;
            entity.InventoryItemName = dto.InventoryItemName;
            entity.InventoryItemCode = dto.InventoryItemCode;
            entity.InventoryItemLangName = dto.InventoryItemLangName;
            entity.InventoryItemCourse = dto.InventoryItemCourse;
            entity.InventoryItemIsFeatured = dto.InventoryItemIsFeatured ?? false;
            entity.InventoryItemPrice = dto.InventoryItemPrice;
            entity.InventoryItemIsMarketPrice = dto.InventoryItemIsMarketPrice ?? false;
            entity.InventoryItemAllowPriceOverride = dto.InventoryItemAllowPriceOverride ?? false;
            entity.InventoryItemCostPrice = dto.InventoryItemCostPrice;
            entity.InventoryItemDescription = dto.InventoryItemDescription ?? string.Empty;
            entity.InventoryItemIsOnSale = dto.InventoryItemIsOnSale ?? false;
            // InventoryItemImage không cập nhật ở đây
        }

        /// <summary>
        /// Entity -> ResponseDTO
        /// </summary>
        public static InventoryItemResponseDTO ToResponseDto(InventoryItem entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new InventoryItemResponseDTO
            {
                InventoryItemId = entity.InventoryItemId,
                InventoryItemCategoryId = entity.InventoryItemCategoryId,
                UnitId = entity.UnitId,
                InventoryItemTypeId = entity.InventoryItemTypeId,
                InventoryItemName = entity.InventoryItemName,
                InventoryItemCode = entity.InventoryItemCode,
                InventoryItemLangName = entity.InventoryItemLangName,
                InventoryItemCourse = entity.InventoryItemCourse,
                InventoryItemIsFeatured = entity.InventoryItemIsFeatured,
                InventoryItemPrice = entity.InventoryItemPrice,
                InventoryItemIsMarketPrice = entity.InventoryItemIsMarketPrice,
                InventoryItemAllowPriceOverride = entity.InventoryItemAllowPriceOverride,
                InventoryItemCostPrice = entity.InventoryItemCostPrice,
                InventoryItemDescription = entity.InventoryItemDescription,
                InventoryItemImage = entity.InventoryItemImage,
                InventoryItemIsOnSale = entity.InventoryItemIsOnSale
            };
        }
    }
}
