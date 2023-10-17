using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Handlers;
using Client.Contracts;

namespace Client.Repositories
{
    public class EmployeeRepository : GeneralRepository<EmployeeDto, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(string request = "Employee/") : base(request)
        {

        }

       

        
    }
}
