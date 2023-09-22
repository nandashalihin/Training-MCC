using HR_Management.Models;
using System;
using System.Collections.Generic;

namespace HR_Management.Controllers
{
    public class RegionController
    {
        private readonly Region regionModel = new Region();

        public void DisplayAllRegions()
        {
            var regions = regionModel.GetAll();
            var regionData = new List<Dictionary<string, string>>();

            foreach (var region in regions)
            {
                var regionInfo = new Dictionary<string, string>
                {
                    { "Id", region.Id.ToString() },
                    { "Name", region.Name }
                };
                regionData.Add(regionInfo);
            }

            ConsoleHelper.DisplayTable(regionData);
        }

        public void AddRegion()
        {
            Console.Write("Enter Region Name: ");
            var name = Console.ReadLine();

            var result = regionModel.Insert(name);
            Console.WriteLine(result);
        }

        public void UpdateRegion()
        {
            Console.Write("Enter Region Id to Update: ");
            if (int.TryParse(Console.ReadLine(), out int regionId))
            {
                var existingRegion = regionModel.GetById(regionId);

                if (existingRegion != null)
                {
                    Console.Write("Enter New Region Name: ");
                    var newName = Console.ReadLine();

                    var result = regionModel.Update(regionId, newName);
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine($"Region with Id {regionId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Region Id.");
            }
        }

        public void DeleteRegion()
        {
            Console.Write("Enter Region Id to Delete: ");
            if (int.TryParse(Console.ReadLine(), out int regionId))
            {
                var result = regionModel.Delete(regionId);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Invalid Region Id.");
            }
        }

        public void RunRegionMenu()
        {
            while (true)
            {
                Console.WriteLine("Menu Region:");
                Console.WriteLine("1. Tampilkan semua data Region");
                Console.WriteLine("2. Tambahkan Region");
                Console.WriteLine("3. Perbarui Region");
                Console.WriteLine("4. Hapus Region");
                Console.WriteLine("5. Kembali ke Menu Utama");

                Console.Write("Pilih menu (1-5): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllRegions();
                        break;
                    case "2":
                        AddRegion();
                        break;
                    case "3":
                        UpdateRegion();
                        break;
                    case "4":
                        DeleteRegion();
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
