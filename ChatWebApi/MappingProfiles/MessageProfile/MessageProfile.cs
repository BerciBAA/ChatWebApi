using AutoMapper;
using ChatWebApi.Contracts.Message;
using ChatWebApi.Models.Message;

namespace ChatWebApi.MappingProfiles.MessageProfile
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageDto, MessageEntity>();

            CreateMap<MessageEntity, MessageDto>();

            CreateMap<MessageDto, AddMessageRequest>();

            CreateMap<AddMessageRequest, MessageDto>();

            CreateMap<GetMessageBySizeResponse, MessageDto>();

            CreateMap<MessageDto, GetMessageBySizeResponse>();
        }
    }
}
