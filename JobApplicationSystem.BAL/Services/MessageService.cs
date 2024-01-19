using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationSystem.BAL.Services
{
    public class MessageService : IMessageService
    {
        private readonly JobApplicationSystemContext _context;

        public MessageService(JobApplicationSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(string userId)
        {
            return await _context.Messages
                .Where(m => m.Sender == userId || m.Receiver == userId)
                .OrderBy(m => m.Date)
                .ToListAsync();
        }

        public async Task SendMessageAsync(string senderId, string receiverId, string content)
        {
            var message = new Message
            {
                Sender = senderId,
                Receiver = receiverId,
                Content = content,
                Date = DateTime.Now
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}
