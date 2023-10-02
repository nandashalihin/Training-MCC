using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private readonly BookingManagementDbContext _context;

        public BookingRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Set<Booking>().Include(b => b.Room).Include(b => b.Employee).ToList();
        }

        public Booking? GetByGuid(Guid guid)
        {
            return _context.Set<Booking>().Include(b => b.Room).Include(b => b.Employee).FirstOrDefault(b => b.Guid == guid);
        }

        public Booking? Create(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Add(booking);
                _context.SaveChanges();
                return booking;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Update(booking);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Booking booking)
        {
            try
            {
                _context.Set<Booking>().Remove(booking);
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
