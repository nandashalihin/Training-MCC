using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    // Mendefinisikan interface IRoomRepository yang merupakan turunan dari IRepository dengan tipe Room.
    public interface IRoomRepository : IRepository<Room>
    {
        // Deklarasi metode GetRoomName yang akan diimplementasikan oleh kelas yang mengimplementasikan interface ini.
        // Metode ini digunakan untuk mendapatkan nama ruangan berdasarkan GUID ruangan.
        string GetRoomName(Guid roomGuid);
    }

}
