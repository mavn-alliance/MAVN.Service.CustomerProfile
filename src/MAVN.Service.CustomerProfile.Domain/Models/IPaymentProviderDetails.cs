using System;

namespace MAVN.Service.CustomerProfile.Domain.Models
{
    public interface IPaymentProviderDetails
    {
        Guid Id { get; set; }

        Guid PartnerId { get; set; }

        string PaymentIntegrationProvider { get; set; }

        string PaymentIntegrationProperties { get; set; }
    }
}
