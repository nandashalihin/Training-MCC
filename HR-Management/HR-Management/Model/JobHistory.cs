using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HR_Management
{
    public class JobHistory
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.Now;
        public int JobId { get; set; }
        public int DepartmentId { get; set; }

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

        private List<JobHistory> ExecuteReader(SqlCommand command)
        {
            var jobHistories = new List<JobHistory>();

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
                                jobHistories.Add(new JobHistory
                                {
                                    EmployeeId = reader.GetInt32(0),
                                    StartDate = reader.GetDateTime(1),
                                    EndDate = reader.GetDateTime(2),
                                    JobId = reader.GetInt32(3),
                                    DepartmentId = reader.GetInt32(4)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return jobHistories;
            }
        }

        public List<JobHistory> GetAll()
        {
            var sql = "SELECT * FROM tbl_job_histories";
            var parameters = new List<SqlParameter>();

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            return ExecuteReader(command);
        }

        public JobHistory GetByEmployeeIdAndStartDate(int employeeId, DateTime startDate)
        {
            var sql = "SELECT * FROM tbl_job_histories WHERE employee_id = @employee_id AND start_date = @start_date";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@employee_id", employeeId),
                new SqlParameter("@start_date", startDate)
            };

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            var jobHistories = ExecuteReader(command);
            return jobHistories.Count > 0 ? jobHistories[0] : null;
        }

        public string Insert(JobHistory jobHistory)
        {
            var sql = "INSERT INTO tbl_job_histories (employee_id, start_date, end_date, job_id, department_id) " +
                      "VALUES (@employee_id, @start_date, @end_date, @job_id, @department_id);";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@employee_id", jobHistory.EmployeeId),
                new SqlParameter("@start_date", jobHistory.StartDate),
                new SqlParameter("@end_date", (object)jobHistory.EndDate ?? DBNull.Value),
                new SqlParameter("@job_id", jobHistory.JobId),
                new SqlParameter("@department_id", jobHistory.DepartmentId)
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

        // Perhatikan bahwa tidak ada metode Update dan Delete karena primary key adalah kombinasi dari dua kolom.
    }
}
