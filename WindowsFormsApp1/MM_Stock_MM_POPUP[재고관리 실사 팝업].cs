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
    public partial class MM_Stock_MM_POPUP : Form
    {
        static public string LoginID;
        public static string GetItemName;
        public static string GetItemCode;

        public MM_Stock_MM_POPUP()
        {
            InitializeComponent();
        }
       
        private void ITEM_POPUP_Load(object sender, EventArgs e)
        {

            cboCompanyCode.FlatStyle = FlatStyle.Popup;
            cboCompanyCode.BackColor = Color.Ivory;

            cboPlantCode.FlatStyle = FlatStyle.Popup;
            cboPlantCode.BackColor = Color.Ivory;
            cboPlantCode.Enabled = false;
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
                DB.sqlcmd = new SqlCommand("MM_Stock_MM_POPUP_S1", DB.conn, DB.transaction);
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
                DB.adapter.Fill(ds, "MM_Stock_MM_POPUP_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MM_Stock_MM_POPUP_S1";
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].Width = 50;

                // 두번쨰 그리드
          
                dataGridView2.ColumnCount = 13;
                dataGridView2.Columns[0].Name =  "회사";
                dataGridView2.Columns[1].Name =  "공장";
                dataGridView2.Columns[2].Name =  "품목코드";
                dataGridView2.Columns[3].Name =  "품목명";
                dataGridView2.Columns[4].Name =  "품목유형";
                dataGridView2.Columns[5].Name =  "규격";
                dataGridView2.Columns[6].Name =  "현재재고";
                dataGridView2.Columns[7].Name =  "실사 후 재고";
                dataGridView2.Columns[8].Name =  "변경수량";
                dataGridView2.Columns[9].Name =  "기본단위";
                dataGridView2.Columns[10].Name = "단위중량";
                dataGridView2.Columns[11].Name = "회사코드";
                dataGridView2.Columns[12].Name = "공장코드";

                foreach (DataGridViewColumn dg12Column in dataGridView2.Columns)
                {
                    dg12Column.ReadOnly = true;
                }

                dataGridView2.Columns[7].ReadOnly = false;
                dataGridView2.Columns[10].Visible = false;
                dataGridView2.Columns[11].Visible = false;
                dataGridView2.Columns[12].Visible = false;

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
                DB.transaction.Dispose();
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
                DB.sqlcmd = new SqlCommand("MM_Stock_MM_POPUP_S2", DB.conn, DB.transaction);
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
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE",  (txtItemCode.Text.Length == 0) ? "" : txtItemCode.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME",  (txtItemName.Text.Length == 0) ? "" : txtItemName.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "MM_Stock_MM_POPUP_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MM_Stock_MM_POPUP_S2";
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
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
            if (MessageBox.Show("실사품목을 추가하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                try
                {
                    int delcnt = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                        if (isChecked)
                        {
                            object[] drrow1Values = 
                            { 
                               dataGridView1.Rows[i].Cells["회사코드"].Value, 
                               dataGridView1.Rows[i].Cells["공장코드"].Value, 
                               dataGridView1.Rows[i].Cells["품목코드"].Value
                            };
                            for (int j = 0; j < dataGridView2.Rows.Count; j++)
                            {
                                object[] drrow2Values = 
                                { 
                                    dataGridView2.Rows[j].Cells["회사코드"].Value, 
                                    dataGridView2.Rows[j].Cells["공장코드"].Value, 
                                    dataGridView2.Rows[j].Cells["품목코드"].Value
                                };
                                if (drrow1Values[0] == drrow2Values[0] && drrow1Values[1] == drrow2Values[1] && drrow1Values[2] == drrow2Values[2])
                                {
                                    MessageBox.Show("이미 추가된 실사품목입니다.");
                                    return;
                                }
                            }
                            try
                            {
                                dataGridView2.Rows.Add(
                                    dataGridView1.Rows[i].Cells["회사"].Value.ToString(),           // 그리드2의  0번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["공장"].Value.ToString(),           // 그리드2의  1번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["품목코드"].Value.ToString(),        // 그리드2의  2번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["품목명"].Value.ToString(),          // 그리드2의  3번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["품목유형"].Value.ToString(),        // 그리드2의  4번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["규격"].Value.ToString(),           // 그리드2의  5번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["현재재고"].Value.ToString(),        // 그리드2의  6번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["현재재고"].Value.ToString(),        // 그리드2의  7번컬럼에 삽입 
                                    0,                                                              // 그리드2의  8번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["기본단위"].Value.ToString(),                // 그리드2의  9번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["단위중량"].Value.ToString(),        // 그리드2의  10번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["회사코드"].Value.ToString(),        // 그리드2의  11번컬럼에 삽입 
                                    dataGridView1.Rows[i].Cells["공장코드"].Value.ToString()        // 그리드2의  12번컬럼에 삽입 
                                    );                               
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
                        MessageBox.Show("추가할 품목을 선택해 주세요.");
                        return;
                    }
                    else MessageBox.Show("품목이 추가되었습니다.");

                    for (int i = 1; i < dataGridView2.Columns.Count - 1; i++)
                    {
                        dataGridView2.Columns[i].ReadOnly = true;
                    }
                    dataGridView2.Columns[8].ReadOnly = false;
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
            if (MessageBox.Show("실사 내역을 저장하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridView2.Rows.Count == 0)
                {
                    MessageBox.Show("실사 품목을 입력해주세요.");
                    return;
                }
                try
                {
                    DB.conn.Close();

                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.ConnectionString = DB.connectionString;
                    DB.conn.Open();

                    dataGridView2.EndEdit();
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        try
                        {
                            int checkString = 0;
                            bool errorCheck = int.TryParse(dataGridView2.Rows[i].Cells["실사 후 재고"].Value.ToString(), out checkString);
                            if (errorCheck)
                            {
                                if (dataGridView2.Rows[i].Cells["실사 후 재고"].Value.ToString().Length == 0 || dataGridView2.Rows[i].Cells["실사 후 재고"].Value == null)
                                {
                                    MessageBox.Show("조정수량을 입력해주세요.");
                                    return;
                                }
                                DB.transaction = DB.conn.BeginTransaction();
                                DB.sqlcmd = new SqlCommand("MM_Stock_MM_POPUP_U1", DB.conn, DB.transaction);
                                DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE", dataGridView2.Rows[i].Cells["회사코드"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@COMP_NAME", dataGridView2.Rows[i].Cells["회사"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", dataGridView2.Rows[i].Cells["공장코드"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@PLANT_NAME", dataGridView2.Rows[i].Cells["공장"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", dataGridView2.Rows[i].Cells["품목코드"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME", dataGridView2.Rows[i].Cells["품목명"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@AFTER_QTY",   Convert.ToInt32(dataGridView2.Rows[i].Cells["실사 후 재고"].Value.ToString()));
                                DB.sqlcmd.Parameters.AddWithValue("@CHANGED_QTY", Convert.ToInt32(dataGridView2.Rows[i].Cells["변경수량"].Value.ToString()));
                                DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID", LoginID);

                                DB.sqlcmd.ExecuteNonQuery();
                                DB.transaction.Commit();
                            }
                            else
                            {
                                MessageBox.Show("실사 후 재고란에는 숫자만 입력해주세요.");
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            DB.transaction.Rollback();
                            MessageBox.Show(ex.Message);
                            MessageBox.Show("저장이 취소되었습니다.");
                            return;
                        }
                    }
                    MessageBox.Show("저장이 완료되었습니다.");              
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
            if (MessageBox.Show("실사 품목을 삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                            dataGridView2.Rows.Remove(dataGridView2.SelectedRows[i- delcnt]);                              
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        delcnt++;
                    }
                    if (delcnt == 0)
                    {
                        MessageBox.Show("삭제할 항목을 선택해 주세요.");
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
        private void txtItemCode_Click(object sender, EventArgs e)
        {
            ITEMSELECT_POPUP I_Popup = new ITEMSELECT_POPUP();
            I_Popup.ShowDialog();
            txtItemCode.Text = GetItemCode;
            txtItemName.Text = GetItemName;
            txtItemCode.SelectionStart = txtItemCode.TextLength;           
        }

        private void txtItemName_Click(object sender, EventArgs e)
        {
            txtItemCode_Click(sender, e);
            txtItemName.SelectionStart = txtItemName.TextLength;
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
                cboPlantCode.Text = string.Empty;
                cboPlantCode.Enabled = false;
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int checkString = 0;
            bool errorCheck = int.TryParse(dataGridView2.Rows[e.RowIndex].Cells["실사 후 재고"].Value.ToString(), out checkString);
            if (errorCheck)
            {
                int beforeQty = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["현재재고"].Value.ToString());
                int afterQty = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["실사 후 재고"].Value.ToString());
               
                dataGridView2.Rows[e.RowIndex].Cells[9].Value = afterQty - beforeQty;
            }
            else
            {
                MessageBox.Show("실사 후 재고란에는 숫자만 입력해주세요.");
                dataGridView2.Rows[e.RowIndex].Cells["실사 후 재고"].Value = 0;
            }
        }

        private void btnItemClear_Click(object sender, EventArgs e)
        {
            txtItemCode.Text = string.Empty;
            txtItemName.Text = string.Empty;
            txtLOTNO.Text    = string.Empty;
        }

        private void txtLOTNO_MouseClick(object sender, MouseEventArgs e)
        {
            txtLOTNO.SelectionStart = txtLOTNO.Text.Length;
        }
    }
}
