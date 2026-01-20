using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication_Templeto_F777.Context;
using WebApplication_Templeto_F777.Helpers;
using WebApplication_Templeto_F777.Models;
using WebApplication_Templeto_F777.ViewModels.ChefViewModels;

namespace WebApplication_Templeto_F777.Areas.Admin.Controllers
{[Area("Admin")]
    public class ChefController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderpath;

        public ChefController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderpath = Path.Combine(_environment.WebRootPath, "images");
        }

        public async Task<IActionResult> Index()
        {
            var chefs = await _context.Chefs.Select(x=> new ChefGetVM()
            {
                Id=x.Id,
                Name=x.Name,
                Description=x.Description,
                ImagePath=x.ImagePath,
                ProfessionName=x.Profession.Name
            }).ToListAsync();
            return View(chefs);
        }
        public async Task<IActionResult> Create()
        {
            await _SendProfessionWithViewBag();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ChefCreateVM vm)
        {
            await _SendProfessionWithViewBag();
            if (!ModelState.IsValid)
                return View(vm);
            var professionisExist = await _context.Professions.AnyAsync(x => x.Id == vm.ProfessionId);
            if (!professionisExist)
            {
                ModelState.AddModelError("ProfessionId", "bele profesiya yoxdu");
                return View(vm);
            }
            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "2 mbdan cox olmamalidi");
                return View(vm);
            }
            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("Image", "ancaq image yukleye bilersiniz");
                return View(vm);
            }

            var uniqueFileName = await vm.Image.FileUpload(_folderpath);

            Chef chef = new()
            {
             Name=vm.Name,
             Description=vm.Description,
             ImagePath=uniqueFileName,
             ProfessionId=vm.ProfessionId
          
            };
            await _context.Chefs.AddAsync(chef);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            await _SendProfessionWithViewBag();

            var findId = await _context.Chefs.FindAsync(id);
            if (findId is null)
                return NotFound();

            _context.Chefs.Remove(findId);
            await _context.SaveChangesAsync();
            var deletedImagePath = Path.Combine(_folderpath, findId.ImagePath);
            FileHelper.FileDelete(deletedImagePath);

            return RedirectToAction("Index");
             
        }

        private async Task _SendProfessionWithViewBag()
        {
            var profesions = await _context.Professions.Select(x=> new SelectListItem()
            {
                Value=x.Id.ToString(),
                Text=x.Name
            }).ToListAsync();
            ViewBag.Professions = profesions;
        }
    }
}
