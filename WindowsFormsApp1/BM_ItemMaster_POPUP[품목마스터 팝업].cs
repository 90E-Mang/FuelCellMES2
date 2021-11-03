using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class BM_ItemMaster_POPUP : Form
    {
        public BM_ItemMaster_POPUP()
        {
            InitializeComponent();
        }

        private void CombMaster()
        {
            try
            {
                CustGroupCmb.FlatStyle = FlatStyle.Popup;
                CustGroupCmb.BackColor = Color.Ivory;
                CustType1Cmb.FlatStyle = FlatStyle.Popup;
                CustType1Cmb.BackColor = Color.Ivory;
                CustType2Cmb.FlatStyle = FlatStyle.Popup;
                CustType2Cmb.BackColor = Color.Ivory;

                DB.conn.Close();

                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();

                SqlDataAdapter CuGroupAdapter = new SqlDataAdapter();
                DataSet CuGroupdt = new DataSet();
                CuGroupAdapter.TableMappings.Add("Table", "CUST_GROUP");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE = 'CUST_GROUP'";
                CuGroupAdapter.SelectCommand = DB.sqlcmd;
                CuGroupAdapter.SelectCommand.ExecuteNonQuery();
                CuGroupAdapter.Fill(CuGroupdt);
                CustGroupCmb.DataSource = CuGroupdt.Tables[0];
                CustGroupCmb.DisplayMember = "LWRCODENAME";
                CustGroupCmb.ValueMember = "LWRCODE";
                CustGroupCmb.Text = "";

                SqlDataAdapter CuType1Adapter = new SqlDataAdapter();
                DataSet CuType1dt = new DataSet();
                CuType1Adapter.TableMappings.Add("Table", "CUST_TYPE1");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE = 'CUST_TYPE1'";
                CuType1Adapter.SelectCommand = DB.sqlcmd;
                CuType1Adapter.SelectCommand.ExecuteNonQuery();
                CuType1Adapter.Fill(CuType1dt);
                CustType1Cmb.DataSource = CuType1dt.Tables[0];
                CustType1Cmb.DisplayMember = "LWRCODENAME";
                CustType1Cmb.ValueMember = "LWRCODE";
                CustType1Cmb.Text = "";

                SqlDataAdapter CuType2Adapter = new SqlDataAdapter();
                DataSet CuType2dt = new DataSet();
                CuType2Adapter.TableMappings.Add("Table", "CUST_TYPE2");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE = 'CUST_TYPE2'";
                CuType2Adapter.SelectCommand = DB.sqlcmd;
                CuType2Adapter.SelectCommand.ExecuteNonQuery();
                CuType2Adapter.Fill(CuType2dt);
                CustType2Cmb.DataSource = CuType2dt.Tables[0];
                CustType2Cmb.DisplayMember = "LWRCODENAME";
                CustType2Cmb.ValueMember = "LWRCODE";
                CustType2Cmb.Text = "";
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
        private void select()
        {
            DB.conn.Close();
            dataGridView1.Columns.Clear();

            DB.conn = new SqlConnection(DB.connectionString);
            DB.conn.ConnectionString = DB.connectionString;
            DB.conn.Open();
            //트랜잭션 시작
            DB.transaction = DB.conn.BeginTransaction();
            DB.sqlcmd = new SqlCommand("Item_POPUP_Cust_S1", DB.conn, DB.transaction);
            DB.sqlcmd.CommandType = CommandType.StoredProcedure;

            DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE", CustCodeTxtBox.Text);
            DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME", CustNameTxtBox.Text);
            DB.sqlcmd.Parameters.AddWithValue("@CUST_GROUP", CustGroupCmb.Text);
            DB.sqlcmd.Parameters.AddWithValue("@CUST_TYPE1", CustType1Cmb.Text);
            DB.sqlcmd.Parameters.AddWithValue("@CUST_TYPE2", CustType2Cmb.Text);

            DB.sqlcmd.ExecuteNonQuery();

            DB.adapter = new SqlDataAdapter(DB.sqlcmd);
            DataSet ds = new DataSet();
            DB.adapter.Fill(ds, "Item_POPUP_Cust_S1");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Item_POPUP_Cust_S1";
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
            }
            DB.transaction.Commit();
            DB.adapter.Dispose();
            DB.sqlcmd.Dispose();
            DB.conn.Dispose();
            DB.conn.Close();
            DB.sqlDR.Close();

        }
        private void Cust_POPUP_Load(object sender, EventArgs e)
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
                DB.sqlcmd = new SqlCommand("Item_POPUP_Cust_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.ExecuteNonQuery();
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "Item_POPUP_Cust_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Item_POPUP_Cust_S2";
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }

                DB.transaction.Commit();

                CombMaster();
            }
            catch (Exception ex)
            {
                //DB.transaction.Rollback();
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

        private void SelectButton_Click(object sender, EventArgs e)
        {
            try
            {
                select();
                MessageBox.Show("조회성공");

            }
            catch (Exception ex)
            {
                DB.transaction.Rollback();
                MessageBox.Show("조회실패\n" + ex.Message);
            }
            finally
            {
                DB.conn.Dispose();
                DB.sqlDR.Close();
                DB.conn.Close();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                try
                {
                    string cuscode = row.Cells[1].Value.ToString();
                    BM_ItemMaster_MM.Custcode = cuscode;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {

                }

                this.Close();
            }
        }
    }
}
