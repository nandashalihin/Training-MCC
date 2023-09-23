using HR_Management.Controllers;

class Program
{
    static void Main()
    {
        RegionController regionController = new RegionController();
        JobController jobController = new JobController();
        DepartmentController departmentController = new DepartmentController(); // Tambahkan DepartmentController

        while (true)
        {
            Console.WriteLine("Menu Utama:");
            Console.WriteLine("1. Kelola Region");
            Console.WriteLine("2. Kelola Job");
            Console.WriteLine("3. Kelola Department"); // Tambahkan opsi untuk mengelola departemen
            Console.WriteLine("4. Keluar");

            Console.Write("Pilih menu (1-4): ");
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
                    departmentController.RunDepartmentMenu(); // Panggil menu departemen
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        }
    }
}
