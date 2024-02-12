using AutoMapper;
using RestaurantReservation.API.Models.Reservations;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.API.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationWithoutDetailsDto>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>();
        }
    }
}
