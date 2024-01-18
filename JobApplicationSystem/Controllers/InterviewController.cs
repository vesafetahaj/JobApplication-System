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
           
            await _interviewService.ScheduleInterviewAsync(interview);
            return RedirectToAction("Index", "Job"); 
        }

        return View(interview);
    }


    [Authorize(Roles = "Employer")]

    public async Task<IActionResult> Details(int id)
    {
        var interview = await _interviewService.GetInterviewByIdAsync(id);
        return View(interview);
    }

    // GET: Interview/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var interview = await _interviewService.GetInterviewByIdAsync(id);
        return View(interview);
    }

    // POST: Interview/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Interview interview)
    {
        if (id != interview.InterviewId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _interviewService.UpdateInterviewAsync(interview);
            return RedirectToAction("Index", "Job"); 
        }

        return View(interview);
    }

    // GET: Interview/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var interview = await _interviewService.GetInterviewByIdAsync(id);
        return View(interview);
    }

    // POST: Interview/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _interviewService.DeleteInterviewAsync(id);
        return RedirectToAction("Index", "Job"); 
    }

    [Authorize(Roles = "Employer")]

    public async Task<IActionResult> EmployerScheduledInterviews()
    {
        string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var employer = await _employerService.GetEmployerDetailsAsync(loggedInUserId);

        var interviews = await _interviewService.GetScheduledInterviewsForEmployerAsync(employer.EmployerId);

        return View(interviews);
    }
}
