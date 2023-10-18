using BookingManagementApp.DTOs;
using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Handlers;
using Client.Contracts;

namespace Client.Repositories
{
    public class UniversityRepository : GeneralRepository<University, Guid>, IUniversityRepository
    {
        public UniversityRepository(string request = "University/") : base(request)
        {

        }

       

        
    }
}
