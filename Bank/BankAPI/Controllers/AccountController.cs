using BLL.Abstract;
using Entities.DTOs.AccountDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            var result = _accountService.GetAll().Data;        
            return Ok(result);
            
        }

        [HttpGet("{id}")]
        
        public IActionResult Get(int id)
        {
            var result = _accountService.Get(id).Data;            
            return Ok(result);            
        }

        [HttpPost]

        public IActionResult AddAccount(AccountAddDTO accountAddDTO)
        {
            _accountService.Add(accountAddDTO);          
            return Ok("201");            
        }

        [HttpPut]
        public IActionResult UpdateAccount(AccountUpdateDTO accountUpdateDTO)
        {
            _accountService.Update(accountUpdateDTO);           
            return Ok("200");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            _accountService.Delete(id);           
            return Ok("Deleted Successfully");
        }
    }
}
