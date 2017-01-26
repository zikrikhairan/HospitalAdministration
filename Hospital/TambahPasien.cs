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
    public partial class TambahPasien : Form
    {
        public SqlConnection connection;
        public TambahPasien()
        {
            InitializeComponent();
            connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\v11.0";
            builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";
            builder.IntegratedSecurity = true;
            connection.ConnectionString = builder.ConnectionString;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nama = textBox1.Text;
                string namalengkap = textBox2.Text;
                string jeniskelamin = comboBox1.Text;
                string notelepon = textBox4.Text;
                string alamat = textBox5.Text;
                DateTime time = DateTime.Now;
                string format = "yyyy-MM-dd HH:MM:ss";

                if ((nama == "") || (namalengkap == "") || (textBox3.Text == "") || (jeniskelamin == "") || (notelepon == "") || (alamat == ""))
                {
                    MessageBox.Show("Data Harus Diisi");
                }
                else
                {                   
                    int umur = Convert.ToInt32(textBox3.Text);
                    connection.Open();
                    SqlCommand add = new SqlCommand("INSERT INTO Pasien (Nama_panggilan, Nama_lengkap, Umur, Jenis_kelamin, No_telepon, Alamat, Tanggal_masuk) OUTPUT Inserted.Id_pasien VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + comboBox1.SelectedValue + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + time.ToString(format) + "')", connection);
                    SqlDataReader reader = add.ExecuteReader();
                    reader.Read();
                    String s = reader.GetInt64(0).ToString();
                    connection.Close();
                    MessageBox.Show("Data Berhasil Diinputkan");
                    OutPatient jalan = new OutPatient();
                    jalan.textBox1.Text = s;
                    jalan.textBox2.Text = textBox1.Text;
                    jalan.textBox3.Text = textBox2.Text;
                    jalan.textBox4.Text = textBox3.Text;
                    jalan.comboBox2.Text = comboBox1.Text;
                    jalan.textBox6.Text = textBox5.Text;
                    jalan.textBox12.Text = textBox4.Text;
                    jalan.dateTimePicker1.Value = dateTimePicker1.Value;
                    jalan.textBox1.ReadOnly = true;
                    jalan.textBox2.ReadOnly = true;
                    jalan.textBox3.ReadOnly = true;
                    jalan.textBox4.ReadOnly = true;
                    jalan.comboBox2.Enabled = false;
                    jalan.textBox6.ReadOnly = true;
                    jalan.textBox12.ReadOnly = true;
                    jalan.dateTimePicker1.Enabled = false;
                    jalan.button5.Enabled = false;
                    jalan.textBox15.Visible = false;
                    jalan.label18.Visible = false;
                    jalan.button2.Enabled = false;
                    jalan.button11.Enabled = false;

                    jalan.Show();
                    
                    this.Hide(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.TambahPasien_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage hp = new Homepage();
            hp.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string nama = textBox1.Text;
                string namalengkap = textBox2.Text;
                string jeniskelamin = comboBox1.Text;
                string notelepon = textBox4.Text;
                string alamat = textBox5.Text;
                DateTime time = DateTime.Now;
                string format = "yyyy-MM-dd HH:MM:ss";

                if ((nama == "") || (namalengkap == "") || (textBox3.Text == "") || (jeniskelamin == "") || (notelepon == "") || (alamat == ""))
                {
                    MessageBox.Show("Data Harus Diisi");
                }
                else
                {
                    int umur = Convert.ToInt32(textBox3.Text);
                    connection.Open();
                    SqlCommand add = new SqlCommand("INSERT INTO Pasien (Nama_panggilan, Nama_lengkap, Umur, Jenis_kelamin, No_telepon, Alamat, Tanggal_masuk) OUTPUT Inserted.Id_pasien VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + comboBox1.SelectedValue + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + time.ToString(format) + "')", connection);
                    SqlDataReader reader = add.ExecuteReader();
                    reader.Read();
                    String s = reader.GetInt64(0).ToString();
                    PasienInap inap = new PasienInap();
                    inap.textBox1.Text = s;
                    inap.textBox2.Text = textBox1.Text;
                    inap.textBox3.Text = textBox2.Text;
                    inap.textBox4.Text = textBox3.Text;
                    inap.comboBox3.Text = comboBox1.Text;
                    inap.textBox16.Text = textBox4.Text;
                    inap.textBox6.Text = textBox5.Text;
                    inap.dateTimePicker1.Value = dateTimePicker1.Value;
                    inap.textBox1.ReadOnly = true;
                    inap.textBox2.ReadOnly = true;
                    inap.textBox3.ReadOnly = true;
                    inap.textBox4.ReadOnly = true;
                    inap.comboBox3.Enabled = false;
                    inap.textBox16.ReadOnly = true;
                    inap.textBox6.ReadOnly = true;
                    inap.dateTimePicker1.Enabled = false;
                    inap.button9.Enabled = false;
                    inap.button13.Enabled = false;
                    inap.label18.Visible = false;
                    inap.textBox15.Visible = false;
                    inap.button1.Enabled = false;
                    inap.button6.Enabled = false;
                    inap.Show();

                    MessageBox.Show("Data Berhasil Diinputkan");
                    connection.Close();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TambahPasien_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void TambahPasien_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = new String[] { "Laki-laki", "Perempuan" };
        }
    }
}
