using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"FullName: {FullName} | Gender: {Gender} | Login: {Login} | Password: {Password}";
        }
    }
}
