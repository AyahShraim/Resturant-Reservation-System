using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.Employees;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;

namespace RestaurantReservation.API.Controllers
{
    /// <summary>
    /// Managing employee controller
    /// </summary>
    [Route("api/employees")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;

        /// <summary>
        /// Constructor for EmployeesController
        /// </summary>
        /// <param name="employeeRepository">The repository for employee data</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <exception cref="ArgumentNullException">Thrown if employeeRepository or mapper is null</exception>

        public EmployeesController(EmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get list of employees
        /// </summary>
        /// <param name="pageNumber">The page number to return</param>
        /// <param name="pageSize">The page number to return</param>
        /// <returns>List of employees</returns>
        /// <response code="200">Returns the list of employees</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeWithoutDetailsDto>>> GetEmployees(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (employeeEntities, paginationMetaData) = await _employeeRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<EmployeeWithoutDetailsDto>>(employeeEntities));
        }

        /// <summary>
        /// Get employee by Id
        /// </summary>
        /// <param name="id">Id of employee to return</param>
        /// <returns>Employee</returns>
        /// <response code="200">Returns the employee with the specified id</response>
        /// <response code="404">If no employee with the specified id is found</response>
        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                var error = EmployeeErrors.NotFound(id);
                return NotFound(error);
            }

            return Ok(_mapper.Map<EmployeeWithoutDetailsDto>(employee));
        }

        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="employeeDto">Employee data</param>
        /// <returns>The created employee</returns>
        /// <response code="201">Returns the created employee</response>
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto employeeDto)
        {
            var employeeToAdd = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddAsync(employeeToAdd);
            var employeeToReturn = _mapper.Map<EmployeeWithoutDetailsDto>(employeeToAdd);
            return CreatedAtRoute("GetEmployee", new { id = employeeToReturn.Id }, employeeToReturn);
        }

        /// <summary>
        /// Delete employee by Id
        /// </summary>
        /// <param name="id">Id of employee to delete</param>
        /// <returns>No content if success</returns>
        /// <response code="204">If employee deleted successfully</response>
        /// <response code="404">If no employee with the specified id is found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                var error = EmployeeErrors.NotFound(id);
                return NotFound(error);
            }
            await _employeeRepository.DeleteAsync(existingEmployee);
            return NoContent();
        }

        /// <summary>
        /// Update employee by Id
        /// </summary>
        /// <param name="id">Id of employee to update</param>
        /// <param name="employee">The updated employee data</param>
        /// <returns>No content if success</returns>
        /// <response code="204">If employee updated successfully</response>
        /// <response code="404">If no employee with the specified id is found</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, EmployeeDto employee)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                var error = EmployeeErrors.NotFound(id);
                return NotFound(error);
            }
            _mapper.Map(employee, existingEmployee);
            await _employeeRepository.UpdateAsync(existingEmployee);
            return NoContent();
        }

        /// <summary>
        /// Get managers
        /// </summary>
        /// <param name="pageNumber">The page number to return</param>
        /// <param name="pageSize">Max number of records per page</param>
        /// <returns>List of managers</returns>
        /// <response code="200">Returns the list of managers</response>
        [HttpGet("managers")]
        public async Task<ActionResult<IEnumerable<EmployeeWithoutDetailsDto>>> GetManagers(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (managerEntities, paginationMetaData) = await _employeeRepository.ListManagersAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<EmployeeWithoutDetailsDto>>(managerEntities));
        }

        /// <summary>
        /// Get average order amount for an employee
        /// </summary>
        /// <param name="employeeId">Id of the employee</param>
        /// <returns>Average order amount</returns>
        /// <response code="200">Returns the average order amount for the specified employee</response>
        /// <response code="404">If no employee with the specified id is found</response>
        [HttpGet("{employeeId}/average-order-amount")]
        public async Task<ActionResult<decimal>> GetAverageOrderAmount(int employeeId)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeId);
            if (existingEmployee == null)
            {
                var error = EmployeeErrors.NotFound(employeeId);
                return NotFound(error);
            }
            var avgOrderAmount = await _employeeRepository.CalculateAverageOrderAmountAsync(employeeId);
            return Ok(new { AverageOrderAmount = avgOrderAmount });
        }
    }
}
