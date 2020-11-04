using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductDAL
    {
        List<ProductDTO> GetAll();
        List<ProductDTO> GetSort(string column = "ProductName");
        List<ProductDTO> Find(string productName);
        ProductDTO Add(ProductDTO product);
        void AddResponceId(string productId, string responceId);
        void Delete(int id);
    }
}
