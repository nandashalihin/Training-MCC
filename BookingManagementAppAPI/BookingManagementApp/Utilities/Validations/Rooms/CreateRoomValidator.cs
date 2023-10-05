using BookingManagementApp.DTOs;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.Rooms
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomDto>
    {
        public CreateRoomValidator() 
        {
            // Aturan validasi untuk properti Name: Tidak boleh kosong (NotEmpty)
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("Name tidak boleh kosong");

            // Aturan validasi untuk properti Floor:
            // - Tidak boleh kosong (NotEmpty)
            // - Harus lebih besar dari atau sama dengan 1 (GreaterThanOrEqualTo)
            RuleFor(e => e.Floor)
                .NotEmpty()
                .WithMessage("Floor tidak boleh kosong")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Floor harus lebih besar dari atau sama dengan 1");

            // Aturan validasi untuk properti Capacity:
            // - Tidak boleh kosong (NotEmpty)
            // - Harus berada dalam rentang 10 hingga 1000 (InclusiveBetween)
            RuleFor(e => e.Capacity)
                .NotEmpty()
                .WithMessage("Capacity tidak boleh kosong")
                .InclusiveBetween(10, 1000)
                .WithMessage("Capacity harus berada dalam rentang 10 hingga 1000");

        }
    }
}

