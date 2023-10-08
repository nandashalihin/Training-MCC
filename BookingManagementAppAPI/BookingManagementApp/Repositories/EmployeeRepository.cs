using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingManagementApp.Repositories
{
    // Mendefinisikan kelas EmployeeRepository, yang merupakan turunan dari GeneralRepository<Employee> dan mengimplementasikan IEmployeeRepository.
    public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
    {
        // Konstruktor kelas EmployeeRepository dengan parameter BookingManagementDbContext context.
        // Mewarisi konstruktor dari kelas GeneralRepository<Employee> dan mengirimkan konteks (context) ke kelas induk.
        public EmployeeRepository(BookingManagementDbContext context) : base(context) { }

        // Metode GetLastNik digunakan untuk mendapatkan nomor induk karyawan (NIK) terakhir.
        public string GetLastNik()
        {
            // Mengambil data karyawan (employee) dari tabel Employees, mengurutkannya berdasarkan NIK secara descending (tertinggi ke terendah),
            // dan mengambil data pertama (tertinggi).
            Employee? employee = _context.Employees.OrderByDescending(e => e.Nik).FirstOrDefault();

            // Mengembalikan NIK karyawan terakhir jika ada, atau string kosong ("") jika tidak ada data karyawan.
            return employee?.Nik ?? "";
        }

        // Metode GetGuidByEmail digunakan untuk mendapatkan data karyawan berdasarkan alamat email.
        public Employee GetGuidByEmail(string email)
        {
            // Mengambil entitas (entity) Employee yang memiliki alamat email yang sesuai.
            var entity = _context.Set<Employee>().FirstOrDefault(e => e.Email == email);

            // Menghapus semua entitas yang telah dilacak oleh ChangeTracker.
            _context.ChangeTracker.Clear();

            // Mengembalikan entitas karyawan yang sesuai dengan alamat email.
            return entity;
        }

        // Metode GetEmployeeNik digunakan untuk mendapatkan NIK karyawan berdasarkan GUID karyawan.
        public string GetEmployeeNik(Guid employeeGuid)
        {
            // Mencari karyawan berdasarkan GUID yang diberikan.
            var employee = _context.Employees.FirstOrDefault(e => e.Guid == employeeGuid);

            // Mengembalikan NIK karyawan jika ditemukan, atau null jika tidak ditemukan.
            return employee?.Nik;
        }

        // Metode GetEmployeeName digunakan untuk mendapatkan nama lengkap karyawan berdasarkan GUID karyawan.
        public string GetEmployeeName(Guid employeeGuid)
        {
            // Mencari karyawan berdasarkan GUID yang diberikan.
            var employee = _context.Employees.FirstOrDefault(e => e.Guid == employeeGuid);

            // Mengembalikan nama lengkap karyawan jika ditemukan, atau null jika tidak ditemukan.
            return $"{employee?.FirstName} {employee?.LastName}";
        }
    }

}
