using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class RoleRepository : GeneralRepository<Role>, IRoleRepository
    {
        public RoleRepository(BookingManagementDbContext context) : base(context) { }

        public Guid? GetDefaultRoleGuid()
        {
            return _context.Set<Role>().FirstOrDefault(r => r.Name == "user")?.Guid;
        }
    }
}
