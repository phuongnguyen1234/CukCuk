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
    public static class UnitMapper
    {
        /// <summary>
        /// DTO -> entity
        /// </summary>
        public static Unit ToEntity(UnitDTO dto)
        {
            if (dto == null) return null;

            return new Unit
            {
                UnitName = dto.UnitName,
                UnitDescription = dto.UnitDescription ?? string.Empty,
            };
        }

        /// <summary>
        /// Map giá trị từ DTO lên entity hiện có (dùng cho update). Không cập nhật Id.
        /// </summary>
        public static void UpdateFromDto(Unit existing, UnitDTO dto)
        {
            if (existing == null) throw new ArgumentNullException(nameof(existing));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            existing.UnitName = dto.UnitName;
            existing.UnitDescription = dto.UnitDescription ?? string.Empty;
        }

        /// <summary>
        /// Entity -> DTO
        /// </summary>
        public static UnitDTO ToDto(Unit entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new UnitDTO
            {
                UnitId = entity.UnitId,
                UnitName = entity.UnitName,
                UnitDescription = entity.UnitDescription,
            };
        }
    }
}
