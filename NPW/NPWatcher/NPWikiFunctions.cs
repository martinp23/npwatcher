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
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using NPWatcher;//should become obsolete, for intermediate use only

namespace NPWikiFunctions
{
    public class Util
    {
        public const string wikiurl = "http://en.wikipedia.org/w/index.php?title=";
        public const string apiurl = "http://en.wikipedia.org/w/api.php";
        public const string queryurl = "http://en.wikipedia.org/w/query.php";

        public static Boolean login(string Username, string Userpass, CookieContainer cookies)
        {
            string eUsername = HttpUtility.UrlEncode(Username);
            string eUserpass = HttpUtility.UrlEncode(Userpass);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiurl + "?action=login&lgname=" + eUsername + "&lgpassword=" + eUserpass);
            request = annotateRequest(request, cookies);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string content = sr.ReadToEnd();
            string matchString = "login result=&quot;Success&quot";
            string test = Regex.Escape(matchString);
            Regex regex = new Regex(test);
            Boolean result = regex.Match(content).Success;
            return regex.Match(content).Success;
        }

        private static HttpWebRequest annotateRequest(HttpWebRequest request, CookieContainer cookies)
        {
            request.CookieContainer = cookies;
            request.Method = "GET";//should be looked into
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "NPWatcher/1.0";
            return request;
        }

        public static Boolean approved(string username)
        {
            //check
            username = Regex.Escape(username);
            username = username.Replace("-", "[-]");
            Regex r = new Regex(username, RegexOptions.IgnoreCase);
            Match m = r.Match(new WikiFunctions().getWikiText("User:Martinp23/NPWatcher/Checkpage/Users"));
            if (!m.Success)
            {
                MessageBox.Show("You are not approved to use NPWatcher.  Please request approval from Martinp23", "Not Approved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return m.Success;
        }
    }
}
