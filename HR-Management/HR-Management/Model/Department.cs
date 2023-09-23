using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HR_Management.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public int ManagerId { get; set; }

        private readonly string connectionString;

        public Department()
        {
            connectionString = Connection.GetConnectionString();
        }

        private void InitializeSqlCommand(SqlCommand command, string sql, List<SqlParameter> parameters)
        {
            command.Connection = new SqlConnection(connectionString);
            command.CommandText = sql;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }
        }

        private List<Department> ExecuteReader(SqlCommand command)
        {
            var departments = new List<Department>();

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
                                departments.Add(new Department
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    LocationId = reader.GetInt32(2),
                                    ManagerId = reader.GetInt32(3)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return departments;
            }
        }

        public List<Department> GetAll()
        {
            var sql = "SELECT * FROM tbl_departments";
            var parameters = new List<SqlParameter>();

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            return ExecuteReader(command);
        }

        public Department GetById(int id)
        {
            var sql = "SELECT * FROM tbl_departments WHERE Id = @id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            var departments = ExecuteReader(command);
            return departments.Count > 0 ? departments[0] : null;
        }

        public string Insert(Department department)
        {
            var sql = "INSERT INTO tbl_departments (name, location_id, manager_id) " +
                      "VALUES (@name, @location_id, @manager_id);";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@name", department.Name),
                new SqlParameter("@location_id", department.LocationId),
                new SqlParameter("@manager_id", department.ManagerId)
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

        public string Update(Department department)
        {
            var sql = "UPDATE tbl_departments SET name = @name, " +
                      "location_id = @location_id, manager_id = @manager_id " +
                      "WHERE Id = @Id;";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@name", department.Name),
                new SqlParameter("@location_id", department.LocationId),
                new SqlParameter("@manager_id", department.ManagerId),
                new SqlParameter("@Id", department.Id)
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
            var sql = "DELETE FROM tbl_departments WHERE Id = @id";
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
