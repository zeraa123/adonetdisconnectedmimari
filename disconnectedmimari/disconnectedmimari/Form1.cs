using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace disconnectedmimari
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-I2H6A9U\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Goster();
            //Disconnected mimari yöntemi ile yapılan işlem

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string SirketAdi = textBox1.Text;
            string Telefon = textBox2.Text;
            if(SirketAdi==null || Telefon == null)
            {
                MessageBox.Show("Şirket adını veya Telefon numarasını girmediniz.");
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = string.Format("insert into Nakliyeciler(SirketAdi,Telefon) Values('{0}','{1}')", SirketAdi, Telefon);
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("İşlem başarılı");
                Goster();
            }
           
        }

        public void Goster()
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from Nakliyeciler", connection);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            view.DataSource = dt;

        }

        private void view_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //datagridview'dan seçili satırı alma işlemi
            textBox1.Text = view.CurrentRow.Cells["SirketAdi"].Value.ToString();
            textBox2.Text = view.CurrentRow.Cells["Telefon"].Value.ToString();
        }
    }
}
