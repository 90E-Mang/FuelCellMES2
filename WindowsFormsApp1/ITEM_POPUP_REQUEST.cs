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
    public partial class ITEM_POPUP_REQUEST : Form
    {
        static public string LoginID;
        static public string Custcode = null;
        static public string Custname = null;

        public ITEM_POPUP_REQUEST()
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
                CompCodeCmbBox.FlatStyle = FlatStyle.Popup;
                CompCodeCmbBox.BackColor = Color.Ivory;
                PlantCodeCmbBox.FlatStyle = FlatStyle.Popup;
                PlantCodeCmbBox.BackColor = Color.Ivory;

                DB.conn.Close();
                dataGridView1.Columns.Clear();
                dataGridView2.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("ITEM_POPUP_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "삭제"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "ITEM_POPUP_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "ITEM_POPUP_S1";
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].Width = 50;

                
                // 두번쨰 그리드
                var chkCol2 = new DataGridViewCheckBoxColumn
                {
                    Name = "check", HeaderText = "선택"
                };
                dataGridView2.Columns.Add(chkCol2);
                dataGridView2.Columns[0].Width = 50;
                dataGridView2.ColumnCount = 9;
                dataGridView2.Columns[1].Name = "요청 수량";
                dataGridView2.Columns[2].Name = "품목 코드"; 
                dataGridView2.Columns[3].Name = "품명";
                dataGridView2.Columns[4].Name = "품목 유형";
                dataGridView2.Columns[5].Name = "품목 규격";
                dataGridView2.Columns[6].Name = "기본 단위";
                dataGridView2.Columns[7].Name = "단가";
                dataGridView2.Columns[8].Name = "단위 중량";

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
                DB.sqlcmd = new SqlCommand("ITEM_POPUP_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", ItemCodeTxtBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME", ItemNameTxtBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_TYPE", ItemTypeTxtBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME", CustTxtBox.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "ITEM_POPUP_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "ITEM_POPUP_S2";
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

        private void CompCodeCmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CompCodeCmbBox.Text != "")
            {
                 DB.conn.Close();

                //DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                string compcode = CompCodeCmbBox.SelectedValue.ToString();

                SqlDataAdapter PLANTAdapter = new SqlDataAdapter();
                DataSet PLANTdt = new DataSet();
                PLANTAdapter.TableMappings.Add("Table", "PLANT_NAME");
                DB.sqlcmd.CommandType = CommandType.Text;
                DB.sqlcmd.CommandText = "SELECT PLANT_CODE, PLANT_NAME FROM TB_PlantMaster WHERE COMP_CODE = '" + compcode + "'";
                PLANTAdapter.SelectCommand = DB.sqlcmd;
                PLANTAdapter.SelectCommand.ExecuteNonQuery();
                PLANTAdapter.Fill(PLANTdt);
                PlantCodeCmbBox.DataSource = PLANTdt.Tables[0];
                PlantCodeCmbBox.DisplayMember = "PLANT_NAME";
                PlantCodeCmbBox.ValueMember = "PLANT_CODE";
                PlantCodeCmbBox.Text = null;
                
                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
            }
            else
            {
                PlantCodeCmbBox.DisplayMember = null;
                PlantCodeCmbBox.ValueMember = null;
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
                    ///////////////////////////////////////////////////////////////////////////////////////
                    //string custname ="";
                    //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    //{
                    //    bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                    //    if(isChecked)
                    //    {
                    //        custname = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    //        break;
                    //    }
                    //}
                    //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    //{
                    //    bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                    //    if (isChecked)
                    //    {
                    //        try
                    //        {
                    //            if (custname != dataGridView1.Rows[i].Cells[3].Value.ToString())
                    //            {
                    //                break;
                    //            }
                    //            else
                    //            {
                    //            dataGridView2.Rows.Add(false, 0,
                    //                dataGridView1.Rows[i].Cells[1].Value.ToString(),
                    //                dataGridView1.Rows[i].Cells[2].Value.ToString(),
                    //                dataGridView1.Rows[i].Cells[3].Value.ToString(),
                    //                dataGridView1.Rows[i].Cells[4].Value.ToString(),
                    //                dataGridView1.Rows[i].Cells[7].Value.ToString(),
                    //                dataGridView1.Rows[i].Cells[8].Value.ToString(),
                    //                dataGridView1.Rows[i].Cells[9].Value.ToString()
                    //                );
                    //            }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            MessageBox.Show(ex.Message);
                    //        }
                    //        delcnt++;
                    //    }
                    //}

                    ///////////////////////////////////////////////////////////////////////////////////////
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                        if (isChecked)
                        {
                            try
                            {
                                dataGridView2.Rows.Add(false, 0, 
                                    dataGridView1.Rows[i].Cells[1].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[2].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[3].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[4].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[7].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[8].Value.ToString(),
                                    dataGridView1.Rows[i].Cells[9].Value.ToString()
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

                    DB.transaction = DB.conn.BeginTransaction();
                    DB.sqlcmd = new SqlCommand("MTRL_Request_H_I1", DB.conn, DB.transaction);
                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                    DB.sqlcmd.Parameters.AddWithValue("@COMP_CODE", CompCodeCmbBox.SelectedValue);
                    DB.sqlcmd.Parameters.AddWithValue("@COMP_NAME", CompCodeCmbBox.Text);
                    DB.sqlcmd.Parameters.AddWithValue("@PLANT_CODE", PlantCodeCmbBox.SelectedValue);
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
                                DB.sqlcmd = new SqlCommand("MTRL_Request_D_I1", DB.conn, DB.transaction);
                                DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE",     dataGridView2.Rows[i].Cells["품목 코드"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME",     dataGridView2.Rows[i].Cells["품명"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@REQQTY",        dataGridView2.Rows[i].Cells["요청 수량"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@UNITCOST",      dataGridView2.Rows[i].Cells["단가"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@BASEUNIT",      dataGridView2.Rows[i].Cells["기본 단위"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@UNITWGT",       dataGridView2.Rows[i].Cells["단위 중량"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_SPEC",     dataGridView2.Rows[i].Cells["품목 규격"].Value.ToString());
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
                }
            }
        }

        private void CustTxtBox_MouseClick(object sender, MouseEventArgs e)
        {
            Cust_POPUP cust_POPUP = new Cust_POPUP();
            cust_POPUP.ShowDialog();
            CustTxtBox.Text = Custname;
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
                        bool isChecked = Convert.ToBoolean(dataGridView2.Rows[i-delcnt].Cells[0].Value);
                        if (isChecked)
                        {
                            try
                            {
                                dataGridView2.Rows.Remove(dataGridView2.Rows[i- delcnt]);
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
    }
}
