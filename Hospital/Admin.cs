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
    public partial class Admin : Form
    {
        public SqlConnection connection;
        public Admin()
        {
            InitializeComponent();
            connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\v11.0";
            builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";
            builder.IntegratedSecurity = true;
            connection.ConnectionString = builder.ConnectionString;
        }
        private void Admin_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            admintable();
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Homepage hp = new Homepage();
            hp.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand add = new SqlCommand("UPDATE Admin SET Username = '" + textBox1.Text + "', Password = '" + textBox2.Text + "' WHERE Id = '" + textBox3.Text + "'", connection);
                add.ExecuteNonQuery();
                MessageBox.Show("Admin Berhasil Diupdate");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void admintable()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Admin", connection);
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

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            admintable();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
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
                    textBox1.Text = row.Cells["Username"].Value.ToString();
                    textBox2.Text = row.Cells["Password"].Value.ToString();
                    textBox3.Text = row.Cells["Id"].Value.ToString();
                }
            }
        }
    }
}
