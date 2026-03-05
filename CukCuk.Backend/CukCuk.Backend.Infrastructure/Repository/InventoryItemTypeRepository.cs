using CukCuk.Backend.Core.Entities;
using CukCuk.Backend.Core.Interfaces.Database;
using CukCuk.Backend.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Infrastructure.Repository
{
    /// <summary>
    /// Repo của InventoryItemType
    /// </summary>
    /// Created by Phuong 28/02/2026
    public class InventoryItemTypeRepository : BaseRepository<InventoryItemType>, IInventoryItemTypeRepository
    {
        public InventoryItemTypeRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }
    }
}