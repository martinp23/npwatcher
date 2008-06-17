/*
From: http://svn.martinp23.com/npw/

Copyright 2008 Martin Peeks
Copyright 2008 Reedy
Copyright 2008 Martijn Hoekstra

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
using NPWikiFunctions;

namespace NPWatcher
{
    public partial class LogOn : Form
    {
        private LoginManager caller;

        public LogOn(LoginManager caller)
        {
            this.caller = caller;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Util.approved(loginTxt.Text))
            {
                caller.LoggedIn = caller.login(loginTxt.Text, pwdTxt.Text);
            }
            else
            {
                caller.Cancelled = true;
            }
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            caller.Cancelled = true;
            this.Close();
        }

        private void LogOn_Load(object sender, EventArgs e)
        {
            loginTxt.Text = Main.settings.username;
        }

      
    }
}
