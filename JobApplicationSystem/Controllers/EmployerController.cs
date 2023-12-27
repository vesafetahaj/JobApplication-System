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
        private readonly ICompanyService _companyService;
        public EmployerController(IEmployerService employerService, ICompanyService companyService)
        {
            _employerService = employerService;
            _companyService = companyService;
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

            var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);

            if (_employerService.HasProvidedPersonalInfo(loggedInUserId))
            {
                return RedirectToAction(nameof(Details));
            }

            var companies = _companyService.GetCompaniesAsync().Result;
            var companyList = new SelectList(companies, "CompanyId", "Name");

            ViewData["CompanyList"] = companyList;

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

            var companies = _companyService.GetCompaniesAsync().Result;
            var companyList = new SelectList(companies, "CompanyId", "Name");
            ViewData["CompanyList"] = companyList;

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
                    var companies = _companyService.GetCompaniesAsync().Result;
                    var companyList = new SelectList(companies, "CompanyId", "Name");

                    ViewData["CompanyList"] = companyList;

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

            if (employer.Company == null)
            {
                ModelState.AddModelError("Company", "Company is required.");
            }
            if (string.IsNullOrWhiteSpace(employer.Image))
            {
                ModelState.AddModelError("Image", "Image is required.");
            }

            if (!ModelState.IsValid)
            {
                var companiesForView = _companyService.GetCompaniesAsync().Result;
                var companyListForView = new SelectList(companiesForView, "CompanyId", "Name");
                ViewData["CompanyList"] = companyListForView;

                return View(employer);
            }

            employer.User = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await _employerService.EditPersonalInfoAsync(employer))
            {
                return RedirectToAction("Details", new { id = employer.EmployerId });
            }

            var companies = _companyService.GetCompaniesAsync().Result;
            var companyList = new SelectList(companies, "CompanyId", "Name");
            ViewData["CompanyList"] = companyList;

            return View(employer);
        }


        public ActionResult Index()
        {
            return View();
        }
     
        // GET: EmployerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployerController/Create
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

        // GET: EmployerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployerController/Edit/5
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

        // GET: EmployerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployerController/Delete/5
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
