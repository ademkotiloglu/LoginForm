using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    
    public partial class Ana : DevExpress.XtraEditors.XtraForm
    {
        Database1Entities db = new Database1Entities();
        bool edit = false;
        int secimID = -1;
        public Ana()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (secimID > 0) 
            duzenle();
            else
                kaydet();
        }
        void kaydet()
        {
            Login login = new Login();
            login.Kullanici = textEdit1.Text;
            login.Parola = textEdit2.Text;
            login.Durum = comboBox1.SelectedIndex;
            db.Login.Add(login);
            db.SaveChanges();
            dataGridView1.DataSource = db.Login.OrderByDescending(x => x.Id).ToList();
            gridduzenle1(dataGridView1);
            textEdit1.Text = "";
            textEdit2.Text = "";
            MessageBox.Show("başarılı");
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int yoneticiid = int.Parse(yonetimid.Text);
            if (yoneticiid == 0)
            {
                FormYetki frm1 = new FormYetki();
                frm1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Form 2 Açma Yetkiniz Yok");
            }
        }

        private void Ana_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = db.Login.OrderByDescending(x => x.Id).ToList();
            gridduzenle1(dataGridView1);
            label6.Text = secimID.ToString();
        }

        private void Ana_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public static void gridduzenle1(DataGridView dgv)
        {
            if (dgv.Columns.Count > 0)
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    switch (dgv.Columns[i].HeaderText)
                    {


                        case "Id":
                            dgv.Columns[i].Visible = false; break;


                    }
                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

           
            
        }

        void sil()
        {
          
                int id = int.Parse(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
                var model = db.Login.FirstOrDefault(x => x.Id == id);
                db.Login.Remove(model);
                db.SaveChanges();
                XtraMessageBox.Show("Silme Başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = db.Login.OrderByDescending(x => x.Id).ToList();
            gridduzenle1(dataGridView1);
            textEdit1.Text = "";
            textEdit2.Text = "";
            
        }


        void duzenle()
        {
            int id = int.Parse(label5.Text);
            var uye = db.Login.FirstOrDefault(x => x.Id == id);
            uye.Kullanici = textEdit1.Text;
            uye.Parola = textEdit2.Text;
            uye.Durum = comboBox1.SelectedIndex;
            db.SaveChanges();
            XtraMessageBox.Show("Düzenleme Başarılı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = db.Login.OrderByDescending(x => x.Id).ToList();
            gridduzenle1(dataGridView1);
            textEdit1.Text = "";
            textEdit2.Text = "";

        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sil();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            edit = true;
            secimID = int.Parse(dataGridView1.CurrentRow.Cells["Id"].Value.ToString());
            label5.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            textEdit1.Text = dataGridView1.CurrentRow.Cells["Kullanici"].Value.ToString();
            textEdit2.Text = dataGridView1.CurrentRow.Cells["Parola"].Value.ToString();
            label4.Text = dataGridView1.CurrentRow.Cells["Durum"].Value.ToString();
            label6.Text = secimID.ToString();
            int durum1 = int.Parse(label4.Text);
            if (durum1 == 0)
            {
                comboBox1.Text = "Yönetici";
            }
            else
            {
                comboBox1.Text = "Misafir";
            }
        }
    }
}