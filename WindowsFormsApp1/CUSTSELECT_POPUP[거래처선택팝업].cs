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
    public partial class CUSTSELECT_POPUP : Form
    {

        public CUSTSELECT_POPUP()
        {
            InitializeComponent();
        }
       
        private void CUSTSELECT_POPUP_Load(object sender, EventArgs e)
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
                DB.sqlcmd = new SqlCommand("CUST_POPUP_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.ExecuteNonQuery();
               
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "CUST_POPUP_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "CUST_POPUP_S1";
                
                DB.transaction.Commit();
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

        private void SelectButton_Click(object sender, EventArgs e)
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
                DB.sqlcmd = new SqlCommand("CUST_POPUP_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE", CustCodeTxtBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME", CustNameTxtBox.Text);

                DB.sqlcmd.ExecuteNonQuery();
                
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "CUST_POPUP_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "CUST_POPUP_S2";                

                DB.transaction.Commit();
                
            }
            catch (Exception ex)
            {
                DB.transaction.Rollback();
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

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                try
                {
                    string CustCode = row.Cells[0].Value.ToString();
                    string CustName = row.Cells[1].Value.ToString();

                    MM_StockMM_REC.GetCustCode = CustCode;
                    MM_StockMM_REC.GetCustName = CustName;
                    MM_MTRL_Order_POPUP.GetCustCode = CustCode;
                    MM_MTRL_Order_POPUP.GetCustName = CustName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                }
                this.Close();
            }
        }      
    }
}
