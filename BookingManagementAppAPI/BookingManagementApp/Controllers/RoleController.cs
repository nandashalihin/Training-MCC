using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using BookingManagementApp.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleController(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Mendapatkan semua data role dari repository
            var result = _roleRepository.GetAll();

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

            // Melakukan mapping setiap item dalam variabel result ke dalam objek RoleDto
            var data = result.Select(x => (RoleDto)x);

            // Mengembalikan respons OK dengan data yang sesuai
            return Ok(new ResponseOKHandler<IEnumerable<RoleDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            // Mendapatkan data role berdasarkan GUID dari repository
            var result = _roleRepository.GetByGuid(guid);

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

            // Mengembalikan respons OK dengan data RoleDto yang sesuai
            return Ok(new ResponseOKHandler<RoleDto>((RoleDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateRoleDto roleDto)
        {
            try
            {
                // Membuat role baru menggunakan data yang diterima dalam request
                var result = _roleRepository.Create(roleDto);

                // Jika gagal membuat role, kembalikan respons BadRequest
                if (result is null)
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to create data"
                    });
                }

                // Mengembalikan respons OK dengan data RoleDto yang sesuai
                return Ok(new ResponseOKHandler<RoleDto>((RoleDto)result));
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

        [HttpPut]
        public IActionResult Update(RoleDto roleDto)
        {
            try
            {
                // Mendapatkan objek role berdasarkan GUID dari repository
                var roleById = _roleRepository.GetByGuid(roleDto.Guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (roleById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                // Memperbarui objek role dengan data yang diterima dalam request

                Role toUpdate = roleDto;

                //Inisialiasi nilai CreatedDate agar tidak ada perubahan dari data awal
                toUpdate.CreatedDate = roleById.CreatedDate;

                // Menyimpan perubahan ke dalam repository
                var result = _roleRepository.Update(roleById);

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

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                // Mendapatkan objek role berdasarkan GUID dari repository
                var roleById = _roleRepository.GetByGuid(guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (roleById is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "ID Not Found"
                    });
                }

                // Menghapus objek role dari repository
                var result = _roleRepository.Delete(roleById);

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
    }
}
