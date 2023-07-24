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
    public partial class acount : Form
    {
        public acount()
        {
            InitializeComponent();
        }

        private void acount_Load(object sender, EventArgs e)
        {
            try
            {
                con_.emp.Close();
                con_.con.Close();
                con_.emp.Open();
                con_.con.Open();
                SqlCommand cmd = new SqlCommand("select fname,sname,salary,age,gender from emp  where username='" + con_.user + "'", con_.emp);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    label1.Text = "Name: " + dr.GetString(0) + " " + dr.GetString(1);
                    label2.Text = "Salary: " + dr.GetValue(2).ToString();
                    label3.Text = "Age: " + dr.GetInt32(3).ToString();
                    label4.Text = "Gender: " + dr.GetString(4);
                }
                con_.emp.Close();
                con_.emp.Open();
                SqlCommand cmd1 = new SqlCommand("select email from emial where username='" + con_.user + "'", con_.con);
                dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    listBox2.Items.Add(dr.GetString(0));
                }
                con_.emp.Close();
                con_.emp.Open();
                SqlCommand cmd2 = new SqlCommand("select pnum from phnum where username='" + con_.user + "'", con_.emp);
                dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    listBox1.Items.Add(dr.GetString(0));
                }
                con_.con.Close();
                con_.con.Open();
                SqlCommand cmd4 = new SqlCommand("select satrday,sunday,monday,tuseday,wensday,thersday,frieday from work where username='" + con_.user + "'", con_.con);
                dr = cmd4.ExecuteReader();
                while (dr.Read())
                {
                    label7.Text = "Satrday: " + dr.GetString(0);
                    label8.Text = "Sunday: " + dr.GetString(1);
                    label9.Text = "Mounday: " + dr.GetString(2);
                    label10.Text = "Tuesday: " + dr.GetString(3);
                    label11.Text = "Wensday: " + dr.GetString(4);
                    label12.Text = "Thersday: " + dr.GetString(5);
                    label13.Text = "Freiday: " + dr.GetString(6);
                }
            }
            catch
            {

            }
            con_.emp.Close();
            con_.con.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void acount_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
