using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IRepository<Account>
    {
        public AccountRepository(BookingManagementDbContext context) : base(context) { }
    }
}
