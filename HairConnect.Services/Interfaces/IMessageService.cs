namespace HairConnect.Services.Interfaces
{
    using Data.Models;
    using Models.Messages;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        bool ConversationExists(string senderId, string receiverId);

        Task CreateConversation(string senderId, string receiverId);

        Task<Conversation> GetConversation(string senderId, string receiverId);

        ShowConversationModel GetConversationToShow(Conversation conversation);

        Task CreateMessage(string senderId, string receiverId, string content);

        Task DeleteMessages(Conversation conversation);

        Task<IEnumerable<ListConversationModel>> GetConversationsForUser(string id);

        Task DeleteAllMessagesAndConvosFromUser(User user);
    }
}
