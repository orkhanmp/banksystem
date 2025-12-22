using BLL.Abstract;
using Entities.DTOs.CustomerDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var result = _customerService.GetAll().Data;
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id) {
            var result = _customerService.Get(id).Data;
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerAddDTO customerAddDTO)
        {
            _customerService.Add(customerAddDTO);
            return Ok("201");
        }

        [HttpPut]
        public IActionResult UpdateCustomer(CustomerUpdateDTO customerUpdateDTO)
        {
            _customerService.Update(customerUpdateDTO);
            return Ok("200");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _customerService.Delete(id);
            return Ok("Deleted Successfully");
        }
    }
}
