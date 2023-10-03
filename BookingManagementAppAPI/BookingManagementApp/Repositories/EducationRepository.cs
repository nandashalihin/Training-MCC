using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Repositories
{
    public class EducationRepository : GeneralRepository<Education>, IRepository<Education>
    {
        public EducationRepository(BookingManagementDbContext context) : base(context) { }
    }
}
