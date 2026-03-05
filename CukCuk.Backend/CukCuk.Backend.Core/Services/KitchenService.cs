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
    public class KitchenService : CrudService<Kitchen, KitchenDTO, IKitchenRepository>, IKitchenService
    {
        /// <summary>
        /// service của kitchen
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="validator"></param>
        /// Created by Phuong 28/02/2026
        public KitchenService(IKitchenRepository repository, IValidator<KitchenDTO> validator)
            : base(repository, validator)
        {
        }

        /// <summary>
        /// Entity -> DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override KitchenDTO MapToDto(Kitchen entity)
            => KitchenMapper.ToDto(entity);

        /// <summary>
        /// DTO -> Entity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override Kitchen MapToEntity(KitchenDTO dto)
            => KitchenMapper.ToEntity(dto);

        /// <summary>
        /// DTO -> Entity (update)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        protected override void MapToEntity(Kitchen entity, KitchenDTO dto)
            => KitchenMapper.UpdateFromDto(entity, dto);
    }
}
