using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Entities;
using System;

namespace CukCuk.Backend.Core.Mappers
{
    /// <summary>
    /// Mapper 2 chiều của InventoryItemType
    /// </summary>
    /// Created by Phuong 28/02/2026
    public static class InventoryItemTypeMapper
    {
        /// <summary>
        /// DTO -> entity
        /// </summary>
        public static InventoryItemType ToEntity(InventoryItemTypeDTO dto)
        {
            if (dto == null) return null;

            return new InventoryItemType
            {
                InventoryItemTypeName = dto.InventoryItemTypeName,
            };
        }

        /// <summary>
        /// Map giá trị từ DTO lên entity hiện có (dùng cho update). Không cập nhật Id.
        /// </summary>
        public static void UpdateFromDto(InventoryItemType existing, InventoryItemTypeDTO dto)
        {
            if (existing == null) throw new ArgumentNullException(nameof(existing));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            existing.InventoryItemTypeName = dto.InventoryItemTypeName;
        }

        /// <summary>
        /// Entity -> DTO
        /// </summary>
        public static InventoryItemTypeDTO ToDto(InventoryItemType entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new InventoryItemTypeDTO
            {
                InventoryItemTypeId = entity.InventoryItemTypeId,
                InventoryItemTypeName = entity.InventoryItemTypeName,
            };
        }
    }
}