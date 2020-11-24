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
    public class ProductDAL : IProductDAL
    {
        private string connectionString;

        public ProductDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ProductDTO Add(ProductDTO product)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    string query = "";
                    query = "Insert into Products(ProductName,Price,IsBought,ResponceId,UserId) " +
                    $"values('{product.ProductName}',{product.Price},{product.IsBought},{product.ResponceId},{product.UserId})";
                    comm.CommandText = query;
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ProductDTO ProductDTO = new ProductDTO();
                ProductDTO.Id = -1;
                return ProductDTO;
            }
            return product;
        }

        public void AddResponceId(string productId, string responceId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    string query = "";
                    query = $"update Products set ResponceId = {responceId} where Id = {productId};";
                    comm.CommandText = query;
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
            }
        }

        public long Delete(long id)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    comm.CommandText = "Delete from Produts where Id = @Id";
                    comm.Parameters.AddWithValue("@Id", id);
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public List<ProductDTO> Find(string productName)
        {
            List<ProductDTO> Products = new List<ProductDTO>();
            try
            {

                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    comm.CommandText = "select * from Products where ProductName Like '%" + productName + "%'";
                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductDTO productAdd = new ProductDTO();
                        productAdd.Id = (long)reader["Id"];
                        productAdd.UserId = (long)reader["UserId"];
                        productAdd.ResponceId = (long)reader["ResponceId"];
                        productAdd.ProductName = reader["ProductName"].ToString();
                        productAdd.Price = (decimal)reader["Price"];
                        productAdd.IsBought = (bool)reader["IsBought"];
                        Products.Add(productAdd);

                    }
                    conn.Close();
                    return Products;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ProductDTO productAdd = new ProductDTO();
                productAdd.Id = -1;
                Products.Add(productAdd);
                return Products;
            }
        }

        public List<ProductDTO> GetAll()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    comm.CommandText = "select * from Products";
                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductDTO productAdd = new ProductDTO();
                        productAdd.Id = (long)reader["Id"];
                        productAdd.ResponceId = (long)reader["ResponceId"];
                        productAdd.UserId = (long)reader["UserId"];
                        productAdd.Price = (decimal)reader["Price"];
                        productAdd.IsBought = (bool)reader["IsBought"];
                        productAdd.ProductName = reader["ProductName"].ToString();
                        products.Add(productAdd);
                    }

                    return products;
                }
            }
            catch (Exception e)
            {
                ProductDTO ProductAdd = new ProductDTO();
                ProductAdd.Id = -1;
                Console.WriteLine(e.Message);
                products.Clear();
                products.Add(ProductAdd);
                return products;
            }
        }

        public List<ProductDTO> GetSort(string column = "ProductName")
        {
            List<ProductDTO> products = new List<ProductDTO>();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    comm.CommandText = "select * from Products order by " + column;
                    SqlDataReader reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductDTO productAdd = new ProductDTO();
                        productAdd.Id = (long)reader["Id"];
                        productAdd.ResponceId = (long)reader["ResponceId"];
                        productAdd.UserId = (long)reader["UserId"];
                        productAdd.Price = (decimal)reader["Price"];
                        productAdd.IsBought = (bool)reader["IsBought"];
                        productAdd.ProductName = reader["ProductName"].ToString();
                        products.Add(productAdd);
                    }
                    conn.Close();
                    return products;
                }
            }
            catch (Exception e)
            {
                ProductDTO productAdd = new ProductDTO();
                productAdd.Id = -1;
                Console.WriteLine(e.Message);
                products.Clear();
                products.Add(productAdd);
                return products;
            }
        }
    }
}
