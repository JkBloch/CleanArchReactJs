using EmployeeManagement.Application.DTOs.Permissions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Validators.Permission
{
    public class CreatePermissionValidator : AbstractValidator<CreatePermissionDto>
    {
        public CreatePermissionValidator()
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
