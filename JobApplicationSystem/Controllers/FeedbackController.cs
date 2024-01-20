using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplicationSystem.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var feedbackList = _feedbackService.GetAllFeedback();
            return View(feedbackList);
        }

        [Authorize(Roles = "Admin, Applicant, Employer")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Applicant, Employer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback
                {
                    Rating = feedbackViewModel.Rating,
                    Comments = feedbackViewModel.Comments,
                };

                _feedbackService.SubmitFeedback(feedback);

                return RedirectToAction("ThankYou");
            }

            return View(feedbackViewModel);
        }

        [Authorize(Roles = "Admin, Applicant, Employer")]
        public ActionResult ThankYou()
        {
            return View();
        }

    }
}
