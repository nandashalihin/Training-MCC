using HR_Management.Models;
using System;
using System.Collections.Generic;

namespace HR_Management.Controllers
{
    public class DepartmentController
    {
        private readonly Department departmentModel = new Department();

        public void DisplayAllDepartments()
        {
            var departments = departmentModel.GetAll();
            var departmentData = new List<Dictionary<string, string>>();

            foreach (var department in departments)
            {
                var departmentInfo = new Dictionary<string, string>
        {
            { "Id", department.Id.ToString() },
            { "Name", department.Name },
            { "LocationId", department.LocationId.ToString() },
            { "ManagerId", department.ManagerId.ToString() }
        };
                departmentData.Add(departmentInfo);
            }

            ConsoleHelper.DisplayTable(departmentData);
        }


        public void AddDepartment()
        {
            Console.Write("Enter Department Name: ");
            var name = Console.ReadLine();

            Console.Write("Enter Location ID: ");
            if (int.TryParse(Console.ReadLine(), out int locationId))
            {
                Console.Write("Enter Manager ID: ");
                if (int.TryParse(Console.ReadLine(), out int managerId))
                {
                    var newDepartment = new Department
                    {
                        Name = name,
                        LocationId = locationId,
                        ManagerId = managerId
                    };

                    var result = departmentModel.Insert(newDepartment);
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("Invalid Manager ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Location ID.");
            }
        }

        public void UpdateDepartment()
        {
            Console.Write("Enter Department ID to Update: ");
            if (int.TryParse(Console.ReadLine(), out int departmentId))
            {
                var existingDepartment = departmentModel.GetById(departmentId);

                if (existingDepartment != null)
                {
                    Console.Write("Enter New Department Name: ");
                    var newName = Console.ReadLine();

                    Console.Write("Enter New Location ID: ");
                    if (int.TryParse(Console.ReadLine(), out int newLocationId))
                    {
                        Console.Write("Enter New Manager ID: ");
                        if (int.TryParse(Console.ReadLine(), out int newManagerId))
                        {
                            existingDepartment.Name = newName;
                            existingDepartment.LocationId = newLocationId;
                            existingDepartment.ManagerId = newManagerId;

                            var result = departmentModel.Update(existingDepartment);
                            Console.WriteLine(result);
                        }
                        else
                        {
                            Console.WriteLine("Invalid New Manager ID.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid New Location ID.");
                    }
                }
                else
                {
                    Console.WriteLine($"Department with ID {departmentId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Department ID.");
            }
        }

        public void DeleteDepartment()
        {
            Console.Write("Enter Department ID to Delete: ");
            if (int.TryParse(Console.ReadLine(), out int departmentId))
            {
                var result = departmentModel.Delete(departmentId);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Invalid Department ID.");
            }
        }

        public void RunDepartmentMenu()
        {
            while (true)
            {
                Console.WriteLine("Menu Department:");
                Console.WriteLine("1. Tampilkan semua data Department");
                Console.WriteLine("2. Tambahkan Department");
                Console.WriteLine("3. Perbarui Department");
                Console.WriteLine("4. Hapus Department");
                Console.WriteLine("5. Kembali ke Menu Utama");

                Console.Write("Pilih menu (1-5): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllDepartments();
                        break;
                    case "2":
                        AddDepartment();
                        break;
                    case "3":
                        UpdateDepartment();
                        break;
                    case "4":
                        DeleteDepartment();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            }
        }
    }
}
