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
    public partial class BM_CustomerMaster_MM : Form
    {
        bool addCheck = true;
        public BM_CustomerMaster_MM()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.ResizeRedraw = true;
            StartDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), 1);
        }
        public delegate void FormClosed(string str);
        public event FormClosed FormCloseEvent;
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.FormCloseEvent("거래처 마스터");
            this.Close();

        }
        private void CM_CustomerMaster_MM_Load(object sender, EventArgs e)
        {
            CombMaster();

            cboSearchCust_Group.FlatStyle = FlatStyle.Popup;
            cboSearchCust_Group.BackColor = Color.Ivory;
            cboSearchCust_Group.SelectedIndex = 0;

            cboSearchCustType1.FlatStyle = FlatStyle.Popup;
            cboSearchCustType1.BackColor = Color.Ivory;
            cboSearchCustType1.SelectedIndex = 0;

            cboSearchCustType2.FlatStyle = FlatStyle.Popup;
            cboSearchCustType2.BackColor = Color.Ivory;
            cboSearchCustType2.SelectedIndex = 0;

            cboCust_Group.FlatStyle = FlatStyle.Popup;
            cboCust_Group.BackColor = Color.Ivory;

            cboCust_Type1.FlatStyle = FlatStyle.Popup;
            cboCust_Type1.BackColor = Color.Ivory;

            cboCust_Type2.FlatStyle = FlatStyle.Popup;
            cboCust_Type2.BackColor = Color.Ivory;

            cboCustBank.FlatStyle = FlatStyle.Popup;
            cboCustBank.BackColor = Color.Ivory;
            cboCustBank.Text = null;

            cboUseFlag.FlatStyle = FlatStyle.Popup;
            cboUseFlag.BackColor = Color.Ivory;

            DB.conn = new SqlConnection(DB.connectionString);
            DB.conn.ConnectionString = DB.connectionString;
            DB.conn.Open();
            //트랜잭션 시작
            DB.transaction = DB.conn.BeginTransaction();
            DB.sqlcmd = new SqlCommand("CM_CustomerMaster_MM_S3", DB.conn, DB.transaction);
            DB.sqlcmd.CommandType = CommandType.StoredProcedure;

            DB.sqlcmd.ExecuteNonQuery();
            DB.adapter = new SqlDataAdapter(DB.sqlcmd);
            DataSet ds = new DataSet();
            DB.adapter.Fill(ds, "CM_CustomerMaster_MM_S3");
            CustList.DataSource = ds;
            CustList.DataMember = "CM_CustomerMaster_MM_S3";
            DB.transaction.Commit();
            DB.adapter.Dispose();
            DB.sqlcmd.Dispose();
            DB.conn.Dispose();
            DB.conn.Close();
        }

        private void btnDoSearch_Click(object sender, EventArgs e)
        {
            CustList.Visible = true;
            DataSet CustSelect = GetData();
            if (addCheck)
            {
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"

                };
                CustList.Columns.Add(chkCol);
                addCheck = false;
            }
            CustList.DataSource = CustSelect.Tables[0];
            CustList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CustList.Columns[1].ReadOnly = true;
            CustList.Columns[2].ReadOnly = true;
            CustList.Columns["check"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            CustList.Refresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("선택된 데이터를 삭제하시겠습니까?", "데이터 삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                try
                {
                    string connectionString = "Data Source=192.168.0.163; Initial Catalog=HIAIRMES;User ID=hiair;Password=@hiair";
                    SqlConnection sqlConn = new SqlConnection(connectionString);
                    int CheckCount = 0;

                    CustList.EndEdit();
                    for (int i = 0; i < CustList.Rows.Count; i++)
                    {

                        bool isChecked = Convert.ToBoolean(CustList.Rows[i].Cells[0].Value);
                        if (isChecked)
                        {
                            SqlCommand sqlComm = new SqlCommand();
                            sqlComm.Connection = sqlConn;
                            sqlComm.CommandText = "delete from TB_CustMaster where CUST_CODE=@CUST_CODE";
                            sqlComm.Parameters.AddWithValue("@CUST_CODE", CustList.Rows[i].Cells[1].Value.ToString());
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
                txtCust_Code.ReadOnly = true;

            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCust_Code.ReadOnly = false;
            txtCust_Code.Clear();
            txtCust_Name.Clear();
            cboCust_Group.Text = null;
            cboCust_Type1.Text = null;
            cboCust_Type2.Text = null;
            txtCust_ABBR.Clear();
            txtCust_EngName.Clear();
            txtRegister_No.Clear();
            txtCorporate_No.Clear();
            txtPresident_Name.Clear();
            txtEmail.Clear();
            txtComp_Type.Clear();
            txtComp_Industy.Clear();
            txtCust_Tel.Clear();
            txtCust_Fax.Clear();
            txtCust_Zip.Clear();
            txtCust_Area.Clear();
            txtCust_Address.Clear();
            cboCustBank.Text = null;
            txtBankAccount.Clear();
            txtAccountOwner.Clear();
            cboUseFlag.Text = null;
            StartDate.Refresh();
            EndDate.Refresh();
            txtEndReason.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("작성 내용을 저장하시겠습니까?", "데이터 저장", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                int result_value = 0;
                try
                {
                    string strConn = "Data Source=192.168.0.163; Initial Catalog=HIAIRMES;User ID=hiair;Password=@hiair"; ;
                    SqlConnection conn = new SqlConnection(strConn);
                    if (txtCust_Code.ReadOnly == false)
                    {
                        if (txtCust_Code.Text.Length == 0)
                        {
                            MessageBox.Show("거래처코드는 필수 입력 항목입니다!!");
                            return;
                        }
                        for (int i = 0; i < CustList.Rows.Count; i++)
                        {
                            if (txtCust_Code.Text == CustList.Rows[i].Cells[1].Value.ToString())
                            {
                                MessageBox.Show("거래처코드는 중복될 수 없습니다.");
                                return;
                            }
                        }
                       
                        SqlCommand CustInsert = new SqlCommand("CM_CustomerMaster_MM_I1", conn);
                        CustInsert.CommandType = CommandType.StoredProcedure;

                        CustInsert.Parameters.AddWithValue("@CUST_CODE",txtCust_Code.Text);
                        CustInsert.Parameters.AddWithValue("@CUST_NAME",txtCust_Name.Text);
                        CustInsert.Parameters.AddWithValue("@CUST_GROUP",(cboCust_Group.SelectedValue == null) ? "" : cboCust_Group.SelectedValue.ToString());
                        CustInsert.Parameters.AddWithValue("@CUST_TYPE1",(cboCust_Type1.SelectedValue == null) ? "" : cboCust_Type1.SelectedValue.ToString());
                        CustInsert.Parameters.AddWithValue("@CUST_TYPE2",(cboCust_Type2.SelectedValue == null) ? "" : cboCust_Type2.SelectedValue.ToString());
                        CustInsert.Parameters.AddWithValue("@CUST_ABBR",txtCust_ABBR.Text);
                        CustInsert.Parameters.AddWithValue("@CUST_NAME_ENG",txtCust_EngName.Text);
                        CustInsert.Parameters.AddWithValue("@REGISTER_NO",txtRegister_No.Text);
                        CustInsert.Parameters.AddWithValue("@CORPORATE_NO",txtCorporate_No.Text);
                        CustInsert.Parameters.AddWithValue("@PRESIDENT_NAME",txtPresident_Name.Text);
                        CustInsert.Parameters.AddWithValue("@CUST_EMAIL",txtEmail.Text);
                        CustInsert.Parameters.AddWithValue("@BIZ_TYPE",txtComp_Type.Text);
                        CustInsert.Parameters.AddWithValue("@BIZ_INDUSTY",txtComp_Industy.Text);
                        CustInsert.Parameters.AddWithValue("@CUST_TEL",txtCust_Tel.Text);
                        CustInsert.Parameters.AddWithValue("@CUST_FAX",txtCust_Fax.Text);
                        CustInsert.Parameters.AddWithValue("@CUST_ZIP",txtCust_Zip.Text);
                        CustInsert.Parameters.AddWithValue("@CUST_AREA_NAME",txtCust_Area.Text);
                        CustInsert.Parameters.AddWithValue("@CUST_ADDR",txtCust_Address.Text);
                        CustInsert.Parameters.AddWithValue("@BANK_NAME", (cboCustBank.SelectedValue == null) ? "" : cboCustBank.SelectedValue.ToString());
                        CustInsert.Parameters.AddWithValue("@BANK_NO",txtBankAccount.Text);
                        CustInsert.Parameters.AddWithValue("@ACCOUNT_OWNER",txtAccountOwner.Text);
                        CustInsert.Parameters.AddWithValue("@BIZ_STATUS", (cboUseFlag.SelectedValue == null) ? "" : cboUseFlag.SelectedValue.ToString());
                        CustInsert.Parameters.AddWithValue("@CUST_END_CAUSE", txtEndReason.Text);

                        CustInsert.Parameters.Add("@ERROR_CODE", SqlDbType.Int);
                        CustInsert.Parameters["@ERROR_CODE"].Direction = ParameterDirection.Output;

                        conn.Open();
                        CustInsert.ExecuteNonQuery();


                        result_value = Convert.ToInt32(CustInsert.Parameters["@ERROR_CODE"].Value);
                        CustInsert.Dispose();
                    }
                    else
                    {               
                        SqlCommand CustUpdate = new SqlCommand("CM_CustomerMaster_MM_U1", conn);
                        CustUpdate.CommandType = CommandType.StoredProcedure;

                        CustUpdate.Parameters.AddWithValue("@CUST_CODE", txtCust_Code.Text);
                        CustUpdate.Parameters.AddWithValue("@CUST_NAME",txtCust_Name.Text);
                        CustUpdate.Parameters.AddWithValue("@CUST_GROUP",cboCust_Group.SelectedValue.ToString());
                        CustUpdate.Parameters.AddWithValue("@CUST_TYPE1",cboCust_Type1.SelectedValue.ToString());
                        CustUpdate.Parameters.AddWithValue("@CUST_TYPE2",cboCust_Type2.SelectedValue.ToString());
                        CustUpdate.Parameters.AddWithValue("@CUST_ABBR",txtCust_ABBR.Text);
                        CustUpdate.Parameters.AddWithValue("@CUST_NAME_ENG",txtCust_EngName.Text);
                        CustUpdate.Parameters.AddWithValue("@REGISTER_NO",txtRegister_No.Text);
                        CustUpdate.Parameters.AddWithValue("@CORPORATE_NO",txtCorporate_No.Text);
                        CustUpdate.Parameters.AddWithValue("@PRESIDENT_NAME",txtPresident_Name.Text);
                        CustUpdate.Parameters.AddWithValue("@CUST_EMAIL",txtEmail.Text);
                        CustUpdate.Parameters.AddWithValue("@BIZ_TYPE",txtComp_Type.Text);
                        CustUpdate.Parameters.AddWithValue("@BIZ_INDUSTY",txtComp_Industy.Text);
                        CustUpdate.Parameters.AddWithValue("@CUST_TEL",txtCust_Tel.Text);
                        CustUpdate.Parameters.AddWithValue("@CUST_FAX",txtCust_Fax.Text);
                        CustUpdate.Parameters.AddWithValue("@CUST_ZIP",txtCust_Zip.Text);
                        CustUpdate.Parameters.AddWithValue("@CUST_AREA_NAME",txtCust_Area.Text);
                        CustUpdate.Parameters.AddWithValue("@CUST_ADDR",txtCust_Address.Text);
                        CustUpdate.Parameters.AddWithValue("@BANK_NAME",(cboCustBank.SelectedValue == null) ? "" : cboCustBank.SelectedValue.ToString());
                        CustUpdate.Parameters.AddWithValue("@BANK_NO",txtBankAccount.Text);
                        CustUpdate.Parameters.AddWithValue("@ACCOUNT_OWNER",txtAccountOwner.Text);
                        CustUpdate.Parameters.AddWithValue("@BIZ_STATUS",(cboUseFlag.SelectedValue == null) ? "" : cboUseFlag.SelectedValue.ToString());
                        CustUpdate.Parameters.AddWithValue("@CUST_END_CAUSE",txtEndReason.Text);
                       
                        conn.Open();
                        CustUpdate.ExecuteNonQuery();
                        CustUpdate.Dispose();
                    }
                    if (result_value == 1)
                    {
                        MessageBox.Show("거래처코드는 중복될 수 없습니다.");
                        MessageBox.Show("데이터 저장에 실패했습니다.");                  
                        return;
                    }
                    else
                    {
                        MessageBox.Show("데이터를 저장했습니다.");
                        txtCust_Code.ReadOnly = true;
                        conn.Dispose();
                        conn.Close();
                        btnDoSearch_Click(sender, e);
                    }
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

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataSet GetData()
        {
            DataSet ds = new DataSet();
            try
            {
                string strConn = "Data Source=192.168.0.163; Initial Catalog=HIAIRMES;User ID=hiair;Password=@hiair"; ;
                SqlConnection conn = new SqlConnection(strConn);

                conn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("CM_CustomerMaster_MM_S1", conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.AddWithValue("@CUST_GROUP", cboSearchCust_Group.SelectedValue.ToString());
                adapter.SelectCommand.Parameters.AddWithValue("@CUST_NAME", txtSearchCust_Name.Text);
                adapter.SelectCommand.Parameters.AddWithValue("@CUST_TYPE1", cboSearchCustType1.SelectedValue.ToString());
                adapter.SelectCommand.Parameters.AddWithValue("@CUST_TYPE2", cboSearchCustType2.SelectedValue.ToString());

                adapter.Fill(ds);
                adapter.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ds;
        }

        private void CustList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cboCust_Group.Text = null;
                cboCust_Type1.Text = null;
                cboCust_Type2.Text = null;
                cboCustBank.Text = null;
                cboUseFlag.Text = null;

                DataGridViewRow selectedRow = CustList.Rows[e.RowIndex];
                txtCust_Code.ReadOnly = true;

                string strConn = "Data Source=192.168.0.163; Initial Catalog=HIAIRMES;User ID=hiair;Password=@hiair"; ;
                SqlConnection conn = new SqlConnection(strConn);

                conn.Open();

                SqlCommand cmd = new SqlCommand("CM_CustomerMaster_MM_S2", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@CUST_CODE", SqlDbType.VarChar, 20);
                param.Value = selectedRow.Cells[1].Value.ToString();
                cmd.Parameters.Add(param);

                param = new SqlParameter("@CUST_NAME", SqlDbType.VarChar, 100);
                param.Value = selectedRow.Cells[2].Value.ToString();
                cmd.Parameters.Add(param);

                SqlDataReader mdr = cmd.ExecuteReader();
                while (mdr.Read())
                {
                    txtCust_Code.Text = mdr["CUST_CODE"].ToString();
                    txtCust_Name.Text = mdr["CUST_NAME"].ToString();
                    cboCust_Group.Text = mdr["CUST_GROUP"].ToString();
                    cboCust_Type1.Text = mdr["CUST_TYPE1"].ToString();
                    cboCust_Type2.Text = mdr["CUST_TYPE2"].ToString();
                    txtCust_ABBR.Text = (mdr["CUST_ABBR"] == null) ? string.Empty : mdr["CUST_ABBR"].ToString();
                    txtCust_EngName.Text = (mdr["CUST_NAME_ENG"] == null) ? string.Empty : mdr["CUST_NAME_ENG"].ToString();
                    txtRegister_No.Text = (mdr["REGISTER_NO"] == null) ? string.Empty : mdr["REGISTER_NO"].ToString();
                    txtCorporate_No.Text = (mdr["CORPORATE_NO"] == null) ? string.Empty : mdr["CORPORATE_NO"].ToString(); ;
                    txtPresident_Name.Text = (mdr["PRESIDENT_NAME"] == null) ? string.Empty : mdr["PRESIDENT_NAME"].ToString();
                    txtEmail.Text = (mdr["CUST_EMAIL"] == null) ? string.Empty : mdr["CUST_EMAIL"].ToString();
                    txtComp_Type.Text = (mdr["BIZ_TYPE"] == null) ? string.Empty : mdr["BIZ_TYPE"].ToString();
                    txtComp_Industy.Text = (mdr["BIZ_INDUSTY"] == null) ? string.Empty : mdr["BIZ_INDUSTY"].ToString();
                    txtCust_Tel.Text = (mdr["CUST_TEL"] == null) ? string.Empty : mdr["CUST_TEL"].ToString();
                    txtCust_Fax.Text = (mdr["CUST_FAX"] == null) ? string.Empty : mdr["CUST_FAX"].ToString();
                    txtCust_Zip.Text = (mdr["CUST_ZIP"] == null) ? string.Empty : mdr["CUST_ZIP"].ToString();
                    txtCust_Address.Text = (mdr["CUST_ADDR"] == null) ? string.Empty : mdr["CUST_ADDR"].ToString();
                    cboCustBank.SelectedItem = (mdr["BANK_NAME"] == null) ? string.Empty : mdr["BANK_NAME"].ToString();
                    txtBankAccount.Text = (mdr["BANK_NO"] == null) ? string.Empty : mdr["BANK_NO"].ToString();
                    txtAccountOwner.Text = (mdr["ACCOUNT_OWNER"] == null) ? string.Empty : mdr["ACCOUNT_OWNER"].ToString();
                    cboUseFlag.SelectedItem = (mdr["BIZ_STATUS"] == null) ? string.Empty : mdr["BIZ_STATUS"].ToString();
                    txtEndReason.Text = (mdr["CUST_END_CAUSE"] == null) ? string.Empty : mdr["CUST_END_CAUSE"].ToString();
                }
                mdr.Close();
                conn.Close();
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
        private void CombMaster()
        {
            try
            {
                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter COMPAdapter = new SqlDataAdapter();
                DataSet COMPdt = new DataSet();
                COMPAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='CUST_GROUP' ORDER BY SORT_NO";
                COMPAdapter.SelectCommand = DB.sqlcmd;
                COMPAdapter.SelectCommand.ExecuteNonQuery();
                COMPAdapter.Fill(COMPdt);

                cboSearchCust_Group.DataSource = COMPdt.Tables[0];
                cboSearchCust_Group.DisplayMember = "LWRCODENAME";
                cboSearchCust_Group.ValueMember = "LWRCODE";

                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter CustAdapter = new SqlDataAdapter();
                DataSet CUSTdt = new DataSet();
                CustAdapter.TableMappings.Add("Table", "GROUPNAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='CUST_GROUP' ORDER BY SORT_NO";
                CustAdapter.SelectCommand = DB.sqlcmd;
                CustAdapter.SelectCommand.ExecuteNonQuery();
                CustAdapter.Fill(CUSTdt);

                cboCust_Group.DataSource = CUSTdt.Tables[0];
                cboCust_Group.DisplayMember = "LWRCODENAME";
                cboCust_Group.ValueMember = "LWRCODE";

                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter INOUTAdapter = new SqlDataAdapter();
                DataSet INOUTdt = new DataSet();
                INOUTAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='CUST_TYPE1' ORDER BY SORT_NO";
                INOUTAdapter.SelectCommand = DB.sqlcmd;
                INOUTAdapter.SelectCommand.ExecuteNonQuery();
                INOUTAdapter.Fill(INOUTdt);

                cboSearchCustType1.DataSource = INOUTdt.Tables[0];
                cboSearchCustType1.DisplayMember = "LWRCODENAME";
                cboSearchCustType1.ValueMember = "LWRCODE";

                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter TYPE1Adapter = new SqlDataAdapter();                
                DataSet TYPE1dt = new DataSet();
                TYPE1Adapter.TableMappings.Add("Table2", "TYPE1NAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='CUST_TYPE1' ORDER BY SORT_NO";
                TYPE1Adapter.SelectCommand = DB.sqlcmd;
                TYPE1Adapter.SelectCommand.ExecuteNonQuery();
                TYPE1Adapter.Fill(TYPE1dt);

                cboCust_Type1.DataSource = TYPE1dt.Tables[0];
                cboCust_Type1.DisplayMember = "LWRCODENAME";
                cboCust_Type1.ValueMember = "LWRCODE";

                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter INOUTTYPEAdapter = new SqlDataAdapter();
                DataSet INOUT_Typedt = new DataSet();
                INOUTTYPEAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='CUST_TYPE2' ORDER BY SORT_NO";
                INOUTTYPEAdapter.SelectCommand = DB.sqlcmd;
                INOUTTYPEAdapter.SelectCommand.ExecuteNonQuery();
                INOUTTYPEAdapter.Fill(INOUT_Typedt);

                cboSearchCustType2.DataSource = INOUT_Typedt.Tables[0];
                cboSearchCustType2.DisplayMember = "LWRCODENAME";
                cboSearchCustType2.ValueMember = "LWRCODE";

                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter TYPE2Adapter = new SqlDataAdapter();
                DataSet TYPE2dt = new DataSet();
                TYPE2Adapter.TableMappings.Add("Table2", "TYPE2NAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='CUST_TYPE2' ORDER BY SORT_NO";
                TYPE2Adapter.SelectCommand = DB.sqlcmd;
                TYPE2Adapter.SelectCommand.ExecuteNonQuery();
                TYPE2Adapter.Fill(TYPE2dt);

                cboCust_Type2.DataSource = TYPE2dt.Tables[0];
                cboCust_Type2.DisplayMember = "LWRCODENAME";
                cboCust_Type2.ValueMember = "LWRCODE";

                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter BANKAdapter = new SqlDataAdapter();
                DataSet BANKdt = new DataSet();
                BANKAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='BANK_CODE' ORDER BY SORT_NO";
                BANKAdapter.SelectCommand = DB.sqlcmd;
                BANKAdapter.SelectCommand.ExecuteNonQuery();
                BANKAdapter.Fill(BANKdt);

                cboCustBank.DataSource = BANKdt.Tables[0];
                cboCustBank.DisplayMember = "LWRCODENAME";
                cboCustBank.ValueMember = "LWRCODE";

                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter BIZSTATSAdapter = new SqlDataAdapter();
                DataSet BIZSTATSdt = new DataSet();
                BIZSTATSAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='BIZ_STATUS' ORDER BY SORT_NO";
                BIZSTATSAdapter.SelectCommand = DB.sqlcmd;
                BIZSTATSAdapter.SelectCommand.ExecuteNonQuery();
                BIZSTATSAdapter.Fill(BIZSTATSdt);

                cboUseFlag.DataSource = BIZSTATSdt.Tables[0];
                cboUseFlag.DisplayMember = "LWRCODENAME";
                cboUseFlag.ValueMember = "LWRCODE";

                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
            }
        }
    }
}
