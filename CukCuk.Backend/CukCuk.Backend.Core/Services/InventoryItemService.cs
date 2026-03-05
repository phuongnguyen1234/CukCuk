﻿using CukCuk.Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Services
{
    using CukCuk.Backend.Core.Interfaces.Repository;
    using CukCuk.Backend.Core.Interfaces.Service;
    using CukCuk.Backend.Core.Interfaces.File;
    using CukCuk.Backend.Core.Exceptions;
    using FluentValidation;
    using FluentValidation.Results;
    using System.Collections;
    using CukCuk.Backend.Core.Mappers;
    using System.Globalization;
    using CukCuk.Backend.Core.DTOs;

    /// <summary>
    /// service của item
    /// </summary>
    /// Created by Phuong 26/02/2026
    public class InventoryItemService : BaseService<InventoryItem>, IInventoryItemService
    {
        private readonly IInventoryItemRepository _inventoryRepository;
        private readonly IInventoryItemCategoryRepository _categoryRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IKitchenRepository _kitchenRepository;
        private readonly IInventoryItemTypeRepository _typeRepository;
        private readonly IFileStorageService _fileStorage;
        private readonly IValidator<InventoryItemRequestDTO> _dtoValidator;

        public InventoryItemService(
            IInventoryItemRepository repository,
            IInventoryItemCategoryRepository categoryRepository,
            IUnitRepository unitRepository,
            IKitchenRepository kitchenRepository,
            IInventoryItemTypeRepository typeRepository,
            IFileStorageService fileStorage,
            IValidator<InventoryItemRequestDTO> dtoValidator)
            : base(repository)
        {
            _inventoryRepository = repository;
            _categoryRepository = categoryRepository;
            _unitRepository = unitRepository;
            _kitchenRepository = kitchenRepository;
            _typeRepository = typeRepository;
            _fileStorage = fileStorage;
            _dtoValidator = dtoValidator ?? throw new ArgumentNullException(nameof(dtoValidator));
        }

        /// <summary>
        /// Lấy dữ liệu có phân trang và lọc
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<PagedResult<InventoryItemResponseDTO>> GetPagedAsync(int page, int pageSize, IEnumerable<FilterCriteria>? filters = null, string? search = null)
        {
            var paged = await _inventoryRepository.GetPagedAsync(page, pageSize, filters, search);

            // Lấy danh sách Category và Unit để map tên
            // Lưu ý: Nếu dữ liệu lớn, nên dùng cache hoặc join trong repository. Ở đây dùng GetAllAsync vì danh mục/đơn vị thường ít.
            var categories = await _categoryRepository.GetAllAsync();
            var units = await _unitRepository.GetAllAsync();
            var types = await _typeRepository.GetAllAsync();

            var dtoItems = paged.Items?.Select(item =>
            {
                var dto = InventoryItemMapper.ToResponseDto(item);
                
                var category = categories.FirstOrDefault(c => c.InventoryItemCategoryId == item.InventoryItemCategoryId);
                if (category != null) dto.InventoryItemCategoryName = category.InventoryItemCategoryName;

                var unit = units.FirstOrDefault(u => u.UnitId == item.UnitId);
                if (unit != null) dto.UnitName = unit.UnitName;

                var type = types.FirstOrDefault(t => t.InventoryItemTypeId == item.InventoryItemTypeId);
                if (type != null) dto.InventoryItemTypeName = type.InventoryItemTypeName;

                return dto;
            }) ?? Enumerable.Empty<InventoryItemResponseDTO>();

            return new PagedResult<InventoryItemResponseDTO>(dtoItems, paged.Page, paged.PageSize, paged.TotalItems);
        }

        /// <summary>
        /// Lấy món theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task<InventoryItemResponseDTO?> GetByIdAsync(Guid id)
        {
            var entity = await base.GetByIdAsync(id); // Gọi GetById của BaseService để lấy entity
            if (entity is null) return null;
            
            var dto = InventoryItemMapper.ToResponseDto(entity);

            // Lấy tên Category
            if (entity.InventoryItemCategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(entity.InventoryItemCategoryId.Value);
                dto.InventoryItemCategoryName = category?.InventoryItemCategoryName;
            }

            // Lấy tên Unit
            var unit = await _unitRepository.GetByIdAsync(entity.UnitId);
            dto.UnitName = unit?.UnitName;

            // Lấy danh sách Kitchen
            var kitchens = await _inventoryRepository.GetKitchensForItemAsync(id);
            dto.Kitchens = kitchens.Select(KitchenMapper.ToDto);

            // Lấy tên Type
            var type = await _typeRepository.GetByIdAsync(entity.InventoryItemTypeId);
            dto.InventoryItemTypeName = type?.InventoryItemTypeName;
            
            // Lấy danh sách chi tiết addition để hiển thị UI (Tên, Giá...)
            var additions = await _inventoryRepository.GetAdditionsForItemAsync(id);
            dto.Additions = additions.Select(InventoryItemAdditionMapper.ToDto);
            return dto;
        }

        /// <summary>
        /// Tạo món mới
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="imageStream"></param>
        /// <param name="imageFileName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exceptions.ValidationException"></exception>
        public async Task<Guid> CreateWithImageAsync(InventoryItemRequestDTO dto, Stream? imageStream, string? imageFileName, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            if (imageStream != null && !string.IsNullOrWhiteSpace(imageFileName))
            {
                dto.InventoryItemImage = imageFileName;
            }

            var validationContext = new ValidationContext<InventoryItemRequestDTO>(dto);
            var validationResultDto = await _dtoValidator.ValidateAsync(validationContext, cancellationToken);
            if (!validationResultDto.IsValid)
            {
                throw new Exceptions.ValidationException(BuildValidationData(validationResultDto));
            }

            var entity = InventoryItemMapper.ToEntity(dto);

            if (imageStream != null && !string.IsNullOrWhiteSpace(imageFileName))
            {
                var path = await _fileStorage.SaveFileAsync(imageStream, imageFileName, "uploads", cancellationToken);
                entity.InventoryItemImage = path;
            }

            var id = await base.CreateAsync(entity);

            // Nếu DTO có addition ids, chèn vào bảng nối
            if (dto.AdditionIds != null && dto.AdditionIds.Any())
            {
                await _inventoryRepository.AddItemAdditionsAsync(id, dto.AdditionIds);
            }

            // Nếu DTO có kitchen ids, chèn vào bảng nối
            if (dto.KitchenIds != null && dto.KitchenIds.Any())
            {
                await _inventoryRepository.AddItemKitchensAsync(id, dto.KitchenIds);
            }

            return id;
        }

        /// <summary>
        /// Sửa món
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <param name="imageStream"></param>
        /// <param name="imageFileName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exceptions.ValidationException"></exception>
        public async Task<bool> UpdateWithImageAsync(Guid id, InventoryItemRequestDTO dto, Stream? imageStream, string? imageFileName, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await base.GetByIdAsync(id);
            if (existing is null) return false;

            var previousImagePath = existing.InventoryItemImage;

            var replacingImage = imageStream != null && !string.IsNullOrWhiteSpace(imageFileName);
            if (replacingImage)
            {
                dto.InventoryItemImage = imageFileName;
            }

            var validationContext = new ValidationContext<InventoryItemRequestDTO>(dto);
            validationContext.RootContextData["ExcludeId"] = id;
            var validationResultDto = await _dtoValidator.ValidateAsync(validationContext, cancellationToken);
            if (!validationResultDto.IsValid)
            {
                throw new Exceptions.ValidationException(BuildValidationData(validationResultDto));
            }

            InventoryItemMapper.UpdateFromDto(existing, dto);

            if (replacingImage)
            {
                if (!string.IsNullOrWhiteSpace(previousImagePath))
                {
                    try
                    {
                        await _fileStorage.DeleteFileAsync(previousImagePath);
                    }
                    catch
                    {
                        // ignore
                    }
                }

                var path = await _fileStorage.SaveFileAsync(imageStream!, imageFileName!, "uploads", cancellationToken);
                existing.InventoryItemImage = path;
            }
            else
            {
                existing.InventoryItemImage = previousImagePath;
            }

            var updated = await base.UpdateAsync(existing);
            if (!updated) return false;

            // Đồng bộ associations (many-to-many)
            var currentAdditions = await _inventoryRepository.GetAdditionsForItemAsync(id);
            var currentAdditionIds = currentAdditions.Select(a => a.InventoryItemAdditionId).ToHashSet();
            var newAdditionIds = dto.AdditionIds?.Where(g => g != Guid.Empty).Distinct().ToHashSet() ?? new HashSet<Guid>();

            var toAdd = newAdditionIds.Except(currentAdditionIds).ToArray();
            var toRemove = currentAdditionIds.Except(newAdditionIds).ToArray();

            if (toAdd.Any())
            {
                await _inventoryRepository.AddItemAdditionsAsync(id, toAdd);
            }

            if (toRemove.Any())
            {
                await _inventoryRepository.RemoveItemAdditionsAsync(id, toRemove);
            }

            // Đồng bộ Kitchens (many-to-many)
            var currentKitchens = await _inventoryRepository.GetKitchensForItemAsync(id);
            var currentKitchenIds = currentKitchens.Select(k => k.KitchenId).ToHashSet();
            var newKitchenIds = dto.KitchenIds?.Where(g => g != Guid.Empty).Distinct().ToHashSet() ?? new HashSet<Guid>();

            var kitchensToAdd = newKitchenIds.Except(currentKitchenIds).ToArray();
            var kitchensToRemove = currentKitchenIds.Except(newKitchenIds).ToArray();

            if (kitchensToAdd.Any())
            {
                await _inventoryRepository.AddItemKitchensAsync(id, kitchensToAdd);
            }

            if (kitchensToRemove.Any())
            {
                await _inventoryRepository.RemoveItemKitchensAsync(id, kitchensToRemove);
            }

            return true;
        }

        /// <summary>
        /// Xóa món
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> DeleteAsync(Guid id)
        {
            // Xóa links trước (an toàn nếu có ràng buộc FK)
            await _inventoryRepository.RemoveAllItemAdditionsAsync(id);
            await _inventoryRepository.RemoveAllItemKitchensAsync(id);

            var existing = await base.GetByIdAsync(id);
            if (existing != null && !string.IsNullOrWhiteSpace(existing.InventoryItemImage))
            {
                try
                {
                    await _fileStorage.DeleteFileAsync(existing.InventoryItemImage);
                }
                catch
                {
                    // ignore
                }
            }

            return await base.DeleteAsync(id);
        }

        // quản lý addition của món

        // /// <summary>
        // /// Lấy danh sách id sở thích
        // /// </summary>
        // /// <param name="inventoryItemId"></param>
        // /// <returns></returns>
        // public async Task<IEnumerable<Guid>> GetAdditionIdsAsync(Guid inventoryItemId)
        // {
        //     var existing = await base.GetByIdAsync(inventoryItemId);
        //     if (existing == null) return Enumerable.Empty<Guid>();

        //     return await _inventoryRepository.GetAdditionIdsForItemAsync(inventoryItemId);
        // }

        // /// <summary>
        // /// Thêm danh sách sở thích cho món
        // /// </summary>
        // /// <param name="inventoryItemId"></param>
        // /// <param name="additionIds"></param>
        // /// <returns></returns>
        // /// <exception cref="ArgumentNullException"></exception>
        // public async Task<bool> AddAdditionsAsync(Guid inventoryItemId, IEnumerable<Guid> additionIds)
        // {
        //     if (additionIds == null) throw new ArgumentNullException(nameof(additionIds));

        //     var existing = await base.GetByIdAsync(inventoryItemId);
        //     if (existing == null) return false;

        //     var ids = additionIds.Where(g => g != Guid.Empty).Distinct().ToArray();
        //     if (!ids.Any()) return true; // nothing to do

        //     // Optionally: validate each additionId exists (requires addition repo) — skipped for now
        //     await _inventoryRepository.AddItemAdditionsAsync(inventoryItemId, ids);
        //     return true;
        // }

        // /// <summary>
        // /// Xóa danh sách sở thích của món
        // /// </summary>
        // /// <param name="inventoryItemId"></param>
        // /// <param name="additionIds"></param>
        // /// <returns></returns>
        // /// <exception cref="ArgumentNullException"></exception>
        // public async Task<bool> RemoveAdditionsAsync(Guid inventoryItemId, IEnumerable<Guid> additionIds)
        // {
        //     if (additionIds == null) throw new ArgumentNullException(nameof(additionIds));

        //     var existing = await base.GetByIdAsync(inventoryItemId);
        //     if (existing == null) return false;

        //     var ids = additionIds.Where(g => g != Guid.Empty).Distinct().ToArray();
        //     if (!ids.Any()) return true;

        //     await _inventoryRepository.RemoveItemAdditionsAsync(inventoryItemId, ids);
        //     return true;
        // }

        // /// <summary>
        // /// Xóa sở thích của món
        // /// </summary>
        // /// <param name="inventoryItemId"></param>
        // /// <param name="additionId"></param>
        // /// <returns></returns>
        // public async Task<bool> RemoveAdditionAsync(Guid inventoryItemId, Guid additionId)
        // {
        //     if (additionId == Guid.Empty) return true;

        //     var existing = await base.GetByIdAsync(inventoryItemId);
        //     if (existing == null) return false;

        //     await _inventoryRepository.RemoveItemAdditionsAsync(inventoryItemId, new[] { additionId });
        //     return true;
        // }

        /// <summary>
        /// Tạo code từ name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<string> GenerateNewCodeAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            int charsPerWord = 1;
            string potentialCode;
            const int maxNameBasedAttempts = 5; // Limit attempts to extend the code from the name
            int attempt = 0;

            while (attempt < maxNameBasedAttempts)
            {
                potentialCode = GenerateCodeFromName(name, charsPerWord);
                if (string.IsNullOrEmpty(potentialCode))
                {
                    // This can happen if the name only contains spaces or is empty
                    return string.Empty;
                }

                if (!await _inventoryRepository.ExistsByCodeAsync(potentialCode))
                {
                    return potentialCode;
                }

                charsPerWord++;
                attempt++;
            }

            // Fallback strategy: if the name-based generation fails, append a number
            string baseCode = GenerateCodeFromName(name, charsPerWord - 1);
            int numericSuffix = 2;
            while (true)
            {
                potentialCode = $"{baseCode}{numericSuffix}";
                if (!await _inventoryRepository.ExistsByCodeAsync(potentialCode))
                {
                    return potentialCode;
                }
                numericSuffix++;

                // Safety break to prevent an actual infinite loop, though highly unlikely
                if (numericSuffix > 999)
                {
                    throw new InvalidOperationException("Unable to generate a unique code.");
                }
            }
        }

        /// <summary>
        /// Phương thức helper tạo code từ name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="charsPerWord"></param>
        /// <returns></returns>
        private static string GenerateCodeFromName(string name, int charsPerWord)
        {
            if (string.IsNullOrWhiteSpace(name) || charsPerWord <= 0)
                return string.Empty;

            var normalizedName = RemoveDiacritics(name);
            var words = normalizedName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var codeBuilder = new StringBuilder();
            foreach (var word in words)
            {
                var part = word.Length >= charsPerWord ? word.Substring(0, charsPerWord) : word;
                codeBuilder.Append(part);
            }

            return codeBuilder.ToString().ToUpperInvariant();
        }

        /// <summary>
        /// Loại bỏ dấu trong từ
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

    }
}
