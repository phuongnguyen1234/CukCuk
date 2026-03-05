using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CukCuk.Backend.Infrastructure.Repository
{
    using CukCuk.Backend.Core.Entities;
    using CukCuk.Backend.Core.Interfaces.Database;
    using CukCuk.Backend.Core.Interfaces.Repository;

    /// <summary>
    /// Repo của InventoryItemAddition
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class InventoryItemAdditionRepository : BaseRepository<InventoryItemAddition>, IInventoryItemAdditionRepository
    {
        public InventoryItemAdditionRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }
    }
}
