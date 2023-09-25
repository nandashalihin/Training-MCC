﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_employees")]
    public class Employee : BaseEntity
    {
        [Column(name: "nik")]
        public char Nik { get; set; }

        [Column(name: "first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Column(name: "last_name", TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }

        [Column(name: "birth_date")]
        public DateTime BirthDate { get; set; }

        [Column(name: "gender")]
        public int Gender { get; set; }

        [Column(name: "hiring_date")]
        public DateTime HiringDate { get; set; }

        [Column(name: "email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column(name: "phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }
    }
}
