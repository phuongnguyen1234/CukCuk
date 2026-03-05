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
                .WithMessage("Tên sở thích không được để trống.");

            // giá sở thích >= 0
            RuleFor(x => x.InventoryItemAdditionPrice)
                .GreaterThanOrEqualTo(0m)
                .WithMessage("Giá sở thích không được âm.");
        }
    }
}