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
    public partial class PW_Purchase_Warehousing : Form
    {
        static public string LoginID;
        static public string Custcode = "";
        static public string Custname = "";
        string IN_No = null;
        public PW_Purchase_Warehousing()
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
                DB.sqlcmd = new SqlCommand("PW_Purchase_Warehousing_H_S3", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@INPUT_NO",  InputTxtBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE", Custcode);
                DB.sqlcmd.Parameters.AddWithValue("@STARTDATE", StartDate.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ENDDATE",   EndDate.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "취소"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "PW_Purchase_Warehousing_H_S3");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "PW_Purchase_Warehousing_H_S3";
                dataGridView1.Columns[0].Width = 50;
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].ReadOnly = false;

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
                DB.sqlcmd = new SqlCommand("PW_Purchase_Warehousing_D_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@INPUT_NO", IN_No);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView2.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "PW_Purchase_Warehousing_D_S2");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "PW_Purchase_Warehousing_D_S2";
                dataGridView2.Columns[0].Width = 50;
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                dataGridView2.Columns[0].ReadOnly = false;
                dataGridView2.Columns[10].ReadOnly = false;
                dataGridView2.Columns[6].Visible = false;
                dataGridView2.Columns[11].Width = 60;
                dataGridView2.Columns[13].Width = 80;
                dataGridView2.Columns[15].Width = 80;


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
        private void load()
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
                DB.sqlcmd = new SqlCommand("PW_Purchase_Warehousing_H_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "취소"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "PW_Purchase_Warehousing_H_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "PW_Purchase_Warehousing_H_S1";
                dataGridView1.Columns[0].Width = 50;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].ReadOnly = false;
                

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
                DB.sqlcmd = new SqlCommand("PW_Purchase_Warehousing_D_S1", DB.conn, DB.transaction);
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
                DB.adapter.Fill(ds, "PW_Purchase_Warehousing_D_S1");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "PW_Purchase_Warehousing_D_S1";
                dataGridView2.Columns[0].Width = 50;
                for (int i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }
                dataGridView2.Columns[0].ReadOnly = false;
                dataGridView2.Columns[11].ReadOnly = false;

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
        private void PW_Purchase_Warehousing_Load(object sender, EventArgs e)
        {
            load();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            IN_No = row.Cells["입고번호"].Value.ToString();

            SelectDetail();
        }

        private void CustNameTxtBox_MouseClick(object sender, MouseEventArgs e)
        {
            Cust_POPUP cust_POPUP = new Cust_POPUP();
            cust_POPUP.ShowDialog();
            CustNameTxtBox.Text = Custname;
        }

        private void SelButton_Click(object sender, EventArgs e)
        {
            selectHeader();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택된 구매입고 상세정보를 수정하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                                DB.sqlcmd = new SqlCommand("PW_Purchase_Warehousing_D_U1", DB.conn, DB.transaction);
                                DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                DB.sqlcmd.Parameters.AddWithValue("@INPUT_NO",          dataGridView2.Rows[i].Cells["입고번호"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@MTRL_ORDERNO",      dataGridView2.Rows[i].Cells["발주번호"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO",         dataGridView2.Rows[i].Cells["요청번호"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@LOT_NO",            dataGridView2.Rows[i].Cells["LOT번호"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE",        dataGridView2.Rows[i].Cells["공장코드"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE",         dataGridView2.Rows[i].Cells["품목코드"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME",         dataGridView2.Rows[i].Cells["품목 명"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@INQTY",             dataGridView2.Rows[i].Cells["입고수량"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID",          LoginID);
                               
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
                        MessageBox.Show("수정할 구매입고 상세정보를 선택해 주세요.");
                    }
                    else MessageBox.Show("선택된 구매입고 상세정보가 수정되었습니다.");
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

        private void Add_WarehousingButton_Click(object sender, EventArgs e)
        {

            PW_ORDER_POPUP oRDER_POPUP = new PW_ORDER_POPUP();
            PW_ORDER_POPUP.LoginID = LoginID;
            oRDER_POPUP.ShowDialog();
            load();
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

            //if (selcnt1 != 0 && selcnt2 != 0)
            ////그리드1 체크된게 있고 그리드2에도 체크된게 있으면 
            //{
            //    MessageBox.Show("구매 입고 정보와 구매 입고 상세정보 둘중 하나의 정보만 삭제할 수 있습니다.");
            //    return;
            //}
            //else 

            if ((selcnt1 != 0 && selcnt2 == 0) || (selcnt1 != 0 && selcnt2 != 0 ))
            //그리드1 체크된거 있고 그리드2 체크된거 없으면
            {
                if (MessageBox.Show("선택된 입고정보를 삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                                IN_No = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                SelectDetail();
                                DB.conn = new SqlConnection(DB.connectionString);
                                DB.conn.ConnectionString = DB.connectionString;
                                DB.conn.Open();
                                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                                {
                                    try
                                    {

                                        DB.transaction = DB.conn.BeginTransaction();

                                        DB.sqlcmd = new SqlCommand("PW_Purchase_Warehousing_D_D1", DB.conn, DB.transaction);
                                        DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                        DB.sqlcmd.Parameters.AddWithValue("@INPUT_NO",          dataGridView2.Rows[i].Cells["입고번호"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@MTRL_ORDERNO",      dataGridView2.Rows[i].Cells["발주번호"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO",         dataGridView2.Rows[i].Cells["요청번호"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE",        dataGridView2.Rows[i].Cells["공장코드"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE",         dataGridView2.Rows[i].Cells["품목코드"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@LOT_NO",            dataGridView2.Rows[i].Cells["LOT번호"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@INQTY",             dataGridView2.Rows[i].Cells["입고수량"].Value.ToString());
                                        DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID",          LoginID);

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

                                    DB.sqlcmd = new SqlCommand("PW_Purchase_Warehousing_H_D1", DB.conn, DB.transaction);
                                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                    DB.sqlcmd.Parameters.AddWithValue("@INPUT_NO", dataGridView1.Rows[i].Cells["입고번호"].Value.ToString());

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
                        MessageBox.Show("선택된 입고정보가 삭제되었습니다.");
                        selectHeader();
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
                if (MessageBox.Show("선택된 입고 상세정보를 삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                                    DB.sqlcmd = new SqlCommand("PW_Purchase_Warehousing_D_D1", DB.conn, DB.transaction);
                                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                    DB.sqlcmd.Parameters.AddWithValue("@INPUT_NO",          dataGridView2.Rows[i].Cells["입고번호"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@MTRL_ORDERNO",      dataGridView2.Rows[i].Cells["발주번호"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@REQUESTNO",         dataGridView2.Rows[i].Cells["요청번호"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE",        dataGridView2.Rows[i].Cells["공장코드"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE",         dataGridView2.Rows[i].Cells["품목코드"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@LOT_NO",            dataGridView2.Rows[i].Cells["LOT번호"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@INQTY",             dataGridView2.Rows[i].Cells["입고수량"].Value.ToString());
                                    DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID",          LoginID);

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
                        MessageBox.Show("선택된 입고 상세정보가 삭제되었습니다.");
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
    }
}
