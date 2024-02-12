using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;

namespace RestaurantReservation.API.Controllers
{
    /// <summary>
    /// Managing menu items controller
    /// </summary>
    [Route("api/menuItems")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly MenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;

        /// <summary>
        /// Constructor for MenuItemsController
        /// </summary>
        /// <param name="menuItemRepository">The repository for menu item data</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <exception cref="ArgumentNullException">Thrown if menuItemRepository or mapper is null</exception>
        public MenuItemsController(MenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository ?? throw new ArgumentNullException(nameof(menuItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get list of menu items
        /// </summary>
        /// <param name="pageNumber">The page number to return</param>
        /// <param name="pageSize">Max number of records per page</param>
        /// <returns>List of menu items</returns>
        /// <response code="200">Returns the list of menu items</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemWithoutDetailsDto>>> GetMenuItems(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (menuItemEntities, paginationMetaData) = await _menuItemRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<MenuItemWithoutDetailsDto>>(menuItemEntities));
        }

        /// <summary>
        /// Get menu item by Id
        /// </summary>
        /// <param name="id">Id of menu item to return</param>
        /// <returns>Menu item</returns>
        /// <respone code="200">Returns the menu item with the specified id</respone>
        /// <response code="404">If no menu item with the specified id is found</response>
        [HttpGet("{id}", Name = "GetMenuItem")]
        public async Task<IActionResult> Get(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
            {
                var error = MenuItemsErrors.NotFound(id);
                return NotFound(error);
            }

            return Ok(_mapper.Map<MenuItemWithoutDetailsDto>(menuItem));
        }

        /// <summary>
        /// Create a new menu item
        /// </summary>
        /// <param name="menuItemDto">Menu item data</param>
        /// <returns>The created menu item</returns>
        /// <response code="201">Returns the created menu item</response>
        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(MenuItemDto menuItemDto)
        {
            var menuItemToAdd = _mapper.Map<MenuItem>(menuItemDto);
            await _menuItemRepository.AddAsync(menuItemToAdd);
            var menuItemToReturn = _mapper.Map<MenuItemWithoutDetailsDto>(menuItemToAdd);
            return CreatedAtRoute("GetMenuItem", new { id = menuItemToReturn.Id }, menuItemToReturn);
        }

        /// <summary>
        /// Delete a menu item by Id
        /// </summary>
        /// <param name="id">Id of menu item to delete</param>
        /// <returns>No content if success</returns>
        /// <response code="204">If menu item deleted successfully</response>
        /// <response code="404">If no menu item with the specified id is found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(id);
            if (existingMenuItem == null)
            {
                var error = MenuItemsErrors.NotFound(id);
                return NotFound(error);
            }
            await _menuItemRepository.DeleteAsync(existingMenuItem);
            return NoContent();
        }

        /// <summary>
        /// Update a menu item by Id
        /// </summary>
        /// <param name="id">Id of menu item to update</param>
        /// <param name="menuItem">The updated menu item data</param>
        /// <returns>No content if success</returns>
        /// <response code="204">If menu item updated successfully</response>
        /// <response code="404">If no menu item with the specified id is found</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, MenuItemDto menuItem)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(id);
            if (existingMenuItem == null)
            {
                var error = MenuItemsErrors.NotFound(id);
                return NotFound(error);
            }
            _mapper.Map(menuItem, existingMenuItem);
            await _menuItemRepository.UpdateAsync(existingMenuItem);
            return NoContent();
        }
    }
}
