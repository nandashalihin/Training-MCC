using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_rooms")]
    public class Room : BaseEntity
    {
        [Column(name: "room_name", TypeName ="nvarchar100")]
        public string Name { get; set; }

        [Column(name: "room_floor")]
        public int Floor { get; set; }

        [Column(name: "room_capacity")]
        public int Capacity { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
