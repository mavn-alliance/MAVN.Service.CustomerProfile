using System;
using System.Text.RegularExpressions;
using FluentValidation;
using JetBrains.Annotations;
using MAVN.Service.CustomerProfile.Client.Models.Requests;

namespace MAVN.Service.CustomerProfile.Models.Validation
{
    [UsedImplicitly]
    public class ReferralLeadProfileRequestValidator : AbstractValidator<ReferralLeadProfileRequest>
    {
        public ReferralLeadProfileRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(o => o.ReferralLeadId)
                .Must(o => o != Guid.Empty)
                .WithMessage("Referral lead id required.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name required.")
                .MaximumLength(100)
                .WithMessage("First name shouldn't be longer than 100 characters.")
                .Must(o => Patterns.NameRegex.IsMatch(o))
                .WithMessage("First name contains illegal characters.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name required.")
                .MaximumLength(100)
                .WithMessage("Last name shouldn't be longer than 100 characters.")
                .Must(o => Patterns.NameRegex.IsMatch(o))
                .WithMessage("Last name contains illegal characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number required.")
                .MaximumLength(50)
                .WithMessage("Phone number shouldn't be longer than 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email required.")
                .EmailAddress()
                .WithMessage("Email invalid.");

            RuleFor(o => o.Note)
                .MaximumLength(2000)
                .WithMessage("Note shouldn't be longer than 2000 characters.");
        }
    }
}
