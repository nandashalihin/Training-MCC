using BookingManagementApp.Models;  // Mengimpor namespace 'BookingManagementApp.Models'

namespace BookingManagementApp.Contracts
{
    // Membuat sebuah interface bernama 'IEmployeeRepository'
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // Mendefinisikan metode 'GetLastNik' yang akan diimplementasikan oleh kelas lain yang mengimplementasikan interface ini.
        // Metode ini mengembalikan string dan tidak memiliki parameter.
        string? GetLastNik();
        Employee GetGuidByEmail(string email);
    }
}
