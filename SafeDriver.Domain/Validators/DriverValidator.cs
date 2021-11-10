using System;
using FluentValidation;
using SafeDriver.Domain.Entities;

namespace SafeDriver.Domain.Validators
{
    public class DriverValidator : AbstractValidator<Driver> 
    {
        public DriverValidator()
        {
            RuleFor(driver => driver.EmailAddress).EmailAddress().WithMessage("Endereço de e-mail inválido.");
            RuleFor(driver => driver.EmailAddress).NotEmpty().NotNull().WithMessage("Preencha o endereço de e-mail.");
            
            RuleFor(driver => driver.Name).MinimumLength(1).WithMessage("O nome deve ter entre 1 e 255 caracteres.");
            RuleFor(driver => driver.Name).MaximumLength(255).WithMessage("O nome deve ter entre 1 e 255 caracteres.");

            RuleFor(driver => driver.PhoneNumber).NotNull().NotEmpty().WithMessage("Preencha o número de telefone.");
            RuleFor(driver => driver.PhoneNumber).MinimumLength(6).MaximumLength(24).WithMessage("O número de telefone deve ter no mínimo 6 caracteres.");

            RuleFor(driver => driver.BirthDate).NotNull().NotEmpty().WithMessage("Preencha a data de nascimento.");
            
            RuleFor(driver => driver.BirthDate).LessThan(DateTime.Now.AddYears(-18)).WithMessage("O motorista precisa ser maior de idade.");
            
            RuleFor(driver => driver.DocumentNumber).NotNull().NotEmpty().WithMessage("Preencha o número do documento.");
            RuleFor(driver => driver.DocumentNumber).MinimumLength(6).WithMessage("O número do documento precisa ter no mínimo 6 caracteres.");
            RuleFor(driver => driver.DocumentNumber).MaximumLength(40).WithMessage("O número do documento pode ter no máximo 40 caracteres.");

            RuleFor(driver => driver.DriversLicenseNumber).NotNull().NotEmpty().WithMessage("Preencha o número da carteira de motorista.");
            RuleFor(driver => driver.DriversLicenseNumber).MinimumLength(6).WithMessage("O número da carteira de motorista precisa ter no mínimo 6 caracteres.");
            RuleFor(driver => driver.DriversLicenseNumber).MaximumLength(40).WithMessage("O número da carteira de motorista pode ter no máximo 40 caracteres.");

            RuleFor(driver => driver.AutomotiveInsuranceProvider).MinimumLength(2)
                .When(driver => string.IsNullOrEmpty(driver.AutomotiveInsuranceProvider) == false)
                .WithMessage("O nome do seguro precisa ter no mínimo 2 caracteres.");

            RuleFor(driver => driver.DriverLicenseExpireDate).GreaterThan(DateTime.Now)
                .WithMessage("Carteira de motorista não pode estar vencida.");    
        }
    }
}