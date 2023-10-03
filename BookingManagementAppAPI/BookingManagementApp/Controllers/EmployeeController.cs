using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Mendapatkan data Employee dari repository dan disimpan pada variabel result
            var result = _employeeRepository.GetAll();

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

            // Melakukan mapping setiap item dalam variabel result ke dalam objek EmployeeDto
            // menggunakan explicit operator, kemudian mengembalikan respons OK dengan data yang sesuai
            var data = result.Select(x => (EmployeeDto)x);
            return Ok(new ResponseOKHandler<IEnumerable<EmployeeDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            // Mendapatkan data Employee berdasarkan GUID dari repository
            var result = _employeeRepository.GetByGuid(guid);

            // Jika data tidak ditemukan, kembalikan respons NotFound
            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            // Mengembalikan respons OK dengan data EmployeeDto yang sesuai
            return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto employeeDto)
        {
            try
            {
                // Membuat objek Employee dari data yang diterima dalam request
                Employee toCreate = employeeDto;

                // Menghasilkan Nomor Induk Karyawan (NIK) baru dan mengisi NIK dalam objek Employee
                toCreate.Nik = GenerateHandler.GenerateNik(_employeeRepository.GetLastNik());
                toCreate.Nik = null; // Perlu ditinjau ulang, mengapa NIK di-set menjadi null?

                // Menyimpan objek Employee yang baru dibuat ke dalam repository
                var result = _employeeRepository.Create(toCreate);

                // Mengembalikan respons OK dengan data EmployeeDto yang sesuai
                return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)result));
            }
            catch (ExceptionHandler ex)
            {
                // Jika terjadi kesalahan, mengembalikan respons dengan status kode 500 (InternalServerError)
                // dan pesan kesalahan yang sesuai
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                // Mendapatkan objek Employee berdasarkan GUID dari repository
                var entity = _employeeRepository.GetByGuid(guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                // Menghapus objek Employee dari repository
                _employeeRepository.Delete(entity);

                // Mengembalikan respons OK dengan pesan "Data Deleted"
                return Ok(new ResponseOKHandler<string>("Data Deleted"));
            }
            catch (ExceptionHandler ex)
            {
                // Jika terjadi kesalahan, mengembalikan respons dengan status kode 500 (InternalServerError)
                // dan pesan kesalahan yang sesuai
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            try
            {
                // Mendapatkan objek Employee berdasarkan GUID dari repository
                var entity = _employeeRepository.GetByGuid(employeeDto.Guid);

                // Jika data tidak ditemukan, kembalikan respons NotFound
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                // Memperbarui objek Employee dengan data yang diterima dalam request
                Employee toUpdate = employeeDto;
                toUpdate.Nik = entity.Nik; // Menyimpan NIK yang sudah ada
                toUpdate.CreatedDate = entity.CreatedDate; // Menyimpan tanggal pembuatan yang sudah ada

                // Menyimpan perubahan ke dalam repository
                _employeeRepository.Update(toUpdate);

                // Mengembalikan respons OK dengan pesan "Data Updated"
                return Ok(new ResponseOKHandler<string>("Data Updated"));
            }
            catch (ExceptionHandler ex)
            {
                // Jika terjadi kesalahan, mengembalikan respons dengan status kode 500 (InternalServerError)
                // dan pesan kesalahan yang sesuai
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

    }
}
