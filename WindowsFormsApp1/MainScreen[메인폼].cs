using System;
using Draw = System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class MainScreen : Form
    {
        static public string LoginID;

        private Point _imageLocation = new Point(20,3);
        private Point _imgHitArea = new Point(20,4);
        Image closeR;

        public MainScreen()
        {
            InitializeComponent();

            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += tabControl1_DrawItem;
            closeR = Properties.Resources.close1;
            tabControl1.Padding = new Point(10, 5);
        }
        private void MainScreen_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = $"{LoginID}님 환영합니다.";
            Common.DICT_REMOVE_INDEX.Add("시작화면", 0);
        }

        // tab닫기
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                Brush titleBrush = new SolidBrush(Color.Black);
                Image img = new Bitmap(closeR);
                Font f = Font;
                
                Rectangle r = e.Bounds;
                r = tabControl1.GetTabRect(e.Index);
                r.Offset(2, 2);

                string title = tabControl1.TabPages[e.Index].Text;

                // TabPage Text
                e.Graphics.DrawString(title + " ", f, titleBrush, new PointF(r.X, r.Y));

                // TabPage 의 닫기 버튼
                if (tabControl1.SelectedIndex >= 1)
                    e.Graphics.DrawImage(img, new Point(r.X + (tabControl1.GetTabRect(e.Index).Width - _imageLocation.X), _imageLocation.Y));

                // 각 Tab별로 close button 에 대한 image값 
                if (tabControl1.SelectedTab == tabControl1.TabPages[e.Index])
                    img = Properties.Resources.close1;
                else
                    img = Properties.Resources.close1;

                img.Dispose();
                img = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeleteTabpage(string temp)
        {
            int aint = 0;

            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                if (tabControl1.TabPages[i].Text == temp)
                {
                    aint = i;
                }
            }
            tabControl1.TabPages.RemoveAt(aint);

            int index = Common.DICT_REMOVE_INDEX[temp];
            Common.DICT_REMOVE_INDEX.Remove(temp);

            //탭페이지를 앞으로 한칸씩땡긴다.
            if (Common.DICT_REMOVE_INDEX.Count > 2)
            {
                for (int i = index; i < Common.DICT_REMOVE_INDEX.Count; i++)
                {
                    string tempstring = Common.DICT_REMOVE_INDEX.FirstOrDefault(x => x.Value == i + 1).Key;
                    int tempint = Common.DICT_REMOVE_INDEX[tempstring];
                    Common.DICT_REMOVE_INDEX.Remove(tempstring);
                    Common.DICT_REMOVE_INDEX.Add(tempstring, tempint - 1);
                }
            }
            tabControl1.SelectedIndex = aint - 1;
        }

        static public int tabindex = 0;

        //탭컨트롤에서 탭에 그린 닫기버튼내부의 영역을 클릭하면 폼 닫기
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            tabindex = tc.SelectedIndex;

            Point p = e.Location;

            int _tabWidth = 0;
            _tabWidth = tabControl1.GetTabRect(tc.SelectedIndex).Width - (_imgHitArea.X);

            Rectangle r = tabControl1.GetTabRect(tc.SelectedIndex);
            r.Offset(_tabWidth, _imgHitArea.Y);
            r.Width = 16;
            r.Height = 16;
            if (r.Contains(p))
            {
                TabPage TabP = tc.TabPages[tc.SelectedIndex];
                tc.TabPages.Remove(TabP);

                int index = Common.DICT_REMOVE_INDEX[TabP.Text];
                Common.DICT_REMOVE_INDEX.Remove(TabP.Text);

                //탭페이지를 앞으로 한칸씩땡긴다.
                for (int i = index; i < Common.DICT_REMOVE_INDEX.Count; i++)
                {
                    string tempstring = Common.DICT_REMOVE_INDEX.FirstOrDefault(x => x.Value == i + 1).Key;
                    int tempint = Common.DICT_REMOVE_INDEX[tempstring];

                    Common.DICT_REMOVE_INDEX.Remove(tempstring);
                    Common.DICT_REMOVE_INDEX.Add(tempstring, tempint - 1);
                }
            }
        }
        // 닫기
        
        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("종료하시겠습니까?", "종료",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (exit == DialogResult.Yes)
            {
                Visible = false;
                MainScreen aMainScreen = new MainScreen();
                aMainScreen.Close();
            }
        }

        private void LogOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult logout = MessageBox.Show($"{LoginID}님 로그아웃 하시겠습니까?", "로그아웃",
                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (logout == DialogResult.Yes)
            {
                Visible = false;
                LoginScreen aLoginScreen = new LoginScreen();
                aLoginScreen.ShowDialog();
                Close();
            }
        }

        #region<메뉴 탭 컨트롤>
        private void 거래처정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                BM_CustomerMaster_MM custmaster = new BM_CustomerMaster_MM();
                custmaster.FormCloseEvent += DeleteTabpage;
                custmaster.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(custmaster);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(custmaster);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                custmaster.Dock = DockStyle.Fill;
                custmaster.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }
        
        private void 사용자정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                User_info user_info = new User_info();
                user_info.FormCloseEvent += DeleteTabpage;
                user_info.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(user_info);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(user_info);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                user_info.Dock = DockStyle.Fill;
                user_info.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
            
        }

        private void 공통코드관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                BM_CommCode_MM comm_Code_M = new BM_CommCode_MM();
                comm_Code_M.FormCloseEvent += DeleteTabpage;
                comm_Code_M.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(comm_Code_M);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(comm_Code_M);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                comm_Code_M.Dock = DockStyle.Fill;
                comm_Code_M.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }
        private void 품목정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                BM_ItemMaster_MM item = new BM_ItemMaster_MM();
                item.FormCloseEvent += DeleteTabpage;
                item.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(item);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(item);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                item.Dock = DockStyle.Fill;
                item.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }

        private void 구매요청관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                MTRL_Request MTRL_Req = new MTRL_Request();
                MTRL_Req.FormCloseEvent += DeleteTabpage;
                MTRL_Req.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(MTRL_Req);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(MTRL_Req);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                MTRL_Req.Dock = DockStyle.Fill;
                MTRL_Req.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }
        

        private void 회사코드관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                BM_CompanyMaster_MM compcode = new BM_CompanyMaster_MM();
                compcode.FormCloseEvent += DeleteTabpage;
                compcode.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(compcode);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(compcode);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                compcode.Dock = DockStyle.Fill;
                compcode.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }

        private void 공장코드관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                BM_PlantMaster_MM plantMaster_MM = new BM_PlantMaster_MM();
                plantMaster_MM.FormCloseEvent += DeleteTabpage;
                plantMaster_MM.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(plantMaster_MM);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(plantMaster_MM);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                plantMaster_MM.Dock = DockStyle.Fill;
                plantMaster_MM.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }

        private void 자재발주관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                MM_MTRL_Order_MM mm_MTRL_Order_MM = new MM_MTRL_Order_MM();
                mm_MTRL_Order_MM.FormCloseEvent += DeleteTabpage;
                mm_MTRL_Order_MM.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(mm_MTRL_Order_MM);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(mm_MTRL_Order_MM);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                mm_MTRL_Order_MM.Dock = DockStyle.Fill;
                mm_MTRL_Order_MM.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }
        

        private void 자재수불현황ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                MM_StockMM_REC mM_StockMM_REC = new MM_StockMM_REC();
                mM_StockMM_REC.FormCloseEvent += DeleteTabpage;
                mM_StockMM_REC.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(mM_StockMM_REC);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(mM_StockMM_REC);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                mM_StockMM_REC.Dock = DockStyle.Fill;
                mM_StockMM_REC.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }
        private void 자재재고관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                MM_Stock_MM mM_Stock_MM = new MM_Stock_MM();
                mM_Stock_MM.FormCloseEvent += DeleteTabpage;
                mM_Stock_MM.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(mM_Stock_MM);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(mM_Stock_MM);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                mM_Stock_MM.Dock = DockStyle.Fill;
                mM_Stock_MM.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }
        private void 자재불출관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                MM_STOCKOUT_MM mM_Stock_OUT = new MM_STOCKOUT_MM();
                mM_Stock_OUT.FormCloseEvent += DeleteTabpage;
                mM_Stock_OUT.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(mM_Stock_OUT);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(mM_Stock_OUT);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                mM_Stock_OUT.Dock = DockStyle.Fill;
                mM_Stock_OUT.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }
        #endregion

        private void 구매입고관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                PW_Purchase_Warehousing pW_Purchase_Warehousing = new PW_Purchase_Warehousing();
                pW_Purchase_Warehousing.FormCloseEvent += DeleteTabpage;
                pW_Purchase_Warehousing.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(pW_Purchase_Warehousing);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(pW_Purchase_Warehousing);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                pW_Purchase_Warehousing.Dock = DockStyle.Fill;
                pW_Purchase_Warehousing.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }

        private void 자재불출요청ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = (sender.ToString());
            if (!Common.DICT_REMOVE_INDEX.ContainsKey(str))
            {
                STOCKOUT_REQ sTOCKOUT_REQ = new STOCKOUT_REQ();
                sTOCKOUT_REQ.FormCloseEvent += DeleteTabpage;
                sTOCKOUT_REQ.TopLevel = false;
                tabControl1.TabPages.Add(str);
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(sTOCKOUT_REQ);
                tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(sTOCKOUT_REQ);

                Common.DICT_REMOVE_INDEX.Add(str, tabControl1.SelectedIndex); //Dictionary로 화면텍스트와 탭번호 저장
                sTOCKOUT_REQ.Dock = DockStyle.Fill;
                sTOCKOUT_REQ.Show();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages[Common.DICT_REMOVE_INDEX[str]];
            }
        }
    }

}