using System.Threading.Tasks;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticate;

        public AccountController(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await _authenticate.Authenticate(loginViewModel.Email, loginViewModel.Password);

            if (result)
            {
                if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(loginViewModel.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt. (password must be wrong)");
                return View(loginViewModel);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticate.RegisterUser(registerViewModel.Email, registerViewModel.Password);

                if (result)
                {
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt. (email must be unique)");
                    return View(registerViewModel);
                }
            }
            return View(registerViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _authenticate.Logout();
            return Redirect("/Account/Login");
        }

    }
}
