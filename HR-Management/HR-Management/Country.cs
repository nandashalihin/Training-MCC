using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HR_Management
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegionId { get; set; }

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

        private List<Country> ExecuteReader(SqlCommand command)
        {
            var countries = new List<Country>();

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
                                countries.Add(new Country
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    RegionId = reader.GetInt32(2)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return countries;
            }
        }

        public List<Country> GetAll()
        {
            var sql = "SELECT * FROM tbl_countries";
            var parameters = new List<SqlParameter>();

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            return ExecuteReader(command);
        }

        public Country GetById(int id)
        {
            var sql = "SELECT * FROM tbl_countries WHERE Id = @id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            var countries = ExecuteReader(command);
            return countries.Count > 0 ? countries[0] : null;
        }

        public string Insert(Country country)
        {
            var sql = "INSERT INTO tbl_countries (name, region_id) " +
                      "VALUES (@name, @region_id);";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@name", country.Name),
                new SqlParameter("@region_id", country.RegionId)
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

        public string Update(Country country)
        {
            var sql = "UPDATE tbl_countries SET name = @name, region_id = @region_id WHERE Id = @Id;";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@name", country.Name),
                new SqlParameter("@region_id", country.RegionId),
                new SqlParameter("@Id", country.Id)
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
            var sql = "DELETE FROM tbl_countries WHERE Id = @id";
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
