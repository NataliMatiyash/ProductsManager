using BusinessLogic.Interfaces;
using DAL.Interfaces;
using DTO;

namespace BusinessLogic.Concrete
{
    public class AuthManager: UserManager
    {
        public AuthManager(UserDTO user) : base(user)
        {
            addRemovePermitions = false;
        }

        public override bool AddProduct(string title_, decimal price, string comment_)
        {
            return false;
        }
        public override long DeleteProduct(long id)
        {
            return -1;
        }
    }
}
