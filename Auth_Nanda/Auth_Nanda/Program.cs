using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Program
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }

    static void Menu1()
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
    static void Menu2()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("         MENU GANJIL GENAP");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("1. Cek Ganjil Genap");
        Console.WriteLine("2. Print Ganjil Genap (dengan Limit)");
        Console.WriteLine("3. Logout");
        Console.WriteLine("-----------------------------------------------");
    }

    static void Menu3()
    {

        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("1. Edit");
        Console.WriteLine("2. Delete");
        Console.WriteLine("3. Back");
        Console.WriteLine("-----------------------------------------------");
    }

    static bool IsPasswordValid(string password)
    {

        if (password.Length < 8)
        {
            return false;
        }

        if (!Regex.IsMatch(password, @"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)"))
        {
            return false;
        }

        return true;
    }


    static void PrintEvenOdd(int limit, string choice)
    {
        if (limit < 1)
        {
            Console.WriteLine("Batas harus minimal 1.");
            return;
        }

        string result = string.Join(", ",
            Enumerable.Range(1, limit)
                .Where(i => (choice == "Genap" && i % 2 == 0) || (choice == "Ganjil" && i % 2 != 0))
        );

        Console.WriteLine($"Print bilangan 1 - {limit} : {result}");
    }

    static string EvenOddCheck(int input)
    {
        if (input < 1)
        {
            return "Invalid Input!!!";
        }

        return input % 2 == 0 ? "Genap" : "Ganjil";
    }

    static void CreateUserAndAddToCollection(List<Program> users, string firstName, string lastName, string password)
    {
        string UserName = firstName.Substring(0, 2) + lastName.Substring(0, 2);
        int Id = users.Count + 1;
        ;
        Program newUser = new Program
        {
            Id = Id,    
            FirstName = firstName,
            LastName = lastName,
            Password = password,
            UserName = UserName+Id

        };

        users.Add(newUser);
    }


    static void EditUser(List<Program> users, int userId)
    {
        Program userToEdit = users.FirstOrDefault(user => user.Id == userId);

        if (userToEdit != null)
        {
            Console.Write("First Name: ");
            userToEdit.FirstName = Console.ReadLine();
            Console.Write("Last Name: ");
            userToEdit.LastName = Console.ReadLine();

            string newPassword;
            bool isPasswordValid;
            do
            {
                Console.Write("Password: ");
                newPassword = Console.ReadLine();
                isPasswordValid = IsPasswordValid(newPassword);
                if (!isPasswordValid)
                {
                    Console.WriteLine("Password is invalid. It must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.");
                }
            } while (!isPasswordValid);

            userToEdit.Password = newPassword;
            Console.WriteLine("User berhasil diedit.");
        }
        else
        {
            Console.WriteLine("User tidak ditemukan.");
        }
    }

    static void DeleteUser(List<Program> users, int userId)
    {
        Program userToDelete = users.FirstOrDefault(user => user.Id == userId);

        if (userToDelete != null)
        {
            users.Remove(userToDelete);
            Console.WriteLine("User berhasil dihapus.");
        }
        else
        {
            Console.WriteLine("User tidak ditemukan.");
        }
    }



    static void SearchUser(List<Program> users)
    {
        Console.Write("Masukkan nama pengguna yang ingin Anda cari: ");
        string searchName = Console.ReadLine();

        var foundUsers = users.Where(user =>
            user.FirstName.Contains(searchName, StringComparison.OrdinalIgnoreCase) ||
            user.LastName.Contains(searchName, StringComparison.OrdinalIgnoreCase)
        ).ToList();

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

    static void UserLogin(List<Program> users)
    {
        Console.Write("Username: ");
        string lUsername = Console.ReadLine();
        Console.Write("Username: ");
        string lPassword = Console.ReadLine();



        var foundUsers = users.Where(user =>
            user.UserName.Contains(lUsername, StringComparison.OrdinalIgnoreCase) &&
            user.Password.Contains(lPassword, StringComparison.OrdinalIgnoreCase)
        ).ToList();
        if (foundUsers.Count != 1)
        {

            Console.WriteLine("Data User Tidak Ditemukan");

        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Login Sebagai : " + lUsername);
            Console.WriteLine();
            while (true)
            {
                Menu2();
                Console.Write("Pilihan : ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Pilihan tidak valid!");
                    continue;
                }

                if (choice == 1)
                {
                    Console.Write("Masukkan Bilangan yang ingin di cek : ");
                    int num;
                    if (!int.TryParse(Console.ReadLine(), out num))
                    {
                        Console.WriteLine("Input tidak valid!");
                        continue;
                    }

                    string result = EvenOddCheck(num);
                    Console.WriteLine(result);
                }
                else if (choice == 2)
                {
                    Console.Write("Pilih (Ganjil/Genap) : ");
                    string choice2 = Console.ReadLine();
                    Console.Write("Masukkan limit : ");
                    int limit;
                    if (!int.TryParse(Console.ReadLine(), out limit))
                    {
                        Console.WriteLine("Limit tidak valid!");
                        continue;
                    }

                    if (choice2 != "Genap" && choice2 != "Ganjil")
                    {
                        Console.WriteLine("Input pilihan tidak valid!!!");
                        continue;
                    }

                    PrintEvenOdd(limit, choice2);
                }
                else if (choice == 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid!");
                }
            }
        }


    }
    public static void Main(string[] args)
    {
        List<Program> users = new List<Program>();
        while (true)
        {
            Menu1();
            Console.Write("Pilihan : ");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Pilihan tidak valid!");
                continue;
            }

            if (choice == 1)
            {
                Console.Write("First Name : ");
                string firstName = Console.ReadLine();
                Console.Write("Last Name : ");
                string lastName = Console.ReadLine();

                
                string password;
                bool isPasswordValid;
                do
                {
                    Console.Write("Password: ");
                    password = Console.ReadLine();
                    isPasswordValid = IsPasswordValid(password);
                    if (!isPasswordValid)
                    {
                        Console.WriteLine("Password is invalid. It must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.");
                    }
                } while (!IsPasswordValid(password));

                CreateUserAndAddToCollection(users, firstName, lastName, password);
                Console.WriteLine("User berhasil ditambahkan.");
                Console.ReadLine();
            }

            else if (choice == 2)
            {
                foreach (Program user in users)
                {
                    Console.WriteLine();
                    Console.WriteLine("======= Daftar User =======");
                    Console.WriteLine($"ID: {user.Id}");
                    Console.WriteLine($"Name: {user.FirstName}  {user.LastName}");
                    Console.WriteLine($"Username: {user.UserName}");
                    Console.WriteLine($"Password: {user.Password}");
                    Console.WriteLine("-----------------------------------------------");
                }
                Console.WriteLine("Berikut Data pengguna. Pilih opsi selanjutnya:");
                Console.WriteLine();
                Menu3();
                while (true)
                {
                    Console.Write("Pilihan : ");
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Pilihan tidak valid!");
                        continue;
                    }

                    if (choice == 1)
                    {
                        Console.Write("Masukkan ID pengguna yang ingin diedit: ");
                        if (int.TryParse(Console.ReadLine(), out int editUserId))
                        {
                            EditUser(users, editUserId);
                        }
                        else
                        {
                            Console.WriteLine("ID pengguna tidak valid!");
                        }
                    }
                    else if (choice == 2)
                    {
                        Console.Write("Masukkan ID pengguna yang ingin dihapus: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteUserId))
                        {
                            DeleteUser(users, deleteUserId);
                        }
                        else
                        {
                            Console.WriteLine("ID pengguna tidak valid!");
                        }
                    }
                    else if (choice == 3)
                    {

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Pilihan tidak valid!");
                    }
                }
            }

            else if (choice == 3)
            {

                SearchUser(users);
            }
            else if (choice == 4)
            {
                UserLogin(users);

            }
            else if (choice == 5)
            {

                break;
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid!");
            }
        }
    }
}
