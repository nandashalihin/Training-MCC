using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs
{
    public class CreateRoleDto
    {
        public string Name { get; set; }

        //DTO Untuk Create Role
        public static implicit operator Role(CreateRoleDto createRoleDto)
        {
            return new Role
            {
                Name = createRoleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
