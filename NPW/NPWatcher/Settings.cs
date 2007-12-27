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
using System.Text;

using System.IO;
using System.Xml.Serialization;

namespace NPWatcher
{
    [Serializable, XmlRoot("NPWSettings")]
    public class Settings
    {
        internal Settings() { }
        public string username = "";
        public List<string> stubTypes = new List<string>();
        public bool hidePatrolled;
        public bool hideBots;
        public bool hideAdmins;
        public string pagelimit = "20";
        public int refreshinterval;

        public static void SavePrefs(Settings settings, string file)
        {
            try
            {
                using (FileStream fStream = new FileStream(file, FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Settings));
                    xs.Serialize(fStream, settings);
                }
            }
            catch { throw; }
        }

        public static Settings LoadPrefs(string file)
        {
            if (System.IO.File.Exists(file))
            {
                try
                {
                    using (FileStream fStream = new FileStream(file, FileMode.Open))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(Settings));
                        return (Settings)xs.Deserialize(fStream);
                    }
                }
                catch { throw; }
            }
            else
                return new Settings();
        }
    }
}
