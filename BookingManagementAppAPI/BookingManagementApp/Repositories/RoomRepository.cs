using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class RoomRepository : GeneralRepository<Room>, IRepository<Room>
    {
        public RoomRepository(BookingManagementDbContext context) : base(context) { }
    }
}
