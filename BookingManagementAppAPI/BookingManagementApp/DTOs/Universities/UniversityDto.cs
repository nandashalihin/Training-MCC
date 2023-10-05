using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs
{
    public class UniversityDto
    {
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        //DTO untuk get University
        public static explicit operator UniversityDto(University university)
        {
            return new UniversityDto
            {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name
            };
        }

        //DTO untuk update University
        public static implicit operator University(UniversityDto universityDto)
        {
            return new University
            {
                Guid = universityDto.Guid,
                Code = universityDto.Code,
                Name = universityDto.Name,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
