using System;
using System.Text.RegularExpressions;
using FluentValidation;
using JetBrains.Annotations;
using MAVN.Service.CustomerProfile.Client.Models.Requests;

namespace MAVN.Service.CustomerProfile.Models.Validation
{
    [UsedImplicitly]
    public class ReferralFriendProfileRequestValidator : AbstractValidator<ReferralFriendProfileRequest>
    {
        public ReferralFriendProfileRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(o => o.ReferralFriendId)
                .Must(o => o != Guid.Empty)
                .WithMessage("Referral friend id required.");

            RuleFor(o => o.ReferrerId)
                .Must(o => o != Guid.Empty)
                .WithMessage("Referrer id required.");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name required.")
                .MinimumLength(3)
                .WithMessage("Full name should be at least 3 characters long.")
                .MaximumLength(200)
                .WithMessage("Full name shouldn't be longer than 200 characters.")
                .Must(o => Patterns.NameRegex.IsMatch(o))
                .WithMessage("Full name contains illegal characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email required.")
                .EmailAddress()
                .WithMessage("Email invalid.");
        }
    }
}
