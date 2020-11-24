using BusinessLogic.Concrete;
using DAL.Concrete;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductsManagerWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            userDal = new UserDAL(connectionString);
        }

        private UserManager userManager;
        private UserDAL userDal;
        private string connectionString = @"Data Source=DESKTOP-EA43ILH\MSSQLSERVER02;Initial Catalog=ProductManager;Integrated Security=True";


        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            uint isLogIn = 0;
            UserDTO user = new UserDTO();
            user.Login = txtUsername.Text;
            user.Password = txtPassword.Text;
            isLogIn = userDal.LogIn(user);

            if (isLogIn != 0)
            {
                if (isLogIn == 1)
                {
                    user.Email = connectionString;
                    userManager = new AuthManager(user);
                }
                else
                {
                    user.Email = connectionString;
                    userManager = new AdminManager(user);
                }
                Products tp = new Products(userManager);
                this.Visible = false;
                tp.Show();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Close();
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                Application.Exit();

            }
        }
    }
}
