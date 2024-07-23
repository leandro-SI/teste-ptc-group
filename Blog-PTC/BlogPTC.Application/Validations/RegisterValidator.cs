using BlogPTC.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Application.Validations
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.UserName)
                .NotNull().WithMessage("Username obrigatório.")
                .NotEmpty().WithMessage("Username obrigatório.")
                .MinimumLength(5).WithMessage("Use mais caracteres");

            RuleFor(u => u.Email)
                .NotNull().WithMessage("E-mail obrigatório.")
                .NotEmpty().WithMessage("E-mail obrigatório.")
                .MinimumLength(10).WithMessage("Use mais caracteres")
                .EmailAddress().WithMessage("E-mail inválido.");

            RuleFor(u => u.Password)
                .NotNull().WithMessage("Password obrigatório.")
                .NotEmpty().WithMessage("Password obrigatório.")
                .MinimumLength(6).WithMessage("Use mais caracteres");
        }
    }
}
