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
    public partial class Karyawan : Form
    {
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;
        public Karyawan()
        {
            InitializeComponent();
        }
        void cleartext()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";

        }
        void nomerotomatis()
        {
            int hitung;
            string urutan;
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            conn.Open();
            cmd = new NpgsqlCommand("select id_karyawan from karyawan where id_karyawan in(select max(id_karyawan) from karyawan) order by id_karyawan desc", conn);
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
        private void button3_Click(object sender, EventArgs e)
        {
            Home fm = new Home();
            fm.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            
        }
        void TampilBarang()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand("Select * from karyawan", conn);
                ds = new DataSet();
                da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds, "karyawan");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "karyawan";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch
            {
                MessageBox.Show("erro");
            }
            finally
            {
                conn.Close();
            }
        }
        private void Form8_Load(object sender, EventArgs e)
        {
            TampilBarang();
            nomerotomatis();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("Delete from karyawan where id_karyawan=@id_karyawan", CommandType.Text, new NpgsqlParameter("@id_karyawan", Convert.ToInt16(textBox3.Text))))
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

        private void button4_Click(object sender, EventArgs e)
        {
            cleartext();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TampilBarang();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("update karyawan set nama_karyawan=@nama_karyawan,jabatan=@jabatan,tanggal_lahir=@tanggal_lahir,tanggal_masuk=@tanggal_masuk,no_telp=@no_telp,alamat=@alamat,kota=@kota,provinsi=@provinsi,username=@username,password=@password where id_karyawan=@id_karyawan",
                CommandType.Text,
                new NpgsqlParameter("@id_karyawan", Convert.ToInt16(textBox1.Text)),
                new NpgsqlParameter("@nama_karyawan", textBox2.Text),
                new NpgsqlParameter("@jabatan", textBox4.Text),
                new NpgsqlParameter("@tanggal_lahir", dateTimePicker1.Value),
                new NpgsqlParameter("@tanggal_masuk", tanggal.Value),
                new NpgsqlParameter("@no_telp", textBox7.Text),
                new NpgsqlParameter("@alamat", textBox8.Text),
                new NpgsqlParameter("@kota", textBox9.Text),
                new NpgsqlParameter("@provinsi", textBox10.Text),
                new NpgsqlParameter("@username", textBox11.Text),
                new NpgsqlParameter("@password", textBox12.Text)))
                {
                    textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = ""; dateTimePicker1.Text = ""; tanggal.Text = ""; textBox7.Text = ""; textBox8.Text = ""; textBox9.Text = ""; textBox10.Text = ""; textBox11.Text = ""; textBox12.Text = "";
                    MessageBox.Show("Data Has been Saved");
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("Insert into karyawan values(@id_karyawan,@nama_karyawan,@jabatan,@tanggal_lahir,@tanggal_masuk,@no_telp,@alamat,@kota,@provinsi,@username,@password)",
                CommandType.Text,
                new NpgsqlParameter("@id_karyawan", Convert.ToInt16(textBox1.Text)),
                new NpgsqlParameter("@nama_karyawan", textBox2.Text),
                new NpgsqlParameter("@jabatan", textBox4.Text),
                new NpgsqlParameter("@tanggal_lahir", dateTimePicker1.Value),
                new NpgsqlParameter("@tanggal_masuk", tanggal.Value),
                new NpgsqlParameter("@no_telp", textBox7.Text),
                new NpgsqlParameter("@alamat", textBox8.Text),
                new NpgsqlParameter("@kota", textBox9.Text),
                new NpgsqlParameter("@provinsi", textBox10.Text),
                new NpgsqlParameter("@username", textBox11.Text),
                new NpgsqlParameter("@password", textBox12.Text)))
                {
                    textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = ""; dateTimePicker1.Text = ""; tanggal.Text = ""; textBox7.Text = ""; textBox8.Text = ""; textBox9.Text = ""; textBox10.Text = ""; textBox11.Text = ""; textBox12.Text = "";
                    MessageBox.Show("Data Has been Saved");
                    Karyawan fm = new Karyawan();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home fm = new Home();
            fm.Show();
            this.Hide();
        }
    }
}
