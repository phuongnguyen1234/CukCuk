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
    public class UnitService : CrudService<Unit, UnitDTO, IUnitRepository>, IUnitService
    {
        /// <summary>
        /// service của unit
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="validator"></param>
        /// Created by Phuong 26/02/2026
        public UnitService(IUnitRepository repository, IValidator<UnitDTO> validator)
            : base(repository, validator)
        {
        }

        /// <summary>
        /// Entity -> DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override UnitDTO MapToDto(Unit entity)
            => UnitMapper.ToDto(entity);

        /// <summary>
        /// DTO -> Entity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override Unit MapToEntity(UnitDTO dto)
            => UnitMapper.ToEntity(dto);

        /// <summary>
        /// DTO -> Entity (update)
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        protected override void MapToEntity(Unit entity, UnitDTO dto)
            => UnitMapper.UpdateFromDto(entity, dto);
    }
}
