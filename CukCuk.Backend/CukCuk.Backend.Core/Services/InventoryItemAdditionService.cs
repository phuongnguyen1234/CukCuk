using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Entities;
using CukCuk.Backend.Core.Exceptions;
using CukCuk.Backend.Core.Interfaces.Repository;
using CukCuk.Backend.Core.Interfaces.Service;
using CukCuk.Backend.Core.Mappers;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Services
{
    /// <summary>
    /// service của addition
    /// </summary>
    /// Created by Phuong 26/02/2026
    public class InventoryItemAdditionService : CrudService<InventoryItemAddition, InventoryItemAdditionDTO, IInventoryItemAdditionRepository>, IInventoryItemAdditionService
    {
        public InventoryItemAdditionService(
            IInventoryItemAdditionRepository repository,
            IValidator<InventoryItemAdditionDTO> validator)
            : base(repository, validator)
        {
        }

        /// <summary>
        /// Map entity -> DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override InventoryItemAdditionDTO MapToDto(InventoryItemAddition entity)
            => InventoryItemAdditionMapper.ToDto(entity);

        /// <summary>
        /// Map DTO -> entity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override InventoryItemAddition MapToEntity(InventoryItemAdditionDTO dto)
            => InventoryItemAdditionMapper.ToEntity(dto);

        /// <summary>
        /// Map DTO -> khi update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        protected override void MapToEntity(InventoryItemAddition entity, InventoryItemAdditionDTO dto)
            => InventoryItemAdditionMapper.UpdateFromDto(entity, dto);
    }
}
