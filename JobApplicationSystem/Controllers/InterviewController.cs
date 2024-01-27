using Microsoft.AspNetCore.Mvc;
using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

public class InterviewController : Controller
{
    private readonly IInterviewService _interviewService;
    private readonly IApplicationService _applicationService;
    private readonly IEmployerService _employerService;

    public InterviewController(IInterviewService interviewService, IApplicationService applicationService, IEmployerService employerService)
    {
        _interviewService = interviewService;
        _applicationService = applicationService;
        _employerService = employerService;
    }

    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> Schedule(int applicationId)
    {
        var application = await _applicationService.GetApplicationByIdAsync(applicationId);

        if (application == null)
        {

            return RedirectToAction("UnauthorizedJob", "Job");
        }

        var interviewModel = new Interview
        {
            Application = applicationId,
            ApplicationNavigation = application,
        };

        return View(interviewModel);
    }


    [Authorize(Roles = "Employer")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Schedule(Interview interview)
    {
        if (ModelState.IsValid)
        {
            bool isConflict = await _interviewService.HasConflictAsync(interview);

            if (isConflict)
            {
                ModelState.AddModelError(string.Empty, "An interview already exists at the same time or the selected time is before today.");
                return View(interview);
            }

            await _interviewService.ScheduleInterviewAsync(interview);
            return RedirectToAction("EmployerScheduledInterviews");
        }

        return View(interview);
    }



    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> Details(int id)
    {
        var interview = await _interviewService.GetInterviewByIdAsync(id);
        return View(interview);
    }

    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> Edit(int id)
    {
        var interview = await _interviewService.GetInterviewByIdAsync(id);

        if (interview == null)
        {
            return NotFound();
        }

        return View(interview);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> Edit(int id, Interview interview)
    {
        if (id != interview.InterviewId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            bool isConflict = await _interviewService.HasConflictAsync(interview);

            if (isConflict)
            {
                ModelState.AddModelError(string.Empty, "An interview already exists at the same time or the selected time is before today.");
                return View(interview);
            }


            var existingInterview = await _interviewService.GetInterviewByIdAsync(id);
            interview.Application = existingInterview.Application;

            await _interviewService.UpdateInterviewAsync(interview);

            return RedirectToAction("EmployerScheduledInterviews");
        }

        return View(interview);
    }


    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> Delete(int id)
    {
        var interview = await _interviewService.GetInterviewByIdAsync(id);

        if (interview == null)
        {
            return NotFound();
        }

        return View(interview);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _interviewService.DeleteInterviewAsync(id);
        return RedirectToAction("EmployerScheduledInterviews");
    }

    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> EmployerScheduledInterviews()
    {
        string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);
        if (!_employerService.HasProvidedPersonalInfo(loggedInUserId))
        {
            TempData["ErrorMessage"] = "You cannot take actions without providing personal information first.";
            return RedirectToAction("PersonalInfo", "Employer");
        }
        var interviews = await _interviewService.GetScheduledInterviewsForEmployerAsync(employer.EmployerId);

        return View(interviews);
    }
}
