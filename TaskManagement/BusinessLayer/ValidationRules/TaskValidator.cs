using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class TaskValidator : AbstractValidator<EntityLayer.Concrete.Task>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("İsmi boş geçemezsin");
        }
    }
}
