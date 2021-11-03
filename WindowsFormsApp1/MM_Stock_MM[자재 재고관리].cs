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
    public partial class MM_Stock_MM : Form
    {
        public static string GetItemCode;
        public static string GetItemName;
        public MM_Stock_MM()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        public delegate void FormClosed(string str);
        public event FormClosed FormCloseEvent;


        private void MM_Stock_MM_Load(object sender, EventArgs e)
        {          
            CombMaster();
            
            cboCompanyCode.FlatStyle = FlatStyle.Popup;
            cboCompanyCode.BackColor = Color.Ivory;
            
            cboPlantCode.FlatStyle = FlatStyle.Popup;
            cboPlantCode.BackColor = Color.Ivory;
            cboPlantCode.Enabled = false;
           
            cboUseFlag.FlatStyle = FlatStyle.Popup;
            cboUseFlag.BackColor = Color.Ivory;

            DB.conn.Close();
            dataGridView1.Columns.Clear();

            DB.conn = new SqlConnection(DB.connectionString);
            DB.conn.ConnectionString = DB.connectionString;
            DB.conn.Open();
            //트랜잭션 시작
            DB.transaction = DB.conn.BeginTransaction();
            DB.sqlcmd = new SqlCommand("MM_Stock_MM_S1", DB.conn, DB.transaction);
            DB.sqlcmd.CommandType = CommandType.StoredProcedure;          

            DB.sqlcmd.ExecuteNonQuery();
            DB.adapter = new SqlDataAdapter(DB.sqlcmd);
            DataSet ds = new DataSet();
            DB.adapter.Fill(ds, "MM_Stock_MM_S1");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "MM_Stock_MM_S1";
            DB.transaction.Commit();
            DB.adapter.Dispose();
            DB.sqlcmd.Dispose();
            DB.conn.Dispose();
            DB.conn.Close();
        }
        private void btnDoSearch_Click_1(object sender, EventArgs e)
        {
            GetData();
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

        private void btnDoBalance_Click(object sender, EventArgs e)
        {
            MM_Stock_MM_POPUP DoBalance = new MM_Stock_MM_POPUP();
            DoBalance.ShowDialog();
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
                DB.sqlcmd = new SqlCommand("MM_Stock_MM_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE", (cboCompanyCode.SelectedItem.ToString() == "전체") ? "" : cboCompanyCode.SelectedValue.ToString());
                if (cboPlantCode.Enabled == false)
                {
                    DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", "");
                }
                else
                {
                    DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", (cboPlantCode.SelectedItem.ToString() == "전체") ? "" : cboPlantCode.SelectedValue.ToString());
                }
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", (txtItemCode.Text.Length == 0) ? "" : txtItemCode.Text);
                DB.sqlcmd.Parameters.AddWithValue("@USEFLAG",   cboUseFlag.SelectedValue.ToString());

                DB.sqlcmd.ExecuteNonQuery();
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "MM_Stock_MM_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MM_Stock_MM_S2";

                dataGridView1.Columns["회사코드"].Visible = false;
                dataGridView1.Columns["공장코드"].Visible = false;
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

                SqlDataAdapter USEAdapter = new SqlDataAdapter();
                DataSet USEdt = new DataSet();
                USEAdapter.TableMappings.Add("Table", "USE_FLAG");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE = 'USE_TYPE' ORDER BY SORT_NO";
                USEAdapter.SelectCommand = DB.sqlcmd;
                USEAdapter.SelectCommand.ExecuteNonQuery();
                USEAdapter.Fill(USEdt);
                cboUseFlag.DataSource = USEdt.Tables[0];
                cboUseFlag.DisplayMember = "LWRCODENAME";
                cboUseFlag.ValueMember = "LWRCODE";
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
