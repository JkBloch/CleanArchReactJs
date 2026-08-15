using EmployeeManagement.Application.DTOs.State;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Validators.State
{
    public class UpdateStateValidator : AbstractValidator<UpdateStateDto>
    {
        public UpdateStateValidator()
        {
            RuleFor(x => x.Code)
                    .NotEmpty()
                    .MaximumLength(10);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }

    }
}
