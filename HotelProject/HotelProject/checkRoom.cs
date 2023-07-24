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
using System.Net;
using System.Net.Sockets;

namespace HotelProject
{
    public partial class checkRoom : Form
    {
        DateTime time1;
        DateTime time2;
        Socket sck;
        EndPoint epLocal, epRomote;
        byte[] buffer;
        public checkRoom()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#3b975c");
        }


        public static string ip1 = "";
        public static string ip2 = "";
        public static string port1 = "";
        public static string port2 = "";
        public void getIp() 
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            if (myIP == "192.168.43.243")
            {
                ip1 = "192.168.43.243";
                ip2 = "192.168.43.150";
                port1 = "81";
                port2 = "80";
            }
            else
            {
                ip1 = "192.168.43.150";
                ip2 = "192.168.43.243";
                port1 = "80";
                port2 = "81";
            }
           // MessageBox.Show(ip1 + "\n" + ip2);
        }







        private void checkRoom_Load(object sender, EventArgs e)
        {
            textBox6.Text = dateTimePicker3.Value.ToShortTimeString();
            Logi op = new Logi();
            op.timer1.Start();
            comboBox1.Text = "DRK";
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            getIp();
            epLocal = new IPEndPoint(IPAddress.Parse("192.168.43.243"), Convert.ToInt32("81"));
            sck.Bind(epLocal);
            epRomote = new IPEndPoint(IPAddress.Parse("192.168.43.243"), Convert.ToInt32("80"));
            sck.Connect(epRomote);
            buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRomote, new AsyncCallback(MessageCallBack), buffer);
        }
        private void send()
        {
           
                    ASCIIEncoding aEncoding = new ASCIIEncoding();
                    byte[] sendingMessge = new byte[1500];
                    sendingMessge = aEncoding.GetBytes(textBox3.Text);

                    sck.Send(sendingMessge);
                   listBox2.Items.Add("Me: " + textBox3.Text+"\n");
                    textBox3.Text = "";
            
        }


        private void MessageCallBack(IAsyncResult aResoult)
        {
            try
            {
                byte[] reciveData = new byte[1500];
                reciveData = (byte[])aResoult.AsyncState;

                ASCIIEncoding aEncoding = new ASCIIEncoding();
                string receveMessge = aEncoding.GetString(reciveData);

                listBox2.Items.Add("You: " + receveMessge.ToString() + "\n");

                buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRomote, new AsyncCallback(MessageCallBack), buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void retRoom(string type)
        {
            listBox1.Items.Clear();
            try
            {  con_.con.Open();
                        SqlCommand cmd = new SqlCommand("select room_id from room where check_='NO' and tp_id='" + type + "'", con_.con);
                        SqlDataReader dr;
                        dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            listBox1.Items.Add(dr.GetValue(0));
                        }
                        con_.con.Close();
                
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            retRoom(comboBox1.Text);
            aboutRoom();
        }



        private void aboutRoom()
        {
            string abt = "About Room:";
            try
            {
                if (comboBox1.Text == "DRK")
                {
                    label6.Text = abt + "\n" + " (deluxe room\nking size bed)\n" + "110$";
                }
                else if (comboBox1.SelectedItem == "DRK")
                {
                    label6.Text = abt + "\n" + " (deluxe room\nking size bed)\n" + "110$";
                }
                else if (comboBox1.SelectedItem == "DRT")
                {
                    label6.Text = abt + "\n" + " (Deluxe room\ntwin beds)\n" + "180$";
                }
                else if (comboBox1.SelectedItem == "CNS")
                {
                    label6.Text = abt + "\n" + " (connectedsuite \nfamily room)\n" + "250$";
                }
                else if (comboBox1.SelectedItem == "SSK")
                {
                    label6.Text = abt + "\n" + " (senior suit \nking size bed )\n" + "190$";
                }
                else if (comboBox1.SelectedItem == "JSK")
                {
                    label6.Text = abt + "\n" + " (Junior suite \nking size bed)\n" + "160$";
                }
                else if (comboBox1.SelectedItem == "HSK")
                {
                    label6.Text = abt + "\n" + " (Honeymoon\n suite king\n size bed)\n" + "210$";
                }
            }
            catch
            {

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < dateTimePicker2.Value)
            {
                sumDay(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString());
                price(comboBox1.Text);
            }
        }


        private int sumDay(string d1, string d2)
        {
            time1 = Convert.ToDateTime(d1);
            time2 = Convert.ToDateTime(d2);
            int day = (time1.Year * 365) + (time1.Month * 30) + (time1.Day);
            int day2 = (time2.Year * 365) + (time2.Month * 30) + (time2.Day);
            label8.Text = "Days: " + (day2 - day);
            return day2 - day;
        }


        private int price(string tp)
        {
            sumDay(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString());
            int prcice = 0;
            try
            {
                if (tp == "DRK")
                {
                    prcice = 110;
                }
                else if (tp == "DRT")
                {
                    prcice = 180;
                }
                else if (tp == "CNS")
                {
                    prcice = 250;
                }
                else if (tp == "SSK")
                {
                    prcice = 190;
                }
                else if (tp == "JSK")
                {
                    prcice = 160;
                }
                else if (tp == "HSK")
                {
                    prcice = 210;
                }
            }
            catch
            {

            }
            int ret = sumDay(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString()) * prcice;
            textBox1.Text = ret.ToString();
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                        int prc = 0;
                        string wait = "";
                        if (checkBox1.Checked == true)
                        {
                            wait = "YES";
                            prc = 0;
                        }
                        else
                        {
                            wait = "NO";
                            prc = price(comboBox1.Text);
                        }
                        con_.con.Open();
                        SqlCommand cmd = new SqlCommand("insert chec values ('" + listBox1.SelectedItem + "','" + sumDay(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString()) + "','" + prc + "','" + wait + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "')", con_.con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Check");
                        con_.con.Close();
             
            }
            catch (Exception c)
            {
                MessageBox.Show("please check information\n" + c.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con_.con.Close();
            con_.con.Open();
            if (checkBox1.Checked == true)
            {
                SqlCommand cmd = new SqlCommand("update chec set wait='YES' where room_id='"+textBox2.Text+"' and ch_in='"+dateTimePicker1.Value.ToString()+"'");
            }
            else
            {

                SqlCommand cmd = new SqlCommand("update chec set sumDay='"+textBox2.Text+"' where room_id='" + textBox2.Text + "' and ch_in='" + dateTimePicker1.Value.ToString() + "'");
            }
            con_
                .con.Close();
        }
        string day1 = "";
        string day2 = "";
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }
        

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            send();
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            string type = "";
            string wait = "";
            int day = 0;
            int prce = 0;

            try
            {
                con_.con.Close();
                con_.con.Open();
                SqlCommand cmd = new SqlCommand("select room.tp_id,chec.numDay,chec.wait,chec.ch_in,chec.ch_out,chec.sumPrc from room inner join chec on(room.room_id=chec.room_id) where room.room_id='" + textBox2.Text + "' and chec.ch_in='" + dateTimePicker1.Value.ToString() + "'", con_.con);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                string m = "";
                while (dr.Read())
                {
                    type = dr.GetString(0);
                    day = dr.GetInt32(1);
                    wait = dr.GetString(2);
                    dateTimePicker1.Value = dr.GetDateTime(3);
                    dateTimePicker2.Value = dr.GetDateTime(4);
                    // prce = dr.GetInt32(5);
                }
                price(type);
                if (wait == "YES")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                label8.Text = "Days: " + day;
                label10.Text = "Price: " + price(type);
                textBox1.Text = prce.ToString();
                con_.con.Close();
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message);
            }


        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            retRoom(comboBox1.Text);
            aboutRoom();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < dateTimePicker2.Value)
            {
                sumDay(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString());
                price(comboBox1.Text);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            try
            {
                int prc = 0;
                string wait = "";
                if (checkBox1.Checked == true)
                {
                    wait = "YES";
                    prc = 0;
                }
                else
                {
                    wait = "NO";
                    prc = price(comboBox1.Text);
                }
                con_.con.Open();
                SqlCommand cmd = new SqlCommand("insert chec values ('" + listBox1.SelectedItem + "','" + sumDay(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString()) + "','" + prc + "','" + wait + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "')", con_.con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Check");
                con_.con.Close();

            }
            catch (Exception c)
            {
                MessageBox.Show("please check information\n" + c.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            con_.con.Close();
            con_.con.Open();
            if (checkBox1.Checked == true)
            {
                SqlCommand cmd = new SqlCommand("update chec set wait='YES' where room_id='" + textBox2.Text + "' and ch_in='" + dateTimePicker1.Value.ToString() + "'");
            }
            else
            {

                SqlCommand cmd = new SqlCommand("update chec set sumDay='" + textBox2.Text + "' where room_id='" + textBox2.Text + "' and ch_in='" + dateTimePicker1.Value.ToString() + "'");
            }
            con_
                .con.Close();
        }

        private void checkRoom_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logi op = new Logi();
            op.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con_.emp.Close();
            con_.emp.Open();
            SqlCommand cmd = new SqlCommand("insert desk values (@a,@b,@c,@d)",con_.emp);
            cmd.Parameters.AddWithValue("@a", textBox4.Text);
            cmd.Parameters.AddWithValue("@b", textBox5.Text);
            cmd.Parameters.AddWithValue("@c", textBox6.Text);
            cmd.Parameters.AddWithValue("@d", dateTimePicker3.Value.ToShortDateString());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Check");
            con_.emp.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dateTimePicker1.Value.ToString());
        }
    }
}