using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HR_Management.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }

        private readonly string connectionString;

        public Job()
        {
            connectionString = Connection.GetConnectionString();
        }

        public List<Job> GetAll()
        {
            var jobs = new List<Job>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("SELECT id AS Id, title AS Title, min_salary AS MinSalary, max_salary AS MaxSalary FROM tbl_jobs", connection);
            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                jobs.Add(new Job
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Title = reader["Title"].ToString(),
                    MinSalary = Convert.ToDecimal(reader["MinSalary"]),
                    MaxSalary = Convert.ToDecimal(reader["MaxSalary"])
                });
            }

            return jobs;
        }

        public Job GetById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("SELECT id AS Id, title AS Title, min_salary AS MinSalary, max_salary AS MaxSalary FROM tbl_jobs WHERE Id = @id", connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Job
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Title = reader["Title"].ToString(),
                    MinSalary = Convert.ToDecimal(reader["MinSalary"]),
                    MaxSalary = Convert.ToDecimal(reader["MaxSalary"])
                };
            }

            return null;
        }

        public string Insert(Job job)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("INSERT INTO tbl_jobs (Title, min_salary, max_salary) " +
                                              "VALUES (@title, @minSalary, @maxSalary);", connection);
            command.Parameters.Add("@title", SqlDbType.VarChar).Value = job.Title;
            command.Parameters.Add("@minSalary", SqlDbType.Decimal).Value = job.MinSalary;
            command.Parameters.Add("@maxSalary", SqlDbType.Decimal).Value = job.MaxSalary;
            connection.Open();

            try
            {
                var result = command.ExecuteNonQuery();
                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Update(Job job)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("UPDATE tbl_jobs SET Title = @title, " +
                                              "min_salary = @minSalary, max_salary = @maxSalary " +
                                              "WHERE Id = @id;", connection);
            command.Parameters.Add("@title", SqlDbType.VarChar).Value = job.Title;
            command.Parameters.Add("@minSalary", SqlDbType.Decimal).Value = job.MinSalary;
            command.Parameters.Add("@maxSalary", SqlDbType.Decimal).Value = job.MaxSalary;
            command.Parameters.Add("@id", SqlDbType.Int).Value = job.Id;
            connection.Open();

            try
            {
                var result = command.ExecuteNonQuery();
                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("DELETE FROM tbl_jobs WHERE Id = @id", connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            connection.Open();

            try
            {
                var result = command.ExecuteNonQuery();
                return result.ToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
