using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        private readonly BookingManagementDbContext _context;

        public AccountRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Set<Account>().Include(a => a.Employee).ToList();
        }

        public Account? GetByGuid(Guid guid)
        {
            return _context.Set<Account>().Include(a => a.Employee).FirstOrDefault(a => a.Guid == guid);
        }

        public Account? Create(Account account)
        {
            try
            {
                _context.Set<Account>().Add(account);
                _context.SaveChanges();
                return account;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(Account account)
        {
            try
            {
                _context.Set<Account>().Update(account);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Account account)
        {
            try
            {
                _context.Set<Account>().Remove(account);
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
