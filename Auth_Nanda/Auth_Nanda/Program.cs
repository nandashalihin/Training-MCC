using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Auth_Nanda;
public class Program
{
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

    static void Main(string[] args)
    {
        UserManager userManager = new UserManager();

        while (true)
        {
            Menu.DisplayMainMenu();
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
                Console.Write("Password: ");
                string password = Console.ReadLine();
                userManager.CreateUser(firstName, lastName, password);
            }
            else if (choice == 2)
            {
                List<User> allUsers = userManager.GetUsers();
                foreach (User user in allUsers)
                {
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine($"ID: {user.Id}");
                    Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                    Console.WriteLine($"Username: {user.UserName}");
                    Console.WriteLine($"Password: {user.Password}");
                    Console.WriteLine("-----------------------------------------------");
                }

                Menu.DisplayEditDeleteMenu();
                Console.Write("Pilihan : ");
                int editChoice;
                if (!int.TryParse(Console.ReadLine(), out editChoice))
                {
                    Console.WriteLine("Pilihan tidak valid!");
                    continue;
                }

                if (editChoice == 1)
                {
                    Console.Write("Masukkan ID pengguna yang ingin diedit: ");
                    if (int.TryParse(Console.ReadLine(), out int editUserId))
                    {
                        User userToEdit = userManager.GetUserById(editUserId);
                        if (userToEdit != null)
                        {
                            Console.Write("First Name : ");
                            string newFirstName = Console.ReadLine();
                            Console.Write("Last Name : ");
                            string newLastName = Console.ReadLine();
                            Console.Write("Password: ");
                            string newPassword = Console.ReadLine();
                            userManager.EditUser(editUserId, newFirstName, newLastName, newPassword);
                        }
                        else
                        {
                            Console.WriteLine("ID pengguna tidak valid!");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID pengguna tidak valid!");
                    }
                }
                else if (editChoice == 2)
                {
                    Console.Write("Masukkan ID pengguna yang ingin dihapus: ");
                    if (int.TryParse(Console.ReadLine(), out int deleteUserId))
                    {
                        userManager.DeleteUser(deleteUserId);
                    }
                    else
                    {
                        Console.WriteLine("ID pengguna tidak valid!");
                    }
                }
                else if (editChoice == 3)
                {
                    Menu.DisplayMainMenu();
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid!");
                }
            }
            else if (choice == 3)
            {
                Console.Write("Masukkan nama pengguna yang ingin Anda cari: ");
                string searchName = Console.ReadLine();
                List<User> foundUsers = userManager.GetUsers().Where(user =>
                    user.FirstName.Contains(searchName, StringComparison.OrdinalIgnoreCase) ||
                    user.LastName.Contains(searchName, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                Menu.DisplaySearchResults(foundUsers);
            }
            else if (choice == 4)
            {
                // Logika login pengguna
                Console.Write("Username: ");
                string lUsername = Console.ReadLine();
                Console.Write("Password: ");
                string lPassword = Console.ReadLine();

                User loggedInUser = userManager.GetUsers().FirstOrDefault(user =>
                    user.UserName.Equals(lUsername, StringComparison.OrdinalIgnoreCase) &&
                    user.Password.Equals(lPassword)
                );

                if (loggedInUser != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("Login Sebagai : " + lUsername);
                    Console.WriteLine();
                    while (true)
                    {
                        Menu.DisplayUserMenu();
                        Console.Write("Pilihan : ");
                        int userChoice;
                        if (!int.TryParse(Console.ReadLine(), out userChoice))
                        {
                            Console.WriteLine("Pilihan tidak valid!");
                            continue;
                        }

                        if (userChoice == 1)
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
                        else if (userChoice == 2)
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
                        else if (userChoice == 3)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Pilihan tidak valid!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Login gagal. Username atau password salah.");
                }
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
