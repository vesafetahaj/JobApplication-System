using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.BAL.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessagesAsync(string userId);
        Task SendMessageAsync(string senderId, string receiverId, string content);
    }
}
