using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml.Serialization;

namespace NPWatcher
{
    [Serializable, XmlRoot("NPWSettings")]
    class Settings
    {
        internal Settings() { }

        public static void SavePrefs(Settings settings)
        {
            try
            {
                using (FileStream fStream = new FileStream("settings.xml", FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Settings));
                    xs.Serialize(fStream, settings);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static Settings LoadPrefs()
        {
            if (System.IO.File.Exists("settings.xml"))
            {
                try
                {
                    using (FileStream fStream = new FileStream("settings.xml", FileMode.Open))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(Settings));
                        return (Settings)xs.Deserialize(fStream);
                    }
                }
                catch (Exception ex) { throw ex; }
            }
            else
                return new Settings();
        }
    }
}
