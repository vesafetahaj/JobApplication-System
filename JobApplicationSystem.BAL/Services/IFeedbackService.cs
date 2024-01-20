using JobApplicationSystem.DAL.Models;
using System.Collections.Generic;

namespace JobApplicationSystem.BAL.Services
{
    public interface IFeedbackService
    {
        IEnumerable<Feedback> GetAllFeedback();
        void SubmitFeedback(Feedback feedback);
    }
}
