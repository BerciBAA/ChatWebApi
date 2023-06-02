using ChatWebApi.Models.Member;

namespace ChatWebApi.Repositories.Member
{
    public interface IMemberRepository
    {
        public Task<MemberEntity?> RegisterMemberAsync(MemberEntity memberEntity);

        public Task<MemberEntity?> GetMemberByNameAsync(string name);

        public Task<MemberEntity?> GetMemberByMemberIdAsync(Guid memberId);

        public Task<MemberEntity?> GetMemberByEmailAsync(string email);

        public Task<IEnumerable<MemberEntity>?> GetMemberBySizeAsync(int page,int size);
    }
}
