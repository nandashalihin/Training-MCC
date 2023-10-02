using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

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
            var employees = _employeeRepository.GetAll();
            if (employees.Any())
            {
                return Ok(employees);
            }
            else
            {
                return NotFound("Data Not Found");
            }
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is not null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound("Data Not Found");
            }
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            var createdEmployee = _employeeRepository.Create(employee);
            if (createdEmployee is not null)
            {
                return Ok(createdEmployee);
            }
            else
            {
                return BadRequest("Failed to create data");
            }
        }

        [HttpPut("{guid}")]
        public IActionResult Update(Employee employee)
        {
            var employeeById = _employeeRepository.GetByGuid(employee.Guid);
            if (employeeById is null)
            {
                return NotFound("ID Not Found");
            }

            employeeById.FirstName = employee.FirstName;
            employeeById.LastName = employee.LastName;
            var result = _employeeRepository.Update(employeeById);
            if (!result)
            {
                return BadRequest("Failed to Update Date");
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
    }
}
