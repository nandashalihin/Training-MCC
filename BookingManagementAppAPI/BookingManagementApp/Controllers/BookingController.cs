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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingController(IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
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

        // Mendefinisikan metode HTTP GET dengan endpoint "rooms-in-use".
        [HttpGet("rooms-in-use")]
        public IActionResult GetRoomsInUseToday()
        {
            try
            {
                // Mengambil tanggal hari ini.
                var currentDate = DateTime.Today;

                // Menggunakan _bookingRepository untuk mengambil daftar pemesanan (bookingsToday) untuk tanggal hari ini.
                var bookingsToday = _bookingRepository.GetBookingsForDate(currentDate);

                // Memeriksa jika tidak ada pemesanan yang ditemukan.
                if (bookingsToday == null)
                {
                    // Jika tidak ada pemesanan, mengembalikan respons BadRequest dengan pesan error.
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Failed to fetch data"
                    });
                }

                // Menggunakan data pemesanan (bookingsToday) untuk membuat daftar ruangan yang sedang digunakan (roomsInUse).
                var roomsInUse = bookingsToday.Select(b => new BookingInfoDto
                {
                    BookingGuid = b.Guid, // GUID pemesanan
                    RoomName = _bookingRepository.GetRoomByGuid(b.RoomGuid).Name, // Nama ruangan dari GUID ruangan pemesanan
                    Status = b.Status, // Status pemesanan
                    Floor = _bookingRepository.GetRoomByGuid(b.RoomGuid).Floor, // Lantai ruangan dari GUID ruangan pemesanan
                    BookedBy = _bookingRepository.GetBookedBy(b.EmployeeGuid) // Nama karyawan yang melakukan pemesanan
                }).ToList();

                // Mengembalikan daftar ruangan yang sedang digunakan dalam respons OK.
                return Ok(new ResponseOKHandler<IEnumerable<BookingInfoDto>>(roomsInUse));
            }
            catch (Exception ex)
            {
                // Mengatasi pengecualian (exception) yang mungkin terjadi.
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while fetching data",
                    Error = ex.Message // Pesan error dari pengecualian
                });
            }
        }


        // Mendefinisikan metode HTTP GET dengan endpoint "GetAllDetails".
        [HttpGet("GetAllDetails")]
        public IActionResult GetAllDetails()
        {
            try
            {
                // Mengambil daftar pemesanan (bookings), karyawan (employees), dan ruangan (rooms) menggunakan repositori yang sesuai.
                var bookings = _bookingRepository.GetAll();
                var employees = _employeeRepository.GetAll();
                var rooms = _roomRepository.GetAll();

                // Memeriksa jika tidak ada data pemesanan yang ditemukan atau tidak ada data pemesanan sama sekali.
                if (bookings == null || !bookings.Any())
                {
                    // Jika tidak ada data pemesanan, mengembalikan respons BadRequest dengan pesan error.
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "No booking data found"
                    });
                }

                // Membuat daftar bookingDetails yang berisi detail lengkap dari setiap pemesanan.
                var bookingDetails = bookings.Select(b => new BookingDetailDto
                {
                    Guid = b.Guid, // GUID pemesanan
                    BookedNIK = _employeeRepository.GetEmployeeNik(b.EmployeeGuid), // NIK karyawan yang melakukan pemesanan
                    BookedBy = _employeeRepository.GetEmployeeName(b.EmployeeGuid), // Nama karyawan yang melakukan pemesanan
                    RoomName = _roomRepository.GetRoomName(b.RoomGuid), // Nama ruangan dari GUID ruangan pemesanan
                    StartDate = b.StartDate, // Tanggal mulai pemesanan
                    EndDate = b.EndDate, // Tanggal berakhir pemesanan
                    Status = b.Status, // Status pemesanan
                    Remarks = b.Remarks // Catatan pemesanan
                }).ToList();

                // Mengembalikan daftar detail pemesanan dalam respons OK.
                return Ok(new ResponseOKHandler<List<BookingDetailDto>>(bookingDetails));
            }
            catch (Exception ex)
            {
                // Mengatasi pengecualian (exception) yang mungkin terjadi.
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while fetching booking details",
                    Error = ex.Message // Pesan error dari pengecualian
                });
            }
        }


        // Mendefinisikan metode HTTP GET dengan endpoint "GetByGuid/{guid}".
        [HttpGet("GetByGuid/{guid}")]
        public IActionResult GetByGuidDetail(Guid guid)
        {
            try
            {
                // Menggunakan _bookingRepository untuk mencari pemesanan (booking) berdasarkan GUID yang diberikan.
                var booking = _bookingRepository.GetByGuid(guid);

                // Memeriksa jika pemesanan tidak ditemukan.
                if (booking == null)
                {
                    // Jika tidak ada pemesanan yang ditemukan, mengembalikan respons NotFound dengan pesan error.
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Booking not found"
                    });
                }

                // Membuat objek BookingDetailDto yang berisi detail lengkap dari pemesanan yang ditemukan.
                var bookingDetail = new BookingDetailDto
                {
                    Guid = booking.Guid, // GUID pemesanan
                    BookedNIK = _employeeRepository.GetEmployeeNik(booking.EmployeeGuid), // NIK karyawan yang melakukan pemesanan
                    BookedBy = _employeeRepository.GetEmployeeName(booking.EmployeeGuid), // Nama karyawan yang melakukan pemesanan
                    RoomName = _roomRepository.GetRoomName(booking.RoomGuid), // Nama ruangan dari GUID ruangan pemesanan
                    StartDate = booking.StartDate, // Tanggal mulai pemesanan
                    EndDate = booking.EndDate, // Tanggal berakhir pemesanan
                    Status = booking.Status, // Status pemesanan
                    Remarks = booking.Remarks // Catatan pemesanan
                };

                // Mengembalikan detail pemesanan dalam respons OK.
                return Ok(new ResponseOKHandler<BookingDetailDto>(bookingDetail));
            }
            catch (Exception ex)
            {
                // Mengatasi pengecualian (exception) yang mungkin terjadi.
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while fetching booking by GUID",
                    Error = ex.Message // Pesan error dari pengecualian
                });
            }
        }


        // Mendefinisikan metode HTTP GET dengan endpoint "AvailableRooms".
        [HttpGet("AvailableRooms")]
        public IActionResult GetAvailableRooms()
        {
            try
            {
                // Mendapatkan tanggal hari ini.
                DateTime today = DateTime.Today;

                // Menggunakan _bookingRepository untuk mengambil daftar pemesanan (bookingsToday) untuk tanggal hari ini.
                var bookingsToday = _bookingRepository.GetBookingsForDate(today);

                // Menggunakan _roomRepository untuk mengambil daftar semua ruangan (allRooms).
                var allRooms = _roomRepository.GetAll();

                // Memfilter daftar semua ruangan (allRooms) untuk menemukan ruangan yang tersedia pada hari ini.
                var availableRooms = allRooms.Where(room =>
                    !bookingsToday.Any(booking => booking.RoomGuid == room.Guid)).ToList();

                // Memeriksa jika tidak ada ruangan yang tersedia atau daftar ruangan kosong.
                if (availableRooms == null || !availableRooms.Any())
                {
                    // Jika tidak ada ruangan yang tersedia, mengembalikan respons BadRequest dengan pesan error.
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "No available rooms found for today"
                    });
                }

                // Membuat daftar DTO (Data Transfer Object) yang berisi informasi ruangan yang tersedia.
                var availableRoomDtos = availableRooms.Select(room => new AvailableRoomDto
                {
                    RoomGuid = room.Guid, // GUID ruangan
                    RoomName = room.Name, // Nama ruangan
                    Floor = room.Floor, // Lantai ruangan
                    Capacity = room.Capacity // Kapasitas ruangan
                }).ToList();

                // Mengembalikan daftar ruangan yang tersedia dalam respons OK.
                return Ok(new ResponseOKHandler<List<AvailableRoomDto>>(availableRoomDtos));
            }
            catch (Exception ex)
            {
                // Mengatasi pengecualian (exception) yang mungkin terjadi.
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while fetching available rooms",
                    Error = ex.Message // Pesan error dari pengecualian
                });
            }
        }


        // Mendefinisikan metode HTTP GET dengan endpoint "BookingDuration".
        [HttpGet("BookingDuration")]
        public IActionResult GetBookingDuration()
        {
            try
            {
                // Menggunakan _bookingRepository untuk mengambil daftar semua pemesanan (allBookings).
                var allBookings = _bookingRepository.GetAll();

                // Menginisialisasi daftar hari-hari akhir pekan (weekendDays).
                var weekendDays = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };

                // Menghitung durasi pemesanan (bookingDurations) untuk setiap pemesanan.
                var bookingDurations = allBookings.Select(booking =>
                {
                    var startDate = booking.StartDate.Date;
                    var endDate = booking.EndDate.Date;

                    // Menghasilkan daftar hari-hari non-akhir pekan antara tanggal mulai dan tanggal berakhir pemesanan.
                    var nonWeekendDays = Enumerable.Range(0, (int)(endDate - startDate).TotalDays + 1)
                        .Select(offset => startDate.AddDays(offset))
                        .Where(date => !weekendDays.Contains(date.DayOfWeek));

                    // Menghitung jumlah hari kerja (workingDays).
                    var workingDays = nonWeekendDays.Count();

                    // Membuat objek BookingDurationDto yang berisi informasi durasi pemesanan.
                    return new BookingDurationDto
                    {
                        RoomGuid = booking.RoomGuid, // GUID ruangan
                        RoomName = booking.Room?.Name, // Nama ruangan
                        BookingLength = workingDays // Durasi pemesanan dalam hari kerja
                    };
                }).ToList();

                // Memeriksa jika tidak ada durasi pemesanan yang ditemukan atau daftar durasi pemesanan kosong.
                if (bookingDurations == null || !bookingDurations.Any())
                {
                    // Jika tidak ada durasi pemesanan yang ditemukan, mengembalikan respons BadRequest dengan pesan error.
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "No booking durations found"
                    });
                }

                // Mengembalikan daftar durasi pemesanan dalam respons OK.
                return Ok(new ResponseOKHandler<List<BookingDurationDto>>(bookingDurations));
            }
            catch (Exception ex)
            {
                // Mengatasi pengecualian (exception) yang mungkin terjadi.
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "An error occurred while calculating booking durations",
                    Error = ex.Message // Pesan error dari pengecualian
                });
            }
        }


    }
}
