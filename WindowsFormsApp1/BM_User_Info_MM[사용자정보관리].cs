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
    public partial class User_info : Form
    {
        public User_info()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
        }
        public delegate void FormClosed(string str);
        public event FormClosed FormCloseEvent;
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.FormCloseEvent("사용자 정보");
            this.Close();

        }
        // 조회 매서드
        private void select()
        {
            DB.conn.Close();
            dataGridView1.Columns.Clear();

            DB.conn = new SqlConnection(DB.connectionString);
            DB.conn.ConnectionString = DB.connectionString;
            DB.conn.Open();
            //트랜잭션 시작
            DB.transaction = DB.conn.BeginTransaction();
            DB.sqlcmd = new SqlCommand("USER_INFO_S1", DB.conn, DB.transaction);
            DB.sqlcmd.CommandType = CommandType.StoredProcedure;

            DB.sqlcmd.Parameters.AddWithValue("@USER_NAME", textBox1.Text);
            DB.sqlcmd.Parameters.AddWithValue("@USER_ID", textBox2.Text);

            DB.sqlcmd.ExecuteNonQuery();
            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "check",
                HeaderText = "삭제"
            };
            dataGridView1.Columns.Add(chkCol);
            DB.adapter = new SqlDataAdapter(DB.sqlcmd);
            DataSet ds = new DataSet();
            DB.adapter.Fill(ds, "USER_INFO_S1");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "USER_INFO_S1";
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;

            DB.transaction.Commit();
        }
        private void User_info_Load(object sender, EventArgs e)
        {
            try
            {
                select();
                textBox3.ReadOnly = true;
            }
            catch (Exception ex)
            {
                DB.transaction.Rollback();
                MessageBox.Show("조회실패\n" + ex.Message);
            }
            finally
            {
                DB.conn.Close();
            }
        }

        // 조건 조회 버튼
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                select();
                textBox3.ReadOnly = true;
            }
            catch (Exception ex)
            {
                DB.transaction.Rollback();
                MessageBox.Show("조회실패\n" + ex.Message);
            }
            finally
            {
                DB.conn.Close();
            }
        }

        private void txtboxclear()
        {
            textBox3.ReadOnly = false;

            textBox3.Text = null;
            textBox9.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox8.Text = null;
        }
        // 추가 버튼
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                txtboxclear();
                this.button4.Enabled = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //SAVE BUTTON
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox9.Text == "" || textBox4.Text == "" || textBox5.Text == "" ||
                textBox6.Text == "" || textBox7.Text == "")
            {
                MessageBox.Show("비고를 제외한 모든 항목을 작성해주세요.");
            }
            else
            {
                if (textBox3.ReadOnly)
                {
                    try
                    {
                        DB.conn.Close();

                        DB.conn = new SqlConnection(DB.connectionString);
                        DB.conn.ConnectionString = DB.connectionString;
                        DB.conn.Open();
                        //트랜잭션 시작
                        DB.transaction = DB.conn.BeginTransaction();
                        DB.sqlcmd = new SqlCommand("USER_INFO_U1", DB.conn, DB.transaction);
                        DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                        DB.sqlcmd.Parameters.AddWithValue("@USER_ID", textBox3.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@USER_PW", textBox9.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@USER_NAME", textBox4.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@WORKPART", textBox5.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@PHONE_NUM", textBox6.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@EMAIL", textBox7.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@RMK", textBox8.Text);

                        DB.sqlcmd.ExecuteNonQuery();

                        DB.transaction.Commit();
                        MessageBox.Show("업데이트 되었습니다.");
                    }
                    catch (Exception ex)
                    {
                        DB.transaction.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        DB.conn.Close();
                        select();
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
                        DB.sqlcmd = new SqlCommand("USER_INFO_I1", DB.conn, DB.transaction);
                        DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                        DB.sqlcmd.Parameters.AddWithValue("@USER_ID", textBox3.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@USER_PW", textBox9.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@USER_NAME", textBox4.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@WORKPART", textBox5.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@PHONE_NUM", textBox6.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@EMAIL", textBox7.Text);
                        DB.sqlcmd.Parameters.AddWithValue("@RMK", textBox8.Text);

                        DB.sqlcmd.ExecuteNonQuery();

                        DB.transaction.Commit();
                        MessageBox.Show("저장되었습니다.");

                        textBox3.ReadOnly = true;

                    }
                    catch (Exception)
                    {
                        DB.transaction.Rollback();
                        MessageBox.Show("사용자 ID가 중복되었습니다.");
                    }
                    finally
                    {
                        DB.conn.Close();
                        select();
                    }
                }
            }
        }
        //DELETE BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.ReadOnly = false;
            DB.conn.Close();

            DB.conn = new SqlConnection(DB.connectionString);
            DB.conn.ConnectionString = DB.connectionString;
            DB.conn.Open();
            try
            {
                dataGridView1.EndEdit();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);

                    if (isChecked)
                    {
                        try
                        {
                            DB.transaction = DB.conn.BeginTransaction();
                            DB.sqlcmd = new SqlCommand("USER_INFO_D1", DB.conn, DB.transaction);
                            DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                            DB.sqlcmd.Parameters.AddWithValue("@USER_ID", dataGridView1.Rows[i].Cells["아이디"].Value.ToString());

                            DB.sqlcmd.ExecuteNonQuery();

                            DB.transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            DB.transaction.Rollback();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                MessageBox.Show("삭제되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DB.conn.Close();
                txtboxclear();
                select();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.ReadOnly = true;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                try
                {
                    DB.conn = new SqlConnection(DB.connectionString);
                    //DB.conn.ConnectionString = DB.connectionString;
                    DB.conn.Open();

                    //트랜잭션 시작
                    DB.transaction = DB.conn.BeginTransaction();
                    DB.sqlcmd = new SqlCommand("USER_INFO_S3", DB.conn, DB.transaction);
                    DB.sqlcmd.CommandType = CommandType.StoredProcedure;

                    DB.sqlcmd.Parameters.AddWithValue("@USER_ID", row.Cells["아이디"].Value.ToString());
                    //DB.sqlcmd.Parameters.AddWithValue("@USER_ID", row.Cells["성명"].Value.ToString());

                    DB.sqlcmd.ExecuteNonQuery();

                    DB.sqlDR = DB.sqlcmd.ExecuteReader();
                    while (DB.sqlDR.Read())
                    {
                        textBox3.Text = DB.sqlDR["USER_ID"].ToString();
                        textBox9.Text = DB.sqlDR["USER_PW"].ToString();
                        textBox4.Text = DB.sqlDR["USER_NAME"].ToString();
                        textBox5.Text = DB.sqlDR["WORKPART"].ToString();
                        textBox6.Text = DB.sqlDR["PHONE_NUM"].ToString();
                        textBox7.Text = DB.sqlDR["EMAIL"].ToString();
                        textBox8.Text = DB.sqlDR["RMK"].ToString();
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
                    DB.conn.Close();
                }
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
    }
}
