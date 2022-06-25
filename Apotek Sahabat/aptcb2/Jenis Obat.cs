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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;
        void Bersihkan()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }
        void nomerotomatis()
        {
            int hitung;
            string urutan;
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            conn.Open();
            cmd = new NpgsqlCommand("select id_jenis_obat from jenisObat where id_jenis_obat in(select max(id_jenis_obat) from jenisObat) order by id_jenis_obat desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt16(rd[0]) + 1;
                urutan = "" + hitung;
            }
            else
            {
                urutan = "1";
            }
            rd.Close();
            textBox1.Text = urutan;
            conn.Close();

        }
        void TampilBarang()
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
                conn.Open();
                cmd = new NpgsqlCommand("Select * from jenisObat", conn);
                ds = new DataSet();
                da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds, "jenisObat");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "jenisObat";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch
            {
                MessageBox.Show("erro");
            }
        }
       
        public static class SqlDBHelper
        {
            public static bool ExecuteDataSet(out DataTable dt, string sql, CommandType cmdType, params NpgsqlParameter[] parameters)
            {
                using (DataSet ds = new DataSet())
                using (NpgsqlConnection connStr = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134"))
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connStr))
                {
                    cmd.CommandType = cmdType;
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }

                    try
                    {
                        cmd.Connection.Open();
                        new NpgsqlDataAdapter(cmd).Fill(ds);
                    }
                    catch (NpgsqlException ex)
                    {
                        dt = null;
                        return false;

                    }


                    dt = ds.Tables[0];
                    return true;
                }
            }
            public static bool ExecuteNonQuery(string sql, CommandType cmdType, params NpgsqlParameter[] parameters)
            {
                using (DataSet ds = new DataSet())
                using (NpgsqlConnection connStr = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134"))
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connStr))
                {
                    cmd.CommandType = cmdType;
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }

                    try
                    {

                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (NpgsqlException ex)
                    {

                        return false;

                    }
                    return true;
                }
            }

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("Insert into jenisObat values(@id_jenis_obat,@jenis_obat, @keterangan)",
              CommandType.Text, new NpgsqlParameter("@id_jenis_obat", Convert.ToInt16(textBox1.Text)),
                new NpgsqlParameter("@jenis_obat", textBox2.Text),
                new NpgsqlParameter("@keterangan", textBox4.Text)))
                {
                    textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = "";
                    MessageBox.Show("Data Has been Updated");
                    Form1 fm = new Form1();
                    fm.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("gagal");
                }
            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TampilBarang();
            nomerotomatis();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("Delete from jenisObat where jenis_obat=@jenis_obat", CommandType.Text, new NpgsqlParameter("@jenis_obat", textBox3.Text)))
                {
                    textBox3.Text = "";
                    MessageBox.Show("Data Has been Delete");
                }
            }
            catch
            {
                MessageBox.Show("Salah");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("update jenisObat set id_jenis_obat=@id_jenis_obat,jenis_obat=@jenis_obat, keterangan=@keterangan where id_jenis_obat=@id_jenis_obat",
                 CommandType.Text, new NpgsqlParameter("@id_jenis_obat", Convert.ToInt16(textBox1.Text)),
                new NpgsqlParameter("@jenis_obat", textBox2.Text),
                new NpgsqlParameter("@keterangan", textBox4.Text)))
                {
                    textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = "";
                    MessageBox.Show("Data Has been Updated");
                }

                else
                {
                    MessageBox.Show("gagal");
                }
            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TampilBarang();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 fm = new Form3();
            fm.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 fm = new Form3();
            fm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bersihkan();
        }
    }
}
