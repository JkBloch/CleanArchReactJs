using AutoMapper;
using EmployeeManagement.Application.DTOs.Admin.Permissions;
using EmployeeManagement.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Mapping.Admin
{
    public class PermissionProfile :Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionDto>();            

            CreateMap<CreatePermissionDto, Permission>();

            CreateMap<UpdatePermissionDto, Permission>();
        }
    }
}
