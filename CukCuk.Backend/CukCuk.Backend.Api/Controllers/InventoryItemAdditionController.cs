using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CukCuk.Backend.Core.Mappers;

namespace CukCuk.Backend.Api.Controllers
{
    public class InventoryItemAdditionController : BaseCrudController<InventoryItemAdditionDTO>
    {
        public InventoryItemAdditionController(IInventoryItemAdditionService service) : base(service)
        {
        }
    }
}
