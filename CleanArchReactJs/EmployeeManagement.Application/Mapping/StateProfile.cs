using AutoMapper;
using EmployeeManagement.Application.DTOs.State;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Mapping
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<State, StateDto>();

            CreateMap<CreateStateDto, State>();

            CreateMap<UpdateStateDto, State>()
                .ForMember(x => x.Id,opt => opt.Ignore()) 
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())  
                .ForMember(x => x.CreatedBy, opt => opt.Ignore()) 
                .ForMember(x => x.IsDeleted, opt => opt.Ignore());
        }
    }
}
