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
    public partial class Form12 : Form
    {
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;
        void Bersihkan()
        {
            textBox2.Text = "";
            textBox4.Text = "";
            textBox7.Text = "";
        }
        void Fillcombo()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            try
            {
               conn.Open();
                NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select id_obat from obat";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["id_obat"]);
                }
            }
            catch
            {

            }
        }
        void TampilBarang()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand("Select * from historyHargaObat", conn);
                ds = new DataSet();
                da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds, "historyHargaObat");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "historyHargaObat";
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
        void nomerotomatis()
        {
            int hitung;
            string urutan;
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            conn.Open();
            cmd = new NpgsqlCommand("select id_history_harga from historyHargaObat where id_history_harga in(select max(id_history_harga) from historyHargaObat) order by id_history_harga desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt16(rd[0]) + 1;
                urutan = ""+hitung;
            }
            else
            {
                urutan = "1";
            }
            rd.Close();
            textBox2.Text = urutan;
            conn.Close();

        }
        public Form12()
        {
            InitializeComponent();
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            Fillcombo();
            TampilBarang();
            nomerotomatis();
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
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("Insert into historyHargaObat values(@id_history_harga,@id_obat,@harga,@mulai_berlaku,@akhir_berlaku,@status)",
                CommandType.Text,
                new NpgsqlParameter("@id_history_harga", Convert.ToInt16(textBox2.Text)),
                new NpgsqlParameter("@harga", textBox4.Text),
                new NpgsqlParameter("@mulai_berlaku", dateTimePicker1.Value),
                new NpgsqlParameter("@akhir_berlaku", tanggal.Value),
                new NpgsqlParameter("@id_obat", Convert.ToInt16(comboBox1.Text)),
                new NpgsqlParameter("@status", textBox7.Text)))
                {
                    textBox2.Text = ""; textBox4.Text = ""; dateTimePicker1.Text = ""; tanggal.Text = ""; comboBox1.Text = ""; textBox7.Text = "";
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
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("update historyHargaObat set id_obat=@id_obat,harga=@harga,mulai_berlaku=@mulai_berlaku,akhir_berlaku=@akhir_berlaku,status=@status where id_history_harga=@id_history_harga",
                CommandType.Text,
                new NpgsqlParameter("@id_history_harga", Convert.ToInt16(textBox2.Text)),
                new NpgsqlParameter("@id_obat", Convert.ToInt16(comboBox1.Text)),
                new NpgsqlParameter("@harga", textBox4.Text),
                new NpgsqlParameter("@mulai_berlaku", dateTimePicker1.Value),
                new NpgsqlParameter("@akhir_berlaku", tanggal.Value),
                new NpgsqlParameter("@status", textBox7.Text)))
                {
                    textBox2.Text = ""; comboBox1.Text = ""; textBox4.Text = ""; dateTimePicker1.Text = ""; tanggal.Text = ""; textBox7.Text = "";
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("Delete from historyHargaObat where id_history_harga=@id_history_harga", CommandType.Text, new NpgsqlParameter("@id_history_harga", Convert.ToInt16(textBox3.Text))))
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

        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand("Select * from obat", conn);
                ds = new DataSet();
                da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds, "obat");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "obat";
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

        private void button5_Click(object sender, EventArgs e)
        {
            TampilBarang();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Bersihkan();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 fm = new Form3();
            fm.Show();
            this.Hide();
        }
    }
}
