using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.Restaurants;
using RestaurantReservation.API.Models.Tables;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;

namespace RestaurantReservation.API.Controllers
{
    [Route("api/tables")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly TableRepository _tableRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;

        public TablesController(TableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableWithoutDetailsDto>>> Get(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (tableEntities, paginationMetaData) = await _tableRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<TableWithoutDetailsDto>>(tableEntities));
        }

        [HttpGet("{id}", Name = "GetTable")]
        public async Task<IActionResult> GetTable(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
            {
                var error = TableErrors.NotFound(id);
                return NotFound(error);
            }

            return Ok(_mapper.Map<TableWithoutDetailsDto>(table));
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> Create( TableDto tableDto)
        {
            var tableToAdd = _mapper.Map<Table>(tableDto);
            await _tableRepository.AddAsync(tableToAdd);
            var tableToReturn = _mapper.Map<TableWithoutDetailsDto>(tableToAdd);
            return CreatedAtRoute("GetTable", new { id = tableToReturn.Id }, tableToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingTable = await _tableRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                var error = TableErrors.NotFound(id);
                return NotFound(error);
            }

            await _tableRepository.DeleteAsync(existingTable);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, TableDto table)
        {
            var existingTable = await _tableRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                var error = TableErrors.NotFound(id);
                return NotFound(error);
            }
            _mapper.Map(table, existingTable);
            await _tableRepository.UpdateAsync(existingTable);
            return NoContent();
        }
    }
}
