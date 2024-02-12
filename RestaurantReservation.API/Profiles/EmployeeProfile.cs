using AutoMapper;
using RestaurantReservation.API.Models.Employees;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeWithoutDetailsDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
