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
    public partial class Form1 : Form
    {
        DateTime d = DateTime.Now;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            con_.con.Close();
            string tomorrow = d.Month + "/" + (d.Day + 1) + "/" + d.Year;
            string today = d.ToShortDateString();
            con_.con.Open();
            try
            {
                if (d.Hour == 23 && d.Minute >= 50)
                {
                    SqlCommand cmd = new SqlCommand("update room set check_ = 'YES' where room_id in (select room_id from chec where ch_in = '" + tomorrow + "')", con_.con);
                    SqlCommand cmd1 = new SqlCommand("update room set check_ = 'NO' where room_id = any(select room_id from chec where ch_out = '" + today + "')", con_.con);
                    SqlCommand cmd2 = new SqlCommand("update room set check_ = 'NO' where room_id in(select room_id from chec where ch_in = '" + today + "' and (sumPrc = 0 and wait = 'NO'))", con_.con);
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
                SqlCommand cmd3 = new SqlCommand("update room set check_ = 'YES' where room_id in(select room_id from chec where ch_in = '" + today + "' and (sumPrc != 0 or wait = 'YES'))", con_.con);
                cmd3.ExecuteNonQuery();
                con_.con.Close();
            }
            catch (Exception c)
            {
                timer1.Enabled = false;
                MessageBox.Show(c.Message);
            }
        }

        private void test_Click(object sender, EventArgs e)
        {
            timer1.Start();
            MessageBox.Show(d.Hour.ToString() + "  " + d.Minute.ToString());
        }




        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
