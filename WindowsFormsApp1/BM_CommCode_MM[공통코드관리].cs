using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class BM_CommCode_MM : Form
    {
        static public string LoginID;
        string UPRCODE = null;
        public BM_CommCode_MM()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        public delegate void FormClosed(string str);
        public event FormClosed FormCloseEvent;
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.FormCloseEvent("공통 코드 관리");
            this.Close();

        }
        private void CommCodeMLoad(object sender, EventArgs e)
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
                DB.sqlcmd = new SqlCommand("CommCodeM_S2", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;


                DB.sqlcmd.Parameters.AddWithValue("@USEFLAG", UTPCmbBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@UPRCODENAME", CodeTxtBox.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "CommCodeM_S2");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "CommCodeM_S2";
                //dataGridView1.Columns[13].Width = 120;
                //dataGridView1.Columns[15].Width = 120;
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].ReadOnly = true;
                }
                dataGridView1.Columns[0].Width = 50;

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
//코드 조회 버튼
        private void Select_Click(object sender, EventArgs e)
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
                DB.sqlcmd = new SqlCommand("CommCodeM_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@USEFLAG", UTPCmbBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@UPRCODENAME", CodeTxtBox.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "CommCodeM_S1");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "CommCodeM_S1";
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
            }
            catch (Exception ex)
            {
                DB.transaction.Rollback();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DB.transaction.Commit();
                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
                DB.sqlDR.Close();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            
        }

        private void DSaveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("수정하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                                DB.sqlcmd = new SqlCommand("CommCodeM_LW_U1", DB.conn, DB.transaction);
                                DB.sqlcmd.CommandType = CommandType.StoredProcedure;
                                DB.sqlcmd.Parameters.AddWithValue("@UPRCODE",       UPRCODE);
                                DB.sqlcmd.Parameters.AddWithValue("@LWRCODE",       dataGridView2.Rows[i].Cells["코드"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@LWRCODENAME",   dataGridView2.Rows[i].Cells["코드 명"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@CODE_VALUE1",   dataGridView2.Rows[i].Cells["코드정보 1"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@CODE_VALUE2",   dataGridView2.Rows[i].Cells["코드정보 2"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@CODE_VALUE3",   dataGridView2.Rows[i].Cells["코드정보 3"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@SORT_NO",       dataGridView2.Rows[i].Cells["순번"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@REMARK",        dataGridView2.Rows[i].Cells["비고"].Value.ToString());
                                DB.sqlcmd.Parameters.AddWithValue("@USEFLAG",       dataGridView2.Rows[i].Cells["사용유무"].Value.ToString());
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
                        MessageBox.Show("수정할 항목을 선택해 주세요.");
                    }
                    else MessageBox.Show("수정되었습니다.");
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

        private void DSelectButton_Click(object sender, EventArgs e)
        {
            try
            {
                DB.conn.Close();
                dataGridView2.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.sqlcmd = new SqlCommand("CommCodeM_LW_S1", DB.conn);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@UPRCODE", UPRCODE);
                DB.sqlcmd.Parameters.AddWithValue("@USEFLAG", DUTPCmbBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@LWRCODENAME", DCodeTxtBox.Text);

                //DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView2.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "CommCodeM_LW_S1");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "CommCodeM_LW_S1";
                dataGridView2.Columns[0].Width = 50;

                //DB.transaction.Commit();
            }
            catch (Exception )
            {
            }
            finally
            {
                DB.adapter.Dispose();
                DB.sqlcmd.Dispose();
                DB.conn.Dispose();
                DB.conn.Close();
                DB.sqlDR.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            UPRCODE = row.Cells["코드ID"].Value.ToString();

            try
            {
                DB.conn.Close();
                dataGridView2.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("CommCodeM_LW_S1", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@UPRCODE", row.Cells["코드ID"].Value.ToString());
                DB.sqlcmd.Parameters.AddWithValue("@USEFLAG", DUTPCmbBox.Text);
                DB.sqlcmd.Parameters.AddWithValue("@LWRCODENAME", DCodeTxtBox.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"
                };
                dataGridView2.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "CommCodeM_LW_S1");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "CommCodeM_LW_S1";
                dataGridView2.Columns[0].Width = 50;

                DB.transaction.Commit();
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
    }
}
