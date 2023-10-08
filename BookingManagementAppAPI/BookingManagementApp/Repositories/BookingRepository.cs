using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    // Mendefinisikan kelas BookingRepository, yang merupakan turunan dari GeneralRepository<Booking> dan mengimplementasikan IBookingRepository.
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        // Konstruktor kelas BookingRepository dengan parameter BookingManagementDbContext context.
        // Mewarisi konstruktor dari kelas GeneralRepository<Booking> dan mengirimkan konteks (context) ke kelas induk.
        public BookingRepository(BookingManagementDbContext context) : base(context) { }

        // Metode GetBookedBy digunakan untuk mendapatkan nama karyawan yang melakukan pemesanan berdasarkan GUID karyawan.
        public string GetBookedBy(Guid employeeGuid)
        {
            // Mencari karyawan berdasarkan GUID yang diberikan.
            var employee = _context.Employees.FirstOrDefault(e => e.Guid == employeeGuid);

            // Jika karyawan ditemukan, maka mengembalikan nama lengkap karyawan (FirstName dan LastName).
            // Jika karyawan tidak ditemukan, mengembalikan "Unknown".
            if (employee != null)
            {
                return $"{employee.FirstName} {employee.LastName}";
            }
            return "Unknown";
        }

        // Metode GetBookingsForDate digunakan untuk mendapatkan daftar pemesanan (booking) untuk tanggal tertentu.
        public List<Booking> GetBookingsForDate(DateTime date)
        {
            // Mengambil daftar pemesanan (Bookings) yang memiliki tanggal mulai (StartDate) sama dengan tanggal yang diberikan
            // dan tanggal selesai (EndDate) lebih besar atau sama dengan tanggal yang diberikan.
            return _context.Bookings
                .Where(b => b.StartDate.Date == date.Date && b.EndDate.Date >= date.Date)
                .ToList();
        }

        // Metode GetRoomByGuid digunakan untuk mendapatkan data ruangan berdasarkan GUID ruangan.
        public Room GetRoomByGuid(Guid Roomguid)
        {
            // Mencari entitas (entity) Room berdasarkan GUID yang diberikan.
            var entity = _context.Set<Room>().FirstOrDefault(e => e.Guid == Roomguid);

            // Menghapus semua entitas yang telah dilacak oleh ChangeTracker.
            _context.ChangeTracker.Clear();

            // Mengembalikan entitas ruangan yang sesuai dengan GUID yang diberikan.
            return entity;
        }
    }

}
