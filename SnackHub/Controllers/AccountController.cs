using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackHub.AppContext;
using SnackHub.Enums;
using SnackHub.Models;
using SnackHub.ViewModels;
using System.Security.Claims;

namespace SnackHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult LoginEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginEmail(LoginEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.SnackHubUsers.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return RedirectToAction("RegisterPrompt", new { email = model.Email });
            }
            else
            {
                return RedirectToAction("LoginPassword", new { email = model.Email });
            }
        }

        [HttpGet]
        public IActionResult LoginPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("LoginEmail");

            var model = new LoginPasswordViewModel { Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginPassword(LoginPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.SnackHubUsers.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Senha incorreta!");
                return View(model);
            }

            await SignInUser(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult RegisterNewUser(string email)
        {
            var model = new RegisterViewModel { Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.SnackHubUsers.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null)
            {
                return RedirectToAction("LoginPassword", new { email = model.Email });
            }

            var newUser = new User
            {
                Email = model.Email,
                FullName = model.FullName,
                PasswordHash = HashPassword(model.Password),
                UserType = UserType.Normal,
                CreationDate = DateTime.Now
            };

            _context.SnackHubUsers.Add(newUser);
            await _context.SaveChangesAsync();
            await SignInUser(newUser);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            var dummyUser = new User();
            return _passwordHasher.HashPassword(dummyUser, password);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var dummyUser = new User();
            var result = _passwordHasher.VerifyHashedPassword(dummyUser, storedHash, password);
            return result == PasswordVerificationResult.Success;
        }

        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserType", ((int)user.UserType).ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        [HttpGet]
        public IActionResult RegisterPrompt(string email)
        {
            var promptModel = new RegisterPromptViewModel { Email = email };
            return View(promptModel);
        }
    }
}
