using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.API.Models.OrderItems;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;

namespace RestaurantReservation.API.Controllers
{
    /// <summary>
    /// Managing order items controller
    /// </summary>
    [Route("api/orderItems")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly OrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;

        /// <summary>
        /// Constructor for OrderItemsController
        /// </summary>
        /// <param name="orderItemRepository">The repository for order item data</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <exception cref="ArgumentNullException">Thrown if orderItemRepository or mapper is null</exception>
        public OrderItemsController(OrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get list of order items
        /// </summary>
        /// <param name="pageNumber">The page number to return</param>
        /// <param name="pageSize">Max number of records per page</param>
        /// <returns>List of order items</returns>
        /// <response code="200">Returns the list of order items</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemWithoutDetailsDto>>> GetOrderItems(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (orderItemEntities, paginationMetaData) = await _orderItemRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<OrderItemWithoutDetailsDto>>(orderItemEntities));
        }

        /// <summary>
        /// Get order item by Id
        /// </summary>
        /// <param name="id">Id of order item to return</param>
        /// <returns>Order item</returns>
        /// <response code="200">Returns the order item with the specified id</response>
        /// <response code="404">If no order item with the specified id is found</response>
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

        /// <summary>
        /// Create a new order item
        /// </summary>
        /// <param name="orderItemDto">Order item data</param>
        /// <returns>The created order item</returns>
        /// <response code="201">Returns the created order item</response>
        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(OrderItemDto orderItemDto)
        {
            var orderItemToAdd = _mapper.Map<OrderItem>(orderItemDto);
            await _orderItemRepository.AddAsync(orderItemToAdd);
            var orderItemToReturn = _mapper.Map<OrderItemWithoutDetailsDto>(orderItemToAdd);
            return CreatedAtRoute("GetOrderItem", new { id = orderItemToReturn.Id }, orderItemToReturn);
        }

        /// <summary>
        /// Delete an order item by Id
        /// </summary>
        /// <param name="id">Id of order item to delete</param>
        /// <returns>No content if success</returns>
        /// <response code="204">If order item deleted successfully</response>
        /// <response code="404">If no order item with the specified id is found</response>
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

        /// <summary>
        /// Update an order item by Id
        /// </summary>
        /// <param name="id">Id of order item to update</param>
        /// <param name="orderItem">The updated order item data</param>
        /// <returns>No content if success</returns>
        /// <response code="204">If order item updated successfully</response>
        /// <response code="404">If no order item with the specified id is found</response>
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
