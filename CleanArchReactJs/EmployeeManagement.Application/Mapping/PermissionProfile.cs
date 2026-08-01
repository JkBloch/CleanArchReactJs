using AutoMapper;
using EmployeeManagement.Application.DTOs.Permissions;
using EmployeeManagement.Application.DTOs.Users;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Mapping
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
