using FluentValidation;
using ChatWebApi.Contracts.Member;
using ChatWebApi.Services.Member;

namespace ChatWebApi.Validators.Member
{
    public class LoginMemberRequestValidator : AbstractValidator<LoginMemberRequest>
    {
        private readonly IMemberService _memberService;

        public LoginMemberRequestValidator(IMemberService memberService)
        {
            _memberService = memberService;

            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("The {PropertyName} cannot be blank!");

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("The {PropertyName} cannot be blank!");

            RuleFor(x => new {x.Name, x.Password})
              .MustAsync(async (x, token) => {

                  var memberDto = await _memberService.GetMemberByNameAsync(x.Name);

                  if (memberDto is null)
                      return false;

                  return BCrypt.Net.BCrypt.Verify(x.Password, memberDto.Password);
              })
              .WithMessage("Incorrect password or no such user!");
        }
    }
}
