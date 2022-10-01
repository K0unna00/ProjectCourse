using FinalAgain.Areas.manage.ViewModels;
using FinalAgain.DAL;
using FinalAgain.Helpers;
using FinalAgain.Models;
using FinalAgain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinalAgain.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDBContext _context;

        public UserAccountController(UserManager<AppUser> userManager,
                                      SignInManager<AppUser> signInManager,
                                        IWebHostEnvironment env,
                                        RoleManager<IdentityRole> roleManager,
                                        AppDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                int roleValue = Convert.ToInt32(model.Role);
                string role = Enum.GetName(typeof(Roles), roleValue);
                var user = new AppUser
                {
                    UserName = model.UserName,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded )
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, role);
                    if(roleResult.Succeeded)
                        return RedirectToAction("index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            if (model.ConfirmPassword != model.Password)
            {
                ModelState.AddModelError("ConfirmPassword", "Password and ConfirmPassword doestn match");
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                if (user.Password == null)
                {
                    ModelState.AddModelError("Password", "Password is Required!!");
                }
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email )
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest();

            var isExists = await _userManager.FindByEmailAsync(email);
            if (isExists == null)
                return NotFound();

            return RedirectToAction("Login");
        }
        public async Task<IActionResult> ResetPassword(string id, string token)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(token))
                return BadRequest();

            var isExists = await _userManager.FindByIdAsync(id);
             
            if (isExists == null)
                return NotFound();

            ResetPasswordViewModel resetPasswordVW = new ResetPasswordViewModel
            {
                Email = isExists.Email,
                Token = token
            };
            return View(resetPasswordVW);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string id, ResetPasswordViewModel resetPasswordVW)
        {

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(resetPasswordVW.Token))
                return BadRequest();
            if (string.IsNullOrEmpty(resetPasswordVW.NewPassword) ||  string.IsNullOrEmpty(resetPasswordVW.ConfirmPassword))
            {
                ModelState.AddModelError("", "New password and Confirm password is required");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            var isExists = await _userManager.FindByIdAsync(id);
            if (isExists == null)
                return RedirectToAction("error", "dashboard");
            var result = await _userManager.ResetPasswordAsync(isExists, resetPasswordVW.Token, resetPasswordVW.NewPassword);
            if (result.Errors == null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Login");
        }

        //// This Method is used for Creating Model
        //public IActionResult CreateRole()
        //{ 
        //    var result= _roleManager.CreateAsync(new IdentityRole("Marker")).Result;
        //    var result= _roleManager.CreateAsync(new IdentityRole("Admin")).Result;
        //    var result= _roleManager.CreateAsync(new IdentityRole("Student")).Result;
        //    return Ok("salam");
        //}
    }
}
