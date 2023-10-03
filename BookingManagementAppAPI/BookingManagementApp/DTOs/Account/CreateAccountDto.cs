using BookingManagementApp.Models;
using System;

namespace BookingManagementApp.DTOs
{
    public class CreateAccountDto
    {
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        //DTO Untuk Create Acoount
        public static implicit operator Account(CreateAccountDto createAccountDto)
        {
            return new Account
            {
                Password = createAccountDto.Password,
                IsDeleted = createAccountDto.IsDeleted,
                Otp = createAccountDto.Otp,
                IsUsed = createAccountDto.IsUsed,
                ExpiredTime = createAccountDto.ExpiredTime,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
