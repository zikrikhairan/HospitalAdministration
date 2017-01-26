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
    public partial class PasienInap : Form
    {
        public SqlConnection connection = null;
        public PasienInap()
        {
            InitializeComponent();
            connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(LocalDB)\\v11.0";
            builder.AttachDBFilename = "E:\\databasebp2\\RS.mdf";
            builder.IntegratedSecurity = true;
            connection.ConnectionString = builder.ConnectionString;
        }
        
        private void PasienInap_Load(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
            dataGridView4.Visible = false;
            button5.Enabled = false;
            button13.Visible = false;
            checkBox2.Enabled = false;
            dateTimePicker2.Visible = false;
            label21.Visible = false;
            checkBox1.Visible = false;
            pilihobat();
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT Id_dokter, Nama, Departemen FROM Dokter", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetString(0).ToString());      // pemakaian collection nama dokter
                namadokter.Add(reader.GetString(1).ToString());
                departemen.Add(reader.GetString(2).ToString());
            }
            connection.Close();
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT Id_kamar, Harga, Nama_kamar, Tersedia FROM Kamar", connection);
            SqlDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                comboBox2.Items.Add(reader1.GetInt64(0).ToString());
                hargakamar.Add(reader1.GetInt32(1).ToString());
                namakamar.Add(reader1.GetString(2).ToString());
                tersedia.Add(reader1.GetBoolean(3).ToString());
            }
            connection.Close();
        }

        public void pilihobat()
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
                DataGridViewCheckBoxColumn checkcolumn = new DataGridViewCheckBoxColumn();
                checkcolumn.HeaderText = "Pilih";
                dataGridView1.Columns.Add(checkcolumn);
                connection.Close();
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
            Homepage hm = new Homepage();
            hm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
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
            textBox7.Text = sum.ToString();
            textBox11.Text = count.ToString();
            dataGridView3.DataSource = table;
        }

        private void PasienInap_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox10.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || textBox14.Text == "" || textBox7.Text == "" || dateTimePicker1.Value == null)
                {
                    MessageBox.Show("Isi Semua Data");
                }
                else
                {
                    TagihanPasienInap inap = new TagihanPasienInap();
                    inap.textBox2.Text = textBox1.Text;
                    inap.textBox1.Text = textBox10.Text;
                    inap.comboBox1.Text = comboBox2.Text;
                    inap.comboBox2.Text = comboBox1.Text;
                    inap.textBox10.Text = textBox14.Text;
                    inap.textBox6.Text = textBox7.Text;
                    inap.dateTimePicker1.Value = dateTimePicker1.Value;
                    inap.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox10.ReadOnly = true; 
            button13.Enabled = true;
            button5.Enabled = true;
            button13.Visible = true;
            dataGridView2.Visible = true;
            dataGridView4.Visible = false;
            dateTimePicker2.Visible = true;
            label1.Visible = true;
            checkBox1.Visible = true;
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT  * FROM Pasien ", connection);
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = command1;
            DataTable table = new DataTable();
            adapter1.Fill(table);
            dataGridView2.DataSource = table;
            dataGridView2.ReadOnly = true;
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
                comboBox3.Text = row.Cells["Jenis_kelamin"].Value.ToString();
                textBox6.Text = row.Cells["Alamat"].Value.ToString();
                textBox16.Text = row.Cells["No_telepon"].Value.ToString();
                dateTimePicker1.Text = row.Cells["Tanggal_masuk"].Value.ToString();
            }  
        }
        public List<string> namadokter = new List<string>();  // contoh pemakaian collection
        public List<string> departemen = new List<string>();
     
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                textBox8.Text = namadokter[comboBox1.SelectedIndex];
                textBox9.Text = departemen[comboBox1.SelectedIndex];
            }
            else
            {
                textBox8.Text = "";
                textBox9.Text = "";
            }
        }
        public List<string> namakamar = new List<string>();
        public List<string> hargakamar = new List<string>();
        public List<string> tersedia = new List<string>();

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                textBox12.Text = namakamar[comboBox2.SelectedIndex];
                textBox14.Text = hargakamar[comboBox2.SelectedIndex];
                checkBox2.Checked = Convert.ToBoolean(tersedia[comboBox2.SelectedIndex]);
            }
            else
            {
                textBox12.Text = "";
                textBox14.Text = "";
                checkBox2.Checked = false;
            }
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

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand caridiinap = new SqlCommand("SELECT Id_pasien FROM Adm_Pasien_Jalan WHERE Id_pasien = '" + textBox1.Text + "'", connection);
                SqlDataReader rad = caridiinap.ExecuteReader();
                if (rad.Read())
                {
                    MessageBox.Show("ID Pasien sudah terdaftar di Pasien Rawat Jalan");
                }
                else
                {
                    if (rad.HasRows)
                    {
                        connection.Close();
                        connection.Open();
                        SqlCommand tagihan = new SqlCommand("INSERT INTO Adm_Pasien_Inap (Id_pasien) VALUES ('" + textBox1.Text + "')", connection);
                        tagihan.ExecuteNonQuery();
                        MessageBox.Show("ID Tagihan Didapatkan");
                        connection.Close();
                        connection.Open();
                        SqlCommand idtagihan = new SqlCommand("Select Id_tagihan, Id_pasien FROM Adm_Pasien_Inap WHERE Id_Pasien ='" + textBox1.Text + "'", connection);
                        SqlDataReader reader = idtagihan.ExecuteReader();
                        reader.Read();
                        String s = reader.GetInt64(0).ToString();
                        textBox10.Text = s;
                        connection.Close();
                    }
                    else
                    {
                        connection.Close();
                        connection.Open();
                        SqlCommand tagihan2 = new SqlCommand("INSERT INTO Adm_Pasien_Inap (Id_pasien) VALUES ('" + textBox1.Text + "')", connection);
                        tagihan2.ExecuteNonQuery();
                        MessageBox.Show("ID Tagihan Didapatkan");
                        SqlCommand idtagihan = new SqlCommand("Select Id_tagihan, Id_pasien FROM Adm_Pasien_Inap WHERE Id_Pasien ='" + textBox1.Text + "'", connection);
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
        }
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Close();
                connection.Open();
                SqlCommand tagihan = new SqlCommand("Select Id_tagihan, Id_pasien FROM Adm_Pasien_Inap WHERE Id_Pasien ='" + textBox1.Text + "'", connection);
                tagihan.ExecuteNonQuery();
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
                connection.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Silahkan klik tombol 'Get ID' ");
            }
            connection.Close();
        }
        public string bagian;
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand caridiinap = new SqlCommand("SELECT Id_pasien FROM Adm_Pasien_Jalan WHERE Id_pasien = '" + textBox1.Text + "'", connection);
                SqlDataReader rad = caridiinap.ExecuteReader();
                if (rad.Read())
                {
                    MessageBox.Show("ID Pasien sudah terdaftar di Pasien Rawat Jalan");
                }
                else
                {        
                            int jumlah = Convert.ToInt32(textBox14.Text);
                            connection.Close();
                            connection.Open();
                            SqlCommand command = new SqlCommand("SELECT Id_kamar, Tersedia FROM Kamar", connection);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                if (reader.GetInt64(0).ToString() == comboBox2.Text && reader.GetBoolean(1) == false)
                                {
                                    MessageBox.Show("Kamar tidak tersedia");
                                }
                                else if (reader.GetInt64(0).ToString() == comboBox2.Text && reader.GetBoolean(1) == true)
                                {
                                    MessageBox.Show("Kamar Tersedia");
                                    connection.Close();
                                    connection.Open();
                                    Int64 idkamar = Convert.ToInt64(comboBox2.Text);
                                    connection.Close();
                                    connection.Open();
                                    SqlCommand add = new SqlCommand("UPDATE Adm_Pasien_Inap SET Id_dokter = '" + comboBox1.Text + "', Id_kamar = '" + idkamar + "', Info = '" + textBox15.Text + "', Keluar = '" + false + "' WHERE Id_tagihan = '" + textBox10.Text + "' AND Id_pasien = '" + textBox1.Text + "'", connection);
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

                                    connection.Close();
                                    connection.Open();
                                    foreach (DataGridViewRow row in dataGridView3.Rows)
                                    {
                                        Int64 a = Convert.ToInt64(row.Cells[0].Value);
                                        SqlCommand obat = new SqlCommand(" INSERT INTO Pengobatan (Id_tagihan, Id_Obat, Jumlah_obat, Total_harga) VALUES ('" + textBox10.Text + "',  '" + a + "', '" + textBox11.Text + "', '" + textBox7.Text + "')", connection);
                                        obat.ExecuteNonQuery();
                                    }
                                    MessageBox.Show("Data Pengobatan Berhasil Diinputkan");
                                    button5.Enabled = true;
                                    connection.Close();
                                    connection.Open();
                                    SqlCommand ketersediaan = new SqlCommand("UPDATE Kamar SET Tersedia = '" + false + "' WHERE Id_kamar = '" + idkamar + "'", connection);
                                    ketersediaan.ExecuteNonQuery();
                                    MessageBox.Show("Kamar " + comboBox2.Text + " sudah terisi");
                                }
                                else
                                {

                                }               
                            }
                        }
                        connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                
                connection.Close();
                connection.Open();
                SqlCommand add = new SqlCommand("DELETE FROM Pasien WHERE Id_pasien = '" + textBox1.Text + "'", connection);
                add.ExecuteNonQuery();
                MessageBox.Show("Data telah berhasil Dihapus");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox10.ReadOnly = true;
            button13.Enabled = true;
            button5.Enabled = true;
            button13.Visible = true;
            dataGridView4.Visible = true;
            dataGridView2.Visible = false;
            dateTimePicker2.Visible = true;
            label1.Visible = true;
            checkBox1.Visible = true;
            connection.Open();
            SqlCommand command1 = new SqlCommand("SELECT Adm_Pasien_Inap.Id_tagihan, Pasien.Id_pasien, Pasien.Nama_panggilan, Pasien.Nama_lengkap, Pasien.Umur, Pasien.Jenis_kelamin, Pasien.No_telepon, Pasien.Alamat, Pasien.Tanggal_masuk, Adm_Pasien_Inap.Tanggal_keluar, Dokter.Id_dokter, Dokter.Nama, Dokter.Departemen, Adm_Pasien_Inap.Id_kamar, Kamar.Nama_kamar, Kamar.Harga, Adm_Pasien_Inap.Lama_inap, Adm_Pasien_Inap.Jumlah, Adm_Pasien_Inap.Info, Adm_Pasien_Inap.Keluar FROM Adm_Pasien_Inap JOIN Pasien ON  Adm_Pasien_Inap.Id_pasien=Pasien.Id_pasien JOIN Dokter ON Adm_Pasien_Inap.Id_dokter=Dokter.Id_dokter JOIN Kamar ON Adm_Pasien_Inap.Id_kamar=Kamar.Id_kamar  ORDER BY Id_pasien ASC", connection);
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = command1;
            DataTable table = new DataTable();
            adapter1.Fill(table);
            dataGridView4.DataSource = table;
            dataGridView4.ReadOnly = true;
            connection.Close();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                comboBox3.Text = row.Cells["Jenis_kelamin"].Value.ToString();
                textBox6.Text = row.Cells["Alamat"].Value.ToString();
                textBox16.Text = row.Cells["No_telepon"].Value.ToString();
                dateTimePicker1.Text = row.Cells["Tanggal_masuk"].Value.ToString();
                dateTimePicker2.Text = row.Cells["Tanggal_keluar"].Value.ToString();
                comboBox1.Text = row.Cells["Id_dokter"].Value.ToString();
                textBox8.Text = row.Cells["Nama"].Value.ToString();
                textBox9.Text = row.Cells["Departemen"].Value.ToString();
                comboBox2.Text = row.Cells["Id_kamar"].Value.ToString();
                textBox12.Text = row.Cells["Nama_kamar"].Value.ToString();
                textBox14.Text = row.Cells["Harga"].Value.ToString();
                textBox13.Text = row.Cells["Lama_inap"].Value.ToString();
                textBox15.Text = row.Cells["Jumlah"].Value.ToString();
                textBox17.Text = row.Cells["Info"].Value.ToString();
                checkBox1.Checked = Convert.ToBoolean(row.Cells["Keluar"].Value);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView3.DataSource = null;
            try
            {
                
            connection.Close();
            connection.Open();
            SqlCommand obat= new SqlCommand("SELECT * FROM Pengobatan WHERE Id_tagihan = '" + textBox10.Text + "'", connection);
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
                    textBox7.Text = (red["Total_harga"].ToString());
                    red.Close();
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

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
