using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationSystem.Controllers
{

    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly IEmployerService _employerService;
        private readonly IApplicantService _applicantService;


        public JobController(IJobService jobService, IEmployerService employerService, IApplicantService applicantService)
        {
            _jobService = jobService;
            _employerService = employerService;
            _applicantService = applicantService;   
        }

        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Index()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);

            if (employer != null)
            {
                var jobs = await _jobService.GetJobsByEmployerIdAsync(employer.EmployerId);
                return View(jobs);
            }
            else
            {
                return RedirectToAction("PersonalInfo", "Employer");
            }
        }

        [Authorize(Roles = "Employer, Applicant")]
        public async Task<ActionResult> Details(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        [Authorize(Roles = "Employer")]
        public ActionResult CreateJob()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!_employerService.HasProvidedPersonalInfo(loggedInUserId))
            {
                TempData["ErrorMessage"] = "You cannot take actions without providing personal information first.";
                return RedirectToAction("PersonalInfo", "Employer");
            }
            return View();
        }

        public ActionResult UnauthorizedJob()
        {
            return View();
        }

        [Authorize(Roles = "Employer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateJob(Job job)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);
                    
                    if (employer != null)
                    {
                        job.Employer = employer.EmployerId;
                        job.Company = employer.Company;

                        await _jobService.AddJobAsync(job);

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }

                return View(job);
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> EditJob(int id)
        {

            var job = await _jobService.GetJobByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);
            job.Company = employer.Company;


            if (job.Employer != employer.EmployerId)
            {
                return RedirectToAction(nameof(UnauthorizedJob));
            }

            return View(job);
        }

        [Authorize(Roles = "Employer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditJob(int id, Job updatedJob)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var job = await _jobService.GetJobByIdAsync(id);

                    if (job.Employer != updatedJob.Employer)
                    {
                        return RedirectToAction(nameof(UnauthorizedJob));
                    }

                    bool result = await _jobService.UpdateJobAsync(id, updatedJob);

                    if (result)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return View(updatedJob);
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> DeleteJob(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);

            if (job.Employer != employer.EmployerId)
            {
                return RedirectToAction(nameof(UnauthorizedJob));
            }

            return View(job);
        }

        [Authorize(Roles = "Employer")]
        [HttpPost, ActionName("DeleteJob")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteJobConfirmed(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);

            if (job.Employer != employer.EmployerId)
            {
                return RedirectToAction(nameof(UnauthorizedJob));
            }

            bool result = await _jobService.DeleteJobAsync(id);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }


        [Authorize(Roles = "Employer, Applicant")]
        public async Task<ActionResult> Jobs()
        {
            var allJobs = await _jobService.GetAllJobsAsync();
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            

            return View("Jobs", allJobs);
        }

        [Authorize(Roles = "Employer, Applicant")]
        public async Task<ActionResult> Search(string searchQuery)
        {
            var searchResults = await _jobService.SearchJobsAsync(searchQuery);
            var searchResultsList = searchResults.ToList();

            return View("Jobs", searchResultsList);
        }


    }
}
