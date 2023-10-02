using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IRepository<Booking> _bookingRepository;

        public BookingController(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookingRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            var result = _bookingRepository.Create(booking);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var bookingById = _bookingRepository.GetByGuid(guid);
            if (bookingById is null)
            {
                return NotFound("ID Not Found");
            }
            var result = _bookingRepository.Delete(bookingById);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Booking booking)
        {
            var bookingById = _bookingRepository.GetByGuid(booking.Guid);
            if (bookingById is null)
            {
                return NotFound("ID Not Found");
            }
            bookingById.StartDate = booking.StartDate;
            bookingById.EndDate = booking.EndDate;
            bookingById.Status = booking.Status;
            bookingById.Remarks = booking.Remarks;
            bookingById.RoomGuid = booking.RoomGuid;
            bookingById.EmployeeGuid = booking.EmployeeGuid;

            var result = _bookingRepository.Update(bookingById);
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }
            return Ok(result);
        }
    }
}
