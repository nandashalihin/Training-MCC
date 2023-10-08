using BookingManagementApp.Utilities.Handlers.Enums;

namespace BookingManagementApp.DTOs
{
    public class RegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
