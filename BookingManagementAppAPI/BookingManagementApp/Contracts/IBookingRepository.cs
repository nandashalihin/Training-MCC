using BookingManagementApp.Models;
using BookingManagementApp.Repositories;

namespace BookingManagementApp.Contracts
{
    // Mendefinisikan interface IBookingRepository yang merupakan turunan dari IRepository dengan tipe Booking.
    public interface IBookingRepository : IRepository<Booking>
    {
        // Deklarasi metode GetBookedBy yang akan diimplementasikan oleh kelas yang mengimplementasikan interface ini.
        // Metode ini digunakan untuk mendapatkan informasi tentang siapa yang melakukan pemesanan berdasarkan GUID karyawan.
        string GetBookedBy(Guid employeeGuid);

        // Deklarasi metode GetBookingsForDate yang akan diimplementasikan oleh kelas yang mengimplementasikan interface ini.
        // Metode ini bertujuan untuk mendapatkan daftar pemesanan (booking) untuk tanggal tertentu.
        List<Booking> GetBookingsForDate(DateTime date);

        // Deklarasi metode GetRoomByGuid yang akan diimplementasikan oleh kelas yang mengimplementasikan interface ini.
        // Metode ini digunakan untuk mendapatkan data ruangan berdasarkan GUID ruangan.
        Room GetRoomByGuid(Guid guid);
    }

}
