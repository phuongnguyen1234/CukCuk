using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Reflection;
using System.Linq;
using CukCuk.Backend.Core.Interfaces.Service;

namespace CukCuk.Backend.Api.Controllers
{
    /// <summary>
    /// Controller cơ sở chứa các cấu hình chung
    /// </summary>
    /// Created by Phuong 01/03/2026
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }

    /// <summary>
    /// Controller Generic xử lý các thao tác CRUD cơ bản
    /// </summary>
    /// <typeparam name="TDto">DTO dùng chung cho thêm/sửa/lấy</typeparam>
    public abstract class BaseCrudController<TDto> : BaseController
    {
        protected readonly IBaseCrudService<TDto> _baseService;

        protected BaseCrudController(IBaseCrudService<TDto> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
        {
            var items = await _baseService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<TDto>> GetById(Guid id)
        {
            var item = await _baseService.GetByIdAsync(id);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TDto>> Create([FromBody] TDto dto)
        {
            if (dto is null) return BadRequest();

            var newId = await _baseService.CreateAsync(dto);
            if (newId == Guid.Empty) return BadRequest();

            // Tự động gán ID lại cho DTO để trả về client (dùng Reflection)
            var idProp = typeof(TDto).GetProperty($"{typeof(TDto).Name.Replace("DTO", "")}Id") 
                         ?? typeof(TDto).GetProperty("Id");
            
            if (idProp != null && idProp.CanWrite)
            {
                idProp.SetValue(dto, newId);
            }

            return CreatedAtAction(nameof(GetById), new { id = newId }, dto);
        }

        [HttpPut("{id:guid}")]
        public virtual async Task<IActionResult> Update(Guid id, [FromBody] TDto dto)
        {
            if (dto is null) return BadRequest();
            // Lưu ý: Việc gán ID vào DTO nên được xử lý ở Service hoặc DTO phải có sẵn ID
            var updated = await _baseService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _baseService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}