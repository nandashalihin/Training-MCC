using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Repositories
{
    public class EducationRepository : IRepository<Education>
    {
        private readonly BookingManagementDbContext _context;

        public EducationRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Education> GetAll()
        {
            return _context.Set<Education>().Include(e => e.University).ToList();
        }

        public Education? GetByGuid(Guid guid)
        {
            return _context.Set<Education>().Include(e => e.University).FirstOrDefault(e => e.Guid == guid);
        }

        public Education? Create(Education education)
        {
            try
            {
                _context.Set<Education>().Add(education);
                _context.SaveChanges();
                return education;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Education education)
        {
            try
            {
                _context.Set<Education>().Update(education);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Education education)
        {
            try
            {
                _context.Set<Education>().Remove(education);
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
