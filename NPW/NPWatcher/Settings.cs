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
