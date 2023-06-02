using FluentValidation;
using ChatWebApi.Contracts.Message;
using ChatWebApi.Services.Member;

namespace ChatWebApi.Validators.Message
{
    public class GetMessageBySizeRequestValidator : AbstractValidator<GetMessageBySizeRequest>
    {
        private readonly IMemberService _memberService;
        public GetMessageBySizeRequestValidator(IMemberService memberService)
        {
            _memberService = memberService;

            RuleFor(x => x.ToId)
                .MustAsync(async (x, token) => await _memberService.GetMemberByMemberIdAsync(x) is not null)
                .WithMessage("The person you want to send it to does not exist!");

            RuleFor(x => x.FromId)
               .MustAsync(async (x, token) => await _memberService.GetMemberByMemberIdAsync(x) is not null)
               .WithMessage("There is no user who sends!");

            RuleFor(x => x.Page)
              .GreaterThanOrEqualTo(0)
              .WithMessage("0 or less");

            RuleFor(x => x.Page)
                .NotNull()
                .WithMessage("The {PropertyName} cannot be blank!");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("0 or less");

            RuleFor(x => x.Size)
                .NotNull()
                .WithMessage("The {PropertyName} cannot be blank!");

        }
    }
}
