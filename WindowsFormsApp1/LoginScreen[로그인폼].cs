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

namespace WindowsFormsApp1
{
    public partial class LoginScreen : Form
    {
        
        public LoginScreen()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

        }
        private void Login()
        {
            bool login = false;                     // 로그인 여부 확인
            string login_id = textBox1.Text;        // 로그인 ID
            string login_password = textBox2.Text;  // 로그인 PASSWORD

            // 로그인 할 id 를 검색
            // TB_USER --> sql상에서 테이블 이름
            string sql = "SELECT * FROM TB_USER WHERE USER_ID=\'" + login_id + "\' ";
            
            //DB에 연결
            try
            {
                DB.conn = new SqlConnection(DB.connectionString);
                DB.sqlcmd = new SqlCommand(sql, DB.conn);
                DB.conn.Open();
                DB.sqlDR = DB.sqlcmd.ExecuteReader();
                while (DB.sqlDR.Read())
                {
                    // 검색된 아이디와 비밀번호가 일치한 경우 로그인된 것으로 봄
                    //sql상에 컬럼이름 UserID, UserPW
                    if (login_id == DB.sqlDR["USER_ID"].ToString() && login_password == DB.sqlDR["USER_PW"].ToString())
                    {
                        login = true;
                        MainScreen.LoginID = DB.sqlDR["USER_NAME"].ToString();
                        BM_ItemMaster_MM.LoginID = MainScreen.LoginID;
                        BM_CommCode_MM.LoginID = MainScreen.LoginID;

                        MTRL_Request.LoginID = MainScreen.LoginID;
                        PW_Purchase_Warehousing.LoginID = DB.sqlDR["USER_ID"].ToString();
                        MM_STOCKOUT_ITEM_POPUP.LoginID = DB.sqlDR["USER_ID"].ToString();
                        STOCKOUT_REQ.LoginID = DB.sqlDR["USER_ID"].ToString();
                        ITEM_POPUP_REQUEST.LoginID = MainScreen.LoginID;
                        MM_MTRL_Order_MM.LoginID = MainScreen.LoginID;
                        MM_MTRL_Order_POPUP.LoginID = MainScreen.LoginID;
                        MM_Stock_MM_POPUP.LoginID = MainScreen.LoginID;
                        MM_STOCKOUT_MM.LoginID = MainScreen.LoginID;
                        MM_STOCKOUT_POPUP.LoginID = MainScreen.LoginID;
                    }
                }
                DB.sqlcmd.Dispose();
                DB.sqlDR.Close();
                DB.conn.Dispose();
                DB.conn.Close();

                if (login)
                {
                    //this.LoginID = this.textBox1.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Visible = false;
                    MainScreen aMainScreen = new MainScreen();
                    aMainScreen.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("아이디/암호를 확인해 주세요.");
                }
                DB.sqlDR.Close();
                DB.conn.Dispose();
                DB.conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DB.sqlDR.Close();
                DB.conn.Dispose();
                DB.conn.Close();
            }

        }

        //로그인 로그인 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        //로그인 종료버튼
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("종료하시겠습니까?", "종료" , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(answer == DialogResult.Yes)
            {
                Close();
                Application.Exit();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
                button1.Select();
            }
        }
        //public string LoginID
        //{
        //    get;
        //    private set;
        //}
    }
}
