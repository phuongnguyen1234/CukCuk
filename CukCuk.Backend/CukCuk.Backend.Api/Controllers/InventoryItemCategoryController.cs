using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CukCuk.Backend.Api.Controllers
{
    using CukCuk.Backend.Core.DTOs;
    using CukCuk.Backend.Core.Entities;
    using CukCuk.Backend.Core.Interfaces.Service;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class InventoryItemCategoryController : BaseCrudController<InventoryItemCategoryDTO>
    {
        public InventoryItemCategoryController(IInventoryItemCategoryService service) : base(service)
        {
        }
    }
}
