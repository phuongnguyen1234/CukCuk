using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Interfaces.Repository;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Validators
{
    /// <summary>
    /// Validator của additionDTO
    /// </summary>
    /// Created by Phuong 26/02/2026
    public class InventoryItemAdditionDtoValidator : AbstractValidator<InventoryItemAdditionDTO>
    {
        private readonly IInventoryItemAdditionRepository _repository;

        public InventoryItemAdditionDtoValidator(IInventoryItemAdditionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            // tên sở thích ko null
            RuleFor(x => x.InventoryItemAdditionName)
                .NotEmpty()
                .WithMessage("Tên sở thích không được để trống.")
                .MaximumLength(100)
                .WithMessage("Tên sở thích không được quá 100 ký tự.")
                .MustAsync(async (dto, name, context, ct) =>
                {
                    if (string.IsNullOrWhiteSpace(name)) return false;

                    Guid? excludeId = null;
                    if (context.RootContextData.TryGetValue("ExcludeId", out var raw) && raw is Guid g && g != Guid.Empty)
                        excludeId = g;

                    var exists = await _repository.ExistsByNameAsync(name.Trim(), excludeId);
                    return !exists;
                })
                .WithMessage("Tên sở thích phục vụ đã tồn tại trong hệ thống. Vui lòng chọn tên khác.");

            // giá sở thích >= 0
            RuleFor(x => x.InventoryItemAdditionPrice)
                .GreaterThanOrEqualTo(0m)
                .WithMessage("Giá sở thích không được âm.");
        }
    }
}