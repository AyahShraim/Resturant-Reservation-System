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
    /// <summary>
    /// Managing orders controller
    /// </summary>
    [Route("api/orders")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;

        /// <summary>
        /// Constructor for OrdersController
        /// </summary>
        /// <param name="orderRepository">The repository for order data</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <exception cref="ArgumentNullException">Thrown if orderRepository or mapper is null</exception>
        public OrdersController(OrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <param name="pageNumber">The page number to return</param>
        /// <param name="pageSize">Max number of records per page</param>
        /// <returns>List of orders</returns>
        /// <response code="200">Returns the list of orders</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderWithoutDetailsDto>>> GetOrders(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (orderEntities, paginationMetaData) = await _orderRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<OrderWithoutDetailsDto>>(orderEntities));
        }

        /// <summary>
        /// Get order by Id
        /// </summary>
        /// <param name="id">Id of order to return</param>
        /// <returns>Order</returns>
        /// <response code="200">Returns the order with the specified id</response>
        /// <response code="404">If no order with the specified id is found</response>
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

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="orderDto">Order data</param>
        /// <returns>The created order</returns>
        /// <response code="201">Returns the created order</response>
        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(OrderDto orderDto)
        {
            var orderToAdd = _mapper.Map<Order>(orderDto);
            await _orderRepository.AddAsync(orderToAdd);
            var orderToReturn = _mapper.Map<OrderWithoutDetailsDto>(orderToAdd);
            return CreatedAtRoute("GetOrder", new { id = orderToReturn.Id }, orderToReturn);
        }

        /// <summary>
        /// Delete an order by Id
        /// </summary>
        /// <param name="id">Id of order to delete</param>
        /// <returns>No content if success</returns>
        /// <response code="204">If order deleted successfully</response>
        /// <response code="404">If no order with the specified id is found</response>
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

        /// <summary>
        /// Update an order by Id
        /// </summary>
        /// <param name="id">Id of order to update</param>
        /// <param name="order">The updated order data</param>
        /// <returns>No content if success</returns>
        /// <response code="204">If order updated successfully</response>
        /// <response code="404">If no order with the specified id is found</response>
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
