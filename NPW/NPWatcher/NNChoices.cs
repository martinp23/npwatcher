using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPWatcher
{
    public partial class NNChoices : Form
    {
        public NNChoices()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (genRB.Checked)
                Main.nntag = "db-bio";
            else if (bandRB.Checked)
                Main.nntag = "db-band";
            else if (clubRB.Checked)
                Main.nntag = "db-club";
            else if (groupRB.Checked)
                Main.nntag = "db-group";
            else if (webRB.Checked)
                Main.nntag = "db-web";
            leave(true);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            leave(false);
        }

        private void leave(bool choice)
        {
            Main.nnchoicessuc = choice;
            Close();
        }
    }
}