using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication_Templeto_F777.Models;
using WebApplication_Templeto_F777.ViewModels.UserViewModels;

namespace WebApplication_Templeto_F777.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager , SignInManager<AppUser> _signInManager,RoleManager<IdentityRole> _roleManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            AppUser user = new()
            {
                FullName=vm.FullName,
                UserName=vm.UserName,
                Email=vm.Email
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");

            
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var User = await _userManager.FindByEmailAsync(vm.Email);
            if(User is null)
            {
                ModelState.AddModelError("", "USer or Password is wrong");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(User, vm.Password, false, true);
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "USer or Password is wrong");
                return View(vm);
            }
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Admin"
            });
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Member"
            });
            return Ok("Role was Created");
        }


    }
}
