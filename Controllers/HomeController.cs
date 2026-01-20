using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using WebApplication_Templeto_F777.Context;
using WebApplication_Templeto_F777.ViewModels.ChefViewModels;


namespace WebApplication_Templeto_F777.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
     public async Task<IActionResult> Index()
        {
            var chefs = await  _context.Chefs.Select(x => new ChefGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImagePath = x.ImagePath,
                ProfessionName = x.Profession.Name
            }).ToListAsync();
            return View(chefs);
        }
    }
}
