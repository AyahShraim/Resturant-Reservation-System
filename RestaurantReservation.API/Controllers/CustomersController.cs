using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;


namespace RestaurantReservation.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;
        public CustomersController(CustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomersWithoutReservationsDto>>> GetCustomers(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var(customerEntities, paginationMetaData) = await _customerRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<CustomersWithoutReservationsDto>>(customerEntities));
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id); 
            if(customer == null)
            {
                var error = CustomerErrors.NotFound(id);
                return NotFound(error);  
            }

            return Ok(_mapper.Map<CustomersWithoutReservationsDto>(customer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerCreationDto>> Create(CustomerCreationDto customerDto)
        {
            var existingCustomer = await _customerRepository.GetByEmailAsync(customerDto.Email);
            if (existingCustomer != null)
            {
                var error = CustomerErrors.EmailNotUnique;
                return Conflict(error);
            }
            var customerToAdd = _mapper.Map<Customer>(customerDto);
            await _customerRepository.AddAsync(customerToAdd);
            var customerToReturn = _mapper.Map<CustomersWithoutReservationsDto>(customerToAdd);
            return CreatedAtRoute("GetCustomer", new { id = customerToReturn.Id }, customerToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if(existingCustomer == null)
            {
                var error = CustomerErrors.NotFound(id);
                return NotFound(error);               
            }
            
            await _customerRepository.DeleteAsync(existingCustomer);       
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CustomerCreationDto customer)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                var error = CustomerErrors.NotFound(id);
                return NotFound(error);
            }
            _mapper.Map(customer, existingCustomer);
            await _customerRepository.UpdateAsync(existingCustomer);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdate(int id, JsonPatchDocument<CustomerCreationDto> patchDocument)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                var error = CustomerErrors.NotFound(id);
                return NotFound(error);
            }

            var customerToPatch = _mapper.Map<CustomerCreationDto>(existingCustomer);
            patchDocument.ApplyTo(customerToPatch, ModelState);

            TryValidateModel(customerToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(customerToPatch, existingCustomer);
            await _customerRepository.UpdateAsync(existingCustomer);
            return NoContent();
        }
    }
}
