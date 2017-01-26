using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hospital.Properties;

namespace Hospital
{
    public partial class Homepage : Form
    {
        Image addpatient = Resources.addpatient;

        public Homepage()
        {
            InitializeComponent();
            pictureBox1.Image = addpatient;
        }

        private void pasienToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void keluarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Show();
        }

        private void kelolaAkunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Admin adm = new Admin();
            adm.Show();
        }

        private void rawatInapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            PasienInap ip = new PasienInap();
            ip.Show();
        }

        private void rAWATINAPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            OutPatient op = new OutPatient();
            op.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            TagihanPasienInap inb = new TagihanPasienInap();
            inb.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            TagihanPasienJalan outb = new TagihanPasienJalan();
            outb.Show();
        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TambahPasien addin = new TambahPasien();
            addin.Show();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            int addpatient_Width = addpatient.Width + ((addpatient.Width * 20) / 100);
            int addpatient_Height = addpatient.Height + ((addpatient.Height * 20) / 100);

            Bitmap addpatient1 =new Bitmap(addpatient_Width, addpatient_Height);
            Graphics g = Graphics.FromImage(addpatient1);
            g.DrawImage(addpatient, new Rectangle(Point.Empty, addpatient1.Size));
            pictureBox1.Image = addpatient1;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = addpatient;
        }
        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Visible = false; 
            TambahPasien addin = new TambahPasien();
            addin.Show();
        }

        private void Homepage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void staffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Dokter doc = new Dokter();
            doc.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox5.Visible == true)
            {
                pictureBox5.Visible = false;
                pictureBox6.Visible = true;
            }
            else if (pictureBox6.Visible == true)
            {
                pictureBox6.Visible = false;
                pictureBox7.Visible = true;
            }
            else if (pictureBox7.Visible == true)
            {
                pictureBox7.Visible = false;
                pictureBox8.Visible = true;
            }
            else if (pictureBox8.Visible == true)
            {
                pictureBox8.Visible = false;
                pictureBox9.Visible = true;
            }
            else if (pictureBox9.Visible == true)
            {
                pictureBox9.Visible = false;
                pictureBox10.Visible = true;
            }
            else if (pictureBox10.Visible == true)
            {
                pictureBox10.Visible = false;
                pictureBox5.Visible = true;
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Obat obat = new Obat();
            obat.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Kamar kamar = new Kamar();
            kamar.Show();
        }

    }
}
