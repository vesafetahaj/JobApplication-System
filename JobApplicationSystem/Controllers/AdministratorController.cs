using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {

        private readonly IAdministratorService _adminService;
        public AdministratorController(IAdministratorService adminService)
        {
            _adminService = adminService;

        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var admin = await _adminService.GetAdministratorDetailsAsync(loggedInUserId);
            return admin != null ? View(admin) : (IActionResult)NotFound();
        }

        public async Task<IActionResult> PersonalInfo()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_adminService.HasProvidedPersonalInfo(loggedInUserId))
            {
                return RedirectToAction(nameof(Details));
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonalInfo([Bind("AdminId,Name,Surname,Email,Mobile,User,Image,Address")] Administrator administrator)
        {
            if (!ModelState.IsValid)
            {

                return View(administrator);
            }

            administrator.User = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await _adminService.SavePersonalInfoAsyncAdmin(administrator))
            {
                return RedirectToAction("Details", new { id = administrator.AdminId });
            }

            return View(administrator);
        }


        public async Task<IActionResult> EditPersonalInfo()
        {
            try
            {
                string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var admin = await _adminService.GetAdministratorDetailsAsync(loggedInUserId);
                if (admin != null)
                {
                    return View(admin);

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPersonalInfo([Bind("AdminId,Name,Surname,Email,Mobile,User,Image,Address")] Administrator admin)
        {
            if (string.IsNullOrWhiteSpace(admin.Name))
            {
                ModelState.AddModelError("Name", "Name is required.");
            }
            if (string.IsNullOrWhiteSpace(admin.Surname))
            {
                ModelState.AddModelError("Surname", "Surname is required.");
            }
            if (string.IsNullOrWhiteSpace(admin.Email))
            {
                ModelState.AddModelError("Email", "Email is required.");
            }

            if (string.IsNullOrWhiteSpace(admin.Mobile))
            {
                ModelState.AddModelError("Mobile", "Mobile is required.");
            }
            if (string.IsNullOrWhiteSpace(admin.Image))
            {
                ModelState.AddModelError("Image", "Image is required.");
            }

            if (string.IsNullOrWhiteSpace(admin.Address))
            {
                ModelState.AddModelError("Address", "Address is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(admin);
            }


            if (!ModelState.IsValid)
            {
                return View(admin);
            }

            admin.User = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await _adminService.EditPersonalInfoAsync(admin))
            {
                return RedirectToAction("Details", new { id = admin.AdminId });
            }
            return View(admin);

        }
        public ActionResult Index()
        {
            return View();
        }
      
        
    }
}
