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
    [Route("api/employees")]
    [Authorize]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;
        public EmployeesController(EmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeWithoutDetailsDto>>> GetEmployees(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (employeeEntities, paginationMetaData) = await _employeeRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<EmployeeWithoutDetailsDto>>(employeeEntities));
        }

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

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto employeeDto)
        {
            var employeeToAdd = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddAsync(employeeToAdd);
            var employeeToReturn = _mapper.Map<EmployeeWithoutDetailsDto>(employeeToAdd);
            return CreatedAtRoute("GetEmployee", new { id = employeeToReturn.Id }, employeeToReturn);
        }

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

        [HttpGet("managers")]
        public async Task<ActionResult<IEnumerable<EmployeeWithoutDetailsDto>>> GetManagers(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var (managerEntities, paginationMetaData) = await _employeeRepository.ListManagersAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<EmployeeWithoutDetailsDto>>(managerEntities));
        }


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
