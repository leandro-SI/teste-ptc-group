using BlogPTC.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Application.Validations
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("E-mail obrigatório")
                .NotNull().WithMessage("E-mail obrigatório")
                .MinimumLength(10).WithMessage("Email inválido")
                .EmailAddress().WithMessage("E-mail inválido.");

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password obrigatório")
                .NotNull().WithMessage("Password obrigatório")
                .MinimumLength(5).WithMessage("Use mais caracteres");
        }
    }
}
