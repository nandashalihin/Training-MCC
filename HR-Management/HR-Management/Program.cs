using HR_Management;
using System.Data;
using System.Data.SqlClient;

namespace HR_Management;

 class Program
{
    string connectionString = Connection.GetConnectionString();

    
    private static void Main()
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
            Console.WriteLine("8. Keluar");

            Console.Write("Pilih menu (1-8): ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var region = new Region();

                    var getAllRegion = region.GetAll();

                    if (getAllRegion.Count > 0)
                    {
                        foreach (var region1 in getAllRegion)
                        {
                            Console.WriteLine($"Id: {region1.Id}, Name: {region1.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    break;
                case "2":
                    var employee = new Employee();

                    var getAllEmployees = employee.GetAll();

                    if (getAllEmployees.Count > 0)
                    {
                        foreach (var employee1 in getAllEmployees)
                        {
                            Console.WriteLine($"Id: {employee1.Id}");
                            Console.WriteLine($"First Name: {employee1.first_name}");
                            Console.WriteLine($"Last Name: {employee1.last_name}");
                            Console.WriteLine($"email: {employee1.email}");
                            Console.WriteLine($"Phone Number: {employee1.phone_number}");
                            Console.WriteLine($"Hire Date: {employee1.hire_date.ToString("dd/MM/yyyy")}");
                            Console.WriteLine($"salary: {employee1.salary}");
                            Console.WriteLine($"Commission Pct: {employee1.commission_pct}");
                            Console.WriteLine($"Job ID: {employee1.job_id}");
                            Console.WriteLine($"Manager ID: {employee1.manager_id}");
                            Console.WriteLine($"Department ID: {employee1.department_id}");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    break;
                case "3":
                    var country = new Country();
                    var getAllCountries = country.GetAll();

                    if (getAllCountries.Count > 0)
                    {
                        Console.WriteLine("Data from tbl_countries:");
                        foreach (var c in getAllCountries)
                        {
                            Console.WriteLine($"ID: {c.Id}");
                            Console.WriteLine($"Name: {c.Name}");
                            Console.WriteLine($"Region ID: {c.RegionId}");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found in tbl_countries");
                    }

                    break;
                case "4":
                    var location = new Location();
                    var getAllLocations = location.GetAll();

                    if (getAllLocations.Count > 0)
                    {
                        Console.WriteLine("Data from tbl_locations:");
                        foreach (var loc in getAllLocations)
                        {
                            Console.WriteLine($"ID: {loc.Id}");
                            Console.WriteLine($"Street Address: {loc.StreetAddress}");
                            Console.WriteLine($"Postal Code: {loc.Postal}");
                            Console.WriteLine($"City: {loc.City}");
                            Console.WriteLine($"State/Province: {loc.StateProvince}");
                            Console.WriteLine($"Country ID: {loc.CountryId}");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found in tbl_locations");
                    }
                    break;
                case "5":
                    var department = new Department();
                    var getAllDepartments = department.GetAll();

                    if (getAllDepartments.Count > 0)
                    {
                        Console.WriteLine("Data from tbl_departments:");
                        foreach (var dept in getAllDepartments)
                        {
                            Console.WriteLine($"ID: {dept.Id}");
                            Console.WriteLine($"Name: {dept.Name}");
                            Console.WriteLine($"Location ID: {dept.LocationId}");
                            Console.WriteLine($"Manager ID: {dept.ManagerId}");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found in tbl_departments");
                    }

                    break;
                case "6":
                    var job = new Job();
                    var getAllJobs = job.GetAll();

                    if (getAllJobs.Count > 0)
                    {
                        Console.WriteLine("Data from tbl_jobs:");
                        foreach (var j in getAllJobs)
                        {
                            Console.WriteLine($"ID: {j.Id}");
                            Console.WriteLine($"Title: {j.Title}");
                            Console.WriteLine($"Min Salary: {j.MinSalary}");
                            Console.WriteLine($"Max Salary: {j.MaxSalary}");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found in tbl_jobs");
                    }
                    break;
                case "7":
                    var jobHistory = new JobHistory();
                    var getAllJobHistories = jobHistory.GetAll();

                    if (getAllJobHistories.Count > 0)
                    {
                        Console.WriteLine("Data from tbl_job_histories:");
                        foreach (var jh in getAllJobHistories)
                        {
                            Console.WriteLine($"Employee ID: {jh.EmployeeId}");
                            Console.WriteLine($"Start Date: {jh.StartDate}");
                            Console.WriteLine($"End Date: {jh.EndDate}");
                            Console.WriteLine($"Job ID: {jh.JobId}");
                            Console.WriteLine($"Department ID: {jh.DepartmentId}");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found in tbl_job_histories");
                    }
                    break;
                case "8":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }
        } 
        /*var employee = new Employee();

        var getAllEmployees = employee.GetAll();

        if (getAllEmployees.Count > 0)
        {
            foreach (var employee1 in getAllEmployees)
            {
                Console.WriteLine($"Id: {employee1.Id}");
                Console.WriteLine($"First Name: {employee1.first_name}");
                Console.WriteLine($"Last Name: {employee1.last_name}");
                Console.WriteLine($"email: {employee1.email}");
                Console.WriteLine($"Phone Number: {employee1.phone_number}");
                Console.WriteLine($"Hire Date: {employee1.hire_date}");
                Console.WriteLine($"salary: {employee1.salary}");
                Console.WriteLine($"Commission Pct: {employee1.commission_pct}");
                Console.WriteLine($"Job ID: {employee1.job_id}");
                Console.WriteLine($"Manager ID: {employee1.manager_id}");
                Console.WriteLine($"Department ID: {employee1.department_id}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }
*/

        /*var employee = new Employee();

        // Mengambil data karyawan berdasarkan ID yang ingin diubah
        int employeeIdToUpdate = 11; // ID karyawan yang ingin diubah
        var employeeToUpdate = employee.GetById(employeeIdToUpdate);

        if (employeeToUpdate != null)
        {
            // Menampilkan detail karyawan sebelum pembaruan
            Console.WriteLine("Employee Details Before Update:");
            Console.WriteLine($"Id: {employeeToUpdate.Id}");
            Console.WriteLine($"First Name: {employeeToUpdate.first_name}");
            Console.WriteLine($"Last Name: {employeeToUpdate.last_name}");
            Console.WriteLine($"email: {employeeToUpdate.email}");
            Console.WriteLine($"Phone Number: {employeeToUpdate.phone_number}");
            Console.WriteLine($"Hire Date: {employeeToUpdate.hire_date}");
            Console.WriteLine($"salary: {employeeToUpdate.salary}");
            Console.WriteLine($"Commission Pct: {employeeToUpdate.commission_pct}");
            Console.WriteLine($"Job ID: {employeeToUpdate.job_id}");
            Console.WriteLine($"Manager ID: {employeeToUpdate.manager_id}");
            Console.WriteLine($"Department ID: {employeeToUpdate.department_id}");
            Console.WriteLine();

            // Melakukan pembaruan (update) pada karyawan
            employeeToUpdate.first_name = "Najwa;"; // Ganti dengan data baru
            employeeToUpdate.last_name = "Sahira";   // Ganti dengan data baru
            employeeToUpdate.phone_number = "08970322873";
            // Melakukan pembaruan pada karyawan di database
            string updateResult = employee.Update(employeeToUpdate);

            if (int.TryParse(updateResult, out int result) && result > 0)
            {
                Console.WriteLine("Employee Updated Successfully.");
            }
            else
            {
                Console.WriteLine("Error updating employee.");
                Console.WriteLine(updateResult);
            }

            // Menampilkan detail karyawan setelah pembaruan
            Console.WriteLine("\nEmployee Details After Update:");
            Console.WriteLine($"Id: {employeeToUpdate.Id}");
            Console.WriteLine($"First Name: {employeeToUpdate.first_name}");
            Console.WriteLine($"Last Name: {employeeToUpdate.last_name}");
            Console.WriteLine($"email: {employeeToUpdate.email}");
            Console.WriteLine($"Phone Number: {employeeToUpdate.phone_number}");
            Console.WriteLine($"Hire Date: {employeeToUpdate.hire_date}");
            Console.WriteLine($"salary: {employeeToUpdate.salary}");
            Console.WriteLine($"Commission Pct: {employeeToUpdate.commission_pct}");
            Console.WriteLine($"Job ID: {employeeToUpdate.job_id}");
            Console.WriteLine($"Manager ID: {employeeToUpdate.manager_id}");
            Console.WriteLine($"Department ID: {employeeToUpdate.department_id}");
        }
        else
        {
            Console.WriteLine("Employee not found.");
        }*/

        /*var employee = new Employee();

        // DELETE: Employee
        int employeeIdToDelete = 21; // Ganti dengan ID karyawan yang ingin dihapus
        var deleteResult = employee.Delete(employeeIdToDelete);

        int.TryParse(deleteResult, out int deleteResultInt);

        if (deleteResultInt > 0)
        {
            Console.WriteLine("Delete Success");
        }
        else
        {
            Console.WriteLine("Delete Failed");
            Console.WriteLine(deleteResult);
        }*/

        /*var employee = new Employee();

        // INSERT: Employee
        var newEmployee = new Employee
        {
            first_name = "NewFirstName", // Ganti dengan nama baru
            last_name = "NewLastName",   // Ganti dengan nama baru
            email = "newemail@example.com",
            phone_number = "1234567890",
            hire_date = DateTime.Now,
            salary = 50000,
            commission_pct = null, // Ubah sesuai dengan komisi jika ada, atau biarkan null
            job_id = 1, // Ganti dengan ID pekerjaan yang sesuai
            manager_id = 1, // Ubah sesuai dengan ID manajer jika ada, atau biarkan null
            department_id = 1 // Ganti dengan ID departemen yang sesuai
        };

        var insertResult = employee.Insert(newEmployee);

        int.TryParse(insertResult, out int insertResultInt);

        if (insertResultInt > 0)
        {
            Console.WriteLine("Insert Success");
        }
        else
        {
            Console.WriteLine("Insert Failed");
            Console.WriteLine(insertResult);
        }
*/








        //REGION

        /*var region = new Region();

        var getAllRegion = region.GetAll();

        if (getAllRegion.Count > 0)
        {
            foreach (var region1 in getAllRegion)
            {
                Console.WriteLine($"Id: {region1.Id}, Name: {region1.Name}");
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }*/

        /*var updateResult = region.Update(9,"tes");
        int.TryParse(updateResult, out int result);
        if (result > 0)
        {
            Console.WriteLine("update Success");
        }
        else
        {
            Console.WriteLine("update Failed");
            Console.WriteLine(updateResult);
        }*/

        /*var deleteResult = region.Delete(6);
        int.TryParse(deleteResult, out int result);
        if (result > 0)
        {
            Console.WriteLine("Delete Success");
        }
        else
        {
            Console.WriteLine("Delete Failed");
            Console.WriteLine(deleteResult);
        }*/

        /*var insertResult = region.Insert("Region 5");
        int.TryParse(insertResult, out int result);
        if (result > 0)
        {
            Console.WriteLine("Insert Success");
        }
        else
        {
            Console.WriteLine("Insert Failed");
            Console.WriteLine(insertResult);
        }*/
    }

    /*// GET ALL: Region
    public static void GetAllRegions()
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM tbl_regions";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                }
            else
                Console.WriteLine("No rows found.");

            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // GET BY ID: Region
    public static void GetRegionById(int id) { }

    // INSERT: Region
    public static void InsertRegion(string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO tbl_regions VALUES (@name);";

        try
        {
            var pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.Value = name;
            pName.SqlDbType = SqlDbType.VarChar;
            command.Parameters.Add(pName);

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                switch (result)
                {
                    case >= 1:
                        Console.WriteLine("Insert Success");
                        break;
                    default:
                        Console.WriteLine("Insert Failed");
                        break;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

   
    // UPDATE: Region
    public static void UpdateRegion(int id, string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE tbl_regions SET Name = @name WHERE Id = @id;";

        try
        {
            var pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.Value = id;
            pId.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(pId);

            var pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.Value = name;
            pName.SqlDbType = SqlDbType.VarChar;
            command.Parameters.Add(pName);

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                if (result > 0)
                {
                    Console.WriteLine("Update Success");
                }
                else
                {
                    Console.WriteLine("Update Failed");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // DELETE: Region
    public static void DeleteRegion(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM tbl_regions WHERE Id = @id;";

        try
        {
            var pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.Value = id;
            pId.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(pId);

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                if (result > 0)
                {
                    Console.WriteLine("Delete Success");
                }
                else
                {
                    Console.WriteLine("Delete Failed");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

*/
} 
