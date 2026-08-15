using AutoMapper;
using EmployeeManagement.Application.DTOs.Users;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                    d => d.FullName,
                    o => o.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(
                    d => d.Role,
                    o => o.MapFrom(s =>
                        s.UserRoles.FirstOrDefault().Role.Name));

            CreateMap<CreateUserDto, User>();

            CreateMap<UpdateUserDto, User>();
        }
    }
}
