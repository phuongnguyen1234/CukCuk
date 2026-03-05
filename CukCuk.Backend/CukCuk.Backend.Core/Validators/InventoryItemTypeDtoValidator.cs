using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Interfaces.Repository;
using FluentValidation;
using System;

namespace CukCuk.Backend.Core.Validators
{
    /// <summary>
    /// Validator của InventoryItemTypeDTO
    /// </summary>
    /// Created by Phuong 28/02/2026
    public class InventoryItemTypeDtoValidator : AbstractValidator<InventoryItemTypeDTO>
    {
        private readonly IInventoryItemTypeRepository _repository;

        public InventoryItemTypeDtoValidator(IInventoryItemTypeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            // Tên loại món không được để trống, không quá 100 ký tự
            RuleFor(x => x.InventoryItemTypeName)
                .NotEmpty()
                .WithMessage("Tên loại món không được để trống.")
                .MaximumLength(100)
                .WithMessage("Tên loại món không được quá 100 ký tự.");
            
            // Có thể thêm rule check trùng tên nếu cần
        }
    }
}