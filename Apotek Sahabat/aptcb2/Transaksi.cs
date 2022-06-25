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
    public partial class Transaksi : Form
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataRow dr;
        DataRow dp;
        private NpgsqlCommand cmd;
        private DataSet ds;
        private NpgsqlDataAdapter da;
        private NpgsqlDataReader rd;
        public Transaksi()
        {
            InitializeComponent();
            dt.Columns.Add("Id detail transaksi");
            dt.Columns.Add("Id transaksi");
            dt.Columns.Add("Id obat");
            dt.Columns.Add("harga jual");
            dt.Columns.Add("kuantitas");
            dt.Columns.Add("diskon");
            dt.Columns.Add("total");
            dt2.Columns.Add("id transaksi");
            dt2.Columns.Add("tanggal");
            dt2.Columns.Add("id jenis transaksi");
            dt2.Columns.Add("id karyawan");
            dt2.Columns.Add("id customer");
        }
        void tampil()
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand("Select id_obat,nama_obat,stok,harga from obat", conn);
                ds = new DataSet();
                da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds, "obat");
                dataGridView3.DataSource = ds;
                dataGridView3.DataMember = "obat";
                
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
        private void button3_Click(object sender, EventArgs e)
        {
            
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
 
        private void Tambah_Click(object sender, EventArgs e)
        {
            int n = 0, total = (Convert.ToInt32(textBox6.Text) * Convert.ToInt32(textBox7.Text)) - (Convert.ToInt32(textBox7.Text) * Convert.ToInt32(textBox4.Text));
            int Grdtotal = 0;
            dr = dt.NewRow();
            dr["Id detail transaksi"]= Convert.ToInt16(textBox9.Text);
            dr["Id transaksi"]= Convert.ToInt16(textBox1.Text);
            dr["Id obat"] = Convert.ToInt16(textBox2.Text);
            dr["harga jual"] = Convert.ToInt32(textBox6.Text);
            dr["kuantitas"] = Convert.ToInt16(textBox7.Text);
            dr["diskon"] = Convert.ToInt32(textBox4.Text);
            dr["total"] = total;
            dt.Rows.Add(dr);
            dataGridView1.DataSource = dt;
            textBox9.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox4.Text = "";
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                Grdtotal = Grdtotal + Convert.ToInt32(dataGridView1.Rows[row].Cells[6].Value);
            }
            richTextBox1.Text = "Rp. " + Grdtotal;


        }
        private void button2_Click(object sender, EventArgs e)
        {
            int qty = 0;
            string pname = "";
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                if (SqlDBHelper.ExecuteNonQuery("insert into detailTransaksi values(@id_detail_transaksi,@id_transaksi,@id_obat,@harga_jual,@kuantitas,@diskon)",
                    CommandType.Text,
                new NpgsqlParameter("@id_detail_transaksi", Convert.ToInt16(dr.Cells["Id detail transaksi"].Value)),
                new NpgsqlParameter("@id_transaksi", Convert.ToInt16(dr.Cells["Id transaksi"].Value)),
                new NpgsqlParameter("@id_obat", Convert.ToInt16(dr.Cells["Id obat"].Value)),
                new NpgsqlParameter("@harga_jual", Convert.ToInt32(dr.Cells["harga jual"].Value)),
                new NpgsqlParameter("@kuantitas", Convert.ToInt16(dr.Cells["kuantitas"].Value)),
                new NpgsqlParameter("@diskon", Convert.ToInt32(dr.Cells["diskon"].Value))))
                {
                    qty = Convert.ToInt32(dr.Cells["kuantitas"].Value);
                    pname = Convert.ToString(dr.Cells["id obat"].Value);

                    NpgsqlConnection cons = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
                    cons.Open();
                    NpgsqlCommand cmd6 = cons.CreateCommand();
                    cmd6.CommandType = CommandType.Text;
                    cmd6.CommandText = "update obat set stok=stok-" + qty + " where id_obat="+(Convert.ToInt16(pname));
                    cmd6.ExecuteNonQuery();
                    MessageBox.Show("berhasil");
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            tampil();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int Grdtotal = 0;
            int kembalian = 0;
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                Grdtotal = Grdtotal + Convert.ToInt32(dataGridView1.Rows[row].Cells[6].Value);
                kembalian = (Convert.ToInt32(textBox3.Text)) - Grdtotal;
            }
            richTextBox1.Text = "Rp. " + Grdtotal;
            richTextBox2.Text = "Rp" + kembalian;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView3.Rows[e.RowIndex];
                textBox2.Text = row.Cells[0].Value.ToString();
                textBox6.Text = row.Cells[3].Value.ToString();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NpgsqlConnection conn = new NpgsqlConnection("Server = localhost; Port = 5432; Database = sahabat; User Id = postgres; Password = wse32134");
            try
            {
                conn.Open();
                cmd = new NpgsqlCommand("Select id_obat,nama_obat,stok,harga from obat where nama_obat=@nama_obat", conn);
                cmd.Parameters.AddWithValue("nama_obat", string.Format(textBox10.Text));
                ds = new DataSet();
                da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds, "obat");
                dataGridView3.DataSource = ds;
                dataGridView3.DataMember = "obat";

            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dp = dt2.NewRow();
            dp["id transaksi"] = Convert.ToInt16(textBox1.Text);
            dp["tanggal"] = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");
            dp["id jenis transaksi"] = Convert.ToInt16(comboBox1.Text);
            dp["id karyawan"] = Convert.ToInt16(textBox8.Text);
            dp["id customer"] = Convert.ToInt16(textBox5.Text);
            dt2.Rows.Add(dp);
            dataGridView2.DataSource = dt2;
        }

        private void button7_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow dp in dataGridView2.Rows)
            {
                if (SqlDBHelper.ExecuteNonQuery("insert into customer (id_customer,nama_customer) values (@id_customer,@nama_customer)",
                CommandType.Text,
                new NpgsqlParameter("@id_customer", Convert.ToInt16(textBox5.Text)),
                new NpgsqlParameter("@nama_customer", Convert.ToString(textBox11.Text))))
                {
                    if (SqlDBHelper.ExecuteNonQuery("insert into Transaksi (id_transaksi,tanggal,id_jenis_transaksi,id_karyawan,id_customer) values(@id_transaksi,@tanggal,@id_jenis_transaksi,@id_karyawan,@id_customer)",
                    CommandType.Text,
                    new NpgsqlParameter("@id_transaksi", Convert.ToInt16(dp.Cells["id transaksi"].Value)),
                    new NpgsqlParameter("@tanggal", Convert.ToDateTime(dp.Cells["tanggal"].Value)),
                    new NpgsqlParameter("@id_jenis_transaksi", Convert.ToInt16(dp.Cells["id jenis transaksi"].Value)),
                    new NpgsqlParameter("@id_karyawan", Convert.ToInt16(dp.Cells["id karyawan"].Value)),
                    new NpgsqlParameter("@id_customer", Convert.ToInt16(dp.Cells["id customer"].Value))))
                    MessageBox.Show("berhasil");
                }
                
               
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transaksi fm = new Transaksi();
            fm.Show();
            fm.Close();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
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
