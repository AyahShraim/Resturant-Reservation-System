using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.API.Models.OrderItems;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/orderItems")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly OrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;
        public OrderItemsController(OrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemWithoutDetailsDto>>> GetOrderItems(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (orderItemEntities, paginationMetaData) = await _orderItemRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<OrderItemWithoutDetailsDto>>(orderItemEntities));
        }

        [HttpGet("{id}", Name = "GetOrderItem")]
        public async Task<IActionResult> Get(int id)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(id);
            if (orderItem == null)
            {
                var error = OrderItemsErrors.NotFound(id);
                return NotFound(error);
            }

            return Ok(_mapper.Map<OrderItemWithoutDetailsDto>(orderItem));
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(OrderItemDto orderItemDto)
        {
            var orderItemToAdd = _mapper.Map<OrderItem>(orderItemDto);
            await _orderItemRepository.AddAsync(orderItemToAdd);
            var orderItemToReturn = _mapper.Map<OrderItemWithoutDetailsDto>(orderItemToAdd);
            return CreatedAtRoute("GetOrderItem", new { id = orderItemToReturn.Id }, orderItemToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingOrderItem = await _orderItemRepository.GetByIdAsync(id);
            if (existingOrderItem == null)
            {
                var error = OrderItemsErrors.NotFound(id);
                return NotFound(error);
            }
            await _orderItemRepository.DeleteAsync(existingOrderItem);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, OrderItemDto orderItem)
        {
            var existingOrderItem = await _orderItemRepository.GetByIdAsync(id);
            if (existingOrderItem == null)
            {
                var error = OrderItemsErrors.NotFound(id);
                return NotFound(error);
            }
            _mapper.Map(orderItem, existingOrderItem);
            await _orderItemRepository.UpdateAsync(existingOrderItem);
            return NoContent();
        }
    }
}
