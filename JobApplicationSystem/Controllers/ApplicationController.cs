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
           
            var job = await _jobService.GetJobByIdAsync(jobId);


            var applicationModel = new Application
            {
                JobNavigation = job
            };

            return View(applicationModel);
        }
        [HttpPost]
        public async Task<IActionResult> ApplyForJob([Bind("ApplicationId,Education,Experience,Applicant,Job,Resume")] Application application)
        {
            try
            {
                int? jobId = application.Job;
                application.Job = jobId;
                

                string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var applicant = await _applicantService.GetApplicantDetailsAsync(loggedInUserId);
                application.Applicant = applicant.ApplicantId;
                application.Education = applicant.Education;

                if (!_applicationService.CheckIfApplicantApplied(application.Applicant, application.Job))
                {
                    await _applicationService.AddApplicationAsync(application);
                    return RedirectToAction("Applications", "Applicant");
                }
                else
                {
                    return RedirectToAction("UnauthorizedApplication");
                }
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your application.");
            }

            return View(application);
        }

        public IActionResult UnauthorizedApplication()
        {
            return View();
        }

    }
}
