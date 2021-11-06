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
    public partial class PW_ORDER_POPUP : Form
    {
        static public string LoginID;
        static public string Custcode = "";
        static public string Custname = "";

        public PW_ORDER_POPUP()
        {
            InitializeComponent();
            StartDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), 1);
            EndDate.Value   = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")), int.Parse(DateTime.Now.ToString("MM")), int.Parse(DateTime.Now.ToString("dd")));
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
                DB.sqlcmd = new SqlCommand("PW_ORDER_POPUP_S1", DB.conn, DB.transaction);
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
                DB.adapter.Fill(ds, "PW_ORDER_POPUP_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "PW_ORDER_POPUP_S1";

                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].ReadOnly = false;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 90;
                dataGridView1.Columns[5].Width = 80;
                dataGridView1.Columns[9].Width = 80;
                dataGridView1.Columns[10].Width = 80;
                dataGridView1.Columns[12].Width = 80;
                dataGridView1.Columns[13].Width = 50;
                dataGridView1.Columns[14].Width = 80;


                // 두번쨰 그리드
                

                for (int i = 1; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }

                dataGridView2.ColumnCount = 13;
                dataGridView2.Columns[0].Name = "발주번호";
                dataGridView2.Columns[1].Name = "요청번호"; 
                dataGridView2.Columns[2].Name = "회사코드";
                dataGridView2.Columns[3].Name = "공장코드";
                dataGridView2.Columns[4].Name = "거래처코드";
                dataGridView2.Columns[5].Name = "거래처";
                dataGridView2.Columns[6].Name = "품목코드";
                dataGridView2.Columns[7].Name = "품명";
                dataGridView2.Columns[8].Name = "품목유형";
                dataGridView2.Columns[9].Name = "잔여 발주수량";
                dataGridView2.Columns[10].Name = "입고수량";
                dataGridView2.Columns[10].ReadOnly = false;
                dataGridView2.Columns[11].Name = "기본단위";
                dataGridView2.Columns[12].Name = "단위중량";

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

        private void CustTxtBox_MouseClick(object sender, MouseEventArgs e)
        {
            Cust_POPUP cust_POPUP = new Cust_POPUP();
            cust_POPUP.ShowDialog();
            CustTxtBox.Text = Custname;
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
                DB.sqlcmd = new SqlCommand("PW_ORDER_POPUP_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@MTRL_ORDERNO",  OrderNoTxtBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME",     CustTxtBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@STARTDATE",     StartDate.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ENDDATE",        EndDate.Text);

                DB.sqlcmd.ExecuteNonQuery();

                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);

                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "PW_ORDER_POPUP_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "PW_ORDER_POPUP_S2";

                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 80;
                dataGridView1.Columns[3].Width = 80;
                dataGridView1.Columns[4].Width = 90;
                dataGridView1.Columns[5].Width = 80;
                dataGridView1.Columns[9].Width = 80;
                dataGridView1.Columns[10].Width = 80;
                dataGridView1.Columns[12].Width = 80;
                dataGridView1.Columns[13].Width = 50;
                dataGridView1.Columns[14].Width = 80;

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
            if (MessageBox.Show("입고할 발주품목을 추가하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                try
                {
                    dataGridView2.Rows.Clear();
                    
                    int delcnt = 0;
                    string check_cust1 = "";
                    string check_cust2 = "";


                    for (int i = 0; i < dataGridView1.Rows.Count; i++) // 기준 거래처코드 가져옴
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                    
                        if (isChecked)
                        {
                            check_cust1 = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            break;
                        }
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);

                        if (isChecked)
                        {
                            check_cust2 = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            if (check_cust1 == check_cust2)
                            {
                                try
                                {
                                    dataGridView2.Rows.Add(
                                        dataGridView1.Rows[i].Cells[1].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[2].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[3].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[4].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[5].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[6].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[7].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[8].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[9].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[12].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[12].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[13].Value.ToString(),
                                        dataGridView1.Rows[i].Cells[14].Value.ToString()
                                        );
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                delcnt++;
                            }
                            else
                            {
                                dataGridView2.Rows.Clear();
                                MessageBox.Show("회사, 공장, 거래처가 같은 품목만 동시에 불출 요청할 수 있습니다.\n불출요청이 취소되었습니다.");
                                break;
                            }
                        }
                    }
                    if (delcnt == 0)
                    {
                        MessageBox.Show("입고할 발주품목을 선택해 주세요.");
                    }
                    
                    
                    for (int i = 1; i < dataGridView2.Columns.Count; i++)
                    {
                        dataGridView2.Columns[i].ReadOnly = true;
                    }
                    dataGridView2.Columns[10].ReadOnly = false;
                    
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
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("저장하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DB.conn.Close();

                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.ConnectionString = DB.connectionString;
                    DB.conn.Open();

                    dataGridView2.EndEdit();

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                            DB.transaction = DB.conn.BeginTransaction();

                            DB.sqlcmd = new SqlCommand("PW_Purchase_Warehousing_H_I1", DB.conn, DB.transaction);
                            DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                            DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE", dataGridView2.Rows[i].Cells["회사코드"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE", dataGridView2.Rows[i].Cells["거래처코드"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID",  LoginID);

                            DB.sqlcmd.ExecuteNonQuery();

                            DB.transaction.Commit();

                            break;
                    }
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
                    dataGridView2.EndEdit();

                    int selcnt = 0;

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        try
                        {
                            DB.conn.Close();

                            DB.conn = new SqlConnection(DB.connectionString);
                            DB.conn.ConnectionString = DB.connectionString;
                            DB.conn.Open();

                            DB.transaction = DB.conn.BeginTransaction();
                            DB.sqlcmd = new SqlCommand("PW_Purchase_Warehousing_D_I1", DB.conn, DB.transaction);
                            DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                            DB.sqlcmd.Parameters.AddWithValue("@MTRL_ORDERNO",	    dataGridView2.Rows[i].Cells["발주번호"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO",		    dataGridView2.Rows[i].Cells["요청번호"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE",		    dataGridView2.Rows[i].Cells["회사코드"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE",	    dataGridView2.Rows[i].Cells["공장코드"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE",		    dataGridView2.Rows[i].Cells["거래처코드"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE",		    dataGridView2.Rows[i].Cells["품목코드"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME",	        dataGridView2.Rows[i].Cells["품명"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_TYPE",		    dataGridView2.Rows[i].Cells["품목유형"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@INQTY",			    dataGridView2.Rows[i].Cells["입고수량"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@BASEUNIT",		    dataGridView2.Rows[i].Cells["기본단위"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@UNITWGT",           dataGridView2.Rows[i].Cells["단위중량"].Value.ToString());
                            DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID",		    LoginID);
                           
                            DB.sqlcmd.ExecuteNonQuery();

                            DB.transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            DB.transaction.Rollback();
                            MessageBox.Show(ex.Message);
                            MessageBox.Show("저장이 취소되었습니다.");
                            break;
                        }

                        selcnt++;

                    }
                    if (selcnt == 0)
                    {
                        MessageBox.Show("저장할 입고항목을 선택해 주세요.");
                    }
                    else
                    {
                        if (MessageBox.Show("저장되었습니다.") == DialogResult.OK)
                        {
                            this.Close();
                        }
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
                }
            }
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    if (dataGridView2.Rows.Count == 0)
                    {
                        MessageBox.Show("등록할 정보가 없습니다.");
                    }
                    else
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
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void ScanBtn_Click(object sender, EventArgs e)
        {
            Scan_QRCode scan_QRCode = new Scan_QRCode();
            scan_QRCode.ShowDialog();
            QRTxtBox.Text = Scan_QRCode.Qrcode;


        }

        private void QRTxtBox_TextChanged(object sender, EventArgs e)
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
                DB.sqlcmd = new SqlCommand("M_Input_D_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@QRCODE", QRTxtBox.Text);

                DB.sqlcmd.ExecuteNonQuery();

                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "M_Input_D_S1");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "M_Input_D_S1";

                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                

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
    }
}
