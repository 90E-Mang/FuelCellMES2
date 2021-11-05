using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WindowsFormsApp1
{
    class DB
    {
        public static SqlConnection conn = new SqlConnection();
        public static SqlTransaction transaction = null;
        public static SqlCommand sqlcmd = new SqlCommand();
        public static SqlDataReader sqlDR = null;
        public static SqlDataAdapter adapter = new SqlDataAdapter();
        public static string connectionString = "Data Source = 123.248.155.8; Initial Catalog = HIAIRMES; User ID = hiair; Password=@hiair";
       
        static void DBconn(string[] args)
        {
            // Sql 연결정보(서버:127.0.0.1, 포트:3535, 아이디:sa, 비밀번호 : password, db : member)
            // Sql 새연결정보 생성
            conn = new SqlConnection(connectionString);
            conn.Open();

            sqlcmd = new SqlCommand();
            sqlcmd.Connection = conn;
            //sqlComm.CommandText = "insert into tbl_member (id,name,addr) values (@param1,@param2,@param3)";
            //sqlComm.CommandText = "update tbl_member set addr=@param3 where id=@param1 and name=@param2";
            //sqlComm.CommandText = "delete tbl_member where id=@param1 and name=@param2 and addr=@param3";
            //sqlComm.Parameters.AddWithValue("@param1", "abc");
            //sqlComm.Parameters.AddWithValue("@param2", "홍길동");
            //sqlComm.Parameters.AddWithValue("@param3", "서울");
            sqlcmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }

    }
}
