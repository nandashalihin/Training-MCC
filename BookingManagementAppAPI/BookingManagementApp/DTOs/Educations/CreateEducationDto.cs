using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs
{
    public class CreateEducationDto
    {
        public Guid Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }

        //DTO Untuk Create Education
        public static implicit operator Education(CreateEducationDto createEducationDto)
        {
            return new Education
            {
                Guid = createEducationDto.Guid,
                Major = createEducationDto.Major,
                Degree = createEducationDto.Degree,
                Gpa = createEducationDto.Gpa,
                UniversityGuid = createEducationDto.UniversityGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
