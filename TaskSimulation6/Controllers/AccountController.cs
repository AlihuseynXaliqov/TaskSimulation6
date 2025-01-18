using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskSimulation6.DAL.Context;
using TaskSimulation6.Models;
using TaskSimulation6.ViewModel.Account;

namespace TaskSimulation6.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> role;

        public AccountController(AppDbContext dbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> role)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.role = role;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            AppUser appUser = new AppUser()
            {
                Name = vm.Name,
                UserName = vm.UserName,
                Email = vm.Email

            };
            var result = await userManager.CreateAsync(appUser, vm.Password);
            /* await userManager.AddToRoleAsync(appUser, Roles.Admin.ToString());*/
            await userManager.AddToRoleAsync(appUser, Roles.User.ToString());


            if (!result.Succeeded)
            {

                foreach (var item in result.Errors)
                {
                    if (item.Code == "")
                        ModelState.AddModelError("", "Duzgun daxil edilmeyib");
                }
                return View(vm);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var newUser = await userManager.FindByEmailAsync(vm.EmailOrUserName) ??
                await userManager.FindByNameAsync(vm.EmailOrUserName);

            if (newUser == null)
            {
                ModelState.AddModelError("", "Melumatlar duzgun deyil");
                return View(vm);
            }
            var result = await signInManager.CheckPasswordSignInAsync(newUser, vm.Password, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Melumatlar duzgun deyil");
                return View(vm);
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Az sonra yoxlayin");
                return View(vm);
            }
            await signInManager.SignInAsync(newUser, true);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                await role.CreateAsync(new IdentityRole
                {
                    Name = item.ToString(),
                });

            }
            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
