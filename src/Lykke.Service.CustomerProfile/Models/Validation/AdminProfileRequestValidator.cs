using System;
using System.Text.RegularExpressions;
using FluentValidation;
using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models.Requests;

namespace Lykke.Service.CustomerProfile.Models.Validation
{
    [UsedImplicitly]
    public class AdminProfileRequestValidator : AbstractValidator<AdminProfileRequest>
    {
        public AdminProfileRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(o => o.AdminId)
                .Must(o => o != Guid.Empty)
                .WithMessage("Admin id required.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name required.")
                .MaximumLength(255)
                .WithMessage("First name shouldn't be longer than 255 characters.")
                .Must(o => Patterns.NameRegex.IsMatch(o))
                .WithMessage("First name contains illegal characters.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name required.")
                .MaximumLength(255)
                .WithMessage("Last name shouldn't be longer than 255 characters.")
                .Must(o => Patterns.NameRegex.IsMatch(o))
                .WithMessage("Last name contains illegal characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email required.")
                .EmailAddress()
                .WithMessage("Email invalid.");

            RuleFor(x => x.Company)
                .NotEmpty()
                .WithMessage("Company required");

            RuleFor(x => x.Department)
                .NotEmpty()
                .WithMessage("Department required");

            RuleFor(x => x.JobTitle)
                .NotEmpty()
                .WithMessage("JobTitle required");
        }
    }
}
