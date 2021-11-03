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
    public partial class MM_MTRL_Order_MM : Form
    {
        static public string LoginID;
        string Ordno = null;
        public MM_MTRL_Order_MM()
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
            TabControl tc = (TabControl)sender;
            MainScreen.tabindex = tc.SelectedIndex;

            TabPage TabP = (TabPage)tc.TabPages[tc.SelectedIndex];
            tc.TabPages.Remove(TabP);



            //MessageBox.Show(TabP.Text);
            int index = Common.DICT_REMOVE_INDEX[TabP.Text];
            Common.DICT_REMOVE_INDEX.Remove(TabP.Text);

            //탭페이지를 앞으로 한칸씩땡긴다.
            for (int i = index; i < Common.DICT_REMOVE_INDEX.Count; i++)
            {
                string tempstring = Common.DICT_REMOVE_INDEX.FirstOrDefault(x => x.Value == i + 1).Key;
                int tempint = Common.DICT_REMOVE_INDEX[tempstring];
                Common.DICT_REMOVE_INDEX.Remove(tempstring);
                Common.DICT_REMOVE_INDEX.Add(tempstring, tempint - 1);
            }
        }
        
        private void MTRL_Order_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DB.conn.Close();
                dataGridView1.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("MM_MTRL_Order_MM_H_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@ORDERNO",       txtSearch_OrderNo.Text);
                DB.sqlcmd.Parameters.AddWithValue("@CUSTNAME",      txtSearch_CustName.Text);
                DB.sqlcmd.Parameters.AddWithValue("@STARTDATE",     StartDate.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ENDDATE",       EndDate.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                
                DB.adapter.Fill(ds, "MM_MTRL_Order_MM_H_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MM_MTRL_Order_MM_H_S1";
                dataGridView1.Columns[0].Width = 50;
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }

                dataGridView2.Columns.Clear();
                ds.Clear();
                DB.transaction.Commit();
                DB.sqlcmd.Dispose();
                DB.adapter.Dispose();

                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("MM_MTRL_Order_MM_D_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@ORDERNO", txtSearch_OrderNo.Text);
                DB.sqlcmd.Parameters.AddWithValue("@CUSTNAME", txtSearch_CustName.Text);

                DB.sqlcmd.ExecuteNonQuery();

                var chkCol2 = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView2.Columns.Add(chkCol2);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DB.adapter.Fill(ds, "MM_MTRL_Order_D_S1");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "MM_MTRL_Order_D_S1";
                dataGridView2.Columns[2].Width = 120;
                dataGridView2.Columns[0].Width = 50;
                for (int i = 0 ; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                

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
            MM_MTRL_Order_POPUP Order_POPUP = new MM_MTRL_Order_POPUP();
            Order_POPUP.ShowDialog();
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
                    Ordno = dataGridView1.Rows[i].Cells["발주번호"].Value.ToString();
                    selcnt++;
                }
            }
            if (selcnt == 0)
            {
                MessageBox.Show("발주서를 출력할 발주번호를 체크해주세요");
                Ordno = string.Empty;
                return;
            }
            else if (selcnt > 1)
            {
                MessageBox.Show("좌측 발주번호 중 하나만 체크해서 출력하세요.");
                Ordno = string.Empty;
                return;
            }
            else
            {
                SelectDetail();
                MakeExcel();
                MessageBox.Show("발주서 출력이 완료되었습니다.");
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
                MessageBox.Show("발주 정보와 발주 상세정보 둘중 하나의 정보만 삭제할 수 있습니다.");
                return;
            }
            else if (selcnt1 != 0 && selcnt2 == 0)
            //그리드1 체크된거 있고 그리드2 체크된거 없으면
            {
                if (MessageBox.Show("선택된 발주 정보를 삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                                Ordno = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                SelectDetail();
                                DB.conn = new SqlConnection(DB.connectionString);
                                DB.conn.ConnectionString = DB.connectionString;
                                DB.conn.Open();
                                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                                {
                                    try
                                    {

                                        DB.transaction = DB.conn.BeginTransaction();

                                        DB.sqlcmd = new SqlCommand("MM_MTRL_Order_MM_D_D1", DB.conn, DB.transaction);
                                        DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                        DB.sqlcmd.Parameters.AddWithValue("@ORDERNO", dataGridView2.Rows[j].Cells["발주번호"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME", dataGridView2.Rows[j].Cells["거래처명"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", dataGridView2.Rows[j].Cells["구매요청번호"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", dataGridView2.Rows[j].Cells["품목코드"].Value.ToString());

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

                                    DB.sqlcmd = new SqlCommand("MM_MTRL_Order_MM_H_D1", DB.conn, DB.transaction);
                                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                    DB.sqlcmd.Parameters.AddWithValue("@ORDERNO", dataGridView1.Rows[i].Cells["발주번호"].Value.ToString());

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
                        MessageBox.Show("선택된 발주 정보가 삭제되었습니다.");
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
                if (MessageBox.Show("선택된 발주 상세정보를 삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DB.conn.Close();

                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.ConnectionString = DB.connectionString;
                    DB.conn.Open();
                    try
                    {
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

                                    DB.sqlcmd = new SqlCommand("MM_MTRL_Order_MM_D_D1", DB.conn, DB.transaction);
                                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                    DB.sqlcmd.Parameters.AddWithValue("@ORDERNO", dataGridView2.Rows[i].Cells["발주번호"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME", dataGridView2.Rows[i].Cells["거래처명"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", dataGridView2.Rows[i].Cells["구매요청번호"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", dataGridView2.Rows[i].Cells["품목코드"].Value.ToString());

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
                        MessageBox.Show("선택된 발주 상세정보가 삭제되었습니다.");
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
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                Ordno = row.Cells["발주번호"].Value.ToString();

                SelectDetail();
            }
            catch (ArgumentOutOfRangeException)
            {

            }          
        }
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
                DB.sqlcmd = new SqlCommand("MM_MTRL_Order_MM_H_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@ORDERNO", txtSearch_OrderNo.Text);
                DB.sqlcmd.Parameters.AddWithValue("@CUSTNAME", txtSearch_CustName.Text);
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
                DB.adapter.Fill(ds, "MM_MTRL_Order_MM_H_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MM_MTRL_Order_MM_H_S2";
                dataGridView1.Columns[0].Width = 50;
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                    if (i + 7 < dataGridView1.Columns.Count)
                    {
                        dataGridView1.Columns[i+7].Visible = false;
                    }
                }
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
                DB.sqlcmd = new SqlCommand("MM_MTRL_Order_MM_D_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@ORDERNO", Ordno);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView2.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "MM_MTRL_Order_MM_D_S2");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "MM_MTRL_Order_MM_D_S2";
                dataGridView2.Columns[0].Width = 50;
                for (int i = 1; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                dataGridView2.Columns["기본단위"].Visible = false;
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
        private void MakeExcel()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                if (isChecked)
                {
                    Excel.Application xlApp = null;
                    Excel.Workbook xlBook = null;
                    Excel.Worksheet xlSheet = null;
                    try
                    {
                        string FileName = @"C:\요청서\(진)발주서.xlsx";
                        string FullPath = Path.GetFullPath(FileName);

                        xlApp = new Excel.Application();
                        xlBook = xlApp.Workbooks.Open(FullPath);                       
                        xlSheet = xlBook.Worksheets["Sheet1"];
                        xlApp.Visible = true;

                        // 데이터 입력  [row, colunm] 품목 위쪽 입력부 고정항목.
                        xlSheet.Cells[4, 3].value = Ordno;
                        xlSheet.Cells[5, 3].value = dataGridView1.Rows[i].Cells["회사"].Value.ToString();
                        xlSheet.Cells[6, 3].value = dataGridView1.Rows[i].Cells["회사전화"].Value.ToString();
                        xlSheet.Cells[7, 3].value = dataGridView1.Rows[i].Cells["회사팩스"].Value.ToString();
                        xlSheet.Cells[9, 4].value = dataGridView1.Rows[i].Cells["공장"].Value.ToString();
                        xlSheet.Cells[9, 11].value = dataGridView1.Rows[i].Cells["거래처명"].Value.ToString();
                        xlSheet.Cells[10, 11].value = dataGridView1.Rows[i].Cells["거래처전화"].Value.ToString(); ;
                        xlSheet.Cells[10, 4].value = Ordno;
                        xlSheet.Cells[11, 4].value = dataGridView1.Rows[i].Cells["발주자"].Value.ToString();
                        xlSheet.Cells[12, 4].value = dataGridView1.Rows[i].Cells["발주일자"].Value.ToString();
                        xlSheet.Cells[13, 4].value = dataGridView1.Rows[i].Cells["발주총액"].Value.ToString();

                        // 발주 상세 품목 입력

                        for (int j = 0; j < dataGridView2.Rows.Count; j++)
                        {
                            xlSheet.Cells[15 + j, 1].value = j + 1;
                            xlSheet.Cells[15 + j, 2].value = dataGridView2.Rows[j].Cells["품목명"].Value.ToString();
                            xlSheet.Cells[15 + j, 5].value = dataGridView2.Rows[j].Cells["품목코드"].Value.ToString();
                            xlSheet.Cells[15 + j, 6].value = "2022-01-15";
                            xlSheet.Cells[15 + j, 8].value = dataGridView2.Rows[j].Cells["기본단위"].Value.ToString();
                            xlSheet.Cells[15 + j, 9].value = dataGridView2.Rows[j].Cells["발주수량"].Value.ToString();
                            xlSheet.Cells[15 + j, 11].value = dataGridView2.Rows[j].Cells["단가"].Value.ToString();
                            xlSheet.Cells[15 + j, 13].value = dataGridView2.Rows[j].Cells["총 가격"].Value.ToString();
                        }


                        string NewFileName = $@"C:\요청서\(진)발주서_{Ordno}.xlsx";
                        string NewFullPath = Path.GetDirectoryName(FullPath);
                        xlBook.SaveAs(Path.Combine(NewFullPath, NewFileName));
                            
                        //xlBook.Close(true);
                        //xlApp.Quit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        ReleaseExcelObject(xlSheet);
                        ReleaseExcelObject(xlBook);
                        ReleaseExcelObject(xlApp);
                    }
                }
            }
        }
        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
