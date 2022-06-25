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
    public partial class Supplier : Form
    {
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;
        public Supplier()
        {
            InitializeComponent();
        }

        void cleartext()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

        }
        void nomerotomatis()
        {
            int hitung;
            string urutan;
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            conn.Open();
            cmd = new NpgsqlCommand("select id_supplier from supplier where id_supplier in(select max(id_supplier) from supplier) order by id_supplier desc", conn);
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
            
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void TampilBarang()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand("Select * from supplier", conn);
                ds = new DataSet();
                da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds, "obat");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "obat";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch
            {
                MessageBox.Show("error");
            }
            finally
            {
                conn.Close();
            }
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            nomerotomatis();
            TampilBarang();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("Insert into supplier values(@id_supplier,@nama_perusahaan,@no_telp,@alamat,@kota,@provinsi,@negara)",
              CommandType.Text,
              new NpgsqlParameter("@id_supplier", Convert.ToInt16(textBox1.Text)),
              new NpgsqlParameter("@nama_perusahaan", textBox2.Text),
              new NpgsqlParameter("@no_telp", textBox4.Text),
              new NpgsqlParameter("@alamat", textBox5.Text),
              new NpgsqlParameter("@kota", textBox6.Text),
              new NpgsqlParameter("@provinsi", textBox7.Text),
              new NpgsqlParameter("@negara", textBox8.Text)))

                {
                    textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = "";
                    MessageBox.Show("Data Has been Saved");
                    Supplier fm = new Supplier();
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("Delete from supplier where nama_perusahaan=@nama_perusahaan", CommandType.Text, new NpgsqlParameter("@nama_perusahaan", textBox3.Text)))
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

        private void button6_Click(object sender, EventArgs e)
        {
            TampilBarang();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("update supplier set nama_perusahaan=@nama_perusahaan,no_telp=@no_telp,alamat=@alamat,kota=@kota,provinsi=@provinsi,negara=@negara where id_supplier=@id_supplier",
                CommandType.Text, new NpgsqlParameter("@id_supplier", Convert.ToInt16(textBox1.Text)),
                new NpgsqlParameter("@nama_perusahaan", textBox2.Text),
                new NpgsqlParameter("@no_telp", textBox4.Text),
                new NpgsqlParameter("@alamat", textBox5.Text),
                new NpgsqlParameter("@kota", textBox6.Text),
                new NpgsqlParameter("@provinsi", textBox7.Text),
                new NpgsqlParameter("@negara", textBox8.Text)))
                {
                    textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = "";
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

        private void button4_Click(object sender, EventArgs e)
        {
            cleartext();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home fm = new Home();
            fm.Show();
            this.Hide();
        }
    }
}
