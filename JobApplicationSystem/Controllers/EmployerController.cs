using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationSystem.Controllers
{
    [Authorize(Roles = "Employer")]
    public class EmployerController : Controller
    {
        // GET: EmployerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EmployerController/Details/5
        public ActionResult Details(int id)
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
