using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPWatcher
{
    public partial class LogOn : Form
    {
        public LogOn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main.username = loginTxt.Text;
            Main.password = pwdTxt.Text;
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Main.dialogcancel = true;
            this.Close();
        }

      
    }
}
