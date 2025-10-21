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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tool_Room_System
{
    public partial class Return_Tools : Form
    {
        public Return_Tools()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }

        //LOAD
        private void Return_Tools_Load(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
            connect.Open();

            SqlCommand cmd = new SqlCommand("SP_LOAD_DATA_RETURN_TOOLS", connect);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            connect.Close();
        }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox2.Text = row.Cells[0].Value.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please input the Fields");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please Select number from the Table");
            }
            else if (string.IsNullOrEmpty(textBox1.Text) || !int.TryParse(textBox2.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity to return.");
            }
            else if (!int.TryParse(textBox2.Text, out int issueId) || issueId <= 0)
            {
                MessageBox.Show("Please select a valid issue ID.");
            }
            else
            {
                SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();

                SqlCommand cmd = new SqlCommand("UPDATE_ISSUETOOL", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Issue_ID", SqlDbType.Int).Value = issueId;  // Include the Issue_ID parameter
                cmd.Parameters.Add("@Return_Date", SqlDbType.NVarChar).Value = dateTimePicker1.Value.ToShortDateString();
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;  // Include the quantity parameter

                cmd.ExecuteNonQuery();
                MessageBox.Show("Tools Returned Successfully!");
                connect.Close();
            }
        }


        //Applying textBox1_TextChanged Events by scanning of Barcode
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                ExecuteCode();
            }
            else if (textBox1.Text == "")
            {
                ExecuteCode();
            }
        }

        private void ExecuteCode()
        {
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();

                SqlCommand cmd = new SqlCommand("SP_VIEWISSUEBOOK", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID_Number", SqlDbType.NVarChar).Value = textBox1.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                connect.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        //Hover RGB
        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int redValue = 40;
                int greenValue = 167;
                int blueValue = 69;

                Color customColor = Color.FromArgb(redValue, greenValue, blueValue);

                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = customColor;
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        //Refresh
        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();

                SqlCommand cmd = new SqlCommand("SP_LOAD_DATA_RETURN_TOOLS", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                connect.Close();
                MessageBox.Show("Issued Tools refreshed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
