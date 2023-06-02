using AutoMapper;
using ChatWebApi
    
    .Contracts.Member;
using ChatWebApi.Models.Member;
using ChatWebApi.Services.TokenGenerator;

namespace ChatWebApi.MappingProfiles.Member
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<MemberDto, MemberEntity>()
                .ForMember(x => x.Password, opt => opt.AddTransform(source => BCrypt.Net.BCrypt.HashPassword(source)));

            CreateMap<MemberEntity, MemberDto>();

            CreateMap<RegisterMemberRequest, MemberDto>();

            CreateMap<MemberDto, RegisterMemberRequest>();

            CreateMap<RegisterMemberResponse, MemberDto>();

            CreateMap<MemberDto, RegisterMemberResponse>();

            CreateMap<LoginMemberRequest, MemberDto>();

            CreateMap<MemberDto, LoginMemberRequest>();

            CreateMap<MemberDto, GetMemberBySizeResponse>();

            CreateMap<GetMemberBySizeResponse, MemberDto>();

            CreateMap<MemberDto, LoginMemberResponse>()
                .ForMember(x => x.Jwt, opt => opt.Ignore());

            CreateMap<LoginMemberResponse, MemberDto>();
            
        }
    }
}
