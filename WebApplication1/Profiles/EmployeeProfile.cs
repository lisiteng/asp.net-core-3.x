using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.Models;

namespace WebApplication1.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ForMember(dest => dest.name, opt => opt.MapFrom(src => $"{src.firstName} {src.lastName}"))
                .ForMember(dest => dest.genderDisplay, opt => opt.MapFrom(src => src.gender.ToString()))
                .ForMember(dest => dest.age, opt => opt.MapFrom(src => DateTime.Now.Year - src.birthday.Year));
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<Employee, UpdateEmployeeDto>();
        }
    }
}
