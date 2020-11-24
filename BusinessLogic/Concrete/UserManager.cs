using DAL.Concrete;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Concrete
{
    abstract public class UserManager
    {
        public bool addRemovePermitions { set; get; }

        protected UserDAL userDal;
        protected ProductDAL productDal;
        protected ResponceDAL responseDal;

        protected UserDTO currentUser;
        public UserManager(UserDTO user)
        {
            currentUser = user;
            userDal = new UserDAL(user.Email);
            productDal = new ProductDAL(user.Email);
            responseDal = new ResponceDAL(user.Email);
        }
        public List<(long ID, string FullName, string Title, string Text)> Find(string str)
        {
            return GetJoinAll(productDal.Find(str));
        }

        public List<(long ID, string FullName, string Title, string Text)> GetAll()
        {
            var topics = productDal.GetAll();


            return GetJoinAll(topics);
        }

        protected List<(long ID, string FullName, string Title, string Text)> GetJoinAll(List<ProductDTO> products)
        {
            var users = userDal.GetAll();
            var responces = responseDal.GetAll();

            var res = from ts in products
                      join us in users on ts.UserId equals us.Id
                      join cs in responces on ts.ResponceId equals cs.Id
                      select new { ID = ts.Id, us.FullName, ts.ProductName , ts.Price, cs.Responce };
            List<(long ID, string FullName, string Title, string Text)> ls = new List<(long ID, string FullName, string Title, string Text)>();
            foreach (var i in res)
            {
                Console.WriteLine($"{i.ID} {i.FullName} \nTitle: {i.ProductName} \nText: {i.Responce}");
                ls.Add((i.ID, i.FullName, i.ProductName, i.Responce));
            }

            return ls;
        }
        public abstract bool AddProduct(string title_, decimal price, string comment_);
        public abstract long DeleteProduct(long id);
    }
}
