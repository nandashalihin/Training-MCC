using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

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
            var result = _roleRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _roleRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Role role)
        {
            var result = _roleRepository.Create(role);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Role role)
        {
            var roleById = _roleRepository.GetByGuid(role.Guid);
            if (roleById is null)
            {
                return NotFound("ID Not Found");
            }

            roleById.Name = role.Name;

            var result = _roleRepository.Update(roleById);
            if (!result)
            {
                return BadRequest("Failed to Update Date");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var roleById = _roleRepository.GetByGuid(guid);
            if (roleById is null)
            {
                return NotFound("ID Not Found");
            }

            var result = _roleRepository.Delete(roleById);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }

            return Ok(result);
        }
    }
}
