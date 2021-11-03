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
    public partial class MM_MTRL_Order_POPUP : Form
    {
        static public string LoginID;
        public static string GetCustName;
        public static string GetCustCode;

        public MM_MTRL_Order_POPUP()
        {
            InitializeComponent();
            StartDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), 1);
            EndDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), int.Parse(DateTime.Now.ToString("dd")));
        }
        
        private void ITEM_POPUP_Load(object sender, EventArgs e)
        {         
           try
           {
               DB.conn.Close();
               dataGridView1.Columns.Clear();
               dataGridView2.Columns.Clear();

               DB.conn = new SqlConnection(DB.connectionString);
               DB.conn.ConnectionString = DB.connectionString;
               DB.conn.Open();
               //트랜잭션 시작
               DB.transaction = DB.conn.BeginTransaction();
               DB.sqlcmd = new SqlCommand("MM_MTRL_Order_POPUP_S1", DB.conn, DB.transaction);
               DB.sqlcmd.CommandType = CommandType.StoredProcedure;

               DB.sqlcmd.ExecuteNonQuery();
               var chkCol = new DataGridViewCheckBoxColumn
               {
                   Name = "check",
                   HeaderText = "선택"
               };
               dataGridView1.Columns.Add(chkCol);
               DB.adapter = new SqlDataAdapter(DB.sqlcmd);
               DataSet ds = new DataSet();
               DB.adapter.Fill(ds, "MM_MTRL_Order_POPUP_S1");
               dataGridView1.DataSource = ds;
               dataGridView1.DataMember = "MM_MTRL_Order_POPUP_S1";
               for (int i = 1; i < dataGridView1.Columns.Count; i++)
               {
                   dataGridView1.Columns[i].ReadOnly = true;
               }
               dataGridView1.Columns[0].Width = 50;


               // 두번쨰 그리드
               var chkCol2 = new DataGridViewCheckBoxColumn
               {
                   Name = "check",
                   HeaderText = "선택"
               };
               dataGridView2.Columns.Add(chkCol2);
               
               dataGridView2.ColumnCount = 10;
               dataGridView2.Columns[1].Name = "요청번호";
               dataGridView2.Columns[2].Name = "요청수량";
               dataGridView2.Columns[3].Name = "잔여수량";
               dataGridView2.Columns[4].Name = "발주수량";
               dataGridView2.Columns[5].Name = "품목코드";
               dataGridView2.Columns[6].Name = "품목명";             
               dataGridView2.Columns[7].Name = "기본단위";               
               dataGridView2.Columns[8].Name = "단위중량";
               dataGridView2.Columns[9].Name = "단가";
               
               dataGridView2.Columns[0].Width = 50;


                DB.transaction.Commit();

               
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
                DB.conn.Close();
                dataGridView1.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("MM_MTRL_Order_POPUP_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", txtSearch_OrderNo.Text);
                DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME", txtSearch_CustName.Text);
                DB.sqlcmd.Parameters.AddWithValue("@STARTDATE", StartDate.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ENDDATE", EndDate.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "MM_MTRL_Order_POPUP_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MM_MTRL_Order_POPUP_S2";
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].Width = 50;

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
        private void InsertButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("품목을 추가하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {              
                
                try
                {               
                    int delcnt = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                        if (isChecked)
                        {
                            object[] drrow1Values = {dataGridView1.Rows[i].Cells[1].Value, dataGridView1.Rows[i].Cells[4].Value};
                            for (int j = 0; j < dataGridView2.Rows.Count; j++)
                            {
                                object[] drrow2Values = { dataGridView2.Rows[j].Cells[1].Value, dataGridView2.Rows[j].Cells[5].Value};
                                if (drrow1Values[0] == drrow2Values[0] && drrow1Values[1] == drrow2Values[1])
                                {
                                    MessageBox.Show("이미 추가된 품목입니다.");
                                    return;
                                }
                            }                                                     
                            try
                            {                               
                                dataGridView2.Rows.Add(false,
                                    dataGridView1.Rows[i].Cells[1].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[6].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[8].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[8].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[4].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[5].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[10].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[11].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[9].Value.ToString()                                   
                                    );
                                dataGridView2.Columns[0].Width = 50;
                                txtCustCode.ReadOnly = false;
                                txtCustName.ReadOnly = false;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            delcnt++;
                        }
                    }
                    dataGridView2.Columns[1].ReadOnly = true;
                    dataGridView2.Columns[2].ReadOnly = true;
                    dataGridView2.Columns[3].ReadOnly = true;
                    dataGridView2.Columns[4].ReadOnly = false;
                    dataGridView2.Columns[5].ReadOnly = true;
                    dataGridView2.Columns[6].ReadOnly = true;
                    dataGridView2.Columns[7].ReadOnly = true;
                    dataGridView2.Columns[8].ReadOnly = true;
                    dataGridView2.Columns[9].ReadOnly = false;

                    if (delcnt == 0)
                    {
                        MessageBox.Show("추가할 품목을 선택해 주세요.");
                        return;
                    }
                    else MessageBox.Show("추가되었습니다.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    DB.conn.Dispose();
                    DB.conn.Close();
                }
            }
            else
            {
                return;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("저장하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int checkCount = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    bool isChecked = Convert.ToBoolean(dataGridView2.Rows[i].Cells[0].Value);
                    if (isChecked)
                    {
                        checkCount++;
                    }
                }
                if (checkCount == 0)
                {
                    MessageBox.Show("발주할 품목을 선택해주세요.");
                    return;
                }
                if (txtCustName.Text.Length == 0 || txtCustCode.Text.Length == 0)
                {
                    MessageBox.Show("거래처는 필수 입력 항목입니다. 상단 입력창을 통해 입력해주세요.");
                    return;
                }
                try
                {
                    DB.conn.Close();

                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.ConnectionString = DB.connectionString;
                    DB.conn.Open();

                    DB.transaction = DB.conn.BeginTransaction();
                    DB.sqlcmd = new SqlCommand("MM_MTRL_Order_MM_H_I1", DB.conn, DB.transaction);
                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                    DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE", txtCustCode.Text);
                    DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME", txtCustName.Text);                   
                    DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID", LoginID);

                    DB.sqlcmd.ExecuteNonQuery();

                    DB.transaction.Commit();
                }
                catch (Exception)
                {
                    DB.transaction.Rollback();

                    throw;
                }
                finally
                {
                    DB.conn.Dispose();
                    DB.conn.Close();
                    DB.sqlcmd.Dispose();
                }


                try
                {
                    DB.conn.Close();

                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.ConnectionString = DB.connectionString;
                    DB.conn.Open();

                    dataGridView2.EndEdit();
                    int selcnt = 0;
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView2.Rows[i].Cells[0].Value);
                        if (isChecked)
                        {
                            try
                            {
                                DB.transaction = DB.conn.BeginTransaction();
                                DB.sqlcmd = new SqlCommand("MM_MTRL_Order_MM_D_I1", DB.conn, DB.transaction);
                                DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", dataGridView2.Rows[i].Cells["요청번호"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE", txtCustCode.Text);
                                DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME", txtCustName.Text);
                                DB.sqlcmd.Parameters.AddWithValue("@REQQTY", Convert.ToInt32(dataGridView2.Rows[i].Cells["잔여수량"].Value.ToString()));
                                DB.sqlcmd.Parameters.AddWithValue("@ORDQTY", Convert.ToInt32(dataGridView2.Rows[i].Cells["발주수량"].Value.ToString()));
                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", dataGridView2.Rows[i].Cells["품목코드"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME", dataGridView2.Rows[i].Cells["품목명"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@UNITCOST", dataGridView2.Rows[i].Cells["단가"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID", LoginID);

                                DB.sqlcmd.ExecuteNonQuery();

                                DB.transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                DB.transaction.Rollback();
                                MessageBox.Show(ex.Message);
                                MessageBox.Show("저장이 취소되었습니다.");
                            }
                            selcnt++;
                        }

                    }
                    if (selcnt == 0)
                    {
                        MessageBox.Show("저장할 항목을 선택해 주세요.");
                    }
                    else
                    {
                        MessageBox.Show("저장되었습니다.");
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("저장이 취소되었습니다.");

                }
                finally
                {
                    dataGridView2.Rows.Clear();

                    DB.conn.Dispose();
                    DB.conn.Close();
                    DB.sqlcmd.Dispose();
                    this.Close();
                }
            }
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {

                    dataGridView2.EndEdit();
                    int datagridview2cnt = dataGridView2.Rows.Count;
                    int delcnt = 0;
                    for (int i = 0; i < datagridview2cnt; i++)
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView2.Rows[i - delcnt].Cells[0].Value);
                        if (isChecked)
                        {
                            try
                            {
                                dataGridView2.Rows.Remove(dataGridView2.Rows[i - delcnt]);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            delcnt++;
                        }
                    }
                    if (delcnt == 0)
                    {
                        MessageBox.Show("삭제할 항목을 선택해 주세요.");
                    }
                    else MessageBox.Show("삭제되었습니다.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                }
            }
        }
        private void txtCustName_MouseClick(object sender, MouseEventArgs e)
        {
            CUSTSELECT_POPUP CustPopup = new CUSTSELECT_POPUP();
            CustPopup.ShowDialog();
            txtCustName.Text = GetCustName;
            txtCustCode.Text = GetCustCode;
            txtCustCode.ReadOnly = true;
            txtCustName.ReadOnly = true;
            txtCustName.SelectionStart = txtCustName.Text.Length;
        }

        private void txtCustCode_MouseClick(object sender, MouseEventArgs e)
        {
            txtCustName_MouseClick(sender, e);
            txtCustCode.SelectionStart = txtCustCode.Text.Length;
        }
    }
}
