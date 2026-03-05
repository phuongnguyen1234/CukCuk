using CukCuk.Backend.Core.DTOs;
using CukCuk.Backend.Core.Entities;
using CukCuk.Backend.Core.Interfaces.Repository;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace CukCuk.Backend.Core.Validators
{
    /// <summary>
    /// Validator của kitchenDTO
    /// </summary>
    /// Created by Phuong 28/02/2026
    public class KitchenDtoValidator : AbstractValidator<KitchenDTO>
    {
        private readonly IKitchenRepository _repository;

        public KitchenDtoValidator(IKitchenRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            // tên nơi chế biến ko null, ko quá 100 ký tự
            RuleFor(x => x.KitchenName)
                .NotEmpty()
                .WithMessage("Tên nơi chế biến không được để trống.")
                .MaximumLength(100)
                .WithMessage("Tên nơi chế biến không được quá 100 ký tự.");


                
        }
    }
}