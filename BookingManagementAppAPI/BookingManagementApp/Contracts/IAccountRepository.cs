using BookingManagementApp.Data;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Contracts
{
    public interface IAccountRepository : IRepository<Account>
    {
        /*object GetByGuidi(object guid);*/
        string? GetPasswordByGuid(Guid guid);
    }
}
