using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Auth_Nanda
{
    public class UserManager
    {
        private int currentId = 1;
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
        private List<User> users = new List<User>();

        public void CreateUser(string firstName, string lastName, string password)
        {
            while (true)
            {
                if (!IsPasswordValid(password))
                {
                    Console.WriteLine("Password is invalid. It must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.");
                    Console.Write("Masukkan Password yang Sesuai: ");
                    password = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }

            string baseUserName = firstName.Substring(0, 2) + lastName.Substring(0, 2);
            int Id = users.Count + 1;


            string userName = baseUserName;
            while (users.Any(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)))
            {
                userName = baseUserName + Id;
                Id++;
            }

            User newUser = new User
            {
                Id = Id,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                UserName = userName
            };

            users.Add(newUser);
            Console.WriteLine("User berhasil ditambahkan.");
        }


        public void EditUser(int userId, string newFirstName, string newLastName, string newPassword)
        {
            User userToEdit = users.FirstOrDefault(user => user.Id == userId);

            if (userToEdit != null)
            {
                while (true)
                {
                    if (!IsPasswordValid(newPassword))
                    {
                        Console.WriteLine("Password is invalid. It must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.");
                        Console.Write("Masukkan Password yang Sesuai: ");
                        newPassword = Console.ReadLine();
                    }
                    else
                    {
                        break;
                    }
                }

                string baseUserName = newFirstName.Substring(0, 2) + newLastName.Substring(0, 2);
                int Id = users.Count + 1;


                string userName = baseUserName;
                while (users.Any(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && user.Id != userId))
                {
                    userName = baseUserName + Id;
                    Id++;
                }

                userToEdit.FirstName = newFirstName;
                userToEdit.LastName = newLastName;
                userToEdit.Password = newPassword;
                userToEdit.UserName = userName;
                Console.WriteLine("User berhasil diedit.");
            }
            else
            {
                Console.WriteLine("User tidak ditemukan.");
            }
        }


        public void DeleteUser(int userId)
        {
            User userToDelete = users.FirstOrDefault(user => user.Id == userId);

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

        public List<User> GetUsers()
        {
            return users;
        }

        public User GetUserById(int userId)
        {
            return users.FirstOrDefault(user => user.Id == userId);
        }
    }
}
