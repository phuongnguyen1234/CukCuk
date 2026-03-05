using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Entities;
using CukCuk.Backend.Core.Interfaces.Service;
using CukCuk.Backend.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CukCuk.Backend.Api.Controllers
{
    public class UnitController : BaseCrudController<UnitDTO>
    {
        public UnitController(IUnitService service) : base(service)
        {
        }
    }
}
