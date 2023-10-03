using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs
{
    public class CreateUniversityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }

        //DTO Untuk Create University
        public static implicit operator University(CreateUniversityDto createUniversityDto)
        {
            return new University
            {
                Code = createUniversityDto.Code,
                Name = createUniversityDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
