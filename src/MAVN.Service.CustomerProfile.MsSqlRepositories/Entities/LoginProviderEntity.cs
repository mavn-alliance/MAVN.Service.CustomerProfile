using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.CustomerProfile.Domain.Enums;
using MAVN.Service.CustomerProfile.Domain.Models;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Entities
{
    [Table("login_providers")]
    public class LoginProviderEntity : ILoginProvider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("customer_id")]
        [ForeignKey(nameof(CustomerProfile))]
        [Required]
        public string CustomerId { get; set; }

        [Column("login_provider")]
        [Required]
        [DefaultValue(LoginProvider.Standard)]
        public LoginProvider LoginProvider { get; set; }

        public virtual CustomerProfileEntity CustomerProfile { get; set; }
    }
}
