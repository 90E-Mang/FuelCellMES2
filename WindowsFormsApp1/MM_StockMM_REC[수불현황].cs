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
    public partial class MM_StockMM_REC : Form
    {
        public static string GetItemCode;
        public static string GetItemName;
        public static string GetCustCode;
        public static string GetCustName;
        public MM_StockMM_REC()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            StartDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), 1);
            EndDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), int.Parse(DateTime.Now.ToString("dd")));

        }
        public delegate void FormClosed(string str);
        public event FormClosed FormCloseEvent;


        private void MM_StockMM_REC_Load(object sender, EventArgs e)
        {          
            CombMaster();
            
            cboCompanyCode.FlatStyle = FlatStyle.Popup;
            cboCompanyCode.BackColor = Color.Ivory;
            

            cboPlantCode.FlatStyle = FlatStyle.Popup;
            cboPlantCode.BackColor = Color.Ivory;
            cboPlantCode.Enabled = false;

            cboInout_Group.FlatStyle = FlatStyle.Popup;
            cboInout_Group.BackColor = Color.Ivory;

            cboInOut_Type.FlatStyle = FlatStyle.Popup;
            cboInOut_Type.BackColor = Color.Ivory;

            cboInout_Reason.FlatStyle = FlatStyle.Popup;
            cboInout_Reason.BackColor = Color.Ivory;

            DB.conn.Close();
            dataGridView1.Columns.Clear();

            DB.conn = new SqlConnection(DB.connectionString);
            DB.conn.ConnectionString = DB.connectionString;
            DB.conn.Open();
            //트랜잭션 시작
            DB.transaction = DB.conn.BeginTransaction();
            DB.sqlcmd = new SqlCommand("MM_StockMM_REC_S1", DB.conn, DB.transaction);
            DB.sqlcmd.CommandType = CommandType.StoredProcedure;

            DB.sqlcmd.Parameters.AddWithValue("@PLANTCODE","");

            DB.sqlcmd.ExecuteNonQuery();
            DB.adapter = new SqlDataAdapter(DB.sqlcmd);
            DataSet ds = new DataSet();
            DB.adapter.Fill(ds, "MM_StockMM_REC_S1");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "MM_StockMM_REC_S1";
            DB.transaction.Commit();
            DB.adapter.Dispose();
            DB.sqlcmd.Dispose();
            DB.conn.Dispose();
            DB.conn.Close();
        }
        private void btnDoSearch_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            GetData();
            dataGridView1.Refresh();
        }
        private void GetData()
        {
            try
            {
                DB.conn.Close();
                dataGridView1.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("MM_StockMM_REC_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE",  (cboCompanyCode.SelectedItem.ToString() == "전체") ? "" : cboCompanyCode.SelectedValue.ToString());
                if (cboPlantCode.Enabled == false)
                {
                    DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", "");                   
                }
                else
                {
                    DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", (cboPlantCode.SelectedItem.ToString() == "전체") ? "" : cboPlantCode.SelectedValue.ToString());
                }
                DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE",      (txtCustCode.Text.Length == 0) ? "" : txtCustCode.Text);
                DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME",      (txtCustName.Text.Length == 0) ? "" : txtCustName.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE",      (txtItemCode.Text.Length == 0) ? "" : txtItemCode.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME",      (txtItemName.Text.Length == 0) ? "" : txtItemName.Text);
                DB.sqlcmd.Parameters.AddWithValue("@SEQ",            (txtInoutNo.Text.Length == 0)  ? "" : txtInoutNo.Text);
                DB.sqlcmd.Parameters.AddWithValue("@LOT_NO",         (txtLOTNO.Text.Length == 0)    ? "" : txtLOTNO.Text);
                DB.sqlcmd.Parameters.AddWithValue("@INOUTGROUP",     (cboInout_Group.SelectedItem.ToString() == "전체") ?  "" : cboInout_Group.SelectedValue.ToString());
                DB.sqlcmd.Parameters.AddWithValue("@INOUTTYPE",      (cboInOut_Type.SelectedItem.ToString() == "전체")  ?  "" : cboInOut_Type.SelectedValue.ToString());
                DB.sqlcmd.Parameters.AddWithValue("@INOUTREASON",    (cboInout_Reason.SelectedItem.ToString() == "전체")?  "" : cboInout_Reason.SelectedValue.ToString());
                DB.sqlcmd.Parameters.AddWithValue("@STARTDATE",       StartDate.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ENDDATE",         EndDate.Text);

                DB.sqlcmd.ExecuteNonQuery();              
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "MM_StockMM_REC_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MM_StockMM_REC_S2";
                DB.transaction.Commit();
                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
                //DB.sqlDR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DB.conn.Dispose();
                //DB.sqlDR.Close();
                DB.conn.Close();
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
                COMPAdapter.TableMappings.Add("Table", "COMP_NAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT COMP_CODE, COMP_NAME FROM TB_CompanyMaster";
                COMPAdapter.SelectCommand = DB.sqlcmd;
                COMPAdapter.SelectCommand.ExecuteNonQuery();
                COMPAdapter.Fill(COMPdt);

                cboCompanyCode.DataSource = COMPdt.Tables[0];
                cboCompanyCode.DisplayMember = "COMP_NAME";
                cboCompanyCode.ValueMember = "COMP_CODE";

                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter INOUTAdapter = new SqlDataAdapter();
                DataSet INOUTdt = new DataSet();
                INOUTAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='INOUT_GROUP'";
                INOUTAdapter.SelectCommand = DB.sqlcmd;
                INOUTAdapter.SelectCommand.ExecuteNonQuery();
                INOUTAdapter.Fill(INOUTdt);

                cboInout_Group.DataSource = INOUTdt.Tables[0];
                cboInout_Group.DisplayMember = "LWRCODENAME";
                cboInout_Group.ValueMember = "LWRCODE";

                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter INOUTTYPEAdapter = new SqlDataAdapter();
                DataSet INOUT_Typedt = new DataSet();
                INOUTTYPEAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='INOUT_TYPE'";
                INOUTTYPEAdapter.SelectCommand = DB.sqlcmd;
                INOUTTYPEAdapter.SelectCommand.ExecuteNonQuery();
                INOUTTYPEAdapter.Fill(INOUT_Typedt);

                cboInOut_Type.DataSource = INOUT_Typedt.Tables[0];
                cboInOut_Type.DisplayMember = "LWRCODENAME";
                cboInOut_Type.ValueMember = "LWRCODE";

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

        private void cboCompanyCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompanyCode.Text != "전체")
            {
                cboPlantCode.Enabled = true;
                DB.conn.Close();

                //DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                string compcode = cboCompanyCode.SelectedValue.ToString();

                SqlDataAdapter PLANTAdapter = new SqlDataAdapter();
                DataSet PLANTdt = new DataSet();
                PLANTAdapter.TableMappings.Add("Table", "PLANT_NAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT PLANT_CODE, PLANT_NAME FROM TB_PlantMaster WHERE COMP_CODE LIKE '%" + compcode + "%' OR COMP_CODE = ''";
                PLANTAdapter.SelectCommand = DB.sqlcmd;
                PLANTAdapter.SelectCommand.ExecuteNonQuery();
                PLANTAdapter.Fill(PLANTdt);

                cboPlantCode.DataSource = PLANTdt.Tables[0];
                cboPlantCode.DisplayMember = "PLANT_NAME";
                cboPlantCode.ValueMember = "PLANT_CODE";

                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
            }
            else
            {
                cboPlantCode.Text = null;
                cboPlantCode.Enabled = false;                
            }
        }

        private void cboInout_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboInout_Group.Text != "전체")
            {
                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                string chooseCase = cboInout_Group.SelectedValue.ToString();
                
                SqlDataAdapter ReasonAdapter = new SqlDataAdapter();
                DataSet Reasondt = new DataSet();
                ReasonAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT DISTINCT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='INOUT_REASON' AND LWRCODE LIKE '%" + chooseCase + "%' OR LWRCODE = ''";
                ReasonAdapter.SelectCommand = DB.sqlcmd;
                ReasonAdapter.SelectCommand.ExecuteNonQuery();
                ReasonAdapter.Fill(Reasondt);

                cboInout_Reason.DataSource = Reasondt.Tables[0];
                cboInout_Reason.DisplayMember = "LWRCODENAME";
                cboInout_Reason.ValueMember = "LWRCODE";

                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
            }
            else
            {
                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                
                SqlDataAdapter ReasonAdapter = new SqlDataAdapter();
                DataSet Reasondt = new DataSet();
                ReasonAdapter.TableMappings.Add("Table", "PLANT_NAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = $"SELECT DISTINCT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='INOUT_REASON'";
                ReasonAdapter.SelectCommand = DB.sqlcmd;
                ReasonAdapter.SelectCommand.ExecuteNonQuery();
                ReasonAdapter.Fill(Reasondt);

                cboInout_Reason.DataSource = Reasondt.Tables[0];
                cboInout_Reason.DisplayMember = "LWRCODENAME";
                cboInout_Reason.ValueMember = "LWRCODE";

                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
            }           
        }                      
        private void txtItemCode_MouseClick(object sender, MouseEventArgs e)
        {
            txtItemName_MouseClick(sender, e);
            txtItemCode.SelectionStart = txtItemName.Text.Length;
        }
        private void txtItemName_MouseClick(object sender, MouseEventArgs e)
        {
            ITEMSELECT_POPUP Ipopup = new ITEMSELECT_POPUP();
            Ipopup.ShowDialog();
            txtItemCode.Text = GetItemCode;
            txtItemName.Text = GetItemName;
            txtItemName.SelectionStart = txtItemName.Text.Length;
        }
        private void txtCustCode_MouseClick(object sender, MouseEventArgs e)
        {
            CUSTSELECT_POPUP CustPopup = new CUSTSELECT_POPUP();
            CustPopup.ShowDialog();
            txtCustCode.Text = GetCustCode;
            txtCustName.Text = GetCustName;
            txtCustCode.SelectionStart = txtCustCode.Text.Length;
            
        }
        private void txtCustName_MouseClick(object sender, MouseEventArgs e)
        {
            txtCustCode_MouseClick(sender, e);
            txtCustName.SelectionStart = txtCustName.Text.Length;
        }
        private void txtLOTNO_MouseClick(object sender, MouseEventArgs e)
        {
            txtInoutNo.SelectionStart = txtInoutNo.Text.Length;
        }

        private void txtInoutNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDoSearch_Click_1(sender, e);
                btnDoSearch.Select();
            }
        }

        private void txtLOTNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDoSearch_Click_1(sender, e);
                btnDoSearch.Select();
            }
        }
    }
}
