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
    public partial class Obat : Form
    {
        public SqlConnection connection = null;
        public Obat()
        {
            InitializeComponent();
            connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\v11.0";
            builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";
            builder.IntegratedSecurity = true;
            connection.ConnectionString = builder.ConnectionString;
        }

        private void Obat_Load(object sender, EventArgs e)
        {
            tabelobat();
            textBox4.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Data Harus Diisi");
                }
                else
                {
                    int harga = Int32.Parse(textBox2.Text);
                    connection.Open();
                    SqlCommand add = new SqlCommand("INSERT INTO Obat (Nama_obat, Harga, Pemakaian) VALUES ('" + textBox1.Text + "', '" + harga + "', '" + textBox3.Text + "')", connection);
                    add.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diinputkan");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int harga = Int32.Parse(textBox2.Text);
                connection.Open();
                SqlCommand add = new SqlCommand("UPDATE Obat SET Nama_obat = '" + textBox1.Text + "', Harga = '" + harga + "', Pemakaian = '" + textBox3.Text + "' WHERE Id_obat = '" + textBox4.Text + "'", connection);
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
                SqlCommand add = new SqlCommand("DELETE FROM Obat WHERE Id_obat = '" + textBox4.Text + "'", connection);
                add.ExecuteNonQuery();
                MessageBox.Show("Data telah berhasil Dihapus");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void tabelobat()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Obat", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                textBox1.Text = row.Cells["Nama_obat"].Value.ToString();
                textBox2.Text = row.Cells["Harga"].Value.ToString();
                textBox3.Text = row.Cells["Pemakaian"].Value.ToString();
                textBox4.Text = row.Cells["Id_obat"].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Obat_Load(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage hm = new Homepage();
            hm.Show();
        }

        private void Obat_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
