using BookingManagementApp.DTOs;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.Employees
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeValidator()
        {
            // Aturan validasi untuk properti FirstName: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.FirstName)
                .NotEmpty();

            // Aturan validasi untuk properti BirthDate:
            // - Tidak boleh kosong (NotEmpty)
            // - Harus lebih besar atau sama dengan tanggal 18 tahun yang lalu
            RuleFor(e => e.BirthDate)
                .Must(birthDate => DateTime.Today.Year - birthDate.Year > 18)
    .WithMessage("Umur harus lebih dari 18 tahun") ;
                

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
