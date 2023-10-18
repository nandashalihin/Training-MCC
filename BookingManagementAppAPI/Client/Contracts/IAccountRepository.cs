using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Handlers;
using Client.Models;

namespace Client.Contracts
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
        Task<ResponseOKHandler<TokenDto>> Login(LoginDto login);

    }

    /*public interface IAccountRepository : IRepository<AccountDto, Guid>
    {
        Task<ResponseOKHandler<object>> Login(LoginDto login);
    }*/
}
