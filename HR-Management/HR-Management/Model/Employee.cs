using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HR_Management
{
    public class Employee
    {
        public int Id { get; set; }
        public string first_name { get; set; } = "";
        public string last_name { get; set; } = "";
        public string email { get; set; } = "";
        public string phone_number { get; set; } = "";
        public DateTime hire_date { get; set; }
        public int salary { get; set; }
        public decimal? commission_pct { get; set; }
        public int job_id { get; set; }
        public int? manager_id { get; set; }
        public int department_id { get; set; }

        private readonly string connectionString = Connection.GetConnectionString();

        private void InitializeSqlCommand(SqlCommand command, string sql, List<SqlParameter> parameters)
        {
            command.Connection = new SqlConnection(connectionString);
            command.CommandText = sql;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }
        }

        private List<Employee> ExecuteReader(SqlCommand command)
        {
            var employees = new List<Employee>();

            using (var connection = command.Connection)
            {
                try
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employee
                                {
                                    Id = reader.GetInt32(0),
                                    first_name = reader.GetString(1),
                                    last_name = reader.GetString(2),
                                    email = reader.GetString(3),
                                    phone_number = reader.GetString(4),
                                    hire_date = reader.GetDateTime(5),
                                    salary = reader.GetInt32(6),
                                    commission_pct = reader.IsDBNull(7) ? null : (decimal?)reader.GetDecimal(7),
                                    job_id = reader.GetInt32(8),
                                    manager_id = reader.IsDBNull(9) ? null : (int?)reader.GetInt32(9),
                                    department_id = reader.GetInt32(10)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return employees;
            }
        }

        public List<Employee> GetAll()
        {
            var sql = "SELECT * FROM tbl_employees";
            var parameters = new List<SqlParameter>();

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            return ExecuteReader(command);
        }

        public Employee GetById(int id)
        {
            var sql = "SELECT * FROM tbl_employees WHERE Id = @id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            var employees = ExecuteReader(command);
            return employees.Count > 0 ? employees[0] : null;
        }

        public string Insert(Employee employee)
        {
            var sql = "INSERT INTO tbl_employees (first_name, last_name, email, phone_number, hire_date, salary, commission_pct, job_id, manager_id, department_id) " +
                      "VALUES (@first_name, @last_name, @email, @phone_number, @hire_date, @salary, @commission_pct, @job_id, @manager_id, @department_id);";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@first_name", employee.first_name),
                new SqlParameter("@last_name", employee.last_name),
                new SqlParameter("@email", employee.email),
                new SqlParameter("@phone_number", employee.phone_number),
                new SqlParameter("@hire_date", employee.hire_date),
                new SqlParameter("@salary", employee.salary),
                new SqlParameter("@commission_pct", (object)employee.commission_pct ?? DBNull.Value),
                new SqlParameter("@job_id", employee.job_id),
                new SqlParameter("@manager_id", (object)employee.manager_id ?? DBNull.Value),
                new SqlParameter("@department_id", employee.department_id)
            };

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            try
            {
                using (var connection = command.Connection)
                {
                    connection.Open();

                    var result = command.ExecuteNonQuery();

                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Update(Employee employee)
        {
            var sql = "UPDATE tbl_employees SET first_name = @first_name, last_name = @last_name, email = @email, " +
                      "phone_number = @phone_number, hire_date = @hire_date, salary = @salary, commission_pct = @commission_pct, " +
                      "job_id = @job_id, manager_id = @manager_id, department_id = @department_id WHERE Id = @Id;";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@first_name", employee.first_name),
                new SqlParameter("@last_name", employee.last_name),
                new SqlParameter("@email", employee.email),
                new SqlParameter("@phone_number", employee.phone_number),
                new SqlParameter("@hire_date", employee.hire_date),
                new SqlParameter("@salary", employee.salary),
                new SqlParameter("@commission_pct", (object)employee.commission_pct ?? DBNull.Value),
                new SqlParameter("@job_id", employee.job_id),
                new SqlParameter("@manager_id", (object)employee.manager_id ?? DBNull.Value),
                new SqlParameter("@department_id", employee.department_id),
                new SqlParameter("@Id", employee.Id)
            };

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            try
            {
                using (var connection = command.Connection)
                {
                    connection.Open();

                    var result = command.ExecuteNonQuery();

                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Delete(int id)
        {
            var sql = "DELETE FROM tbl_employees WHERE Id = @id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            try
            {
                using (var connection = command.Connection)
                {
                    connection.Open();

                    var result = command.ExecuteNonQuery();

                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
