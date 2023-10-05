using BookingManagementApp.DTOs;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.Employees
{
    public class UpdateEmployeeValidator : AbstractValidator<EmployeeDto>
    {
        // Konstruktor dari kelas UpdateEmployeeValidator
        public UpdateEmployeeValidator()
        {
            // RuleFor digunakan untuk menentukan aturan validasi untuk setiap properti pada objek EmployeeDto.

            // Aturan validasi untuk properti Guid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Guid)
                .NotEmpty()
                .WithMessage("Guid tidak boleh kosong");

            // Aturan validasi untuk properti FirstName: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.FirstName)
                .NotEmpty();

            // Aturan validasi untuk properti BirthDate:
            // - Tidak boleh kosong (NotEmpty)
            // - Harus lebih besar atau sama dengan tanggal 18 tahun yang lalu
            RuleFor(e => e.BirthDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now.AddYears(-18))
                .WithMessage("Umur harus minimal 18 tahun");

            // Aturan validasi untuk properti Gender:
            // - Tidak boleh null (NotNull)
            // - Harus merupakan nilai enum yang valid (IsInEnum)
            RuleFor(e => e.Gender)
                .NotNull()
                .IsInEnum();

            // Aturan validasi untuk properti HiringDate: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.HiringDate)
                .NotEmpty();

            // Aturan validasi untuk properti Email:
            // - Tidak boleh kosong (NotEmpty)
            // - Harus memiliki format email yang benar (EmailAddress)
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email tidak boleh kosong")
                .EmailAddress().WithMessage("Format Email salah");

            // Aturan validasi untuk properti PhoneNumber:
            // - Tidak boleh kosong (NotEmpty)
            // - Panjangnya harus minimal 10 karakter dan maksimal 20 karakter
            RuleFor(e => e.PhoneNumber)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(20);
        }
    }

}
