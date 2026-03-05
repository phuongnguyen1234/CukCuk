using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Entities;
using CukCuk.Backend.Core.Interfaces.Repository;
using CukCuk.Backend.Core.Interfaces.Service;
using CukCuk.Backend.Core.Mappers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Services
{
    /// <summary>
    /// Service của InventoryItemType
    /// </summary>
    /// Created by Phuong 28/02/2026
    public class InventoryItemTypeService : CrudService<InventoryItemType, InventoryItemTypeDTO, IInventoryItemTypeRepository>, IInventoryItemTypeService
    {
        public InventoryItemTypeService(IInventoryItemTypeRepository repository, IValidator<InventoryItemTypeDTO> validator)
            : base(repository, validator)
        {
        }

        /// <summary>
        /// Map entity -> DTO
        /// </summary>
        protected override InventoryItemTypeDTO MapToDto(InventoryItemType entity)
            => InventoryItemTypeMapper.ToDto(entity);

        /// <summary>
        /// Map DTO -> entity
        /// </summary>
        protected override InventoryItemType MapToEntity(InventoryItemTypeDTO dto)
            => InventoryItemTypeMapper.ToEntity(dto);

        /// <summary>
        /// Map DTO -> khi update
        /// </summary>
        protected override void MapToEntity(InventoryItemType entity, InventoryItemTypeDTO dto)
            => InventoryItemTypeMapper.UpdateFromDto(entity, dto);
    }
}