using AutoMapper;
using EmployeeManagement.Application.DTOs.Roles;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Mapping
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
