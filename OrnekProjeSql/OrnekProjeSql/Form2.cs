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

namespace OrnekProjeSql
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=kutuphaneyeni;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da=new SqlDataAdapter("select * from ogrenci", baglanti);
            DataSet ds=new DataSet();//sanal tablo
            da.Fill(ds);//dataAdapterın içini ds ile doldur
            dataGridView1.DataSource = ds.Tables[0];//dataGridView1 in kaynağı ds.tables olarak verilir.
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kayitekle= new SqlCommand("insert into ogrenci (ad,soyad,dtarih,cinsiyet,sinif,puan) values (@k1,@k2,@k3,@k4,@k5,@k6)", baglanti);
            kayitekle.Parameters.AddWithValue("@k1", textBox1.Text);
            kayitekle.Parameters.AddWithValue("@k2", textBox2.Text);
            kayitekle.Parameters.AddWithValue("@k3", textBox3.Text);
            kayitekle.Parameters.AddWithValue("@k4", textBox4.Text);
            kayitekle.Parameters.AddWithValue("@k5", textBox5.Text);
            kayitekle.Parameters.AddWithValue("@k6", textBox6.Text);

            kayitekle.ExecuteNonQuery();
            baglanti.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sil = new SqlCommand("delete from ogrenci where ad=@silinecek",baglanti);

            sil.Parameters.AddWithValue("@silinecek",textBox2.Text);//ad alanına girilen değeri alsın
            sil.ExecuteNonQuery();
            baglanti.Close();//id ye göre sil
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen= dataGridView1.SelectedCells[0].RowIndex;

            string no = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            string ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            string soyad = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            string dtarih= dataGridView1.Rows[secilen].Cells[3].Value.ToString();   
            string cinsiyet= dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            string sinif = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            string puan= dataGridView1.Rows[secilen].Cells[6].Value.ToString();

            textBox1.Text = no;
            textBox2.Text = ad;
            textBox3.Text = soyad;
            textBox4.Text = dtarih;
            textBox5.Text = cinsiyet;
            textBox6.Text = sinif;
            textBox7.Text = puan;
    }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("update ogrenci set ad=@ad,soyad=@soyad,dtarih=@dtarih,cinsiyet=@cinsiyet,sinif=@sinif,puan=@puan where ogrno=@ogrno",baglanti);
           
            komutguncelle.Parameters.AddWithValue("@ogrno",textBox1.Text); 
            komutguncelle.Parameters.AddWithValue("@ad",textBox2.Text);
            komutguncelle.Parameters.AddWithValue("@soyad",textBox3.Text);
            komutguncelle.Parameters.AddWithValue("@dtarih",textBox4.Text);
            komutguncelle.Parameters.AddWithValue("@cinsiyet",textBox5.Text);
            komutguncelle.Parameters.AddWithValue("@sinif",textBox6.Text);
            komutguncelle.Parameters.AddWithValue("@puan",textBox7.Text);
            
          
            komutguncelle.ExecuteNonQuery();
       
            baglanti.Close();

            getStudent();
        }
        private void getStudent()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from ogrenci", baglanti);
            DataSet ds = new DataSet();//sanal tablo
            da.Fill(ds);//dataAdapterın içini ds ile doldur
            dataGridView1.DataSource = ds.Tables[0];//dataGridView1 in kaynağı ds.tables olarak verilir.
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            SqlDataAdapter da= new SqlDataAdapter("select * from ogrenci where ad='" + textBox8.Text + "'" ,baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource= ds.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da= new SqlDataAdapter("select top 1 ogrenci.ad,puan from ogrenci order by puan desc ", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = (ds.Tables[0]);

            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select count(sinif) from ogrenci group by sinif", baglanti);
            SqlDataReader oku = komut1.ExecuteReader();
            while (oku.Read())
            {
                label11.Text = oku[0].ToString();

            }
            baglanti.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT ad as 'Öğrenci Adı',soyad as 'Öğrenci Soyadı',puan as 'Puanı' from ogrenci", baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = (ds.Tables[0]);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select ogrenci.sinif,COUNT(sinif) from ogrenci group by (sinif)", baglanti);
            DataSet ds= new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = (ds.Tables[0]);

            baglanti.Open();
            SqlCommand komut2= new SqlCommand("select count(ogrno) from ogrenci",baglanti);
            SqlDataReader oku2=komut2.ExecuteReader();
            while (oku2.Read())
            {
                label12.Text= oku2[0].ToString();
            }
            baglanti.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {
          

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    } 
    
 
}
