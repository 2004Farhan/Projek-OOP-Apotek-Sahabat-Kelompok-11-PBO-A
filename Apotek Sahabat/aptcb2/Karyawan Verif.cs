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
    public partial class Form7 : Form
    {
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;
        public Form7()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home fm = new Home();
            fm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            try
            {

                conn.Open();
                cmd = new NpgsqlCommand("select * from karyawan where username='" + txtusername.Text + "' and password='" + txtpassword.Text + "'", conn);
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    Karyawan fm = new Karyawan();
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

        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
