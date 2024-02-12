using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.API.Models.Orders;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/orders")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;
        public OrdersController(OrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderWithoutDetailsDto>>> GetOrders(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (orderEntities, paginationMetaData) = await _orderRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<OrderWithoutDetailsDto>>(orderEntities));
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                var error = OrderErrors.NotFound(id);
                return NotFound(error);
            }

            return Ok(_mapper.Map<OrderWithoutDetailsDto>(order));
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(OrderDto orderDto)
        {
            var orderToAdd = _mapper.Map<Order>(orderDto);
            await _orderRepository.AddAsync(orderToAdd);
            var orderToReturn = _mapper.Map<OrderWithoutDetailsDto>(orderToAdd);
            return CreatedAtRoute("GetOrder", new { id = orderToReturn.Id }, orderToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
            {
                var error = OrderErrors.NotFound(id);
                return NotFound(error);
            }
            await _orderRepository.DeleteAsync(existingOrder);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, OrderDto order)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
            {
                var error = OrderErrors.NotFound(id);
                return NotFound(error);
            }
            _mapper.Map(order, existingOrder);
            await _orderRepository.UpdateAsync(existingOrder);
            return NoContent();
        }
    }
}
