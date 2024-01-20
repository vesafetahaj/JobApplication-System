using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Contracts
{
    public interface IFeedbackRepository<Feedback>
    {
        IEnumerable<Feedback> GetAllFeedback();
        void AddFeedback(Feedback feedback);
    }
}
