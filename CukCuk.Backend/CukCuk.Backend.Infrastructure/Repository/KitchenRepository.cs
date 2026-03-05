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
    /// Repo của Unit
    /// </summary>
    /// Created by Phuong 25/02/2026
    public class KitchenRepository : BaseRepository<Kitchen>, IKitchenRepository
    {
        public KitchenRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }
    }
}
