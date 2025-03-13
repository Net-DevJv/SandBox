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

        // GET: /Account/LoginEmail
        // Exibe a tela onde o usuário informa apenas o email.
        [HttpGet]
        public IActionResult LoginEmail()
        {
            return View();
        }

        // POST: /Account/LoginEmail
        // Verifica se o email existe; se não existir, redireciona para cadastro; se existir, redireciona para a tela de senha.
        [HttpPost]
        public async Task<IActionResult> LoginEmail(LoginEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                // Se o email não existir, redireciona para o cadastro, pré-preenchendo o email.
                return RedirectToAction("RegisterNewUser", new { email = model.Email });
            }
            else
            {
                // Se o email existir, redireciona para a tela de inserir a senha.
                return RedirectToAction("LoginPassword", new { email = model.Email });
            }
        }

        // GET: /Account/LoginPassword
        // Exibe a tela para que o usuário insira a senha, usando o email informado.
        [HttpGet]
        public IActionResult LoginPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("LoginEmail");

            var model = new LoginPasswordViewModel { Email = email };
            return View(model);
        }

        // POST: /Account/LoginPassword
        // Processa a inserção de senha e faz o login.
        [HttpPost]
        public async Task<IActionResult> LoginPassword(LoginPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Credenciais inválidas.");
                return View(model);
            }

            await SignInUser(user);
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/RegisterNewUser
        // Exibe a tela de registro para criar uma nova conta, pré-preenchida com o email.
        [HttpGet]
        public IActionResult RegisterNewUser(string email)
        {
            var model = new RegisterViewModel { Email = email };
            return View(model);
        }

        // POST: /Account/RegisterNewUser
        // Cria a conta se o email ainda não existir; caso exista, redireciona para a tela de senha.
        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null)
            {
                // Se o usuário já existe, redireciona para a tela de inserir senha.
                return RedirectToAction("LoginPassword", new { email = model.Email, fromRegister = true });
            }

            // Cria um novo usuário
            var newUser = new User
            {
                Email = model.Email,
                FullName = model.FullName,
                PasswordHash = HashPassword(model.Password),
                UserType = UserType.Normal  // ou 0, se preferir
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Faz login automático após o cadastro (opcional)
            await SignInUser(newUser);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        // Efetua o logout e redireciona para a página inicial.
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // Métodos auxiliares:

        // Gera o hash seguro da senha utilizando o PasswordHasher do ASP.NET Core Identity.
        private string HashPassword(string password)
        {
            var dummyUser = new User();
            return _passwordHasher.HashPassword(dummyUser, password);
        }

        // Verifica se a senha fornecida corresponde ao hash armazenado.
        private bool VerifyPassword(string password, string storedHash)
        {
            var dummyUser = new User();
            var result = _passwordHasher.VerifyHashedPassword(dummyUser, storedHash, password);
            return result == PasswordVerificationResult.Success;
        }

        // Cria os Claims, efetua o sign-in e gera o cookie de autenticação.
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
    }
}
