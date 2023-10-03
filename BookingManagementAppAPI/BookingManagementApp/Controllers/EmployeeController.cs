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
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var result = _employeeRepository.Create(employee);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var employeeById = _employeeRepository.GetByGuid(guid);
            if (employeeById is null)
            {
                return NotFound("ID Not Found");
            }
            var result = _employeeRepository.Delete(employeeById);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            var employeeById = _employeeRepository.GetByGuid(employee.Guid);
            if (employeeById is null)
            {
                return NotFound("ID Not Found");
            }
            employeeById.Nik = employee.Nik;
            employeeById.FirstName = employee.FirstName;
            employeeById.LastName = employee.LastName;
            employeeById.BirthDate = employee.BirthDate;
            employeeById.Gender = employee.Gender;
            employeeById.HiringDate = employee.HiringDate;
            employeeById.Email = employee.Email;
            employeeById.PhoneNumber = employee.PhoneNumber;

            var result = _employeeRepository.Update(employeeById);
            if (!result)
            {
                return BadRequest("Failed to Update Data");
            }
            return Ok(result);
        }
    }
}
