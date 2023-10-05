using BookingManagementApp.DTOs;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.Roles
{
    public class UpdateRoleValidator : AbstractValidator<RoleDto>
    {
        public UpdateRoleValidator()
        {
            // RuleFor digunakan untuk menentukan aturan validasi untuk setiap properti pada objek Role.

            // Aturan validasi untuk properti Guid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Guid)
                .NotEmpty()
                .WithMessage("Guid tidak boleh kosong");

            // Aturan validasi untuk properti Name:
            // - Tidak boleh kosong (NotEmpty)
            // - Harus memiliki panjang minimal 3 karakter (MinimumLength)
            // - Tidak boleh memiliki panjang lebih dari 50 karakter (MaximumLength)
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Name tidak boleh kosong")
                .MinimumLength(3)
                .WithMessage("Name harus memiliki setidaknya 3 karakter")
                .MaximumLength(50)
                .WithMessage("Name tidak boleh lebih dari 50 karakter");
        }

    }
}
