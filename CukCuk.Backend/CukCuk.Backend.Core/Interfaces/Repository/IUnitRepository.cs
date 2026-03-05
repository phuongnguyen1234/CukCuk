using CukCuk.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Interfaces.Repository
{
    /// <summary>
    /// interface repo của Unit
    /// </summary>
    /// Create by Phuong 26/02/2026
    public interface IUnitRepository : IBaseRepository<Unit>
    {
        /// <summary>
        /// Check unit name đã tồn tại hay chưa
        /// </summary>
        /// <param name="unitName"></param>
        /// <param name="excludeId"></param>
        /// <returns></returns>
        Task<bool> ExistsByNameAsync(string unitName, Guid? excludeId = null);
    }
}
