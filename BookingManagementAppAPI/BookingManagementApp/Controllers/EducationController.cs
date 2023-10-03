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
    public class EducationController : ControllerBase
    {
        private readonly IRepository<Education> _educationRepository;

        public EducationController(IRepository<Education> educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Mendapatkan semua data education dari repository
            var result = _educationRepository.GetAll();

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

            // Melakukan mapping setiap item dalam variabel result ke dalam objek EducationDto
            var data = result.Select(x => (EducationDto)x);

            // Mengembalikan respons OK dengan data yang sesuai
            return Ok(new ResponseOKHandler<IEnumerable<EducationDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            // Mendapatkan data education berdasarkan GUID dari repository
            var result = _educationRepository.GetByGuid(guid);

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

            // Mengembalikan respons OK dengan data EducationDto yang sesuai
            return Ok(new ResponseOKHandler<EducationDto>((EducationDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateEducationDto educationDto)
        {
            try
            {
                // Membuat education baru menggunakan data yang diterima dalam request
                var result = _educationRepository.Create(educationDto);

                // Jika gagal membuat education, kembalikan respons BadRequest
                if (result is null)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to create data"
                    });
                }

                // Mengembalikan respons OK dengan data EducationDto yang sesuai
                return Ok(new ResponseOKHandler<EducationDto>((EducationDto)result));
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
                // Mendapatkan objek education berdasarkan GUID dari repository
                var educationById = _educationRepository.GetByGuid(guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (educationById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                // Menghapus objek education dari repository
                var result = _educationRepository.Delete(educationById);

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
        public IActionResult Update(EducationDto educationDto)
        {
            try
            {
                // Mendapatkan objek education berdasarkan GUID dari repository
                var educationById = _educationRepository.GetByGuid(educationDto.Guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (educationById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                Education toUpdate = educationDto;

                //Inisialiasi nilai CreatedDate agar tidak ada perubahan dari data awal
                toUpdate.CreatedDate = educationById.CreatedDate;

                //Melakukan Update dengan parameter toUpdate
                var result = _educationRepository.Update(toUpdate);

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
