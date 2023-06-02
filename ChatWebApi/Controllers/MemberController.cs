using AutoMapper;
using FluentValidation;
using ChatWebApi.Contracts.Member;
using ChatWebApi.Models.Member;
using ChatWebApi.Services.Member;
using ChatWebApi
    .Services.TokenGenerator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IValidator<RegisterMemberRequest> _validatorRegisterMemberRequest;

        private readonly IValidator<LoginMemberRequest> _validatorLoginMemberRequest;

        private readonly IValidator<GetMemberBySizeRequest> _validatorGetMemberBySizeRequest;

        private readonly IMapper _mapper;

        private readonly IMemberService _memberService;

        private readonly IChatWebApiService _ChatWebApiService;

        public MemberController(IMapper mapper,
                                IMemberService memberService,
                                IChatWebApiService ChatWebApiService,
                                IValidator<RegisterMemberRequest> validatorRegisterMemberRequest,
                                IValidator<LoginMemberRequest> validatorLoginMemberRequest,
                                IValidator<GetMemberBySizeRequest> validatorGetMemberBySizeRequest)
        {
            _validatorRegisterMemberRequest = validatorRegisterMemberRequest;
            _validatorLoginMemberRequest = validatorLoginMemberRequest;
            _mapper = mapper;
            _memberService = memberService;
            _ChatWebApiService = ChatWebApiService;
            _validatorGetMemberBySizeRequest = validatorGetMemberBySizeRequest;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterMember([FromBody] RegisterMemberRequest request)
        {
            var validationResult = await _validatorRegisterMemberRequest.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var memberDto = _mapper.Map<RegisterMemberRequest, MemberDto>(request);

            var resultMemberDto = await _memberService.RegisterMemberAsync(memberDto);

            if (resultMemberDto is null)
                return BadRequest();

            var result = _mapper.Map<MemberDto, RegisterMemberResponse>(resultMemberDto);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginMember([FromBody] LoginMemberRequest request)
        {
            var validationResult = await _validatorLoginMemberRequest.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var memberDto = _mapper.Map<LoginMemberRequest, MemberDto>(request);

            var resultMemberDto = await _memberService.LoginMemberAsync(memberDto);

            if (resultMemberDto is null)
                return BadRequest("No such user!");

            var JWT = _ChatWebApiService.GenerateToken(resultMemberDto);

            var result = _mapper.Map<MemberDto, LoginMemberResponse>(resultMemberDto);

            if (JWT is null)
                return BadRequest("No such user!");

            result.Jwt = JWT;

            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpPost("GetMemberBySizeAsync")]
        public async Task<IActionResult> GetMemberBySizeAsync([FromBody] GetMemberBySizeRequest request)
        {
            var validationResult = _validatorGetMemberBySizeRequest.Validate(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var membersDto = await _memberService.GetMemberBySizeAsync(request.Page, request.Size);

            if (membersDto is null)
                return BadRequest("There are no users!");

            var result = _mapper.Map<IEnumerable<MemberDto>, IEnumerable<GetMemberBySizeResponse>>(membersDto);

            return Ok(result);
        }
    }
}
