using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        Database1Entities db = new Database1Entities();
       
        public Form1()
        {
            InitializeComponent();
            //beni hatırla için ekle
            Inıt_Data();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
           
        }
        // mükerrer kayit bulma
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string yeniKullaniciAdi = textEdit4.Text;
            var varMi = db.Login.Any(k => k.Kullanici == yeniKullaniciAdi);
            if (!varMi)
            {
                kaydet();
            }
               else
            {
                MessageBox.Show("Bu Kullanıcı Daha Önce Kayıt Edilmiş !");
            }
        }

        void kaydet()
        {
            Login login = new Login();
            login.Kullanici = textEdit4.Text;
            login.Parola = textEdit3.Text;
            login.Durum = 2;
            db.Login.Add(login);
            db.SaveChanges();
            MessageBox.Show("başarılı");

            groupBox2.Visible = false;
            groupBox1.Visible = true;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
          
                try
                {
                    Login kullanici = db.Login.First(s => s.Kullanici == textEdit1.Text.Trim() && s.Parola == textEdit2.Text.Trim());
                MessageBox.Show("Başarılı Olarak Giriş Yaptınız");
                //beni hatırla için ekle
                save_data();
                this.Hide();
                Ana frm = new Ana();
                frm.Show();
                
                frm.yonetimid.Text = kullanici.Durum.ToString();
                if (kullanici.Durum == 0 || kullanici.Durum ==2)
                {
                    frm.dataGridView1.Enabled = true;
                    frm.groupBox1.Enabled = true;
                }
                else
                {
                    frm.dataGridView1.Enabled = false;
                    frm.groupBox1.Enabled = false;
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Giriş Yapılamadı . Kullanıc Adı veya Şifreyi Kontrol Ediniz ..");
                    return;
                }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox1.Visible = true;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            SifremiUnuttum sfr = new SifremiUnuttum();
            sfr.ShowDialog();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkEdit1_CheckedChanged_1(object sender, EventArgs e)
        {
            textEdit2.PasswordChar = '\0';
            if (checkEdit1.Checked == false)
            {
                textEdit2.PasswordChar = '*';
            }
        }

        //beni hatırla için ekle
        private void Inıt_Data()
        {
            if (Properties.Settings.Default.Username != string.Empty)
            {
                if (Properties.Settings.Default.Remember == true)
                {
                    textEdit1.Text = Properties.Settings.Default.Username;
                    textEdit2.Text = Properties.Settings.Default.Password;
                    checkEdit2.Checked = true;
                }
                else
                {
                    textEdit1.Text = Properties.Settings.Default.Username;
                }
            }
        }

        private void save_data()
        {
            if (checkEdit2.Checked)
            {
                Properties.Settings.Default.Username = textEdit1.Text;
                Properties.Settings.Default.Password = textEdit2.Text;
                Properties.Settings.Default.Remember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Remember = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
