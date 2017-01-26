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
    public partial class Kamar : Form
    {
        public SqlConnection connection = null;
        public Kamar()
        {
            InitializeComponent();
            connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\v11.0";
            builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";
            builder.IntegratedSecurity = true;
            connection.ConnectionString = builder.ConnectionString;
        }

        private void Kamar_Load(object sender, EventArgs e)
        {
            tabelkamar();
        }
        public void tabelkamar()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Kamar", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Data Harus Diisi");
                }
                else
                {
                    int harga = Int32.Parse(textBox3.Text);
                    connection.Open();
                    SqlCommand add = new SqlCommand("INSERT INTO Kamar (Id_kamar, Nama_kamar, Jenis_kamar, Harga, Tersedia) VALUES ('" + textBox4.Text + "','" + textBox1.Text + "','" + textBox2.Text + "', '" + harga + "', '" + true + "')", connection);
                    add.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diinputkan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int harga = Int32.Parse(textBox3.Text);
                connection.Open();
                SqlCommand add = new SqlCommand("UPDATE Kamar SET Id_kamar = '" + textBox4.Text + "', Nama_kamar = '" + textBox1.Text + "',Jenis_kamar = '" + textBox2.Text + "', Harga = '" + harga + "', Tersedia = '" + checkBox1.Checked + "' WHERE Id_kamar = '" + textBox4.Text + "'", connection);
                add.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Diupdate");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand add = new SqlCommand("DELETE FROM Kamar WHERE Id_kamar = '" + textBox4.Text + "'", connection);
                add.ExecuteNonQuery();
                MessageBox.Show("Data telah berhasil Dihapus");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage hm = new Homepage();
            hm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Kamar_Load(sender, e);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                textBox1.Text = row.Cells["Nama_kamar"].Value.ToString();
                textBox2.Text = row.Cells["Jenis_kamar"].Value.ToString();
                textBox3.Text = row.Cells["Harga"].Value.ToString();
                textBox4.Text = row.Cells["Id_kamar"].Value.ToString();
                checkBox1.Checked = (bool)row.Cells["Tersedia"].Value;

            }
        }
    }
}