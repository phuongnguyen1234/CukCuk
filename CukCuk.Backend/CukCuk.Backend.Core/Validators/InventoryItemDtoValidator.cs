using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Interfaces.Repository;
using FluentValidation;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Validators
{
    /// <summary>
    /// validator của itemDTO
    /// </summary>
    /// Created by Phuong 26/02/2026
    public class InventoryItemDtoValidator : AbstractValidator<InventoryItemRequestDTO>
    {
        private readonly IInventoryItemRepository _repository;
        private readonly IUnitRepository _unitRepository;
        private readonly IInventoryItemCategoryRepository _categoryRepository;
        private readonly IInventoryItemTypeRepository _typeRepository;
        private readonly IInventoryItemAdditionRepository _additionRepository;
        private readonly IKitchenRepository _kitchenRepository;
        private static readonly string[] _allowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public InventoryItemDtoValidator(
            IInventoryItemRepository repository,
            IUnitRepository unitRepository,
            IInventoryItemCategoryRepository categoryRepository,
            IInventoryItemTypeRepository typeRepository,
            IInventoryItemAdditionRepository additionRepository,
            IKitchenRepository kitchenRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitRepository = unitRepository ?? throw new ArgumentNullException(nameof(unitRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _typeRepository = typeRepository ?? throw new ArgumentNullException(nameof(typeRepository));
            _additionRepository = additionRepository ?? throw new ArgumentNullException(nameof(additionRepository));
            _kitchenRepository = kitchenRepository ?? throw new ArgumentNullException(nameof(kitchenRepository));

            // tên ko dc null
            RuleFor(x => x.InventoryItemName)
                .NotEmpty()
                .WithMessage("Tên món không được để trống.")
                .MaximumLength(100)
                .WithMessage("Tên món không quá 100 ký tự.");

            // mã ko dc null, duy nhất
            RuleFor(x => x.InventoryItemCode)
                .NotEmpty()
                .WithMessage("Mã món không được để trống.")
                .MaximumLength(255)
                .WithMessage("Mã món không quá 255 ký tự.")
                .MustAsync(async (dto, code, context, ct) =>
                {
                    if (string.IsNullOrWhiteSpace(code)) return false;

                    Guid? excludeId = null;
                    if (context.RootContextData.TryGetValue("ExcludeId", out var raw) && raw is Guid g && g != Guid.Empty)
                        excludeId = g;

                    var exists = await _repository.ExistsByCodeAsync(code.Trim(), excludeId);
                    return !exists;
                })
                .WithMessage("Mã món đã tồn tại trong hệ thống. Vui lòng chọn mã khác.");

            // tên món ngôn ngữ khác không quá 500 ký tự (nếu có)
            When(x => !string.IsNullOrWhiteSpace(x.InventoryItemLangName), () =>
            {
                RuleFor(x => x.InventoryItemLangName)
                    .MaximumLength(500)
                    .WithMessage("Tên món ngôn ngữ khác không quá 500 ký tự.");
            });

            // đơn vị ko dc null
            RuleFor(x => x.UnitId)
                .NotEqual(Guid.Empty)
                .WithMessage("Đơn vị không được để trống.")
                .MustAsync(async (id, cancellationToken) => await _unitRepository.GetByIdAsync(id) != null)
                .WithMessage("Đơn vị tính không hợp lệ hoặc đã bị xóa.");

            // loại ko dc null
            RuleFor(x => x.InventoryItemTypeId)
                .NotEqual(Guid.Empty)
                .WithMessage("Loại không được để trống.")
                .MustAsync(async (id, cancellationToken) => await _typeRepository.GetByIdAsync(id) != null)
                .WithMessage("Loại món không hợp lệ hoặc đã bị xóa.");

            // nhóm thực đơn (nếu có) phải tồn tại
            When(x => x.InventoryItemCategoryId.HasValue && x.InventoryItemCategoryId.Value != Guid.Empty, () =>
            {
                RuleFor(x => x.InventoryItemCategoryId.Value)
                    .MustAsync(async (id, cancellationToken) => await _categoryRepository.GetByIdAsync(id) != null)
                    .WithMessage("Nhóm thực đơn không hợp lệ hoặc đã bị xóa.");
            });

            // sở thích phục vụ (nếu có) phải tồn tại
            When(x => x.AdditionIds != null && x.AdditionIds.Any(), () =>
            {
                RuleForEach(x => x.AdditionIds)
                    .NotEmpty().WithMessage("ID sở thích phục vụ không được rỗng.")
                    .MustAsync(async (id, cancellationToken) => await _additionRepository.GetByIdAsync(id) != null)
                    .WithMessage("Một hoặc nhiều sở thích phục vụ không hợp lệ hoặc đã bị xóa.");
            });

            // nơi chế biến (nếu có) phải tồn tại
            When(x => x.KitchenIds != null && x.KitchenIds.Any(), () =>
            {
                RuleForEach(x => x.KitchenIds)
                    .NotEmpty().WithMessage("ID nơi chế biến không được rỗng.")
                    .MustAsync(async (id, cancellationToken) => await _kitchenRepository.GetByIdAsync(id) != null)
                    .WithMessage("Một hoặc nhiều nơi chế biến không hợp lệ hoặc đã bị xóa.");
            });

            // thứ tự món phải là 1, 2 hoặc 3
            RuleFor(x => x.InventoryItemCourse)
                .Must(c => c == 1 || c == 2 || c == 3)
                .WithMessage("Thứ tự món phải là 1, 2 hoặc 3.");

            // giá >= 0
            RuleFor(x => x.InventoryItemPrice)
                .GreaterThanOrEqualTo(0m)
                .WithMessage("Giá bán không được âm.")
                .Must(price => price % 1 == 0)
                .WithMessage("Giá bán phải là số nguyên.");

            // giá vốn >= 0
            RuleFor(x => x.InventoryItemCostPrice)
                .GreaterThanOrEqualTo(0m)
                .WithMessage("Giá vốn không được âm.");

            // mô tả không quá 500 ký tự
            RuleFor(x => x.InventoryItemDescription)
                .MaximumLength(500)
                .WithMessage("Mô tả không quá 500 ký tự.");

            // ảnh phải có định dạng jpg, jpeg, png, gif (nếu có)
            When(x => !string.IsNullOrWhiteSpace(x.InventoryItemImage), () =>
            {
                RuleFor(x => x.InventoryItemImage)
                    .Must(HaveAllowedImageExtension)
                    .WithMessage("Ảnh chỉ có thể có định dạng jpg, jpeg, png, gif.");
            });
        }

        /// <summary>
        /// kiểm tra xem đường dẫn ảnh có phần mở rộng hợp lệ hay không
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private static bool HaveAllowedImageExtension(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) return true;
            try
            {
                var ext = Path.GetExtension(imagePath).ToLowerInvariant();
                return _allowedImageExtensions.Contains(ext);
            }
            catch
            {
                return false;
            }
        }
    }
}
