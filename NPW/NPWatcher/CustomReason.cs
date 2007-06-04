using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPWatcher
{
    public partial class CustomReason : Form
    {
        public CustomReason()
        {
            InitializeComponent();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (ReasonTxt.Text != null)
            { Main.dbReason = ReasonTxt.Text; }
            Main.crsuc = true;
            this.Close();
        }

        private void Cancelbtn_Click(object sender, EventArgs e)
        {
            Main.crsuc = false;
            this.Close();
        }

        private void CustomReason_Load(object sender, EventArgs e)
        {

        }
    }
}