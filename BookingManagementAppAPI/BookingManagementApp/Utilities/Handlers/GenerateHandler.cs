namespace BookingManagementApp.Utilities.Handlers
{
    // Membuat kelas publik 'GenerateHandler'
    public class GenerateHandler
    {
        // Mendefinisikan metode statis 'GenerateNik' yang digunakan untuk menghasilkan Nomor Induk Karyawan (NIK) baru berdasarkan NIK terakhir yang diberikan.
        public static string GenerateNik(string lastNik)
        {
            //Jika lastNik bernilai empty string maka akan mengembalikan nilai "111111"
            if (lastNik == "") return "111111";

            //Jika lastNik bukan empy string maka akan mengkonversinya ke NIK dan nilainya ditambah 1
            int nik = Convert.ToInt32(lastNik);
            nik += 1;

            //mengembalikan nilai berupa NIK yang dikonversi ke string
            return nik.ToString();
        }
    }
}
