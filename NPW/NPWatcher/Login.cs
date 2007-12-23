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
            Main.settings.username = loginTxt.Text;
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Main.dialogcancel = true;
            this.Close();
        }

        private void LogOn_Load(object sender, EventArgs e)
        {
            loginTxt.Text = Main.settings.username;
        }

      
    }
}
