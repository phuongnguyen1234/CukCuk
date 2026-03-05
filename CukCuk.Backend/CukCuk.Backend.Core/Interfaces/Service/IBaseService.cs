using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Interfaces.Service
{
    /// <summary>
    /// interface base service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// Created by Phuong 26/02/2026
    public interface IBaseService<T>
    {
        /// <summary>
        /// Lấy object T theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy tất cả object T
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Tạo object T
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Guid> CreateAsync(T entity);

        /// <summary>
        /// Sửa object T
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Xóa object T
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
