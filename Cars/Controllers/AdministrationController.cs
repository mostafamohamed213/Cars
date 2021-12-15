using Cars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cars.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


    
        [HttpGet]
        public async Task<IActionResult> ListUsers(string currentFilter,
  string searchString,
          string currentFilter2,
            string searchString2,
  int? pageNumber)
        {


            if (searchString != null || searchString2 != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
                searchString2 = currentFilter2;
            }
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilter2"] = searchString2;

            var users = (from s in userManager.Users
                         where s.Id != "039e233e-da34-4bbc-aa4a-8b5ff8942e48"
                         select s).OrderBy(a => a.UserName).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.ToLower().Trim().Contains(searchString.ToLower().Trim())).ToList();
            }
            if (!String.IsNullOrEmpty(searchString2))
            {
                users = users.Where(s => s.FirstName.ToLower().Trim().Contains(searchString2.ToLower().Trim())).ToList();
            }

            //var users = userManager.Users;
            // return View(users);
            int count = users.Count();
            var query = users.AsEnumerable().Select(async xs => new Users_in_Role_ViewModel
            {
                Username = xs.UserName,
                FirstName = xs.FirstName,
                Email = xs.Email,
                UserId = xs.Id,
                Role = string.Join(",", await userManager.GetRolesAsync(xs))
            }).ToList();

            int pageSize = 10;
            int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            ViewBag.pages = TotalPages;
            ViewBag.currentpage = pageNumber ?? 1;
            return View(await PaginatedList<Users_in_Role_ViewModel>.CreateAsync(query, pageNumber ?? 1, pageSize));
        }

        [Authorize(Permissions.Roles.View)]
        [HttpGet]
        public async Task<IActionResult> ListRoles(int? pageNumber)
        {

            IQueryable<IdentityRole> roles = roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8").AsNoTracking();
            // return View(roles);
            int count = roles.Count();
            int pageSize = 10;
            int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            ViewBag.pages = TotalPages;
            ViewBag.currentpage = pageNumber ?? 1;
            return View(await PaginatedList<IdentityRole>.CreateAsync(roles.OrderBy(a => a.Name), pageNumber ?? 1, pageSize));
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Permissions.Roles.Create)]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [Authorize(Permissions.Users.Edit)]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            // GetClaimsAsync retunrs the list of user Claims
            //  var userClaims = await userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await userManager.GetRolesAsync(user);
            var role = userRoles.FirstOrDefault();
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                SeconedName = user.SeconedName,
                Role = role

            };
            ViewData["roles"] = new SelectList(roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8"), "Name", "Name");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {


            var user = await userManager.FindByIdAsync(model.Id);



            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }

            else
            {
                if (ModelState.IsValid)
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    var role = userRoles.FirstOrDefault();
                    if (role != model.Role)
                    {
                        IdentityResult resultroleremove = null;
                        resultroleremove = await userManager.RemoveFromRoleAsync(user, role);
                        if (resultroleremove.Succeeded)
                        {
                            IdentityResult resultroleadd = null;
                            resultroleadd = await userManager.AddToRoleAsync(user, model.Role);
                            if (!resultroleadd.Succeeded)
                            {
                                foreach (var error3 in resultroleadd.Errors)
                                {
                                    ModelState.AddModelError("", error3.Description);
                                }
                                ViewData["roles"] = new SelectList(roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8"), "Name", "Name");
                                return View(model);
                            }
                        }
                        else
                        {
                            foreach (var error2 in resultroleremove.Errors)
                            {
                                ModelState.AddModelError("", error2.Description);
                            }
                            ViewData["roles"] = new SelectList(roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8"), "Name", "Name");
                            return View(model);
                        }
                    }
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.FirstName = model.FirstName;
                    user.SeconedName = model.SeconedName;
                    //  user.PasswordHash = userManager.PasswordHasher.HashPassword(user,model.NewPassword);

                    IdentityResult result = await userManager.UpdateAsync(user);





                    if (result.Succeeded)
                    {
                        //Generate Token

                        //Set new Password
                        if (!String.IsNullOrEmpty(model.NewPassword))
                        {
                            var token = await userManager.GeneratePasswordResetTokenAsync(user);

                            var result1 = await userManager.ResetPasswordAsync(user, token, model.NewPassword);
                            if (result1.Succeeded)
                            {
                                //       await signInManager.RefreshSignInAsync(user);
                                //   await userManager.UpdateSecurityStampAsync(user);
                                return RedirectToAction("ListUsers");
                            }
                            else
                            {
                                foreach (var error1 in result1.Errors)
                                {
                                    ModelState.AddModelError("", error1.Description);
                                }
                                ViewData["roles"] = new SelectList(roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8"), "Name", "Name");
                                return View(model);
                            }
                        }
                        //   await userManager.UpdateSecurityStampAsync(user);
                        //    await signInManager.RefreshSignInAsync(user);

                        return RedirectToAction("ListUsers");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                ViewData["roles"] = new SelectList(roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8"), "Name", "Name");
                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> EditUserByuser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            // GetClaimsAsync retunrs the list of user Claims
            //  var userClaims = await userManager.GetClaimsAsync(user);
            // GetRolesAsync returns the list of user Roles
            //  var userRoles = await userManager.GetRolesAsync(user);
            // var role = userRoles.FirstOrDefault();
            var model = new EditUserByuserViewModel
            {
                userId = user.Id,


            };
            //  ViewData["roles"] = new SelectList(roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8"), "Name", "Name");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserByuser(EditUserByuserViewModel model)
        {



            var user = await userManager.FindByIdAsync(model.userId);



            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.userId} cannot be found";
                return View("NotFound");
            }

            else
            {
                if (ModelState.IsValid)
                {


                    //Set new Password
                    if (!String.IsNullOrEmpty(model.NewPassword))
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);

                        var result1 = await userManager.ResetPasswordAsync(user, token, model.NewPassword);
                        if (result1.Succeeded)
                        {
                            user.EmailConfirmed = false;
                            IdentityResult result = await userManager.UpdateAsync(user);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            foreach (var error1 in result1.Errors)
                            {
                                ModelState.AddModelError("", error1.Description);
                            }
                            //  ViewData["roles"] = new SelectList(roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8"), "Name", "Name");
                            return View(model);
                        }
                    }
                    //   await userManager.UpdateSecurityStampAsync(user);
                    //    await signInManager.RefreshSignInAsync(user);


                }

                // ViewData["roles"] = new SelectList(roleManager.Roles.Where(a => a.Id != "4ee900da-b09e-49f0-8a08-2ebd111058c8"), "Name", "Name");
                return View(model);
            }
        }

        [Authorize(Permissions.Roles.ManagePermissions)]
        public async Task<IActionResult> ManagePermissions(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
                return NotFound();

            var roleClaims = roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();
            var allClaims = Permissions.GenerateAllPermissions();
            var allPermissions = allClaims.Select(p => new CheckBoxViewModel { DisplayValue = p }).ToList();

            foreach (var permission in allPermissions)
            {
                if (roleClaims.Any(c => c == permission.DisplayValue))
                    permission.IsSelected = true;
            }

            var viewModel = new PermissionsFormViewModel
            {
                RoleId = roleId,
                RoleName = role.Name,
                RoleCalims = allPermissions
            };

            return View(viewModel);
        }


        // Role ID is passed from the URL to the action

        [Authorize(Permissions.Roles.Edit)]
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };



            return View(model);
        }

        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePermissions(PermissionsFormViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
                return NotFound();

            var roleClaims = await roleManager.GetClaimsAsync(role);

            foreach (var claim in roleClaims)
                await roleManager.RemoveClaimAsync(role, claim);

            var selectedClaims = model.RoleCalims.Where(c => c.IsSelected).ToList();

            foreach (var claim in selectedClaims)
                await roleManager.AddClaimAsync(role, new Claim("Permission", claim.DisplayValue));

            return RedirectToAction("ListRoles", "Administration");
        }
    }
}
