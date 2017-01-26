using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hospital
{
    public partial class TagihanPasienInap : Form
    {
        public SqlConnection connection = null;
        public TagihanPasienInap()
        {
            InitializeComponent();
            connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\v11.0";
            builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";
            builder.IntegratedSecurity = true;
            connection.ConnectionString = builder.ConnectionString;
        }

        private void TagihanPasienInap_Load(object sender, EventArgs e)
        {
            connection.Close();
            checkBox1.Checked = true;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox10.ReadOnly = true;
            textBox6.ReadOnly = true;
            comboBox2.Enabled = false;
            comboBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            dataGridView1.ReadOnly = true;
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

        private void TagihanPasienInap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Homepage hm = new Homepage();
            hm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            PasienInap pi = new PasienInap();
            pi.Show();
        }

        private void TagihanPasienInap_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if ((comboBox1.Text == "") || (comboBox2.Text == "") || (textBox5.Text == "") || (textBox7.Text == ""))
                {
                    MessageBox.Show("Data harus Diisi");
                }
                else
                {
                    int totalharga = Convert.ToInt32(textBox7.Text);
                    DateTime keluar = Convert.ToDateTime(dateTimePicker2.Value);
                    int lamainap = Convert.ToInt32(textBox5.Text);
                    Int64 idtag = Convert.ToInt64(textBox1.Text);
                    Int64 idkam = Convert.ToInt64(comboBox1.Text);
                    connection.Open();
                    SqlCommand add = new SqlCommand("UPDATE Adm_Pasien_Inap SET Tanggal_keluar = '" + keluar + "', Lama_inap = '" + lamainap + "',   Jumlah = '" + totalharga + "', Info = '" + idtag + "', Keluar = '" + true + "' WHERE Id_tagihan = '" + textBox1.Text + "'", connection);
                    add.ExecuteNonQuery(); 
                    MessageBox.Show("Data Berhasil Diupdate");
                    MessageBox.Show("Pasien sudah keluar");
                    SqlCommand ketersediaan = new SqlCommand("UPDATE Kamar SET Tersedia = '" + true + "' WHERE Id_kamar = '" + idkam + "'", connection);
                    ketersediaan.ExecuteNonQuery();
                    MessageBox.Show("Kamar " + comboBox1.Text + " sudah kosong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime d1 = dateTimePicker1.Value;
            DateTime d2 = dateTimePicker2.Value;

            Double answer = Math.Floor((d2 - d1).TotalDays);
            textBox5.Text = answer.ToString();
            int a = Convert.ToInt32(textBox10.Text);
            textBox3.Text = Convert.ToString(a * answer);

            int kamar = Convert.ToInt32(textBox6.Text);
            int obat = Convert.ToInt32(textBox3.Text);
            textBox7.Text = Convert.ToString(kamar + obat);
        }
    }
}
