using Microsoft.AspNetCore.Mvc;
using SnackHub.Models;
using SnackHub.Repositories.Interfaces;
using SnackHub.ViewModels;

namespace SnackHub.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var category = _categoryRepository.Categories.OrderBy(c => c.CategoryName);

            return View(category);
        }
    }
}
