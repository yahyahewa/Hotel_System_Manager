using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace HotelProject
{
    class con_
    {
        admin opo= new admin();
        public static SqlConnection con = new SqlConnection("Data Source=192.168.43.150;Initial Catalog=hotel;Persist Security Info=True;User ID=sa;Password=sa");
        public static SqlConnection emp = new SqlConnection("Data Source=DESKTOP-8A3O2K0;Initial Catalog=hotelEmp;Integrated Security=True");
        public string vsb="";
        public static string user="";
    }
}
