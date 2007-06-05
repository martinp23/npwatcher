using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPWatcher
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main.username = loginTxt.Text;
            Main.password = pwdTxt.Text;
            if(asAdmin.Checked)
            {
               // WikiFunctions.asAdmin = true;
                Main.asAdmin = true;
            }
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Main.dialogcancel = true;
            Application.Exit();
        }
    }
}