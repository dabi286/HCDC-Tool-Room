using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Tool_Room_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connect = new SqlConnection("Data Source=LAPTOP-P93JFCQB\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True"))
            {
                connect.Open();
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Please enter a value.");
                    return;
                }

                SqlCommand cmd = new SqlCommand("SP_USERS_LOGIN", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                cmd.Parameters.AddWithValue("@Password", textBox2.Text);

                int result = (int)cmd.ExecuteScalar();
                if (result == 1)
                {
                    SqlCommand cmd1 = new SqlCommand("SP_GET_USERID", connect);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@username", textBox1.Text);
                    int result1 = (int)cmd1.ExecuteScalar();

                    MessageBox.Show("Login successful!");
                    Dashboard mainform = new Dashboard(result1);
                    mainform.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                }
            }
        }


        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admin nextForm = new Admin();
            nextForm.Show();
            this.Hide(); 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
