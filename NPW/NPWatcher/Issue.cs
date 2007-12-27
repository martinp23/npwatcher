/*
From: http://svn.martinp23.com/npw/

Copyright 2007 Martin Peeks
Copyright 2007 Reedy_Boy
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

*/
using System;
using System.Collections;
using System.Globalization;

namespace NPWatcher
{
    /// <summary>
    /// Article issue
    /// </summary>
    public class Issue
    {
        private string name;

        public Issue(string name)
        {
            this.name = name;
        }

        public string getTemplate()
        {
            return "{{" + name + "|" + templatedate() + "}}";
        }

        public string getIssueLine()
        {
            return "|" + name + " = " + issuedate() + "\r\n";
        }

        public string getName()
        {
            return name;
        }

        private string issuedate()
        {
            return DateTime.Now.ToString("Y", CultureInfo.CreateSpecificCulture("en-ZA"));
        }

        private string templatedate()
        {
            return "date = " + issuedate();
        }
    }
}
