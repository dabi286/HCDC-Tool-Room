using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tool_Room_System
{
    public partial class Dashboard : Form
    {
        private static int usertID;
        public Dashboard(int userid)
        {
            InitializeComponent();
            usertID = userid;
        }

        public static int RetUserID()
        {
            return usertID;
        }


        private Add_Tools Add_Tools;
        private Borrow1 Borrow1;
        private Borrow_Tools_Reports borrow6;
        private View_Tools View_T;
        private Add_Students Add_S;
        private Return_Tools Return_T;

        private void button1_Click(object sender, EventArgs e)
        {
            if (Add_Tools == null || Add_Tools.IsDisposed)
            {
                Add_Tools = new Add_Tools();
                Add_Tools.TopLevel = false;
                panel3.Controls.Add(Add_Tools);
            }

            if (Add_Tools != null)
            {
                Add_Tools.Hide();
            }

            Add_Tools.BringToFront();
            Add_Tools.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (View_T == null || View_T.IsDisposed)
            {
                View_T = new View_Tools();
                View_T.TopLevel = false;
                panel3.Controls.Add(View_T);
            }

            if (View_T != null)
            {
                View_T.Hide();
            }

            View_T.BringToFront();
            View_T.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Add_S == null || Add_S.IsDisposed)
            {
                Add_S = new Add_Students();
                Add_S.TopLevel = false;
                panel3.Controls.Add(Add_S);
            }

            if (Add_S != null)
            {
                Add_S.Hide();
            }

            Add_S.BringToFront();
            Add_S.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Borrow1 == null || Borrow1.IsDisposed)
            {
                Borrow1 = new Borrow1();
                Borrow1.TopLevel = false;
                panel3.Controls.Add(Borrow1);
            }

            if (Borrow1 != null)
            {
                Borrow1.Hide();
            }

            Borrow1.BringToFront();
            Borrow1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Return_T == null || Return_T.IsDisposed)
            {
                Return_T = new Return_Tools();
                Return_T.TopLevel = false;
                panel3.Controls.Add(Return_T);
            }

            if (Return_T != null)
            {
                Return_T.Hide();
            }

            Return_T.BringToFront();
            Return_T.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (borrow6 == null || borrow6.IsDisposed)
            {
                borrow6 = new Borrow_Tools_Reports();
                borrow6.TopLevel = false;
                panel3.Controls.Add(borrow6);
            }

            if (borrow6 != null)
            {
                borrow6.Hide();
            }

            borrow6.BringToFront();
            borrow6.Show();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            Form1 Dash = new Form1();
            Dash.Show();
            this.Hide();
        }
    }
}
