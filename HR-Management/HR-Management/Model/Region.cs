using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HR_Management.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private readonly string connectionString;

        public Region()
        {
            // Konstruktor untuk menginisialisasi koneksi
            connectionString = Connection.GetConnectionString();
        }

        // GET ALL: Region
        public List<Region> GetAll()
        {
            var regions = new List<Region>();

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("SELECT * FROM tbl_regions", connection);
            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                regions.Add(new Region
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()
                });
            }

            return regions;
        }

        // GET BY ID: Region
        public Region GetById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("SELECT * FROM tbl_regions WHERE Id = @id", connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Region
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()
                };
            }

            return null;
        }

        // INSERT: Region
        public bool Insert(string name)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("INSERT INTO tbl_regions (Name) VALUES (@name)", connection);
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            connection.Open();

            return command.ExecuteNonQuery() > 0;
        }

        // UPDATE: Region
        public bool Update(int id, string name)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("UPDATE tbl_regions SET Name = @name WHERE Id = @id", connection);
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            connection.Open();

            return command.ExecuteNonQuery() > 0;
        }

        // DELETE: Region
        public bool Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("DELETE FROM tbl_regions WHERE Id = @id", connection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            connection.Open();

            return command.ExecuteNonQuery() > 0;
        }
    }
}
