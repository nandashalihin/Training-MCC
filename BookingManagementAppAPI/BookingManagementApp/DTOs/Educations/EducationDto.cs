﻿using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs
{
    public class EducationDto
    {
        public Guid Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }

        // DTO untuk Get Education
        public static explicit operator EducationDto(Education education)
        {
            return new EducationDto
            {
                Guid = education.Guid,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.Gpa,
                UniversityGuid = education.UniversityGuid
            };
        }

        // DTO untuk Update Education
        public static implicit operator Education(EducationDto educationDto)
        {
            return new Education
            {
                Guid = educationDto.Guid,
                Major = educationDto.Major,
                Degree = educationDto.Degree,
                Gpa = educationDto.Gpa,
                UniversityGuid = educationDto.UniversityGuid,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
