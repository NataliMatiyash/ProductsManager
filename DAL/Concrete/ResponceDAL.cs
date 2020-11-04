using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class ResponceDAL : IResponceDAL
    {
        private string connectionString;

        public ResponceDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ResponceDTO Add(ResponceDTO responce)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    string query = "";
                    query = "Insert into Responces(Responce) " +
                    $"values('{responce.Responce}')";

                    Console.WriteLine(query);
                    comm.CommandText = query;
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return responce;
        }

        public void Delete(long id)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "Delete from Responces where Id = @Id";
                comm.Parameters.AddWithValue("@Id", id);
                comm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public List<ResponceDTO> Find(string reponce)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Responces where ResponceName Like '%" + reponce + "%'";
                SqlDataReader reader = comm.ExecuteReader();

                List<ResponceDTO> responces = new List<ResponceDTO>();
                while (reader.Read())
                {
                    ResponceDTO responce = new ResponceDTO();

                    responce.Id = (long)reader["Id"];
                    responce.Responce = reader["Responce"].ToString();
                    responces.Add(responce);

                }
                conn.Close();
                return responces;
            }
        }

        public List<ResponceDTO> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Responces";
                SqlDataReader reader = comm.ExecuteReader();

                List<ResponceDTO> responces = new List<ResponceDTO>();
                while (reader.Read())
                {
                    ResponceDTO responce = new ResponceDTO();

                    responce.Id = (long)reader["Id"];
                    responce.Responce = reader["Responce"].ToString();
                    responces.Add(responce);
                }

                return responces;
            }
        }

        public List<ResponceDTO> GetSort(string column = "Responce")
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Responses order by " + column;
                SqlDataReader reader = comm.ExecuteReader();

                List<ResponceDTO> responces = new List<ResponceDTO>();
                while (reader.Read())
                {
                    ResponceDTO responce = new ResponceDTO();

                    responce.Id = (long)reader["Id"];
                    responce.Responce = reader["Responce"].ToString();
                    responces.Add(responce);
                }
                conn.Close();
                return responces;
            }
        }
    }
}
