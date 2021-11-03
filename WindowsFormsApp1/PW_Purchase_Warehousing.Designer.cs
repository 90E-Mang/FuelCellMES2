
namespace WindowsFormsApp1
{
    partial class PW_Purchase_Warehousing
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.CustNameTxtBox = new System.Windows.Forms.TextBox();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.InputTxtBox = new System.Windows.Forms.TextBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SelButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.Add_Warehousing_Button = new System.Windows.Forms.Button();
            this.DelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 90);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel6);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel7);
            this.splitContainer1.Panel2.Controls.Add(this.panel5);
            this.splitContainer1.Size = new System.Drawing.Size(1392, 551);
            this.splitContainer1.SplitterDistance = 509;
            this.splitContainer1.TabIndex = 9;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dataGridView1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 19);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(505, 528);
            this.panel6.TabIndex = 21;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(505, 528);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(505, 19);
            this.panel2.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "※ 구매 입고 정보";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.dataGridView2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 19);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(875, 528);
            this.panel7.TabIndex = 22;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(875, 528);
            this.dataGridView2.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.ForeColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(875, 19);
            this.panel5.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(7, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "※ 구매 입고 상세정보";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.EndDate);
            this.panel1.Controls.Add(this.StartDate);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel14);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.CustNameTxtBox);
            this.panel1.Controls.Add(this.panel15);
            this.panel1.Controls.Add(this.InputTxtBox);
            this.panel1.Controls.Add(this.panel13);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1392, 90);
            this.panel1.TabIndex = 8;
            // 
            // EndDate
            // 
            this.EndDate.CalendarFont = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EndDate.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.EndDate.Location = new System.Drawing.Point(705, 40);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(112, 22);
            this.EndDate.TabIndex = 25;
            this.EndDate.Value = new System.DateTime(2021, 9, 24, 22, 16, 41, 0);
            // 
            // StartDate
            // 
            this.StartDate.CalendarFont = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.StartDate.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.StartDate.Location = new System.Drawing.Point(549, 40);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(112, 22);
            this.StartDate.TabIndex = 25;
            this.StartDate.Value = new System.DateTime(2021, 9, 1, 0, 0, 0, 0);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.SkyBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(667, 38);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(32, 26);
            this.panel4.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.SkyBlue;
            this.label4.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(1, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = " ~ ";
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.SkyBlue;
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel14.Controls.Add(this.label1);
            this.panel14.Location = new System.Drawing.Point(423, 38);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(120, 26);
            this.panel14.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.SkyBlue;
            this.label1.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "구매 입고 일자";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.SkyBlue;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.label6);
            this.panel8.Location = new System.Drawing.Point(231, 38);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(80, 26);
            this.panel8.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.SkyBlue;
            this.label6.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(1, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "거래처 명";
            // 
            // CustNameTxtBox
            // 
            this.CustNameTxtBox.Location = new System.Drawing.Point(317, 41);
            this.CustNameTxtBox.Name = "CustNameTxtBox";
            this.CustNameTxtBox.Size = new System.Drawing.Size(100, 21);
            this.CustNameTxtBox.TabIndex = 20;
            this.CustNameTxtBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CustNameTxtBox_MouseClick);
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.SkyBlue;
            this.panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel15.Controls.Add(this.label3);
            this.panel15.Location = new System.Drawing.Point(39, 38);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(80, 26);
            this.panel15.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.SkyBlue;
            this.label3.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(1, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "입고 번호";
            // 
            // InputTxtBox
            // 
            this.InputTxtBox.Location = new System.Drawing.Point(125, 41);
            this.InputTxtBox.Name = "InputTxtBox";
            this.InputTxtBox.Size = new System.Drawing.Size(100, 21);
            this.InputTxtBox.TabIndex = 20;
            // 
            // panel13
            // 
            this.panel13.AutoSize = true;
            this.panel13.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel13.Controls.Add(this.label11);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.ForeColor = System.Drawing.Color.White;
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(1123, 19);
            this.panel13.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(7, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(202, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "※ 구매 입고 조회 조건부";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.SelButton);
            this.panel3.Controls.Add(this.UpdateButton);
            this.panel3.Controls.Add(this.Add_Warehousing_Button);
            this.panel3.Controls.Add(this.DelButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1123, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(267, 88);
            this.panel3.TabIndex = 18;
            // 
            // SelButton
            // 
            this.SelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelButton.BackColor = System.Drawing.Color.LightBlue;
            this.SelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelButton.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SelButton.Location = new System.Drawing.Point(12, 15);
            this.SelButton.Name = "SelButton";
            this.SelButton.Size = new System.Drawing.Size(54, 57);
            this.SelButton.TabIndex = 2;
            this.SelButton.Text = "조회";
            this.SelButton.UseVisualStyleBackColor = false;
            this.SelButton.Click += new System.EventHandler(this.SelButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateButton.BackColor = System.Drawing.Color.LightBlue;
            this.UpdateButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateButton.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.UpdateButton.Location = new System.Drawing.Point(68, 15);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(54, 57);
            this.UpdateButton.TabIndex = 3;
            this.UpdateButton.Text = "수정";
            this.UpdateButton.UseVisualStyleBackColor = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // Add_Warehousing_Button
            // 
            this.Add_Warehousing_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Add_Warehousing_Button.BackColor = System.Drawing.Color.LightBlue;
            this.Add_Warehousing_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Add_Warehousing_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Add_Warehousing_Button.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Add_Warehousing_Button.Location = new System.Drawing.Point(124, 15);
            this.Add_Warehousing_Button.Name = "Add_Warehousing_Button";
            this.Add_Warehousing_Button.Size = new System.Drawing.Size(65, 57);
            this.Add_Warehousing_Button.TabIndex = 4;
            this.Add_Warehousing_Button.Text = "입고추가";
            this.Add_Warehousing_Button.UseVisualStyleBackColor = false;
            this.Add_Warehousing_Button.Click += new System.EventHandler(this.Add_WarehousingButton_Click);
            // 
            // DelButton
            // 
            this.DelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DelButton.BackColor = System.Drawing.Color.LightBlue;
            this.DelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DelButton.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.DelButton.Location = new System.Drawing.Point(191, 15);
            this.DelButton.Name = "DelButton";
            this.DelButton.Size = new System.Drawing.Size(65, 57);
            this.DelButton.TabIndex = 5;
            this.DelButton.Text = "입고취소";
            this.DelButton.UseVisualStyleBackColor = false;
            this.DelButton.Click += new System.EventHandler(this.DelButton_Click);
            // 
            // PW_Purchase_Warehousing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 641);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "PW_Purchase_Warehousing";
            this.Text = "구매 입고 관리";
            this.Load += new System.EventHandler(this.PW_Purchase_Warehousing_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker EndDate;
        private System.Windows.Forms.DateTimePicker StartDate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox InputTxtBox;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button SelButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button Add_Warehousing_Button;
        private System.Windows.Forms.Button DelButton;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox CustNameTxtBox;
    }
}