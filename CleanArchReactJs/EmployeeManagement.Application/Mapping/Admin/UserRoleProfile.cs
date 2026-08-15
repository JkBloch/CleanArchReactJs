using AutoMapper;
using EmployeeManagement.Application.DTOs.Admin.UserRoles;
using EmployeeManagement.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Mapping.Admin
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, UserRoleDto>().ForMember( 
                dest => dest.UserName, 
                opt => opt.MapFrom(src => src.User.UserName)
                );

            CreateMap<CreateUserRoleDto, UserRole>();

            CreateMap<UpdateUserRoleDto, UserRole>();
        }
    }
}
