
namespace WindowsFormsApp1
{
    partial class MainScreen
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.보기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.거래처정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.공장코드관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.공통코드관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.사용자정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.품목정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.회사코드관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.구매자재관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.구매요청관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.자재발주관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.구매입고관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.자재재고관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.자재수불현황ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.자재불출요청ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.자재불출관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.생산관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.품질관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.제품관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.시스템관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.로그아웃ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.보기ToolStripMenuItem,
            this.관리ToolStripMenuItem,
            this.구매자재관리ToolStripMenuItem,
            this.생산관리ToolStripMenuItem,
            this.품질관리ToolStripMenuItem,
            this.제품관리ToolStripMenuItem,
            this.시스템관리ToolStripMenuItem,
            this.LogOutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1246, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 보기ToolStripMenuItem
            // 
            this.보기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.거래처정보ToolStripMenuItem,
            this.공장코드관리ToolStripMenuItem,
            this.공통코드관리ToolStripMenuItem,
            this.사용자정보ToolStripMenuItem,
            this.품목정보ToolStripMenuItem,
            this.회사코드관리ToolStripMenuItem});
            this.보기ToolStripMenuItem.Name = "보기ToolStripMenuItem";
            this.보기ToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.보기ToolStripMenuItem.Text = "기준정보";
            // 
            // 거래처정보ToolStripMenuItem
            // 
            this.거래처정보ToolStripMenuItem.Name = "거래처정보ToolStripMenuItem";
            this.거래처정보ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.거래처정보ToolStripMenuItem.Text = "거래처 관리";
            this.거래처정보ToolStripMenuItem.Click += new System.EventHandler(this.거래처정보ToolStripMenuItem_Click);
            // 
            // 공장코드관리ToolStripMenuItem
            // 
            this.공장코드관리ToolStripMenuItem.Name = "공장코드관리ToolStripMenuItem";
            this.공장코드관리ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.공장코드관리ToolStripMenuItem.Text = "공장 코드 관리";
            this.공장코드관리ToolStripMenuItem.Click += new System.EventHandler(this.공장코드관리ToolStripMenuItem_Click);
            // 
            // 공통코드관리ToolStripMenuItem
            // 
            this.공통코드관리ToolStripMenuItem.Name = "공통코드관리ToolStripMenuItem";
            this.공통코드관리ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.공통코드관리ToolStripMenuItem.Text = "공통 코드 관리";
            this.공통코드관리ToolStripMenuItem.Click += new System.EventHandler(this.공통코드관리ToolStripMenuItem_Click);
            // 
            // 사용자정보ToolStripMenuItem
            // 
            this.사용자정보ToolStripMenuItem.Name = "사용자정보ToolStripMenuItem";
            this.사용자정보ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.사용자정보ToolStripMenuItem.Text = "사용자 정보";
            this.사용자정보ToolStripMenuItem.Click += new System.EventHandler(this.사용자정보ToolStripMenuItem_Click);
            // 
            // 품목정보ToolStripMenuItem
            // 
            this.품목정보ToolStripMenuItem.Name = "품목정보ToolStripMenuItem";
            this.품목정보ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.품목정보ToolStripMenuItem.Text = "품목 정보";
            this.품목정보ToolStripMenuItem.Click += new System.EventHandler(this.품목정보ToolStripMenuItem_Click);
            // 
            // 회사코드관리ToolStripMenuItem
            // 
            this.회사코드관리ToolStripMenuItem.Name = "회사코드관리ToolStripMenuItem";
            this.회사코드관리ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.회사코드관리ToolStripMenuItem.Text = "회사 코드 관리";
            this.회사코드관리ToolStripMenuItem.Click += new System.EventHandler(this.회사코드관리ToolStripMenuItem_Click);
            // 
            // 관리ToolStripMenuItem
            // 
            this.관리ToolStripMenuItem.Name = "관리ToolStripMenuItem";
            this.관리ToolStripMenuItem.Size = new System.Drawing.Size(108, 21);
            this.관리ToolStripMenuItem.Text = "생산 계획 관리";
            // 
            // 구매자재관리ToolStripMenuItem
            // 
            this.구매자재관리ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.구매요청관리ToolStripMenuItem,
            this.자재발주관리ToolStripMenuItem,
            this.구매입고관리ToolStripMenuItem,
            this.자재재고관리ToolStripMenuItem,
            this.자재수불현황ToolStripMenuItem,
            this.자재불출요청ToolStripMenuItem,
            this.자재불출관리ToolStripMenuItem});
            this.구매자재관리ToolStripMenuItem.Name = "구매자재관리ToolStripMenuItem";
            this.구매자재관리ToolStripMenuItem.Size = new System.Drawing.Size(103, 21);
            this.구매자재관리ToolStripMenuItem.Text = "구매자재 관리";
            // 
            // 구매요청관리ToolStripMenuItem
            // 
            this.구매요청관리ToolStripMenuItem.Name = "구매요청관리ToolStripMenuItem";
            this.구매요청관리ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.구매요청관리ToolStripMenuItem.Text = "구매 요청 관리";
            this.구매요청관리ToolStripMenuItem.Click += new System.EventHandler(this.구매요청관리ToolStripMenuItem_Click);
            // 
            // 자재발주관리ToolStripMenuItem
            // 
            this.자재발주관리ToolStripMenuItem.Name = "자재발주관리ToolStripMenuItem";
            this.자재발주관리ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.자재발주관리ToolStripMenuItem.Text = "자재 발주 관리";
            this.자재발주관리ToolStripMenuItem.Click += new System.EventHandler(this.자재발주관리ToolStripMenuItem_Click);
            // 
            // 구매입고관리ToolStripMenuItem
            // 
            this.구매입고관리ToolStripMenuItem.Name = "구매입고관리ToolStripMenuItem";
            this.구매입고관리ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.구매입고관리ToolStripMenuItem.Text = "자재 입고 관리";
            this.구매입고관리ToolStripMenuItem.Click += new System.EventHandler(this.구매입고관리ToolStripMenuItem_Click);
            // 
            // 자재재고관리ToolStripMenuItem
            // 
            this.자재재고관리ToolStripMenuItem.Name = "자재재고관리ToolStripMenuItem";
            this.자재재고관리ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.자재재고관리ToolStripMenuItem.Text = "자재 재고 관리";
            this.자재재고관리ToolStripMenuItem.Click += new System.EventHandler(this.자재재고관리ToolStripMenuItem_Click);
            // 
            // 자재수불현황ToolStripMenuItem
            // 
            this.자재수불현황ToolStripMenuItem.Name = "자재수불현황ToolStripMenuItem";
            this.자재수불현황ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.자재수불현황ToolStripMenuItem.Text = "자재 수불 현황";
            this.자재수불현황ToolStripMenuItem.Click += new System.EventHandler(this.자재수불현황ToolStripMenuItem_Click);
            // 
            // 자재불출요청ToolStripMenuItem
            // 
            this.자재불출요청ToolStripMenuItem.Name = "자재불출요청ToolStripMenuItem";
            this.자재불출요청ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.자재불출요청ToolStripMenuItem.Text = "자재 불출 요청";
            this.자재불출요청ToolStripMenuItem.Click += new System.EventHandler(this.자재불출요청ToolStripMenuItem_Click);
            // 
            // 자재불출관리ToolStripMenuItem
            // 
            this.자재불출관리ToolStripMenuItem.Name = "자재불출관리ToolStripMenuItem";
            this.자재불출관리ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.자재불출관리ToolStripMenuItem.Text = "자재 불출 관리";
            this.자재불출관리ToolStripMenuItem.Click += new System.EventHandler(this.자재불출관리ToolStripMenuItem_Click);
            // 
            // 생산관리ToolStripMenuItem
            // 
            this.생산관리ToolStripMenuItem.Name = "생산관리ToolStripMenuItem";
            this.생산관리ToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.생산관리ToolStripMenuItem.Text = "생산관리";
            // 
            // 품질관리ToolStripMenuItem
            // 
            this.품질관리ToolStripMenuItem.Name = "품질관리ToolStripMenuItem";
            this.품질관리ToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.품질관리ToolStripMenuItem.Text = "품질관리";
            // 
            // 제품관리ToolStripMenuItem
            // 
            this.제품관리ToolStripMenuItem.Name = "제품관리ToolStripMenuItem";
            this.제품관리ToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.제품관리ToolStripMenuItem.Text = "제품관리";
            // 
            // 시스템관리ToolStripMenuItem
            // 
            this.시스템관리ToolStripMenuItem.Name = "시스템관리ToolStripMenuItem";
            this.시스템관리ToolStripMenuItem.Size = new System.Drawing.Size(85, 21);
            this.시스템관리ToolStripMenuItem.Text = "시스템관리";
            // 
            // LogOutToolStripMenuItem
            // 
            this.LogOutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.로그아웃ToolStripMenuItem1,
            this.ExitToolStripMenuItem1});
            this.LogOutToolStripMenuItem.Name = "LogOutToolStripMenuItem";
            this.LogOutToolStripMenuItem.Size = new System.Drawing.Size(46, 21);
            this.LogOutToolStripMenuItem.Text = "계정";
            // 
            // 로그아웃ToolStripMenuItem1
            // 
            this.로그아웃ToolStripMenuItem1.Name = "로그아웃ToolStripMenuItem1";
            this.로그아웃ToolStripMenuItem1.Size = new System.Drawing.Size(128, 22);
            this.로그아웃ToolStripMenuItem1.Text = "로그아웃";
            this.로그아웃ToolStripMenuItem1.Click += new System.EventHandler(this.LogOutToolStripMenuItem1_Click);
            // 
            // ExitToolStripMenuItem1
            // 
            this.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1";
            this.ExitToolStripMenuItem1.Size = new System.Drawing.Size(128, 22);
            this.ExitToolStripMenuItem1.Text = "종료";
            this.ExitToolStripMenuItem1.Click += new System.EventHandler(this.ExitToolStripMenuItem1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 514);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1246, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1246, 489);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 5;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage2.BackgroundImage")));
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1238, 462);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "메인 화면";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1246, 536);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainScreen_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 보기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 로그아웃ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 구매자재관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 생산관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 품질관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 제품관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 시스템관리ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem 사용자정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 공통코드관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 품목정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 구매요청관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 회사코드관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 거래처정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 공장코드관리ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem 자재발주관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 자재수불현황ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 자재재고관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 자재불출관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 구매입고관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 자재불출요청ToolStripMenuItem;
    }
}

