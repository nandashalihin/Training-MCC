namespace BookingManagementApp.DTOs
{
    public class BookingDurationDto
    {
        public Guid RoomGuid { get; set; }
        public string RoomName { get; set; }
        public int BookingLength { get; set; }
    }
}
