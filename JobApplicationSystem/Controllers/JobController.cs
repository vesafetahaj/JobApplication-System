using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationSystem.Controllers
{
    [Authorize(Roles = "Employer")]

    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly IEmployerService _employerService;

        public JobController(IJobService jobService, IEmployerService employerService)
        {
            _jobService = jobService;
            _employerService = employerService;
        }
        // GET: JobController
        public async Task<ActionResult> Index()
        {
            var jobs = await _jobService.GetAllJobsAsync(); // Assuming you have an asynchronous method to get all jobs
            var jobCardViewModels = jobs.Select(job => new Job
            {
                JobId = job.JobId,
                Title = job.Title,
                Description = job.Description,
                Company = job.Company,
                LastDateToApply = job.LastDateToApply,
                Salary = job.Salary
            }).ToList();

            return View(jobCardViewModels);
        }

        // GET: JobController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JobController/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateJob()
        {
            return View();
        }
        // POST: JobController/Create
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

        // POST: JobController/Create
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

        // GET: JobController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JobController/Edit/5
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

        // GET: JobController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobController/Delete/5
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
