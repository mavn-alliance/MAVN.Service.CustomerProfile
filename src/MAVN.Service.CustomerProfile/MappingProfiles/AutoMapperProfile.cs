using System.Collections.Generic;
using AutoMapper;
using MAVN.Service.CustomerProfile.Client.Models.Requests;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Domain.Models.AdminProfile, Client.Models.Responses.AdminProfile>(MemberList.Source);
            CreateMap<Client.Models.Responses.AdminProfile, Domain.Models.AdminProfile>(MemberList.Destination);
            CreateMap<AdminProfileRequest, Domain.Models.AdminProfile>();

            CreateMap<CustomerProfileResult, CustomerProfileResponse>();
            CreateMap<PaginatedCustomerProfilesModel, PaginatedCustomerProfilesResponse>();
            CreateMap<CustomerProfileRequestModel, CustomerProfileModel>()
                .ForMember(c => c.IsEmailVerified, c => c.Ignore())
                .ForMember(c => c.Registered, c => c.Ignore())
                .ForMember(c => c.PhoneNumber, c => c.Ignore())
                .ForMember(c => c.IsPhoneVerified, c => c.Ignore())
                .ForMember(c => c.ShortPhoneNumber, c => c.Ignore())
                .ForMember(c => c.CountryPhoneCodeId, c => c.Ignore())
                .ForMember(c => c.CountryOfResidenceId, c => c.Ignore())
                .ForMember(c => c.TierId, c => c.Ignore())
                .ForMember(c => c.Status, c => c.Ignore())
                .ForMember(c => c.LoginProviders,
                    opt => opt.MapFrom(x => new List<LoginProvider> {(LoginProvider) x.LoginProvider}));
            CreateMap<ICustomerProfile, Client.Models.Responses.CustomerProfile>();

            // Referral Hotel
            CreateMap<Domain.Models.ReferralHotelProfile, Client.Models.Responses.ReferralHotelProfile>(MemberList
                .Source);
            CreateMap<Client.Models.Responses.ReferralHotelProfile, Domain.Models.ReferralHotelProfile>(MemberList
                .Destination);
            CreateMap<ReferralHotelProfileRequest, Domain.Models.ReferralHotelProfile>();

            // Referral Lead
            CreateMap<Domain.Models.ReferralLeadProfile, Client.Models.Responses.ReferralLeadProfile>(MemberList
                .Source);
            CreateMap<Client.Models.Responses.ReferralLeadProfile, Domain.Models.ReferralLeadProfile>(MemberList
                .Destination);
            CreateMap<ReferralLeadProfileRequest, Domain.Models.ReferralLeadProfile>();

            // Referral Friend
            CreateMap<Domain.Models.ReferralFriendProfile, Client.Models.Responses.ReferralFriendProfile>(MemberList
                .Source);
            CreateMap<Client.Models.Responses.ReferralFriendProfile, Domain.Models.ReferralFriendProfile>(MemberList
                .Destination);
            CreateMap<ReferralFriendProfileRequest, Domain.Models.ReferralFriendProfile>();

            // Partners
            CreateMap<IPartnerContact, PartnerContact>();
            CreateMap<PartnerContactResult, PartnerContactResponse>();
            CreateMap<PaginatedPartnerContactsModel, PaginatedPartnerContactsResponse>();

            CreateMap<PartnerContactRequestModel, PartnerContactModel>();
        }
    }
}
