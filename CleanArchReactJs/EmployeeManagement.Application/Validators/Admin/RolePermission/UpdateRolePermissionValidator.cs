using EmployeeManagement.Application.DTOs.Admin.RolePermissions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Validators.Admin.RolePermission
{
    public class UpdateRolePermissionValidator : AbstractValidator<UpdateRolePermissionDto>
    {
        public UpdateRolePermissionValidator()
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
