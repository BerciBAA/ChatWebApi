using ChatWebApi.Context;
using ChatWebApi.Models.Message;
using Microsoft.EntityFrameworkCore;

namespace ChatWebApi.Repositories.Message
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatContext _chatContext;

        public MessageRepository(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public async Task<MessageEntity?> AddMessageAsync(MessageEntity messageEntity)
        {
            _chatContext.Messages.Add(messageEntity);

            await _chatContext.SaveChangesAsync();

            return _chatContext.Messages.FirstOrDefault(x => x.MessageId == messageEntity.MessageId);
        }

        public async Task<IEnumerable<MessageEntity>?> GetAllMessageByMemberIdAsync(Guid id)
        {
            return await _chatContext.Messages
                    .OrderByDescending(x => x.SendDateTime)
                    .Where(x => x.FromId == id)
                    .ToListAsync();
        }

        public async Task<IEnumerable<MessageEntity>?> GetMessagesBySizeAsync(Guid fromId, Guid toId, int page, int size)
        {
            return await _chatContext.Messages
                    .OrderByDescending(x => x.SendDateTime)
                    .Skip(page * size)
                    .Where(x => x.FromId == fromId)
                    .Where(x => x.ToId == toId)
                    .Take(size)
                    .OrderBy(x => x.SendDateTime)
                    .ToListAsync();
        }

        public async Task<IEnumerable<MessageEntity>?> GetAllMessageBySizeAsync(int size)
        {
            return await _chatContext.Messages
                    .OrderBy(x => x.SendDateTime)
                    .Take(size)
                    .ToListAsync();
        }

        public async Task<MessageEntity?> GetMessageByMessageIdAsync(Guid messageId)
        {
            return await _chatContext.Messages.FirstOrDefaultAsync(x => x.MessageId == messageId);
        }

        public async Task<MessageEntity?> ModifyMessageAsync(MessageEntity messageEntity)
        {
            var ModfiyMessage = await _chatContext.Messages
                                    .FirstOrDefaultAsync(x => x.MessageId == messageEntity.MessageId);

            ModfiyMessage = messageEntity;

            await _chatContext.SaveChangesAsync();

            return await _chatContext.Messages.FirstOrDefaultAsync(x => x.MessageId == ModfiyMessage.MessageId);
        }

        public async Task<bool> RemoveMessageByIdAsync(Guid messageId)
        {
            var messageEntity = await _chatContext.Messages.FirstOrDefaultAsync(x => x.MessageId == messageId);

            if(messageEntity is null)
                return false;

            _chatContext.Messages.Remove(messageEntity);

            var result = await _chatContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
