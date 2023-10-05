using BookingManagementApp.DTOs;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.Account
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountDto>
    {
        // Konstruktor dari kelas CreateAccountValidator
        public CreateAccountValidator()
        {
            // RuleFor digunakan untuk menentukan aturan validasi untuk setiap properti pada objek CreateAccountDto.

            // Aturan validasi untuk properti IsUsed: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.IsUsed)
                .NotEmpty()
                .WithMessage("IsUsed tidak boleh kosong");

            // Aturan validasi untuk properti IsDeleted: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.IsDeleted)
                .NotEmpty()
                .WithMessage("IsDelete tidak boleh kosong");

            // Aturan validasi untuk properti Otp: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Otp)
                .NotEmpty()
                .WithMessage("Otp tidak boleh kosong");

            // Aturan validasi untuk properti ExpiredTime: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.ExpiredTime)
                .NotEmpty()
                .WithMessage("ExpiredTime tidak boleh kosong");

            // Aturan validasi untuk properti Password:
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password harus diisi.") // Tidak boleh kosong
                .MinimumLength(8).WithMessage("Password harus memiliki setidaknya 8 karakter.") // Minimal 8 karakter
                .Matches("[A-Z]").WithMessage("Password harus mengandung setidaknya satu huruf besar.") // Harus mengandung huruf besar
                .Matches("[a-z]").WithMessage("Password harus mengandung setidaknya satu huruf kecil.") // Harus mengandung huruf kecil
                .Matches("[0-9]").WithMessage("Password harus mengandung setidaknya satu angka.") // Harus mengandung angka
                .Matches("[!@#$%^&*]").WithMessage("Password harus mengandung setidaknya satu karakter khusus."); // Harus mengandung karakter khusus

            // Aturan validasi untuk properti Guid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Guid)
                .NotEmpty()
                .WithMessage("Guid tidak boleh kosong");
        }
    }

}
