using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_universities")]
    public class University : BaseEntity
    {
        [Column(name: "code", TypeName = "nvarchar(50)")]
        public string Code { get; set; }
        [Column(name: "name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        public ICollection<Education>? Educations { get; set; }
    }
}

