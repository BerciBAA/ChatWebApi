using FluentValidation;
using ChatWebApi.Contracts.Message;
using ChatWebApi.Services.Member;
using ChatWebApi.Services.Message;
using Microsoft.AspNetCore.Mvc;

namespace ChatWebApi.Validators.Message
{
    public class AddMessageRequestValidator : AbstractValidator<AddMessageRequest>
    {
        private readonly IMemberService _memberService;
        public AddMessageRequestValidator(IMemberService memberService)
        {
            _memberService = memberService;

            RuleFor(x => x.ToId)
                .NotEmpty()
                .WithMessage("The {PropertyName} cannot be blank!");

            RuleFor(x => x.ToId)
                .MustAsync(async (x, token) => await _memberService.GetMemberByMemberIdAsync(x) is not null)
                .WithMessage("The person you want to send it to does not exist!");

            RuleFor(x => x.FromId)
                .NotEmpty()
                .WithMessage("The {PropertyName} cannot be blank!");

            RuleFor(x => x.FromId)
                .MustAsync(async (x, token) => await _memberService.GetMemberByMemberIdAsync(x) is not null)
                .WithMessage("There is no user who sends!");

            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("The {PropertyName} cannot be blank!");

            RuleFor(x => x.Message)
                .MaximumLength(500)
                .WithMessage("The {PropertyName} can be up to 500 characters!");
        }
    }
}
