using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Tool_Room_System
{
    public partial class Borrow1 : Form
    {
        public Borrow1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                connect.Open();

                int userID = Dashboard.RetUserID();

                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please input the Fields");
                }
                else if (textBox2.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Please fill in all the student information fields.");
                }
                else if (textBox8.Text == "")
                {
                    MessageBox.Show("Please select a Item to borrow.");
                }
                else if (string.IsNullOrEmpty(textBox7.Text) || !int.TryParse(textBox7.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.");
                }
                else
                {
                    SqlCommand checkCommand = new SqlCommand("SELECT Count(*) FROM ITEM WHERE Items_ID = @barcode AND status = 'AVAILABLE'",connect);
                    checkCommand.Parameters.AddWithValue("@barcode", textBox3.Text);
                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                            SqlCommand cmd = new SqlCommand("SP_ADDISSUEBOOK", connect);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@Student_ID", SqlDbType.NVarChar).Value = textBox1.Text;
                            cmd.Parameters.Add("@Issue_Date", SqlDbType.NVarChar).Value = dateTimePicker1.Value.ToShortDateString();
                            cmd.Parameters.Add("@Return_Date", SqlDbType.NVarChar).Value = "";
                            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                            cmd.Parameters.Add("@Return_Alert", SqlDbType.NVarChar).Value = "";
                            cmd.Parameters.Add("@Items_ID", SqlDbType.Int).Value = textBox3.Text;
                            cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = userID;
                            cmd.Parameters.Add("@Expected_Return_Date", SqlDbType.NVarChar).Value = dateTimePicker2.Value.ToShortDateString();

                            cmd.ExecuteNonQuery();

                            // Update ADD_TOOLS quantity here

                            MessageBox.Show("Tools Borrowed Successfully!");
                            connect.Close();

                            // Clear the form fields
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            textBox7.Text = "";
                        
                    }
                    else
                    {
                        MessageBox.Show("NOT AVAILABLE");
                    }

                }


            }
            catch (SqlException ex)
            {
                if (ex.Number == 51000)
                {
                    MessageBox.Show(ex.Message);
                }
                else
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }

        }

        //QR SANNER IN STUDENTS IMPLEMENTED ON DATABASE
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                {
                //ADDING CATCH EXCEPTION ON CONVERTING INT TO NVARCHAR
                try
                    {
                    SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
                    connect.Open();
                    SqlCommand cmd = new SqlCommand();
                    string querySelectData = "SELECT * FROM STUDENT where Students_ID ='" + textBox1.Text + "'; ";


                    SqlDataReader reader;

                    cmd.CommandText = querySelectData;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connect;
                    reader = cmd.ExecuteReader();

                    if (reader.Read() == true)
                    {
                        textBox1.Text = reader["Students_ID"].ToString();
                        textBox2.Text = reader["Student_Name"].ToString();
                        textBox6.Text = reader["Email"].ToString();
                        textBox4.Text = reader["Student_Contact"].ToString();
                        textBox5.Text = reader["Department_Name"].ToString();
                        textBox13.Text = reader["Gender"].ToString();
                        textBox14.Text = reader["Address"].ToString();
                        textBox1.SelectAll();
                    }


                    else if (reader.Read() == false)
                    {
                        textBox2.Text = "Not Found";
                        textBox6.Text = "Not Found";
                        textBox4.Text = "Not Found";
                        textBox5.Text = "Not Found";
                        textBox6.Text = "Not Found";
                    }
                    connect.Close();
                    }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }

            }
            }

        //QR SANNER IN TOOLS IMPLEMENTED ON DATABASE
        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
            connect.Open();
            SqlCommand cmd = new SqlCommand();
            string querySelectData = "SELECT i.*, s.* FROM ITEM i, STOCK s  WHERE Status != 'NOT AVAILABLE' AND Quantity != 0 AND i.Items_ID = s.Items_ID AND i.Device_Barcode ='" + textBox12.Text + "'; ";


            SqlDataReader reader;

            cmd.CommandText = querySelectData;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connect;
            reader = cmd.ExecuteReader();

            if (reader.Read() == true)
            {
                textBox8.Text = reader["Item_Name"].ToString();
                textBox9.Text = reader["Categories"].ToString();
                textBox10.Text = reader["Storage_Room"].ToString();
                textBox11.Text = reader["Serial_Number"].ToString();
                textBox3.Text = reader["Items_ID"].ToString();
                textBox15.Text = reader["Period_of_Return"].ToString();
                textBox12.SelectAll();
            }


            else if (reader.Read() == false)
            {
                textBox8.Text = "NOT AVAILABLE";
                textBox9.Text = "NOT AVAILABLE";
                textBox10.Text = "NOT AVAILABLE";
                textBox11.Text = "NOT AVAILABLE";
                textBox3.Text = "";
                textBox15.Text = "";

            }
            connect.Close();
        }

        //LOADED GRID
        private void Borrow1_Load(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection("Data Source=DAVE\\SQLEXPRESS;Initial Catalog=CET_TOOLROOM;Integrated Security=True");
            connect.Open();
            SqlCommand cmd = new SqlCommand("VIEW_TOOLS", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Item_Name", SqlDbType.NVarChar).Value = "";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            connect.Close();
        }


        //REFRESH
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //ADDING CATCH EXCEPTION ON CONVERTING INT TO NVARCHAR
            try
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

                MessageBox.Show("Borrowed tools list refreshed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        //Hover RGB


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                textBox12.Focus();
            }
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                dateTimePicker1.Focus();
            }
            
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox7.Focus();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
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
                        row.DefaultCellStyle.BackColor = Color.Red; 
                        row.DefaultCellStyle.ForeColor = Color.White; 
                        break;
                    }
                }
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            // Reset DateTimePicker to the current date
            dateTimePicker2.Value = DateTime.Now;

            // Call the button1_Click event handler to re-calculate the date
            button2_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if the input in textBox1 is a valid number
            if (int.TryParse(textBox15.Text, out int daysToAdd))
            {
                // Get the current date from the DateTimePicker
                DateTime currentDate = dateTimePicker2.Value;

                // Add the specified number of days to the current date
                DateTime newDate = currentDate.AddDays(daysToAdd);

                // Set the new date in the DateTimePicker
                dateTimePicker2.Value = newDate;
            }
        }
    }
}
