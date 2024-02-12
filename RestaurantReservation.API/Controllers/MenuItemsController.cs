using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/menuItems")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly MenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;
        public MenuItemsController(MenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemWithoutDetailsDto>>> GetMenuItems(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (menuItemEntities, paginationMetaData) = await _menuItemRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<MenuItemWithoutDetailsDto>>(menuItemEntities));
        }

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

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(MenuItemDto menuItemDto)
        {
            var menuItemToAdd = _mapper.Map<MenuItem>(menuItemDto);
            await _menuItemRepository.AddAsync(menuItemToAdd);
            var menuItemToReturn = _mapper.Map<MenuItemWithoutDetailsDto>(menuItemToAdd);
            return CreatedAtRoute("GetMenuItem", new { id = menuItemToReturn.Id }, menuItemToReturn);
        }

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
