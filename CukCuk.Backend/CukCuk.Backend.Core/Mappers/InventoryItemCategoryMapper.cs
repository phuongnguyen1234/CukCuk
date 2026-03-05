using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Mappers
{
    /// <summary>
    /// Mapper 2 chiều của Category
    /// </summary>
    /// Created by Phuong 26/02/2026
    public static class InventoryItemCategoryMapper
    {
        /// <summary>
        /// DTO -> entity
        /// </summary>
        public static InventoryItemCategory ToEntity(InventoryItemCategoryDTO dto)
        {
            if (dto == null) return null;

            return new InventoryItemCategory
            {
                InventoryItemCategoryName = dto.InventoryItemCategoryName,
                InventoryItemCategoryCode = dto.InventoryItemCategoryCode,
            };
        }

        /// <summary>
        /// Map giá trị từ DTO lên entity hiện có (dùng cho update). Không cập nhật Id.
        /// </summary>
        public static void UpdateFromDto(InventoryItemCategory existing, InventoryItemCategoryDTO dto)
        {
            if (existing == null) throw new ArgumentNullException(nameof(existing));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            existing.InventoryItemCategoryName = dto.InventoryItemCategoryName;
            existing.InventoryItemCategoryCode = dto.InventoryItemCategoryCode;
        }

        /// <summary>
        /// Entity -> DTO
        /// </summary>
        public static InventoryItemCategoryDTO ToDto(InventoryItemCategory entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new InventoryItemCategoryDTO
            {
                InventoryItemCategoryId = entity.InventoryItemCategoryId,
                InventoryItemCategoryName = entity.InventoryItemCategoryName,
                InventoryItemCategoryCode = entity.InventoryItemCategoryCode,
            };
        }
    }
}
