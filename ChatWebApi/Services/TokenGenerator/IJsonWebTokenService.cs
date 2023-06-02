using ChatWebApi.Models.Member;

namespace ChatWebApi.Services.TokenGenerator
{
    public interface IChatWebApiService
    {
        public string? GenerateToken(MemberDto memberDto);
    }
}
