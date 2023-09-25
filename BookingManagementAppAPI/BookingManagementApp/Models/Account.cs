using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column(name: "password", TypeName ="nvarchar")]
        public string Password { get; set; }

        [Column(name: "is_deleted")]
        public bool IsDeleted { get; set; }

        [Column(name: "otp")]
        public int Otp { get; set; }

        [Column(name: "is_used")]
        public bool IsUsed { get; set; }

        [Column(name: "expired_time")]
        public DateTime ExpiredTime { get; set; }
    }
}
