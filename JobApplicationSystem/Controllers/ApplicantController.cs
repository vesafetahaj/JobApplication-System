using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace JobApplicationSystem.Controllers
{
    [Authorize(Roles = "Applicant")]
    public class ApplicantController : Controller
    {
        private readonly IApplicantService _applicantService;
        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
           
        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicant = await _applicantService.GetApplicantDetailsAsync(loggedInUserId);
            return applicant != null ? View(applicant) : (IActionResult)NotFound();
        }

        public async Task<IActionResult> PersonalInfo()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicant = await _applicantService.GetApplicantDetailsAsync(loggedInUserId);

            if (_applicantService.HasProvidedPersonalInfo(loggedInUserId))
            {
                return RedirectToAction(nameof(Details));
            }

            return View("PersonalInfo");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonalInfo([Bind("ApplicantId,Name,Surname,Email,Mobile,Address,Image,Education,User")] Applicant applicant, IFormFile resumeFile)
        {
            applicant.User = User.FindFirstValue(ClaimTypes.NameIdentifier);

           

            if (await _applicantService.SavePersonalInfoAsync(applicant))
            {
                return RedirectToAction("Details", new { id = applicant.ApplicantId });
            }

            return View(applicant);
        }
        public async Task<IActionResult> EditPersonalInfo()
        {
            try
            {
                string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var applicant = await _applicantService.GetApplicantDetailsAsync(loggedInUserId);
                if (applicant != null)
                {
                    return View(applicant);

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
        public async Task<IActionResult> EditPersonalInfo([Bind("ApplicantId,Name,Surname,Email,Mobile,Address,Image,User,Education")] Applicant applicant)
        {
            if (string.IsNullOrWhiteSpace(applicant.Name))
            {
                ModelState.AddModelError("Name", "Name is required.");
            }
            if (string.IsNullOrWhiteSpace(applicant.Surname))
            {
                ModelState.AddModelError("Surname", "Surname is required.");
            }
            if (string.IsNullOrWhiteSpace(applicant.Email))
            {
                ModelState.AddModelError("Email", "Email is required.");
            }

            if (string.IsNullOrWhiteSpace(applicant.Mobile))
            {
                ModelState.AddModelError("Mobile", "Mobile is required.");
            }
            if (string.IsNullOrWhiteSpace(applicant.Address))
            {
                ModelState.AddModelError("Address", "Address is required.");
            }
            if (string.IsNullOrWhiteSpace(applicant.Education))
            {
                ModelState.AddModelError("Education", "Education is required.");
            }


            if (string.IsNullOrWhiteSpace(applicant.Image))
            {
                ModelState.AddModelError("Image", "Image is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(applicant);
            }


            applicant.User = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await _applicantService.EditPersonalInfoAsync(applicant))
            {
                return RedirectToAction("Details", new { id = applicant.ApplicantId });
            }
            return View(applicant);

        }
        // GET: ApplicantController
        public ActionResult Index()
        {
            return View();
        }
        
        // GET: ApplicantController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicantController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicantController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplicantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicantController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplicantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
