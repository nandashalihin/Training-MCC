using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs
{
    public class RoleDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        // DTO untuk Get Role
        public static explicit operator RoleDto(Role role)
        {
            return new RoleDto
            {
                Guid = role.Guid,
                Name = role.Name,
            };
        }

        // DTO untuk Update Role
        public static implicit operator Role(RoleDto roleDto)
        {
            return new Role
            {
                Guid = roleDto.Guid,
                Name = roleDto.Name,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
