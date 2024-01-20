using System;
using System.Collections.Generic;
using JobApplicationSystem.BAL.Services;
using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Models;
using JobApplicationSystem.DAL.Repositories;

namespace JobApplicationSystem.BLL.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository<Feedback> _feedbackRepository;

        public FeedbackService(IFeedbackRepository<Feedback> feedbackRepository)
        {
            _feedbackRepository = feedbackRepository ?? throw new ArgumentNullException(nameof(feedbackRepository));
        }

        public IEnumerable<Feedback> GetAllFeedback()
        {
            return _feedbackRepository.GetAllFeedback();
        }

        public void SubmitFeedback(Feedback feedback)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }

            if (feedback.Rating < 1 || feedback.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }
            _feedbackRepository.AddFeedback(feedback);
        }
    }
}
