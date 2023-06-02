using FluentValidation;
using ChatWebApi.Contracts.Member;
using ChatWebApi.Services.Member;

namespace ChatWebApi.Validators.Member
{
    public class RegisterMemberRequestValidator : AbstractValidator<RegisterMemberRequest>
    {
        private readonly IMemberService _memberService;
        public RegisterMemberRequestValidator(IMemberService memberService)
        {
            _memberService = memberService;

            RuleFor(x => x.Email)
                .MustAsync(async (x, token) => await _memberService.GetMemberByEmailAsync(x) is null)
                .WithMessage("{PropertyName} already exists!");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("The {PropertyName} cannot be blank!");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid {PropertyName}!");

            RuleFor(x => x.Name)
               .MustAsync(async (x, token) => await _memberService.GetMemberByNameAsync(x) is null)
               .WithMessage("{PropertyName} already exists!");

            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("The {PropertyName} cannot be blank!");

            RuleFor(x => x.Name)
               .MaximumLength(20)
               .WithMessage("The {PropertyName} can be up to 20 characters!");

            RuleFor(x => x.Name)
               .Matches(@"^[a-zA-Z0-9_]+$")
               .WithMessage("The name does not contain special characters!");

            RuleFor(x => x.Password)
               .MinimumLength(8)
               .WithMessage("The {PropertyName} must consist of at least 8 characters!");

            RuleFor(x => x.Password)
               .NotEmpty()
               .WithMessage("The {PropertyName} cannot be blank!");

            RuleFor(x => x.DateOfBirth)
                .Must(x => x.Year <= DateTime.UtcNow.Year - 13)
                .WithMessage("Member must be older than 13");

            RuleFor(x => x.Password)
                .Equal(x => x.PasswordAgain)
                .WithMessage("The two passwords do not match!");
        }

    }
}
