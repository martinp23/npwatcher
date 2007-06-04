using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPWatcher
{
    public partial class Prod : Form
    {
        public Prod()
        {
            InitializeComponent();
        }

        private void acceptBtn_Click(object sender, EventArgs e)
        {
            Main.doprod = true;
            Main.prodreasonstr = prodTxt.Text;

            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Main.doprod = false;
            Close();
        }

        internal void prod()
        {
            prodTxt.Text = "Article or userpage [[WP:PROD|proposed for deletion]], undisputed for at least five days";
        }
    }
}