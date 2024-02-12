﻿using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.API.Models.Orders;
using RestaurantReservation.API.Models.Reservations;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/reservations")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;
        public ReservationsController(ReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationWithoutDetailsDto>>> GetReservations(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (reservationEntities, paginationMetaData) = await _reservationRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<ReservationWithoutDetailsDto>>(reservationEntities));
        }

        [HttpGet("{id}", Name = "GetReservation")]
        public async Task<IActionResult> Get(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                var error = ReservationErrors.NotFound(id);
                return NotFound(error);
            }

            return Ok(_mapper.Map<ReservationWithoutDetailsDto>(reservation));
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(ReservationDto reservationDto)
        {
            var matchingRestaurantTable = await _reservationRepository.MatchingTableRestaurant(reservationDto.RestaurantId,reservationDto.TableId);
            if (!matchingRestaurantTable )
            {
                var error = ReservationErrors.NotMatchingTableRestaurant(reservationDto.RestaurantId, reservationDto.TableId);
                return NotFound(error);
            }
            var reservationToAdd = _mapper.Map<Reservation>(reservationDto);
            await _reservationRepository.AddAsync(reservationToAdd);
            var reservationToReturn = _mapper.Map<ReservationWithoutDetailsDto>(reservationToAdd);
            return CreatedAtRoute("GetReservation", new { id = reservationToReturn.Id }, reservationToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(id);
            if (existingReservation == null)
            {
                var error = ReservationErrors.NotFound(id);
                return NotFound(error);
            }
            await _reservationRepository.DeleteAsync(existingReservation);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ReservationDto reservation)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(id);
            if (existingReservation == null)
            {
                var error = ReservationErrors.NotFound(id);
                return NotFound(error);
            }

            var matchingRestaurantTable = await _reservationRepository.MatchingTableRestaurant(reservation.RestaurantId, reservation.TableId);
            if (!matchingRestaurantTable)
            {
                var error = ReservationErrors.NotMatchingTableRestaurant(reservation.RestaurantId, reservation.TableId);
                return NotFound(error);
            }

            _mapper.Map(reservation, existingReservation);
            await _reservationRepository.UpdateAsync(existingReservation);
            return NoContent();
        }


        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<ReservationWithoutDetailsDto>>> GetReservationForCustomer(int customerId)
        {
            var reservation = await _reservationRepository.GetReservationsByCustomerAsync(customerId);
            if (reservation.Count() == 0)
            {
                var error = ReservationErrors.NoReservationFound(customerId);
                return NotFound(error);
            }
            return Ok(_mapper.Map<IEnumerable<ReservationWithoutDetailsDto>>(reservation));
        }

        [HttpGet("{reservationId}/orders")]
        public async Task<ActionResult<IEnumerable<OrderWithOrdersItemsDto>>> GetReservationOrdersWithRelatedMenuItems(int reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (reservation == null)
            {
                var error = ReservationErrors.NotFound(reservationId);
                return NotFound(error);
            }
            var details = await _reservationRepository.ListOrdersAndMenuItemsAsync(reservationId);

            return Ok(_mapper.Map<IEnumerable<OrderWithOrdersItemsDto>>(details));
        }

        [HttpGet("{reservationId}/menu-items")]
        public async Task<ActionResult<IEnumerable<MenuItemWithoutDetailsDto>>> GetReservationOrderedMenuItems(int reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (reservation == null)
            {
                var error = ReservationErrors.NotFound(reservationId);
                return NotFound(error);
            }
            var reservationOrderedMenuItems = await _reservationRepository.ListOrderedMenuItemsAsync(reservationId);

            return Ok(_mapper.Map<IEnumerable<MenuItemWithoutDetailsDto>>(reservationOrderedMenuItems));
        }
    }
}
