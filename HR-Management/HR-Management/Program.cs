using HR_Management;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace HR_Management
{
    class Program
    {
        string connectionString = Connection.GetConnectionString();

        private static string GetFullName(Employee employee)
        {
            return $"{employee.first_name} {employee.last_name}";
        }

        private List<Dictionary<string, string>> GetDataAsDictionary()
        {
            var employees = new Employee().GetAll();
            var departments = new Department().GetAll();
            var locations = new Location().GetAll();
            var countries = new Country().GetAll();
            var regions = new Region().GetAll();

            var data = (from employee in employees
                        join department in departments on employee.department_id equals department.Id
                        join location in locations on department.LocationId equals location.Id
                        join country in countries on location.CountryId equals country.Id
                        join region in regions on country.RegionId equals region.Id
                        select new Dictionary<string, string>
                {
                    { "ID", employee.Id.ToString() },
                    { "Full Name", GetFullName(employee) },
                    { "Email", employee.email },
                    { "Phone", employee.phone_number },
                    { "Salary", employee.salary.ToString() },
                    { "Department Name", department.Name },
                    { "Street Address", location.StreetAddress },
                    { "Country Name", country.Name },
                    { "Region Name", region.Name }
                }).ToList();

            return data;
        }



        private void DisplayEmployeeInfo()
        {
            var employees = new Employee().GetAll();
            var employeeData = GetDataAsDictionary();
            ConsoleHelper.DisplayTable(employeeData);
        }

        private void DisplayEmployeeCountAndSalaryByDepartment()
        {
            var departments = new Department().GetAll();
            var employees = new Employee().GetAll();

            var departmentStats = from department in departments
                                  join employee in employees on department.Id equals employee.department_id
                                  group employee.salary by department.Name into departmentGroup
                                  select new
                                  {
                                      Department_Name = departmentGroup.Key,
                                      Total_Employee = departmentGroup.Count(),
                                      Min_Salary = departmentGroup.Min(),
                                      Max_Salary = departmentGroup.Max(),
                                      Average_Salary = Convert.ToInt32(departmentGroup.Average())
                                  };

            var statsData = new List<Dictionary<string, string>>();

            foreach (var result in departmentStats)
            {
                var row = new Dictionary<string, string>
                {
                    { "Department Name", result.Department_Name },
                    { "Total Employee", result.Total_Employee.ToString() },
                    { "Min Salary", result.Min_Salary.ToString() },
                    { "Max Salary", result.Max_Salary.ToString() },
                    { "Average Salary", result.Average_Salary.ToString() }
                };
                statsData.Add(row);
            }

            ConsoleHelper.DisplayTable(statsData);
        }

        private void Run()
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Tampilkan semua data Regions");
                Console.WriteLine("2. Tampilkan semua data Employees");
                Console.WriteLine("3. Tampilkan semua data Countries");
                Console.WriteLine("4. Tampilkan semua data Locations");
                Console.WriteLine("5. Tampilkan semua data Departments");
                Console.WriteLine("6. Tampilkan semua data Jobs");
                Console.WriteLine("7. Tampilkan semua data Job Histories");
                Console.WriteLine("8. Tampilkan data Employee beserta informasi Department, Lokasi, Country, dan Region");
                Console.WriteLine("9. Tampilkan Jumlah Employee pada Tiap Department dan Statistik Gaji (Min, Max, Avg)");
                Console.WriteLine("10. Keluar");

                Console.Write("Pilih menu (1-10): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var region = new Region();
                        var getAllRegion = region.GetAll();
                        var regionData = new List<Dictionary<string, string>>();

                        foreach (var region1 in getAllRegion)
                        {
                            var row = new Dictionary<string, string>
                            {
                                { "Id", region1.Id.ToString() },
                                { "Name", region1.Name }
                            };
                            regionData.Add(row);
                        }

                        ConsoleHelper.DisplayTable(regionData);
                        break;
                    case "2":
                        var employee = new Employee();
                        var getAllEmployees = employee.GetAll();
                        var employeeData = GetDataAsDictionary();
                        ConsoleHelper.DisplayTable(employeeData);
                        break;
                    case "3":
                        var country = new Country();
                        var getAllCountries = country.GetAll();
                        var countryData = new List<Dictionary<string, string>>();

                        foreach (var c in getAllCountries)
                        {
                            var row = new Dictionary<string, string>
                            {
                                { "ID", c.Id.ToString() },
                                { "Name", c.Name },
                                { "Region ID", c.RegionId.ToString() }
                            };
                            countryData.Add(row);
                        }

                        ConsoleHelper.DisplayTable(countryData);
                        break;
                    case "4":
                        var location = new Location();
                        var getAllLocations = location.GetAll();
                        var locationData = new List<Dictionary<string, string>>();

                        foreach (var loc in getAllLocations)
                        {
                            var row = new Dictionary<string, string>
                            {
                                { "ID", loc.Id.ToString() },
                                { "Street Address", loc.StreetAddress },
                                { "Postal Code", loc.Postal },
                                { "City", loc.City },
                                { "State/Province", loc.StateProvince },
                                { "Country ID", loc.CountryId.ToString() }
                            };
                            locationData.Add(row);
                        }

                        ConsoleHelper.DisplayTable(locationData);
                        break;
                    case "5":
                        var department = new Department();
                        var getAllDepartments = department.GetAll();
                        var departmentData = new List<Dictionary<string, string>>();

                        foreach (var dept in getAllDepartments)
                        {
                            var row = new Dictionary<string, string>
                            {
                                { "ID", dept.Id.ToString() },
                                { "Name", dept.Name },
                                { "Location ID", dept.LocationId.ToString() },
                                { "Manager ID", dept.ManagerId.ToString() }
                            };
                            departmentData.Add(row);
                        }

                        ConsoleHelper.DisplayTable(departmentData);
                        break;
                    case "6":
                        var job = new Job();
                        var getAllJobs = job.GetAll();
                        var jobData = new List<Dictionary<string, string>>();

                        foreach (var j in getAllJobs)
                        {
                            var row = new Dictionary<string, string>
                            {
                                { "ID", j.Id.ToString() },
                                { "Title", j.Title },
                                { "Min Salary", j.MinSalary.ToString() },
                                { "Max Salary", j.MaxSalary.ToString() }
                            };
                            jobData.Add(row);
                        }

                        ConsoleHelper.DisplayTable(jobData);
                        break;
                    case "7":
                        var jobHistory = new JobHistory();
                        var getAllJobHistories = jobHistory.GetAll();
                        var jobHistoryData = new List<Dictionary<string, string>>();

                        foreach (var jh in getAllJobHistories)
                        {
                            var row = new Dictionary<string, string>
                            {
                                { "Employee ID", jh.EmployeeId.ToString() },
                                { "Start Date", jh.StartDate.ToString("dd/MM/yyyy") },
                                { "End Date", jh.EndDate.ToString("dd/MM/yyyy") },
                                { "Job ID", jh.JobId.ToString() },
                                { "Department ID", jh.DepartmentId.ToString() }
                            };
                            jobHistoryData.Add(row);
                        }

                        ConsoleHelper.DisplayTable(jobHistoryData);
                        break;
                    case "8":
                        // Tampilkan data Employee beserta informasi Department, Lokasi, Country, dan Region
                        DisplayEmployeeInfo();
                        break;
                    case "9":
                        // Tampilkan Jumlah Employee pada Tiap Department dan Statistik Gaji (Min, Max, Avg)
                        DisplayEmployeeCountAndSalaryByDepartment();
                        break;
                    case "10":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            }
        }

        static void Main()
        {
            Program program = new Program();
            program.Run();
        }
    }
}
