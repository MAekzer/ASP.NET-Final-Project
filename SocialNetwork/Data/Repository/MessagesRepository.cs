using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Repository
{
    public class MessagesRepository : Repository<Message>
    {
        public MessagesRepository(ApplicationDbContext db)
        : base(db)
        {

        }

        /*
        public async Task<List<Message>> GetBySender(string senderId)
        {
            var query = Set.
                Include(m => m.Sender)
                .Include(m => m.Recipient)
                .AsQueryable()
                .Where(m => m.SenderId == senderId);

            if (query is null) return new List<Message>();

            return await query.ToListAsync();
        }

        public async Task<List<Message>> GetByRecipient(string recipientId)
        {
            var query = Set
                .Include(m => m.Sender)
                .Include(m => m.Recipient).
                AsQueryable()
                .Where(m => m.RecipientId == recipientId);

            if (query is null) return new List<Message>();

            return await query.ToListAsync();
        }
        */

        public async Task<List<Message>> GetChat(string userId, string otherId)
        {
            var query = Set
                .Include(m => m.Sender)
                .Include(m => m.Recipient)
                .AsQueryable()
                .Where(m => (m.SenderId == userId && m.RecipientId == otherId)
                         || (m.SenderId == otherId && m.RecipientId == userId));

            if (query is null) return new List<Message>();
                    
            return await query.ToListAsync();
        }

        public async Task Send(User sender, User recipient, string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            Message message = new()
            {
                SenderId = sender.Id,
                Sender = sender,
                RecipientId = recipient.Id,
                Recipient = recipient,
                Text = text,
                Time = DateTime.Now,
            };

            await Create(message);
        }
    }
}
