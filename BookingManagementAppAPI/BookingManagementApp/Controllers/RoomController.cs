using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRepository<Room> _roomRepository;

        public RoomController(IRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roomRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            var result = _roomRepository.Create(room);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Room room)
        {
            var roomById = _roomRepository.GetByGuid(room.Guid);
            if (roomById is null)
            {
                return NotFound("ID Not Found");
            }

            roomById.Name = room.Name;
            roomById.Floor = room.Floor;
            roomById.Capacity = room.Capacity;

            var result = _roomRepository.Update(roomById);
            if (!result)
            {
                return BadRequest("Failed to Update Date");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var roomById = _roomRepository.GetByGuid(guid);
            if (roomById is null)
            {
                return NotFound("ID Not Found");
            }

            var result = _roomRepository.Delete(roomById);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }

            return Ok(result);
        }
    }
}
