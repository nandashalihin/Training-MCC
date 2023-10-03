using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class RoleRepository : GeneralRepository<Role>, IRepository<Role>
    {
        public RoleRepository(BookingManagementDbContext context) : base(context) { }
    }
}
