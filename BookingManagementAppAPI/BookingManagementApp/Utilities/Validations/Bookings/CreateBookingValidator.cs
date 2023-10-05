using BookingManagementApp.DTOs;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.Bookings
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingValidator() {
            // Aturan validasi untuk properti EmployeeGuid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.EmployeeGuid)
                .NotEmpty()
                .WithMessage("EmployeeGuid tidak boleh kosong");

            // Aturan validasi untuk properti StartDate: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.StartDate)
                .NotEmpty()
                .WithMessage("StartDate tidak boleh kosong");

            // Aturan validasi untuk properti EndDate: 
            // - Tidak boleh kosong (NotEmpty)
            // - Harus lebih besar atau sama dengan StartDate (GreaterThanOrEqualTo)
            RuleFor(e => e.EndDate)
                .NotEmpty()
                .WithMessage("EndDate tidak boleh kosong")
                .GreaterThanOrEqualTo(e => e.StartDate)
                .WithMessage("EndDate harus lebih besar atau sama dengan StartDate");

            // Aturan validasi untuk properti Status: 
            // - Tidak boleh kosong (NotEmpty)
            // - Harus merupakan nilai enum yang valid (IsInEnum)
            RuleFor(e => e.Status)
                .NotEmpty()
                .WithMessage("Status tidak boleh kosong")
                .IsInEnum()
                .WithMessage("Status tidak valid");

            // Aturan validasi untuk properti Remarks: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Remarks)
                .NotEmpty()
                .WithMessage("Remarks tidak boleh kosong");

            // Aturan validasi untuk properti RoomGuid: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.RoomGuid)
                .NotEmpty()
                .WithMessage("RoomGuid tidak boleh kosong");

        } 
    }
}
