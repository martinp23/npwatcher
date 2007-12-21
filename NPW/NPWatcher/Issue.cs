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
