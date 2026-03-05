using CukCuk.Backend.Core.Interfaces.Repository;
using CukCuk.Backend.Core.Interfaces.Service;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Services
{
    /// <summary>
    /// Lớp Base Service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// Created by Phuong 25/02/2026
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IBaseRepository<T> _repository;

        protected BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Lấy object T theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<T?> GetByIdAsync(Guid id)
            => _repository.GetByIdAsync(id);

        /// <summary>
        /// Lấy tất cả object T
        /// </summary>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> GetAllAsync()
            => _repository.GetAllAsync();

        /// <summary>
        /// Tạo object T mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<Guid> CreateAsync(T entity)
            => _repository.CreateAsync(entity);

        /// <summary>
        /// Sửa object T
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task<bool> UpdateAsync(T entity)
            => _repository.UpdateAsync(entity);

        /// <summary>
        /// Xóa object T
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<bool> DeleteAsync(Guid id)
            => _repository.DeleteAsync(id);
        
        /// <summary>
        /// Tạo dict lỗi validate
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected static IDictionary BuildValidationData(ValidationResult result)
        {
            var dict = new Dictionary<string, string>();
            foreach (var failureGroup in result.Errors
                .GroupBy(e => e.PropertyName, StringComparer.OrdinalIgnoreCase))
            {
                var messages = string.Join("; ", failureGroup.Select(e => e.ErrorMessage));
                var key = string.IsNullOrWhiteSpace(failureGroup.Key) ? "Validation" : failureGroup.Key;
                dict[key] = messages;
            }
            return dict;
        }

    }
}
