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

namespace WindowsFormsApp1
{
    public partial class MTRL_Request : Form
    {
        static public string LoginID;

        string reqno = null;
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
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            reqno = row.Cells["요청 번호"].Value.ToString();

            SelectDetail();
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
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;
            try
            {
                excelApp = new Excel.Application();

                wb = excelApp.Workbooks.Open(@"C:\요청서\(진)요청서.xlsx");
                // 엑셀파일을 엽니다.
                // ExcelPath 대신 문자열도 가능합니다
                // 예. Open(@"D:\test\test.xlsx");

                ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;
                // 첫번째 Worksheet를 선택합니다.

                Excel.Range rng = ws.Range[ws.Cells[1, 1], ws.Cells[36, 8]];
                // 해당 Worksheet에서 저장할 범위를 정합니다.
                // 지금은 저장할 행렬의 크기만큼 지정합니다.
                // 다른 예시 Excel.Range rng = ws.Range["B2", "G8"];

                object[,] data = new object[36, 8];
                // 저장할 때 사용할 object 행렬
                //int[] ERequestNohH = new int[](4,3);
            
                string RequestNo = "";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                    if (isChecked)
                    {
                        RequestNo = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        break;
                    }
                }
                try
                {
                    DB.conn.Close();
                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.Open();

                    //트랜잭션 시작
                    DB.transaction = DB.conn.BeginTransaction();
                    DB.sqlcmd = new SqlCommand("MTRL_Request_Excel_H1", DB.conn, DB.transaction);
                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                    DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", RequestNo);

                    DB.sqlcmd.ExecuteNonQuery();

                    DB.sqlDR = DB.sqlcmd.ExecuteReader();
                    while (DB.sqlDR.Read())
                    {
                        data[4, 3] = DB.sqlDR["요청 번호"].ToString();
                        data[5, 3] = DB.sqlDR["회사 명"].ToString();
                        data[6, 3] = DB.sqlDR["전화"].ToString();
                        data[7, 3] = DB.sqlDR["팩스"].ToString();
                        data[9, 4] = DB.sqlDR["현장"].ToString();
                        data[10, 4] = DB.sqlDR["요청 번호"].ToString();
                        data[11, 4] = DB.sqlDR["요청자"].ToString();
                        data[12, 4] = DB.sqlDR["요청일자"].ToString();
                    }
                    
                    DB.conn.Close();
                    DB.sqlcmd.Dispose();
                    DB.sqlDR.Close();

                }
                catch (Exception)
                {
                    DB.transaction.Rollback();
                    throw;
                }
                finally
                {
                    DB.sqlDR.Close();
                    DB.conn.Close();
                }
                try
                {
                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.Open();

                    //트랜잭션 시작
                    DB.transaction = DB.conn.BeginTransaction();
                    DB.sqlcmd = new SqlCommand("MTRL_Request_Excel_D1", DB.conn, DB.transaction);
                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                    DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO", RequestNo);

                    DB.sqlcmd.ExecuteNonQuery();

                    DB.sqlDR = DB.sqlcmd.ExecuteReader();
                    while (DB.sqlDR.Read())
                    {
                        int i = 14; 
                        
                        data[i, 1] = DB.sqlDR["No"].ToString();
                        data[i, 2] = DB.sqlDR["거래처"].ToString();
                        data[i, 3] = DB.sqlDR["품명"].ToString();
                        data[i, 4] = DB.sqlDR["품번"].ToString();
                        data[i, 6] = DB.sqlDR["단위"].ToString();
                        data[i, 7] = DB.sqlDR["수량"].ToString();
                        data[i, 8] = (DB.sqlDR["비고"] == null)?"": DB.sqlDR["비고"].ToString();
                        i++;
                    }
                    
                    DB.conn.Close();
                    DB.sqlcmd.Dispose();
                    DB.sqlDR.Close();

                }
                catch (Exception)
                {
                    DB.transaction.Rollback();
                    throw;
                }
                finally
                {
                    DB.sqlDR.Close();
                    DB.conn.Close();
                }

                rng.Value = data;
                // data를 불러온 엑셀파일에 적용시킵니다. 아직 완료 X
                string path = @"C:\요청서\(진)요청서.xlsx";
                if (path != null)
                {
                    // path는 새로 저장될 엑셀파일의 경로입니다.
                    // 따로 지정해준다면, "다른이름으로 저장" 의 역할을 합니다.
                    // 상대경로도 가능합니다. (예. "secondExcel.xlsx")
                    wb.SaveCopyAs(@"C:\요청서\요청서" + RequestNo + ".xlsx");
                }

                else
                {
                    // 따로 저장하지 않는다면 지금 파일에 그대로 저장합니다.
                    wb.Save();
                }


                wb.Close(false);
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
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
