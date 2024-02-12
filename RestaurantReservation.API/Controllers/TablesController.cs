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
    /// <summary>
    /// Controller for managing tables
    /// </summary>
    [Route("api/tables")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly TableRepository _tableRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;

        /// <summary>
        /// Constructor for TablesController
        /// </summary>
        /// <param name="tableRepository">The repository for table data</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <exception cref="ArgumentNullException">Thrown if tableRepository or mapper is null</exception>
        public TablesController(TableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository ?? throw new ArgumentNullException(nameof(tableRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get list of tables
        /// </summary>
        /// <param name="pageNumber">The page number to return</param>
        /// <param name="pageSize">Max number of records per page</param>
        /// <returns>List of tables</returns>
        /// <response code="200">Returns the list of tables</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableWithoutDetailsDto>>> Get(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (tableEntities, paginationMetaData) = await _tableRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<TableWithoutDetailsDto>>(tableEntities));
        }

        /// <summary>
        /// Get a specific table by ID
        /// </summary>
        /// <param name="id">The ID of the table to retrieve</param>
        /// <returns>The table with the specified ID</returns>
        /// <response code="200">Returns the table with the specified ID</response>
        /// <response code="404">If no table with the specified ID is found</response>
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

        /// <summary>
        /// Create a new table
        /// </summary>
        /// <param name="tableDto">The data of the table to create</param>
        /// <returns>The created table</returns>
        /// <response code="201">Returns the created table</response>
        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> Create( TableDto tableDto)
        {
            var tableToAdd = _mapper.Map<Table>(tableDto);
            await _tableRepository.AddAsync(tableToAdd);
            var tableToReturn = _mapper.Map<TableWithoutDetailsDto>(tableToAdd);
            return CreatedAtRoute("GetTable", new { id = tableToReturn.Id }, tableToReturn);
        }

        /// <summary>
        /// Delete a table by ID
        /// </summary>
        /// <param name="id">The ID of the table to delete</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the table is deleted successfully</response>
        /// <response code="404">If no table with the specified ID is found</response>
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

        /// <summary>
        /// Update a table by ID
        /// </summary>
        /// <param name="id">The ID of the table to update</param>
        /// <param name="table">The updated data for the table</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the table is updated successfully</response>
        /// <response code="404">If no table with the specified ID is found</response>
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
