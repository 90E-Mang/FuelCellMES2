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
    public partial class MM_STOCKOUT_POPUP : Form
    {
        static public string LoginID;

        public MM_STOCKOUT_POPUP()
        {
            InitializeComponent();
            StartDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), 1);
            EndDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), int.Parse(DateTime.Now.ToString("dd")));
        }
        
        private void STOCKOUT_POPUP_Load(object sender, EventArgs e)
        {         
           try
           {
               cboCompanyCode.FlatStyle = FlatStyle.Popup;
               cboCompanyCode.BackColor = Color.Ivory;


               cboPlantCode.FlatStyle = FlatStyle.Popup;
               cboPlantCode.BackColor = Color.Ivory;
               cboPlantCode.Enabled = false;

               cboProcessCode.FlatStyle = FlatStyle.Popup;
               cboProcessCode.BackColor = Color.Ivory;

                CombMaster();

               DB.conn.Close();
               dataGridView1.Columns.Clear();
               dataGridView2.Columns.Clear();

               DB.conn = new SqlConnection(DB.connectionString);
               DB.conn.ConnectionString = DB.connectionString;
               DB.conn.Open();
               //트랜잭션 시작
               DB.transaction = DB.conn.BeginTransaction();
               DB.sqlcmd = new SqlCommand("MM_STOCKOUT_POPUP_S1", DB.conn, DB.transaction);
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
               DB.adapter.Fill(ds, "MM_STOCKOUT_POPUP_S1");
               dataGridView1.DataSource = ds;
               dataGridView1.DataMember = "MM_STOCKOUT_POPUP_S1";
               for (int i = 1; i < dataGridView1.Columns.Count; i++)
               {
                   dataGridView1.Columns[i].ReadOnly = true;
               }
               dataGridView1.Columns[0].Width = 50;

                DB.transaction.Commit();
                DB.transaction.Dispose();
                DB.conn.Close();

                // 두번째 그리드
                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("MM_STOCKOUT_POPUP_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.ExecuteNonQuery();               
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds2 = new DataSet();
                DB.adapter.Fill(ds2, "MM_STOCKOUT_POPUP_S2");
                dataGridView2.DataSource = ds2;
                dataGridView2.DataMember = "MM_STOCKOUT_POPUP_S2";
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                DB.transaction.Commit();
                DB.transaction.Dispose();
                DB.conn.Close();
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
                DB.sqlcmd = new SqlCommand("MM_STOCKOUT_POPUP_S3", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE", cboCompanyCode.SelectedValue.ToString());
                if (cboPlantCode.Enabled == false)
                {
                    DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", "");
                }
                else
                {
                    DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", (cboPlantCode.SelectedItem.ToString() == "전체") ? "" : cboPlantCode.SelectedValue.ToString());
                }
                DB.sqlcmd.Parameters.AddWithValue("@PROCESS_CODE", (cboProcessCode.SelectedItem.ToString() == "전체") ? "" : cboProcessCode.SelectedValue.ToString());
                DB.sqlcmd.Parameters.AddWithValue("@OUT_REQ_NO", (txtOutREQ_No.Text.Length == 0) ? "" : txtOutREQ_No.Text);
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
                DB.adapter.Fill(ds, "MM_STOCKOUT_POPUP_S3");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MM_STOCKOUT_POPUP_S3";
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                DB.transaction.Commit();

            }
            catch (Exception ex)
            {
                DB.transaction.Rollback();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DB.transaction.Dispose();
                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Index == e.RowIndex)
                        {
                            row.Cells[0].Value = !Convert.ToBoolean(row.Cells[0].EditedFormattedValue);
                        }
                        else
                        {
                            row.Cells[0].Value = false;
                        }

                    }
                }
                DB.conn.Close();
                dataGridView2.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("MM_STOCKOUT_POPUP_S4", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@OUT_REQ_NO", dataGridView1.Rows[e.RowIndex].Cells["불출요청번호"].Value.ToString());

                DB.sqlcmd.ExecuteNonQuery();
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "MM_STOCKOUT_POPUP_S4");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "MM_STOCKOUT_POPUP_S4";
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                dataGridView2.Columns[7].ReadOnly = false;
                //dataGridView2.Columns[0].Width = 130;
                //dataGridView2.Columns[6].Width = 130;
                //dataGridView2.Columns[7].Width = 130;
                dataGridView2.Columns[10].Visible = false;
                dataGridView2.Columns[11].Visible = false;
                dataGridView2.Columns[12].Visible = false;
                //for (int i = 0; i < dataGridView2.Rows.Count; i++)
                //{
                //    dataGridView1.Rows[i].Cells["불출처리수량"].Value = Convert.ToInt32(dataGridView2.Rows[i].Cells["잔여요청수량"].Value.ToString());
                //}

            }
            catch (ArgumentOutOfRangeException)
            {

            }
            
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("해당 품목을 불출하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {             
                try
                {
                    DB.conn.Close();

                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.ConnectionString = DB.connectionString;
                    DB.conn.Open();

                    dataGridView1.EndEdit();
                    int selcnt = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                        if (isChecked)
                        {
                            DB.transaction = DB.conn.BeginTransaction();
                            DB.sqlcmd = new SqlCommand("MM_STOCKOUT_MM_H_I1", DB.conn, DB.transaction);
                            DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                            DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE", dataGridView1.Rows[i].Cells[7].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", dataGridView1.Rows[i].Cells[8].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@PROCESS_CODE", dataGridView1.Rows[i].Cells[9].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID", LoginID);

                            DB.sqlcmd.ExecuteNonQuery();

                            DB.transaction.Commit();
                            selcnt++;
                        }                    
                    }
                    #region<Validation Check>
                    if (selcnt == 0)
                    {
                        MessageBox.Show("불출처리할 요청목록을 선택해주세요.");
                        return;
                    }
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        int checkString = 0; // 정수 이외의 숫자를 넣었는지 체크용
                        bool checkQtyValue = int.TryParse(dataGridView2.Rows[i].Cells["불출처리수량"].Value.ToString(), out checkString);
                        if (checkQtyValue)
                        {
                            if (checkString <= 0)
                            {
                                MessageBox.Show("불출처리수량은 0보다 큰 값을 입력해주세요.");
                                return;
                            }
                            else if (checkString > Convert.ToInt32(dataGridView2.Rows[i].Cells["잔여요청수량"].Value.ToString()))
                            {
                                MessageBox.Show("불출처리수량은 잔여요청수량보다 작은 값을 입력해주세요.");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("불출처리수량은 숫자만 입력해주세요.");
                            return;
                        }
                    }
                    #endregion
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

                    DataGridViewRow drrow1 = this.dataGridView1.SelectedRows[0];

                    dataGridView2.EndEdit();
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        try
                        {
                            DB.transaction = DB.conn.BeginTransaction();
                            DB.sqlcmd = new SqlCommand("MM_STOCKOUT_MM_D_I1", DB.conn, DB.transaction);
                            DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                            DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE",     drrow1.Cells[7].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE",    drrow1.Cells[8].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@OUT_REQ_NO",    dataGridView2.Rows[i].Cells["불출요청번호"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE",     dataGridView2.Rows[i].Cells["품목코드"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME",     dataGridView2.Rows[i].Cells["품명"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE",     dataGridView2.Rows[i].Cells[10].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@LOT_NO",        dataGridView2.Rows[i].Cells["LOT번호"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@OUT_REQ_QTY",   Convert.ToInt32(dataGridView2.Rows[i].Cells["불출요청수량"].Value.ToString()));
                            DB.sqlcmd.Parameters.AddWithValue("@STOCKOUT_QTY",  Convert.ToInt32(dataGridView2.Rows[i].Cells["불출처리수량"].Value.ToString()));
                            DB.sqlcmd.Parameters.AddWithValue("@LEFT_REQ_QTY",  Convert.ToInt32(dataGridView2.Rows[i].Cells["잔여요청수량"].Value.ToString()));
                            DB.sqlcmd.Parameters.AddWithValue("@BASEUNIT",      dataGridView2.Rows[i].Cells["단위"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@INOUT_REASON",  dataGridView2.Rows[i].Cells[11].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID",      LoginID);

                            DB.sqlcmd.ExecuteNonQuery();

                            DB.transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            DB.transaction.Rollback();
                            MessageBox.Show(ex.Message);
                            MessageBox.Show("저장이 취소되었습니다.");
                        }
                    }
                    MessageBox.Show("저장되었습니다.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("저장이 취소되었습니다.");

                }
                finally
                {
                    DB.conn.Dispose();
                    DB.conn.Close();
                    DB.sqlcmd.Dispose();
                    this.Close();
                }
            }
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("불출 품목을 삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    dataGridView2.EndEdit();
                    int delcnt = 0;
                    int selectedRowCount = dataGridView2.SelectedRows.Count;
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        try
                        {
                            dataGridView2.Rows.Remove(dataGridView2.SelectedRows[i - delcnt]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        delcnt++;
                    }
                    if (delcnt == 0)
                    {
                        MessageBox.Show("삭제할 품목을 선택해 주세요.");
                        return;
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

                SqlDataAdapter processAdapter = new SqlDataAdapter();
                DataSet processDt = new DataSet();
                processAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='PROCESS_CODE'";
                processAdapter.SelectCommand = DB.sqlcmd;
                processAdapter.SelectCommand.ExecuteNonQuery();
                processAdapter.Fill(processDt);

                cboProcessCode.DataSource = processDt.Tables[0];
                cboProcessCode.DisplayMember = "LWRCODENAME";
                cboProcessCode.ValueMember = "LWRCODE";

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
                cboPlantCode.Text = string.Empty;
                cboPlantCode.Enabled = false;
            }
        }

        private void txtOutREQ_No_MouseClick(object sender, MouseEventArgs e)
        {
            txtOutREQ_No.SelectionStart = txtOutREQ_No.Text.Length;
        }

        private void txtOutREQ_No_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectButton_Click(sender, e);
                SelectButton.Select();
            }
        }
        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {           
            MessageBox.Show("불출처리수량은 숫자만 입력해주세요.");
            //dataGridView2.Rows[e.RowIndex].Cells[7].Value = 0;
            return;
        }
    }
}
