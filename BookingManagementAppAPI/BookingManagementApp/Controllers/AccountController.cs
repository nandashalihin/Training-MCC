using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using BookingManagementApp.Handlers;
using Microsoft.AspNetCore.Mvc; 
using System;
using System.Collections.Generic;
using System.Linq;
using BookingManagementApp.Repositories;
using BookingManagementApp.Utilities.Handlers;
using System.Net;

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
            // Mendapatkan semua data account dari repository
            var result = _accountRepository.GetAll();

            // Jika tidak ada data ditemukan, kembalikan respons NotFound
            if (!result.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            // Melakukan mapping setiap item dalam variabel result ke dalam objek AccountDto
            var data = result.Select(x => (AccountDto)x);

            // Mengembalikan respons OK dengan data yang sesuai
            return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            // Mendapatkan data account berdasarkan GUID dari repository
            var result = _accountRepository.GetByGuid(guid);

            // Jika data tidak ditemukan, kembalikan respons NotFound
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "ID Not Found"
                });
            }

            // Mengembalikan respons OK dengan data AccountDto yang sesuai
            return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateAccountDto accountDto)
        {
            try
            {
                // Membuat account baru menggunakan data yang diterima dalam request
                var result = _accountRepository.Create(accountDto);

                // Jika gagal membuat account, kembalikan respons BadRequest
                if (result is null)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to create data"
                    });
                }

                // Mengembalikan respons OK dengan data AccountDto yang sesuai
                return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
            }
            catch (Exception ex)
            {
                // Jika terjadi kesalahan, mengembalikan respons dengan status kode 500 (InternalServerError)
                // dan pesan kesalahan yang sesuai
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while creating data",
                    Error = ex.Message
                });
            }
        }


        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                // Mendapatkan objek account berdasarkan GUID dari repository
                var accountById = _accountRepository.GetByGuid(guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (accountById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                // Menghapus objek account dari repository
                var result = _accountRepository.Delete(accountById);

                // Jika gagal menghapus, kembalikan respons BadRequest
                if (!result)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to delete data"
                    });
                }

                // Mengembalikan respons OK dengan pesan "Data Deleted"
                return Ok(new ResponseOKHandler<string>("Data Deleted"));
            }
            catch (Exception ex)
            {
                // Jika terjadi kesalahan, mengembalikan respons dengan status kode 500 (InternalServerError)
                // dan pesan kesalahan yang sesuai
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while deleting data",
                    Error = ex.Message
                });
            }
        }


        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            try
            {
                // Mendapatkan objek account berdasarkan GUID dari repository
                var accountById = _accountRepository.GetByGuid(accountDto.Guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (accountById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                Account toUpdate = accountDto;

                //Inisialiasi nilai CreatedDate agar tidak ada perubahan dari data awal
                toUpdate.CreatedDate = accountById.CreatedDate;

                //Melakukan Update dengan parameter toUpdate
                var result = _accountRepository.Update(toUpdate);

                // Jika gagal memperbarui, kembalikan respons BadRequest
                if (!result)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to Update Data"
                    });
                }

                // Mengembalikan respons OK dengan pesan "Data Updated"
                return Ok(new ResponseOKHandler<string>("Data Updated"));
            }
            catch (Exception ex)
            {
                // Jika terjadi kesalahan, mengembalikan respons dengan status kode 500 (InternalServerError)
                // dan pesan kesalahan yang sesuai
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while updating data",
                    Error = ex.Message
                });
            }
        }
    }
}
