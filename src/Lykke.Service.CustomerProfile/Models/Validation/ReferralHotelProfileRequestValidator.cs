using System;
using FluentValidation;
using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models.Requests;

namespace Lykke.Service.CustomerProfile.Models.Validation
{
    [UsedImplicitly]
    public class ReferralHotelProfileRequestValidator : AbstractValidator<ReferralHotelProfileRequest>
    {
        public ReferralHotelProfileRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(o => o.ReferralHotelId)
                .Must(o => o != Guid.Empty)
                .WithMessage("Referral hotel id required.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email required.")
                .EmailAddress()
                .WithMessage("Email invalid.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name required.");
        }
    }
}
