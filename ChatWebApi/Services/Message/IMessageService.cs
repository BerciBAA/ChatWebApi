using ChatWebApi.Models.Message;

namespace ChatWebApi.Services.Message
{
    public interface IMessageService
    {
        public Task<MessageDto?> AddMessageAsync(MessageDto messageDto);

        public Task<bool> RemoveMessageByIdAsync(Guid messageId);

        public Task<MessageDto?> ModifyMessageAsync(MessageDto messageDto);

        public Task<IEnumerable<MessageDto>?> GetAllMessageBySizeAsync(int size);

        public Task<IEnumerable<MessageDto>?> GetAllMessageByMemberIdAsync(Guid id);

        public Task<IEnumerable<MessageDto>?> GetMessagesBySizeAsync(Guid fromId, Guid toId, int page, int size);

        public Task<MessageDto?> GetMessageByMessageIdAsync(Guid messageId);
    }
}
