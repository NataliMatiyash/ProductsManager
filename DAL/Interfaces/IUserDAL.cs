using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserDAL
    {
        List<UserDTO> GetAll();
        List<UserDTO> Find(string fullName);
        List<UserDTO> GetSort(string column = "FullName");
        UserDTO Add(UserDTO user);
        void Delete(int id);
    }
}
