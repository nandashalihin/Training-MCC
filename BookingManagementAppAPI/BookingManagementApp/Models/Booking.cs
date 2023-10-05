using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_bookings")]
    public class Booking : BaseEntity
    {
        [Column(name: "start_date")]
        public DateTime StartDate { get; set; }

        [Column(name: "end_date")]
        public DateTime EndDate { get; set; }

        [Column(name: "status")]
        public StatusLevel Status { get; set; }

        [Column(name: "remarks",TypeName ="nvarchar")]
        public string Remarks { get; set; }

        [Column(name: "room_guid")]
        public Guid RoomGuid { get; set; }

        [Column(name: "employee_guid")]
        public Guid EmployeeGuid { get; set; }

        public Room? Room { get; set; }
        public Employee? Employee { get; set; }
    }
}
