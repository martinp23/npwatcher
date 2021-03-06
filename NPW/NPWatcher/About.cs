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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            lblNPWVersion.Text = "NPW Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            lblOSVersion.Text = "Windows version: " + Environment.OSVersion.Version.Major + "." + Environment.OSVersion.Version.Minor;
            lblNETVersion.Text = ".NET Version: " + Environment.Version;
        }

        private void linkMartinp23_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkMartinp23.LinkVisited = true;
            WikiFunctions.LoadLink("http://en.wikipedia.org/wiki/User:Martinp23");
        }

        private void linkReedy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkReedy.LinkVisited = true;
            WikiFunctions.LoadLink("http://en.wikipedia.org/wiki/User:Reedy");
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
            WikiFunctions.LoadLink("http://en.wikipedia.org/wiki/User:Martinp23/NPWatcher");
        }

        private void linkBugsFeatures_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkBugsFeatures.LinkVisited = true;
            WikiFunctions.LoadLink("https://launchpad.net/npwatcher/");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkManual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkManual.LinkVisited = true;
            WikiFunctions.LoadLink("User:Martinp23/NPWatcher/Manual");
        }
    }
}