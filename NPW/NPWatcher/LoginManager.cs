/*
From: http://svn.martinp23.com/npw/

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
using System.Text;
using NPWikiFunctions;
using System.Net;
using System.Windows.Forms;


namespace NPWatcher
{
    public class LoginManager
    {
        private LogOn credentialsDialog;
        private Boolean loggedIn;
        private Boolean cancelled;
        private CookieContainer cookies;

        internal LoginManager()
        {
            credentialsDialog = new LogOn(this);
            cancelled = false;
            loggedIn = false;
            cookies = new CookieContainer();
        }

        public CookieContainer Cookies
        {
            get
            {
                return cookies;
            }
        }

        public void startLogin()
        {
            credentialsDialog.ShowDialog();
            while (!loggedIn && !cancelled)
            {
                MessageBox.Show("Not Logged in", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                credentialsDialog.ShowDialog();
            }

            if (loggedIn)
            {
                MessageBox.Show("Logged in", "Logged in", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public Boolean Cancelled
        {
            get
            {
                return cancelled;
            }
            set
            {
                cancelled = value;
            }
        }

        public Boolean LoggedIn
        {
            get
            {
                return loggedIn;
            }
            set
            {
                loggedIn = value;
            }
        }

        public Boolean login(string username, string password)
        {
            return NPWikiFunctions.Util.login(username, password, cookies);
        }
    }
}

