using BookingManagementApp.Models;  

namespace BookingManagementApp.Contracts
{

    // Mendefinisikan interface IEmployeeRepository yang merupakan turunan dari IRepository dengan tipe Employee.
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // Deklarasi metode GetLastNik yang akan diimplementasikan oleh kelas yang mengimplementasikan interface ini.
        // Metode ini bertujuan untuk mendapatkan nomor induk karyawan (NIK) terakhir.
        string? GetLastNik();

        // Deklarasi metode GetGuidByEmail yang akan diimplementasikan oleh kelas yang mengimplementasikan interface ini.
        // Metode ini digunakan untuk mendapatkan data karyawan berdasarkan alamat email.
        Employee GetGuidByEmail(string email);

        // Deklarasi metode GetEmployeeNik yang akan diimplementasikan oleh kelas yang mengimplementasikan interface ini.
        // Metode ini bertujuan untuk mendapatkan NIK (Nomor Induk Karyawan) karyawan berdasarkan GUID karyawan.
        string GetEmployeeNik(Guid employeeGuid);

        // Deklarasi metode GetEmployeeName yang akan diimplementasikan oleh kelas yang mengimplementasikan interface ini.
        // Metode ini digunakan untuk mendapatkan nama lengkap karyawan berdasarkan GUID karyawan.
        string GetEmployeeName(Guid employeeGuid);
    }

}
