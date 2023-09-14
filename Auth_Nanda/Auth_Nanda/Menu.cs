using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_Nanda
{
    public class Menu
    {
        public static void DisplayMainMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("         Basic Authentication");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. Show User");
            Console.WriteLine("3. Search User");
            Console.WriteLine("4. Login User");
            Console.WriteLine("5. Exit");
            Console.WriteLine("-----------------------------------------------");
        }

        public static void DisplayUserMenu()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("         MENU GANJIL GENAP");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("1. Cek Ganjil Genap");
            Console.WriteLine("2. Print Ganjil Genap (dengan Limit)");
            Console.WriteLine("3. Logout");
            Console.WriteLine("-----------------------------------------------");
        }

        public static void DisplayEditDeleteMenu()
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("1. Edit");
            Console.WriteLine("2. Delete");
            Console.WriteLine("3. Back");
            Console.WriteLine("-----------------------------------------------");
        }

        public static void DisplaySearchResults(List<User> foundUsers)
        {
            if (foundUsers.Count > 0)
            {
                Console.WriteLine("Hasil Pencarian:");
                foreach (var user in foundUsers)
                {
                    Console.WriteLine($"ID: {user.Id}");
                    Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                    Console.WriteLine($"Username: {user.UserName}");
                    Console.WriteLine($"Password: {user.Password}");
                    Console.WriteLine("-----------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("Pengguna tidak ditemukan.");
            }
        }
    }
}
