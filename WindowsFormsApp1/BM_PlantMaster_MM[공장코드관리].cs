using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class BM_PlantMaster_MM : Form
    {
        bool addCheck = true;
        public BM_PlantMaster_MM()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

        }
        public delegate void FormClosed(string str);
        public event FormClosed FormCloseEvent;
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.FormCloseEvent("공장 코드 관리");
            this.Close();

        }


        private void PM_PlantMaster_MM_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string strConn = "Data Source=222.235.141.8; Initial Catalog=HIAIRMES;User ID=kfqb;Password=2211"; ;
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();

            SqlDataAdapter Comp = new SqlDataAdapter("SELECT COMP_NAME FROM TB_CompanyMaster", conn);
            Comp.SelectCommand.CommandType = CommandType.Text;

            Comp.Fill(ds);
            Comp.Dispose();
            cboSearch_CompName.Items.Add("전체");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                cboSearch_CompName.Items.Add(dr["COMP_NAME"]);
                cboComp_Name.Items.Add(dr["COMP_NAME"]);
            }
            ds.Clear();
            conn.Close();

            cboSearch_CompName.FlatStyle = FlatStyle.Popup;
            cboSearch_CompName.BackColor = Color.Ivory;
            cboSearch_CompName.SelectedIndex = 0;

            cboSearch_UseFlag.FlatStyle = FlatStyle.Popup;
            cboSearch_UseFlag.BackColor = Color.Ivory;
            cboSearch_UseFlag.SelectedIndex = 0;

            cboComp_Name.FlatStyle = FlatStyle.Popup;
            cboComp_Name.BackColor = Color.Ivory;

            cboUseFlag.FlatStyle = FlatStyle.Popup;
            cboUseFlag.BackColor = Color.Ivory;
        }

        private void btnDoSearch_Click(object sender, EventArgs e)
        {
            txtPlant_Code.ReadOnly = true;
            cboComp_Name.Enabled = false;
            dataGridView1.Visible = true;
            DataSet PlantSelect = GetData();
            if (addCheck)
            {
                var chkCol = new DataGridViewCheckBoxColumn
                {
                    Name = "check",
                    HeaderText = "선택"

                };
                dataGridView1.Columns.Add(chkCol);
                addCheck = false;
            }
            dataGridView1.DataSource = PlantSelect.Tables[0];
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            for (int i = 1; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
            }
            dataGridView1.Columns["check"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Refresh();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtPlant_Code.ReadOnly = false;
            cboComp_Name.Enabled = true;
            txtPlant_Code.Clear();
            txtPlant_Name.Clear();
            txtRemark.Clear();
            cboComp_Name.SelectedIndex = 0;
            cboUseFlag.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("작성 내용을 저장하시겠습니까?", "데이터 저장", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                int result_value = 0;
                try
                {
                    string strConn = "Data Source=222.235.141.8; Initial Catalog=HIAIRMES;User ID=kfqb;Password=2211"; ;
                    SqlConnection conn = new SqlConnection(strConn);
                    if ((txtPlant_Code.ReadOnly == false) && (cboComp_Name.Enabled = true))
                    {
                        if (txtPlant_Code.Text.Length == 0)
                        {
                            MessageBox.Show("공장코드는 필수 입력 항목입니다!!");
                            return;
                        }
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (txtPlant_Code.Text == dataGridView1.Rows[i].Cells[1].Value.ToString())
                            {
                                MessageBox.Show("공장코드는 중복될 수 없습니다.");
                                return;
                            }
                        }

                        SqlCommand PlantInsert = new SqlCommand("PM_PlantMaster_MM_I1", conn);
                        PlantInsert.CommandType = CommandType.StoredProcedure;

                        PlantInsert.Parameters.AddWithValue("@COMP_NAME",   cboComp_Name.SelectedItem);
                        PlantInsert.Parameters.AddWithValue("@PLANT_CODE",  txtPlant_Code.Text);
                        PlantInsert.Parameters.AddWithValue("@PLANT_NAME", (txtPlant_Name.Text == null) ? "" : txtPlant_Name.Text);
                        PlantInsert.Parameters.AddWithValue("@USEFLAG",     cboUseFlag.SelectedItem);
                        PlantInsert.Parameters.AddWithValue("@REMARK",     (txtRemark.Text == null) ? "" : txtRemark.Text);

                        PlantInsert.Parameters.Add("@ERROR_CODE", SqlDbType.Int);
                        PlantInsert.Parameters["@ERROR_CODE"].Direction = ParameterDirection.Output;

                        conn.Open();
                        PlantInsert.ExecuteNonQuery();


                        result_value = Convert.ToInt32(PlantInsert.Parameters["@ERROR_CODE"].Value);
                        PlantInsert.Dispose();
                    }
                    else
                    {
                        SqlCommand PlantUpdate = new SqlCommand("PM_PlantMaster_MM_U1", conn);
                        PlantUpdate.CommandType = CommandType.StoredProcedure;

                        PlantUpdate.Parameters.AddWithValue("@PLANT_CODE",  txtPlant_Code.Text);
                        PlantUpdate.Parameters.AddWithValue("@PLANT_NAME", (txtPlant_Name.Text == null) ? "" : txtPlant_Name.Text);
                        PlantUpdate.Parameters.AddWithValue("@USEFLAG",     cboUseFlag.SelectedItem);
                        PlantUpdate.Parameters.AddWithValue("@REMARK",     (txtRemark.Text == null) ? "" : txtRemark.Text);

                        conn.Open();
                        PlantUpdate.ExecuteNonQuery();
                        PlantUpdate.Dispose();
                    }
                    if (result_value == 1)
                    {
                        MessageBox.Show("공장코드는 중복될 수 없습니다.");
                        MessageBox.Show("데이터 저장에 실패했습니다.");
                        return;
                    }

                    else
                    {
                        MessageBox.Show("데이터를 저장했습니다.");
                        cboComp_Name.Enabled = true;
                        txtPlant_Code.ReadOnly = true;
                        conn.Dispose();
                        conn.Close();
                        btnDoSearch_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("선택된 데이터를 삭제하시겠습니까?", "데이터 삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                try
                {
                    string connectionString = "Data Source=222.235.141.8; Initial Catalog=HIAIRMES;User ID=kfqb;Password=2211";
                    SqlConnection sqlConn = new SqlConnection(connectionString);
                    int CheckCount = 0;

                    dataGridView1.EndEdit();
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {

                        bool isChecked = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);
                        if (isChecked)
                        {
                            SqlCommand sqlComm = new SqlCommand();
                            sqlComm.Connection = sqlConn;
                            sqlComm.CommandText = "delete from TB_PlantMaster where PLANT_CODE=@PLANT_CODE";
                            sqlComm.Parameters.AddWithValue("@PLANT_CODE", dataGridView1.Rows[i].Cells[2].Value.ToString());
                            sqlConn.Open();
                            sqlComm.ExecuteNonQuery();
                            sqlConn.Close();
                            CheckCount++;
                        }
                    }
                    if (CheckCount > 0)
                    {
                        MessageBox.Show("데이터 삭제를 완료했습니다.");
                    }
                    else
                    {
                        MessageBox.Show("삭제할 데이터를 선택 후 삭제 버튼을 눌러주세요.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNew_Click(sender, e);
                btnDoSearch_Click(sender, e);
                txtPlant_Code.ReadOnly = true;
                cboComp_Name.Enabled = false;
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }       

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cboComp_Name.Text = null;
            txtPlant_Code.Text = null;
            txtPlant_Name.Text = null;
            txtRemark.Text = null;
            cboUseFlag.Text = null;

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            cboComp_Name.Enabled = false;
            txtPlant_Code.ReadOnly = true;

            string strConn = "Data Source=222.235.141.8; Initial Catalog=HIAIRMES;User ID=kfqb;Password=2211"; ;
            SqlConnection conn = new SqlConnection(strConn);

            conn.Open();

            SqlCommand cmd = new SqlCommand("PM_PlantMaster_MM_S2", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter("@COMP_NAME", SqlDbType.VarChar, 100);
            param.Value = selectedRow.Cells[1].Value.ToString();
            cmd.Parameters.Add(param);

            param = new SqlParameter("@PLANT_CODE", SqlDbType.VarChar, 20);
            param.Value = selectedRow.Cells[2].Value.ToString();
            cmd.Parameters.Add(param);

            SqlDataReader mdr = cmd.ExecuteReader();
            while (mdr.Read())
            {
                cboComp_Name.SelectedItem = (string)mdr["COMP_NAME"];
                txtPlant_Code.Text        = (string)mdr["PLANT_CODE"];
                txtPlant_Name.Text        = (mdr["PLANT_NAME"] == null) ? string.Empty : mdr["PLANT_NAME"].ToString();
                cboUseFlag.SelectedItem   = (mdr["USEFLAG"] == null) ? string.Empty : mdr["USEFLAG"].ToString();
                txtRemark.Text            = (mdr["REMARK"] == null) ? string.Empty : mdr["REMARK"].ToString();
            }
            mdr.Close();
            conn.Close();
        }
        private DataSet GetData()
        {
            DataSet ds = new DataSet();
            try
            {
                string strConn = "Data Source=222.235.141.8; Initial Catalog=HIAIRMES;User ID=kfqb;Password=2211"; ;
                SqlConnection conn = new SqlConnection(strConn);

                conn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter("PM_PlantMaster_MM_S1", conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.AddWithValue("@COMP_NAME", cboSearch_CompName.SelectedItem.ToString());
                adapter.SelectCommand.Parameters.AddWithValue("@PLANT_NAME", txtSearch_PlantName.Text);
                adapter.SelectCommand.Parameters.AddWithValue("@USEFLAG", cboSearch_UseFlag.SelectedItem.ToString());

                adapter.Fill(ds);
                adapter.Dispose();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ds;
        }

        private void txtSearch_PlantName_MouseClick(object sender, MouseEventArgs e)
        {
            txtSearch_PlantName.SelectionStart = txtSearch_PlantName.Text.Length;
        }

        private void txtPlant_Code_MouseClick(object sender, MouseEventArgs e)
        {
            txtPlant_Code.SelectionStart = txtPlant_Code.Text.Length;
        }

        private void txtPlant_Name_MouseClick(object sender, MouseEventArgs e)
        {
            txtPlant_Name.SelectionStart = txtPlant_Name.Text.Length;
        }

        private void txtRemark_MouseClick(object sender, MouseEventArgs e)
        {
            txtRemark.SelectionStart = txtRemark.Text.Length;
        }
    }
}
