using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Repositories
{
    public class AccountRoleRepository : IRepository<AccountRole>
    {
        private readonly BookingManagementDbContext _context;

        public AccountRoleRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>().Include(ar => ar.Role).Include(ar => ar.Account).ToList();
        }

        public AccountRole? GetByGuid(Guid guid)
        {
            return _context.Set<AccountRole>().Include(ar => ar.Role).Include(ar => ar.Account).FirstOrDefault(ar => ar.Guid == guid);
        }

        public AccountRole? Create(AccountRole accountRole)
        {
            try
            {
                _context.Set<AccountRole>().Add(accountRole);
                _context.SaveChanges();
                return accountRole;
            }
            catch
            {
                return null;
            }
        }

        public bool Update(AccountRole accountRole)
        {
            try
            {
                _context.Set<AccountRole>().Update(accountRole);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(AccountRole accountRole)
        {
            try
            {
                _context.Set<AccountRole>().Remove(accountRole);
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
