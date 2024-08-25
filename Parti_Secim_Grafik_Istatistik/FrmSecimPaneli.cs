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
    public partial class FrmSecimPaneli : Form
    {
        public FrmSecimPaneli()
        {
            InitializeComponent();
        }


        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=DbSecimProje;Integrated Security=True");

        private void btnOyGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TblIlce (ILCEAD,APARTI,BPARTI,CPARTI,DPARTI,EPARTI) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtILCEAD.Text);
            komut.Parameters.AddWithValue("@p2", txtAPARTI.Text);
            komut.Parameters.AddWithValue("@p3", txtBPARTI.Text);
            komut.Parameters.AddWithValue("@p4", txtCPARTI.Text);
            komut.Parameters.AddWithValue("@p5", txtDPARTI.Text);
            komut.Parameters.AddWithValue("@p6", txtEPARTI.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Oy girişi gerçekleşti.");
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            Grafikler fr= new Grafikler();
            fr.ShowDialog();
        }

        private void btnCikisYap_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
