/*
From: http://svn.martinp23.com/npw/

Copyright 2007 Martin Peeks
Copyright 2007 Reedy_Boy

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

*/

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
            else if (companyRB.Checked)
                Main.nntag = "db-corp";
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