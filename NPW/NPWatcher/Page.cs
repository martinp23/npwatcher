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
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Net;
using System.Threading;
using System.Data;
using System.IO;
using System.Xml;
using NPWikiFunctions;

namespace NPWatcher
{
    class Page
    {
        private string title;
        private string wikitext;
        private string newtext;
        private string urlTitle;
        private WikiFunctions functions;
        private string creator;
        private string rcid;

        Page(string title)
        {
            this.title = title;
            this.urlTitle = System.Web.HttpUtility.UrlEncode(title);
            functions = new WikiFunctions();
            wikitext = newtext = functions.getWikiText(urlTitle);
        }

        #region accessor methods

        public string Title
        {
            get
            {
                return title;
            }
        }

        public string Creator
        {
            get
            {
                if (creator == null)
                {
                    creator = functions.GetCreator(urlTitle);
                }
                return creator;
            }
        }

        public string Wikitext
        {
            get
            {
                return wikitext;
            }
        }

        public string Rcid
        {
            get
            {
                if (rcid == null)
                {
                    rcid = getrcid();
                }
                return rcid;
            }
        }

        #endregion

        #region public methods

        public void append(string text)
        {
            newtext = newtext + text;
        }

        public void prepend(string text)
        {
            newtext = text + newtext;
        }

        public Boolean save(string summary)
        {
           return save(summary, false);
        }

        public Boolean save(string summary, Boolean watch)
        {
            if (wikitext != newtext)
            {
                functions.Save(urlTitle, newtext, summary);
                return (wikitext == functions.getWikiText(urlTitle));
            }
            else return true;
        }

        public Boolean delete(string reason)
        {
            functions.Deletepg(urlTitle, reason);
            return true;
        }

        #endregion

        #region private methods
        //mainly copypaste from Wikifunctions.cs

        private string getrcid()
        {
            string timestamp = getCreationTime();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Util.apiurl + "?action=query&list=recentchanges&rctype=new&rcprop=title|ids&rclimit=5&rcstart=" + timestamp + "&format=xml");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream srcstrm = response.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            string src = work.ReadToEnd();
            StringReader sr = new StringReader(src);
            XmlDocument xml = new XmlDocument();
            xml.Load(sr);
            string rcid = "not found";
            foreach (XmlNode n in xml.GetElementsByTagName("rc"))
            {
                if (n.Attributes.GetNamedItem("title").InnerText == title)
                { rcid = n.Attributes.GetNamedItem("rcid").InnerText.ToString(); }
            }

            return rcid;

        }

        private string getCreationTime()
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(Util.apiurl + "?action=query&prop=revisions&titles=" + urlTitle + "&rvdir=newer&rvlimit=1&rvprop=timestamp&format=xml");
            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();
            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            string src = work.ReadToEnd();
            StringReader sr = new StringReader(src);
            XmlDocument xml = new XmlDocument();
            xml.Load(sr);
            string time = "";
            foreach (XmlNode n in xml.GetElementsByTagName("rev"))
            {
                time = n.Attributes.GetNamedItem("timestamp").InnerText.ToString();
            }
            return time;
        }

        #endregion private methods

    }
}
