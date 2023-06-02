using ChatWebApi.Context;
using ChatWebApi.Models.Member;
using Microsoft.EntityFrameworkCore;

namespace ChatWebApi.Repositories.Member
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ChatContext _chatContext;

        public MemberRepository(ChatContext memberContext)
        {
            _chatContext = memberContext;
        }

        public async Task<MemberEntity?> GetMemberByMemberIdAsync(Guid memberId)
        {
            return await _chatContext.Members.FindAsync(memberId);
        }

        public async Task<MemberEntity?> GetMemberByNameAsync(string name)
        {
            return await _chatContext.Members.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<MemberEntity?> GetMemberByEmailAsync(string email)
        {
            return await _chatContext.Members.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<MemberEntity?> RegisterMemberAsync(MemberEntity memberEntity)
        {
            _chatContext.Add(memberEntity);

            await _chatContext.SaveChangesAsync();

            return await _chatContext.Members.FirstOrDefaultAsync(x => x.MemberId == memberEntity.MemberId);
        }

        public async Task<IEnumerable<MemberEntity>?> GetMemberBySizeAsync(int page, int size)
        {
            return await _chatContext.Members
                    .OrderBy(x => x.Name)
                    .Skip(page * size)
                    .Take(size)
                    .ToListAsync();
        }
    }
}
