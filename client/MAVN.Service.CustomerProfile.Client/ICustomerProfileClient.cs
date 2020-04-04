using JetBrains.Annotations;
using MAVN.Service.CustomerProfile.Client.Api;

namespace MAVN.Service.CustomerProfile.Client
{
    /// <summary>
    /// CustomerProfile client interface.
    /// </summary>
    [PublicAPI]
    public interface ICustomerProfileClient
    {
        /// <summary>
        /// Admin profiles API.
        /// </summary>
        IAdminProfilesApi AdminProfiles { get; }

        /// <summary>
        /// Customers API.
        /// </summary>
        ICustomerProfileApi CustomerProfiles { get; }

        /// <summary>
        /// Partner Contacts API.
        /// </summary>
        IPartnerContactApi PartnerContact { get; }

        /// <summary>
        /// Referral hotels API.
        /// </summary>
        IReferralHotelProfilesApi ReferralHotelProfiles { get; }

        /// <summary>
        /// Referral leads API.
        /// </summary>
        IReferralLeadProfilesApi ReferralLeadProfiles { get; }

        /// <summary>
        /// Referral friends API.
        /// </summary>
        IReferralFriendProfilesApi ReferralFriendProfiles { get; }

        /// <summary>
        /// Statistics API.
        /// </summary>
        IStatisticsApi Statistics { get; }

        /// <summary>
        /// CustomerPhones API.
        /// </summary>
        ICustomerPhonesApi CustomerPhones { get; }
    }
}
