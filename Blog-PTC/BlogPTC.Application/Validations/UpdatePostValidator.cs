using BlogPTC.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Application.Validations
{
    public class UpdatePostValidator : AbstractValidator<UpdatePostDTO>
    {
        public UpdatePostValidator()
        {
            RuleFor(p => p.Id)
                .NotNull().WithMessage("Id obrigatório")
                .NotEmpty().WithMessage("Id obrigatório");

            RuleFor(p => p.Title)
                .NotNull().WithMessage("Título obrigatório")
                .NotEmpty().WithMessage("Título obrigatório")
                .MaximumLength(80).WithMessage("Use menos caracteres");

            RuleFor(p => p.Content)
                .NotNull().WithMessage("Conteúdo obrigatório")
                .NotEmpty().WithMessage("Conteúdo obrigatório")
                .MaximumLength(400).WithMessage("Use menos caracteres");
        }
    }
}
