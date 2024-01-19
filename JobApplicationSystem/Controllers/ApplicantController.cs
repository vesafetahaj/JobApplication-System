using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Text;

namespace JobApplicationSystem.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IApplicantService _applicantService;
        private readonly IApplicationService _applicationService;

        public ApplicantController(IApplicantService applicantService, IApplicationService applicationService)
        {
            _applicantService = applicantService;
            _applicationService = applicationService;
        }

       
        [Authorize(Roles = "Applicant")]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicant = await _applicantService.GetApplicantDetailsAsync(loggedInUserId);
            return applicant != null ? View(applicant) : (IActionResult)NotFound();
        }

        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> PersonalInfo()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicant = await _applicantService.GetApplicantDetailsAsync(loggedInUserId);

            if (_applicantService.HasProvidedPersonalInfo(loggedInUserId))
            {
                return RedirectToAction(nameof(Details));
            }

            return View();
        }


        [Authorize(Roles = "Applicant")]
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

        [Authorize(Roles = "Applicant")]
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

        [Authorize(Roles = "Applicant")]
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
       
       
        [Authorize(Roles = "Applicant")]
        [HttpGet]
        public async Task<IActionResult> Applications()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applications = await _applicantService.GetApplicationsByUserIdAsync(loggedInUserId);
            return View(applications);
        }

        [Authorize(Roles = "Applicant, Employer")]
        public IActionResult DownloadResume(int applicationId)
        {
            var application = _applicationService.GetApplicationByIdAsync(applicationId).Result;

            if (application != null && !string.IsNullOrEmpty(application.Resume))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", application.Resume);

                if (System.IO.File.Exists(filePath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "application/octet-stream", application.Resume);
                }
            }

            return NotFound();
        }



    }
}
