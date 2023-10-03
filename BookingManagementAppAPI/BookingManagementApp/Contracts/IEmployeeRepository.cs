using BookingManagementApp.Models;
using BookingManagementApp.Repositories;

namespace BookingManagementApp.Contracts
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        string GetLastNik();
    }
}
