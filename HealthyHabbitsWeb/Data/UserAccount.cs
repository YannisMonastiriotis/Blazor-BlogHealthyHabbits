using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthyHabbitsWeb.Data
{
    //[Table("user_account")]
    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_name")]
        [MaxLength(200)]
        public string? UserName  { get; set; }

        [Column("passwprd")]
        [MaxLength(100)]
        public string? Password { get; set; }

        [Column("role")]
        [MaxLength(20)]
        public string? Role { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("isemailconfirmed")]
        public bool? IsEmailConfirmed { get; set; }
    }
}
