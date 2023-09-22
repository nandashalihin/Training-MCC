using HR_Management.Controllers;
using System;

namespace HR_Management
{
    class Program
    {
        static void Main()
        {
            RegionController regionController = new RegionController();
            JobController jobController = new JobController();

            while (true)
            {
                Console.WriteLine("Menu Utama:");
                Console.WriteLine("1. Kelola Region");
                Console.WriteLine("2. Kelola Job");
                Console.WriteLine("3. Keluar");

                Console.Write("Pilih menu (1-3): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        regionController.RunRegionMenu();
                        break;
                    case "2":
                        jobController.RunJobMenu();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            }
        }
    }
}
