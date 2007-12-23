using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPWatcher
{
    public partial class ListSource : Form
    {
        internal string category;
        internal bool hidebot;
        internal bool hidepatrolled;

        public ListSource()
        {
            InitializeComponent();
            CatTxt.Click += new EventHandler(CatTxt_Click);
            
        }

        void CatTxt_Click(object sender, EventArgs e)
        {
            CustomRad.Checked = true;
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            if (NPRad.Checked)
                category = "NPRad";
            else if (CSDRad.Checked)
                category = "CSDRad";
            else if (CustomRad.Checked)
                category = CatTxt.Text;

            hidebot = chkHideBot.Checked;
            hidepatrolled = chkHidePatrolled.Checked;
            Close();
        }

        private void ListSource_Load(object sender, EventArgs e)
        {
            chkHideBot.Checked = Main.settings.hideBots;
            chkHidePatrolled.Checked = Main.settings.hidePatrolled;
        }
    }
}