using BookingManagementApp.DTOs;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.AccountRoles
{
    public class UpdateAccountRoleValidator : AbstractValidator<AccountRoleDto>
    {
        // Konstruktor dari kelas UpdateAccountRoleValidator
        public UpdateAccountRoleValidator()
        {
            // RuleFor digunakan untuk menentukan aturan validasi untuk setiap properti pada objek AccountRoleDto.

            // Aturan validasi untuk properti AccountGuid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.AccountGuid)
                .NotEmpty()
                .WithMessage("AccountGuid tidak boleh kosong");

            // Aturan validasi untuk properti RoleGuid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.RoleGuid)
                .NotEmpty()
                .WithMessage("RoleGuid tidak boleh kosong");

            // Aturan validasi untuk properti Guid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Guid)
                .NotEmpty()
                .WithMessage("Guid tidak boleh kosong");
        }
    }

}
