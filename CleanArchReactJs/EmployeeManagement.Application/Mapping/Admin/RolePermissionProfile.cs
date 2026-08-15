using AutoMapper;
using EmployeeManagement.Application.DTOs.Admin.RolePermissions;
using EmployeeManagement.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Mapping.Admin
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile()
        {
            CreateMap<RolePermission, RolePermissionDto>();

            CreateMap<CreateRolePermissionDto, RolePermission>();

            CreateMap<UpdateRolePermissionDto, RolePermission>();
        }
    }
}
