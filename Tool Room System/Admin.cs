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

namespace Tool_Room_System
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connect = new SqlConnection("Data Source=LAPTOP-P93JFCQB\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True"))
            {
                connect.Open();
                if (textBox1.Text == "" || LoginTextBox.Text == "")
                {
                    MessageBox.Show("Please enter a value.");
                    return;
                }

                SqlCommand cmd = new SqlCommand("SP_ADMIN_LOGIN", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                cmd.Parameters.AddWithValue("@Password", LoginTextBox.Text);

                int result = (int)cmd.ExecuteScalar();
                if (result == 1)
                {
                    SqlCommand cmd1 = new SqlCommand("SP_GET_ADMIN_ID", connect);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@username", textBox1.Text);
                    int result1 = (int)cmd1.ExecuteScalar();

                    MessageBox.Show("Login successful!");
                    Admin_control AdminForm = new Admin_control();
                    AdminForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                }
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            LoginTextBox.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BackPictureBtn_Click(object sender, EventArgs e)
        {
            Form1 Dash = new Form1();
            Dash.Show();
            this.Hide();
        }
    }
}
