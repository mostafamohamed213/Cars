using Cars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cars.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly string smtpHost, smtpUsername, smtpPassword, smtpFrom, toollink;
        private readonly int smtpPort;
        private readonly bool smtpEnableSSL;


        public AccountsController(IConfiguration configuration, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            //toollink = configuration.GetSection("Core").GetSection("Tool_Link").Value;
            //smtpHost = configuration.GetSection("Core").GetSection("SMTP_Host").Value;
            //smtpUsername = configuration.GetSection("Core").GetSection("SMTP_Username").Value;
            //smtpPassword = configuration.GetSection("Core").GetSection("SMTP_Password").Value;
            //smtpFrom = configuration.GetSection("Core").GetSection("SMTP_From").Value;
            //smtpPort = int.Parse(configuration.GetSection("Core").GetSection("SMTP_Port").Value);
            //smtpEnableSSL = bool.Parse(configuration.GetSection("Core").GetSection("SMTP_EnableSSL").Value);
        }

        //[HttpGet]
        //public async Task<string> GetCurrentUserId()
        //{
        //    ApplicationUser usr = await GetCurrentUserAsync();
        //    return usr?.Id;
        //}
        //private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        public IActionResult Index()
        {


            return View();
        }

        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }


     


 
        [HttpGet]
        public IActionResult Register()
        {
            int usersCount = userManager.Users.Count();
            if (usersCount < 300)
            {
                ViewData["roles"] = new SelectList(roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8"), "Name", "Name");
                //   var roles = roleManager.Roles;
                return View();
            }
            else
            {
                return View("MaxUsers");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    SeconedName = model.SeconedName,
                 

                };

                // Store user data in AspNetUsers database table
              //  string password = GenerateRandomPassword();
                var result = await userManager.CreateAsync(user, model.Password);
                IdentityResult result2 = null;
                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController
                if (result.Succeeded)
                {
                    result2 = await userManager.AddToRoleAsync(user, model.RoleName);
                    //   await signInManager.SignInAsync(user, isPersistent: false);
                    // return RedirectToAction("index", "home");

                    if (result2.Succeeded)
                    {
                       // SendMail(user.Email, user.FirstName, user.SeconedName, user.UserName, password);
                        return RedirectToAction("ListUsers", "Administration");
                    }
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewData["roles"] = new SelectList(roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8"), "Name", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {

                    

                    return RedirectToAction("index", "home");
                  
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }
    }
    internal class remotip
    {
        public string ip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
    }
}
