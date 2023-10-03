using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class UniversityRepository : GeneralRepository<University>, IRepository<University>
    {
        public UniversityRepository(BookingManagementDbContext context) : base(context) { }
    }
}
