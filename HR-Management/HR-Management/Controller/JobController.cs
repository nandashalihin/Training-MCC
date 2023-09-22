using HR_Management.Models;
using System;
using System.Collections.Generic;

namespace HR_Management.Controllers
{
    public class JobController
    {
        private readonly Job jobModel = new Job();

        public void DisplayAllJobs()
        {
            var jobs = jobModel.GetAll();
            var jobData = new List<Dictionary<string, string>>();

            foreach (var job in jobs)
            {
                var jobInfo = new Dictionary<string, string>
                {
                    { "Id", job.Id.ToString() },
                    { "Title", job.Title },
                    { "MinSalary", job.MinSalary.ToString() },
                    { "MaxSalary", job.MaxSalary.ToString() }
                };
                jobData.Add(jobInfo);
            }

            ConsoleHelper.DisplayTable(jobData);
        }

        public void AddJob()
        {
            Console.Write("Enter Job Title: ");
            var title = Console.ReadLine();

            Console.Write("Enter Minimum Salary: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal minSalary))
            {
                Console.Write("Enter Maximum Salary: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal maxSalary))
                {
                    var newJob = new Job
                    {
                        Title = title,
                        MinSalary = minSalary,
                        MaxSalary = maxSalary
                    };

                    var result = jobModel.Insert(newJob);
                    if (result == "1")
                    {
                        Console.WriteLine("Job berhasil ditambahkan.");
                    }
                    else
                    {
                        Console.WriteLine("Gagal menambahkan job. Silakan coba lagi.");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid Maximum Salary.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Minimum Salary.");
            }
        }

        public void UpdateJob()
        {
            Console.Write("Enter Job Id to Update: ");
            if (int.TryParse(Console.ReadLine(), out int jobId))
            {
                var existingJob = jobModel.GetById(jobId);

                if (existingJob != null)
                {
                    Console.Write("Enter New Job Title: ");
                    var newTitle = Console.ReadLine();

                    Console.Write("Enter New Minimum Salary: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal newMinSalary))
                    {
                        Console.Write("Enter New Maximum Salary: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newMaxSalary))
                        {
                            existingJob.Title = newTitle;
                            existingJob.MinSalary = newMinSalary;
                            existingJob.MaxSalary = newMaxSalary;

                            var result = jobModel.Update(existingJob);
                            if (result == "1")
                            {
                                Console.WriteLine("Job berhasil diupdate.");
                            }
                            else
                            {
                                Console.WriteLine("Gagal menambahkan job. Silakan coba lagi.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Maximum Salary.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Minimum Salary.");
                    }
                }
                else
                {
                    Console.WriteLine($"Job with Id {jobId} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Job Id.");
            }
        }

        public void DeleteJob()
        {
            Console.Write("Enter Job Id to Delete: ");
            if (int.TryParse(Console.ReadLine(), out int jobId))
            {
                var result = jobModel.Delete(jobId);
                if (result == "1")
                {
                    Console.WriteLine("Job berhasil dihapus.");
                }
                else
                {
                    Console.WriteLine("Gagal menambahkan job. Silakan coba lagi.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Job Id.");
            }
        }

        public void RunJobMenu()
        {
            while (true)
            {
                Console.WriteLine("Menu Job:");
                Console.WriteLine("1. Tampilkan semua data Job");
                Console.WriteLine("2. Tambahkan Job");
                Console.WriteLine("3. Perbarui Job");
                Console.WriteLine("4. Hapus Job");
                Console.WriteLine("5. Kembali ke Menu Utama");

                Console.Write("Pilih menu (1-5): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllJobs();
                        break;
                    case "2":
                        AddJob();
                        break;
                    case "3":
                        UpdateJob();
                        break;
                    case "4":
                        DeleteJob();
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
