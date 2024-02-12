using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.Restaurants;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;

namespace RestaurantReservation.API.Controllers
{
    /// <summary>
    /// Controller for managing restaurants
    /// </summary>
    [Route("api/restaurants")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;

        /// <summary>
        /// Constructor for RestaurantsController
        /// </summary>
        /// <param name="restaurantRepository">The repository for restaurant data</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <exception cref="ArgumentNullException">Thrown if restaurantRepository or mapper is null</exception>
        public RestaurantsController(RestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get list of restaurants
        /// </summary>
        /// <param name="pageNumber">The page number to return</param>
        /// <param name="pageSize">Max number of records per page</param>
        /// <returns>List of restaurants</returns>
        /// <response code="200">Returns the list of restaurants</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantWithoutDetailsDto>>> GetRestaurants(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (restaurantEntities, paginationMetaData) = await _restaurantRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<RestaurantWithoutDetailsDto>>(restaurantEntities));
        }


        /// <summary>
        /// Get a specific restaurant by ID
        /// </summary>
        /// <param name="id">The ID of the restaurant to retrieve</param>
        /// <returns>The restaurant with the specified ID</returns>
        /// <response code="200">Returns the restaurant with the specified ID</response>
        /// <response code="404">If no restaurant with the specified ID is found</response>
        [HttpGet("{id}", Name = "GetRestaurant")]
        public async Task<IActionResult> Get(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                var error = RestaurantErrors.NotFound(id);
                return NotFound(error);
            }

            return Ok(_mapper.Map<RestaurantWithoutDetailsDto>(restaurant));
        }

        /// <summary>
        /// Create a new restaurant
        /// </summary>
        /// <param name="restaurantDto">The data of the restaurant to create</param>
        /// <returns>The created restaurant</returns>
        /// <response code="201">Returns the created restaurant</response>
        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> Create(RestaurantDto restaurantDto)
        {
            var restaurantToAdd = _mapper.Map<Restaurant>(restaurantDto);
            await _restaurantRepository.AddAsync(restaurantToAdd);
            var restaurantToReturn = _mapper.Map<RestaurantWithoutDetailsDto>(restaurantToAdd);
            return CreatedAtRoute("GetRestaurant", new { id = restaurantToReturn.Id }, restaurantToReturn);
        }

        /// <summary>
        /// Delete a restaurant by ID
        /// </summary>
        /// <param name="id">The ID of the restaurant to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the restaurant is deleted successfully</response>
        /// <response code="404">If no restaurant with the specified ID is found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(id);
            if (existingRestaurant == null)
            {
                var error = RestaurantErrors.NotFound(id);
                return NotFound(error);
            }

            await _restaurantRepository.DeleteAsync(existingRestaurant);
            return NoContent();
        }

        /// <summary>
        /// Update a restaurant by ID
        /// </summary>
        /// <param name="id">The ID of the restaurant to update</param>
        /// <param name="restaurant">The updated data for the restaurant</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the restaurant is updated successfully</response>
        /// <response code="404">If no restaurant with the specified ID is found</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, RestaurantDto restaurant)
        {
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(id);
            if (existingRestaurant == null)
            {
                var error = RestaurantErrors.NotFound(id);
                return NotFound(error);
            }
            _mapper.Map(restaurant, existingRestaurant);
            await _restaurantRepository.UpdateAsync(existingRestaurant);
            return NoContent();
        }

        /// <summary>
        /// Get a specific restaurant with its details
        /// </summary>
        /// <param name="id">The ID of the restaurant to retrieve</param>
        /// <returns>The restaurant with its details</returns>
        /// <response code="200">Returns the restaurant with its details</response>
        /// <response code="404">If no restaurant with the specified ID is found</response>
        [HttpGet("{id}/details", Name = "GetRestaurantWithDetails")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var restaurant = await _restaurantRepository.GetWithDetails(id);
            if (restaurant == null)
            {
                var error = RestaurantErrors.NotFound(id);
                return NotFound(error);
            }

            return Ok(_mapper.Map<RestaurantWithDetails>(restaurant));
        }
    }
}
