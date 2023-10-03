using BookingManagementApp.Models;
using System;

namespace BookingManagementApp.DTOs
{
    public class AccountDto
    {
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public int Otp { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // DTO untuk Get Account
        public static implicit operator AccountDto(Account account)
        {
            return new AccountDto
            {
                Password = account.Password,
                IsDeleted = account.IsDeleted,
                Otp = account.Otp,
                IsUsed = account.IsUsed,
                ExpiredTime = account.ExpiredTime,
                CreatedDate = account.CreatedDate,
                ModifiedDate = account.ModifiedDate
            };
        }

        // DTO untuk Update Account
        public static implicit operator Account(AccountDto accountDto)
        {
            return new Account
            {
                Password = accountDto.Password,
                IsDeleted = accountDto.IsDeleted,
                Otp = accountDto.Otp,
                IsUsed = accountDto.IsUsed,
                ExpiredTime = accountDto.ExpiredTime,
                CreatedDate = accountDto.CreatedDate,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
