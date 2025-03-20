using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnackHub.Areas.Admin.ViewModels;

namespace SnackHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var userName = User.Identity.IsAuthenticated
                ? (User.Identity.Name ?? "Admin")
                : "Admin";

            var model = new AdminIndexViewModel
            {
                AdminName = userName,
                Notifications = new List<string>
            {
                "Tal produto está com o estoque abaixo!",
                "Confira o relatório de vendas da semana",
                "Novo registro de usuário detectado"
            }
            };

            return View(model);
        }
    }
}
