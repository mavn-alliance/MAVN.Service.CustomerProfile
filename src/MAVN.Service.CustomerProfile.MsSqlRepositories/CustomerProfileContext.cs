using System.Data.Common;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories
{
    public class CustomerProfileContext : MsSqlContext
    {
        private const string Schema = "customer_profile";

        internal DbSet<AdminProfileEntity> AdminProfiles { get; set; }
        
        internal DbSet<AdminProfileArchiveEntity> AdminProfilesArchive { get; set; }

        internal DbSet<CustomerProfileEntity> CustomerProfiles { get; set; }

        internal DbSet<CustomerProfileArchiveEntity> CustomerProfilesArchive { get; set; }

        internal DbSet<PartnerContactEntity> PartnerContacts { get; set; }

        internal DbSet<PartnerContactArchiveEntity> PartnerContactsArchive { get; set; }

        internal DbSet<ReferralHotelProfileEntity> ReferralHotelProfiles { get; set; }

        internal DbSet<ReferralHotelProfileArchiveEntity> ReferralHotelProfilesArchive { get; set; }

        internal DbSet<ReferralLeadProfileEntity> ReferralLeadProfiles { get; set; }

        internal DbSet<ReferralLeadProfileArchiveEntity> ReferralLeadProfilesArchive { get; set; }

        internal DbSet<ReferralFriendProfileEntity> ReferralFriendProfiles { get; set; }

        internal DbSet<ReferralFriendProfileArchiveEntity> ReferralFriendProfilesArchive { get; set; }

        [UsedImplicitly]
        public CustomerProfileContext()
            : base(Schema)
        {
        }

        public CustomerProfileContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        public CustomerProfileContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        public CustomerProfileContext(DbContextOptions contextOptions) 
            : base(Schema, contextOptions)
        {
        }

        protected override void OnLykkeModelCreating(ModelBuilder modelBuilder)
        {
            // configuring admin_profiles table
            modelBuilder.Entity<AdminProfileEntity>()
                .HasIndex(c => c.Email);

            // configuring customer_profile table
            var builder = modelBuilder.Entity<CustomerProfileEntity>();
            builder.HasIndex(c => c.Email).IsUnique();
            builder.HasIndex(c => c.LowerCasedEmail).IsUnique();
            builder.Property(c => c.Status).HasConversion(new EnumToStringConverter<CustomerProfileStatus>());
            builder.HasIndex(c => c.Status).IsUnique(false);

            modelBuilder.Entity<CustomerProfileEntity>()
                .HasQueryFilter(c => c.IsEmailVerified && c.IsPhoneVerified);

            // configuring customer_profile_archive table
            modelBuilder.Entity<CustomerProfileArchiveEntity>()
                .HasIndex(c => c.Email).IsUnique(false);

            modelBuilder.Entity<LoginProviderEntity>()
                .HasOne(l => l.CustomerProfile).WithMany(c => c.LoginProviders);

            // configuring partner_contact table
            modelBuilder.Entity<PartnerContactEntity>()
                .HasIndex(c => c.Email);

            modelBuilder.Entity<PartnerContactEntity>()
                .HasIndex(c => c.PhoneNumber);

            // configuring referral_hotel_profiles table
            modelBuilder.Entity<ReferralHotelProfileEntity>()
                .HasIndex(c => c.Email);

            // configuring referral_friends_profiles table
            modelBuilder.Entity<ReferralFriendProfileEntity>()
                .HasIndex(c => c.Email);

            modelBuilder.Entity<ReferralFriendProfileEntity>()
                .HasIndex(c => c.ReferrerId);

            // configuring referral_lead_profiles table
            modelBuilder.Entity<ReferralLeadProfileEntity>()
                .HasIndex(c => c.Email);

            modelBuilder.Entity<ReferralLeadProfileEntity>()
                .HasIndex(c => c.PhoneNumber);
        }
    }
}
