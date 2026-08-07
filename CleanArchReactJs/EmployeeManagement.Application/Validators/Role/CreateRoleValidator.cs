using EmployeeManagement.Application.DTOs.Roles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Validators.Role
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator()
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
