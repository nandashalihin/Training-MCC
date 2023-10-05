using BookingManagementApp.DTOs;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.AccountRoles
{
    public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDto>
    {
        public CreateAccountRoleValidator() {

            // Aturan validasi untuk properti AccountGuid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.AccountGuid)
                .NotEmpty()
                .WithMessage("AccountGuid tidak boleh kosong");

            // Aturan validasi untuk properti RoleGuid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.RoleGuid)
                .NotEmpty()
                .WithMessage("RoleGuid tidak boleh kosong");
        }
    }
}
