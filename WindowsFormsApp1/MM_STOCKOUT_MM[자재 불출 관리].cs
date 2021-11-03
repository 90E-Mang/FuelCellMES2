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
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class MM_STOCKOUT_MM : Form
    {
        static public string LoginID;
        string stockOutNo = null;
        public MM_STOCKOUT_MM()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            StartDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), 1);
            EndDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), int.Parse(DateTime.Now.ToString("dd")));

        }
        public delegate void FormClosed(string str);
        public event FormClosed FormCloseEvent;
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.FormCloseEvent("자재 불출 관리");
            this.Close();
        }
        
        private void MM_STOCKOUT_Load(object sender, EventArgs e)
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

                DataSet ds = new DataSet();
                DB.conn.Close();
                dataGridView1.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("MM_STOCKOUT_MM_H_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;                

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                
                DB.adapter.Fill(ds, "MM_STOCKOUT_MM_H_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MM_STOCKOUT_MM_H_S1";                
                
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.Columns[0].Width = 50;
 
                dataGridView2.Columns.Clear();
                ds.Clear();
                DB.transaction.Commit();
                DB.sqlcmd.Dispose();
                DB.adapter.Dispose();

                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("MM_STOCKOUT_MM_D_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.ExecuteNonQuery();

                var chkCol2 = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView2.Columns.Add(chkCol2);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DB.adapter.Fill(ds, "MM_STOCKOUT_MM_D_S1");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "MM_STOCKOUT_MM_D_S1";               
                for (int i = 0 ; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView2.Columns[0].Width = 50;
                dataGridView2.Columns[2].Width = 130;
                dataGridView2.Columns[7].Width = 130;

                DB.transaction.Commit();
                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
                DB.sqlDR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DB.conn.Dispose();
                DB.sqlDR.Close();
                DB.conn.Close();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            MM_STOCKOUT_POPUP stockOutPopUP = new MM_STOCKOUT_POPUP();
            stockOutPopUP.ShowDialog();
        }

        private void btnDoInquire_Click(object sender, EventArgs e)
        {
            select();
            dataGridView1.Refresh();
        }
        private void btnRunExcel_Click(object sender, EventArgs e)
        {
            int selcnt = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                if (isChecked)
                {
                    stockOutNo = dataGridView1.Rows[i].Cells["불출번호"].Value.ToString();
                    selcnt++;
                }
            }
            if (selcnt == 0)
            {
                MessageBox.Show("불출 명세서를 출력할 불출번호를 체크해주세요");
                stockOutNo = string.Empty;
                return;
            }
            else if (selcnt > 1)
            {
                MessageBox.Show("좌측 불출번호 중 하나만 체크해서 출력하세요.");
                stockOutNo = string.Empty;
                return;
            }
            else
            {
                SelectDetail();
                //MakeExcel();
                MessageBox.Show("불출 명세서 출력이 완료되었습니다.");
            }                     
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selcnt1 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                if (isChecked)
                {
                    selcnt1++;
                }
            }

            int selcnt2 = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                bool isChecked = Convert.ToBoolean(dataGridView2.Rows[i].Cells[0].Value);
                if (isChecked)
                {
                    selcnt2++;
                }
            }
            //////////////////////카운트 끝

            if (selcnt1 != 0 && selcnt2 != 0)
            //그리드1 체크된게 있고 그리드2에도 체크된게 있으면 
            {
                MessageBox.Show("불출 정보와 불출 상세정보 둘중 하나의 정보만 선택해주세요.");
                return;
            }
            else if (selcnt1 != 0 && selcnt2 == 0)
            //그리드1 체크된거 있고 그리드2 체크된거 없으면
            {
                if (MessageBox.Show("선택된 불출 정보를 삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DB.conn.Close();

                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.ConnectionString = DB.connectionString;
                    DB.conn.Open();
                    try
                    {
                        dataGridView1.EndEdit();
                        int selcnt = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                            if (isChecked)
                            {
                                stockOutNo = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                SelectDetail();
                                DB.conn = new SqlConnection(DB.connectionString);
                                DB.conn.ConnectionString = DB.connectionString;
                                DB.conn.Open();
                                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                                {
                                    try
                                    {

                                        DB.transaction = DB.conn.BeginTransaction();

                                        DB.sqlcmd = new SqlCommand("MM_STOCKOUT_MM_D_D1", DB.conn, DB.transaction);
                                        DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                        DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE", dataGridView1.Rows[i].Cells["회사코드"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", dataGridView1.Rows[i].Cells["공장코드"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE", dataGridView2.Rows[j].Cells["거래처코드"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@STOCKOUT_NO", dataGridView2.Rows[j].Cells["불출번호"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@OUT_REQ_NO", dataGridView2.Rows[j].Cells["불출요청번호"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@LOTNO", dataGridView2.Rows[j].Cells["LOT번호"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", dataGridView2.Rows[j].Cells["품목코드"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@STOCKOUT_QTY", Convert.ToInt32(dataGridView2.Rows[j].Cells["불출수량"].Value.ToString()));
                                        DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID", LoginID);

                                        DB.sqlcmd.ExecuteNonQuery();

                                        DB.transaction.Commit();
                                        DB.transaction.Dispose();
                                        DB.sqlcmd.Dispose();
                                    }
                                    catch (Exception ex)
                                    {
                                        DB.transaction.Rollback();
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                                try
                                {
                                    DB.transaction = DB.conn.BeginTransaction();

                                    DB.sqlcmd = new SqlCommand("MM_STOCKOUT_MM_H_D1", DB.conn, DB.transaction);
                                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                    DB.sqlcmd.Parameters.AddWithValue("@STOCKOUT_NO", dataGridView1.Rows[i].Cells["불출번호"].Value.ToString());

                                    DB.sqlcmd.ExecuteNonQuery();

                                    DB.transaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    DB.transaction.Rollback();
                                    MessageBox.Show(ex.Message);
                                }
                                selcnt++;
                            }                          
                        }
                        if (selcnt == 0)
                        {
                            MessageBox.Show("불출 정보를 선택해주세요.");
                            return;
                        }
                        MessageBox.Show("선택된 불출 정보가 삭제되었습니다.");
                        select();
                        SelectDetail();
                        dataGridView2.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        DB.conn.Dispose();
                        DB.conn.Close();
                        DB.sqlcmd.Dispose();
                    }
                }
            }
            else if (selcnt1 == 0 && selcnt2 != 0)
            //그리드1 체크된거 없고 그리드2 체크된거 있으면
            {
                if (MessageBox.Show("선택된 불출 상세정보를 삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DB.conn.Close();

                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.ConnectionString = DB.connectionString;
                    DB.conn.Open();
                    try
                    {
                        DataGridViewRow drrow1 = dataGridView1.SelectedRows[0];
                        stockOutNo = drrow1.Cells["불출번호"].Value.ToString();
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

                                    DB.sqlcmd = new SqlCommand("MM_STOCKOUT_MM_D_D1", DB.conn, DB.transaction);
                                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                    DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE", drrow1.Cells["회사코드"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", drrow1.Cells["공장코드"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE", dataGridView2.Rows[i].Cells["거래처코드"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@STOCKOUT_NO", dataGridView2.Rows[i].Cells["불출번호"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@OUT_REQ_NO", dataGridView2.Rows[i].Cells["불출요청번호"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@LOTNO", dataGridView2.Rows[i].Cells["LOT번호"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", dataGridView2.Rows[i].Cells["품목코드"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@STOCKOUT_QTY", Convert.ToInt32(dataGridView2.Rows[i].Cells["불출수량"].Value.ToString()));
                                    DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID", LoginID);

                                    DB.sqlcmd.ExecuteNonQuery();

                                    DB.transaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    DB.transaction.Rollback();
                                    MessageBox.Show(ex.Message);
                                }
                                selcnt++;
                            }                            
                        }
                        if (selcnt == 0)
                        {
                            MessageBox.Show("불출 상세 내역을 선택해주세요.");
                            return;
                        }
                        MessageBox.Show("선택된 불출 상세정보가 삭제되었습니다.");
                        Select();
                        SelectDetail();                      
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        DB.conn.Dispose();
                        DB.conn.Close();
                        DB.sqlcmd.Dispose();
                    }
                }
            }
            else
            {
                MessageBox.Show("선택된 항목이없습니다.");
                return;
            }
            dataGridView1.Refresh();
            dataGridView2.Refresh();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            stockOutNo = row.Cells["불출번호"].Value.ToString();

            SelectDetail();
        }
        #region<데이터 조회 메서드>
        private void select()
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
                DB.sqlcmd = new SqlCommand("MM_STOCKOUT_MM_H_S2", DB.conn, DB.transaction);
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
                DB.sqlcmd.Parameters.AddWithValue("@STOCKOUT_NO", (txtStockOutNo.Text.Length == 0) ? "" : txtStockOutNo.Text);
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
                DB.adapter.Fill(ds, "MM_STOCKOUT_MM_H_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MM_STOCKOUT_MM_H_S2";
                
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;

                DB.transaction.Commit();
                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
                dataGridView1.Refresh();
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
        private void SelectDetail()
        {          
            try
            {
                DB.conn.Close();
                dataGridView2.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("MM_STOCKOUT_MM_D_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@STOCKOUT_NO", stockOutNo);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView2.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "MM_STOCKOUT_MM_D_S2");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "MM_STOCKOUT_MM_D_S2";
                
                for (int i = 1; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                dataGridView2.Columns[0].Width = 50;
                dataGridView2.Columns[2].Width = 130;
                dataGridView2.Columns[7].Width = 130;

                DB.transaction.Commit();
                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                dataGridView2.Refresh();
            }
            catch (Exception ex)
            {
                DB.transaction.Rollback();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //DB.sqlDR.Close();
                DB.conn.Dispose();
                DB.conn.Close();
            }
        }
        #endregion
        #region<콤보박스 데이터 바인딩>
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
                cboProcessCode.ValueMember   = "LWRCODE";

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
                cboPlantCode.Text = string.Empty;
                cboPlantCode.Enabled = false;
            }
        }
        #endregion
        //#region<엑셀 출력>
        //private void MakeExcel()
        //{
        //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //    {
        //        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
        //        if (isChecked)
        //        {
        //            Excel.Application xlApp = null;
        //            Excel.Workbook xlBook = null;
        //            Excel.Worksheet xlSheet = null;
        //            try
        //            {
        //                string FileName = @"C:\요청서\(진)불출서.xlsx";
        //                string FullPath = Path.GetFullPath(FileName);
        //
        //                xlApp = new Excel.Application();
        //                xlBook = xlApp.Workbooks.Open(FullPath);
        //                xlSheet = xlBook.Worksheets["Sheet1"];
        //
        //                // 데이터 입력  [row, colunm] 품목 위쪽 입력부 고정항목.
        //                xlSheet.Cells[4, 3].value = stockOutNo;
        //                xlSheet.Cells[5, 3].value = "하이에어 공조";
        //                xlSheet.Cells[6, 3].value = "055-123-4567";
        //                xlSheet.Cells[7, 3].value = "055-123-4568";
        //                xlSheet.Cells[9, 4].value = "판금공장";
        //                xlSheet.Cells[9, 11].value = dataGridView1.Rows[i].Cells["거래처명"].Value.ToString();
        //                xlSheet.Cells[10, 11].value = "055-123-4567";
        //                xlSheet.Cells[10, 4].value = stockOutNo;
        //                xlSheet.Cells[11, 4].value = dataGridView1.Rows[i].Cells["불출자"].Value.ToString();
        //                xlSheet.Cells[12, 4].value = dataGridView1.Rows[i].Cells["불출일자"].Value.ToString();
        //                xlSheet.Cells[13, 4].value = dataGridView1.Rows[i].Cells["불출총액"].Value.ToString();
        //
        //                // 불출 상세 품목 입력
        //
        //                for (int j = 0; j < dataGridView2.Rows.Count; j++)
        //                {
        //                    xlSheet.Cells[15 + j, 1].value = j + 1;
        //                    xlSheet.Cells[15 + j, 2].value = dataGridView2.Rows[j].Cells["품목명"].Value.ToString();
        //                    xlSheet.Cells[15 + j, 5].value = dataGridView2.Rows[j].Cells["품목코드"].Value.ToString();
        //                    xlSheet.Cells[15 + j, 11].value = dataGridView2.Rows[j].Cells["단가"].Value.ToString();
        //                    xlSheet.Cells[15 + j, 13].value = dataGridView2.Rows[j].Cells["총 가격"].Value.ToString();
        //                }
        //
        //
        //                string NewFileName = $@"C:\요청서\(진)불출명세서_{stockOutNo}.xlsx";
        //                string NewFullPath = Path.GetDirectoryName(FullPath);
        //                xlBook.SaveAs(Path.Combine(NewFullPath, NewFileName));
        //
        //                xlBook.Close();
        //                xlApp.Quit();
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }
        //            finally
        //            {
        //                ReleaseExcelObject(xlSheet);
        //                ReleaseExcelObject(xlBook);
        //                ReleaseExcelObject(xlApp);
        //            }
        //        }
        //    }
        //}
        //private static void ReleaseExcelObject(object obj)
        //{
        //    try
        //    {
        //        if (obj != null)
        //        {
        //            Marshal.ReleaseComObject(obj);
        //            obj = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj = null;
        //        throw ex;
        //    }
        //    finally
        //    {
        //        GC.Collect();
        //    }
        //}
        //#endregion

        private void txtStockOutNo_TextChanged(object sender, EventArgs e)
        {
            txtStockOutNo.SelectionStart = txtStockOutNo.Text.Length;
        }

        private void txtStockOutNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDoInquire_Click(sender, e);
                btnDoInquire.Select();
            }
        }
    }
}
