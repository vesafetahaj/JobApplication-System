using Microsoft.AspNetCore.Mvc;
using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Models;
using System.Security.Claims;

namespace JobApplicationSystem.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicantService _applicantService;
        private readonly IJobService _jobService;
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicantService applicantService, IJobService jobService, IApplicationService applicationService)
        {
            _applicantService = applicantService;
            _jobService = jobService;
            _applicationService = applicationService;
        }

        public async Task<IActionResult> ApplyForJob(int jobId)
        {
            // Retrieve the job details
            var job = await _jobService.GetJobByIdAsync(jobId);

            // Create an empty Application model or customize based on your needs
            var applicationModel = new Application
            {
                JobNavigation = job // Associate the job details with the application model
            };

            return View(applicationModel);
        }
        [HttpPost]
        public async Task<IActionResult> ApplyForJob(Application application)
        {
            try
            {
                // Retrieve JobId from the form data
                int? jobId = application.Job;

                // Set the JobId and ApplicantId in the application
                application.Job = jobId;

                string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var applicant = await _applicantService.GetApplicantDetailsAsync(loggedInUserId);
                application.Applicant = applicant.ApplicantId;
                await _applicationService.AddApplicationAsync(application);
                return RedirectToAction("Jobs", "Job"); 
            }
            catch (ArgumentException ex)
            {
                // Handle validation errors
                ModelState.AddModelError(ex.ParamName, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Handle business rule violations
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                ModelState.AddModelError(string.Empty, "An error occurred while processing your application.");
            }

            // If there are errors, return to the form with errors
            return View(application);
        }

        
    }
}
