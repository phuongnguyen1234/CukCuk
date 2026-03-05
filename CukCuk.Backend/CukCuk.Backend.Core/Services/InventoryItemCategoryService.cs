using CukCuk.Backend.Core.Entities;
using CukCuk.Backend.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Services
{
    using CukCuk.Backend.Core.DTOs;
    using CukCuk.Backend.Core.Interfaces.Repository;
    using CukCuk.Backend.Core.Mappers;
    using FluentValidation;

    /// <summary>
    /// service của category
    /// </summary>
    /// Created by Phuong 26/02/2026
    public class InventoryItemCategoryService : CrudService<InventoryItemCategory, InventoryItemCategoryDTO, IInventoryItemCategoryRepository>, IInventoryItemCategoryService
    {
        public InventoryItemCategoryService(IInventoryItemCategoryRepository repository, IValidator<InventoryItemCategoryDTO> validator)
            : base(repository, validator)
        {
        }

        /// <summary>
        /// Map entity -> DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override InventoryItemCategoryDTO MapToDto(InventoryItemCategory entity)
            => InventoryItemCategoryMapper.ToDto(entity);

        /// <summary>
        /// Map DTO -> entity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override InventoryItemCategory MapToEntity(InventoryItemCategoryDTO dto)
            => InventoryItemCategoryMapper.ToEntity(dto);

        /// <summary>
        /// Map DTO -> khi update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        protected override void MapToEntity(InventoryItemCategory entity, InventoryItemCategoryDTO dto)
            => InventoryItemCategoryMapper.UpdateFromDto(entity, dto);
    }
}
