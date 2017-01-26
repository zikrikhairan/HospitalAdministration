using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Hospital
{
    public partial class LupaPassword : Form
    {
        public LupaPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                SqlConnection forgot = new SqlConnection();
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "(LocalDB)\\v11.0";
                builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";
                builder.IntegratedSecurity = true;
                forgot.ConnectionString = builder.ConnectionString;
                forgot.Open();

                SqlCommand admin = new SqlCommand("SELECT * FROM Admin WHERE Username ='" + textBox1.Text + "'", forgot);      
                SqlDataReader dr;
                dr = admin.ExecuteReader();
                int count = 0;
                while (dr.Read())
                {
                    count += 1;
                    if (count == 1)
                    {
                        String Username = dr.GetString(1);
                        String Password = dr.GetString(2);
                        MessageBox.Show("Kata sandi dengan nama ID : " + Username + " adalah : " + Password);
                        this.Hide();
                        Login log = new Login();
                        log.Show();
                    }
                }
                if(count == 0)
                {
                    MessageBox.Show("Nama ID tidak Terdaftar");
                }
            }

        }

        private void LupaPassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login logi = new Login();
            logi.Show();
        }
    }
}
