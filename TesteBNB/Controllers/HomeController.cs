using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TesteBNB.Models;

namespace TesteBNB.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }


    }
}