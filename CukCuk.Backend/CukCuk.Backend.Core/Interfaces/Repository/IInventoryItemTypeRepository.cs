using CukCuk.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Interfaces.Repository
{
    /// <summary>
    /// Interface repository của InventoryItemType
    /// </summary>
    /// Created by Phuong 28/02/2026
    public interface IInventoryItemTypeRepository : IBaseRepository<InventoryItemType>
    {
        // Thêm các phương thức riêng nếu cần (ví dụ check trùng tên/mã)
    }
}