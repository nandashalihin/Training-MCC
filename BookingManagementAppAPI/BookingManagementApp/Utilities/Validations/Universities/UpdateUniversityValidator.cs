using BookingManagementApp.DTOs;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.Universities
{
    public class UpdateUniversityValidator : AbstractValidator <UniversityDto>
    {
        public UpdateUniversityValidator()
        {
            // RuleFor digunakan untuk menentukan aturan validasi untuk setiap properti pada objek University.

            // Aturan validasi untuk properti Guid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Guid)
                .NotEmpty()
                .WithMessage("Guid tidak boleh kosong");

            // Aturan validasi untuk properti Code: 
            // - Tidak boleh kosong (NotEmpty)
            // - Harus memiliki panjang minimal 3 karakter (MinimumLength)
            // - Tidak boleh memiliki panjang lebih dari 10 karakter (MaximumLength)
            RuleFor(e => e.Code)
                .NotEmpty()
                .WithMessage("Code tidak boleh kosong")
                .MinimumLength(3)
                .WithMessage("Code harus memiliki setidaknya 3 karakter")
                .MaximumLength(10)
                .WithMessage("Code tidak boleh lebih dari 10 karakter");

            // Aturan validasi untuk properti Name:
            // - Tidak boleh kosong (NotEmpty)
            // - Harus memiliki panjang minimal 5 karakter (MinimumLength)
            // - Tidak boleh memiliki panjang lebih dari 50 karakter (MaximumLength)
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Nama tidak boleh kosong")
                .MinimumLength(5)
                .WithMessage("Nama harus memiliki setidaknya 5 karakter")
                .MaximumLength(50)
                .WithMessage("Nama tidak boleh lebih dari 50 karakter");
        }

    }
}
