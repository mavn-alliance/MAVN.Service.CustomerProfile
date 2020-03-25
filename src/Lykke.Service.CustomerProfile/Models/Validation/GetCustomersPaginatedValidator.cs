using FluentValidation;
using JetBrains.Annotations;
using Lykke.Service.CustomerProfile.Client.Models;

namespace Lykke.Service.CustomerProfile.Models.Validation
{
    [UsedImplicitly]
    public class GetCustomersPaginatedValidator : AbstractValidator<PaginationModel>
    {
        public GetCustomersPaginatedValidator()
        {
            RuleFor(x => x.CurrentPage)
                .LessThanOrEqualTo(int.MaxValue)
                .GreaterThanOrEqualTo(1)
                .WithMessage($"Current page can't be less than 1 or greater than {int.MaxValue}")
                .Must((page, list, context) =>
                {
                    context.MessageFormatter.AppendArgument("IntMax", int.MaxValue);
                    context.MessageFormatter.AppendArgument("CurrentPage", nameof(page.CurrentPage));
                    context.MessageFormatter.AppendArgument("PageSize", nameof(page.PageSize));
                    return ((long)page.CurrentPage - 1) * page.PageSize <= int.MaxValue;
                })
                .WithMessage("The requested combination of ({CurrentPage} - 1) * {PageSize} can't be more than {IntMax}");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page Size can't be less than 1")
                .LessThanOrEqualTo(1000)
                .WithMessage("Page Size cannot exceed more then 1000");
        }
    }
}
