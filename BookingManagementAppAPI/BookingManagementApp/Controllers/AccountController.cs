using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using BookingManagementApp.Handler;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IRepository<Account> _accountRepository;

        public AccountController(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateAccountDto accountDto)
        {
            // Meng-hash kata sandi sebelum menyimpannya ke database.
            string hashedPassword = HashingHandler.HashPassword(accountDto.Password);

            // Mengganti kata sandi asli dengan yang di-hash sebelum menyimpannya ke DTO.
            accountDto.Password = hashedPassword;

            // Memanggil metode Create dari _accountRepository dengan parameter DTO yang sudah di-hash.
            var result = _accountRepository.Create(accountDto);

            // Memeriksa apakah penciptaan data berhasil atau gagal.
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            // Mengembalikan data yang berhasil dibuat dalam respons OK.
            return Ok((AccountDto)result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var accountById = _accountRepository.GetByGuid(guid);
            if (accountById is null)
            {
                return NotFound("ID Not Found");
            }
            var result = _accountRepository.Delete(accountById);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Account account)
        {
            var accountById = _accountRepository.GetByGuid(account.Guid);
            if (accountById is null)
            {
                return NotFound("ID Not Found");
            }
            accountById.Password = account.Password;
            accountById.IsDeleted = account.IsDeleted;
            accountById.Otp = account.Otp;
            accountById.IsUsed = account.IsUsed;
            accountById.ExpiredTime = account.ExpiredTime;

            var result = _accountRepository.Update(accountById);
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }
            return Ok(result);
        }
    }
}
