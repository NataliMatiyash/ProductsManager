using BusinessLogic.Interfaces;
using DAL.Interfaces;

namespace BusinessLogic.Concrete
{
    public class AuthManager: IAuthManager
    {
        private readonly IUserDAL _userDal;
        public AuthManager(IUserDAL userDal)
        {
            _userDal = userDal;
        }
        public bool Login(string username, string password)
        {
            //return _userDal.Login(username, password);
            return false;
        }
    }
}
