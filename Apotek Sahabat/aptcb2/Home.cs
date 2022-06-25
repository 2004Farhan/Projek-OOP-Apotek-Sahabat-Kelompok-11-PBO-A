using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aptcb2
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 fm = new Form3();
            fm.Show();
            this.Hide();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Transaksi fm = new Transaksi();
            fm.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Supplier fm = new Supplier();
            fm.Show();
            this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form7 fm = new Form7();
            fm.Show();
            this.Hide();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            about fm = new about();
            fm.Show();
            this.Hide();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Laporan fm = new Laporan();
            fm.Show();
            this.Hide();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
