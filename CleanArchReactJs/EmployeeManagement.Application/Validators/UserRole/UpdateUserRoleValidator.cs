using EmployeeManagement.Application.DTOs.UserRoles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Validators.UserRole
{
    public class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleDto>
    {
        public UpdateUserRoleValidator()
        {
            RuleFor(x => x.RoleId)
                 .NotEqual(Guid.Empty)
                 .NotNull()
                 .NotEmpty();

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .NotNull()
                .NotEmpty();
        }

    }

}
