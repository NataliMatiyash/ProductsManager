using BusinessLogic.Interfaces;
using DAL.Concrete;
using DAL.Interfaces;

namespace BusinessLogic.Concrete
{
    public class AdminManager:IAdminManager
    {
        private readonly IResponceDAL _responceDal;
        private readonly IProductDAL _productDal;
        private readonly IUserDAL _userDal;
           
        public AdminManager(IResponceDAL responceDal, IProductDAL productDal, IUserDAL userDal)
        {
            _responceDal = responceDal;
            _productDal = productDal;
            _userDal = userDal;
        }
    }
}
