using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_roles")]
    public class Role : BaseEntity
    {
        [Column(name: "role_name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public ICollection<AccountRole>? AccountRoles { get; set; }
    }
}
