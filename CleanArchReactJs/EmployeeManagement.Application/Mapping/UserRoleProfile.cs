using AutoMapper;
using EmployeeManagement.Application.DTOs.UserRoles;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Mapping
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
