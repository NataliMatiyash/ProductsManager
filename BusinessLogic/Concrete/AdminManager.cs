using BusinessLogic.Interfaces;
using DAL.Concrete;
using DAL.Interfaces;
using DTO;

namespace BusinessLogic.Concrete
{
    public class AdminManager:UserManager
    {

        public AdminManager(UserDTO user) : base(user)
        {
            addRemovePermitions = true;
        }

        public override bool AddProduct(string title_, decimal price, string comment_)
        {
            ProductDTO product = new ProductDTO();

            product.UserId = currentUser.Id;

            product.ProductName = title_;
            product.Price = price;

            ResponceDTO comment = new ResponceDTO();

            comment.Responce = comment_;

            responseDal.Add(comment);

            var comms = responseDal.Find(comment.Responce);

            product.ResponceId = comms[0].Id;
            productDal.Add(product);
            return true;
        }
        public override long DeleteProduct(long id)
        {
            return productDal.Delete(id);
        }
    }
}
