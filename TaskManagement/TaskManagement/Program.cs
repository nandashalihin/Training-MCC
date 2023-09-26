using System;
using System.Collections.Generic;

class Program
{
    static List<Task> taskList = new List<Task>();
    static int nextTaskId = 1;

    class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("===== Aplikasi Manajemen Tugas =====");
            Console.WriteLine("Pilih operasi:");
            Console.WriteLine("1. Tambah Tugas");
            Console.WriteLine("2. Lihat Daftar Tugas");
            Console.WriteLine("3. Edit Tugas");
            Console.WriteLine("4. Hapus Tugas");
            Console.WriteLine("5. Keluar");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        TambahTugas();
                        break;
                    case 2:
                        LihatDaftarTugas();
                        break;
                    case 3:
                        EditTugas();
                        break;
                    case 4:
                        HapusTugas();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
            }

            Console.WriteLine();
        }
    }

    static void TambahTugas()
    {
        Console.Write("Masukkan deskripsi tugas baru: ");
        string newTaskDescription = Console.ReadLine();
        taskList.Add(new Task { Id = nextTaskId, Description = newTaskDescription });
        nextTaskId++;
        Console.WriteLine("Tugas berhasil ditambahkan.");
    }

    static void LihatDaftarTugas()
    {
        Console.WriteLine("===== Daftar Tugas =====");
        foreach (var task in taskList)
        {
            Console.WriteLine($"ID: {task.Id}, Deskripsi: {task.Description}");
        }
        Console.WriteLine("========================");
    }

    static void EditTugas()
    {
        Console.Write("Masukkan ID tugas yang ingin diedit: ");
        if (int.TryParse(Console.ReadLine(), out int taskId))
        {
            var taskToEdit = taskList.Find(task => task.Id == taskId);
            if (taskToEdit != null)
            {
                Console.Write("Masukkan deskripsi tugas yang baru: ");
                taskToEdit.Description = Console.ReadLine();
                Console.WriteLine("Tugas berhasil diubah.");
            }
            else
            {
                Console.WriteLine("Tugas dengan ID tersebut tidak ditemukan.");
            }
        }
        else
        {
            Console.WriteLine("ID tidak valid. Tidak ada tugas yang diubah.");
        }
    }

    static void HapusTugas()
    {
        Console.Write("Masukkan ID tugas yang ingin dihapus: ");
        if (int.TryParse(Console.ReadLine(), out int taskId))
        {
            var taskToRemove = taskList.Find(task => task.Id == taskId);
            if (taskToRemove != null)
            {
                taskList.Remove(taskToRemove);
                Console.WriteLine($"Tugas dengan ID {taskId} berhasil dihapus.");
            }
            else
            {
                Console.WriteLine("Tugas dengan ID tersebut tidak ditemukan.");
            }
        }
        else
        {
            Console.WriteLine("ID tidak valid. Tidak ada tugas yang dihapus.");
        }
    }
}
