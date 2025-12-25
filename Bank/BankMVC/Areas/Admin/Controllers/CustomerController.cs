using AutoMapper;
using BLL.Abstract;
using Entities.DTOs.CustomerDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IAccountService accountService, IMapper mapper)
        {
            _customerService = customerService;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _customerService.GetAll().Data;
            return View(result);
        }

        [HttpGet]

        public IActionResult Detail(int id)
        {
            var result = _customerService.Get(id).Data;
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CustomerAddDTO customerAddDTO)
        {
            _customerService.Add(customerAddDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");

            var result = _customerService.Get(id);


            if (result == null || result.Data == null)
                return RedirectToAction("Index");

            var customer = result.Data;
            var customerUpdateDTO = _mapper.Map<CustomerUpdateDTO>(customer);

            return View(customerUpdateDTO);
        }

        [HttpPost]
        public IActionResult Edit(CustomerUpdateDTO customerUpdateDTO)
        {
            _customerService.Update(customerUpdateDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _customerService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
