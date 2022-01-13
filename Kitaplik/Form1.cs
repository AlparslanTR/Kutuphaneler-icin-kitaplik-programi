using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kitaplik
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\legol\Desktop\Kitaplik.mdb");
        void listele() // Listeleme
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From Kitaplar", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        void temizle() // Textboxları Temizleme 
        {
            TxtKitapID.Text = "";
            TxtKitapAd.Text = "";
            TxtSayfa.Text = "";
            TxtYazar.Text = "";
            comboBox1.Text = "";
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void Form1_Load(object sender, EventArgs e) // Form Açılırken
        {
            listele();
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void BtnListele_Click(object sender, EventArgs e) // Listeleme
        {
            listele();
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        string durum = ""; // Radio butonları için
        private void BtnKaydet_Click(object sender, EventArgs e) // Kaydetme İşlemi
        {
            baglanti.Open();
            OleDbCommand kaydet = new OleDbCommand("Insert into Kitaplar(KitapAd,YazarAd,Tur,Sayfa,Durum)values(@p1,@p2,@p3,@p4,@p5)", baglanti);
            kaydet.Parameters.AddWithValue("@p1", TxtKitapAd.Text);
            kaydet.Parameters.AddWithValue("@p2", TxtYazar.Text);
            kaydet.Parameters.AddWithValue("@p3", comboBox1.Text);
            kaydet.Parameters.AddWithValue("@p4", TxtSayfa.Text);
            kaydet.Parameters.AddWithValue("@p5", durum);
            kaydet.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Kaydedildi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // Çift Tıklandığında verileri textboxlara aktar
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtKitapID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtKitapAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void BtnSil_Click(object sender, EventArgs e) // Silme İşlemi
        {
            DialogResult result = MessageBox.Show("Silmek İstiyor musunuz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                baglanti.Open();
                OleDbCommand sil = new OleDbCommand("Delete From Kitaplar where KitapID=@p1", baglanti);
                sil.Parameters.AddWithValue("@p1", TxtKitapID.Text);
                sil.ExecuteNonQuery();
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Silme İşlemi İptal Edilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            listele();
            temizle();
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        private void BtnGuncelle_Click(object sender, EventArgs e) // Güncelleme İşlemiş
        {
            DialogResult result = MessageBox.Show("Güncellemek İstiyor musunuz ?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                baglanti.Open();
                OleDbCommand guncelle = new OleDbCommand("Update Kitaplar set KitapAd=@p1,YazarAd=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where KitapID=@p6", baglanti);
                guncelle.Parameters.AddWithValue("@p1", TxtKitapAd.Text);
                guncelle.Parameters.AddWithValue("@p2", TxtYazar.Text);
                guncelle.Parameters.AddWithValue("@p3", comboBox1.Text);
                guncelle.Parameters.AddWithValue("@p4", TxtSayfa.Text);
                if (radioButton1.Checked == true)
                {
                    guncelle.Parameters.AddWithValue("@p5", durum);
                }
                if (radioButton2.Checked == true)
                {
                    guncelle.Parameters.AddWithValue("@p5", durum);
                }
                guncelle.Parameters.AddWithValue("@p6", TxtKitapID.Text);
                guncelle.ExecuteNonQuery();
                baglanti.Close();
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("Güncelleme İşlemi İptal Edilmiştir.");
                listele();
                temizle();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand ara = new OleDbCommand("Select*from Kitaplar where KitapAd like '%"+TxtArama.Text+"%'",baglanti);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(ara);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        // BİTTİ :)
    }
}
