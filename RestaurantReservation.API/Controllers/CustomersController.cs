using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.DomainErrors;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using System.Text.Json;


namespace RestaurantReservation.API.Controllers
{
    /// <summary>
    /// Managing customer controller
    /// </summary>
  
    [Authorize]
    [Route("api/customers")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;

        /// <summary>
        /// Constructor for Customer Controller
        /// </summary>
        /// <param name="customerRepository">The repository for customer data</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <exception cref="ArgumentNullException">Thrown if customerRepository or mapper is null</exception>
        public CustomersController(CustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get list of customers
        /// </summary>
        /// <param name="pageNumber">the page number to return</param>
        /// <param name="pageSize"> max number of records per page</param>
        /// <returns>List of customers</returns>
        /// <response code="200">Returns the list of customers</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomersWithoutReservationsDto>>> GetCustomers(int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            var(customerEntities, paginationMetaData) = await _customerRepository.GetAllAsync(pageNumber, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<CustomersWithoutReservationsDto>>(customerEntities));
        }

        /// <summary>
        /// Get customer by Id
        /// </summary>
        /// <param name="id">id of customer to return</param>
        /// <returns>Customer</returns>
        /// <response code="200">Returns the customer with the specified id</response>
        /// <response code="404">If no customer with the specified id is found</response>
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

        /// <summary>
        /// create new customer
        /// </summary>
        /// <param name="customerDto">Customer data</param>
        /// <returns>The created customer</returns>
        /// <response code="201">Returns the created customer</response>
        /// <response code="409">If  customer email isn't unique</response>
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(CustomerDto customerDto)
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

        /// <summary>
        /// Delete customer by Id
        /// </summary>
        /// <param name="id">id of customer to delete</param>
        /// <returns>No content if success </returns>
        ///<response code="204">If customer Deleted successfully</response>
        /// <response code="404">If no customer with the specified id is found</response>
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

        /// <summary>
        /// update customer by Id
        /// </summary>
        /// <param name="id">id of customer to update</param>
        /// <param name="customer">the updated customer data</param>
        /// <returns>no content if success</returns>
        ///<response code="204">If customer Updated successfully</response>
        /// <response code="404">If no customer with the specified id is found</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CustomerDto customer)
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

        /// <summary>
        /// partially update customer by id
        /// </summary>
        /// <param name="id">id of customer to update</param>
        /// <param name="patchDocument">the JSON patch document</param>
        /// <returns>no content if success</returns>
        /// <response code="204">If customer is successfully partially updated</response>
        /// <response code="400">If the request is invalid or the patch operation fails</response>
        /// <response code="404">If no customer with the specified id is found</response>
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartiallyUpdate(int id, JsonPatchDocument<CustomerDto> patchDocument)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                var error = CustomerErrors.NotFound(id);
                return NotFound(error);
            }

            var customerToPatch = _mapper.Map<CustomerDto>(existingCustomer);
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
