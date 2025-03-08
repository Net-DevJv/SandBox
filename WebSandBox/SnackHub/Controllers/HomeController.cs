using Microsoft.AspNetCore.Mvc;
using SnackHub.Repositories.Interfaces;
using SnackHub.ViewModels;
using System.Diagnostics;

namespace SnackHub.Controllers;

public class HomeController : Controller
{
    private readonly ISnacksRepository _snacksRepository;

    public HomeController(ISnacksRepository snacksRepository)
    {
        _snacksRepository = snacksRepository;
    }

    public IActionResult Index()
    {
        var homeViewModel = new HomeViewModel
        {
            FavoriteSnacks = _snacksRepository.FavoriteSnacks
        };

        return View(homeViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
