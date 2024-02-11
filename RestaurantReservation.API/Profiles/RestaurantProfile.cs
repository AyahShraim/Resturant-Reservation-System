using AutoMapper;
using RestaurantReservation.API.Models.Restaurants;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantWithoutDetailsDto>();
            CreateMap<Restaurant, RestaurantWithDetails>();
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantDto, Restaurant>();
        }
    }
}
