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
    public partial class MM_STOCKOUT_ITEM_POPUP : Form
    {
        static public string LoginID;

        public MM_STOCKOUT_ITEM_POPUP()
        {
            InitializeComponent();
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
                CompCodeCmbBox.DataSource = COMPdt.Tables[0];
                CompCodeCmbBox.DisplayMember = "COMP_NAME";
                CompCodeCmbBox.ValueMember = "COMP_CODE";
                CompCodeCmbBox.Text = "";

                SqlDataAdapter PLANTAdapter = new SqlDataAdapter();
                DataSet PLANTdt = new DataSet();
                PLANTAdapter.TableMappings.Add("Table", "PLANT_NAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT PLANT_CODE, PLANT_NAME FROM TB_PlantMaster ";// WHERE COMP_CODE = '" + compcode + "'";
                PLANTAdapter.SelectCommand = DB.sqlcmd;
                PLANTAdapter.SelectCommand.ExecuteNonQuery();
                PLANTAdapter.Fill(PLANTdt);
                PlantCodeCmbBox.DataSource = PLANTdt.Tables[0];
                PlantCodeCmbBox.DisplayMember = "PLANT_NAME";
                PlantCodeCmbBox.ValueMember = "PLANT_CODE";
                PlantCodeCmbBox.Text = "";

                SqlDataAdapter processAdapter = new SqlDataAdapter();
                DataSet processDt = new DataSet();
                processAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='PROCESS_CODE' AND SORT_NO > 1";
                processAdapter.SelectCommand = DB.sqlcmd;
                processAdapter.SelectCommand.ExecuteNonQuery();
                processAdapter.Fill(processDt);
                ProcessCmbBox.DataSource = processDt.Tables[0];
                ProcessCmbBox.DisplayMember = "LWRCODENAME";
                ProcessCmbBox.ValueMember = "LWRCODE";
                ProcessCmbBox.Text = "";

                SqlDataAdapter reasonAdapter = new SqlDataAdapter();
                DataSet reasonDt = new DataSet();
                reasonAdapter.TableMappings.Add("Table", "LWRCODENAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT LWRCODE, LWRCODENAME FROM TB_LWRCOMMCODE WHERE UPRCODE='INOUT_REASON' AND LWRCODE IN ('ON','OG')";
                reasonAdapter.SelectCommand = DB.sqlcmd;
                reasonAdapter.SelectCommand.ExecuteNonQuery();
                reasonAdapter.Fill(reasonDt);
                OutReasonCmbBox.DataSource = reasonDt.Tables[0];
                OutReasonCmbBox.DisplayMember = "LWRCODENAME";
                OutReasonCmbBox.ValueMember = "LWRCODE";
                OutReasonCmbBox.Text = "";

                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
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
                DB.sqlcmd = new SqlCommand("ITEM_POPUP_STOCKOUT_S1", DB.conn, DB.transaction);
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
                DB.adapter.Fill(ds, "ITEM_POPUP_STOCKOUT_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "ITEM_POPUP_STOCKOUT_S1";
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[3].Visible = false;


                // 두번쨰 그리드

                dataGridView2.ColumnCount = 11;
                dataGridView2.Columns[0].Name = "회사코드"; //not visible
                dataGridView2.Columns[1].Name = "회사";
                dataGridView2.Columns[2].Name = "공장코드"; //not visible
                dataGridView2.Columns[3].Name = "공장";
                dataGridView2.Columns[4].Name = "LOT번호"; 
                dataGridView2.Columns[5].Name = "품목코드";
                dataGridView2.Columns[6].Name = "품목";
                dataGridView2.Columns[7].Name = "품목유형";
                dataGridView2.Columns[8].Name = "현재재고";
                dataGridView2.Columns[9].Name = "기본단위";
                dataGridView2.Columns[10].Name = "불출요청수량";

                for (int i = 1; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].ReadOnly = true;
                }

                dataGridView2.Columns[10].ReadOnly = false;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[2].Visible = false;

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
                DB.sqlcmd = new SqlCommand("ITEM_POPUP_STOCKOUT_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE",     CompCodeCmbBox.SelectedValue);
                DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE",    PlantCodeCmbBox.SelectedValue);
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE",     ItemCodeTxtBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME",     ItemNameTxtBox.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);

                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "ITEM_POPUP_STOCKOUT_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "ITEM_POPUP_STOCKOUT_S2";

                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[8].Width = 70;
                dataGridView1.Columns[10].Width = 70;
                dataGridView1.Columns[11].Width = 70;
                dataGridView1.Columns[12].Width = 70;
                dataGridView1.Columns[13].Width = 70;
                dataGridView1.Columns[14].Width = 70;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[3].Visible = false;

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
                    dataGridView2.Rows.Clear();

                    int delcnt = 0;
                    string check_plant1 = "";
                    string check_plant2 = "";
                    int success = 1;
                    
                    for (int i = 0; i < dataGridView1.Rows.Count; i++) // 기준 거래처코드 가져옴
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);

                        if (isChecked)
                        {
                            check_plant1 = dataGridView1.Rows[i].Cells["공장코드"].Value.ToString();
                            break;
                        }
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);

                        if (isChecked)
                        {
                            check_plant2 = dataGridView1.Rows[i].Cells["공장코드"].Value.ToString();
                            
                            if (check_plant1 == check_plant2)
                            {
                                try
                                {
                                    dataGridView2.Rows.Add(  
                                        dataGridView1.Rows[i].Cells["회사코드"].Value.ToString(),
                                        dataGridView1.Rows[i].Cells["회사"].Value.ToString(),
                                        dataGridView1.Rows[i].Cells["공장코드"].Value.ToString(),
                                        dataGridView1.Rows[i].Cells["공장"].Value.ToString(),
                                        dataGridView1.Rows[i].Cells["LOT번호"].Value.ToString(),
                                        dataGridView1.Rows[i].Cells["품목코드"].Value.ToString(),
                                        dataGridView1.Rows[i].Cells["품목"].Value.ToString(),
                                        dataGridView1.Rows[i].Cells["품목유형"].Value.ToString(),
                                        dataGridView1.Rows[i].Cells["현재재고"].Value.ToString(),
                                        dataGridView1.Rows[i].Cells["기본단위"].Value.ToString(),
                                        0
                                        );
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                
                                delcnt++;
                                if (delcnt == 0)
                                {
                                    MessageBox.Show("추가할 품목을 선택해 주세요.");
                                    break;
                                }
                            }
                            else if (check_plant1 != check_plant2)
                            {
                                MessageBox.Show("동일한 공장의 품목만 가져올 수 있습니다.");

                                dataGridView2.ColumnCount = 11;
                                dataGridView2.Columns[0].Name = "회사코드"; //not visible
                                dataGridView2.Columns[1].Name = "회사";
                                dataGridView2.Columns[2].Name = "공장코드"; //not visible
                                dataGridView2.Columns[3].Name = "공장";
                                dataGridView2.Columns[4].Name = "LOT번호";
                                dataGridView2.Columns[5].Name = "품목코드";
                                dataGridView2.Columns[6].Name = "품목";
                                dataGridView2.Columns[7].Name = "품목유형";
                                dataGridView2.Columns[8].Name = "현재재고";
                                dataGridView2.Columns[9].Name = "기본단위";
                                dataGridView2.Columns[10].Name = "불출요청수량";

                                for (int j = 1; j < dataGridView2.Columns.Count; j++)
                                {
                                    dataGridView2.Columns[j].ReadOnly = true;
                                }

                                dataGridView2.Columns[10].ReadOnly = false;
                                dataGridView2.Columns[0].Visible = false;
                                dataGridView2.Columns[2].Visible = false;
                                dataGridView2.Rows.Clear();
                                //DB.transaction.Commit();

                                CombMaster();
                                MessageBox.Show("추가가 취소되었습니다.");
                                success = 0;
                                break;
                            }
                        }
                    }
                    if (success != 0)
                    {
                         MessageBox.Show("추가되었습니다.");
                    }
                    //else MessageBox.Show("추가되었습니다.");
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
            if (MessageBox.Show("등록하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (ProcessCmbBox.Text == "" || OutReasonCmbBox.Text == "" ||
                    ProcessCmbBox.Text == null || OutReasonCmbBox.Text == null ||
                    ProcessCmbBox.Text == "전체" || OutReasonCmbBox.Text == "전체")
                {
                    MessageBox.Show("공정과 수불사유를 선택하세요");
                }
                if (dataGridView2.Rows.Count == 0)
                {
                    MessageBox.Show("등록할 정보가 없습니다.");
                }
                else
                {
                    try
                    {
                        DB.conn.Close();

                        DB.conn = new SqlConnection(DB.connectionString);
                        DB.conn.ConnectionString = DB.connectionString;
                        DB.conn.Open();

                        DB.transaction = DB.conn.BeginTransaction();
                        DB.sqlcmd = new SqlCommand("ITEM_POPUP_STOCKOUT_H_I1", DB.conn,     DB.transaction);
                        DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                        DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE",     dataGridView2.Rows  [0].Cells["회사코드"].Value.ToString());
                        DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE",    dataGridView2.Rows  [0].Cells["공장코드"].Value.ToString());
                        DB.sqlcmd.Parameters.AddWithValue("@PROCESS_CODE",      ProcessCmbBox.SelectedValue);
                        DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID",      LoginID);

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
                            
                            try
                            {
                                DB.transaction = DB.conn.BeginTransaction();
                                DB.sqlcmd = new SqlCommand("ITEM_POPUP_STOCKOUT_D_I1",  DB.conn, DB.transaction);
                                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE",         dataGridView2.Rows[i].Cells["품목코드"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@LOT_NO",            dataGridView2.Rows[i].Cells["LOT번호"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@STOCKQTY",          dataGridView2.Rows[i].Cells["현재재고"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@OUT_REQ_QTY",       dataGridView2.Rows[i].Cells["불출요청수량"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@BASEUNIT",          dataGridView2.Rows[i].Cells["기본단위"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@INOUT_REASON",      OutReasonCmbBox.SelectedValue);
                                DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID",      LoginID);

                                DB.sqlcmd.ExecuteNonQuery();

                                DB.transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                DB.transaction.Rollback();

                                MessageBox.Show(ex.Message);
                                MessageBox.Show("등록이 취소되었습니다.");
                                break;
                            }
                            selcnt++;

                        }
                        if (selcnt == 0)
                        {
                            MessageBox.Show("등록할 항목을 선택해 주세요.");
                        }
                        else
                        {
                            if (MessageBox.Show("등록되었습니다.") == DialogResult.OK)
                            {
                                this.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("등록이 취소되었습니다.");

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
        }

        private void DelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("취소하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridView2.Rows.Count == 0)
                {
                    MessageBox.Show("등록할 정보가 없습니다.");
                }
                else
                {
                    dataGridView2.Columns.Clear();
                    MessageBox.Show("취소되었습니다.");
                }
                
            }
        }

        private void dataGridView2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string name = dataGridView2.CurrentCell.OwningColumn.Name;

            if (name == "불출요청수량")
            {
                e.Control.KeyPress += new KeyPressEventHandler(dataGridView2_KeyPress);
            }

            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView2_KeyPress);
            }

        }
    }
}
