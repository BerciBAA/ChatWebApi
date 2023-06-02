using AutoMapper;
using ChatWebApi.Models.Member;
using ChatWebApi.Models.Message;
using ChatWebApi.Repositories.Message;
using ChatWebApi.Services.Member;

namespace ChatWebApi.Services.Message
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper, IMemberService memberService)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _memberService = memberService;
        }

        public async Task<MessageDto?> AddMessageAsync(MessageDto messageDto)
        {
            var messageEntity = _mapper.Map<MessageDto, MessageEntity>(messageDto);

            var resultMessageDto = await _messageRepository.AddMessageAsync(messageEntity);

            if(resultMessageDto is null)
                return null;

            return _mapper.Map<MessageEntity, MessageDto>(resultMessageDto);
        }

        public async Task<IEnumerable<MessageDto>?> GetAllMessageByMemberIdAsync(Guid id)
        {
            var messagesEntity = await _messageRepository.GetAllMessageByMemberIdAsync(id);

            if (messagesEntity is null)
                return null;

            return _mapper.Map<IEnumerable<MessageEntity>, IEnumerable<MessageDto>>(messagesEntity);
        }

        public async Task<IEnumerable<MessageDto>?> GetMessagesBySizeAsync(Guid fromId, Guid toId, int page, int size)
        {
            var messagesEntity = await _messageRepository.GetMessagesBySizeAsync(fromId, toId, page, size);

            if (messagesEntity is null)
                return null;

            return _mapper.Map<IEnumerable<MessageEntity>, IEnumerable<MessageDto>>(messagesEntity);
        }

        public async Task<IEnumerable<MessageDto>?> GetAllMessageBySizeAsync(int size)
        {
            var messagesEntity = await _messageRepository.GetAllMessageBySizeAsync(size);

            if (messagesEntity is null)
                return null;

            return _mapper.Map<IEnumerable<MessageEntity>, IEnumerable<MessageDto>>(messagesEntity);
        }

        public async Task<MessageDto?> GetMessageByMessageIdAsync(Guid messageId)
        {
            var messagesEntity = await _messageRepository.GetMessageByMessageIdAsync(messageId);

            if (messagesEntity is null)
                return null;

            return _mapper.Map<MessageEntity, MessageDto>(messagesEntity);
        }

        public async Task<MessageDto?> ModifyMessageAsync(MessageDto messageDto)
        {
            var messageEntity = _mapper.Map<MessageDto, MessageEntity>(messageDto);

            var resultMessageDto = await _messageRepository.ModifyMessageAsync(messageEntity);

            if (resultMessageDto is null)
                return null;

            return _mapper.Map<MessageEntity, MessageDto>(resultMessageDto);
        }

        public async Task<bool> RemoveMessageByIdAsync(Guid messageId)
        {
            return await _messageRepository.RemoveMessageByIdAsync(messageId);
        }
    }
}
