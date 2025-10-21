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
    public partial class Add_Tools : Form
    {
        public Add_Tools()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox10.Text == "" || textBox11.Text == "" || textBox12.Text == "")
                {
                    MessageBox.Show("Please Input the Fields");
                }
                else if (!int.TryParse(textBox2.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity");
                }
                else
                {

                    SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("SP_ADD_TOOLS", connect);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Tool_Name", SqlDbType.NVarChar).Value = textBox1.Text;
                    cmd.Parameters.Add("@Date_Entered", SqlDbType.DateTime).Value = dateTimePicker1.Value.ToShortDateString();
                    cmd.Parameters.Add("@Device_Barcode", SqlDbType.NVarChar).Value = textBox3.Text;
                    cmd.Parameters.Add("@Classification", SqlDbType.NVarChar).Value = textBox4.Text;
                    cmd.Parameters.Add("@Tool_Quantity", SqlDbType.NVarChar).Value = textBox2.Text;
                    cmd.Parameters.Add("@Storage_Room", SqlDbType.NVarChar).Value = textBox5.Text;
                    cmd.Parameters.Add("@Serial_Number", SqlDbType.NVarChar).Value = textBox6.Text;
                    cmd.Parameters.Add("@Model", SqlDbType.NVarChar).Value = textBox7.Text;
                    cmd.Parameters.Add("@Location_Storage", SqlDbType.NVarChar).Value = textBox8.Text;
                    cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = comboBox1.Text;
                    cmd.Parameters.Add("@Date_of_Preventive_Maintenance", SqlDbType.NVarChar).Value = textBox10.Text;
                    cmd.Parameters.Add("@For_Calibration", SqlDbType.NVarChar).Value = textBox11.Text;
                    cmd.Parameters.Add("@Date_of_Calibration", SqlDbType.NVarChar).Value = textBox12.Text;
                    cmd.Parameters.Add("@Period_of_Return", SqlDbType.Int).Value = textBox9.Text;

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tools Added!");
                    connect.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                    textBox10.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void Add_Tools_Load(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
            connect.Open();
            SqlCommand cmd = new SqlCommand("SP_REFRESH_BORROW_CS", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        //REFRESH ADD_TOOLS
        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();

                SqlCommand cmd = new SqlCommand("SP_REFRESH_ADD_TOOLS", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                connect.Close();
                MessageBox.Show("Tools refreshed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
