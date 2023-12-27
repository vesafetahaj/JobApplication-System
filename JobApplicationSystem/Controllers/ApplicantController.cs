using JobApplicationSystem.BAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobApplicationSystem.Controllers
{
    [Authorize(Roles = "Applicant")]
    public class ApplicantController : Controller
    {
        private readonly IApplicantService _applicantService;
        public ApplicantController(IApplicantService applicantService)
        {
            applicantService = _applicantService;
           
        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var applicant = await _applicantService.GetApplicantDetailsAsync(loggedInUserId);
            return applicant != null ? View(applicant) : (IActionResult)NotFound();
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
