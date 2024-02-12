using Asp.Versioning;
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
    /// <summary>
    /// Managing reservations controller
    /// </summary>
    [Route("api/reservations")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;

        /// <summary>
        /// Constructor for ReservationsController
        /// </summary>
        /// <param name="reservationRepository">The repository for reservation data</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <exception cref="ArgumentNullException">Thrown if reservationRepository or mapper is null</exception>
        public ReservationsController(ReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get list of reservations
        /// </summary>
        /// <param name="pageNumber">The page number to return</param>
        /// <param name="pageSize">Max number of records per page</param>
        /// <returns>List of reservations</returns>
        /// <response code="200">Returns the list of reservations</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationWithoutDetailsDto>>> GetReservations(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (reservationEntities, paginationMetaData) = await _reservationRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<ReservationWithoutDetailsDto>>(reservationEntities));
        }

        /// <summary>
        /// Get reservation by Id
        /// </summary>
        /// <param name="id">Id of reservation to return</param>
        /// <returns>Reservation</returns>
        /// <response code="200">Returns the reservation with the specified id</response>
        /// <response code="404">If no reservation with the specified id is found</response>
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

        /// <summary>
        /// Create a new reservation
        /// </summary>
        /// <param name="reservationDto">Reservation data</param>
        /// <returns>The created reservation</returns>
        /// <response code="201">Returns the created reservation</response>
        /// <response code="404">If the restaurant or table specified in the reservation does not match</response>
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

        /// <summary>
        /// Delete reservation by Id
        /// </summary>
        /// <param name="id">Id of reservation to delete</param>
        /// <returns>No content if success</returns>
        /// <response code="204">If reservation is deleted successfully</response>
        /// <response code="404">If no reservation with the specified id is found</response>
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

        /// <summary>
        /// Update reservation by Id
        /// </summary>
        /// <param name="id">Id of reservation to update</param>
        /// <param name="reservation">The updated reservation data</param>
        /// <returns>No content if success</returns>
        /// <response code="204">If reservation is updated successfully</response>
        /// <response code="404">If no reservation with the specified id is found or the restaurant or table specified in the reservation does not match </response>
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

        /// <summary>
        /// Get reservations for a specific customer
        /// </summary>
        /// <param name="customerId">Id of the customer</param>
        /// <returns>List of reservations for the customer</returns>
        /// <response code="200">Returns the list of reservations for the customer</response>
        /// <response code="404">If no reservations found for the customer</response>
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

        /// <summary>
        /// Get orders with related menu items for a specific reservation
        /// </summary>
        /// <param name="reservationId">Id of the reservation</param>
        /// <returns>List of orders with related menu items</returns>
        /// <response code="200">Returns the list of orders with related menu items for the reservation</response>
        /// <response code="404">If no reservation with the specified id is found</response>
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

        /// <summary>
        /// Get the menu items ordered in a specific reservation
        /// </summary>
        /// <param name="reservationId">ID of the reservation</param>
        /// <returns>List of menu items ordered in the reservation</returns>
        /// <response code="200">Returns the list of menu items ordered in the reservation</response>
        /// <response code="404">If no reservation with the specified ID is found</response>
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
