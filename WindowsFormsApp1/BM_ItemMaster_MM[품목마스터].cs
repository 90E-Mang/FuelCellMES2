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
    public partial class BM_ItemMaster_MM : Form
    {
        static public string LoginID;
        static public string Custcode = null;
        static public string Custname = null;


        public BM_ItemMaster_MM()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

        }
        public delegate void FormClosed(string str);
        public event FormClosed FormCloseEvent;
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.FormCloseEvent("품목 마스터");
            this.Close();

        }
        private void CombMaster()
        {
            DB.conn.Close();

            DB.conn.ConnectionString = DB.connectionString;
            DB.conn.Open();

            SqlDataAdapter USEAdapter = new SqlDataAdapter();
            DataSet USEdt = new DataSet();
            USEAdapter.TableMappings.Add("Table", "USE_TYPE");
            DB.sqlcmd.CommandType = CommandType.Text;
            DB.sqlcmd.CommandText = "SELECT LWRCODE, SORT_NO FROM TB_LWRCOMMCODE WHERE UPRCODE = 'USE_TYPE'";
            USEAdapter.SelectCommand = DB.sqlcmd;
            USEAdapter.SelectCommand.ExecuteNonQuery();
            USEAdapter.Fill(USEdt);
            UseCmbBox.DataSource = USEdt.Tables[0];
            UseCmbBox.DisplayMember = "LWRCODE";
            UseCmbBox.ValueMember = "SORT_NO";

            SqlDataAdapter MTYPEAdapter = new SqlDataAdapter();
            DataSet MTYPEdt = new DataSet();
            MTYPEAdapter.TableMappings.Add("Table", "MTRL_TYPE");
            DB.sqlcmd.CommandType = CommandType.Text;
            DB.sqlcmd.CommandText = "SELECT LWRCODENAME, SORT_NO FROM TB_LWRCOMMCODE WHERE UPRCODE = 'MTRL_TYPE'";
            MTYPEAdapter.SelectCommand = DB.sqlcmd;
            MTYPEAdapter.SelectCommand.ExecuteNonQuery();
            MTYPEAdapter.Fill(MTYPEdt);
            MTCmbBox.DataSource = MTYPEdt.Tables[0];
            MTCmbBox.DisplayMember = "LWRCODENAME";
            MTCmbBox.ValueMember = "SORT_NO";

            SqlDataAdapter UWGTAdapter = new SqlDataAdapter();
            DataSet UWGTEdt = new DataSet();
            UWGTAdapter.TableMappings.Add("Table", "STCD");
            DB.sqlcmd.CommandType = CommandType.Text;
            DB.sqlcmd.CommandText = "SELECT LWRCODE, SORT_NO FROM TB_LWRCOMMCODE WHERE UPRCODE = 'STCD'";
            UWGTAdapter.SelectCommand = DB.sqlcmd;
            UWGTAdapter.SelectCommand.ExecuteNonQuery();
            UWGTAdapter.Fill(UWGTEdt);
            UWGTComBox.DataSource = UWGTEdt.Tables[0];
            UWGTComBox.DisplayMember = "LWRCODE";
            UWGTComBox.ValueMember = "SORT_NO";

            SqlDataAdapter InsCHKAdapter = new SqlDataAdapter();
            DataSet InsCHKdt = new DataSet();
            InsCHKAdapter.TableMappings.Add("Table", "USE_CHK");
            DB.sqlcmd.CommandType = CommandType.Text;
            DB.sqlcmd.CommandText = "SELECT LWRCODE, SORT_NO FROM TB_LWRCOMMCODE WHERE UPRCODE = 'USE_CHK'";
            InsCHKAdapter.SelectCommand = DB.sqlcmd;
            InsCHKAdapter.SelectCommand.ExecuteNonQuery();
            InsCHKAdapter.Fill(InsCHKdt);
            InsCHKCmbBox.DataSource = InsCHKdt.Tables[0];
            InsCHKCmbBox.DisplayMember = "LWRCODE";
            InsCHKCmbBox.ValueMember = "SORT_NO";

            DB.adapter.Dispose();
            DB.sqlcmd.Dispose();
            DB.conn.Dispose();
            DB.conn.Close();
        }
        private void select()
        {
            DB.conn.Close();
            dataGridView1.Columns.Clear();

            DB.conn = new SqlConnection(DB.connectionString);
            DB.conn.ConnectionString = DB.connectionString;
            DB.conn.Open();
            //트랜잭션 시작
            DB.transaction = DB.conn.BeginTransaction();
            DB.sqlcmd = new SqlCommand("ITEM_MASTER_S1", DB.conn, DB.transaction);
            DB.sqlcmd.CommandType = CommandType.StoredProcedure;

            DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", textBox2.Text);
            DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME", textBox3.Text);

            DB.sqlcmd.ExecuteNonQuery();
            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "check",
                HeaderText = "삭제"
            };
            dataGridView1.Columns.Add(chkCol);
            DB.adapter = new SqlDataAdapter(DB.sqlcmd);
            DataSet ds = new DataSet();
            DB.adapter.Fill(ds, "ITEM_MASTER_S1");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "ITEM_MASTER_S1";
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
        private void txtboxclear()
        {
            ICodeBox.ReadOnly = false;

            ICodeBox.Text = null;
            INameBox.Text = null;
            MTCmbBox.Text = null;
            ISpecBox.Text = null;
            MaxStockBox.Text = null;
            SafeStockBox.Text = null;
            BUnitWGTBox.Text = null;
            BaseCostBox.Text = null;
            UWGTComBox.Text = null;
            InsCHKCmbBox.Text = null;
            MinOrderBox.Text = null;
            OrderBox.Text = null;
            MaxOrderBox.Text = null;
            CNameBox.Text = null;
            UseCmbBox.Text = null;
            RmkBox.Text = null;

        }
        private void BM_ItemMaster_MM_Load(object sender, EventArgs e)
        {
            try
            {
                CNameBox.Text = Custname;

                DB.conn.Close();
                dataGridView1.Columns.Clear();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                //트랜잭션 시작
                DB.transaction = DB.conn.BeginTransaction();
                DB.sqlcmd = new SqlCommand("ITEM_MASTER_S3", DB.conn, DB.transaction);
                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", textBox2.Text);
                DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME", textBox3.Text);

                DB.sqlcmd.ExecuteNonQuery();
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "삭제"
                };
                dataGridView1.Columns.Add(chkCol);
                DB.adapter = new SqlDataAdapter(DB.sqlcmd);
                DataSet ds = new DataSet();
                DB.adapter.Fill(ds, "ITEM_MASTER_S3");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "ITEM_MASTER_S3";
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
                CombMaster();

                ICodeBox.ReadOnly = true;
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

        // 조건 조회 버튼 
        private void SelButtonClick(object sender, EventArgs e)
        {

            try
            {
                select();
                ICodeBox.ReadOnly = true;

            }
            catch (Exception ex)
            {
                DB.transaction.Rollback();
                MessageBox.Show("조회실패\n" + ex.Message);
            }
            finally
            {
                DB.conn.Dispose();
                DB.sqlDR.Close();
                DB.conn.Close();
            }

        }

        // 추가 버튼
        private void InsertButtonClick(object sender, EventArgs e)
        {
            try
            {
                txtboxclear();

            }
            catch (Exception)
            {
                throw;
            }
        }

        //DELETE BUTTON
        private void DelButtonClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("삭제하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ICodeBox.ReadOnly = true;

                DB.conn.Close();

                DB.conn = new SqlConnection(DB.connectionString);
                DB.conn.ConnectionString = DB.connectionString;
                DB.conn.Open();
                try
                {
                    dataGridView1.EndEdit();
                    int delcnt = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                        if (isChecked)
                        {
                            try
                            {
                                DB.transaction = DB.conn.BeginTransaction();
                                DB.sqlcmd = new SqlCommand("ITEM_MASTER_D1", DB.conn, DB.transaction);
                                DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                                DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", dataGridView1.Rows[i].Cells["품목 코드"].Value.ToString());

                                DB.sqlcmd.ExecuteNonQuery();

                                DB.transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                DB.transaction.Rollback();
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
                    DB.conn.Dispose();
                    DB.conn.Close();
                    txtboxclear();
                    select();
                    ICodeBox.ReadOnly = true;
                }
            }
        }

        //SAVE BUTTON
        private void SaveButtonClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("저장하시겠습니까?", "안내", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (ICodeBox.Text == "" || INameBox.Text == "" || MTCmbBox.Text == "" ||
                UseCmbBox.Text == "")
                {
                    MessageBox.Show("품목 코드와 품명, 품목 유형,사용여부는 필수 입력 정보입니다.");
                }
                else
                {
                    //CNameBox.Text = Custcode;
                    if (ICodeBox.ReadOnly)
                    {
                        try
                        {
                            DB.conn.Close();

                            DB.conn = new SqlConnection(DB.connectionString);
                            DB.conn.ConnectionString = DB.connectionString;
                            DB.conn.Open();
                            //트랜잭션 시작
                            DB.transaction = DB.conn.BeginTransaction();
                            DB.sqlcmd = new SqlCommand("ITEM_MASTER_U1", DB.conn, DB.transaction);
                            DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", ICodeBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME", INameBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_TYPE", MTCmbBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_SPEC", ISpecBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@MAXSTOCK", MaxStockBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@SAFESTOCK", SafeStockBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@BASEUNIT", UWGTComBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@UNITCOST", BaseCostBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@UNITWGT", BUnitWGTBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@INSPFLAG", InsCHKCmbBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@MINORDERQTY", MinOrderBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@ORDERQTY", OrderBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@MAXORDERQTY", MaxOrderBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE", Custcode);
                            DB.sqlcmd.Parameters.AddWithValue("@CUST_NAME", CNameBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@USEFLAG", UseCmbBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@REMARK", RmkBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID", LoginID);

                            DB.sqlcmd.ExecuteNonQuery();

                            DB.transaction.Commit();
                            MessageBox.Show("업데이트 되었습니다.");
                            ICodeBox.ReadOnly = true;
                        }
                        catch (Exception ex)
                        {
                            DB.transaction.Rollback();
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            DB.conn.Dispose();
                            DB.conn.Close();
                            select();
                            DB.conn.Dispose();
                            DB.conn.Close();

                        }
                    }
                    else
                    {
                        try
                        {
                            DB.conn.Close();

                            DB.conn = new SqlConnection(DB.connectionString);
                            DB.conn.ConnectionString = DB.connectionString;
                            DB.conn.Open();
                            //트랜잭션 시작
                            DB.transaction = DB.conn.BeginTransaction();
                            DB.sqlcmd = new SqlCommand("ITEM_MASTER_I1", DB.conn, DB.transaction);
                            DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", ICodeBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME", INameBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_TYPE", MTCmbBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@ITEM_SPEC", ISpecBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@MAXSTOCK", MaxStockBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@SAFESTOCK", SafeStockBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@BASEUNIT", UWGTComBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@UNITCOST", BaseCostBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@UNITWGT", BUnitWGTBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@INSPFLAG", InsCHKCmbBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@MINORDERQTY", MinOrderBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@ORDERQTY", OrderBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@MAXORDERQTY", MaxOrderBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@CUST_CODE", CNameBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@USEFLAG", UseCmbBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@REMARK", RmkBox.Text);
                            DB.sqlcmd.Parameters.AddWithValue("@LOGIN_ID", LoginID);


                            DB.sqlcmd.ExecuteNonQuery();

                            DB.transaction.Commit();
                            MessageBox.Show("저장되었습니다.");
                            ICodeBox.ReadOnly = true;

                        }
                        catch (Exception ex)
                        {
                            DB.transaction.Rollback();
                            MessageBox.Show(ex.Message);

                        }
                        finally
                        {
                            DB.conn.Dispose();
                            DB.conn.Close();
                            select();
                            DB.conn.Dispose();
                            DB.conn.Close();
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ICodeBox.ReadOnly = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                try
                {
                    DB.sqlcmd.Dispose();
                    DB.conn.Dispose();
                    DB.conn.Close();
                    DB.sqlDR.Close();
                    DB.conn = new SqlConnection(DB.connectionString);
                    DB.conn.ConnectionString = DB.connectionString;
                    DB.conn.Open();

                    //트랜잭션 시작
                    DB.transaction = DB.conn.BeginTransaction();
                    DB.sqlcmd = new SqlCommand("ITEM_MASTER_S2", DB.conn, DB.transaction);
                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                    DB.sqlcmd.Parameters.AddWithValue("@ITEM_CODE", row.Cells["품목 코드"].Value.ToString());
                    DB.sqlcmd.Parameters.AddWithValue("@ITEM_NAME", row.Cells["품명"].Value.ToString());

                    DB.sqlcmd.ExecuteNonQuery();

                    DB.sqlDR = DB.sqlcmd.ExecuteReader();
                    while (DB.sqlDR.Read())
                    {
                        ICodeBox.Text = DB.sqlDR["ITEM_CODE"].ToString();
                        INameBox.Text = DB.sqlDR["ITEM_NAME"].ToString();
                        MTCmbBox.Text = DB.sqlDR["ITEM_TYPE"].ToString();
                        ISpecBox.Text = DB.sqlDR["ITEM_SPEC"].ToString();
                        MaxStockBox.Text = DB.sqlDR["MAXSTOCK"].ToString();
                        SafeStockBox.Text = DB.sqlDR["SAFESTOCK"].ToString();
                        BUnitWGTBox.Text = DB.sqlDR["UNITWGT"].ToString();
                        BaseCostBox.Text = DB.sqlDR["UNITCOST"].ToString();
                        UWGTComBox.Text = DB.sqlDR["BASEUNIT"].ToString();
                        InsCHKCmbBox.Text = DB.sqlDR["INSPFLAG"].ToString();
                        MinOrderBox.Text = DB.sqlDR["MINORDERQTY"].ToString();
                        OrderBox.Text = DB.sqlDR["ORDERQTY"].ToString();
                        MaxOrderBox.Text = DB.sqlDR["MAXORDERQTY"].ToString();
                        CNameBox.Text = DB.sqlDR["CUST_CODE"].ToString();
                        UseCmbBox.Text = DB.sqlDR["USEFLAG"].ToString();
                        RmkBox.Text = DB.sqlDR["REMARK"].ToString();
                    }
                    DB.conn.Close();
                    DB.sqlcmd.Dispose();
                    DB.sqlDR.Close();
                }
                catch (IndexOutOfRangeException)
                {
                }
                catch (Exception)
                {
                    DB.transaction.Rollback();
                    throw;
                }
                finally
                {
                    DB.sqlDR.Close();
                    DB.conn.Dispose();
                    DB.conn.Close();
                }
            }
        }

        private void CNameBox_MouseClick(object sender, MouseEventArgs e)
        {
            Cust_POPUP cust_POPUP = new Cust_POPUP();
            cust_POPUP.ShowDialog();
            CNameBox.Text = Custname;

        }

        private void MaxStockBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void SafeStockBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void OrderBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void MinOrderBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void MaxOrderBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void BaseCostBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
    }
}
