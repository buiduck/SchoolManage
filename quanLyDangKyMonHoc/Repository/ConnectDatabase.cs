using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace quanLyDangKyMonHoc.Repository
{
    internal class ConnectDatabase
    {
        public static SqlConnection conn { get; set; }
        public static SqlConnection GetConnection()
        {
            string connectString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;
            conn = new SqlConnection(connectString);
            return conn;
        }
    }
}
