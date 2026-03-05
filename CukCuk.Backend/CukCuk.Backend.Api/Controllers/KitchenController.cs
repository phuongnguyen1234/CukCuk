using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Entities;
using CukCuk.Backend.Core.Interfaces.Service;
using CukCuk.Backend.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CukCuk.Backend.Api.Controllers
{
    public class KitchenController : BaseCrudController<KitchenDTO>
    {
        public KitchenController(IKitchenService service) : base(service)
        {
        }
    }
}
