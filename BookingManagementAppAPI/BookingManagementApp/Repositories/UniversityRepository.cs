using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
    {
        public UniversityRepository(BookingManagementDbContext context) : base(context) { }

        public Guid GetGuidByCode(string code)
        {
            Guid guid = _context.Universities.Where(e => e.Code == code)
                .Select(e => e.Guid).FirstOrDefault();

            return guid;
        }
    }
}
