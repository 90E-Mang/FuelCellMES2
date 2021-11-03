
namespace WindowsFormsApp1
{
    partial class BM_PlantMaster_MM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtSearch_PlantName = new System.Windows.Forms.TextBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnDoSearch = new System.Windows.Forms.Button();
            this.cboSearch_UseFlag = new System.Windows.Forms.ComboBox();
            this.cboSearch_CompName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.txtPlant_Code = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPlant_Name = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboUseFlag = new System.Windows.Forms.ComboBox();
            this.cboComp_Name = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Highlight;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1311, 1061);
            this.splitContainer1.SplitterDistance = 330;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.splitContainer2.Panel1.Controls.Add(this.txtSearch_PlantName);
            this.splitContainer2.Panel1.Controls.Add(this.btnQuit);
            this.splitContainer2.Panel1.Controls.Add(this.btnDoSearch);
            this.splitContainer2.Panel1.Controls.Add(this.cboSearch_UseFlag);
            this.splitContainer2.Panel1.Controls.Add(this.cboSearch_CompName);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.panel5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.splitContainer2.Panel2.Controls.Add(this.txtPlant_Code);
            this.splitContainer2.Panel2.Controls.Add(this.label8);
            this.splitContainer2.Panel2.Controls.Add(this.txtPlant_Name);
            this.splitContainer2.Panel2.Controls.Add(this.btnDelete);
            this.splitContainer2.Panel2.Controls.Add(this.btnSave);
            this.splitContainer2.Panel2.Controls.Add(this.btnNew);
            this.splitContainer2.Panel2.Controls.Add(this.txtRemark);
            this.splitContainer2.Panel2.Controls.Add(this.label7);
            this.splitContainer2.Panel2.Controls.Add(this.cboUseFlag);
            this.splitContainer2.Panel2.Controls.Add(this.cboComp_Name);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.panel3);
            this.splitContainer2.Size = new System.Drawing.Size(1311, 330);
            this.splitContainer2.SplitterDistance = 150;
            this.splitContainer2.TabIndex = 0;
            // 
            // txtSearch_PlantName
            // 
            this.txtSearch_PlantName.Location = new System.Drawing.Point(340, 73);
            this.txtSearch_PlantName.Name = "txtSearch_PlantName";
            this.txtSearch_PlantName.Size = new System.Drawing.Size(192, 21);
            this.txtSearch_PlantName.TabIndex = 42;
            this.txtSearch_PlantName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtSearch_PlantName_MouseClick);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.BackColor = System.Drawing.Color.LightBlue;
            this.btnQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuit.Location = new System.Drawing.Point(1084, 61);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(91, 43);
            this.btnQuit.TabIndex = 41;
            this.btnQuit.Text = "종료";
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnDoSearch
            // 
            this.btnDoSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoSearch.BackColor = System.Drawing.Color.LightBlue;
            this.btnDoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoSearch.Location = new System.Drawing.Point(987, 61);
            this.btnDoSearch.Name = "btnDoSearch";
            this.btnDoSearch.Size = new System.Drawing.Size(91, 43);
            this.btnDoSearch.TabIndex = 40;
            this.btnDoSearch.Text = "조회";
            this.btnDoSearch.UseVisualStyleBackColor = false;
            this.btnDoSearch.Click += new System.EventHandler(this.btnDoSearch_Click);
            // 
            // cboSearch_UseFlag
            // 
            this.cboSearch_UseFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearch_UseFlag.FormattingEnabled = true;
            this.cboSearch_UseFlag.Items.AddRange(new object[] {
            "전체",
            "Y",
            "N"});
            this.cboSearch_UseFlag.Location = new System.Drawing.Point(643, 74);
            this.cboSearch_UseFlag.Name = "cboSearch_UseFlag";
            this.cboSearch_UseFlag.Size = new System.Drawing.Size(101, 20);
            this.cboSearch_UseFlag.TabIndex = 39;
            // 
            // cboSearch_CompName
            // 
            this.cboSearch_CompName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearch_CompName.FormattingEnabled = true;
            this.cboSearch_CompName.Location = new System.Drawing.Point(69, 74);
            this.cboSearch_CompName.Name = "cboSearch_CompName";
            this.cboSearch_CompName.Size = new System.Drawing.Size(196, 20);
            this.cboSearch_CompName.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(584, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 36;
            this.label3.Text = "사용구분";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "공장명";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "회사";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel5.Controls.Add(this.label20);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1307, 27);
            this.panel5.TabIndex = 33;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(20, 5);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 19);
            this.label20.TabIndex = 1;
            this.label20.Text = "조회내용";
            // 
            // txtPlant_Code
            // 
            this.txtPlant_Code.Location = new System.Drawing.Point(340, 62);
            this.txtPlant_Code.Name = "txtPlant_Code";
            this.txtPlant_Code.Size = new System.Drawing.Size(159, 21);
            this.txtPlant_Code.TabIndex = 52;
            this.txtPlant_Code.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtPlant_Code_MouseClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(281, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 12);
            this.label8.TabIndex = 51;
            this.label8.Text = "공장코드";
            // 
            // txtPlant_Name
            // 
            this.txtPlant_Name.Location = new System.Drawing.Point(552, 63);
            this.txtPlant_Name.Name = "txtPlant_Name";
            this.txtPlant_Name.Size = new System.Drawing.Size(192, 21);
            this.txtPlant_Name.TabIndex = 43;
            this.txtPlant_Name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtPlant_Name_MouseClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.LightBlue;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(1181, 77);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 43);
            this.btnDelete.TabIndex = 50;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.LightBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(1084, 77);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 43);
            this.btnSave.TabIndex = 49;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.BackColor = System.Drawing.Color.LightBlue;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Location = new System.Drawing.Point(987, 77);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(91, 43);
            this.btnNew.TabIndex = 48;
            this.btnNew.Text = "신규";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(69, 114);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(675, 21);
            this.txtRemark.TabIndex = 47;
            this.txtRemark.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtRemark_MouseClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 12);
            this.label7.TabIndex = 46;
            this.label7.Text = "비고";
            // 
            // cboUseFlag
            // 
            this.cboUseFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUseFlag.FormattingEnabled = true;
            this.cboUseFlag.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.cboUseFlag.Location = new System.Drawing.Point(825, 63);
            this.cboUseFlag.Name = "cboUseFlag";
            this.cboUseFlag.Size = new System.Drawing.Size(101, 20);
            this.cboUseFlag.TabIndex = 45;
            // 
            // cboComp_Name
            // 
            this.cboComp_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComp_Name.FormattingEnabled = true;
            this.cboComp_Name.Location = new System.Drawing.Point(69, 63);
            this.cboComp_Name.Name = "cboComp_Name";
            this.cboComp_Name.Size = new System.Drawing.Size(196, 20);
            this.cboComp_Name.TabIndex = 43;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(766, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 42;
            this.label4.Text = "사용구분";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(505, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "공장명";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 12);
            this.label6.TabIndex = 40;
            this.label6.Text = "회사";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.label19);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1307, 27);
            this.panel3.TabIndex = 34;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(20, 5);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 19);
            this.label19.TabIndex = 0;
            this.label19.Text = "상세내용";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1307, 723);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // PM_PlantMaster_MM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 1061);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PM_PlantMaster_MM";
            this.Text = "공장 코드 관리";
            this.Load += new System.EventHandler(this.PM_PlantMaster_MM_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnDoSearch;
        private System.Windows.Forms.ComboBox cboSearch_UseFlag;
        private System.Windows.Forms.ComboBox cboSearch_CompName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboUseFlag;
        private System.Windows.Forms.ComboBox cboComp_Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSearch_PlantName;
        private System.Windows.Forms.TextBox txtPlant_Code;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPlant_Name;
    }
}