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
    public partial class admin : Form
    {
        SqlCommand sqlcmd;
        SqlDataAdapter sda;
        SqlDataReader dr;
        DataTable dt;
        Socket sck;
        EndPoint epLocal, epRomote;
        byte[] buffer;
        public admin()
        {
            InitializeComponent();
            comboBox1.Items.Add("admin");
            comboBox1.Items.Add("reception");
            comboBox1.Items.Add("resturant");
            comboBox2.Items.Add("male");
            comboBox2.Items.Add("female");
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con_.con.Close();
                con_.emp.Close();
                string[] week = new string[7];
                for (int i = 0; i < week.Length; i++) { week[i] = "NO"; }
                if (checkBox1.Checked == true) { week[0] = "YES"; }
                if (checkBox2.Checked == true) { week[1] = "YES"; }
                if (checkBox3.Checked == true) { week[2] = "YES"; }
                if (checkBox4.Checked == true) { week[3] = "YES"; }
                if (checkBox5.Checked == true) { week[4] = "YES"; }
                if (checkBox6.Checked == true) { week[5] = "YES"; }
                if (checkBox7.Checked == true) { week[6] = "YES"; }
                con_.con.Open();
                con_.emp.Open();
                SqlCommand cmd = new SqlCommand("insert log_ values (@a,@b,@c)", con_.emp);
                cmd.Parameters.AddWithValue("@a", textBox1.Text);
                cmd.Parameters.AddWithValue("@b", textBox2.Text);
                cmd.Parameters.AddWithValue("@c", comboBox1.Text);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("insert emp values (@a,@b,@c,@d,@e,@f)", con_.emp);
                cmd1.Parameters.AddWithValue("@a", textBox1.Text);
                cmd1.Parameters.AddWithValue("@b", textBox3.Text);
                cmd1.Parameters.AddWithValue("@c", textBox4.Text);
                cmd1.Parameters.AddWithValue("@d", textBox5.Text);
                cmd1.Parameters.AddWithValue("@e", textBox6.Text);
                cmd1.Parameters.AddWithValue("@f", comboBox2.Text);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("insert work values(@a,@b,@c,@d,@e,@f,@g,@h)", con_.con);
                cmd2.Parameters.AddWithValue("@a", textBox1.Text);
                cmd2.Parameters.AddWithValue("@b", week[0]);
                cmd2.Parameters.AddWithValue("@c", week[1]);
                cmd2.Parameters.AddWithValue("@d", week[2]);
                cmd2.Parameters.AddWithValue("@e", week[3]);
                cmd2.Parameters.AddWithValue("@f", week[4]);
                cmd2.Parameters.AddWithValue("@g", week[5]);
                cmd2.Parameters.AddWithValue("@h", week[6]);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("insert emial values (@a,@b)", con_.con);
                cmd3.Parameters.AddWithValue("@a", textBox1.Text);
                cmd3.Parameters.AddWithValue("@b", textBox8.Text);
                cmd3.ExecuteNonQuery();
                SqlCommand cmd4 = new SqlCommand("insert phnum values (@a,@b)", con_.emp);
                cmd4.Parameters.AddWithValue("@a", textBox1.Text);
                cmd4.Parameters.AddWithValue("@b", textBox7.Text);
                cmd4.ExecuteNonQuery();
                MessageBox.Show("Add");
                con_.emp.Close();
                con_.con.Close();
            }
            catch (Exception c)
            {
                MessageBox.Show("Please check information" + c.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            try
            {
                con_.emp.Close();
                con_.emp.Open();
                SqlCommand cmd = new SqlCommand("select * from log_ where username='" + textBox1.Text + "'", con_.emp);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    textBox1.Text = dr.GetString(0);
                    textBox2.Text = dr.GetString(1);
                    comboBox1.Text = dr.GetString(2);
                }
                con_.emp.Close();
                con_.emp.Open();
                SqlCommand cmd1 = new SqlCommand("select * from emp where username='" + textBox1.Text + "'", con_.emp);
                dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    textBox3.Text = dr.GetString(1);
                    textBox4.Text = dr.GetString(2);
                    textBox5.Text = dr.GetValue(3).ToString();
                    textBox6.Text = dr.GetInt32(4).ToString();
                    comboBox2.Text = dr.GetString(5);
                }
                con_.emp.Close();
                con_.emp.Open();
                SqlCommand cmd2 = new SqlCommand("select * from phnum where username='" + textBox1.Text + "'", con_.emp);
                dr = cmd2.ExecuteReader();
                while (dr.Read())
                {
                    listBox1.Items.Add(dr.GetString(1));
                }
                con_.emp.Close();
                con_.con.Open();
                SqlCommand cmd3 = new SqlCommand("select * from work where username='" + textBox1.Text + "'", con_.con);
                dr = cmd3.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetString(1) == "YES") { checkBox1.Checked = true; }
                    if (dr.GetString(2) == "YES") { checkBox2.Checked = true; }
                    if (dr.GetString(3) == "YES") { checkBox3.Checked = true; }
                    if (dr.GetString(4) == "YES") { checkBox4.Checked = true; }
                    if (dr.GetString(5) == "YES") { checkBox5.Checked = true; }
                    if (dr.GetString(6) == "YES") { checkBox6.Checked = true; }
                    if (dr.GetString(7) == "YES") { checkBox7.Checked = true; }
                }
                con_.con.Close();
                con_.con.Open();
                SqlCommand cmd4 = new SqlCommand("select email from emial where username='" + textBox1.Text + "'", con_.con);
                dr = cmd4.ExecuteReader();
                while (dr.Read())
                {
                    listBox2.Items.Add(dr.GetString(0));
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
            con_.con.Close();
        }
        private void up1()
        {
            SqlCommand cmd = new SqlCommand("update log_ set username=@a , pass=@b , auth=@c where username='" + textBox1.Text + "'", con_.emp);
            cmd.Parameters.AddWithValue("@a", textBox1.Text);
            cmd.Parameters.AddWithValue("@b", textBox2.Text);
            cmd.Parameters.AddWithValue("@c", comboBox1.Text);
            cmd.ExecuteNonQuery();
        }

        private void up2()
        {
            SqlCommand cmd1 = new SqlCommand("update emp set username=@a , fname=@b , sname=@c , salary=@d , age=@e , gender=@f where username = '" + textBox1.Text + "'", con_.emp);
            cmd1.Parameters.AddWithValue("@a", textBox1.Text);
            cmd1.Parameters.AddWithValue("@b", textBox3.Text);
            cmd1.Parameters.AddWithValue("@c", textBox4.Text);
            cmd1.Parameters.AddWithValue("@d", textBox5.Text);
            cmd1.Parameters.AddWithValue("@e", textBox6.Text);
            cmd1.Parameters.AddWithValue("@f", comboBox2.Text);
            cmd1.ExecuteNonQuery();
        }
        private void up3()
        {
            string[] week = new string[7];
            for (int i = 0; i < week.Length; i++) { week[i] = "NO"; }
            if (checkBox1.Checked == true) { week[0] = "YES"; }
            if (checkBox2.Checked == true) { week[1] = "YES"; }
            if (checkBox3.Checked == true) { week[2] = "YES"; }
            if (checkBox4.Checked == true) { week[3] = "YES"; }
            if (checkBox5.Checked == true) { week[4] = "YES"; }
            if (checkBox6.Checked == true) { week[5] = "YES"; }
            if (checkBox7.Checked == true) { week[6] = "YES"; }


            SqlCommand cmd2 = new SqlCommand("update work set username=@a , satrday=@b , sunday=@c , monday=@d , tuseday=@e , wensday=@f , thersday=@g , frieday=@h where username='" + textBox1.Text + "'", con_.con);
            cmd2.Parameters.AddWithValue("@a", textBox1.Text);
            cmd2.Parameters.AddWithValue("@b", week[0]);
            cmd2.Parameters.AddWithValue("@c", week[1]);
            cmd2.Parameters.AddWithValue("@d", week[2]);
            cmd2.Parameters.AddWithValue("@e", week[3]);
            cmd2.Parameters.AddWithValue("@f", week[4]);
            cmd2.Parameters.AddWithValue("@g", week[5]);
            cmd2.Parameters.AddWithValue("@h", week[6]);
            cmd2.ExecuteNonQuery();
        }
        private void up4()
        {
            SqlCommand cmd3 = new SqlCommand("update emial set username=@a , email=@b  where username='" + textBox1.Text + "'", con_.con);
            cmd3.Parameters.AddWithValue("@a", textBox1.Text);
            cmd3.Parameters.AddWithValue("@b", textBox8.Text);
            cmd3.ExecuteNonQuery();
        }
        private void up5()
        {
            SqlCommand cmd4 = new SqlCommand("update phnum set username=@a , pnum=@b  where username='" + textBox1.Text + "'", con_.emp);
            cmd4.Parameters.AddWithValue("@a", textBox1.Text);
            cmd4.Parameters.AddWithValue("@b", textBox7.Text);
            cmd4.ExecuteNonQuery();
            MessageBox.Show("update");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            con_.con.Close();
            con_.emp.Close();
            con_.con.Open();
            con_.emp.Open();
            try
            {
                Task t1 = new Task(up1);
                Task t2 = new Task(up2);
                Task t3 = new Task(up3);
                Task t4 = new Task(up4);
                Task t5 = new Task(up5);
                //t1.Start();
                //t2.Start();
                //t3.Start();
                //t4.Start();
                //t5.Start();
            }
            catch (Exception c)
            {
                MessageBox.Show("Please check information");
            }
            con_.con.Close();
            con_.emp.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void admin_Load(object sender, EventArgs e)
        {
            server();
            tabControl1.SelectedIndex = 1;
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            epLocal = new IPEndPoint(IPAddress.Parse("192.168.43.243"), Convert.ToInt32("80"));
            sck.Bind(epLocal);
            epRomote = new IPEndPoint(IPAddress.Parse("192.168.43.243"), Convert.ToInt32("81"));
            sck.Connect(epRomote);
            buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRomote, new AsyncCallback(MessageCallBack), buffer);
        }
        private void MessageCallBack(IAsyncResult aResoult)
        {
            try
            {
                byte[] reciveData = new byte[1500];
                reciveData = (byte[])aResoult.AsyncState;

                ASCIIEncoding aEncoding = new ASCIIEncoding();
                string receveMessge = aEncoding.GetString(reciveData);

                listBox3.Items.Add("You: " + receveMessge.ToString() + "\n");

                buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRomote, new AsyncCallback(MessageCallBack), buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void server()
        {
            try
            {
                con_.emp.Close();
                con_.emp.Open();
                sqlcmd = new SqlCommand("SELECT name FROM sys.servers", con_.emp);
                dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox3.Items.Add(dr.GetString(0));
                }
                con_.emp.Close();
                comboBox3.Text="DESKTOP-8A3O2K0";
                database(comboBox3.Text);
            }
            catch
            {

            }
        }

        private void database(string server)
        {
            try
            {
                comboBox4.Items.Clear();
                if (server == "DESKTOP-8A3O2K0")
                {
                    con_.emp.Close();
                    con_.emp.Open();
                    sqlcmd = new SqlCommand("select name from sys.databases", con_.emp);
                    dr = sqlcmd.ExecuteReader();
                    while (dr.Read())
                    {
                        comboBox4.Items.Add(dr.GetString(0));
                    }
                }
                else if (server == "192.168.43.150")
                {
                    con_.emp.Close();
                    con_.con.Close();
                    con_.con.Open();
                    sqlcmd = new SqlCommand("select name from sys.databases", con_.con);
                    dr = sqlcmd.ExecuteReader();
                    while (dr.Read())
                    {
                        comboBox4.Items.Add(dr.GetString(0));
                    }
                }
                con_.con.Close();
                comboBox4.Text = comboBox4.Items[0].ToString();
                TableView(comboBox4.Text, comboBox3.Text);
            }
            catch
            {

            }
        }

        private void TableView(string db, string server)
        {
            comboBox5.Items.Clear();
            comboBox6.Items.Clear();
            if (server == "DESKTOP-8A3O2K0")
            {
                con_.emp.Close();
                con_.emp.Open();
                sqlcmd = new SqlCommand("use " + db + " select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE='BASE TABLE'", con_.emp);
                dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox5.Items.Add(dr.GetString(0));
                }
                sqlcmd = null;
                dr = null;
                con_.emp.Close();
                con_.emp.Open();
                sqlcmd = new SqlCommand("use " + db + " select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE ='VIEW'", con_.emp);
                dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox6.Items.Add(dr.GetString(0));
                }
                con_.emp.Close();
            }
            else if (server == "192.168.43.150")
            {

                con_.con.Close();
                con_.con.Open();
                sqlcmd = new SqlCommand("use " + db + " select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE ='BASE TABLE' ", con_.con);
                dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox5.Items.Add(dr.GetString(0));
                }
                con_.con.Close();
                con_.con.Open();
                sqlcmd = new SqlCommand("use " + db + " select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_TYPE ='VIEW' ", con_.con);
                dr = sqlcmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox6.Items.Add(dr.GetString(0));
                }
                con_.con.Close();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            database(comboBox3.SelectedItem.ToString());
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableView(comboBox4.SelectedItem.ToString(), comboBox3.Text);

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string qurey = "";
            if (textBox9.SelectedText == "")
            {
                qurey = textBox9.Text;
            }
            else if (textBox9.SelectedText != "") 
            {
                qurey = textBox9.SelectedText;
            }
            try
            {
                tabControl2.SelectedIndex = 0;
                if (comboBox3.Text == "DESKTOP-8A3O2K0")
                {
                    SqlConnection emp = new SqlConnection("Data Source=DESKTOP-8A3O2K0;Initial Catalog=" + comboBox4.Text + ";Integrated Security=True");
                  
                    emp.Open();
                    sqlcmd = new SqlCommand( qurey, emp);
                    sda = new SqlDataAdapter(sqlcmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                    emp.Close();
                }
                else if (comboBox3.Text == "192.168.43.150")
                {
                    SqlConnection con = new SqlConnection("Data Source = 192.168.43.150; Initial Catalog = " + comboBox4.Text + "; Persist Security Info = True; User ID = sa; Password=sa");

                    con.Open();
                    sqlcmd = new SqlCommand(qurey, con);
                    sda = new SqlDataAdapter(sqlcmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                    con.Close();
                }
            }
            catch(SqlException ex)
            {
                tabControl2.SelectedIndex = 1;
                richTextBox1.Text = ex.Message;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {


            ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] sendingMessge = new byte[1500];
            sendingMessge = aEncoding.GetBytes(textBox10.Text);

            sck.Send(sendingMessge);
            listBox3.Items.Add("Me: " + textBox10.Text + "\n");
            textBox10.Text = "";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
