using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPWatcher
{
    public partial class frmLangChoose : Form
    {
        public frmLangChoose()
        {
            InitializeComponent();
        }

        public string lang
        {
            get { return langTxt.Text; }
            set { langTxt.Text = value; }
        }
    }
}