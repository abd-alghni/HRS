using AutoMapper;
using HRS.Core.Dtos;
using HRS.Core.ViewModels;
using HRS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRS.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            
            CreateMap<CreateEmployeeDto, Employee>().ForMember(x => x.Image, x => x.Ignore());
            CreateMap<UpdateEmployeeDto, Employee>().ForMember(x => x.Image, x => x.Ignore());
            CreateMap<Employee, UpdateEmployeeDto>().ForMember(x => x.Image, x => x.Ignore());
            CreateMap<Employee, EmployeeViewModel>();

            CreateMap<Salary, SalaryViewModel>();
            CreateMap<CreateSalaryDto, Salary>();
            CreateMap<UpdateSalaryDto, Salary>();
            CreateMap<Salary, UpdateSalaryDto>();


            CreateMap<Department, DepartmentViewModel>();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();
            CreateMap<Department, UpdateDepartmentDto>();



            CreateMap<CreateHolidayDto, Holiday>();
            CreateMap<UpdateHolidayDto, Holiday>();
            CreateMap<Holiday, UpdateHolidayDto>();
            CreateMap<Holiday, HolidayViewModel>()
                .ForMember(x => x.StartDate, x => x.MapFrom(x => x.StartDate.ToString("yyyy/MM/dd")))
                .ForMember(x => x.EndDate, x => x.MapFrom(x => x.EndDate.ToString("yyyy/MM/dd")))
                .ForMember(x => x.Status, x => x.MapFrom(x => x.Status.ToString()));


            CreateMap<Taask, TaaskViewModel>()
                .ForMember(x => x.StartTime, x => x.MapFrom(x => x.StartTime.ToString("yyyy/MM/dd")))
                .ForMember(x => x.EndTime, x => x.MapFrom(x => x.EndTime.ToString("yyyy/MM/dd")))
                .ForMember(x => x.Status, x => x.MapFrom(x => x.Status.ToString())); ;
            CreateMap<CreateTaaskDto, Taask>();
            CreateMap<UpdateTaaskDto, Taask>();
            CreateMap<Taask, UpdateTaaskDto>();

            CreateMap<ContentChangeLog, ContentChangeLogViewModel>();

        }
    }
}
