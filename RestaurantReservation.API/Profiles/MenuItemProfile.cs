using AutoMapper;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()    
        {
            CreateMap<MenuItem, MenuItemWithoutDetailsDto>();
            CreateMap<MenuItem, MenuItemDto>();
            CreateMap<MenuItemDto, MenuItem>();
        }
    }
}
