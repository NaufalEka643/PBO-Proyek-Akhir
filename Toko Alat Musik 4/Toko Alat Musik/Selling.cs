using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class Selling : Form
    {
        public Selling()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\Documents\MusicShopDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            String query = "select InstName,InstPrice from InstrumentTbl";
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
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void InstrumentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            InstNameTb.Text = InstrumentDGV.SelectedRows[0].Cells[0].Value.ToString();
            InstPriceTb.Text = InstrumentDGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void BrandSearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filter();
        }
        int n = 0,GrdTotal=0;
        private void button1_Click(object sender, EventArgs e)
        {
            if(InstNameTb.Text == "" || InstPriceTb.Text == "" || InstQtyTb.Text == "")
            {
                MessageBox.Show("Pilih Produk dan Masukkan Jumlahnya");
            }else
            {
                int total = Convert.ToInt32(InstQtyTb.Text) * Convert.ToInt32(InstPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = InstNameTb.Text;
                newRow.Cells[2].Value = InstPriceTb.Text;
                newRow.Cells[3].Value = InstQtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                n++;
                GrdTotal = GrdTotal + total;
                AmtLbl.Text = "" + GrdTotal;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void insertBill()
        {
            string Today;
            Today = DateTime.Today.Date.ToString();
            try
            {
                Con.Open();
                string query = "Insert into BillTbl values(" + GrdTotal + ",'"+Today+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Gudang berhasil ditambahkan");
                Con.Close();
                populate();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void InstrumentDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BrandSearchCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            insertBill();
        }
        int instid, instqty, instprice, tottal, pos = 60;

        private void InstNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        string instname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("MuziMaster Shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Tomato, new Point(80));
            e.Graphics.DrawString("ID    INSTRUMENT  PRICE   QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Regular), Brushes.Tomato, new Point(5, 20));
            foreach(DataGridViewRow row in BillDGV,Rows)
            {
                instid = Convert.ToInt32(row.Cells["Column1"].Value);
                instname =""+ Row.Cells["Column2"].Value;
                instprice = Convert.ToInt32(row.Cells["Column3"].Value);
                instqyt = Convert.ToInt32(row.Cells["Column4"].Value);
                tottal = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString(""+instid, new Font("Century Gothic", 10, FontStyle.Regular), Brushes.Blue, new Point(5, pos));
                e.Graphics.DrawString(""+instname, new Font("Century Gothic", 10, FontStyle.Regular), Brushes.Blue, new Point(25, pos));
                e.Graphics.DrawString("" + instprice, new Font("Century Gothic", 10, FontStyle.Regular), Brushes.Blue, new Point(125, pos));
                e.Graphics.DrawString("" + instqty, new Font("Century Gothic", 10, FontStyle.Regular), Brushes.Blue, new Point(190, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 10, FontStyle.Regular), Brushes.Blue, new Point(220, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Grand Total Rs"+GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Tomato, new Point(60, 200));
            e.Graphics.DrawString("****Shop Music The best****", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Tomato, new Point(50,230));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            InstNameTb.Text = "";
            InstQtyTb.Text = "";
            InstPriceTb.Text = "";

        }
    }
}
