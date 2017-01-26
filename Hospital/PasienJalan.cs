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
    public partial class OutPatient : Form
    {
        public SqlConnection connection = null;
        public OutPatient()
        {
            InitializeComponent();
            connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\v11.0";
            builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";
            builder.IntegratedSecurity = true;
            connection.ConnectionString = builder.ConnectionString;
        }

        private void OutPatient_FormClosed(object sender, FormClosedEventArgs e)
        {
            Homepage hm = new Homepage();
            hm.Show();
        }

        private void OutPatient_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            dataGridView2.Visible = false;
            dataGridView4.Visible = false;
            pilihobat();
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT Id_dokter, Nama, Departemen  FROM Dokter", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0).ToString());      // pemakaian collection nama dokter
                namadokter.Add(reader.GetString(1).ToString());
                departemen.Add(reader.GetString(2).ToString());
            }
            connection.Close();    
        }
        public void pilihobat()
        {
            try
            {
                connection.Close();
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Obat", connection);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                DataGridViewCheckBoxColumn checkcolumn = new DataGridViewCheckBoxColumn();
                checkcolumn.HeaderText = "Pilih";
                dataGridView1.Columns.Add(checkcolumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }
            
        private void OutPatient_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage hp = new Homepage();
            hp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                TagihanPasienJalan jalan = new TagihanPasienJalan();
                jalan.textBox2.Text = textBox1.Text;
                jalan.textBox1.Text = textBox10.Text;
                jalan.comboBox1.Text = comboBox1.Text;
                jalan.dateTimePicker1.Value = dateTimePicker1.Value;
                jalan.textBox4.Text = textBox11.Text;
                jalan.textBox5.Text = textBox14.Text;
                jalan.textBox6.Text = textBox15.Text;
                jalan.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public List<string> departemen = new List<string>();
        public List<string> namadokter = new List<string>();  // contoh pemakaian collection
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                textBox8.Text = namadokter[comboBox1.SelectedIndex];
                textBox9.Text = departemen[comboBox1.SelectedIndex];
            }
            else
                textBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("id_obat");
            table.Columns.Add("nama obat");
            table.Columns.Add("harga");
            int sum = 0, count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[4].Value != null && (bool)row.Cells[4].Value == true)
                {
                    table.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString());
                    sum += (int)row.Cells[2].Value;
                    count++;
                }
            }
            textBox14.Text = sum.ToString();
            textBox11.Text = count.ToString();
            dataGridView3.DataSource = table;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                try
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[4].Value == null ||
                       (int)dataGridView1.Rows[e.RowIndex].Cells[4].Value == 0)
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = 1;
                    else
                        dataGridView1.Rows[e.RowIndex].Cells[4].Value = 0;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand tagihan = new SqlCommand("Select Id_tagihan, Id_pasien FROM Adm_Pasien_Jalan WHERE Id_Pasien ='" + textBox1.Text + "'", connection);
                SqlDataReader reader = tagihan.ExecuteReader();
                reader.Read();
                String s = reader.GetInt64(0).ToString();
                if (s == null)
                {
                    MessageBox.Show("ID Tagihan belum ada!");
                }
                else
                {
                    MessageBox.Show("ID Tagihan sudah ada!");
                    textBox10.Text = s;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Silahkan klik tombol 'Get ID' ");
            }
            connection.Close();
        }

        public string bagian;
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand caridiinap = new SqlCommand("SELECT Id_pasien FROM Adm_Pasien_Inap WHERE Id_pasien = '" + textBox1.Text + "'", connection);
                SqlDataReader rad = caridiinap.ExecuteReader();
                if (rad.Read())
                {
                    MessageBox.Show("ID Pasien sudah terdaftar di Pasien Rawat Inap");
                }
                else
                {
                    int jumlah = Convert.ToInt32(textBox14.Text);
                    connection.Close();
                    connection.Open();
                    SqlCommand add = new SqlCommand("UPDATE Adm_Pasien_Jalan SET Id_dokter = '" + comboBox1.Text + "',  Jumlah = '" + jumlah + "', Info = '" + textBox15.Text + "' WHERE Id_tagihan = '" + textBox10.Text + "'", connection);
                    add.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diupdate");
                    connection.Close();
                    connection.Open();
                    SqlCommand depdok = new SqlCommand("SELECT * FROM Dokter where Id_dokter = '" + comboBox1.Text + "'", connection);
                    SqlDataAdapter adapsi = new SqlDataAdapter(depdok);
                    DataTable tabel = new DataTable();
                    adapsi.Fill(tabel);
                    foreach (DataRow row in tabel.Rows)
                    {
                        bagian = row["Id_dokter"].ToString();
                    }

                    Dok1 dokter1 = new Dok1();
                    dokter1.DataB();
                    Dok2 dokter2 = new Dok2();
                    dokter2.DataC();
                    Dok3 dokter3 = new Dok3();
                    dokter3.DataD();

                    if (bagian.Contains("UMU"))
                    {
                        MessageBox.Show(dokter1.DataA() + dokter1.DataB());
                    }
                    else if (bagian.Contains("THT"))
                    {
                        MessageBox.Show(dokter2.DataA() + dokter2.DataC());
                    }
                    else if (bagian.Contains("PDL"))
                    {
                        MessageBox.Show(dokter3.DataA() + dokter3.DataD());
                    }
                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        Int64 a = Convert.ToInt64(row.Cells[0].Value);
                        SqlCommand obat = new SqlCommand("INSERT INTO Pengobatan (Id_tagihan, Id_Obat, Jumlah_obat, Total_harga) VALUES ('" + textBox10.Text + "',  '" + a + "', '" + textBox11.Text + "', '" + textBox11.Text + "')", connection);
                        obat.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data Pengobatan Berhasil Diinputkan");
                    button3.Enabled = true;

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();
                SqlCommand add = new SqlCommand("DELETE FROM Adm_Pasien_Jalan WHERE Id_tagihan = '" + textBox10.Text + "'", connection);
                add.ExecuteNonQuery();
                MessageBox.Show("Data telah berhasil Dihapus");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox10.ReadOnly = true;
            button5.Enabled = true;
            button6.Visible = true;
            dataGridView2.Visible = true;
            dataGridView4.Visible = false;
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT * FROM Pasien", connection);
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = command1;
            DataTable table = new DataTable();
            adapter1.Fill(table);
            dataGridView2.DataSource = table;
            connection.Close();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView2.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                textBox1.Text = row.Cells["Id_pasien"].Value.ToString();
                textBox2.Text = row.Cells["Nama_panggilan"].Value.ToString();
                textBox3.Text = row.Cells["Nama_lengkap"].Value.ToString();
                textBox4.Text = row.Cells["Umur"].Value.ToString();
                comboBox2.Text = row.Cells["Jenis_kelamin"].Value.ToString();
                textBox6.Text = row.Cells["Alamat"].Value.ToString();
                textBox12.Text = row.Cells["No_telepon"].Value.ToString();
                dateTimePicker1.Text = row.Cells["Tanggal_masuk"].Value.ToString();
            } 
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox10.ReadOnly = true;
            button5.Enabled = true;
            button6.Visible = true;
            dataGridView4.Visible = true;
            dataGridView2.Visible = false;
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT Adm_Pasien_Jalan.Id_tagihan, Pasien.Id_pasien, Pasien.Nama_panggilan, Pasien.Nama_lengkap, Pasien.Umur, Pasien.Jenis_kelamin, Pasien.No_telepon, Pasien.Alamat, Pasien.Tanggal_masuk, Dokter.Id_dokter, Dokter.Nama, Dokter.Departemen, Adm_Pasien_Jalan.Jumlah FROM Pasien JOIN Adm_Pasien_Jalan ON  Adm_Pasien_Jalan.Id_pasien=Pasien.Id_pasien JOIN Dokter ON Adm_Pasien_Jalan.Id_dokter=Dokter.Id_dokter ORDER BY Id_pasien ASC", connection);
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = command1;
            DataTable table = new DataTable();
            adapter1.Fill(table);
            dataGridView4.DataSource = table;
            dataGridView4.ReadOnly = true;
            connection.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                SqlCommand caridiinap = new SqlCommand("SELECT Id_pasien FROM Adm_Pasien_Inap WHERE Id_pasien = '" + textBox1.Text + "'", connection);
                SqlDataReader rad = caridiinap.ExecuteReader();
                if (textBox1.Text == string.Empty)
                {
                    MessageBox.Show("Isi ID pasien");
                }
                if (rad.Read())
                {
                    MessageBox.Show("ID Pasien sudah terdaftar di Pasien Rawat Inap");
                }
                else
                {
                    if (rad.HasRows)
                    {
                        SqlCommand tagihan = new SqlCommand("INSERT INTO Adm_Pasien_Jalan (Id_pasien) VALUES ('" + textBox1.Text + "')", connection);
                        tagihan.ExecuteNonQuery();
                        MessageBox.Show("ID Tagihan Didapatkan");
                        connection.Close();
                        connection.Open();
                        SqlCommand idtagihan = new SqlCommand("Select Id_tagihan, Id_pasien FROM Adm_Pasien_Jalan WHERE Id_Pasien ='" + textBox1.Text + "'", connection);
                        SqlDataReader reader = idtagihan.ExecuteReader();
                        reader.Read();
                        String s = reader.GetInt64(0).ToString();
                        textBox10.Text = s;
                    }
                    else
                    {
                        connection.Close();
                        connection.Open();
                        SqlCommand tagihan2 = new SqlCommand("INSERT INTO Adm_Pasien_Jalan (Id_pasien) VALUES ('" + textBox1.Text + "')", connection);
                        tagihan2.ExecuteNonQuery();
                        MessageBox.Show("ID Tagihan Didapatkan");
                        SqlCommand idtagihan = new SqlCommand("Select Id_tagihan, Id_pasien FROM Adm_Pasien_Jalan WHERE Id_Pasien ='" + textBox1.Text + "'", connection);
                        SqlDataReader reader = idtagihan.ExecuteReader();
                        reader.Read();
                        String s = reader.GetInt64(0).ToString();
                        textBox10.Text = s;
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dataGridView4.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (cell != null)
            {
                DataGridViewRow row = cell.OwningRow;
                textBox10.Text = row.Cells["Id_tagihan"].Value.ToString();
                textBox1.Text = row.Cells["Id_pasien"].Value.ToString();
                textBox2.Text = row.Cells["Nama_panggilan"].Value.ToString();
                textBox3.Text = row.Cells["Nama_lengkap"].Value.ToString();
                textBox4.Text = row.Cells["Umur"].Value.ToString();
                comboBox2.Text = row.Cells["Jenis_kelamin"].Value.ToString();
                textBox6.Text = row.Cells["Alamat"].Value.ToString();
                textBox12.Text = row.Cells["No_telepon"].Value.ToString();
                dateTimePicker1.Text = row.Cells["Tanggal_masuk"].Value.ToString();
                comboBox1.Text = row.Cells["Id_dokter"].Value.ToString();
                textBox8.Text = row.Cells["Nama"].Value.ToString();
                textBox9.Text = row.Cells["Departemen"].Value.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView3.DataSource = null;
            try
            {
                connection.Open();
                SqlCommand obat = new SqlCommand("SELECT * FROM Pengobatan WHERE Id_tagihan = '" + textBox10.Text + "'", connection);
                SqlDataReader red = obat.ExecuteReader();
                while (red.Read())
                {
                    if (textBox1.Text == "")
                    {
                        MessageBox.Show("ID tagihan tidak ada!");
                    }
                    else
                    {
                        textBox11.Text = (red["Jumlah_obat"].ToString());
                        textBox14.Text = (red["Total_harga"].ToString());
                        connection.Close();
                        connection.Open();
                        SqlDataAdapter adap = new SqlDataAdapter();
                        adap.SelectCommand = obat;
                        DataTable tabel = new DataTable();
                        adap.Fill(tabel);
                        dataGridView3.DataSource = tabel;
                        dataGridView3.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }
    }
}
