using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool_Room_System
{
    public partial class Admin_control : Form
    {
        public Admin_control()
        {
            InitializeComponent();
        }

        private void ExitLblBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RegisterStaffBtn_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both Username and Password.");
                return;
            }
            try
            {
                using (SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_REGISTER_USERS", connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        connect.Open();
                        cmd.ExecuteNonQuery();
                        connect.Close();

                        MessageBox.Show("Staff registered successfully!");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void Admin_control_Load(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
            connect.Open();

            SqlCommand cmd = new SqlCommand("SP_STATUS_TABLE", connect);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            connect.Close();

            SqlCommand cmd2 = new SqlCommand("VIEW_USERS", connect);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            connect.Close();
        }

        private void UpdateStatusBtn_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(TextBoxStatus.Text, out int itemsID) || itemsID <= 0)
            {
                MessageBox.Show("Please enter a valid positive integer for Items ID.");
            }
            else if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a status from the dropdown.");
            }
            else
            {
                using (SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True"))
                {
                    connect.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_UPDATE_ITEM_STATUS", connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Items_ID", SqlDbType.Int).Value = itemsID;
                        cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = comboBox1.SelectedItem.ToString();

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Status Updated Successfully!");
                    }
                }
            }
        }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                TextBoxStatus.Text = row.Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void RegisterAdminBtn_Click(object sender, EventArgs e)
        {
            string username = textBox3.Text;
            string password = textBox4.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both Username and Password.");
                return;
            }
            try
            {
                using (SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_ADMIN_REGISTER", connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        connect.Open();
                        cmd.ExecuteNonQuery();
                        connect.Close();

                        MessageBox.Show("Admin registered successfully!");
                        textBox3.Text = "";
                        textBox4.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().Equals("NOT AVAILABLE", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = Color.Red; // Change to any color you prefer
                        row.DefaultCellStyle.ForeColor = Color.White; // Change text color for better visibility
                        break; // If you want to color only once in the row, remove this line if you want to color the entire row where any cell has the text
                    }
                }
            }
        }

        private void ExecuteCode()
        {
            SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
            connect.Open();
            SqlCommand cmd = new SqlCommand("SP_SEARCH_STATUS", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Item_Name", SqlDbType.NVarChar).Value = textBox5.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                ExecuteCode();
            }
            else if (textBox5.Text == "")
            {
                ExecuteCode();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox5_TextChanged(sender, e);
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
            connect.Open();

            SqlCommand cmd = new SqlCommand("SP_STATUS_TABLE", connect);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            MessageBox.Show("Tools Refresh Succesfully!");

            connect.Close();
        }

        private void RefreshBtnAU_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
            connect.Open();

            SqlCommand cmd2 = new SqlCommand("VIEW_USERS", connect);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            MessageBox.Show("Admin/Users Refresh successfully!");

            connect.Close();
        }

        private void RemoveToolBtn_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(TextBoxStatus.Text, out int stocksID) || stocksID <= 0)
            {
                MessageBox.Show("Please enter a valid positive integer for Items ID.");
            }
            else
            {
                using (SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True"))
                {
                    connect.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_DELETE_STOCK", connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Stocks_ID", SqlDbType.Int).Value = stocksID;

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tool Remove Successfully!");
                    }
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 Dash = new Form1();
            Dash.Show();
            this.Hide();
        }
    }
}
