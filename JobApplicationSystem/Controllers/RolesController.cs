using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using JobApplicationSystem.BAL.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobApplicationSystem.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _manager;
        private readonly IRoleService _roleService;
        public RolesController(RoleManager<IdentityRole> roleManager, IRoleService roleService)
        {
            _manager = roleManager;
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            var roles = _roleService.GetAllRoles();
            ViewData["Roles"] = new SelectList(roles);
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var roleExists = _manager.RoleExistsAsync(role.Name).GetAwaiter().GetResult();
                if (!roleExists)
                {
                    var result = _manager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Role already exists.");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var role = _manager.FindByIdAsync(id).GetAwaiter().GetResult();
            if (role != null)
            {
                return View(role);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var existingRole = _manager.FindByIdAsync(role.Id).GetAwaiter().GetResult();
                if (existingRole != null)
                {
                    existingRole.Name = role.Name;
                    var result = _manager.UpdateAsync(existingRole).GetAwaiter().GetResult();
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Role not found.");
                }
            }

            return View(role);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var role = _manager.FindByIdAsync(id).GetAwaiter().GetResult();
            if (role != null)
            {
                return View(role);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string id)
        {
            var role = _manager.FindByIdAsync(id).GetAwaiter().GetResult();
            if (role != null)
            {
                var result = _manager.DeleteAsync(role).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}
