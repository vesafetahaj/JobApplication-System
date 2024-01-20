using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationSystem.DAL.Repositories
{
    public class FeedbackRepository : IFeedbackRepository<Feedback>
    {
        private readonly JobApplicationSystemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeedbackRepository(JobApplicationSystemContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public IEnumerable<Feedback> GetAllFeedback()
        {
            return _context.Feedbacks.Include(f => f.AspNetUserNavigation).ToList();
        }

        public void AddFeedback(Feedback feedback)
        {
            if (feedback == null)
            {
                throw new ArgumentNullException(nameof(feedback));
            }

            string loggedInUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            feedback.AspNetUser = loggedInUserId;
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }
    }
}
