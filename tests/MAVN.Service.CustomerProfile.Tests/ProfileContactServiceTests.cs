using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Logs;
using Lykke.Logs.Loggers.LykkeConsole;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;
using MAVN.Service.CustomerProfile.Domain.Repositories;
using MAVN.Service.CustomerProfile.DomainServices;
using Moq;
using Xunit;

namespace MAVN.Service.CustomerProfile.Tests
{
    public class ProfileContactServiceTests
    {
        [Fact]
        public async Task TryingToCreateProfileContact_EverythingValid_SuccessfullyCreated()
        {
            var partnerContactRepository = new Mock<IPartnerContactRepository>();
            var partnerContactResult = PartnerContactErrorCodes.None;
            partnerContactRepository
                .Setup(x => x.CreateIfNotExistAsync(It.IsAny<PartnerContactModel>()))
                .ReturnsAsync(partnerContactResult)
                .Verifiable();

            PartnerContactService partnerContactService;

            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                partnerContactService = new PartnerContactService(
                    partnerContactRepository.Object,
                    logFactory);
            }

            var actual = await partnerContactService.CreateIfNotExistsAsync(new PartnerContactModel
            {

            });

            Assert.Equal(partnerContactResult, actual);
        }

        [Fact]
        public async Task TryingToCreateProfileContact_ProfileContactAlreadyExists_ErrorCodeIsReturned()
        {
            var partnerContactRepository = new Mock<IPartnerContactRepository>();
            var partnerContactResult = PartnerContactErrorCodes.PartnerContactAlreadyExists;

            partnerContactRepository
                .Setup(x => x.CreateIfNotExistAsync(It.IsAny<PartnerContactModel>()))
                .ReturnsAsync(partnerContactResult);

            PartnerContactService partnerContactService;

            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                partnerContactService = new PartnerContactService(
                    partnerContactRepository.Object,
                    logFactory);
            }

            var actual = await partnerContactService.CreateIfNotExistsAsync(new PartnerContactModel
            {
            });

            Assert.Equal(partnerContactResult, actual);
        }

        [Fact]
        public async Task TryingToGetProfileContactByLocationId_ValidLocationId_SuccessfullyReturned()
        {
            var partnerContactRepository = new Mock<IPartnerContactRepository>();
            var contactModel = new PartnerContactModel();
            partnerContactRepository
                .Setup(x => x.GetByLocationIdAsync(It.IsAny<string>()))
                .ReturnsAsync(contactModel)
                .Verifiable();

            PartnerContactService partnerContactService;

            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                partnerContactService = new PartnerContactService(
                    partnerContactRepository.Object,
                    logFactory);
            }

            var actual = await partnerContactService.GetByLocationIdAsync("testContactId");

            partnerContactRepository.Verify();
            Assert.Equal(PartnerContactErrorCodes.None, actual.ErrorCode);
        }

        [Fact]
        public async Task TryingToGetProfileContactByLocationId_InvalidLocationId_ErrorCodeIsReturned()
        {
            var partnerContactRepository = new Mock<IPartnerContactRepository>();
            partnerContactRepository
                .Setup(x => x.GetByLocationIdAsync(It.IsAny<string>()))
                .ReturnsAsync((IPartnerContact) null);

            PartnerContactService partnerContactService;

            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                partnerContactService = new PartnerContactService(
                    partnerContactRepository.Object,
                    logFactory);
            }

            var actual = await partnerContactService.GetByLocationIdAsync("testContactId");

            Assert.Equal(PartnerContactErrorCodes.PartnerContactDoesNotExist, actual.ErrorCode);
            Assert.Null(actual.PartnerContact);
        }

        [Fact]
        public async Task TryingToDeleteProfileContactByLocationId_ValidLocationId_SuccessfullyDeleted()
        {
            var partnerContactRepository = new Mock<IPartnerContactRepository>();
            partnerContactRepository
                .Setup(x => x.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(true)
                .Verifiable();

            PartnerContactService partnerContactService;

            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                partnerContactService = new PartnerContactService(
                    partnerContactRepository.Object,
                    logFactory);
            }

            await partnerContactService.RemoveAsync("testContactId");

            partnerContactRepository.Verify();
        }

        [Fact]
        public async Task TryingToGetPaginatedProfileContacts_EverythingIsValid_SuccessfullyReturned()
        {
            var partnerContactRepository = new Mock<IPartnerContactRepository>();
            var partnerContactPaginatedResult = new List<IPartnerContact> {new PartnerContactModel(),};
            partnerContactRepository
                .Setup(x => x.GetPaginatedAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(partnerContactPaginatedResult)
                .Verifiable();

            PartnerContactService partnerContactService;

            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                partnerContactService = new PartnerContactService(
                    partnerContactRepository.Object,
                    logFactory);
            }

            var actual = await partnerContactService.GetPaginatedAsync(1, 5);

            partnerContactRepository.Verify();
            Assert.NotNull(actual.PartnerContacts);
            Assert.True(actual.PartnerContacts.Any());
        }

        [Fact]
        public async Task TryingToGetPaginatedProfileContact_CurrentPageIsInvalid_ArgumentExceptionIsThrown()
        {
            var partnerContactRepository = new Mock<IPartnerContactRepository>();

            PartnerContactService partnerContactService;

            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                partnerContactService = new PartnerContactService(
                    partnerContactRepository.Object,
                    logFactory);
            }

            await Assert.ThrowsAsync<ArgumentException>(() => partnerContactService.GetPaginatedAsync(-1, 10));
        }

        [Fact]
        public async Task TryingToGetPaginatedProfileContacts_PageSizeIsInvalid_ArgumentExceptionIsThrown()
        {
            var partnerContactRepository = new Mock<IPartnerContactRepository>();

            PartnerContactService partnerContactService;

            using (var logFactory = LogFactory.Create().AddUnbufferedConsole())
            {
                partnerContactService = new PartnerContactService(
                    partnerContactRepository.Object,
                    logFactory);
            }

            await Assert.ThrowsAsync<ArgumentException>(() => partnerContactService.GetPaginatedAsync(1, 0));
        }
    }
}
