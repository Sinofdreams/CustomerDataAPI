using CustomerDataAPI.DTO;
using CustomerDataAPI.Helpers;
using CustomerDataAPI.Mapper;
using CustomerDataAPI.Models;
using CustomerDataAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CustomerDataAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<CustomerModel> customers = await _customerRepository.GetAllCustomers(query);

            var totalRecords = await _customerRepository.GetCustomerCountAsync(query);

            var totalPages = (int)Math.Ceiling((double)totalRecords / query.PageSize);

            var response = new CustomerResponse
            {
                TotalRecords = totalRecords,
                Page = query.PageNumber,
                PageSize = query.PageSize,
                TotalPages = totalPages,
                Customers = customers.Select(c => c.ToCustomer()).ToList()
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerRepository.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer.ToCustomer());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer createCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = createCustomer.ToCustomerModel();

            await _customerRepository.AddCustomer(customer);

            return CreatedAtAction(nameof(Get), new { id = customer.Id}, customer.ToCustomer());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody]Customer updateCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerRepository.UpdateCustomer(id, updateCustomer);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer.ToCustomer());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerRepository.DeleteCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
