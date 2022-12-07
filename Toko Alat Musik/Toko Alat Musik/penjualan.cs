using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toko_Alat_Musik
{
    public partial class penjualan : Form
    {
        public penjualan()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\Documents\MusicShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            String query = "select InstNama,InstHarga from InstrumentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            InstrumentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void filter()
        {
            Con.Open();
            String query = "select InstNama,InstHarga from InstrumentTbl where InstBrand='" + BrandSearchCb.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            InstrumentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void penjualan_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void InstrumentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            InstNamaTb.Text = InstrumentDGV.SelectedRows[0].Cells[0].Value.ToString();
            InstHargaTb.Text = InstrumentDGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void BrandSearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filter();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            populate();
        }
    }
}
