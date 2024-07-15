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
    public partial class SifremiUnuttum : DevExpress.XtraEditors.XtraForm
    {

        Database1Entities db = new Database1Entities();
        public SifremiUnuttum()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            degistir();
        }

        void degistir()
        {
            try
            {
                Login kullanici = db.Login.First(s => s.Kullanici == textEdit4.Text.Trim());
                kullanici.Parola = textBox1.Text;
                db.SaveChanges();
                MessageBox.Show("Şifre Değiştirildi");
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcı Adı Bulunamadı !");
                return;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '\0';
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '\0';
            if (checkEdit1.Checked == false)
            {
                textBox1.PasswordChar = '*';
            }
        }
    }
}