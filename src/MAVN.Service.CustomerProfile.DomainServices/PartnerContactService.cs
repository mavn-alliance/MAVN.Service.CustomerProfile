using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.Domain.Services;

namespace MAVN.Service.CustomerProfile.DomainServices
{
    public class PartnerContactService : IPartnerContactService
    {
        private readonly IPartnerContactRepository _partnerContactRepository;
        private readonly ILog _log;

        public PartnerContactService(
            IPartnerContactRepository partnerContactRepository,
            ILogFactory logFactory)
        {
            _partnerContactRepository = partnerContactRepository;
            _log = logFactory.CreateLog(this);
        }
        public async Task<PartnerContactResult> GetByLocationIdAsync(string locationId)
        {
            var partnerContact = await _partnerContactRepository.GetByLocationIdAsync(locationId);

            if (partnerContact == null)
            {
                _log.Warning("Partner Contact not found", context: locationId);

                return new PartnerContactResult { ErrorCode = PartnerContactErrorCodes.PartnerContactDoesNotExist };
            }

            return new PartnerContactResult { PartnerContact = partnerContact };
        }

        public async Task<PaginatedPartnerContactsModel> GetPaginatedAsync(int currentPage, int pageSize)
        {
            if (currentPage < 1)
            {
                throw new ArgumentException("Current page can't be negative", nameof(currentPage));
            }

            if (pageSize == 0)
            {
                throw new ArgumentException("Page size can't be 0", nameof(pageSize));
            }

            var skip = (currentPage - 1) * pageSize;
            var take = pageSize;

            var partnerContacts = await _partnerContactRepository.GetPaginatedAsync(skip, take);

            var totalCount = await _partnerContactRepository.GetTotalAsync();

            return new PaginatedPartnerContactsModel
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                PartnerContacts = partnerContacts,
                TotalCount = totalCount
            };
        }

        public async Task CreateOrUpdateAsync(PartnerContactModel partnerContact)
        {
            await _partnerContactRepository.CreateOrUpdateAsync(partnerContact);

            _log.Info("Partner Contact is created or updated", context: partnerContact.LocationId);
        }

        public Task RemoveIfExistsAsync(string locationId)
        {
            return _partnerContactRepository.DeleteIfExistsAsync(locationId);
        }
    }
}
