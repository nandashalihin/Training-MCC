using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingManagementDbContext context) : base(context) { }
        public string GetLastNik()
        {
            Employee? employee = _context.Employees.OrderByDescending(e => e.Nik).FirstOrDefault();

            return employee?.Nik ?? "";
        }
        public Employee GetGuidByEmail(string email)
        {
            var entity = _context.Set<Employee>().FirstOrDefault(e => e.Email == email);
            _context.ChangeTracker.Clear();
            return entity;

        }
    }
}
