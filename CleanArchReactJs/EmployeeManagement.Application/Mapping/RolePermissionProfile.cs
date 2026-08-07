using AutoMapper;
using EmployeeManagement.Application.DTOs.RolePermissions;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Mapping
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
