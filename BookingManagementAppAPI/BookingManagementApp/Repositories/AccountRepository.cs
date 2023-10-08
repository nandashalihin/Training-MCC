using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;


namespace BookingManagementApp.Repositories
{
    public class AccountRepository : GeneralRepository<Account>, IAccountRepository
    {
        public AccountRepository(BookingManagementDbContext context) : base(context) { }

        public string? GetPasswordByGuid(Guid guid)
        {
            return _context.Accounts.Where(e => e.Guid == guid)
                .Select(e => e.Password).FirstOrDefault();

        }

       
    }
}
