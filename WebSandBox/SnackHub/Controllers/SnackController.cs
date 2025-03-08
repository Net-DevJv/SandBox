using Microsoft.AspNetCore.Mvc;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;
using SnackHub.ViewModels;

namespace SnackHub.Controllers
{
    public class SnackController : Controller
    {
        private readonly ISnacksRepository _snacksRepository;

        public SnackController(ISnacksRepository snacksRepository)
        {
            _snacksRepository = snacksRepository;
        }

        public IActionResult List(string category)
        {
            IEnumerable<SnackModel> snacks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                snacks = _snacksRepository.Snacks.OrderBy(s => s.SnackId);
                currentCategory = "Todos os lanches";
            }
            else
            {
                //if (string.Equals("Normal", category, StringComparison.OrdinalIgnoreCase))
                //{
                //    snacks = _snacksRepository.Snacks.Where(l => l.Category.CategoryName.Equals("Normal")).OrderBy(s => s.SnackName);
                //}
                //else
                //{
                //    snacks = _snacksRepository.Snacks.Where(l => l.Category.CategoryName.Equals("Natural")).OrderBy(s => s.SnackName);
                //}

                snacks = _snacksRepository.Snacks.Where(l => l.Category.CategoryName.Equals(category)).OrderBy(c => c.SnackName);

                currentCategory = category;
            }

            var snacksViewModel = new SnackListViewModel
            {
                Snacks = snacks,
                CurrentCategory = currentCategory
            };

            return View(snacksViewModel);
        }

        public IActionResult Details(int snackId)
        {
            var snack = _snacksRepository.Snacks.FirstOrDefault(l => l.SnackId == snackId);

            return View(snack);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<SnackModel> snacks;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                snacks = _snacksRepository.Snacks.OrderBy(p => p.SnackId);
                currentCategory = "Todos os Lanches";
            }
            else
            {
                snacks = _snacksRepository.Snacks.Where(p => p.SnackName.ToLower().Contains(searchString.ToLower()));

                if (snacks.Any())
                    currentCategory = "Lanches";
                else
                    currentCategory = "Nenhum lanche foi encontrado";

            }

            return View("~/Views/Snack/List.cshtml", new SnackListViewModel
            {
                Snacks = snacks,
                CurrentCategory = currentCategory
            });
        }
    }
}
