namespace BookingManagementApp.DTOs
{
    //DTO Untuk Mapping data Available Room
    public class AvailableRoomDto
    {
        public Guid RoomGuid { get; set; }
        public string RoomName { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
    }

}
