using BookingManagementApp.DTOs;
using BookingManagementApp.Models;

namespace Client.Contracts
{
    public interface IEmployeeRepository : IRepository<EmployeeDto, Guid>
    {


    }
}
