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
    public partial class Initial_Dashboard : Form
    {
        public Initial_Dashboard()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 6;

            if (panel2.Width >= 800)
            {
                timer1.Stop();

                Form1 Dash = new Form1();
                Dash.Show();
                this.Hide();
            }
        }


    }
}
