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
    /// Mapper 2 chiều của Unit
    /// </summary>
    /// Created by Phuong 26/02/2026
    public static class KitchenMapper
    {
        /// <summary>
        /// DTO -> entity
        /// </summary>
        public static Kitchen ToEntity(KitchenDTO dto)
        {
            if (dto == null) return null;

            return new Kitchen
            {
                KitchenName = dto.KitchenName,
            };
        }

        /// <summary>
        /// Map giá trị từ DTO lên entity hiện có (dùng cho update). Không cập nhật Id.
        /// </summary>
        public static void UpdateFromDto(Kitchen existing, KitchenDTO dto)
        {
            if (existing == null) throw new ArgumentNullException(nameof(existing));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            existing.KitchenName = dto.KitchenName;
        }

        /// <summary>
        /// Entity -> DTO
        /// </summary>
        public static KitchenDTO ToDto(Kitchen entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new KitchenDTO
            {
                KitchenId = entity.KitchenId,
                KitchenName = entity.KitchenName,
            };
        }
    }
}
