using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using System.Security.Claims;

namespace JobApplicationSystem.Controllers
{
    [Authorize(Roles = "Employer")]
    public class EmployerController : Controller
    {
        private readonly IEmployerService _employerService;
        private readonly IApplicantService _applicantService;
        private readonly IApplicationService _applicationService;
        private readonly IJobService _jobService;

        public EmployerController(IEmployerService employerService, IJobService jobService, IApplicantService applicantService, IApplicationService applicationService)
        {
            _employerService = employerService;
            _jobService = jobService;
            _applicantService = applicantService;       
            _applicationService = applicationService;   
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);
            return employer != null ? View(employer) : (IActionResult)NotFound();
        }

        public async Task<IActionResult> PersonalInfo()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (_employerService.HasProvidedPersonalInfo(loggedInUserId))
            {
                return RedirectToAction(nameof(Details));
            }

          

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonalInfo([Bind("EmployerId,Name,Surname,Email,Mobile,Address,Image,Company,User")] Employer employer)
        {
            employer.User = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await _employerService.SavePersonalInfoAsync(employer))
            {
                return RedirectToAction("Details", new { id = employer.EmployerId });
            }

            

            return View(employer);
        }

        [HttpGet]
        public async Task<IActionResult> EditPersonalInfo()
        {
            try
            {
                string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);

                if (employer != null)
                {
                    return View(employer);
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
        public async Task<IActionResult> EditPersonalInfo([Bind("EmployerId,Name,Surname,Email,Mobile,Address,Image,Company,User")] Employer employer)
        {
            if (string.IsNullOrWhiteSpace(employer.Name))
            {
                ModelState.AddModelError("Name", "Name is required.");
            }
            if (string.IsNullOrWhiteSpace(employer.Surname))
            {
                ModelState.AddModelError("Surname", "Surname is required.");
            }
            if (string.IsNullOrWhiteSpace(employer.Email))
            {
                ModelState.AddModelError("Email", "Email is required.");
            }

            if (string.IsNullOrWhiteSpace(employer.Mobile))
            {
                ModelState.AddModelError("Mobile", "Mobile is required.");
            }
            if (string.IsNullOrWhiteSpace(employer.Address))
            {
                ModelState.AddModelError("Address", "Address is required.");
            }
            if (string.IsNullOrWhiteSpace(employer.Company))
            {
                ModelState.AddModelError("Company", "Company is required.");
            }

            if (string.IsNullOrWhiteSpace(employer.Image))
            {
                ModelState.AddModelError("Image", "Image is required.");
            }

            if (!ModelState.IsValid)
            {
               

                return View(employer);
            }

            employer.User = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await _employerService.EditPersonalInfoAsync(employer))
            {
                return RedirectToAction("Details", new { id = employer.EmployerId });
            }

           

            return View(employer);
        }

        public async Task<IActionResult> ViewApplicants(int jobId)
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);

            var job = await _jobService.GetJobByIdAsync(jobId);

            if (job == null || job.Employer != employer.EmployerId)
            {
                return RedirectToAction("UnauthorizedJob", "Job");
            }

            var applicants = await _applicationService.GetApplicantsForJobAsync(jobId);

            ViewBag.Job = job;
            ViewBag.Applicants = applicants;

            return View();
        }


        public async Task<IActionResult> ApproveApplication(int applicationId)
        {
            var application = await _applicationService.GetApplicationByIdAsync(applicationId);

            if (application != null)
            {
                application.Status = "Approved";

                await _applicationService.UpdateApplicationAsync(applicationId, application);


               
            }

            return Ok();
        }

        public async Task<IActionResult> DeclineApplication(int applicationId)
        {
            var application = await _applicationService.GetApplicationByIdAsync(applicationId);

            if (application != null)
            {
                application.Status = "Declined";

                await _applicationService.UpdateApplicationAsync(applicationId, application);

            }
            return Ok();

        }
        public ActionResult Index()
        {
            return View();
        }
     
       
    }
}
