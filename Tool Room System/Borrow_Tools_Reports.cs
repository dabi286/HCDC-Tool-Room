using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool_Room_System
{
    public partial class Borrow_Tools_Reports : Form
    {
        public Borrow_Tools_Reports()
        {
            InitializeComponent();
        }

        private void Borrow_Tools_Reports_Load(object sender, EventArgs e)
        {
            //PASS ALL DATA REPORTS WHERE RETURN_DATE != '' OR RETURN_DATE = ''
            SqlConnection connect = new SqlConnection("Data Source=LAPTOP-P93JFCQB\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
            connect.Open();

            //FUNCTION FROM DATABASE
            SqlCommand cmd = new SqlCommand("SELECT * FROM F_GET_ISSUE_DATE_REPORT()", connect);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();

            //FUNCTION FROM DATABASE
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM F_GET_RETURN_DATE_REPORT()", connect);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            connect.Close();

        }

        //Events textBox in Borrow_Tools_Reports
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1_TextChanged(sender, e);
        }
        private void BorrowEvents()
        {
            try
            {
                //SEARCH for Borrow
                //ADDING CATCH EXCEPTION ON CONVERTING INT TO NVARCHAR
                SqlConnection connect = new SqlConnection("Data Source=LAPTOP-P93JFCQB\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();
                SqlCommand cmd = new SqlCommand("SEARCH_BARROW_TOOLS_REPORT", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Student_ID", SqlDbType.NVarChar).Value = textBox1.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                BorrowEvents();
            }
            else if (textBox1.Text == "")
            {
                BorrowEvents();
            }
        }


        //Events textBox in Return_Tools_Reports
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2_TextChanged(sender, e);
        }

        private void ReturnEvents()
        {
            try
            {
                //SEARCH FOR RETURN
                SqlConnection connect = new SqlConnection("Data Source=LAPTOP-P93JFCQB\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();
                SqlCommand cmd = new SqlCommand("SEARCH_RETURN_TOOLS_REPORT", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Student_ID", SqlDbType.NVarChar).Value = textBox2.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                connect.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                ReturnEvents();
            }
            else if (textBox2.Text == "")
            {
                ReturnEvents();
            }
        }

        //REFRESH DATAGRID 1 AND 2
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=LAPTOP-P93JFCQB\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM F_GET_ISSUE_DATE_REPORT()", connect);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            try
            {
                SqlConnection connect = new SqlConnection("Data Source=LAPTOP-P93JFCQB\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();

                SqlCommand cmd2 = new SqlCommand("SELECT * FROM F_GET_RETURN_DATE_REPORT()", connect);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                dataGridView2.DataSource = dt2;
                connect.Close();

                connect.Close();

                MessageBox.Show("Tools Report List Refreshed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        //Hover RGB (BORROW)
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



        //Hover RGB (Return)
        private void dataGridView2_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int redValue = 40;
                int greenValue = 167;
                int blueValue = 69;

                Color customColor = Color.FromArgb(redValue, greenValue, blueValue);

                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = customColor;
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                dataGridView2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
        }
    }
}
