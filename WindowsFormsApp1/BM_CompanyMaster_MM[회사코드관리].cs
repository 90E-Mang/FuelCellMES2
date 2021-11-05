using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class BM_CompanyMaster_MM : Form
    {
        bool addCheck = true;
        public BM_CompanyMaster_MM()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.ResizeRedraw = true;
        }
        public delegate void FormClosed(string str);
        public event FormClosed FormCloseEvent;
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            MainScreen.tabindex = tc.SelectedIndex;

            TabPage TabP = (TabPage)tc.TabPages[tc.SelectedIndex];
            tc.TabPages.Remove(TabP);



            //MessageBox.Show(TabP.Text);
            int index = Common.DICT_REMOVE_INDEX[TabP.Text];
            Common.DICT_REMOVE_INDEX.Remove(TabP.Text);

            //탭페이지를 앞으로 한칸씩땡긴다.
            for (int i = index; i < Common.DICT_REMOVE_INDEX.Count; i++)
            {
                string tempstring = Common.DICT_REMOVE_INDEX.FirstOrDefault(x => x.Value == i + 1).Key;
                int tempint = Common.DICT_REMOVE_INDEX[tempstring];
                Common.DICT_REMOVE_INDEX.Remove(tempstring);
                Common.DICT_REMOVE_INDEX.Add(tempstring, tempint - 1);
            }
        }

        private void btnDoSearch_Click(object sender, EventArgs e)
        {
            CompanyList.Visible = true;
            DataSet CompanySelect = GetData();
            if (addCheck)
            {
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"

                };
                CompanyList.Columns.Add(chkCol);            
                addCheck = false;
            }
            CompanyList.DataSource = CompanySelect.Tables[0];
            CompanyList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CompanyList.Columns[1].ReadOnly = true;
            CompanyList.Columns[2].ReadOnly = true;
            CompanyList.Columns["check"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CompanyList.Refresh();
        }

        private void CompanyList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = CompanyList.SelectedRows[0];
            string strConn = "Data Source=123.248.155.8; Initial Catalog=HIAIRMES;User ID=hiair;Password=@hiair"; ;
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();

            string procName = "CM_CompanyCode_MM_S1";
            SqlCommand cmd = new SqlCommand(procName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@COMP_CD", SqlDbType.VarChar,10);
            param.Value = selectedRow.Cells[1].Value.ToString();
            cmd.Parameters.Add(param);

            SqlParameter param2 = new SqlParameter("@COMP_NM", SqlDbType.VarChar,30);
            param2.Value = selectedRow.Cells[2].Value.ToString();
            cmd.Parameters.Add(param2);

            SqlDataReader mdr = cmd.ExecuteReader();
            while (mdr.Read())
            {
                txtCompany_Code.Text    = (string)mdr["COMP_CD"];
                txtCompany_Name.Text    = (string)mdr["COMP_NM"];
                txtRegister_No.Text     = (string)mdr["REGISTER_NO"];
                txtCorporate_No.Text    = (string)mdr["CORPORATE_NO"];
                txtPresident_Name.Text  = (string)mdr["PRESIDENT_NAME"];
                txtComp_Type.Text       = (string)mdr["COMP_TYPE"];
                txtComp_Industy.Text    = (string)mdr["COMP_INDUSTY"];
                txtCompany_Zip.Text     = (string)mdr["COMP_ZIP"];
                txtCompany_Address.Text = (string)mdr["COMP_ADDR"];
                txtCompany_Tel.Text     = (string)mdr["COMP_TEL"];
                txtCompany_Fax.Text     = (string)mdr["COMP_FAX"];
                txtCompany_Area.Text    = (string)mdr["COMP_AREA_NAME"];
                cboUseFlag.Text         = (string)mdr["USE_YN"];
            }
            mdr.Close();
            conn.Close();
            txtCompany_Code.ReadOnly = true;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCompany_Code.ReadOnly = false;
            txtCompany_Code.Clear();
            txtCompany_Name.Clear();
            txtRegister_No.Clear();
            txtCorporate_No.Clear();
            txtPresident_Name.Clear();
            txtComp_Type.Clear();
            txtComp_Industy.Clear();
            txtCompany_Zip.Clear();
            txtCompany_Address.Clear();
            txtCompany_Tel.Clear();
            txtCompany_Fax.Clear();
            txtCompany_Area.Clear();
            cboUseFlag.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("작성 내용을 저장하시겠습니까?", "데이터 저장", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                string COMP_CD = txtCompany_Code.Text;
                string connectionString = "Data Source=123.248.155.8; Initial Catalog=HIAIRMES;User ID=hiair;Password=@hiair";
                SqlConnection sqlConn = new SqlConnection(connectionString);
                int rowIndex = CompanyList.CurrentRow.Index;
                try
                {
                    if (txtCompany_Code.ReadOnly == false)
                    {
                        if (txtCompany_Code.Text.Length == 0)
                        {
                            MessageBox.Show("회사코드는 필수 입력 항목입니다!!");
                            return;
                        }
                        for (int i = 0; i < CompanyList.Rows.Count; i++)
                        {
                            if (txtCompany_Code.Text == CompanyList.Rows[i].Cells[1].Value.ToString())
                            {
                                MessageBox.Show("회사코드는 중복될 수 없습니다.");
                                return;
                            }
                        }
                        
                        SqlCommand sqlComm = new SqlCommand();
                        sqlComm.Connection = sqlConn;
                        sqlComm.CommandText = "insert into CIS_COMP (COMP_CD,COMP_NM,REGISTER_NO,CORPORATE_NO,PRESIDENT_NAME,COMP_TYPE,COMP_INDUSTY,COMP_ZIP,COMP_ADDR,COMP_TEL,COMP_FAX,COMP_AREA_NAME,USE_YN)" +
                                              " values(@COMP_CD,@COMP_NM,@REGISTER_NO,@CORPORATE_NO,@PRESIDENT_NAME,@COMP_TYPE,@COMP_INDUSTY,@COMP_ZIP,@COMP_ADDR,@COMP_TEL,@COMP_FAX,@COMP_AREA_NAME,@USE_YN)";
                        sqlComm.Parameters.AddWithValue("@COMP_CD",         txtCompany_Code.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_NM",         txtCompany_Name.Text);
                        sqlComm.Parameters.AddWithValue("@REGISTER_NO",     txtRegister_No.Text);
                        sqlComm.Parameters.AddWithValue("@CORPORATE_NO",    txtCorporate_No.Text);
                        sqlComm.Parameters.AddWithValue("@PRESIDENT_NAME",  txtPresident_Name.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_TYPE",       txtComp_Type.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_INDUSTY",    txtComp_Industy.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_ZIP",        txtCompany_Zip.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_ADDR",       txtCompany_Address.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_TEL",        txtCompany_Tel.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_FAX",        txtCompany_Fax.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_AREA_NAME",  txtCompany_Area.Text);
                        sqlComm.Parameters.AddWithValue("@USE_YN",          cboUseFlag.Text);
                        sqlConn.Open();
                        sqlComm.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                    else
                    {
                        SqlCommand sqlComm = new SqlCommand();
                        sqlComm.Connection = sqlConn;
                        sqlComm.CommandText = "update CIS_COMP set COMP_NM=@COMP_NM, REGISTER_NO=@REGISTER_NO, CORPORATE_NO=@CORPORATE_NO, PRESIDENT_NAME=@PRESIDENT_NAME, COMP_TYPE=@COMP_TYPE, COMP_INDUSTY=@COMP_INDUSTY," +
                                              " COMP_ZIP=@COMP_ZIP, COMP_ADDR=@COMP_ADDR, COMP_TEL=@COMP_TEL, COMP_AREA_NAME=@COMP_AREA_NAME, USE_YN=@USE_YN" +
                                              " where COMP_CD=@COMP_CD";
                        sqlComm.Parameters.AddWithValue("@COMP_CD",         txtCompany_Code.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_NM",         txtCompany_Name.Text);
                        sqlComm.Parameters.AddWithValue("@REGISTER_NO",     txtRegister_No.Text);
                        sqlComm.Parameters.AddWithValue("@CORPORATE_NO",    txtCorporate_No.Text);
                        sqlComm.Parameters.AddWithValue("@PRESIDENT_NAME",  txtPresident_Name.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_TYPE",       txtComp_Type.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_INDUSTY",    txtComp_Industy.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_ZIP",        txtCompany_Zip.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_ADDR",       txtCompany_Address.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_TEL",        txtCompany_Tel.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_FAX",        txtCompany_Fax.Text);
                        sqlComm.Parameters.AddWithValue("@COMP_AREA_NAME",  txtCompany_Area.Text);
                        sqlComm.Parameters.AddWithValue("@USE_YN",          cboUseFlag.Text);
                        sqlConn.Open();
                        sqlComm.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                    MessageBox.Show("데이터를 저장했습니다.");
                    txtCompany_Code.ReadOnly = true;;
                    btnDoSearch_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (result == DialogResult.No)
            {
                return;
            }                      
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("선택된 데이터를 삭제하시겠습니까?", "데이터 삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                try
                {
                    string connectionString = "Data Source=123.248.155.8; Initial Catalog=HIAIRMES;User ID=hiair;Password=@hiair";
                    SqlConnection sqlConn = new SqlConnection(connectionString);
                    int CheckCount = 0;

                    CompanyList.EndEdit();
                    for (int i = 0; i < CompanyList.Rows.Count; i++)
                    {

                        bool isChecked = Convert.ToBoolean(CompanyList.Rows[i].Cells[0].Value);
                        if (isChecked)
                        {
                            SqlCommand sqlComm = new SqlCommand();
                            sqlComm.Connection = sqlConn;
                            sqlComm.CommandText = "delete from CIS_COMP where COMP_CD=@COMP_CD";
                            sqlComm.Parameters.AddWithValue("@COMP_CD", CompanyList.Rows[i].Cells[1].Value.ToString());
                            sqlConn.Open();
                            sqlComm.ExecuteNonQuery();
                            sqlConn.Close();
                            CheckCount++;
                        }
                    }
                    if (CheckCount > 0)
                    {
                        MessageBox.Show("데이터 삭제를 완료했습니다.");
                    }
                    else
                    {
                        MessageBox.Show("삭제할 데이터를 선택 후 삭제 버튼을 눌러주세요.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNew_Click(sender, e);
                btnDoSearch_Click(sender, e);
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            FormCloseEvent("회사 코드 관리");
        }
        private DataSet GetData()
        {
            string findCompany = txtSearchCompany.Text;
            DataSet ds = new DataSet();
            try
            {
                string strConn = "Data Source=123.248.155.8; Initial Catalog=HIAIRMES;User ID=hiair;Password=@hiair"; 
                SqlConnection conn = new SqlConnection(strConn);
                if (findCompany == "" || findCompany.Length == 0 || findCompany == " ")
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT COMP_CD AS '회사코드', COMP_NM AS '회사명' FROM CIS_COMP", conn);
                    adapter.Fill(ds);

                }
                else
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT COMP_CD AS '회사코드', COMP_NM AS '회사명' FROM CIS_COMP WHERE COMP_NM LIKE '%" + findCompany + "%'", conn);
                    adapter.Fill(ds);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ds;
        }

        private void CM_CompanyCode_MM_Load(object sender, EventArgs e)
        {
            btnDoSearch_Click(sender, e);
            cboUseFlag.FlatStyle = FlatStyle.Popup;
            cboUseFlag.BackColor = Color.Ivory;
        }
    }
}
