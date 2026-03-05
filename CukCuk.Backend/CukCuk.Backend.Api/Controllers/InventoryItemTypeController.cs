using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CukCuk.Backend.Api.Controllers
{
    public class InventoryItemTypeController : BaseCrudController<InventoryItemTypeDTO>
    {
        public InventoryItemTypeController(IInventoryItemTypeService service) : base(service)
        {
        }
    }
}