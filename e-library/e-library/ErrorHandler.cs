using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_library
{
    class ErrorHandler
    {
        public static void HandleError(Exception ex)
        {
            Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
        }

        public static void HandleInvalidInputError()
        {
            Console.WriteLine("Input tidak valid. Masukkan angka yang valid.");
        }
    }
}
