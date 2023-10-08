using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using BookingManagementApp.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Mendapatkan semua data booking dari repository
            var result = _bookingRepository.GetAll();

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

            // Melakukan mapping setiap item dalam variabel result ke dalam objek BookingDto
            var data = result.Select(x => (BookingDto)x);

            // Mengembalikan respons OK dengan data yang sesuai
            return Ok(new ResponseOKHandler<IEnumerable<BookingDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            // Mendapatkan data booking berdasarkan GUID dari repository
            var result = _bookingRepository.GetByGuid(guid);

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

            // Mengembalikan respons OK dengan data BookingDto yang sesuai
            return Ok(new ResponseOKHandler<BookingDto>((BookingDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateBookingDto bookingDto)
        {
            try
            {
                // Membuat booking baru menggunakan data yang diterima dalam request
                var result = _bookingRepository.Create(bookingDto);

                // Jika gagal membuat booking, kembalikan respons BadRequest
                if (result is null)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to create data"
                    });
                }

                // Mengembalikan respons OK dengan data BookingDto yang sesuai
                return Ok(new ResponseOKHandler<BookingDto>((BookingDto)result));
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
                // Mendapatkan objek booking berdasarkan GUID dari repository
                var bookingById = _bookingRepository.GetByGuid(guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (bookingById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                // Menghapus objek booking dari repository
                var result = _bookingRepository.Delete(bookingById);

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
        public IActionResult Update(BookingDto bookingDto)
        {
            try
            {
                // Mendapatkan objek booking berdasarkan GUID dari repository
                var bookingById = _bookingRepository.GetByGuid(bookingDto.Guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (bookingById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                Booking toUpdate = bookingDto;

                //Inisialiasi nilai CreatedDate agar tidak ada perubahan dari data awal
                toUpdate.CreatedDate = bookingById.CreatedDate;

                //Melakukan Update dengan parameter toUpdate
                var result = _bookingRepository.Update(toUpdate);

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
