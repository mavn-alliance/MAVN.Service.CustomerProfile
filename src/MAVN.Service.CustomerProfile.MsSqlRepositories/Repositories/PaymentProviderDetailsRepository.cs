using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Common.Encryption;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Repositories
{
    public class PaymentProviderDetailsRepository : IPaymentProviderDetailsRepository
    {
        private readonly PostgreSQLContextFactory<CustomerProfileContext> _contextFactory;
        private readonly IEncryptionService _encryptionService;

        public PaymentProviderDetailsRepository(PostgreSQLContextFactory<CustomerProfileContext> contextFactory, IEncryptionService encryptionService)
        {
            _contextFactory = contextFactory;
            _encryptionService = encryptionService;
        }

        public async Task CreateAsync(IPaymentProviderDetails model)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = PaymentProviderDetailsEntity.Create(model);
                entity = _encryptionService.Encrypt(entity);

                context.PaymentProviderDetails.Add(entity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateAsync(IPaymentProviderDetails model)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var existingEntity = await context.PaymentProviderDetails.FindAsync(model.Id);

                if (existingEntity == null)
                    return false;

                existingEntity = _encryptionService.Decrypt(existingEntity);

                existingEntity.PaymentIntegrationProperties = model.PaymentIntegrationProperties;
                existingEntity.PaymentIntegrationProvider = model.PaymentIntegrationProvider;

                existingEntity = _encryptionService.Encrypt(existingEntity);

                context.PaymentProviderDetails.Update(existingEntity);

                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = await context.PaymentProviderDetails.FindAsync(id);

                if (entity == null)
                    return false;

                context.PaymentProviderDetails.Remove(entity);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<IReadOnlyList<IPaymentProviderDetails>> GetListByPartnerIdAsync(Guid partnerId)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.PaymentProviderDetails
                    .Where(p => p.PartnerId == partnerId)
                    .Select(p => _encryptionService.Decrypt(p))
                    .ToListAsync();

                return result;
            }
        }

        public async Task<IPaymentProviderDetails> GetByPartnerIdAndProviderAsync(Guid partnerId,
            string paymentProvider)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.PaymentProviderDetails
                    .FirstOrDefaultAsync(p =>
                        p.PartnerId == partnerId && p.PaymentIntegrationProvider == paymentProvider);

                if (result != null)
                    result = _encryptionService.Decrypt(result);

                return result;
            }
        }
    }
}
