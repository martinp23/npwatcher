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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            lblNPWVersion.Text = "NPW Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            lblOSVersion.Text = "Windows version: " + Environment.OSVersion.Version.Major.ToString() + "." + Environment.OSVersion.Version.Minor.ToString();
            lblNETVersion.Text = ".NET Version: " + Environment.Version.ToString();
        }

        private void linkMartinp23_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkMartinp23.LinkVisited = true;
            WikiFunctions.LoadLink("http://en.wikipedia.org/wiki/User:Martinp23");
        }

        private void linkReedy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkReedy.LinkVisited = true;
            WikiFunctions.LoadLink("http://en.wikipedia.org/wiki/User:Reedy Boy");
        }

        private void linkMartijn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkMartijn.LinkVisited = true;
            WikiFunctions.LoadLink("http://en.wikipedia.org/wiki/User:Martijn Hoekstra");
        }

        private void linkSnowolf_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkSnowolf.LinkVisited = true;
            WikiFunctions.LoadLink("http://en.wikipedia.org/wiki/User:Snowolf");
        }

        private void linkNPW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkNPW.LinkVisited = true;
            WikiFunctions.LoadLink("http://en.wikipedia.org/wiki/User:User:Martinp23/NPWatcher");
        }

        private void linkBugsFeatures_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkBugsFeatures.LinkVisited = true;
            WikiFunctions.LoadLink("https://launchpad.net/npwatcher/");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}