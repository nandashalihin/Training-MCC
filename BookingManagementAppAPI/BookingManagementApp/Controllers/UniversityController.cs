﻿using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
namespace BookingManagementApp.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IRepository<University> _universityRepository;

        public UniversityController(IRepository<University> universityRepository)
        {
            _universityRepository = universityRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _universityRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }
            var data = result.Select(x => (UniversityDto)x);
            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _universityRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Id Not Found");
            }
            return Ok((UniversityDto)result);
        }

        [HttpPost]
        public IActionResult Create(CreateUniversityDto universityDto)
        {
            var result = _universityRepository.Create(universityDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((UniversityDto)result);
        }
        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var universityById = _universityRepository.GetByGuid(guid);
            if (universityById is null)
            {
                return NotFound("ID Not Found");
            }
            var result = _universityRepository.Delete(universityById);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(University university)
        {
            var universityById = _universityRepository.GetByGuid(university.Guid);
            if (universityById is null)
            {
                return NotFound("ID Not Found");
            }
            universityById.Code = university.Code;
            universityById.Name = university.Name;

            var result = _universityRepository.Update(universityById);
            if (!result)
            {
                return BadRequest("Failed to Update Date");

            }
            return Ok(result);
        }
    }
}
