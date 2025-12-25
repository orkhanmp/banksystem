using Entities.DTOs.AuthDTO;
using Entities.TableModels.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region  Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid)
            {
                AppUser foundedUser = null;


                foundedUser = await _userManager.FindByEmailAsync(dto.Email);


                if (foundedUser == null)
                {
                    ViewBag.Message = "İstifadəçi adınız və ya şifrəniz yanlışdır!";
                    goto end;
                }

                var signInResult = await _signInManager.PasswordSignInAsync(foundedUser, dto.Password, true, true);

                if (!signInResult.Succeeded)
                {
                    ViewBag.Message = "İstifadəçi adınız və ya şifrəniz yanlışdır!";
                    goto end;
                }
            }
            var callbackUrl = Request.Query["ReturnUrl"];

            if (!string.IsNullOrWhiteSpace(callbackUrl))
            {
                return Redirect(callbackUrl);
            }

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        end:
            return View();
        }


        #endregion



        #region   Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser();

                user.Email = dto.Email;
                user.UserName = dto.Email;
                user.FirstName = dto.Name;
                user.LastName = dto.Surname;
                user.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    ViewBag.Message = "Qeydiyyat tamamlandı!";

                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(dto);
        }
        #endregion


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Accounts");
        }
    }

}
