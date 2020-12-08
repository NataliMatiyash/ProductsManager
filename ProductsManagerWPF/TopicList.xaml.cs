using BusinessLogic.Concrete;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductsManagerWPF
{
    public class Itm
    {
        public long ID { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
    /// <summary>
    /// Interaction logic for TopicList.xaml
    /// </summary>
    public partial class TopicList : Window
    {
        public TopicList()
        {
            InitializeComponent();
        }
        public TopicList(UserManager user)
        {
            InitializeComponent();
            userManager = user;
            btnDelete.Visibility = btnAdd.Visibility = (userManager.addRemovePermitions ? Visibility.Visible : Visibility.Hidden);
            updateTable(userManager.GetAll());
        }

        private UserManager userManager;

        private void updateTable(List<(long ID, string FullName, string Title, string Text)> ls)
        {
            dataGridView1.Items.Clear();
            foreach (var row in ls)
            {
                Itm it = new Itm();
                it.ID = row.ID;
                it.FullName = row.FullName;
                it.Title = row.Title;
                it.Text = row.Text;
                dataGridView1.Items.Add(it);
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
                dataGridView1.Items.Add(s);
        }


        private void ButtonLogOut_Click(object sender, EventArgs e)
        {
            var mv = new MainWindow();
            mv.Show();
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
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

        private void ButtonFind_Click(object sender, EventArgs e)
        {
            string input_comment = Microsoft.VisualBasic.Interaction.InputBox("What do you want to find? Input Title!", "Find", "Smth from Title");
            if (!string.IsNullOrEmpty(input_comment))
            {
                updateTable(userManager.Find(input_comment));
            }
        }

        private void ButtonDeleteTopic_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Incorrect input data!\n" + ex.Message.ToString(), "Error");
                }
            }
        }
    }
}
