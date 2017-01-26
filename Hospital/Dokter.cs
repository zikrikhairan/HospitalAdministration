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
    public partial class Dokter : Form
    {
        public SqlConnection connection;
        public Dokter()
        {
            InitializeComponent();
            connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\v11.0";
            builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";
            builder.IntegratedSecurity = true;
            connection.ConnectionString = builder.ConnectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand add = new SqlCommand("INSERT INTO Dokter (Id_dokter, Nama, Departemen, Jenis_kelamin, Alamat, Gaji, Umur, Email, No_telepon, Tanggal_lahir, Info) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + comboBox1.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "', '" + dateTimePicker1.Value + "', '" + textBox11.Text + "')", connection);
                add.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Diinputkan");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void populatetable()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Dokter", connection);
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
        private void Dokter_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'rSDataSet.Dokter' table. You can move, or remove it, as needed.
            dataGridView1.ReadOnly = true;
            populatetable();
            comboBox1.DataSource = new String[] { "Laki-laki", "Perempuan" };
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
                textBox1.Text = row.Cells["Id_dokter"].Value.ToString();
                textBox2.Text = row.Cells["Nama"].Value.ToString();
                textBox3.Text = row.Cells["Departemen"].Value.ToString();
                comboBox1.Text = row.Cells["Jenis_kelamin"].Value.ToString();
                textBox5.Text = row.Cells["Alamat"].Value.ToString();
                textBox6.Text = row.Cells["Gaji"].Value.ToString();
                textBox7.Text = row.Cells["Umur"].Value.ToString();
                textBox8.Text = row.Cells["Email"].Value.ToString();
                textBox9.Text = row.Cells["No_telepon"].Value.ToString();
                dateTimePicker1.Text = row.Cells["Tanggal_lahir"].Value.ToString();
                textBox11.Text = row.Cells["Info"].Value.ToString();

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand add = new SqlCommand("UPDATE Dokter SET Id_dokter = '" + textBox1.Text + "', Nama = '" + textBox2.Text + "', Departemen = '" + textBox3.Text + "', Jenis_kelamin = '" + comboBox1.Text + "', Alamat = '" + textBox5.Text + "', Gaji = '" + textBox6.Text + "', Umur = '" + textBox7.Text + "', Email = '" + textBox8.Text + "', No_telepon = '" + textBox9.Text + "', Tanggal_lahir = '" + dateTimePicker1.Value + "', Info = '" + textBox11.Text + "' WHERE Id_dokter = '" + textBox1.Text +"'", connection);
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
                SqlCommand add = new SqlCommand("DELETE FROM Dokter WHERE Id_dokter = '" + textBox1.Text + "'", connection);
                add.ExecuteNonQuery();
                MessageBox.Show("Data telah berhasil Dihapus");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dokter_Load(sender, e);
        }

        private void Dokter_FormClosed(object sender, FormClosedEventArgs e)
        {
            Homepage hm = new Homepage();
            hm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage hm = new Homepage();
            hm.Show();
        }

        private void Dokter_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
