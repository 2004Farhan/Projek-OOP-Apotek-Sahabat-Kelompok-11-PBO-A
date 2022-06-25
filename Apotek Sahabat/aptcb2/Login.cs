using Npgsql;
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
    public partial class Login : Form
    {
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;

        public Login()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
                conn.Open();
                cmd = new NpgsqlCommand("select * from karyawan where username='" + txtusername.Text + "' and password='" + txtpassword.Text + "'", conn);
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    Home fm = new Home();
                    fm.Show();
                    this.Hide();
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Masukkan dengan benar");
                }
            }
            catch
            {
                MessageBox.Show("Input yang bener");
            }
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtusername_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
