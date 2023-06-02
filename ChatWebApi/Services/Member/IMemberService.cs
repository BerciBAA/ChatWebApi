using ChatWebApi.Models.Member;

namespace ChatWebApi.Services.Member
{
    public interface IMemberService
    {
        public Task<MemberDto?> RegisterMemberAsync(MemberDto memberDto);

        public Task<MemberDto?> LoginMemberAsync(MemberDto memberDto);

        public Task<MemberDto?> GetMemberByMemberIdAsync(Guid memberId);

        public Task<MemberDto?> GetMemberByEmailAsync(string email);

        public Task<MemberDto?> GetMemberByNameAsync(string name);

        public Task<IEnumerable<MemberDto>?> GetMemberBySizeAsync(int page, int size);
    }
}
