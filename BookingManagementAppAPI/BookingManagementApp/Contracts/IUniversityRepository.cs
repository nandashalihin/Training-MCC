using BookingManagementApp.Models;  // Mengimpor namespace 'BookingManagementApp.Models'

namespace BookingManagementApp.Contracts
{
    public interface IUniversityRepository : IRepository<University>
    {
        Guid GetGuidByCode(string email);
    }
}
