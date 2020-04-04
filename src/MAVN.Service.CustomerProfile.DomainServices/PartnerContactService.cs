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

        public async Task<PartnerContactErrorCodes> CreateIfNotExistsAsync(PartnerContactModel partnerContact)
        {
            var creationResult = await _partnerContactRepository.CreateIfNotExistAsync(partnerContact);

            if (creationResult == PartnerContactErrorCodes.PartnerContactAlreadyExists)
            { 
                _log.Warning("Partner Contact already exists", context: partnerContact.LocationId);
                return creationResult;
            }

            _log.Info("Partner Contact is created", context: partnerContact.LocationId);

            return creationResult;
        }

        public async Task<PartnerContactErrorCodes> UpdateAsync(string locationId, string firstName, string lastName, string phoneNumber,
            string email)
        {
            var result = await _partnerContactRepository.UpdateAsync(locationId, firstName, lastName, phoneNumber, email);

            if (result == PartnerContactErrorCodes.PartnerContactDoesNotExist)
            {
                _log.Warning("Partner Contact was not updated", context: locationId);
            }

            return result;
        }

        public async Task RemoveAsync(string locationId)
        {
            var deleted = await _partnerContactRepository.DeleteAsync(locationId);

            if (deleted)
            {
                _log.Info("Partner Contact was removed", context: locationId);
            }
            else
            {
                _log.Warning("Partner Contact was not removed", context: locationId);
            }
        }
    }
}
