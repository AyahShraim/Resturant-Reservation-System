using AutoMapper;
using RestaurantReservation.API.Models.Orders;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderWithoutDetailsDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderWithOrdersItemsDto>();
        }   
    }
}
