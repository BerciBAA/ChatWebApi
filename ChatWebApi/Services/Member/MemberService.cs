using AutoMapper;
using ChatWebApi.Models.Member;
using ChatWebApi.Repositories.Member;

namespace ChatWebApi.Services.Member
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<MemberDto?> RegisterMemberAsync(MemberDto memberDto)
        {
            var memberEntity = _mapper.Map<MemberDto, MemberEntity>(memberDto);
            
            var result = await _memberRepository.RegisterMemberAsync(memberEntity);

            if (result is null)
                return null;
            
            return _mapper.Map<MemberEntity, MemberDto>(result);
        }

        public async Task<MemberDto?> LoginMemberAsync(MemberDto memberDto)
        {
            var memberEntity = _mapper.Map<MemberDto, MemberEntity>(memberDto);

            var resultMemberEntity = await _memberRepository.GetMemberByNameAsync(memberEntity.Name);

            if (resultMemberEntity is null)
                return null;

            return _mapper.Map<MemberEntity, MemberDto>(resultMemberEntity);
        }

        public async Task<MemberDto?> GetMemberByMemberIdAsync(Guid memberId)
        {
            var resultMemberEntity = await _memberRepository.GetMemberByMemberIdAsync(memberId);

            if (resultMemberEntity is null)
                return null;

            return _mapper.Map<MemberEntity, MemberDto>(resultMemberEntity);
        }

        public async Task<MemberDto?> GetMemberByEmailAsync(string email)
        {
            var resultMemberEntity = await _memberRepository.GetMemberByEmailAsync(email);

            if (resultMemberEntity is null)
                return null;

            return _mapper.Map<MemberEntity, MemberDto>(resultMemberEntity);
        }

        public async Task<MemberDto?> GetMemberByNameAsync(string name)
        {
            var resultMemberEntity = await _memberRepository.GetMemberByNameAsync(name);

            if (resultMemberEntity is null)
                return null;

            return _mapper.Map<MemberEntity, MemberDto>(resultMemberEntity);
        }

        public async Task<IEnumerable<MemberDto>?> GetMemberBySizeAsync(int page, int size)
        {
            var membersDto = await _memberRepository.GetMemberBySizeAsync(page, size);

            if (membersDto is null)
                return null;

            return _mapper.Map<IEnumerable<MemberEntity>, IEnumerable<MemberDto>>(membersDto);
        }
    }
}
