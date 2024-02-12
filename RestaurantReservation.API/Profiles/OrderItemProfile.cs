using AutoMapper;
using RestaurantReservation.API.Models.OrderItems;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemWithoutDetailsDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderItem, OrderItemWithMenuItemDto>();
        }
    }
}
