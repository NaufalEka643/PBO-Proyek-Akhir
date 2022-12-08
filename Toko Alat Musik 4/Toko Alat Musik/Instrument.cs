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
using Microsoft.Data.SqlClient;
using Npgsql;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace Toko_Alat_Musik
{
    public partial class Instrument : Form
    {
        public Instrument()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\Documents\MusicShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(InstNameTb.Text == "" || InstPriceTb.Text == "" || InstQtyTb.Text == "")
            {
                MessageBox.Show("Isilah yang kosong");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Insert into InstrumentTbl values('" + InstNameTb.Text + "','" + InstBrandCb.SelectedItem.ToString() + "'," + InstQtyTb.Text + "," + InstPriceTb.Text + ",'" + InstCatCb.SelectedItem.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Gudang berhasil ditambahkan");
                    Con.Close();
                    populate();

                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void populate()
        {
            Con.Open();
            String query = "select * from InstrumentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder (sda);
            var ds = new DataSet();
            sda.Fill(ds);
            InstrumentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void filter()
        {
            Con.Open();
            String query = "select * from InstrumentTbl where InstBrand='"+BrandSearchCb.SelectedItem.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            InstrumentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int InstKey;
        private void button3_Click(object sender, EventArgs e)
        {
            if (InstNameTb.Text == "")
            {
                MessageBox.Show("Silahkan mana yang mau dihapus");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from InstrumentTbl";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Barang berhasil dihapus");
                    Con.Close();
                    populate();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }


        private void InstrumenDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            InstKey = Convert.ToInt32(InstrumentDGV.SelectedRows[0].Cells[0].Value.ToString());
            InstNameTb.Text = InstrumentDGV.SelectedRows[0].Cells[1].Value.ToString();
            InstQtyTb.Text = InstrumentDGV.SelectedRows[0].Cells[3].Value.ToString();
            InstPriceTb.Text = InstrumentDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void Gudang_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (InstPriceTb.Text == "" || InstPriceTb.Text == "" || InstQtyTb.Text == "")
            {
                MessageBox.Show("Informasi Hilang");
            }else
            {
                try
                {
                    Con.Open();
                    string query = "update InstrumentTbl set InstNama='" + InstNameTb.Text + "',InstBrand='" + InstBrandCb.SelectedItem.ToString() + "',InstQty=" + InstQtyTb.Text + ",InstHarga=" + InstPriceTb.Text + ",InstCat=" + InstCatCb.SelectedItem.ToString() + "'where InstId=" + InstKey + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Gudang Berhasil diperbaharui");
                    Con.Close();
                    populate();

                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void InstNamaTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void BrandSearchCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filter();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void InstQtyTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
