using AutoMapper;
using BLL.Abstract;
using Entities.DTOs.AccountDTOs;
using Entities.TableModels;
using Microsoft.AspNetCore.Mvc;

namespace BankMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, ICustomerService customerService, IMapper mapper)
        {
            _accountService = accountService;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _accountService.GetAll().Data;
            return View(result);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var result = _accountService.Get(id).Data;
            return View(result);

        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Customers = _customerService.GetAll().Data;
            return View();
        }

        [HttpPost]

        public IActionResult Add(AccountAddDTO accountAddDTO)
        {
            _accountService.Add(accountAddDTO);
            return RedirectToAction("Index");
           
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
                return RedirectToAction("Index");

            var result = _accountService.Get(id);

            if (result == null || result.Data == null)
                return RedirectToAction("Index");

            var account = result.Data;
            var accountUpdateDTO = _mapper.Map<AccountUpdateDTO>(account);

            ViewBag.Customers = _customerService.GetAll().Data;
            return View(accountUpdateDTO);
        }


        [HttpPost]
        public IActionResult Edit(AccountUpdateDTO accountUpdateDTO)
        {
            _accountService.Update(accountUpdateDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Delete(int id)
        {            
            _accountService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var account = _accountService.Get(id.Value).Data;
                if (account != null)
                {
                    var list = new List<AccountListDTO> { account };
                    return View("Index", list);
                }
                return View("Index", new List<AccountListDTO>());
            }

            var accountList = _accountService.GetAll().Data;
            return View("Index", accountList);
        }
    }
}
    