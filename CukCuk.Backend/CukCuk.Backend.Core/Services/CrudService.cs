using CukCuk.Backend.Core.Exceptions;
using CukCuk.Backend.Core.Interfaces.Repository;
using CukCuk.Backend.Core.Interfaces.Service;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Services
{
    /// <summary>
    /// Cung cấp logic CRUD cơ bản, chung cho các service đơn giản làm việc với DTO.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu entity</typeparam>
    /// <typeparam name="TDto">Kiểu DTO</typeparam>
    /// <typeparam name="TRepository">Kiểu repository cụ thể</typeparam>
    /// Created by Phuong 26/02/2026
    public abstract class CrudService<TEntity, TDto, TRepository> : BaseService<TEntity>, IBaseCrudService<TDto>
        where TEntity : class
        where TDto : class
        where TRepository : class, IBaseRepository<TEntity>
    {
        protected readonly IValidator<TDto> _validator;
        protected new readonly TRepository _repository;

        protected CrudService(TRepository repository, IValidator<TDto> validator)
            : base(repository)
        {
            _repository = repository;
            _validator = validator;
        }

        #region phương thức trừu tượng để map
        /// <summary>
        /// Lớp con phải định nghĩa cách ánh xạ từ Entity sang DTO.
        /// </summary>
        protected abstract TDto MapToDto(TEntity entity);

        /// <summary>
        /// Lớp con phải định nghĩa cách ánh xạ từ DTO sang Entity (để tạo mới).
        /// </summary>
        protected abstract TEntity MapToEntity(TDto dto);

        /// <summary>
        /// Lớp con phải định nghĩa cách cập nhật một Entity từ một DTO.
        /// </summary>
        protected abstract void MapToEntity(TEntity entity, TDto dto);
        #endregion

        #region triển khai CRUD
        
        /// <summary>
        /// Tạo object
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exceptions.ValidationException"></exception>
        public virtual async Task<Guid> CreateAsync(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(BuildValidationData(validationResult));
            }

            var entity = MapToEntity(dto);
            return await base.CreateAsync(entity);
        }

        /// <summary>
        /// Sửa object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exceptions.ValidationException"></exception>
        public virtual async Task<bool> UpdateAsync(Guid id, TDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(BuildValidationData(validationResult));
            }

            var existingEntity = await base.GetByIdAsync(id);
            if (existingEntity == null) return false;

            MapToEntity(existingEntity, dto);
            return await base.UpdateAsync(existingEntity);
        }

        /// <summary>
        /// Lấy tất cả object
        /// </summary>
        /// <returns></returns>
        public new virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(MapToDto);
        }

        /// <summary>
        /// Lấy object theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new virtual async Task<TDto?> GetByIdAsync(Guid id)
        {
            var entity = await base.GetByIdAsync(id);
            return entity == null ? null : MapToDto(entity);
        }
        #endregion
    }
}
