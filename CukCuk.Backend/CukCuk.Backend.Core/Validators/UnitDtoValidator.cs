using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Interfaces.Repository;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Validators
{
    /// <summary>
    /// Validator của unitDTO
    /// </summary>
    /// Created by Phuong 26/02/2026
    public class UnitDtoValidator : AbstractValidator<UnitDTO>
    {
        private readonly IUnitRepository _repository;

        public UnitDtoValidator(IUnitRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            // tên đơn vị ko null, không quá 100 ký tự
            RuleFor(x => x.UnitName)
                .NotEmpty()
                .WithMessage("Tên đơn vị tính không được để trống.")
                .MaximumLength(100)
                .WithMessage("Tên đơn vị tính không được quá 100 ký tự.");
            
            // tên đơn vị là duy nhất
            RuleFor(x => x.UnitName)
                .MustAsync(async (dto, unitName, context, ct) =>
                {
                    if (string.IsNullOrWhiteSpace(unitName)) return false;

                    Guid? excludeId = null;
                    if (context.RootContextData.TryGetValue("ExcludeId", out var raw) && raw is Guid g && g != Guid.Empty)
                        excludeId = g;

                    var exists = await _repository.ExistsByNameAsync(unitName.Trim(), excludeId);
                    return !exists;
                })
                .WithMessage("Tên đơn vị tính đã tồn tại trong hệ thống. Vui lòng chọn tên khác.");

            // mô tả không quá 500 ký tự
            RuleFor(x => x.UnitDescription)
                .MaximumLength(500)
                .WithMessage("Mô tả không được vượt quá 500 ký tự.");
                
        }
    }
}