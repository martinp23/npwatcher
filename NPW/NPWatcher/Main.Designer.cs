namespace NPWatcher
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.pageList = new System.Windows.Forms.ListBox();
            this.mnuPages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.limitCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.limittip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.customWarnCB = new System.Windows.Forms.CheckBox();
            this.db_userreq = new System.Windows.Forms.Button();
            this.AfDBtn = new System.Windows.Forms.Button();
            this.rmvBtn = new System.Windows.Forms.Button();
            this.prodBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cvLinkTxt = new System.Windows.Forms.TextBox();
            this.dvCvBtn = new System.Windows.Forms.Button();
            this.notabilitywarn = new System.Windows.Forms.CheckBox();
            this.dbR1Btn = new System.Windows.Forms.Button();
            this.dbR2Btn = new System.Windows.Forms.Button();
            this.dbR3Btn = new System.Windows.Forms.Button();
            this.dbforeignBtn = new System.Windows.Forms.Button();
            this.dbemptyBtn = new System.Windows.Forms.Button();
            this.dbattackBtn = new System.Windows.Forms.Button();
            this.dbtalkBtn = new System.Windows.Forms.Button();
            this.dbBlankBtn = new System.Windows.Forms.Button();
            this.dbTest = new System.Windows.Forms.Button();
            this.dbvandBtn = new System.Windows.Forms.Button();
            this.dbNonsBtn = new System.Windows.Forms.Button();
            this.dbRepostBtn = new System.Windows.Forms.Button();
            this.dbspamBtn = new System.Windows.Forms.Button();
            this.dbbioBtn = new System.Windows.Forms.Button();
            this.firstarticle = new System.Windows.Forms.CheckBox();
            this.dbBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.uncatBtn = new System.Windows.Forms.Button();
            this.notEngBtn = new System.Windows.Forms.Button();
            this.contextBtn = new System.Windows.Forms.Button();
            this.introBtn = new System.Windows.Forms.Button();
            this.sectionsBtn = new System.Windows.Forms.Button();
            this.howtoBtn = new System.Windows.Forms.Button();
            this.ceBtn = new System.Windows.Forms.Button();
            this.copyBtn = new System.Windows.Forms.Button();
            this.npovBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.stubTxt = new System.Windows.Forms.TextBox();
            this.wikifyBtn = new System.Windows.Forms.Button();
            this.nnBtn = new System.Windows.Forms.Button();
            this.advertBtn = new System.Windows.Forms.Button();
            this.stubBtn = new System.Windows.Forms.Button();
            this.sourcesBtn = new System.Windows.Forms.Button();
            this.toneBtn = new System.Windows.Forms.Button();
            this.cleanupBtn = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.AfDCustom = new System.Windows.Forms.Button();
            this.RmvProd = new System.Windows.Forms.Button();
            this.DelCustom = new System.Windows.Forms.Button();
            this.delGivReasonBtn = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.orphanCB = new System.Windows.Forms.CheckBox();
            this.IotherBtn = new System.Windows.Forms.Button();
            this.I8Btn = new System.Windows.Forms.Button();
            this.I7Btn = new System.Windows.Forms.Button();
            this.I6Btn = new System.Windows.Forms.Button();
            this.I5Btn = new System.Windows.Forms.Button();
            this.I4Btn = new System.Windows.Forms.Button();
            this.I3Btn = new System.Windows.Forms.Button();
            this.I2Btn = new System.Windows.Forms.Button();
            this.I1Btn = new System.Windows.Forms.Button();
            this.getlistBtn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.mnuPages.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageList
            // 
            this.pageList.ContextMenuStrip = this.mnuPages;
            this.pageList.FormattingEnabled = true;
            this.pageList.Location = new System.Drawing.Point(12, 39);
            this.pageList.Name = "pageList";
            this.pageList.Size = new System.Drawing.Size(139, 147);
            this.pageList.TabIndex = 1;
            this.pageList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pageList_MouseClick);
            // 
            // mnuPages
            // 
            this.mnuPages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeSelectedToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearAllToolStripMenuItem});
            this.mnuPages.Name = "mnuPages";
            this.mnuPages.Size = new System.Drawing.Size(190, 54);
            // 
            // removeSelectedToolStripMenuItem
            // 
            this.removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
            this.removeSelectedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeSelectedToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.removeSelectedToolStripMenuItem.Text = "Remove selected";
            this.removeSelectedToolStripMenuItem.Click += new System.EventHandler(this.removeSelectedToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(12, 247);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(984, 473);
            this.webBrowser1.TabIndex = 5;
            this.webBrowser1.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.webBrowser1_ProgressChanged);
            // 
            // limitCB
            // 
            this.limitCB.FormattingEnabled = true;
            this.limitCB.Items.AddRange(new object[] {
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40"});
            this.limitCB.Location = new System.Drawing.Point(49, 12);
            this.limitCB.Name = "limitCB";
            this.limitCB.Size = new System.Drawing.Size(48, 21);
            this.limitCB.TabIndex = 0;
            this.limittip.SetToolTip(this.limitCB, "Enter the number of new pages you \r\nwould like the program to retrieve.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Limit:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(157, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(839, 229);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.customWarnCB);
            this.tabPage1.Controls.Add(this.db_userreq);
            this.tabPage1.Controls.Add(this.AfDBtn);
            this.tabPage1.Controls.Add(this.rmvBtn);
            this.tabPage1.Controls.Add(this.prodBtn);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cvLinkTxt);
            this.tabPage1.Controls.Add(this.dvCvBtn);
            this.tabPage1.Controls.Add(this.notabilitywarn);
            this.tabPage1.Controls.Add(this.dbR1Btn);
            this.tabPage1.Controls.Add(this.dbR2Btn);
            this.tabPage1.Controls.Add(this.dbR3Btn);
            this.tabPage1.Controls.Add(this.dbforeignBtn);
            this.tabPage1.Controls.Add(this.dbemptyBtn);
            this.tabPage1.Controls.Add(this.dbattackBtn);
            this.tabPage1.Controls.Add(this.dbtalkBtn);
            this.tabPage1.Controls.Add(this.dbBlankBtn);
            this.tabPage1.Controls.Add(this.dbTest);
            this.tabPage1.Controls.Add(this.dbvandBtn);
            this.tabPage1.Controls.Add(this.dbNonsBtn);
            this.tabPage1.Controls.Add(this.dbRepostBtn);
            this.tabPage1.Controls.Add(this.dbspamBtn);
            this.tabPage1.Controls.Add(this.dbbioBtn);
            this.tabPage1.Controls.Add(this.firstarticle);
            this.tabPage1.Controls.Add(this.dbBtn);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(831, 203);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Deletion";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // customWarnCB
            // 
            this.customWarnCB.AutoSize = true;
            this.customWarnCB.Location = new System.Drawing.Point(426, 135);
            this.customWarnCB.Name = "customWarnCB";
            this.customWarnCB.Size = new System.Drawing.Size(133, 17);
            this.customWarnCB.TabIndex = 50;
            this.customWarnCB.Text = "Leave custom warning";
            this.customWarnCB.UseVisualStyleBackColor = true;
            // 
            // db_userreq
            // 
            this.db_userreq.Location = new System.Drawing.Point(680, 11);
            this.db_userreq.Name = "db_userreq";
            this.db_userreq.Size = new System.Drawing.Size(80, 49);
            this.db_userreq.TabIndex = 49;
            this.db_userreq.Text = "Delete (user request)";
            this.db_userreq.UseVisualStyleBackColor = true;
            this.db_userreq.Click += new System.EventHandler(this.db_userreq_Click);
            // 
            // AfDBtn
            // 
            this.AfDBtn.Location = new System.Drawing.Point(176, 133);
            this.AfDBtn.Name = "AfDBtn";
            this.AfDBtn.Size = new System.Drawing.Size(80, 48);
            this.AfDBtn.TabIndex = 48;
            this.AfDBtn.Text = "AfD (and notify)";
            this.AfDBtn.UseVisualStyleBackColor = true;
            this.AfDBtn.Click += new System.EventHandler(this.AfDBtn_Click);
            // 
            // rmvBtn
            // 
            this.rmvBtn.Enabled = false;
            this.rmvBtn.Location = new System.Drawing.Point(6, 133);
            this.rmvBtn.Name = "rmvBtn";
            this.rmvBtn.Size = new System.Drawing.Size(80, 49);
            this.rmvBtn.TabIndex = 47;
            this.rmvBtn.Text = "Remove existing tags";
            this.rmvBtn.UseVisualStyleBackColor = true;
            this.rmvBtn.Click += new System.EventHandler(this.rmvBtn_Click);
            // 
            // prodBtn
            // 
            this.prodBtn.Location = new System.Drawing.Point(90, 133);
            this.prodBtn.Name = "prodBtn";
            this.prodBtn.Size = new System.Drawing.Size(80, 48);
            this.prodBtn.TabIndex = 46;
            this.prodBtn.Text = "Prod (and notify)";
            this.prodBtn.UseVisualStyleBackColor = true;
            this.prodBtn.Click += new System.EventHandler(this.prodBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(413, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "URL of copyvio";
            // 
            // cvLinkTxt
            // 
            this.cvLinkTxt.Location = new System.Drawing.Point(500, 160);
            this.cvLinkTxt.Name = "cvLinkTxt";
            this.cvLinkTxt.Size = new System.Drawing.Size(180, 20);
            this.cvLinkTxt.TabIndex = 44;
            // 
            // dvCvBtn
            // 
            this.dvCvBtn.Location = new System.Drawing.Point(594, 69);
            this.dvCvBtn.Name = "dvCvBtn";
            this.dvCvBtn.Size = new System.Drawing.Size(80, 49);
            this.dvCvBtn.TabIndex = 43;
            this.dvCvBtn.Text = "Delete (copyvio)";
            this.dvCvBtn.UseVisualStyleBackColor = true;
            this.dvCvBtn.Click += new System.EventHandler(this.dbCvBtn_Click);
            // 
            // notabilitywarn
            // 
            this.notabilitywarn.AutoSize = true;
            this.notabilitywarn.Location = new System.Drawing.Point(264, 162);
            this.notabilitywarn.Name = "notabilitywarn";
            this.notabilitywarn.Size = new System.Drawing.Size(140, 17);
            this.notabilitywarn.TabIndex = 42;
            this.notabilitywarn.Text = "Leave notability warning";
            this.notabilitywarn.UseVisualStyleBackColor = true;
            // 
            // dbR1Btn
            // 
            this.dbR1Btn.Location = new System.Drawing.Point(342, 69);
            this.dbR1Btn.Name = "dbR1Btn";
            this.dbR1Btn.Size = new System.Drawing.Size(80, 49);
            this.dbR1Btn.TabIndex = 41;
            this.dbR1Btn.Text = "Delete (R1 - no target)";
            this.dbR1Btn.UseVisualStyleBackColor = true;
            this.dbR1Btn.Click += new System.EventHandler(this.dbR1Btn_Click);
            // 
            // dbR2Btn
            // 
            this.dbR2Btn.Location = new System.Drawing.Point(426, 69);
            this.dbR2Btn.Name = "dbR2Btn";
            this.dbR2Btn.Size = new System.Drawing.Size(80, 49);
            this.dbR2Btn.TabIndex = 40;
            this.dbR2Btn.Text = "Delete (R2 - user page)";
            this.dbR2Btn.UseVisualStyleBackColor = true;
            this.dbR2Btn.Click += new System.EventHandler(this.dbR2Btn_Click);
            // 
            // dbR3Btn
            // 
            this.dbR3Btn.Location = new System.Drawing.Point(510, 69);
            this.dbR3Btn.Name = "dbR3Btn";
            this.dbR3Btn.Size = new System.Drawing.Size(80, 49);
            this.dbR3Btn.TabIndex = 39;
            this.dbR3Btn.Text = "Delete (R3 - implausible)";
            this.dbR3Btn.UseVisualStyleBackColor = true;
            this.dbR3Btn.Click += new System.EventHandler(this.dbR3Btn_Click);
            // 
            // dbforeignBtn
            // 
            this.dbforeignBtn.Location = new System.Drawing.Point(258, 11);
            this.dbforeignBtn.Name = "dbforeignBtn";
            this.dbforeignBtn.Size = new System.Drawing.Size(80, 49);
            this.dbforeignBtn.TabIndex = 38;
            this.dbforeignBtn.Text = "Delete (foreign)";
            this.dbforeignBtn.UseVisualStyleBackColor = true;
            this.dbforeignBtn.Click += new System.EventHandler(this.dbforeignBtn_Click);
            // 
            // dbemptyBtn
            // 
            this.dbemptyBtn.Location = new System.Drawing.Point(258, 69);
            this.dbemptyBtn.Name = "dbemptyBtn";
            this.dbemptyBtn.Size = new System.Drawing.Size(80, 49);
            this.dbemptyBtn.TabIndex = 37;
            this.dbemptyBtn.Text = "Delete (empty)";
            this.dbemptyBtn.UseVisualStyleBackColor = true;
            this.dbemptyBtn.Click += new System.EventHandler(this.dbemptyBtn_Click);
            // 
            // dbattackBtn
            // 
            this.dbattackBtn.Location = new System.Drawing.Point(174, 69);
            this.dbattackBtn.Name = "dbattackBtn";
            this.dbattackBtn.Size = new System.Drawing.Size(80, 49);
            this.dbattackBtn.TabIndex = 36;
            this.dbattackBtn.Text = "Delete (attack) and warn";
            this.dbattackBtn.UseVisualStyleBackColor = true;
            this.dbattackBtn.Click += new System.EventHandler(this.dbattackBtn_Click);
            // 
            // dbtalkBtn
            // 
            this.dbtalkBtn.Location = new System.Drawing.Point(90, 69);
            this.dbtalkBtn.Name = "dbtalkBtn";
            this.dbtalkBtn.Size = new System.Drawing.Size(80, 49);
            this.dbtalkBtn.TabIndex = 35;
            this.dbtalkBtn.Text = "Delete (G8 - talk)";
            this.dbtalkBtn.UseVisualStyleBackColor = true;
            this.dbtalkBtn.Click += new System.EventHandler(this.dbtalkBtn_Click);
            // 
            // dbBlankBtn
            // 
            this.dbBlankBtn.Location = new System.Drawing.Point(6, 69);
            this.dbBlankBtn.Name = "dbBlankBtn";
            this.dbBlankBtn.Size = new System.Drawing.Size(80, 49);
            this.dbBlankBtn.TabIndex = 34;
            this.dbBlankBtn.Text = "Delete (blanked)";
            this.dbBlankBtn.UseVisualStyleBackColor = true;
            this.dbBlankBtn.Click += new System.EventHandler(this.dbBlankBtn_Click);
            // 
            // dbTest
            // 
            this.dbTest.Location = new System.Drawing.Point(510, 11);
            this.dbTest.Name = "dbTest";
            this.dbTest.Size = new System.Drawing.Size(80, 49);
            this.dbTest.TabIndex = 33;
            this.dbTest.Text = "Delete (test) and warn";
            this.dbTest.UseVisualStyleBackColor = true;
            this.dbTest.Click += new System.EventHandler(this.dbTest_Click);
            // 
            // dbvandBtn
            // 
            this.dbvandBtn.Location = new System.Drawing.Point(594, 11);
            this.dbvandBtn.Name = "dbvandBtn";
            this.dbvandBtn.Size = new System.Drawing.Size(80, 49);
            this.dbvandBtn.TabIndex = 32;
            this.dbvandBtn.Text = "Delete (vandalism) and warn";
            this.dbvandBtn.UseVisualStyleBackColor = true;
            this.dbvandBtn.Click += new System.EventHandler(this.dbvandBtn_Click);
            // 
            // dbNonsBtn
            // 
            this.dbNonsBtn.Location = new System.Drawing.Point(426, 11);
            this.dbNonsBtn.Name = "dbNonsBtn";
            this.dbNonsBtn.Size = new System.Drawing.Size(80, 49);
            this.dbNonsBtn.TabIndex = 31;
            this.dbNonsBtn.Text = "Delete (nonsense) and warn";
            this.dbNonsBtn.UseVisualStyleBackColor = true;
            this.dbNonsBtn.Click += new System.EventHandler(this.dbNonsBtn_Click);
            // 
            // dbRepostBtn
            // 
            this.dbRepostBtn.Location = new System.Drawing.Point(342, 11);
            this.dbRepostBtn.Name = "dbRepostBtn";
            this.dbRepostBtn.Size = new System.Drawing.Size(80, 49);
            this.dbRepostBtn.TabIndex = 30;
            this.dbRepostBtn.Text = "Delete (repost)";
            this.dbRepostBtn.UseVisualStyleBackColor = true;
            this.dbRepostBtn.Click += new System.EventHandler(this.dbRepostBtn_Click);
            // 
            // dbspamBtn
            // 
            this.dbspamBtn.Location = new System.Drawing.Point(174, 11);
            this.dbspamBtn.Name = "dbspamBtn";
            this.dbspamBtn.Size = new System.Drawing.Size(80, 49);
            this.dbspamBtn.TabIndex = 29;
            this.dbspamBtn.Text = "Delete (spam)";
            this.dbspamBtn.UseVisualStyleBackColor = true;
            this.dbspamBtn.Click += new System.EventHandler(this.dbspamBtn_Click);
            // 
            // dbbioBtn
            // 
            this.dbbioBtn.Location = new System.Drawing.Point(90, 11);
            this.dbbioBtn.Name = "dbbioBtn";
            this.dbbioBtn.Size = new System.Drawing.Size(80, 49);
            this.dbbioBtn.TabIndex = 28;
            this.dbbioBtn.Text = "Delete (non-notable)";
            this.dbbioBtn.UseVisualStyleBackColor = true;
            this.dbbioBtn.Click += new System.EventHandler(this.dbbioBtn_Click);
            // 
            // firstarticle
            // 
            this.firstarticle.AutoSize = true;
            this.firstarticle.Location = new System.Drawing.Point(264, 135);
            this.firstarticle.Name = "firstarticle";
            this.firstarticle.Size = new System.Drawing.Size(150, 17);
            this.firstarticle.TabIndex = 27;
            this.firstarticle.Text = "Leave {{firstarticle}} ~~~~";
            this.firstarticle.UseVisualStyleBackColor = true;
            // 
            // dbBtn
            // 
            this.dbBtn.Location = new System.Drawing.Point(6, 11);
            this.dbBtn.Name = "dbBtn";
            this.dbBtn.Size = new System.Drawing.Size(80, 49);
            this.dbBtn.TabIndex = 26;
            this.dbBtn.Text = "Delete (custom reason)";
            this.dbBtn.UseVisualStyleBackColor = true;
            this.dbBtn.Click += new System.EventHandler(this.dbBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.uncatBtn);
            this.tabPage2.Controls.Add(this.notEngBtn);
            this.tabPage2.Controls.Add(this.contextBtn);
            this.tabPage2.Controls.Add(this.introBtn);
            this.tabPage2.Controls.Add(this.sectionsBtn);
            this.tabPage2.Controls.Add(this.howtoBtn);
            this.tabPage2.Controls.Add(this.ceBtn);
            this.tabPage2.Controls.Add(this.copyBtn);
            this.tabPage2.Controls.Add(this.npovBtn);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.stubTxt);
            this.tabPage2.Controls.Add(this.wikifyBtn);
            this.tabPage2.Controls.Add(this.nnBtn);
            this.tabPage2.Controls.Add(this.advertBtn);
            this.tabPage2.Controls.Add(this.stubBtn);
            this.tabPage2.Controls.Add(this.sourcesBtn);
            this.tabPage2.Controls.Add(this.toneBtn);
            this.tabPage2.Controls.Add(this.cleanupBtn);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(831, 203);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Maintenance";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // uncatBtn
            // 
            this.uncatBtn.Location = new System.Drawing.Point(622, 61);
            this.uncatBtn.Name = "uncatBtn";
            this.uncatBtn.Size = new System.Drawing.Size(84, 49);
            this.uncatBtn.TabIndex = 20;
            this.uncatBtn.Text = "Mark with {{uncategorized}}";
            this.uncatBtn.UseVisualStyleBackColor = true;
            this.uncatBtn.Click += new System.EventHandler(this.uncatBtn_Click);
            // 
            // notEngBtn
            // 
            this.notEngBtn.Location = new System.Drawing.Point(94, 61);
            this.notEngBtn.Name = "notEngBtn";
            this.notEngBtn.Size = new System.Drawing.Size(84, 49);
            this.notEngBtn.TabIndex = 19;
            this.notEngBtn.Text = "Mark with {{notenglish}}";
            this.notEngBtn.UseVisualStyleBackColor = true;
            this.notEngBtn.Click += new System.EventHandler(this.notEngBtn_Click);
            // 
            // contextBtn
            // 
            this.contextBtn.Location = new System.Drawing.Point(534, 61);
            this.contextBtn.Name = "contextBtn";
            this.contextBtn.Size = new System.Drawing.Size(84, 49);
            this.contextBtn.TabIndex = 18;
            this.contextBtn.Text = "Mark with {{context}}";
            this.contextBtn.UseVisualStyleBackColor = true;
            this.contextBtn.Click += new System.EventHandler(this.contextBtn_Click);
            // 
            // introBtn
            // 
            this.introBtn.Location = new System.Drawing.Point(446, 61);
            this.introBtn.Name = "introBtn";
            this.introBtn.Size = new System.Drawing.Size(84, 49);
            this.introBtn.TabIndex = 17;
            this.introBtn.Text = "Mark with {{introrewrite}}";
            this.introBtn.UseVisualStyleBackColor = true;
            this.introBtn.Click += new System.EventHandler(this.introBtn_Click);
            // 
            // sectionsBtn
            // 
            this.sectionsBtn.Location = new System.Drawing.Point(358, 61);
            this.sectionsBtn.Name = "sectionsBtn";
            this.sectionsBtn.Size = new System.Drawing.Size(84, 49);
            this.sectionsBtn.TabIndex = 16;
            this.sectionsBtn.Text = "Mark with {{sections}}";
            this.sectionsBtn.UseVisualStyleBackColor = true;
            this.sectionsBtn.Click += new System.EventHandler(this.sectionsBtn_Click);
            // 
            // howtoBtn
            // 
            this.howtoBtn.Location = new System.Drawing.Point(270, 61);
            this.howtoBtn.Name = "howtoBtn";
            this.howtoBtn.Size = new System.Drawing.Size(84, 49);
            this.howtoBtn.TabIndex = 15;
            this.howtoBtn.Text = "Mark with {{howto}}";
            this.howtoBtn.UseVisualStyleBackColor = true;
            this.howtoBtn.Click += new System.EventHandler(this.howtoBtn_Click);
            // 
            // ceBtn
            // 
            this.ceBtn.Location = new System.Drawing.Point(182, 61);
            this.ceBtn.Name = "ceBtn";
            this.ceBtn.Size = new System.Drawing.Size(84, 49);
            this.ceBtn.TabIndex = 14;
            this.ceBtn.Text = "Mark with {{copyedit}}";
            this.ceBtn.UseVisualStyleBackColor = true;
            this.ceBtn.Click += new System.EventHandler(this.ceBtn_Click);
            // 
            // copyBtn
            // 
            this.copyBtn.Location = new System.Drawing.Point(6, 61);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(84, 49);
            this.copyBtn.TabIndex = 12;
            this.copyBtn.Text = "Mark with {{copypaste}}";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.copyBtn_Click);
            // 
            // npovBtn
            // 
            this.npovBtn.Location = new System.Drawing.Point(622, 6);
            this.npovBtn.Name = "npovBtn";
            this.npovBtn.Size = new System.Drawing.Size(84, 49);
            this.npovBtn.TabIndex = 11;
            this.npovBtn.Text = "Mark with {{npov}}";
            this.npovBtn.UseVisualStyleBackColor = true;
            this.npovBtn.Click += new System.EventHandler(this.npovBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Stub type (eg \"music\")";
            // 
            // stubTxt
            // 
            this.stubTxt.Location = new System.Drawing.Point(246, 136);
            this.stubTxt.Name = "stubTxt";
            this.stubTxt.Size = new System.Drawing.Size(231, 20);
            this.stubTxt.TabIndex = 9;
            // 
            // wikifyBtn
            // 
            this.wikifyBtn.Location = new System.Drawing.Point(534, 6);
            this.wikifyBtn.Name = "wikifyBtn";
            this.wikifyBtn.Size = new System.Drawing.Size(84, 49);
            this.wikifyBtn.TabIndex = 7;
            this.wikifyBtn.Text = "Mark with {{wikify}}";
            this.wikifyBtn.UseVisualStyleBackColor = true;
            this.wikifyBtn.Click += new System.EventHandler(this.wikifyBtn_Click);
            // 
            // nnBtn
            // 
            this.nnBtn.Location = new System.Drawing.Point(446, 6);
            this.nnBtn.Name = "nnBtn";
            this.nnBtn.Size = new System.Drawing.Size(84, 49);
            this.nnBtn.TabIndex = 6;
            this.nnBtn.Text = "Mark with {{notability}}";
            this.nnBtn.UseVisualStyleBackColor = true;
            this.nnBtn.Click += new System.EventHandler(this.nnBtn_Click);
            // 
            // advertBtn
            // 
            this.advertBtn.Location = new System.Drawing.Point(358, 6);
            this.advertBtn.Name = "advertBtn";
            this.advertBtn.Size = new System.Drawing.Size(84, 49);
            this.advertBtn.TabIndex = 5;
            this.advertBtn.Text = "Mark with {{advert}}";
            this.advertBtn.UseVisualStyleBackColor = true;
            this.advertBtn.Click += new System.EventHandler(this.advertBtn_Click);
            // 
            // stubBtn
            // 
            this.stubBtn.Location = new System.Drawing.Point(270, 6);
            this.stubBtn.Name = "stubBtn";
            this.stubBtn.Size = new System.Drawing.Size(84, 49);
            this.stubBtn.TabIndex = 3;
            this.stubBtn.Text = "Mark with {{stub}}";
            this.stubBtn.UseVisualStyleBackColor = true;
            this.stubBtn.Click += new System.EventHandler(this.stubBtn_Click);
            // 
            // sourcesBtn
            // 
            this.sourcesBtn.Location = new System.Drawing.Point(182, 6);
            this.sourcesBtn.Name = "sourcesBtn";
            this.sourcesBtn.Size = new System.Drawing.Size(84, 49);
            this.sourcesBtn.TabIndex = 2;
            this.sourcesBtn.Text = "Mark with {{unsourced}}";
            this.sourcesBtn.UseVisualStyleBackColor = true;
            this.sourcesBtn.Click += new System.EventHandler(this.sourcesBtn_Click);
            // 
            // toneBtn
            // 
            this.toneBtn.Location = new System.Drawing.Point(94, 6);
            this.toneBtn.Name = "toneBtn";
            this.toneBtn.Size = new System.Drawing.Size(84, 49);
            this.toneBtn.TabIndex = 1;
            this.toneBtn.Text = "Mark with {{tone}}";
            this.toneBtn.UseVisualStyleBackColor = true;
            this.toneBtn.Click += new System.EventHandler(this.toneBtn_Click);
            // 
            // cleanupBtn
            // 
            this.cleanupBtn.Location = new System.Drawing.Point(6, 6);
            this.cleanupBtn.Name = "cleanupBtn";
            this.cleanupBtn.Size = new System.Drawing.Size(84, 49);
            this.cleanupBtn.TabIndex = 0;
            this.cleanupBtn.Text = "Mark with {{cleanup}}";
            this.cleanupBtn.UseVisualStyleBackColor = true;
            this.cleanupBtn.Click += new System.EventHandler(this.cleanupBtn_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.AfDCustom);
            this.tabPage3.Controls.Add(this.RmvProd);
            this.tabPage3.Controls.Add(this.DelCustom);
            this.tabPage3.Controls.Add(this.delGivReasonBtn);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(831, 203);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Prods";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // AfDCustom
            // 
            this.AfDCustom.Location = new System.Drawing.Point(192, 6);
            this.AfDCustom.Name = "AfDCustom";
            this.AfDCustom.Size = new System.Drawing.Size(92, 48);
            this.AfDCustom.TabIndex = 4;
            this.AfDCustom.Text = "AfD (using custom reason)";
            this.AfDCustom.UseVisualStyleBackColor = true;
            this.AfDCustom.Click += new System.EventHandler(this.AfDCustom_Click);
            // 
            // RmvProd
            // 
            this.RmvProd.Location = new System.Drawing.Point(290, 6);
            this.RmvProd.Name = "RmvProd";
            this.RmvProd.Size = new System.Drawing.Size(86, 48);
            this.RmvProd.TabIndex = 3;
            this.RmvProd.Text = "Remove tag";
            this.RmvProd.UseVisualStyleBackColor = true;
            this.RmvProd.Click += new System.EventHandler(this.RmvProd_Click);
            // 
            // DelCustom
            // 
            this.DelCustom.Location = new System.Drawing.Point(94, 6);
            this.DelCustom.Name = "DelCustom";
            this.DelCustom.Size = new System.Drawing.Size(92, 48);
            this.DelCustom.TabIndex = 1;
            this.DelCustom.Text = "Delete (using custom reason)";
            this.DelCustom.UseVisualStyleBackColor = true;
            this.DelCustom.Click += new System.EventHandler(this.DelCustom_Click);
            // 
            // delGivReasonBtn
            // 
            this.delGivReasonBtn.Location = new System.Drawing.Point(6, 6);
            this.delGivReasonBtn.Name = "delGivReasonBtn";
            this.delGivReasonBtn.Size = new System.Drawing.Size(82, 48);
            this.delGivReasonBtn.TabIndex = 0;
            this.delGivReasonBtn.Text = "Delete (using given reason)";
            this.delGivReasonBtn.UseVisualStyleBackColor = true;
            this.delGivReasonBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.orphanCB);
            this.tabPage4.Controls.Add(this.IotherBtn);
            this.tabPage4.Controls.Add(this.I8Btn);
            this.tabPage4.Controls.Add(this.I7Btn);
            this.tabPage4.Controls.Add(this.I6Btn);
            this.tabPage4.Controls.Add(this.I5Btn);
            this.tabPage4.Controls.Add(this.I4Btn);
            this.tabPage4.Controls.Add(this.I3Btn);
            this.tabPage4.Controls.Add(this.I2Btn);
            this.tabPage4.Controls.Add(this.I1Btn);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(831, 203);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Images";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // orphanCB
            // 
            this.orphanCB.AutoSize = true;
            this.orphanCB.Checked = true;
            this.orphanCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.orphanCB.Location = new System.Drawing.Point(85, 94);
            this.orphanCB.Name = "orphanCB";
            this.orphanCB.Size = new System.Drawing.Size(98, 17);
            this.orphanCB.TabIndex = 9;
            this.orphanCB.Text = "Orphan image?";
            this.orphanCB.UseVisualStyleBackColor = true;
            // 
            // IotherBtn
            // 
            this.IotherBtn.Location = new System.Drawing.Point(285, 78);
            this.IotherBtn.Name = "IotherBtn";
            this.IotherBtn.Size = new System.Drawing.Size(73, 47);
            this.IotherBtn.TabIndex = 8;
            this.IotherBtn.Text = "Other";
            this.IotherBtn.UseVisualStyleBackColor = true;
            this.IotherBtn.Click += new System.EventHandler(this.IotherBtn_Click);
            // 
            // I8Btn
            // 
            this.I8Btn.Location = new System.Drawing.Point(559, 6);
            this.I8Btn.Name = "I8Btn";
            this.I8Btn.Size = new System.Drawing.Size(73, 47);
            this.I8Btn.TabIndex = 7;
            this.I8Btn.Text = "I8 - Images on commons";
            this.I8Btn.UseVisualStyleBackColor = true;
            this.I8Btn.Click += new System.EventHandler(this.I8Btn_Click);
            // 
            // I7Btn
            // 
            this.I7Btn.Location = new System.Drawing.Point(480, 6);
            this.I7Btn.Name = "I7Btn";
            this.I7Btn.Size = new System.Drawing.Size(73, 47);
            this.I7Btn.TabIndex = 6;
            this.I7Btn.Text = "I7 - Bad FU claim";
            this.I7Btn.UseVisualStyleBackColor = true;
            this.I7Btn.Click += new System.EventHandler(this.I7Btn_Click);
            // 
            // I6Btn
            // 
            this.I6Btn.Location = new System.Drawing.Point(401, 6);
            this.I6Btn.Name = "I6Btn";
            this.I6Btn.Size = new System.Drawing.Size(73, 47);
            this.I6Btn.TabIndex = 5;
            this.I6Btn.Text = "I6 - No FU-rationale";
            this.I6Btn.UseVisualStyleBackColor = true;
            this.I6Btn.Click += new System.EventHandler(this.I6Btn_Click);
            // 
            // I5Btn
            // 
            this.I5Btn.Location = new System.Drawing.Point(322, 6);
            this.I5Btn.Name = "I5Btn";
            this.I5Btn.Size = new System.Drawing.Size(73, 47);
            this.I5Btn.TabIndex = 4;
            this.I5Btn.Text = "I5 - Unused, unfree";
            this.I5Btn.UseVisualStyleBackColor = true;
            this.I5Btn.Click += new System.EventHandler(this.I5Btn_Click);
            // 
            // I4Btn
            // 
            this.I4Btn.Location = new System.Drawing.Point(243, 6);
            this.I4Btn.Name = "I4Btn";
            this.I4Btn.Size = new System.Drawing.Size(73, 47);
            this.I4Btn.TabIndex = 3;
            this.I4Btn.Text = "I4 - No license";
            this.I4Btn.UseVisualStyleBackColor = true;
            this.I4Btn.Click += new System.EventHandler(this.I4Btn_Click);
            // 
            // I3Btn
            // 
            this.I3Btn.Location = new System.Drawing.Point(164, 6);
            this.I3Btn.Name = "I3Btn";
            this.I3Btn.Size = new System.Drawing.Size(73, 47);
            this.I3Btn.TabIndex = 2;
            this.I3Btn.Text = "I3 - Bad license (and warn)";
            this.I3Btn.UseVisualStyleBackColor = true;
            this.I3Btn.Click += new System.EventHandler(this.I3Btn_Click);
            // 
            // I2Btn
            // 
            this.I2Btn.Location = new System.Drawing.Point(85, 6);
            this.I2Btn.Name = "I2Btn";
            this.I2Btn.Size = new System.Drawing.Size(73, 47);
            this.I2Btn.TabIndex = 1;
            this.I2Btn.Text = "I2 - Corrupt/empty";
            this.I2Btn.UseVisualStyleBackColor = true;
            this.I2Btn.Click += new System.EventHandler(this.I2Btn_Click);
            // 
            // I1Btn
            // 
            this.I1Btn.Location = new System.Drawing.Point(6, 6);
            this.I1Btn.Name = "I1Btn";
            this.I1Btn.Size = new System.Drawing.Size(73, 47);
            this.I1Btn.TabIndex = 0;
            this.I1Btn.Text = "I1 - Redundant";
            this.I1Btn.UseVisualStyleBackColor = true;
            this.I1Btn.Click += new System.EventHandler(this.I1Btn_Click);
            // 
            // getlistBtn
            // 
            this.getlistBtn.Location = new System.Drawing.Point(12, 191);
            this.getlistBtn.Name = "getlistBtn";
            this.getlistBtn.Size = new System.Drawing.Size(139, 46);
            this.getlistBtn.TabIndex = 2;
            this.getlistBtn.Text = "Populate list";
            this.getlistBtn.UseVisualStyleBackColor = true;
            this.getlistBtn.Click += new System.EventHandler(this.getlistBtn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 710);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 732);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.getlistBtn);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.limitCB);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.pageList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "NPWatcher";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mnuPages.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox pageList;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ComboBox limitCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip limittip;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cvLinkTxt;
        private System.Windows.Forms.Button dvCvBtn;
        private System.Windows.Forms.CheckBox notabilitywarn;
        private System.Windows.Forms.Button dbR1Btn;
        private System.Windows.Forms.Button dbR2Btn;
        private System.Windows.Forms.Button dbR3Btn;
        private System.Windows.Forms.Button dbforeignBtn;
        private System.Windows.Forms.Button dbemptyBtn;
        private System.Windows.Forms.Button dbattackBtn;
        private System.Windows.Forms.Button dbtalkBtn;
        private System.Windows.Forms.Button dbBlankBtn;
        private System.Windows.Forms.Button dbTest;
        private System.Windows.Forms.Button dbvandBtn;
        private System.Windows.Forms.Button dbNonsBtn;
        private System.Windows.Forms.Button dbRepostBtn;
        private System.Windows.Forms.Button dbspamBtn;
        private System.Windows.Forms.Button dbbioBtn;
        private System.Windows.Forms.CheckBox firstarticle;
        private System.Windows.Forms.Button dbBtn;
        private System.Windows.Forms.Button getlistBtn;
        private System.Windows.Forms.Button prodBtn;
        private System.Windows.Forms.Button stubBtn;
        private System.Windows.Forms.Button sourcesBtn;
        private System.Windows.Forms.Button toneBtn;
        private System.Windows.Forms.Button cleanupBtn;
        private System.Windows.Forms.Button wikifyBtn;
        private System.Windows.Forms.Button nnBtn;
        private System.Windows.Forms.Button advertBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox stubTxt;
        private System.Windows.Forms.Button npovBtn;
        private System.Windows.Forms.Button rmvBtn;
        private System.Windows.Forms.Button AfDBtn;
        private System.Windows.Forms.Button copyBtn;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button delGivReasonBtn;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button AfDCustom;
        private System.Windows.Forms.Button RmvProd;
        private System.Windows.Forms.Button DelCustom;
        private System.Windows.Forms.Button IotherBtn;
        private System.Windows.Forms.Button I8Btn;
        private System.Windows.Forms.Button I7Btn;
        private System.Windows.Forms.Button I6Btn;
        private System.Windows.Forms.Button I5Btn;
        private System.Windows.Forms.Button I4Btn;
        private System.Windows.Forms.Button I3Btn;
        private System.Windows.Forms.Button I2Btn;
        private System.Windows.Forms.Button I1Btn;
        private System.Windows.Forms.CheckBox orphanCB;
        private System.Windows.Forms.Button db_userreq;
        private System.Windows.Forms.Button contextBtn;
        private System.Windows.Forms.Button introBtn;
        private System.Windows.Forms.Button sectionsBtn;
        private System.Windows.Forms.Button howtoBtn;
        private System.Windows.Forms.Button ceBtn;
        private System.Windows.Forms.CheckBox customWarnCB;
        private System.Windows.Forms.Button notEngBtn;
        private System.Windows.Forms.Button uncatBtn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ContextMenuStrip mnuPages;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
    }
}

