using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    // Definisi kelas `RoomRepository` sebagai turunan dari `GeneralRepository<Room>` dan mengimplementasikan `IRoomRepository`.
    public class RoomRepository : GeneralRepository<Room>, IRoomRepository
    {
        // Konstruktor kelas `RoomRepository` dengan parameter `BookingManagementDbContext context`.
        public RoomRepository(BookingManagementDbContext context) : base(context) { }

        // Metode `GetRoomName` untuk mengambil nama ruangan berdasarkan GUID yang diberikan.
        public string GetRoomName(Guid roomGuid)
        {
            // Mencari ruangan dengan GUID yang sesuai di dalam konteks data (_context).
            var room = _context.Rooms.FirstOrDefault(r => r.Guid == roomGuid);

            // Mengembalikan nama ruangan jika ditemukan, atau null jika tidak ditemukan.
            return room?.Name;
        }
    }
}
