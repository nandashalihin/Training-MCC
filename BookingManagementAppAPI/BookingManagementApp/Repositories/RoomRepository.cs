using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private readonly BookingManagementDbContext _context;

        public RoomRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAll()
        {
            return _context.Set<Room>().ToList();
        }

        public Room? GetByGuid(Guid guid)
        {
            return _context.Set<Room>().Find(guid);
        }

        public Room? Create(Room room)
        {
            try
            {
                _context.Set<Room>().Add(room);
                _context.SaveChanges();
                return room;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Room room)
        {
            try
            {
                _context.Set<Room>().Update(room);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Room room)
        {
            try
            {
                _context.Set<Room>().Remove(room);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
