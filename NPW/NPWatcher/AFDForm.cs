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
    public partial class AFDForm : Form
    {
        public AFDForm()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Main.afdsuc = false;
            Close();
        }

        private void nomBtn_Click(object sender, EventArgs e)
        {
            Main.afdReason = reasonTxt.Text;

            try
            {
                string cat = catTxt.SelectedItem.ToString();
                string catcode = "U";

                if (cat == "Media and music")
                { catcode = "M"; }
                else if (cat == "Organisation, corporation, or product")
                { catcode = "O"; }
                else if (cat == "Biographical")
                { catcode = "B"; }
                else if (cat == "Society topics")
                { catcode = "S"; }
                else if (cat == "Web or internet")
                { catcode = "W"; }
                else if (cat == "Games or sports")
                { catcode = "G"; }
                else if (cat == "Science and technology")
                { catcode = "T"; }
                else if (cat == "Fiction and the arts")
                { catcode = "F"; }
                else if (cat == "Places and transportation")
                { catcode = "P"; }
                else if (cat == "Indiscernable or unclassifiable topic")
                { catcode = "I"; }
                else if (cat == "Unknown")
                { catcode = "?"; }

                Main.afdCat = catcode;
            }
            catch (NullReferenceException)
            {
                DialogResult dr = MessageBox.Show("You didn't enter an AfD category.  Would you like to use the default?", "Category",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                    Main.afdCat = "U";
                else
                    MessageBox.Show("Please enter an AfD category");
            }
            Main.afdsuc = true;
            Close();
        }
    }
}