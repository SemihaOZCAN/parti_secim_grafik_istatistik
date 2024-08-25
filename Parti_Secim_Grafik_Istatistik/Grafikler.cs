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


namespace Parti_Secim_Grafik_Istatistik
{
    public partial class Grafikler : Form
    {
        public Grafikler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=DbSecimProje;Integrated Security=True");
        private void Grafikler_Load(object sender, EventArgs e)
        {
            //İlçe adlarını comboboxa çekme...
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select ILCEAD from TblIlce ", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            baglanti.Close();

            //Grafiğe toplam sonuçları getirme...
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select SUM(APARTI),SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) from TblIlce", baglanti);
            SqlDataReader dr2=komut2.ExecuteReader();
            while (dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A PARTİ", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("B PARTİ", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("C PARTİ", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D PARTİ", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E PARTİ", dr2[4]);

            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // İlçe seçildikten sonra progressBara oyların yansıması ve oy oranı
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TblIlce where ILCEAD=@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", comboBox1.Text);

            SqlDataReader dr = komut.ExecuteReader();
            int aParti = 0, bParti = 0, cParti = 0, dParti = 0, eParti = 0;

            while (dr.Read())
            {
                aParti = int.Parse(dr[2].ToString());
                bParti = int.Parse(dr[3].ToString());
                cParti = int.Parse(dr[4].ToString());
                dParti = int.Parse(dr[5].ToString());
                eParti = int.Parse(dr[6].ToString());

                progressBar1.Value = aParti;
                progressBar2.Value = bParti;
                progressBar3.Value = cParti;
                progressBar4.Value = dParti;
                progressBar5.Value = eParti;

                lblA.Text = aParti.ToString();
                lblB.Text = bParti.ToString();
                lblC.Text = cParti.ToString();
                lblD.Text = dParti.ToString();
                lblE.Text = eParti.ToString();
            }
            dr.Close();
            baglanti.Close();

            // En yüksek oyu alan partiyi bulma
            int[] oylar = { aParti, bParti, cParti, dParti, eParti };
            int maxOy = oylar.Max();
            string kazananParti = "";

            if (maxOy == aParti) kazananParti = "A PARTİ";
            else if (maxOy == bParti) kazananParti = "B PARTİ";
            else if (maxOy == cParti) kazananParti = "C PARTİ";
            else if (maxOy == dParti) kazananParti = "D PARTİ";
            else if (maxOy == eParti) kazananParti = "E PARTİ";

            // Kazanan partiyi lblKAZANAN'a yazdırma
            lblKAZANAN.Text = "" + kazananParti;
        }
        
    }
}
