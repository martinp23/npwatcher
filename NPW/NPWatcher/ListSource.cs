/*
From: http://svn.martinp23.com/npw/

Copyright 2008 Martin Peeks
Copyright 2008 Reedy

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
using System.Windows.Forms;

namespace NPWatcher
{
    public partial class ListSource : Form
    {
        internal string category;
        internal bool hidebot;
        internal bool hidepatrolled;
        internal bool hideadmin;

        public ListSource()
        {
            InitializeComponent();
            CatTxt.Click += CatTxt_Click;
        }

        private void CatTxt_Click(object sender, EventArgs e)
        {
            CustomRad.Checked = true;
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            if (NPRad.Checked)
                category = "NPRad";
            else if (NPRadOld.Checked)
                category = "NPRadOld";
            else if (CSDRad.Checked)
                category = "CSDRad";
            else
                category = CatTxt.Text;

            hidebot = chkHideBot.Checked;
            hidepatrolled = chkHidePatrolled.Checked;
            hideadmin = chkHideAdmins.Checked;
            Close();
        }

        private void ListSource_Load(object sender, EventArgs e)
        {
            CSDRad.Enabled = CustomRad.Enabled = CatTxt.Enabled = Main.asAdmin;
            loadBtn.Focus();
        }
    }
}