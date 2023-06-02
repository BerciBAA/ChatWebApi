using ChatWebApi.Models.Message;

namespace ChatWebApi.Repositories.Message
{
    public interface IMessageRepository
    {
        public Task<MessageEntity?> AddMessageAsync(MessageEntity messageEntity);

        public Task<bool> RemoveMessageByIdAsync(Guid messageId);

        public Task<MessageEntity?> ModifyMessageAsync(MessageEntity messageEntity);

        public Task<IEnumerable<MessageEntity>?> GetAllMessageBySizeAsync(int size);

        public Task<IEnumerable<MessageEntity>?> GetAllMessageByMemberIdAsync(Guid id);

        public Task<IEnumerable<MessageEntity>?> GetMessagesBySizeAsync(Guid fromId, Guid toId, int page, int size);

        public Task<MessageEntity?> GetMessageByMessageIdAsync(Guid messageId);
    }
}
