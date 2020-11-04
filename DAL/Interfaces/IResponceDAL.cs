using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IResponceDAL
    {
        List<ResponceDTO> GetAll();
        List<ResponceDTO> GetSort(string column = "Responce");
        List<ResponceDTO> Find(string reponce);
        ResponceDTO Add(ResponceDTO responce);
        void Delete(long id);
    }
}
