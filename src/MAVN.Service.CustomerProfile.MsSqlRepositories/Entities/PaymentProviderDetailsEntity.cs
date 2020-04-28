using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Common.Encryption;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("payment_provider_details")]
    public class PaymentProviderDetailsEntity : IPaymentProviderDetails
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("partner_id")]
        [Required]
        public Guid PartnerId { get; set; }

        [Column("payment_integration_provider")]
        [Required]
        public string PaymentIntegrationProvider { get; set; }

        [Column("payment_integration_properties")]
        [Required]
        [EncryptedProperty]
        public string PaymentIntegrationProperties { get; set; }

        public static PaymentProviderDetailsEntity Create(IPaymentProviderDetails model)
        {
            return new PaymentProviderDetailsEntity
            {
                PartnerId = model.PartnerId,
                Id = model.Id,
                PaymentIntegrationProperties = model.PaymentIntegrationProperties,
                PaymentIntegrationProvider = model.PaymentIntegrationProvider,
            };
        }
    }
}
