using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductDTO
    {
        public long Id;
        public string ProductName;
        public decimal Price;
        public bool IsBought;
        public long? UserId;
        public long? ResponceId;
    }
}
