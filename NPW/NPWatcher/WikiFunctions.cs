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
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Xml;

namespace NPWatcher
{
    class WikiFunctions
    {
        private HttpWebRequest webReq;
        private CookieCollection cookies;
        internal StringCollection adminslist = new StringCollection();
        private CookieContainer cc = new CookieContainer();
        public static string watch = "0";
        //internal static bool asAdmin;
        private DateTime dt = new DateTime();
        private static string wikiurl = "http://en.wikipedia.org/w/index.php?title=";
        private static string apiurl = "http://en.wikipedia.org/w/api.php";
        private static string queryurl = "http://en.wikipedia.org/w/query.php";

        public string Url
        {
            get { return wikiurl; }
        }

        public bool login(string Username, string Userpass)
        {
            //get list of admins first!
            getusergroup("sysop");

            webReq = (HttpWebRequest)WebRequest.Create(wikiurl + "Special:Userlogin&action=submitlogin&type=login");
            string postData = String.Format("wpName=+{0}&wpPassword={1}&wpRemember=1&wpLoginattempt=Log+in",
                new string[] { Username, Userpass });
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.Proxy = WebRequest.GetSystemWebProxy();
            webReq.CookieContainer = new CookieContainer();
            webReq.AllowAutoRedirect = false;
            byte[] postBytes = Encoding.UTF8.GetBytes(postData);
            webReq.ContentLength = postBytes.Length;
            Stream reqStrm = webReq.GetRequestStream();
            reqStrm.Write(postBytes, 0, postBytes.Length);
            reqStrm.Close();

            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
            cookies = webResp.Cookies;
            webResp.Close();

            webRequest(apiurl + "?action=query&list=watchlist&wllimit=3&format=xml");

            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();
            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);

            return (!work.ReadToEnd().Contains("wlnotloggedin"));
        }

        //public bool CheckIfAdmin()
        //{
        //    string userGroups;
        //    List<string> Groups = new List<string>();

        //    userGroups = GetScriptingVar("wgUserGroups");

        //    Regex r = new Regex("\"([a-z]*)\"[,\\]]");

        //    foreach (Match m1 in r.Matches(userGroups))
        //    {
        //        Groups.Add(m1.Groups[1].Value);
        //    }

        //    return (Groups.Contains("sysop") || Groups.Contains("staff"));
        //}

        private string GetScriptingVar(string name)
        {
            webRequest("http://en.wikipedia.org/");

            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();

            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            string src = work.ReadToEnd();

            try
            {
                Regex r = new Regex("var " + name + " = (.*?);\n");
                src = StringBetween(src, "<head>", "</head>");
                Match m = r.Match(src);

                if (!m.Groups[1].Success)
                    return "";

                string s = m.Groups[1].Value.Trim('"');
                s = s.Replace("\\\"", "\"").Replace("\\'", "'");

                return s;
            }
            catch { return ""; }
        }

        private string StringBetween(string source, string start, string end)
        {
            try { return source.Substring(source.IndexOf(start), source.IndexOf(end) - source.IndexOf(start)); }
            catch { return ""; }
        }

        ///Get newpages from 
        ///http://en.wikipedia.org/w/index.php?title=Special:Newpages&namespace=0&limit=20&offset=0&feed=atom
        public StringCollection getCat(string limit, string category)
        {
            Main.settings.pagelimit = limit;
            webRequest(queryurl + "?what=category&cptitle=" + category + "&cplimit=" + limit + "&format=xml");

            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();

            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            string src = work.ReadToEnd();
            src = HttpUtility.HtmlDecode(src);

            StringCollection a = new StringCollection();
            Regex ptitle = new Regex("<title>([^<]*?)</title>");
            MatchCollection mcpt;
            mcpt = ptitle.Matches(src);
            foreach (Match m in mcpt)
            {
                string ms = m.Value;
                ms = ms.Substring(7, ms.Length - 7);
                ms = ms.Substring(0, ms.Length - 8);
                a.Add(ms);

            }

            return a;
        }

        public StringCollection getImgLinks(string image)
        {
            Regex nextPortionRE = new Regex("&amp;from=(.*?)\" title=\"");
            StringCollection a = new StringCollection();
            MatchCollection mcpt;
            string src;

            do
            {
                webRequest(queryurl + "?what=imagelinks&titles=" + image + "&format=xml");

                HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();

                Stream srcstrm = webResp1.GetResponseStream();
                StreamReader work = new StreamReader(srcstrm);
                src = work.ReadToEnd();

                Regex ptitle = new Regex("<il n?s?=?\"?[0|1|2|3|4|5|6|7|8|9]*?\"? ?id=\"[0|1|2|3|4|5|6|7|8|9]*\">([^<]*?)</il>");


                mcpt = ptitle.Matches(src);
                foreach (Match m in mcpt)
                {
                    string ms = m.Value;
                    ms = Regex.Replace(ms, "</il>", "");
                    ms = Regex.Replace(ms, "<il n?s?=?\"?[0|1|2|3|4|5|6|7|8|9]*?\"? ?id=\"[0|1|2|3|4|5|6|7|8|9]*\">", "");

                    a.Add(ms);

                }
            }
            while (nextPortionRE.IsMatch(src));

            a.Remove(image);

            return a;
        }

        public StringCollection getNPs(string limit, bool oldest)
        {
            Main.settings.pagelimit = limit;
            dt = DateTime.Now.ToUniversalTime();
            StringCollection strCol = new StringCollection();
            string tehurl = wikiurl + "Special:Newpages&namespace=0&limit=" + limit + "&hidepatrolled=" + Main.settings.hidePatrolled.ToString() + "&hidebots=" + Main.settings.hideBots + "&feed=atom";

            if (oldest)
                tehurl += "&dir=prev";

            webRequest(tehurl);
            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();
            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            string src = work.ReadToEnd();
            StringReader sr = new StringReader(src);
            XmlDocument xml = new XmlDocument();
            xml.Load(sr);

            foreach (XmlNode n in xml.GetElementsByTagName("title"))
            {
                if (n.InnerXml != "Wikipedia - New pages [en]")
                {
                    if (Main.settings.hideAdmins && !adminslist.Contains(n.NextSibling.NextSibling.NextSibling.NextSibling.FirstChild.InnerText))
                        strCol.Add(n.FirstChild.InnerText);
                    else if (!Main.settings.hideAdmins)
                        strCol.Add(n.FirstChild.InnerText);

                }
            }

            return strCol;
        }

        public string getrcid(string page)
        {
            string timestamp = getCreationTime(page);
            //string t = dt.ToString("yyyyMMddhhmm");
            webRequest(apiurl + "?action=query&list=recentchanges&rctype=new&rcprop=title|ids&rclimit=5&rcstart=" + timestamp + "&format=xml");
            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();
            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            //src = HttpUtility.HtmlDecode(work.ReadToEnd());
            //src = src.Substring(src.IndexOf("<!-- start content -->") + 22);
            //src = src.Substring(0, src.IndexOf("<!-- end content -->"));
            //src = "<div>" + src + "</div>";
            StringReader sr = new StringReader(work.ReadToEnd());
            XmlDocument xml = new XmlDocument();
            xml.Load(sr);
            string rcid = "not found";
            foreach (XmlNode n in xml.GetElementsByTagName("rc"))
            {
                if (n.Attributes.GetNamedItem("title").InnerText == HttpUtility.UrlDecode(page))
                { rcid = n.Attributes.GetNamedItem("rcid").InnerText; }
            }

            return rcid;
        }

        private string getCreationTime(string page)
        {
            webRequest(apiurl + "?action=query&prop=revisions&titles=" + page + "&rvdir=newer&rvlimit=1&rvprop=timestamp&format=xml");
            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();
            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            StringReader sr = new StringReader(work.ReadToEnd());
            XmlDocument xml = new XmlDocument();
            xml.Load(sr);
            string time = "";
            foreach (XmlNode n in xml.GetElementsByTagName("rev"))
            {
                time = n.Attributes.GetNamedItem("timestamp").InnerText;
            }
            return time;
        }

        public void getusergroup(string group)
        {
            //TODO:API
            string src = "";
            webRequest(wikiurl + "Special:Listusers&group=" + group + "&limit=5000");
            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();
            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            src = work.ReadToEnd();
            // src = HttpUtility.HtmlDecode(src);
            src = src.Substring(src.IndexOf("<!-- start content -->") + 22);
            src = src.Substring(0, src.IndexOf("<!-- end content -->"));
            src = "<div>" + src + "</div>";
            StringReader sr = new StringReader(src.Replace("&nbsp;", ""));
            XmlDocument xml = new XmlDocument();
            xml.Load(sr);

            foreach (XmlNode n in xml.GetElementsByTagName("li"))
            {
                adminslist.Add(n.FirstChild.InnerText);
            }
        }

        public string getWikiText(string page)
        {
            webRequest(wikiurl + page + "&action=raw&ctype=text/plain&dontcountme=s");
            try
            {
                HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();

                Stream srcstrm = webResp1.GetResponseStream();
                StreamReader work = new StreamReader(srcstrm);
                return work.ReadToEnd();
            }
            catch
            {
                return "";
            }
        }

        public string GetCreator(string page)
        {
            webRequest(apiurl + "?action=query&prop=revisions&titles=" + page + "&rvlimit=5&rvprop=user&rvlimit=1&rvdir=newer&format=xml");

            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();

            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            string src = HttpUtility.HtmlDecode(work.ReadToEnd());

            Regex getuser = new Regex("user=\"(.+?)\"");
            Match m2 = getuser.Match(src);
            string name = m2.Value;
            if (name.Length >= 8)
            {
                //[anon=\"\"] ?[minor=\"\"] 

                name = name.Substring(6);
                name = name.Replace("\"", "");
                //  name = name.Substring(0, name.Length - 1);
            }
            else
            { name = ""; }
            return name;
        }

        public void Save(string page, string newtxt, string editsummary)
        {
            try
            {
                webRequest(wikiurl + page + "&action=edit");

                HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();

                Stream srcstrm = webResp1.GetResponseStream();
                StreamReader work = new StreamReader(srcstrm);
                string src = work.ReadToEnd();

                Regex editSessionTokenRE1 = new Regex("value=\"([^\"]*?)\" name=\"wpEditToken\"");

                Match m = editSessionTokenRE1.Match(src);
                string editToken = m.Value;
                editToken = editToken.Substring(7);
                editToken = editToken.Substring(0, editToken.Length - 20);
                Regex editSessionTimeRE = new Regex("value=\"([^\"]*?)\" name=\"wpEdittime\"");
                Match m1 = editSessionTimeRE.Match(src);
                string editTime = m1.Value;
                editTime = editTime.Substring(7);
                editTime = editTime.Substring(0, editTime.Length - 19);

                //Regex editAutoSummaryRE = new Regex("name=\"wpAutoSummary\" type=\"hidden\" value=\"([^\"]*?)\"");
                //Match m2 = editAutoSummaryRE.Match(src);
                //string autosummary = m2.Value;
                //autosummary = autosummary.Substring(42);
                //autosummary = autosummary.Substring(0, autosummary.Length - 1);

                webReq = (HttpWebRequest)WebRequest.Create(wikiurl + page + "&action=submit");
                webReq.UserAgent = "NPWatcher/1.0";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.Method = "POST";
                //CookieContainer cc = new CookieContainer();

                //cc.Add(cookies);
                webReq.CookieContainer = cc;
                webReq.Credentials = CredentialCache.DefaultCredentials;
                webReq.Proxy = WebRequest.GetSystemWebProxy();
                watch = "";

                string postData = string.Format("wpSection=&wpStarttime={0}&wpEdittime={1}&wpScrolltop=0" +
                    "&wpTextbox1={2}&wpWatchThis={5}&wpSummary={3}&wpSave=Save%20Page&wpEditToken={4}",
                    new string[] { DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss"), HttpUtility.UrlEncode(editTime),
                HttpUtility.UrlEncode(newtxt), HttpUtility.UrlEncode(editsummary), HttpUtility.UrlEncode(editToken), watch });

                byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                webReq.ContentLength = postBytes.Length;
                Stream reqStrm = webReq.GetRequestStream();
                reqStrm.Write(postBytes, 0, postBytes.Length);
                reqStrm.Close();
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                StreamReader strmReader = new StreamReader(webResp.GetResponseStream());
                strmReader.ReadToEnd();
                strmReader.Close();
                webResp.Close();

            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Something sinister has happened.  The page has been deleted/marked for deletion, but the program was unable to leave a user warning.  If possible, please could you report the page you just deleted/tagged to Martinp23, so he can look into it.  Sorry!");
            }
        }

        public void Save(string page, string newtxt, string editsummary, bool watchthis)
        {
            try
            {
                webRequest(wikiurl + page + "&action=edit");

                HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();

                Stream srcstrm = webResp1.GetResponseStream();
                StreamReader work = new StreamReader(srcstrm);
                string src = work.ReadToEnd();

                Regex editSessionTokenRE1 = new Regex("value=\"([^\"]*?)\" name=\"wpEditToken\"");

                Match m = editSessionTokenRE1.Match(src);
                string editToken = m.Value;
                editToken = editToken.Substring(7);
                editToken = editToken.Substring(0, editToken.Length - 20);
                Regex editSessionTimeRE = new Regex("value=\"([^\"]*?)\" name=\"wpEdittime\"");
                Match m1 = editSessionTimeRE.Match(src);
                string editTime = m1.Value;
                editTime = editTime.Substring(7);
                editTime = editTime.Substring(0, editTime.Length - 19);

                webReq = (HttpWebRequest)WebRequest.Create(wikiurl + page + "&action=submit");
                webReq.UserAgent = "NPWatcher/1.0";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.Method = "POST";
                //CookieContainer cc = new CookieContainer();

                //cc.Add(cookies);
                webReq.CookieContainer = cc;
                webReq.Credentials = CredentialCache.DefaultCredentials;
                webReq.Proxy = WebRequest.GetSystemWebProxy();
                watch = watchthis ? "checked" : "off";

                string postData = string.Format("wpSection=&wpStarttime={0}&wpEdittime={1}&wpScrolltop=" +
                    "&wpTextbox1={2}&wpWatchThis={5}&wpSummary={3}&wpSave=Save%20Page&wpEditToken={4}",
                    new string[] { DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss"), HttpUtility.UrlEncode(editTime),
                HttpUtility.UrlEncode(newtxt), HttpUtility.UrlEncode(editsummary), HttpUtility.UrlEncode(editToken), watch });

                byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                webReq.ContentLength = postBytes.Length;
                Stream reqStrm = webReq.GetRequestStream();
                reqStrm.Write(postBytes, 0, postBytes.Length);
                reqStrm.Close();
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                StreamReader strmReader = new StreamReader(webResp.GetResponseStream());
                strmReader.ReadToEnd();
                strmReader.Close();
                webResp.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Something sinister has happened.  The user has not been warned - please contact Martinp23 with code E1, and mention the page (" + page + ").  Sorry!");
            }
        }

        public void Deletepg(string page, string editsummary)
        {
            webRequest(wikiurl + page + "&action=edit");

            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();

            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            string src = HttpUtility.HtmlDecode(work.ReadToEnd());

            Regex editSessionTokenRE1 = new Regex("value=\"([^\"]*?)\" name=\"wpEditToken\"");

            Match m = editSessionTokenRE1.Match(src);
            string editToken = m.Value;
            editToken = editToken.Substring(7);
            editToken = editToken.Substring(0, editToken.Length - 20);
            Regex editSessionTimeRE = new Regex("value=\"([^\"]*?)\" name=\"wpEdittime\"");
            Match m1 = editSessionTimeRE.Match(src);
            string editTime = m1.Value;
            editTime = editTime.Substring(7);
            editTime = editTime.Substring(0, editTime.Length - 19);

            webReq = (HttpWebRequest)WebRequest.Create(wikiurl + "" + page + "&action=delete");
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.Method = "POST";
            //CookieContainer cc = new CookieContainer();

            //cc.Add(cookies);
            webReq.CookieContainer = cc;
            webReq.Credentials = CredentialCache.DefaultCredentials;
            webReq.Proxy = WebRequest.GetSystemWebProxy();
            watch = "true";

            string postData = string.Format("wpSection=&wpStarttime={0}&wpEdittime={1}&wpScrolltop=" +
                "&wpReason={2}&wpConfirmB=Delete%20Page&wpEditToken={3}",
                new string[] { DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss"), HttpUtility.UrlEncode(editTime),
                 HttpUtility.UrlEncode(editsummary), HttpUtility.UrlEncode(editToken) });

            byte[] postBytes = Encoding.UTF8.GetBytes(postData);
            webReq.ContentLength = postBytes.Length;
            Stream reqStrm = webReq.GetRequestStream();
            reqStrm.Write(postBytes, 0, postBytes.Length);
            reqStrm.Close();
            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
            StreamReader strmReader = new StreamReader(webResp.GetResponseStream());
            strmReader.ReadToEnd();
            strmReader.Close();
            webResp.Close();
        }

        public bool getLogInStatus()
        {
            Regex LoginRegex = new Regex("var wgUserName = (.*?);", RegexOptions.Compiled);

            string src;
            webRequest(wikiurl + "Wikipedia:Sandbox&action=edit");
            try
            {
                HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();

                Stream srcstrm = webResp1.GetResponseStream();
                StreamReader work = new StreamReader(srcstrm);
                src = HttpUtility.HtmlDecode(work.ReadToEnd());
            }
            catch
            {
                src = "";
            }

            Match m = LoginRegex.Match(src);

            return !(!m.Success || m.Groups[1].Value == "null");
        }

        private void webRequest(string URL)
        {
            webReq = (HttpWebRequest)WebRequest.Create(URL);
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.ContentType = "application/x-www-form-urlencoded";
            cc = new CookieContainer();

            webReq.Credentials = CredentialCache.DefaultCredentials;
            webReq.Proxy = WebRequest.GetSystemWebProxy();

            if (cookies == null)
            {
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                cookies = webResp.Cookies;
            }
            cc.Add(cookies);
            webReq.CookieContainer = cc;
        }

        public static void LoadLink(string link)
        {
            try
            {
                System.Diagnostics.Process.Start(link);
            }
            catch { }
        }
    }

    [Serializable]
    public class WikiBotException : Exception
    {
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public WikiBotException() { }
        public WikiBotException(string message)
            : base(message)
        {
            if (message == "login failed")
            {
                System.Windows.Forms.MessageBox.Show("Login Failed, please check your username and password, and that you are " +
                    "connected to the internet", "Login Failure", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        public WikiBotException(string message, Exception inner) : base(message, inner) { }
        protected WikiBotException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}  


