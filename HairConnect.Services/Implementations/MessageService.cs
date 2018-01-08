namespace HairConnect.Services.Implementations
{
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using Models.Messages;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class MessageService : IMessageService
    {
        private readonly HairConnectDbContext db;

        public MessageService(HairConnectDbContext db)
        {
            this.db = db;
        }

        public bool ConversationExists(string senderId, string receiverId)
        {
            if (this.db.Conversations.Any(c => c.SenderId == senderId && c.ReceiverId == receiverId))
            {
                return true;
            }

            return false;
        }

        public async Task CreateConversation(string senderId, string receiverId)
        {
            Conversation senderConversation = new Conversation()
            {
                SenderId = senderId,
                ReceiverId = receiverId
            };

            Conversation receiverConversation = new Conversation()
            {
                SenderId = receiverId,
                ReceiverId = senderId
            };

            await this.db.Conversations.AddAsync(senderConversation);
            await this.db.Conversations.AddAsync(receiverConversation);

            await this.db.SaveChangesAsync();
        }

        public async Task CreateMessage(string senderId, string receiverId, string content)
        {
            Message message = new Message()
            {
                Content = content,
                SenderId = senderId
            };

            Conversation senderConversation = this.GetConversation(senderId, receiverId).Result;
            Conversation receiverConversation = this.GetConversation(receiverId, senderId).Result;
            message.Conversations.Add(new ConversationMessage() { ConversationId = senderConversation.Id });
            message.Conversations.Add(new ConversationMessage() { ConversationId = receiverConversation.Id });
            this.db.Messages.Add(message);

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAllMessagesAndConvosFromUser(User user)
        {
            IEnumerable<Message> messages = this.db.Messages.Where(m => m.SenderId == user.Id);
            IEnumerable<Conversation> conversations = this.db.Conversations.Where(m => m.SenderId == user.Id || m.ReceiverId == user.Id);
            this.db.Messages.RemoveRange(messages);
            this.db.Conversations.RemoveRange(conversations);

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteMessages(Conversation conversation)
        {
            conversation.Messages.Clear();

            await this.db.SaveChangesAsync();
        }

        public async Task<Conversation> GetConversation(string senderId, string receiverId)
        {
            Conversation conversation = await this.db
                .Conversations
                .Where(c => c.SenderId == senderId && c.ReceiverId == receiverId)
                .Include(c => c.Sender)
                .Include(c => c.Receiver)
                .Include(c => c.Messages)
                .ThenInclude(m => m.Message)
                .FirstOrDefaultAsync();

            return conversation;
        }

        public async Task<IEnumerable<ListConversationModel>> GetConversationsForUser(string id)
        {
            return await this.db
                .Conversations
                .Where(c => c.SenderId == id)
                .Include(c => c.Receiver)
                .ProjectTo<ListConversationModel>()
                .ToListAsync();
        }

        public ShowConversationModel GetConversationToShow(Conversation conversation)
        {
            return Mapper.Map<ShowConversationModel>(conversation);
        }
    }
}
