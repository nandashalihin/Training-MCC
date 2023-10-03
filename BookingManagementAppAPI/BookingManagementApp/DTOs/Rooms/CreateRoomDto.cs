using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs
{
    public class CreateRoomDto
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }

        // DTO untuk Create Room
        public static implicit operator Room(CreateRoomDto createRoomDto)
        {
            return new Room
            {
                Name = createRoomDto.Name,
                Floor = createRoomDto.Floor,
                Capacity = createRoomDto.Capacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
