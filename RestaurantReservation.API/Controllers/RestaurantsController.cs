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
    [Route("api/restaurants")]
    [Authorize]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;

        public RestaurantsController(RestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantWithoutDetailsDto>>> GetRestaurants(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (restaurantEntities, paginationMetaData) = await _restaurantRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<RestaurantWithoutDetailsDto>>(restaurantEntities));
        }

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

        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> Create(RestaurantDto restaurantDto)
        {
            var restaurantToAdd = _mapper.Map<Restaurant>(restaurantDto);
            await _restaurantRepository.AddAsync(restaurantToAdd);
            var restaurantToReturn = _mapper.Map<RestaurantWithoutDetailsDto>(restaurantToAdd);
            return CreatedAtRoute("GetRestaurant", new { id = restaurantToReturn.Id }, restaurantToReturn);
        }

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
