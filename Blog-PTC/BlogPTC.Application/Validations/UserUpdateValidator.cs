using BlogPTC.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Application.Validations
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateValidator()
        {
            RuleFor(u => u.UserName)
                .NotNull().WithMessage("Username obrigatório")
                .NotEmpty().WithMessage("Username obrigatório")
                .MinimumLength(6).WithMessage("Use mais caracteres");

            RuleFor(u => u.Email)
                .NotNull().WithMessage("E-mail obrigatório.")
                .NotEmpty().WithMessage("E-mail obrigatório.")
                .MinimumLength(10).WithMessage("Use mais caracteres")
                .EmailAddress().WithMessage("E-mail inválido.");
        }
    }
}
