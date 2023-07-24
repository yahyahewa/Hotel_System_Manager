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
namespace HotelProject
{
    public partial class rest : Form
    {
        public rest()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection emp = new SqlConnection("Data Source=192.168.43.150;Initial Catalog=rest;Persist Security Info=True;User ID=sa;Password=sa");
            emp.Close();
            emp.Open();
            SqlCommand cmd = new SqlCommand("insert desk values (@a,@b,@c,@d)", emp);
            cmd.Parameters.AddWithValue("@a", textBox4.Text);
            cmd.Parameters.AddWithValue("@b", textBox5.Text);
            cmd.Parameters.AddWithValue("@c", textBox6.Text);
            cmd.Parameters.AddWithValue("@d", dateTimePicker3.Value.ToShortDateString());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Check");
            emp.Close();
        }

        private void rest_Load(object sender, EventArgs e)
        {
            textBox6.Text = dateTimePicker3.Value.ToShortTimeString();
            ret();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ret();
        }
        private void ret()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-8A3O2K0;Initial Catalog=master;Integrated Security=True");
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select [name],[Number of Person],[check in] from dbo.desk where [date]='" + dateTimePicker3.Value.ToShortDateString() + "'", con);
            DataTable dt = new DataTable();
            SqlDataAdapter dr = new SqlDataAdapter(cmd);
            dr.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ret();
        }
    }
}
