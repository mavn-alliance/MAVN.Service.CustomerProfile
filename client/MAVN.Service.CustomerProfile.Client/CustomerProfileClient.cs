using Lykke.HttpClientGenerator;
using MAVN.Service.CustomerProfile.Client.Api;

namespace MAVN.Service.CustomerProfile.Client
{
    /// <inheritdoc/>
    public class CustomerProfileClient : ICustomerProfileClient
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CustomerProfileClient"/> with <param name="httpClientGenerator"></param>.
        /// </summary> 
        public CustomerProfileClient(IHttpClientGenerator httpClientGenerator)
        {
            AdminProfiles = httpClientGenerator.Generate<IAdminProfilesApi>();
            CustomerProfiles = httpClientGenerator.Generate<ICustomerProfileApi>();
            PartnerContact = httpClientGenerator.Generate<IPartnerContactApi>();
            ReferralHotelProfiles = httpClientGenerator.Generate<IReferralHotelProfilesApi>();
            ReferralLeadProfiles = httpClientGenerator.Generate<IReferralLeadProfilesApi>();
            ReferralFriendProfiles = httpClientGenerator.Generate<IReferralFriendProfilesApi>();
            Statistics = httpClientGenerator.Generate<IStatisticsApi>();
            CustomerPhones = httpClientGenerator.Generate<ICustomerPhonesApi>();
            PaymentProviderDetails = httpClientGenerator.Generate<IPaymentProviderDetailsApi>();
        }

        /// <inheritdoc/>
        public IAdminProfilesApi AdminProfiles { get; }

        /// <inheritdoc/>
        public ICustomerProfileApi CustomerProfiles { get; }

        /// <inheritdoc/>
        public IPartnerContactApi PartnerContact { get; }

        /// <inheritdoc/>
        public IReferralHotelProfilesApi ReferralHotelProfiles { get; }

        /// <inheritdoc/>
        public IReferralLeadProfilesApi ReferralLeadProfiles { get; }

        /// <inheritdoc/>
        public IReferralFriendProfilesApi ReferralFriendProfiles { get; }

        /// <inheritdoc/>
        public IStatisticsApi Statistics { get; }

        /// <inheritdoc/>
        public ICustomerPhonesApi CustomerPhones { get; }

        /// <inheritdoc/>
        public IPaymentProviderDetailsApi PaymentProviderDetails { get; }
    }
}
