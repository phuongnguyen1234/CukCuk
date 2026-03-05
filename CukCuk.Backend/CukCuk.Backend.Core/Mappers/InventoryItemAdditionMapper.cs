﻿using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Mappers
{
    /// <summary>
    /// Mapper 2 chiều của addition
    /// </summary>
    /// Created by Phuong 26/02/2026
    public static class InventoryItemAdditionMapper
    {
        /// <summary>
        /// DTO -> entity
        /// </summary>
        public static InventoryItemAddition ToEntity(InventoryItemAdditionDTO dto)
        {
            if (dto == null) return null;

            return new InventoryItemAddition
            {
                InventoryItemAdditionName = dto.InventoryItemAdditionName,
                InventoryItemAdditionPrice = dto.InventoryItemAdditionPrice,
            };
        }

        /// <summary>
        /// Map giá trị từ DTO lên entity hiện có (dùng cho update). Không cập nhật Id.
        /// </summary>
        public static void UpdateFromDto(InventoryItemAddition existing, InventoryItemAdditionDTO dto)
        {
            if (existing == null) throw new ArgumentNullException(nameof(existing));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            existing.InventoryItemAdditionName = dto.InventoryItemAdditionName;
            existing.InventoryItemAdditionPrice = dto.InventoryItemAdditionPrice;
        }

        /// <summary>
        /// Entity -> DTO
        /// </summary>
        public static InventoryItemAdditionDTO ToDto(InventoryItemAddition entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new InventoryItemAdditionDTO
            {
                InventoryItemAdditionId = entity.InventoryItemAdditionId,
                InventoryItemAdditionName = entity.InventoryItemAdditionName,
                InventoryItemAdditionPrice = entity.InventoryItemAdditionPrice,
            };
        }
    }
}
