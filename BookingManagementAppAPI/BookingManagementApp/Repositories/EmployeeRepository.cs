using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Repositories
{
    public class EmployeeRepository : GeneralRepository<Employee>, IRepository<Employee>
    {
        public EmployeeRepository(BookingManagementDbContext context) : base(context) { }
    }
}
