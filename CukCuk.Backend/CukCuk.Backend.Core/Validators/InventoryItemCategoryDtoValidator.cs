using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Interfaces.Repository;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Validators
{
    /// <summary>
    /// validator của categoryDTO
    /// </summary>
    /// Created by Phuong 27/02/2026
    public class InventoryItemCategoryDtoValidator : AbstractValidator<InventoryItemCategoryDTO>
    {
        private readonly IInventoryItemCategoryRepository _repository;

        public InventoryItemCategoryDtoValidator(IInventoryItemCategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            // tên nhóm ko null, ko quá 100 ký tự
            RuleFor(x => x.InventoryItemCategoryName)
                .NotEmpty()
                .WithMessage("Tên nhóm thực đơn không được để trống.")
                .MaximumLength(100)
                .WithMessage("Tên nhóm thực đơn không được quá 100 ký tự.");

            // mã nhóm ko null, duy nhất
            RuleFor(x => x.InventoryItemCategoryCode)
                .NotEmpty()
                .WithMessage("Mã nhóm thực đơn không được để trống.")
                .MustAsync(async (dto, code, context, ct) =>
                {
                    if (string.IsNullOrWhiteSpace(code)) return false;

                    Guid? excludeId = null;
                    if (context.RootContextData.TryGetValue("ExcludeId", out var raw) && raw is Guid g && g != Guid.Empty)
                        excludeId = g;

                    var exists = await _repository.ExistsByCodeAsync(code.Trim(), excludeId);
                    return !exists;
                })
                .WithMessage("Mã nhóm thực đơn đã tồn tại trong hệ thống. Vui lòng chọn mã khác.");
        }
    }
}