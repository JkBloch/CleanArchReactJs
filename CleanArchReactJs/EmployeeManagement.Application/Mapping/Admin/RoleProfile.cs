using AutoMapper;
using EmployeeManagement.Application.DTOs.Admin.Roles;
using EmployeeManagement.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Mapping.Admin
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();

            CreateMap<CreateRoleDto, Role>();

            CreateMap<UpdateRoleDto, Role>();
        }
    }
}
