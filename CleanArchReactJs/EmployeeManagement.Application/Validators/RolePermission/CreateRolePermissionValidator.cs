using EmployeeManagement.Application.DTOs.RolePermissions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Validators.RolePermission
{
    public class CreateRolePermissionValidator : AbstractValidator<CreateRolePermissionDto>
    {
        public CreateRolePermissionValidator()
        {
            RuleFor(x => x.RoleId)
                .NotEqual(Guid.Empty)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.PermissionId)
                .NotEqual(Guid.Empty)
                .NotNull()
                .NotEmpty();
        }
    }
}
