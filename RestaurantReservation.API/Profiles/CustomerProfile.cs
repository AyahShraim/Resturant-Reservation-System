using AutoMapper;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<Customer, CustomersWithoutReservationsDto>();
            CreateMap<CustomerCreationDto, Customer>();
            CreateMap<Customer, CustomerCreationDto>();
        }
    }
}
