using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IRoleRepository : IRepository<Role>
    {
        Guid? GetDefaultRoleGuid();
    }
}
