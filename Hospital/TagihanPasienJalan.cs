using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hospital
{
    public partial class TagihanPasienJalan : Form
    {
        public SqlConnection connection = null;
        public TagihanPasienJalan()
        {
            InitializeComponent();
            connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\v11.0";
            builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";
            builder.IntegratedSecurity = true;
            connection.ConnectionString = builder.ConnectionString;
        }

        private void TagihanPasienJalan_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            dataGridView1.Enabled = false;
            comboBox1.Enabled = false;
            connection.Close();
            dataGridView1.ReadOnly = true;

        }

        private void TagihanPasienJalan_FormClosed(object sender, FormClosedEventArgs e)
        {
            Homepage hm = new Homepage();
            hm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand obat = new SqlCommand("SELECT * FROM Pengobatan WHERE Id_tagihan = '" + textBox1.Text + "'", connection);
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = obat;
                DataTable tabel = new DataTable();
                adap.Fill(tabel);
                dataGridView1.DataSource = tabel;
                dataGridView1.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            OutPatient hm = new OutPatient();
            hm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Int64 idpasien = Convert.ToInt64(textBox2.Text);
                //string iddokter = Convert.ToString(comboBox1.Text);
                connection.Open();
                SqlCommand add = new SqlCommand("UPDATE Adm_Pasien_Jalan SET Info = '" + textBox4.Text + "' WHERE Id_tagihan = '" + textBox1.Text + "'", connection);
                add.ExecuteNonQuery();
                MessageBox.Show("Info Berhasil Diinputkan");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }    connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.TagihanPasienJalan_Load(sender, e);
        }

        private void TagihanPasienJalan_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
