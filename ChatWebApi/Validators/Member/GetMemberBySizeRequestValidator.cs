using FluentValidation;
using ChatWebApi.Contracts.Member;

namespace ChatWebApi.Validators.Member
{
    public class GetMemberBySizeRequestValidator : AbstractValidator<GetMemberBySizeRequest>
    {
        public GetMemberBySizeRequestValidator()
        {
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
