using DAL.Concrete;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManager
{
    public class Program
    {
        static private UserDAL userDal_;
        static private ResponceDAL responceDAL;
        static private ProductDAL productDAL;

        static private UserDTO currentUser;
        static uint TryCount = 3;
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;


            string connectionString = @"Data Source=DESKTOP-EA43ILH\MSSQLSERVER02;Initial Catalog=ProductManager;Integrated Security=True";

            userDal_ = new UserDAL(connectionString);
            responceDAL = new ResponceDAL(connectionString);
            productDAL = new ProductDAL(connectionString);


            userDal_.Add(new UserDTO() { FullName="f", Login="d", Password="d" });


            uint isLogIn = LogIn();

            while (currentUser.Id != 0)
            {
                Console.WriteLine("\n\n What do you want to DO?");
                Console.WriteLine("\ntype 'l' to get List of entities");
                Console.WriteLine("type 's' to Sort entity");
                Console.WriteLine("type 'r' to all entitys by user");
                Console.WriteLine("type 'f' to Find entity");
                if (isLogIn == 2)
                {
                    Console.WriteLine("type 'a' to Add entity");
                    Console.WriteLine("type 'r' to make responce");
                }
                Console.WriteLine("type 'o' to logOut");
                Console.WriteLine("type 'q' to Quit");
                try
                {

                    char c = char.Parse(Console.ReadLine());

                    switch (c)
                    {
                        case 'l': //show list of entities
                            {
                                Console.WriteLine("\nList of all entity:");
                                PrintAll();
                            }
                            break;
                        case 's': //
                            {
                                Console.WriteLine("\nSorted by product name:");
                                PrintSorted();
                            }
                            break;
                        case 'a': //create new entity
                            {
                                Console.WriteLine("Add:");
                                AddProduct();
                            }
                            break;
                        case 'r': //remove entity
                            {
                                AllProductsOfUser();
                            }
                            break;

                        case 'f':
                            {
                                Console.WriteLine("Please, enter a word or letters or a price: "); // find by Title
                                string ttl = Console.ReadLine().ToString();
                                Find(ttl);
                            }
                            break;
                        case 'o':
                            {
                                isLogIn = LogIn();
                            }
                            break;
                        case 'q':
                            return;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static private void AllProductsOfUser()
        {
            var products = productDAL.GetAll();
            foreach (var item in products.Where(x => x.UserId == currentUser.Id))
            {
                Console.WriteLine($"{item.Id} {item.ProductName}");
            }

            Console.WriteLine("\n Select id of product: ");
            var id = Console.ReadLine();
            var product = products.Where(x => x.UserId == currentUser.Id).First(x => x.Id == int.Parse(id));
            Console.WriteLine("Enter responce for this product: ");
            string responce = Console.ReadLine();
            if(product.ResponceId != null)
            {
                var resp = responceDAL.Add(new ResponceDTO() { Responce = responce,});
                var myResponce = responceDAL.GetAll().First(x => x.Responce == resp.Responce);
                productDAL.AddResponceId(product.Id.ToString(), myResponce.Id.ToString());
            }
        }

        static private uint LogIn()
        {
            uint isLogIn = 0;

            for (int i = 0; i < TryCount && currentUser == null; i++)
            {
                currentUser = new UserDTO();
                Console.Write("Please enter your login: ");
                currentUser.Login = Console.ReadLine().ToString();

                Console.Write("\nPlease enter your password: ");
                currentUser.Password = Console.ReadLine().ToString();
            
                currentUser = userDal_.LogIn(currentUser);
                Console.WriteLine("user: " + isLogIn.ToString());
            }
            return isLogIn;
        }

        static void Find(string str)
        {
            PrintAll(productDAL.Find(str));
        }

        static void PrintAll()
        {
            var products = productDAL.GetAll();
            PrintAll(products);
        }
        static void PrintSorted()
        {

            var products = productDAL.GetSort();
            PrintAll(products);
        }
        static void PrintAll(List<ProductDTO> products)
        {
            var responces = responceDAL.GetAll();
            var res = from ps in products
                      join resp in responces on ps.ResponceId equals resp.Id
                      select new { ps.Id, ps.ProductName, resp.Responce};
            foreach (var i in res)
            {
                Console.WriteLine($"{i.Id} {i.ProductName} {i.Responce}");
            }
        }

        static private void AddProduct()
        {
            ProductDTO product = new ProductDTO();

            product.UserId = currentUser.Id;

            Console.Write("Please enter Name of product: ");
            product.ProductName = Console.ReadLine().ToString();

            Console.Write("Please enter Price:");
            product.Price = Convert.ToDecimal(Console.ReadLine().ToString());

            productDAL.Add(product);

        }
    }
}
