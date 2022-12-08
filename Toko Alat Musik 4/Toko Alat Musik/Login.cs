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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UidTb.Text = "";
            PassTb.Text = "";
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Asus\Documents\MusicShopDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void button3_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AdminTbl where AdmName='"+UidTb.Text+"' and AdmPass='"+PassTb.Text+"'",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Instrument instr = new Instrument();
                instr.Show();
                this.Hide();
                Con.Close();
            }else
            {
                MessageBox.Show("Wrong Admin Name or Password");
            }
            Con.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Selling sel = new Selling();
            sel.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UidTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
