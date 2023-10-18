using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using BookingManagementApp.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace BookingManagementApp.Controllers

{
    [ApiController]
    [Route("api/[controller]")]

    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Mendapatkan semua data universitas dari repository
            var result = _universityRepository.GetAll();

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

            // Melakukan mapping setiap item dalam variabel result ke dalam objek UniversityDto
            var data = result.Select(x => (UniversityDto)x);

            // Mengembalikan respons OK dengan data yang sesuai
            return Ok(new ResponseOKHandler<IEnumerable<UniversityDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            // Mendapatkan data universitas berdasarkan GUID dari repository
            var result = _universityRepository.GetByGuid(guid);

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

            // Mengembalikan respons OK dengan data UniversityDto yang sesuai
            return Ok(new ResponseOKHandler<UniversityDto>((UniversityDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateUniversityDto universityDto)
        {
            try
            {
                // Membuat universitas baru menggunakan data yang diterima dalam request
                var result = _universityRepository.Create(universityDto);

                // Jika gagal membuat universitas, kembalikan respons BadRequest
                if (result is null)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to create data"
                    });
                }

                // Mengembalikan respons OK dengan data UniversityDto yang sesuai
                return Ok(new ResponseOKHandler<UniversityDto>((UniversityDto)result));
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
                // Mendapatkan objek universitas berdasarkan GUID dari repository
                var universityById = _universityRepository.GetByGuid(guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (universityById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                // Menghapus objek universitas dari repository
                var result = _universityRepository.Delete(universityById);

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
        public IActionResult Update(UniversityDto universityDto)
        {
            try
            {
                // Mendapatkan objek universitas berdasarkan GUID dari repository
                var universityById = _universityRepository.GetByGuid(universityDto.Guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (universityById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                University toUpdate = universityDto;

                //Inisialiasi nilai CreatedDate agar tidak ada perubahan dari data awal
                toUpdate.CreatedDate = universityById.CreatedDate;

                //Melakukan Update dengan parameter toUpdate
                var result = _universityRepository.Update(toUpdate);

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
