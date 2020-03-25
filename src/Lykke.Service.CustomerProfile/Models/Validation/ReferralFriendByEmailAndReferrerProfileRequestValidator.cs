using System;
using System.Text.RegularExpressions;
using FluentValidation;
using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models.Requests;

namespace Lykke.Service.CustomerProfile.Models.Validation
{
    [UsedImplicitly]
    public class ReferralFriendByEmailAndReferrerProfileRequestValidator : AbstractValidator<ReferralFriendByEmailAndReferrerProfileRequest>
    {
        public ReferralFriendByEmailAndReferrerProfileRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(o => o.ReferrerId)
                .Must(o => o != Guid.Empty)
                .WithMessage("Referrer id required.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email required.")
                .EmailAddress()
                .WithMessage("Email invalid.");
        }
    }
}
