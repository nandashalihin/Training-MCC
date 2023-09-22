using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HR_Management
{
    public class Location
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string Postal { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public int CountryId { get; set; }

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

        private List<Location> ExecuteReader(SqlCommand command)
        {
            var locations = new List<Location>();

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
                                locations.Add(new Location
                                {
                                    Id = reader.GetInt32(0),
                                    StreetAddress = reader.GetString(1),
                                    Postal = reader.GetString(2),
                                    City = reader.GetString(3),
                                    StateProvince = reader.GetString(4),
                                    CountryId = reader.GetInt32(5)
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return locations;
            }
        }

        public List<Location> GetAll()
        {
            var sql = "SELECT * FROM tbl_locations";
            var parameters = new List<SqlParameter>();

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            return ExecuteReader(command);
        }

        public Location GetById(int id)
        {
            var sql = "SELECT * FROM tbl_locations WHERE Id = @id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };

            var command = new SqlCommand();
            InitializeSqlCommand(command, sql, parameters);

            var locations = ExecuteReader(command);
            return locations.Count > 0 ? locations[0] : null;
        }

        public string Insert(Location location)
        {
            var sql = "INSERT INTO tbl_locations (street_address, postal, city, state_province, country_id) " +
                      "VALUES (@street_address, @postal, @city, @state_province, @country_id);";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@street_address", location.StreetAddress),
                new SqlParameter("@postal", location.Postal),
                new SqlParameter("@city", location.City),
                new SqlParameter("@state_province", location.StateProvince),
                new SqlParameter("@country_id", location.CountryId)
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

        public string Update(Location location)
        {
            var sql = "UPDATE tbl_locations SET street_address = @street_address, postal = @postal, " +
                      "city = @city, state_province = @state_province, country_id = @country_id WHERE Id = @Id;";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@street_address", location.StreetAddress),
                new SqlParameter("@postal", location.Postal),
                new SqlParameter("@city", location.City),
                new SqlParameter("@state_province", location.StateProvince),
                new SqlParameter("@country_id", location.CountryId),
                new SqlParameter("@Id", location.Id)
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
            var sql = "DELETE FROM tbl_locations WHERE Id = @id";
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
