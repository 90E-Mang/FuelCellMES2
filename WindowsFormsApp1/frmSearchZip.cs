//using System;
//using System.Windows.Forms;

//using System.Security.Permissions; // 브라우저 컨트롤용

//namespace WindowsFormsApp1
//{
//    // 웹브라우저컨트롤을 위해서..
//    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
//    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
//    public partial class frmSearchZip : Form
//    {
//        /// <summary>
//        /// 반환할 우편코드와 주소
//        /// </summary>
//        public string gstrZipCode = "";
//        public string gstrAddress1 = "";

//        public frmSearchZip()
//        {
//            InitializeComponent();

//            wb.Navigate("http://요기에 웹페이지가 있는 주소를 넣음");
//            wb.ObjectForScripting = this; // 제일 중요
//        }
//        /// <summary>
//        /// 해당 브라우저에 있는 javascript 함수 명 호출
//        /// </summary>
//        /// <param name="sZipCode"></param>
//        /// <param name="sAddress1"></param>
//        public void CallForm(object sZipCode, object sAddress1)
//        {
//            try
//            {
//                gstrZipCode = (string)sZipCode;
//                gstrAddress1 = (string)sAddress1;
//                this.Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }
//        private void button1_Click(object sender, EventArgs e)
//        {
//            frmSearchZip frm = new frmSearchZip();
//            frm.ShowDialog();

//            // 창이 닫히면 반환값을 반환한다.
//            if (frm.gstrZipCode != "")
//            {
//                txtZipCd.Text = frm.gstrZipCode;
//                txtAddress1.Text = frm.gstrAddress1;
//            }

//            frm = null;
//        }

//    } // end class
//} // end namespace
