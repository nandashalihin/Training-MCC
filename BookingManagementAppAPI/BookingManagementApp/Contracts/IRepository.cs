using System;  // Mengimpor namespace 'System' untuk menggunakan tipe 'Guid'
using System.Collections.Generic;  // Mengimpor namespace 'System.Collections.Generic' untuk menggunakan koleksi generik

namespace BookingManagementApp.Contracts
{
    // Membuat sebuah generic interface bernama 'IRepository<T>' yang digunakan sebagai kontrak dasar (base contract) untuk mengakses data.
    public interface IRepository<T>
    {
        // Mendefinisikan metode 'GetAll' yang mengembalikan IEnumerable<T>.
        // Metode ini digunakan untuk mendapatkan semua entitas dari repositori data.
        IEnumerable<T> GetAll();

        // Mendefinisikan metode 'GetByGuid' yang mengambil satu entitas berdasarkan GUID (Global Unique Identifier).
        // Metode ini mengembalikan 'T' atau null jika tidak ada yang cocok.
        T? GetByGuid(Guid guid);

        // Mendefinisikan metode 'Create' yang digunakan untuk membuat entitas baru dalam repositori data.
        // Metode ini menerima entitas 'T' sebagai parameter dan mengembalikan 'T' yang baru dibuat.
        T? Create(T entity);

        // Mendefinisikan metode 'Update' yang digunakan untuk memperbarui entitas yang ada dalam repositori data.
        // Metode ini menerima entitas 'T' sebagai parameter dan mengembalikan 'bool' yang menunjukkan apakah pembaruan berhasil.
        bool Update(T entity);

        // Mendefinisikan metode 'Delete' yang digunakan untuk menghapus entitas dari repositori data.
        // Metode ini menerima entitas 'T' sebagai parameter dan mengembalikan 'bool' yang menunjukkan apakah penghapusan berhasil.
        bool Delete(T entity);
    }
}
