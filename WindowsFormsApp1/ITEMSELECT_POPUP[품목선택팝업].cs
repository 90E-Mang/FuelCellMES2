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
    public partial class ITEMSELECT_POPUP : Form
    {

        public ITEMSELECT_POPUP()
        {
            InitializeComponent();
        }
       
        private void ITEMSELECT_POPUP_Load(object sender, EventArgs e)
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
                DB.sqlcmd = new SqlCommand("ITEM_POPUP_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.ExecuteNonQuery();
               
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "ITEM_POPUP_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "ITEM_POPUP_S1";
                
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
                DB.sqlcmd = new SqlCommand("ITEM_POPUP_S4", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", ItemCodeTxtBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME", ItemNameTxtBox.Text);

                DB.sqlcmd.ExecuteNonQuery();
                
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "ITEM_POPUP_S4");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "ITEM_POPUP_S4";                

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
                    string ItemCode = row.Cells[0].Value.ToString();
                    string ItemName = row.Cells[1].Value.ToString();

                    MM_StockMM_REC.GetItemCode = ItemCode;
                    MM_StockMM_REC.GetItemName = ItemName;
                    MM_Stock_MM.GetItemCode = ItemCode;
                    MM_Stock_MM.GetItemName = ItemName;
                    MM_Stock_MM_POPUP.GetItemCode = ItemCode;
                    MM_Stock_MM_POPUP.GetItemName = ItemName;
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

        private void btnChoose_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            try
            {
                string ItemCode = row.Cells[0].Value.ToString();
                string ItemName = row.Cells[1].Value.ToString();

                MM_StockMM_REC.GetItemCode = ItemCode;
                MM_StockMM_REC.GetItemName = ItemName;
                MM_Stock_MM.GetItemCode = ItemCode;
                MM_Stock_MM.GetItemName = ItemName;
                MM_Stock_MM_POPUP.GetItemCode = ItemCode;
                MM_Stock_MM_POPUP.GetItemName = ItemName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
    }
}
