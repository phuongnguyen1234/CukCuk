﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CukCuk.Backend.Api.Controllers
{
    using CukCuk.Backend.Core.Entities;
    using CukCuk.Backend.Core.Interfaces.Service;
    using CukCuk.Backend.Core.DTOs;
    using CukCuk.Backend.Core.Exceptions;
    using CukCuk.Backend.Core.Mappers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class InventoryItemController : BaseController
    {
        private readonly IInventoryItemService _service;

        public InventoryItemController(IInventoryItemService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lấy danh sách món có phân trang và lọc. Format lọc: "Column:Operator:Value". Hỗ trợ: eq, neq, gt, gte, lt, lte, contains... Giá trị có thể phân cách bằng dấu phẩy.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        /// Created by Phuong 26/02/2026
        [HttpGet]
        public async Task<ActionResult<PagedResult<InventoryItemResponseDTO>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery(Name = "filter")] List<string>? filters = null,
            [FromQuery] string? search = null)
        {
            var parsed = FilterQueryParser.Parse(filters);
            var paged = await _service.GetPagedAsync(page, pageSize, parsed, search);
            return Ok(paged);
        }

        /// <summary>
        /// Lấy món theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by Phuong 26/02/2026
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<InventoryItemResponseDTO>> GetById(Guid id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        /// <summary>
        /// Tạo món mới
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="image"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// Created by Phuong 26/02/2026
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<InventoryItemResponseDTO>> Create([FromForm] InventoryItemRequestDTO dto, IFormFile? image, CancellationToken cancellationToken = default)
        {
            if (dto is null) return BadRequest();

            var imageStream = image?.OpenReadStream();
            var imageFileName = image?.FileName;

            var newId = await _service.CreateWithImageAsync(dto, imageStream, imageFileName, cancellationToken);
            if (newId == Guid.Empty) return BadRequest();

            var createdDto = await _service.GetByIdAsync(newId);
            if (createdDto is null) return BadRequest(); // Should not happen, but for safety

            return CreatedAtAction(nameof(GetById), new { id = newId }, createdDto);
        }

        /// <summary>
        /// Sửa món
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <param name="image"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// Created by Phuong 26/02/2026
        [HttpPut("{id:guid}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(Guid id, [FromForm] InventoryItemRequestDTO dto, IFormFile? image, CancellationToken cancellationToken = default)
        {
            if (dto is null) return BadRequest();

            var imageStream = image?.OpenReadStream();
            var imageFileName = image?.FileName;

            var updated = await _service.UpdateWithImageAsync(id, dto, imageStream, imageFileName, cancellationToken);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Xóa món
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by Phuong 26/02/2026
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Tạo code từ tên món
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("generate-code")]
        public async Task<ActionResult<string>> GenerateCode([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name cannot be empty.");
            }
            var code = await _service.GenerateNewCodeAsync(name);
            return Ok(code);
        }
    }
}
