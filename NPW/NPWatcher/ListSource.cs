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
        public ListSource()
        {
            InitializeComponent();
        }


        private void loadBtn_Click(object sender, EventArgs e)
        {
            
            if (NPRad.Checked)
            {
                category = "NPRad";
            }
            else if (CSDRad.Checked)
            {
                category = "CSDRad";
            }
            else if (CustomRad.Checked)
            {
                category = CatTxt.Text;
            }

            Close();
        }

    }
}