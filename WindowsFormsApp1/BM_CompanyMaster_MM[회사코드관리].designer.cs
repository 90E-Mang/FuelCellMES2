
namespace WindowsFormsApp1
{
    partial class BM_CompanyMaster_MM
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCompany_Code = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCompany_Name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRegister_No = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCorporate_No = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPresident_Name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBirthday = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtComp_Type = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtComp_Industy = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCompany_Zip = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCompany_Address = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCompany_Tel = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCompany_Fax = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCompany_Area = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtEmail_Address = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cboUseFlag = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.btnDoSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchCompany = new System.Windows.Forms.TextBox();
            this.CompanyList = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.ControlPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ControlPanel.Controls.Add(this.panel2);
            this.ControlPanel.Controls.Add(this.btnDelete);
            this.ControlPanel.Controls.Add(this.btnNew);
            this.ControlPanel.Controls.Add(this.btnQuit);
            this.ControlPanel.Controls.Add(this.btnSave);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(597, 119);
            this.ControlPanel.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(593, 27);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.label19);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(593, 27);
            this.panel3.TabIndex = 5;
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
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label17.Location = new System.Drawing.Point(20, 5);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 19);
            this.label17.TabIndex = 0;
            this.label17.Text = "상세내용";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.LightBlue;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(420, 48);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 46);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.BackColor = System.Drawing.Color.LightBlue;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Location = new System.Drawing.Point(339, 48);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 46);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "신규";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.BackColor = System.Drawing.Color.LightBlue;
            this.btnQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuit.Location = new System.Drawing.Point(501, 48);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 46);
            this.btnQuit.TabIndex = 3;
            this.btnQuit.Text = "종료";
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.LightBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(258, 48);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 46);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(34, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "회사코드";
            // 
            // txtCompany_Code
            // 
            this.txtCompany_Code.Location = new System.Drawing.Point(93, 176);
            this.txtCompany_Code.Name = "txtCompany_Code";
            this.txtCompany_Code.Size = new System.Drawing.Size(185, 21);
            this.txtCompany_Code.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "회사명";
            // 
            // txtCompany_Name
            // 
            this.txtCompany_Name.Location = new System.Drawing.Point(93, 203);
            this.txtCompany_Name.Name = "txtCompany_Name";
            this.txtCompany_Name.Size = new System.Drawing.Size(426, 21);
            this.txtCompany_Name.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 233);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "사업자번호";
            // 
            // txtRegister_No
            // 
            this.txtRegister_No.Location = new System.Drawing.Point(93, 230);
            this.txtRegister_No.Name = "txtRegister_No";
            this.txtRegister_No.Size = new System.Drawing.Size(185, 21);
            this.txtRegister_No.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(315, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "법인번호";
            // 
            // txtCorporate_No
            // 
            this.txtCorporate_No.Location = new System.Drawing.Point(374, 230);
            this.txtCorporate_No.Name = "txtCorporate_No";
            this.txtCorporate_No.Size = new System.Drawing.Size(145, 21);
            this.txtCorporate_No.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 260);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "대표자명";
            // 
            // txtPresident_Name
            // 
            this.txtPresident_Name.Location = new System.Drawing.Point(93, 257);
            this.txtPresident_Name.Name = "txtPresident_Name";
            this.txtPresident_Name.Size = new System.Drawing.Size(143, 21);
            this.txtPresident_Name.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(315, 260);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "생년월일";
            // 
            // txtBirthday
            // 
            this.txtBirthday.Location = new System.Drawing.Point(374, 257);
            this.txtBirthday.Name = "txtBirthday";
            this.txtBirthday.Size = new System.Drawing.Size(145, 21);
            this.txtBirthday.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 287);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "업태";
            // 
            // txtComp_Type
            // 
            this.txtComp_Type.Location = new System.Drawing.Point(93, 285);
            this.txtComp_Type.Name = "txtComp_Type";
            this.txtComp_Type.Size = new System.Drawing.Size(185, 21);
            this.txtComp_Type.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(339, 287);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "종목";
            // 
            // txtComp_Industy
            // 
            this.txtComp_Industy.Location = new System.Drawing.Point(374, 284);
            this.txtComp_Industy.Name = "txtComp_Industy";
            this.txtComp_Industy.Size = new System.Drawing.Size(145, 21);
            this.txtComp_Industy.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 314);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "우편번호";
            // 
            // txtCompany_Zip
            // 
            this.txtCompany_Zip.Location = new System.Drawing.Point(93, 311);
            this.txtCompany_Zip.Name = "txtCompany_Zip";
            this.txtCompany_Zip.Size = new System.Drawing.Size(143, 21);
            this.txtCompany_Zip.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 341);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 12);
            this.label11.TabIndex = 19;
            this.label11.Text = "사업자주소";
            // 
            // txtCompany_Address
            // 
            this.txtCompany_Address.Location = new System.Drawing.Point(93, 338);
            this.txtCompany_Address.Name = "txtCompany_Address";
            this.txtCompany_Address.Size = new System.Drawing.Size(426, 21);
            this.txtCompany_Address.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(34, 368);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "전화번호";
            // 
            // txtCompany_Tel
            // 
            this.txtCompany_Tel.Location = new System.Drawing.Point(93, 365);
            this.txtCompany_Tel.Name = "txtCompany_Tel";
            this.txtCompany_Tel.Size = new System.Drawing.Size(143, 21);
            this.txtCompany_Tel.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(315, 371);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 12);
            this.label13.TabIndex = 23;
            this.label13.Text = "팩스번호";
            // 
            // txtCompany_Fax
            // 
            this.txtCompany_Fax.Location = new System.Drawing.Point(374, 368);
            this.txtCompany_Fax.Name = "txtCompany_Fax";
            this.txtCompany_Fax.Size = new System.Drawing.Size(145, 21);
            this.txtCompany_Fax.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(58, 395);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 12);
            this.label14.TabIndex = 25;
            this.label14.Text = "지역";
            // 
            // txtCompany_Area
            // 
            this.txtCompany_Area.Location = new System.Drawing.Point(93, 392);
            this.txtCompany_Area.Name = "txtCompany_Area";
            this.txtCompany_Area.Size = new System.Drawing.Size(143, 21);
            this.txtCompany_Area.TabIndex = 26;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(22, 485);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 12);
            this.label15.TabIndex = 27;
            this.label15.Text = "이메일주소";
            // 
            // txtEmail_Address
            // 
            this.txtEmail_Address.Location = new System.Drawing.Point(93, 482);
            this.txtEmail_Address.Name = "txtEmail_Address";
            this.txtEmail_Address.Size = new System.Drawing.Size(275, 21);
            this.txtEmail_Address.TabIndex = 28;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(34, 437);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 12);
            this.label16.TabIndex = 29;
            this.label16.Text = "사용유무";
            // 
            // cboUseFlag
            // 
            this.cboUseFlag.BackColor = System.Drawing.SystemColors.Window;
            this.cboUseFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUseFlag.FormattingEnabled = true;
            this.cboUseFlag.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.cboUseFlag.Location = new System.Drawing.Point(93, 434);
            this.cboUseFlag.Name = "cboUseFlag";
            this.cboUseFlag.Size = new System.Drawing.Size(121, 20);
            this.cboUseFlag.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.btnDoSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtSearchCompany);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 119);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel5.Controls.Add(this.label20);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(456, 27);
            this.panel5.TabIndex = 3;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(31, 5);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 19);
            this.label20.TabIndex = 1;
            this.label20.Text = "조회내용";
            // 
            // btnDoSearch
            // 
            this.btnDoSearch.BackColor = System.Drawing.Color.LightBlue;
            this.btnDoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoSearch.Location = new System.Drawing.Point(247, 71);
            this.btnDoSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDoSearch.Name = "btnDoSearch";
            this.btnDoSearch.Size = new System.Drawing.Size(90, 29);
            this.btnDoSearch.TabIndex = 2;
            this.btnDoSearch.Text = "조회";
            this.btnDoSearch.UseVisualStyleBackColor = false;
            this.btnDoSearch.Click += new System.EventHandler(this.btnDoSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "회사명";
            // 
            // txtSearchCompany
            // 
            this.txtSearchCompany.Location = new System.Drawing.Point(66, 46);
            this.txtSearchCompany.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchCompany.Name = "txtSearchCompany";
            this.txtSearchCompany.Size = new System.Drawing.Size(271, 21);
            this.txtSearchCompany.TabIndex = 0;
            // 
            // CompanyList
            // 
            this.CompanyList.AllowUserToAddRows = false;
            this.CompanyList.AllowUserToDeleteRows = false;
            this.CompanyList.AllowUserToResizeRows = false;
            this.CompanyList.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CompanyList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CompanyList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CompanyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CompanyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CompanyList.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.CompanyList.Location = new System.Drawing.Point(0, 119);
            this.CompanyList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CompanyList.Name = "CompanyList";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.CompanyList.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CompanyList.RowHeadersVisible = false;
            this.CompanyList.RowHeadersWidth = 51;
            this.CompanyList.RowTemplate.Height = 27;
            this.CompanyList.Size = new System.Drawing.Size(460, 424);
            this.CompanyList.TabIndex = 1;
            this.CompanyList.Visible = false;
            this.CompanyList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CompanyList_CellClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer1.Panel1.Controls.Add(this.CompanyList);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Panel2.Controls.Add(this.cboUseFlag);
            this.splitContainer1.Panel2.Controls.Add(this.label16);
            this.splitContainer1.Panel2.Controls.Add(this.txtEmail_Address);
            this.splitContainer1.Panel2.Controls.Add(this.label15);
            this.splitContainer1.Panel2.Controls.Add(this.txtCompany_Area);
            this.splitContainer1.Panel2.Controls.Add(this.label14);
            this.splitContainer1.Panel2.Controls.Add(this.txtCompany_Fax);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Panel2.Controls.Add(this.txtCompany_Tel);
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Panel2.Controls.Add(this.txtCompany_Address);
            this.splitContainer1.Panel2.Controls.Add(this.label11);
            this.splitContainer1.Panel2.Controls.Add(this.txtCompany_Zip);
            this.splitContainer1.Panel2.Controls.Add(this.label10);
            this.splitContainer1.Panel2.Controls.Add(this.txtComp_Industy);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.txtComp_Type);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.txtBirthday);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.txtPresident_Name);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.txtCorporate_No);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.txtRegister_No);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.txtCompany_Name);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.txtCompany_Code);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.ControlPanel);
            this.splitContainer1.Size = new System.Drawing.Size(1070, 547);
            this.splitContainer1.SplitterDistance = 464;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel4.Controls.Add(this.label18);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 119);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(597, 27);
            this.panel4.TabIndex = 31;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label18.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(22, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(129, 19);
            this.label18.TabIndex = 1;
            this.label18.Text = "기본등록사항";
            // 
            // CM_CompanyCode_MM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 547);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CM_CompanyCode_MM";
            this.Text = "회사 코드 관리";
            this.Load += new System.EventHandler(this.CM_CompanyCode_MM_Load);
            this.ControlPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCompany_Code;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCompany_Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRegister_No;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCorporate_No;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPresident_Name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBirthday;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtComp_Type;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtComp_Industy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCompany_Zip;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCompany_Address;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCompany_Tel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCompany_Fax;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCompany_Area;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtEmail_Address;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboUseFlag;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDoSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchCompany;
        private System.Windows.Forms.DataGridView CompanyList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label18;
    }
}