using BusinessLogic.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductsManagerWF
{
    public partial class Products : Form
    {
        public Products(UserManager user)
        {
            InitializeComponent();
            userManager = user;
            btnDelete.Visible = btnAdd.Visible = userManager.addRemovePermitions;
            updateTable(userManager.GetAll());
        }

        private UserManager userManager;

        private void updateTable(List<(long ID, string FullName, string Title, string Text)> ls)
        {
            dataGridView1.Rows.Clear();
            foreach (var row in ls)
            {
                int rowNumber = dataGridView1.Rows.Add();
                dataGridView1.Rows[rowNumber].Cells["columnID"].Value = row.ID;
                dataGridView1.Rows[rowNumber].Cells["columnName"].Value = row.FullName;
                dataGridView1.Rows[rowNumber].Cells["columnTitle"].Value = row.Title;
                dataGridView1.Rows[rowNumber].Cells["columnText"].Value = row.Text;
            }
        }

        private void LoadData()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=ManagerNews;Integrated Security=True";

            SqlConnection myConnection = new SqlConnection(connectionString);

            myConnection.Open();

            string query = "SELECT * FROM Topics ORDER BY ID";
            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }

            reader.Close();
            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }


        private void btnLogOut_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really LogOut?", "LogOut", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                Application.Restart();

            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                Application.Exit();

            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            string input_title = Microsoft.VisualBasic.Interaction.InputBox("Input Title!", "Add", "Title");
            string input_text = Microsoft.VisualBasic.Interaction.InputBox("Input Text!", "Add", "Text");
            string input_comment = Microsoft.VisualBasic.Interaction.InputBox("Input Comment!", "Add", "Comment");

            if (!string.IsNullOrEmpty(input_title) &&
                !string.IsNullOrEmpty(input_title) &&
                !string.IsNullOrEmpty(input_title) &&
                userManager.AddProduct(input_title, 11, input_comment))
            {
                updateTable(userManager.GetAll());
                //try
                //{

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Incorect input data!\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK,
                //                                 MessageBoxIcon.Error);
                //}
            }
        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            string input_comment = Microsoft.VisualBasic.Interaction.InputBox("What do you want to find? Input Title!", "Find", "Smth from Title");
            if (!string.IsNullOrEmpty(input_comment))
            {
                updateTable(userManager.Find(input_comment));
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            string input_id = Microsoft.VisualBasic.Interaction.InputBox("Input topic id!", "Delete", "ID");

            if (!string.IsNullOrEmpty(input_id))
            {
                try
                {
                    if (userManager.DeleteProduct(Convert.ToInt64(input_id)) >= 0)
                    {
                        updateTable(userManager.GetAll());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Incorrect input data!\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK,
                                                 MessageBoxIcon.Error);
                }
            }
        }
    }
}
