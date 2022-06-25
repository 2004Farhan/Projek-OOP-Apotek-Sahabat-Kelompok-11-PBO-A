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
    public partial class Form3 : Form
    {
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;
        void Bersihkan()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox2.Text = "";
        }
        public Form3()
        {
            InitializeComponent();
        }
        void Fillcombo()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            try
            {
                conn.Open();
                NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select id_jenis_obat from jenisObat";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox1.Items.Add(dr["id_jenis_obat"]);
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        void Fillcombo2()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            try
            {
                conn.Open();
                NpgsqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select id_supplier from supplier";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["id_supplier"]);
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        void nomerotomatis()
        {
            int hitung;
            string urutan;
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            conn.Open();
            cmd = new NpgsqlCommand("select id_obat from obat where id_obat in(select max(id_obat) from obat) order by id_obat desc", conn);
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
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form12 fm = new Form12();
            fm.Show();
            this.Hide();
        }
        void TampilBarang()
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlDBHelper.ExecuteNonQuery("Insert into obat values(@id_obat,@id_jenis_obat,@id_supplier,@nama_obat,@kuantitas_per_unit,@harga,@stok,@discontinue)",
              CommandType.Text, new NpgsqlParameter("@id_obat", Convert.ToInt16(textBox1.Text)),
                new NpgsqlParameter("@nama_obat", textBox2.Text),
                new NpgsqlParameter("@kuantitas_per_unit", Convert.ToInt16(textBox4.Text)),
                new NpgsqlParameter("@harga", Convert.ToInt32(textBox5.Text)),
                new NpgsqlParameter("@stok", Convert.ToInt32(textBox6.Text)),
                new NpgsqlParameter("@discontinue", Convert.ToInt32(textBox7.Text)),
                new NpgsqlParameter("@id_jenis_obat", Convert.ToInt16(comboBox1.Text)),
                new NpgsqlParameter("@id_supplier", Convert.ToInt16(comboBox2.Text))))
                {
                    textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; comboBox1.Text = ""; comboBox2.Text = "";
                    MessageBox.Show("Data Has been Updated");
                    Form3 fm = new Form3();
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
                if (SqlDBHelper.ExecuteNonQuery("Delete from obat where nama_obat=@nama_obat", CommandType.Text, new NpgsqlParameter("@nama_obat", textBox3.Text)))
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
       
        private void Form3_Load(object sender, EventArgs e)
        {
            Fillcombo2();
            Fillcombo();
            nomerotomatis();
            TampilBarang();
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
                if (SqlDBHelper.ExecuteNonQuery("update obat set id_jenis_obat=@id_jenis_obat,id_supplier=@id_supplier,nama_obat=@nama_obat,kuantitas_per_unit=@kuantitas_per_unit,harga=@harga,stok=@stok,discontinue=@discontinue where id_obat=@id_obat",
                CommandType.Text, new NpgsqlParameter("@id_obat", Convert.ToInt16(textBox1.Text)),
                new NpgsqlParameter("@nama_obat", textBox2.Text),
                new NpgsqlParameter("@kuantitas_per_unit", Convert.ToInt16(textBox4.Text)),
                new NpgsqlParameter("@harga", Convert.ToInt32(textBox5.Text)),
                new NpgsqlParameter("@stok", Convert.ToInt32(textBox6.Text)),
                new NpgsqlParameter("@discontinue", Convert.ToInt32(textBox7.Text)),
                new NpgsqlParameter("@id_jenis_obat", Convert.ToInt16(comboBox1.Text)),
                new NpgsqlParameter("@id_supplier", Convert.ToInt16(comboBox2.Text))))
                {
                    textBox1.Text = ""; textBox2.Text = ""; textBox4.Text = ""; textBox5.Text = ""; textBox6.Text = ""; textBox7.Text = ""; comboBox1.Text = ""; comboBox2.Text = "";
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

        private void button7_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TampilBarang();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home fm = new Home();
            fm.Show();
            this.Hide();
        }
    }
}
