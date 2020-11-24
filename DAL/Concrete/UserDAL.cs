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
    public class UserDAL : IUserDAL
    {

        private string connectionString;

        public UserDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public UserDTO Add(UserDTO user)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    string query = "";
                    query = "Insert into Users(FullName,Gender,[Login],[Password]) " +
                    $"values('{user.FullName}', {Convert.ToByte(user.Gender)},'{user.Login}','{user.Password}')";
                    //foreach (var i in user.Password)
                    //{
                    //    query += i.ToString();
                    //}
                    //query += ")";
                    comm.CommandText = query;
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return user;
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "Delete from Users where Id = @Id";
                comm.Parameters.AddWithValue("@Id", id);
                comm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public List<UserDTO> Find(string fullName)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Users where FullName Like '" + fullName + "%'";
                SqlDataReader reader = comm.ExecuteReader();

                List<UserDTO> users = new List<UserDTO>();
                while (reader.Read())
                {
                    UserDTO userAdd = new UserDTO();

                    userAdd.Id = (long)reader["Id"];
                    userAdd.FullName = reader["FullName"].ToString();
                    userAdd.Gender = (bool)reader["Gender"];
                    userAdd.Login = reader["Login"].ToString();
                    userAdd.Password = reader["Password"].ToString();
                    users.Add(userAdd);
                }
                conn.Close();
                return users;
            }
        }

        public List<UserDTO> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Users";
                SqlDataReader reader = comm.ExecuteReader();

                List<UserDTO> users = new List<UserDTO>();
                while (reader.Read())
                {
                    UserDTO userAdd = new UserDTO();
                    userAdd.Id = (long)reader["Id"];
                    userAdd.FullName = reader["FullName"].ToString();
                    userAdd.Gender = (bool)reader["Gender"];
                    userAdd.Login = reader["Login"].ToString();
                    userAdd.Password = reader["Password"].ToString();
                    users.Add(userAdd);
                }

                return users;
            }
        }

        public List<UserDTO> GetSort(string column = "LastName")
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                conn.Open();
                comm.CommandText = "select * from Users order by " + column;
                SqlDataReader reader = comm.ExecuteReader();

                List<UserDTO> users = new List<UserDTO>();
                while (reader.Read())
                {
                    UserDTO userAdd = new UserDTO();

                    userAdd.Id = (long)reader["Id"];
                    userAdd.FullName = reader["FullName"].ToString();
                    userAdd.Gender = (bool)reader["Gender"];
                    userAdd.Login = reader["Login"].ToString();
                    userAdd.Password = reader["Password"].ToString();
                    users.Add(userAdd);
                }
                conn.Close();
                return users;
            }
        }


        public uint LogIn(UserDTO user)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                SqlDataReader reader;
                try
                {
                    conn.Open();
                    comm.CommandText = "select * from Users" + " where cast(Users.Password as int) = " + user.Password + " And Login like " + user.Login;
                    reader = comm.ExecuteReader();
                    List<UserDTO> users = new List<UserDTO>();
                    if (reader.Read())
                    {
                        if (reader["Login"].ToString() == user.Login && reader["Password"].ToString() == user.Password)
                        {
                            UserDTO userAdd = new UserDTO();

                            userAdd.Id = (long)reader["Id"];
                            userAdd.FullName = reader["FullName"].ToString();
                            userAdd.Gender = (bool)reader["Gender"];
                            userAdd.Login = reader["Login"].ToString();
                            userAdd.Password = reader["Password"].ToString();
                            users.Add(userAdd);

                            conn.Close();
                            return 2;
                        }
                        return 2;
                    }
                    return 0;
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }

            }
        }
    }
}
