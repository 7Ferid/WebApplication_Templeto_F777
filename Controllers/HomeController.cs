using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication_Templeto_F777.Controllers
{
    public class HomeController : Controller
    {
     public IActionResult Index()
        {
            return View();
        }
    }
}
