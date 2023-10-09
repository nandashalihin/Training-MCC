namespace BookingManagementApp.DTOs
{
    public class BookingDurationDto
    {
        //DTO Untuk Mapping data durasi Booking
        public Guid RoomGuid { get; set; }
        public string RoomName { get; set; }
        public int BookingLength { get; set; }
    }
}
