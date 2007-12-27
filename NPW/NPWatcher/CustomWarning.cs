<!--
From: http://svn.martinp23.com/npw/

Copyright 2007 Martin Peeks
Copyright 2007 Reedy_boy
Copyright 2007 Martijn Hoekstra  

This file is part of NPWatcher.

NPWatcher is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

NPWatcher is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with NPWatcher.  If not, see <http://www.gnu.org/licenses/>.

-->
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPWatcher
{
    public partial class CustomWarning : Form
    {
        public CustomWarning()
        {
            InitializeComponent();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (ReasonTxt.Text != null)
            {
                if (!ReasonTxt.Text.Contains("{"))
                {
                    if (!ReasonTxt.Text.Contains("|"))
                    {
                        Main.cwr = ReasonTxt.Text;
                        this.DialogResult = DialogResult.OK;
                        
                        this.Hide();
                    }
                }
                else
                    MessageBox.Show("The program will automatically add the {{ and }} around the template before putting it on the page, so it's safe to leave them out here");
            }
            else
                MessageBox.Show("Please enter a warning template name, or click cancel");
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }
    }
}