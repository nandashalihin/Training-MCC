using BookingManagementApp.DTOs;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.Educations
{
    public class UpdateEducationValidator : AbstractValidator<EducationDto>
    {
        // Konstruktor dari kelas UpdateEducationValidator
        public UpdateEducationValidator()
        {
            // RuleFor digunakan untuk menentukan aturan validasi untuk setiap properti pada objek EducationDto.

            // Aturan validasi untuk properti Guid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Guid)
                .NotEmpty()
                .WithMessage("Guid tidak boleh kosong");

            // Aturan validasi untuk properti Gpa:
            // - Tidak boleh kosong (NotEmpty)
            // - Harus berada dalam rentang 0 hingga 4 (InclusiveBetween)
            RuleFor(e => e.Gpa)
                .NotEmpty()
                .WithMessage("GPA tidak boleh kosong")
                .InclusiveBetween(0, 4)
                .WithMessage("GPA harus berada dalam rentang 0 hingga 4");

            // Aturan validasi untuk properti UniversityGuid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.UniversityGuid)
                .NotEmpty()
                .WithMessage("UniversityGuid tidak boleh kosong");

            // Aturan validasi untuk properti Major (Fakultas): Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Major)
                .NotEmpty()
                .WithMessage("Fakultas tidak boleh kosong");

            // Aturan validasi untuk properti Degree (Jurusan): Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Degree)
                .NotEmpty()
                .WithMessage("Jurusan tidak boleh kosong");
        }
    }

}
