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
    public partial class Add_Students : Form
    {
        public Add_Students()
        {
            InitializeComponent();
        }

        //PANEL1
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox7.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("Please Input the Fields");
            }
            else
            {
                SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();

                SqlCommand cmd = new SqlCommand("SP_ADD_STUDENTS", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Students_ID", SqlDbType.NVarChar).Value = textBox1.Text;
                cmd.Parameters.Add("@Student_Name", SqlDbType.NVarChar).Value = textBox2.Text;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = textBox5.Text;
                cmd.Parameters.Add("@Student_Contact", SqlDbType.NVarChar).Value = textBox4.Text;
                cmd.Parameters.Add("@Department_Name", SqlDbType.NVarChar).Value = textBox3.Text;
                cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = textBox7.Text;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = textBox8.Text;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Details Added!");
                connect.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
            }
        }

        //PANEL 2
        private void Add_Students_Load(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
            connect.Open();
            SqlCommand cmd = new SqlCommand("SP_VIEW_STUDENTS", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Students_ID", SqlDbType.NVarChar).Value = "";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != "")
            {
                ExecudeCode();
            }
            else if (textBox6.Text == "")
            {
                ExecudeCode();
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            textBox6_TextChanged(sender, e);
        }

        private void ExecudeCode()
        {
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();
                SqlCommand cmd = new SqlCommand("SP_VIEW_STUDENTS", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Students_ID", SqlDbType.NVarChar).Value = textBox6.Text;

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

        //REFRESH
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();

                SqlCommand cmd = new SqlCommand("SP_REFRESH_VIEW_STUDENTS", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                connect.Close();
                MessageBox.Show("Students list refreshed successfully!");
            }
            catch (Exception ex)
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
    }
}
