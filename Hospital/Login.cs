using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO; //digunakan untuk path
using System.Reflection; //digunakan untuk assembly

namespace Hospital
{
    public partial class Login : Form
    {
        public SqlConnection connection = null;
        public Login()
        {
            InitializeComponent();
            connection = new SqlConnection();  //membuat koneksi baru
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();   //membuat koneksi string baru
            builder.DataSource = "(LocalDB)\\v11.0";            // database apa yang digunakan
            builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";         //letak database dan nama database
            builder.IntegratedSecurity = true;          
            connection.ConnectionString = builder.ConnectionString;   //penyamaan koneksi dengan koneksi string
            checkBox1.Checked = true;     // membuat cekbok di ceklis ketika pertama kali muncul
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();           // membuka koneksi baru
                SqlCommand admin = new SqlCommand("SELECT * FROM Admin WHERE Username ='" + textBox1.Text + "' and password ='" + textBox2.Text + "'", connection);
                SqlDataReader dr;         // membaca data yang ada
                dr = admin.ExecuteReader();       // mengeksekusi data yang masuk
                int count = 0;       // 
                while (dr.Read())
                {
                    count += 1;
                }

                if (count == 1)
                {
                    MessageBox.Show("     Login dengan ID : " + textBox1.Text);
                    this.Hide();
                    Homepage hp = new Homepage();
                    hp.Show();
                }
                else if (count > 0)
                {
                    MessageBox.Show("Duplikasi Nama ID dan Kata Sandi");
                }
                else
                {
                    MessageBox.Show("Nama ID atau Kata Sandi salah!!!");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LupaPassword forg = new LupaPassword();
            forg.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '*'; //set passwordchar to '*'
            }
            else
            {
                textBox2.PasswordChar = (char)0; //reset passwordchar to default
                checkBox1.Text = "Sembunyikan password"; //change password to hide password
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
