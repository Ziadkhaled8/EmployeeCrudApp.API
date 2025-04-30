using AutoMapper;
using EmployeeCrudApp.Application.Dtos;
using EmployeeCrudApp.Domain.Entities;

namespace EmployeeCrudApp.Infrastructure
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<Employee, GetEmployeeDto>();
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}
