using AutoMapper;
using RestaurantReservation.API.Models.Tables;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<Table, TableWithoutDetailsDto>();
            CreateMap<Table, TableDto>();
            CreateMap<TableDto, Table>();
        }
    }
}
