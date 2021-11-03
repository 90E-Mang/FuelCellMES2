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
    public partial class MTRL_Request : Form
    {
        static public string LoginID;

        string reqno = null;
        string compnm = null;
        string plantnm = null;
        string comptel = null;
        string compfax = null;
        string maker = null;
        string makedate = null;
        public MTRL_Request()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            StartDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")),1);
            EndDate.Value   = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), int.Parse(DateTime.Now.ToString("dd")));

        }
        public delegate void FormClosed(string str);
        public event FormClosed FormCloseEvent;
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.FormCloseEvent("구매 요청");
            this.Close();

        }
        private void selectHeader()
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
                DB.sqlcmd = new SqlCommand("MTRL_Request_H_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", ReqNoTxtBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@STARTDATE", StartDate.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ENDDATE",   EndDate.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "MTRL_Request_H_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MTRL_Request_H_S1";
                dataGridView1.Columns[0].Width = 50;
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns["전화"].Visible = false;
                dataGridView1.Columns["팩스"].Visible = false;
                dataGridView1.Columns["요청자"].Visible = false;
                dataGridView1.Columns["요청일자"].Visible = false;

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
        private void MTRL_Request_Load(object sender, EventArgs e)
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
                DB.sqlcmd = new SqlCommand("MTRL_Request_H_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", ReqNoTxtBox.Text);
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
                DB.adapter.Fill(ds, "MTRL_Request_H_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "MTRL_Request_H_S2";
                dataGridView1.Columns[0].Width = 50;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns["전화"].Visible = false;
                dataGridView1.Columns["팩스"].Visible = false;
                dataGridView1.Columns["요청자"].Visible = false;
                dataGridView1.Columns["요청일자"].Visible = false;

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

            try
            {
                DB.conn.Close();
                dataGridView2.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("MTRL_Request_D_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView2.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "MTRL_Request_D_S2");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "MTRL_Request_D_S2";
                dataGridView2.Columns[0].Width = 50;
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                dataGridView2.Columns[0].ReadOnly = false;
                dataGridView2.Columns[5].ReadOnly = false;

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

        private void SelButton_Click(object sender, EventArgs e)
        {
            selectHeader();
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            ITEM_POPUP_REQUEST iTEM_POPUP = new ITEM_POPUP_REQUEST();
            iTEM_POPUP.ShowDialog();
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
                DB.sqlcmd = new SqlCommand("MTRL_Request_D_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", reqno);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView2.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "MTRL_Request_D_S1");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "MTRL_Request_D_S1";
                dataGridView2.Columns[0].Width = 50;
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                dataGridView2.Columns[0].ReadOnly = false;
                dataGridView2.Columns[5].ReadOnly = false;

                DB.transaction.Commit();

            }
            catch (IndexOutOfRangeException)
            {
            }
            catch (Exception ex)
            {
                DB.transaction.Rollback();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DB.sqlDR.Close();
                DB.conn.Close();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                reqno = row.Cells["요청 번호"].Value.ToString();

                SelectDetail();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        //수정 버튼
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택된 요청 상세정보를 수정하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                                DB.sqlcmd = new SqlCommand("MTRL_Request_D_U1", DB.conn, DB.transaction);
                                DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO",     dataGridView2.Rows[i].Cells["요청 번호"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@SEQ",           dataGridView2.Rows[i].Cells["SEQ"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@REQQTY",        dataGridView2.Rows[i].Cells["요청 수량"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@ORDERQTY",      dataGridView2.Rows[i].Cells["발주 수량"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@UNITCOST",      dataGridView2.Rows[i].Cells["단가"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID",      LoginID);

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
                        MessageBox.Show("수정할 요청 상세정보를 선택해 주세요.");
                    }
                    else MessageBox.Show("선택된 요청 상세정보가 수정되었습니다.");
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

        private void DelButton_Click(object sender, EventArgs e)
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
                MessageBox.Show("요청 정보와 요청 상세정보 둘중 하나의 정보만 삭제할 수 있습니다.");
            }
            else if (selcnt1 != 0 && selcnt2 == 0)
            //그리드1 체크된거 있고 그리드2 체크된거 없으면
            {
                if (MessageBox.Show("선택된 요청 정보를 삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                                try
                                {
                                    DB.transaction = DB.conn.BeginTransaction();

                                    DB.sqlcmd = new SqlCommand("MTRL_Request_H_D1", DB.conn, DB.transaction);
                                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                    DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", dataGridView1.Rows[i].Cells["요청 번호"].Value.ToString());

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
                        MessageBox.Show("선택된 요청 정보가 삭제되었습니다.");
                        selectHeader();
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
                if (MessageBox.Show("선택된 요청 상세정보를 삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                                    DB.sqlcmd = new SqlCommand("MTRL_Request_D_D1", DB.conn, DB.transaction);
                                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                    DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", dataGridView2.Rows[i].Cells["요청 번호"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@SEQ",       dataGridView2.Rows[i].Cells["SEQ"].Value.ToString());

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
                        MessageBox.Show("선택된 요청 상세정보가 삭제되었습니다.");
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selcnt = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);

                if (isChecked)
                {
                    reqno       = dataGridView1.Rows[i].Cells["요청 번호"].Value.ToString();
                    compnm      = dataGridView1.Rows[i].Cells["회사 명"].Value.ToString();
                    plantnm     = dataGridView1.Rows[i].Cells["공장"].Value.ToString();
                    comptel     = dataGridView1.Rows[i].Cells["전화"].Value.ToString();
                    compfax     = dataGridView1.Rows[i].Cells["팩스"].Value.ToString();
                    maker       = dataGridView1.Rows[i].Cells["요청자"].Value.ToString();
                    makedate    = dataGridView1.Rows[i].Cells["요청일자"].Value.ToString();

                    selcnt++;
                    SelectDetail();
                    MakeExcel();
                }
            }
            if (selcnt == 0)
            {
                MessageBox.Show("요청서를 출력할 요청번호를 체크해주세요");
                reqno = string.Empty;
                return;
            }
            else 
            {
                MessageBox.Show("요청서 출력이 완료되었습니다.");
            }
        }
        private void MakeExcel()
        {
            
            Excel.Application xlApp = null;
            Excel.Workbook xlBook = null;
            Excel.Worksheet xlSheet = null;
            try
            {
                string FileName = @"C:\요청서\(진)요청서.xlsx";
                string FullPath = Path.GetFullPath(FileName);

                xlApp = new Excel.Application();
                xlBook = xlApp.Workbooks.Open(FullPath);
                xlSheet = xlBook.Worksheets["Sheet1"];

                // 데이터 입력  [row, colunm] 품목 위쪽 입력부 고정항목.
                xlSheet.Cells[4, 3].value = reqno;
                xlSheet.Cells[5, 3].value = compnm;
                xlSheet.Cells[6, 3].value = comptel;
                xlSheet.Cells[7, 3].value = compfax;
                xlSheet.Cells[9, 4].value = plantnm;
                xlSheet.Cells[10, 4].value = reqno;
                xlSheet.Cells[11, 4].value = maker;
                xlSheet.Cells[12, 4].value = makedate;
                // 발주 상세 품목 입력

                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    xlSheet.Cells[14 + j, 1].value = j + 1;
                    xlSheet.Cells[14 + j, 2].value = dataGridView2.Rows[j].Cells["품명"].Value.ToString();
                    xlSheet.Cells[14 + j, 5].value = dataGridView2.Rows[j].Cells["품목코드"].Value.ToString();
                    xlSheet.Cells[14 + j, 7].value = "2022-01-15";
                    xlSheet.Cells[14 + j, 8].value = dataGridView2.Rows[j].Cells["기본 단위"].Value.ToString();
                    xlSheet.Cells[14 + j, 11].value = dataGridView2.Rows[j].Cells["요청 수량"].Value.ToString();
                }

                string NewFileName = $@"C:\요청서\(진)요청서_{reqno}.xlsx";
                string NewFullPath = Path.GetDirectoryName(FullPath);
                xlBook.SaveAs(Path.Combine(NewFullPath, NewFileName));

                xlBook.Close();
                xlApp.Quit();
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
