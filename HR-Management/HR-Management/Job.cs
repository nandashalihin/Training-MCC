using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HR_Management
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }

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

        private List<Job> ExecuteReader(SqlCommand command)
        {
            var jobs = new List<Job>();

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
                                jobs.Add(new Job
                                {
                                    Id = reader.GetInt32(0),
                                    Title = reader.GetString(1),
                                    MinSalary = reader.GetDecimal(2),
                                    MaxSalary = reader.GetDecimal(3)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return jobs;
            }
        }

        public List<Job> GetAll()
        {
            var sql = "SELECT * FROM tbl_jobs";
            var parameters = new List<SqlParameter>();

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            return ExecuteReader(command);
        }

        public Job GetById(int id)
        {
            var sql = "SELECT * FROM tbl_jobs WHERE Id = @id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            var jobs = ExecuteReader(command);
            return jobs.Count > 0 ? jobs[0] : null;
        }

        public string Insert(Job job)
        {
            var sql = "INSERT INTO tbl_jobs (title, min_salary, max_salary) " +
                      "VALUES (@title, @min_salary, @max_salary);";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@title", job.Title),
                new SqlParameter("@min_salary", job.MinSalary),
                new SqlParameter("@max_salary", job.MaxSalary)
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

        public string Update(Job job)
        {
            var sql = "UPDATE tbl_jobs SET title = @title, min_salary = @min_salary, max_salary = @max_salary WHERE Id = @Id;";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@title", job.Title),
                new SqlParameter("@min_salary", job.MinSalary),
                new SqlParameter("@max_salary", job.MaxSalary),
                new SqlParameter("@Id", job.Id)
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
            var sql = "DELETE FROM tbl_jobs WHERE Id = @id";
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
