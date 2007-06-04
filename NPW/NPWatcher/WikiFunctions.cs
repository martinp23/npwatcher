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

namespace NPWatcher
{
    class WikiFunctions
    {
        private HttpWebRequest webReq;
        private CookieCollection cookies;
        public static string watch = "0";
        //internal static bool asAdmin;

        public bool login(string Username, string Userpass)
        {
            webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=Special:Userlogin&action=submitlogin&type=login");
            String postData = String.Format("wpName=+{0}&wpPassword={1}&wpRemember=1&wpLoginattempt=Log+in",
                new string[] { Username, Userpass });
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
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


            string src = "";

            webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/api.php?action=query&list=watchlist&wllimit=3&format=xml");
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.ContentType = "application/x-www-form-urlencoded";
            CookieContainer cc = new CookieContainer();
            cc.Add(cookies);
            webReq.CookieContainer = cc;
            webReq.Credentials = CredentialCache.DefaultCredentials;
            webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();


            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            src = work.ReadToEnd();


            if (src.Contains("wlnotloggedin"))
            {
                WikiBotException ex = new WikiBotException("login failed");
                return false;
            }
            else
            { return true; }

        }




        ///Get newpages from 
        ///http://en.wikipedia.org/w/index.php?title=Special:Newpages&namespace=0&limit=20&offset=0&feed=atom
        public StringCollection getCat(string limit, string category)
        {
            string src = "";
            webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/query.php?what=category&cptitle=" + category + "&cplimit=" + limit + "&format=xml");
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.ContentType = "application/x-www-form-urlencoded";
            CookieContainer cc = new CookieContainer();
            cc.Add(cookies);
            webReq.CookieContainer = cc;
            webReq.Credentials = CredentialCache.DefaultCredentials;
            webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();


            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            src = work.ReadToEnd();
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

        public StringCollection getImgLinks(string limit, string image)
        {
            Regex nextPortionRE = new Regex("&amp;from=(.*?)\" title=\"");
            StringCollection a = new StringCollection();
            MatchCollection mcpt;
            string src = "";

            do
            {

                webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/query.php?what=imagelinks&titles=" + image + "&format=xml");
                webReq.UserAgent = "NPWatcher/1.0";
                webReq.ContentType = "application/x-www-form-urlencoded";
                CookieContainer cc = new CookieContainer();
                cc.Add(cookies);
                webReq.CookieContainer = cc;
                webReq.Credentials = CredentialCache.DefaultCredentials;
                webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;

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


        public StringCollection getNPs(string limit)
        {

            string src = "";
            StringCollection strCol = new StringCollection();
           // StringCollection strCol1 = new StringCollection();
          //  StringCollection watchlist = new StringCollection();
            MatchCollection matches;
           // Regex nextPortionRE = new Regex("<watchlist wlstart=\"(.+?)\" />");
            Regex pageTitleTagRE = new Regex("<title>([^<]*?)</title>");


            webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=Special:Newpages&namespace=0&limit=" + limit + "&offset=0&feed=atom");
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.ContentType = "application/x-www-form-urlencoded";
            CookieContainer cc = new CookieContainer();
            cc.Add(cookies);
            webReq.CookieContainer = cc;
            webReq.Credentials = CredentialCache.DefaultCredentials;
            webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();


            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            src = work.ReadToEnd();
            src = HttpUtility.HtmlDecode(src);
            matches = pageTitleTagRE.Matches(src);
            foreach (Match match in matches)
            { strCol.Add(match.Groups[1].Value); }

            strCol.RemoveAt(0);

            return strCol;
        }

        public string getWikiText(string page)
        {

            string src = "";
            webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=" + page + "&action=raw&ctype=text/plain&dontcountme=s");
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.ContentType = "application/x-www-form-urlencoded";
            CookieContainer cc = new CookieContainer();
            cc.Add(cookies);
            webReq.CookieContainer = cc;
            webReq.Credentials = CredentialCache.DefaultCredentials;
            webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();


                Stream srcstrm = webResp1.GetResponseStream();
                StreamReader work = new StreamReader(srcstrm);
                src =  work.ReadToEnd();
                return src;
            }
            catch (WebException e)
            {
                src = "";
                return src;
            }
            finally
            {

            }
            return src;
        }

        public string GetCreator(string page)
        {


            string src = "";
            webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/api.php?action=query&prop=revisions&titles=" + page + "&rvlimit=5&rvprop=user&rvlimit=1&rvdir=newer&format=xml");
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.ContentType = "application/x-www-form-urlencoded";
            CookieContainer cc = new CookieContainer();
            cc.Add(cookies);
            webReq.CookieContainer = cc;
            webReq.Credentials = CredentialCache.DefaultCredentials;
            webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();


            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            src = HttpUtility.HtmlDecode(work.ReadToEnd());


           
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
                string src = "";
                webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=" + page + "&action=edit");
                webReq.UserAgent = "NPWatcher/1.0";
                webReq.ContentType = "application/x-www-form-urlencoded";
                CookieContainer cc = new CookieContainer();
                cc.Add(cookies);
                webReq.CookieContainer = cc;
                webReq.Credentials = CredentialCache.DefaultCredentials;
                webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();


                Stream srcstrm = webResp1.GetResponseStream();
                StreamReader work = new StreamReader(srcstrm);
                src = work.ReadToEnd();

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



                webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=" + page + "&action=submit");
                webReq.UserAgent = "NPWatcher/1.0";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.Method = "POST";
                //CookieContainer cc = new CookieContainer();

                //cc.Add(cookies);
                webReq.CookieContainer = cc;
                webReq.Credentials = CredentialCache.DefaultCredentials;
                webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
                watch = "";

                string postData = string.Format("wpSection=&wpStarttime={0}&wpEdittime={1}&wpScrolltop=" +
                    "&wpTextbox1={2}&wpWatchThis={5}&wpSummary={3}&wpSave=Save%20Page&wpEditToken={4}",
                    new string[] { DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss"), editTime,
                HttpUtility.UrlEncode(newtxt), HttpUtility.UrlEncode(editsummary), editToken, watch });

                byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                webReq.ContentLength = postBytes.Length;
                Stream reqStrm = webReq.GetRequestStream();
                reqStrm.Write(postBytes, 0, postBytes.Length);
                reqStrm.Close();
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                StreamReader strmReader = new StreamReader(webResp.GetResponseStream());
                string respStr = strmReader.ReadToEnd();
                strmReader.Close();
                webResp.Close();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Something sinister has happened.  The page has been deleted/marked for deletion, but the program was unable to leave a user warning.  If possible, please could you report the page you just deleted/tagged to Martinp23, so he can look into it.  Sorry!");
            }




        }

        public void Save(string page, string newtxt, string editsummary, bool watchthis)
        {
            try
            {
                string src = "";
                webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=" + page + "&action=edit");
                webReq.UserAgent = "NPWatcher/1.0";
                webReq.ContentType = "application/x-www-form-urlencoded";
                CookieContainer cc = new CookieContainer();
                cc.Add(cookies);
                webReq.CookieContainer = cc;
                webReq.Credentials = CredentialCache.DefaultCredentials;
                webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;

                HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();


                Stream srcstrm = webResp1.GetResponseStream();
                StreamReader work = new StreamReader(srcstrm);
                src = work.ReadToEnd();

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



                webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=" + page + "&action=submit");
                webReq.UserAgent = "NPWatcher/1.0";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.Method = "POST";
                //CookieContainer cc = new CookieContainer();

                //cc.Add(cookies);
                webReq.CookieContainer = cc;
                webReq.Credentials = CredentialCache.DefaultCredentials;
                webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
                if (watchthis)
                { watch = "checked"; }
                else
                { watch = "off"; }

                string postData = string.Format("wpSection=&wpStarttime={0}&wpEdittime={1}&wpScrolltop=" +
                    "&wpTextbox1={2}&wpWatchThis={5}&wpSummary={3}&wpSave=Save%20Page&wpEditToken={4}",
                    new string[] { DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss"), editTime,
                HttpUtility.UrlEncode(newtxt), HttpUtility.UrlEncode(editsummary), editToken, watch });

                byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                webReq.ContentLength = postBytes.Length;
                Stream reqStrm = webReq.GetRequestStream();
                reqStrm.Write(postBytes, 0, postBytes.Length);
                reqStrm.Close();
                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                StreamReader strmReader = new StreamReader(webResp.GetResponseStream());
                string respStr = strmReader.ReadToEnd();
                strmReader.Close();
                webResp.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Something sinister has happened.  The user has not been warned - please contact Martinp23 with code E1, and mention the page (" + page + ").  Sorry!");
            }




        }
       


        public void Deletepg(string page, string editsummary)
        {
            string src = "";
            webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=" + page + "&action=edit");
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.ContentType = "application/x-www-form-urlencoded";
            CookieContainer cc = new CookieContainer();
            cc.Add(cookies);
            webReq.CookieContainer = cc;
            webReq.Credentials = CredentialCache.DefaultCredentials;
            webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();


            Stream srcstrm = webResp1.GetResponseStream();
            StreamReader work = new StreamReader(srcstrm);
            src = HttpUtility.HtmlDecode(work.ReadToEnd());

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



            webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=" + page + "&action=delete");
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.Method = "POST";
            //CookieContainer cc = new CookieContainer();

            //cc.Add(cookies);
            webReq.CookieContainer = cc;
            webReq.Credentials = CredentialCache.DefaultCredentials;
            webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
            watch = "true";

            string postData = string.Format("wpSection=&wpStarttime={0}&wpEdittime={1}&wpScrolltop=" +
                "&wpReason={2}&wpConfirmB=Delete%20Page&wpEditToken={3}",
                new string[] { DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss"), editTime,
                 HttpUtility.UrlEncode(editsummary), editToken });

            byte[] postBytes = Encoding.UTF8.GetBytes(postData);
            webReq.ContentLength = postBytes.Length;
            Stream reqStrm = webReq.GetRequestStream();
            reqStrm.Write(postBytes, 0, postBytes.Length);
            reqStrm.Close();
            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
            StreamReader strmReader = new StreamReader(webResp.GetResponseStream());
            string respStr = strmReader.ReadToEnd();
            strmReader.Close();
            webResp.Close();






        }

        public bool getLogInStatus()
        {
            Regex LoginRegex = new Regex("var wgUserName = (.*?);", RegexOptions.Compiled);

            string src = "";
            webReq = (HttpWebRequest)WebRequest.Create("http://en.wikipedia.org/w/index.php?title=Wikipedia:Sandbox&action=edit");
            webReq.UserAgent = "NPWatcher/1.0";
            webReq.ContentType = "application/x-www-form-urlencoded";
            CookieContainer cc = new CookieContainer();
            cc.Add(cookies);
            webReq.CookieContainer = cc;
            webReq.Credentials = CredentialCache.DefaultCredentials;
            webReq.Proxy.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse webResp1 = (HttpWebResponse)webReq.GetResponse();


                Stream srcstrm = webResp1.GetResponseStream();
                StreamReader work = new StreamReader(srcstrm);
                src = HttpUtility.HtmlDecode(work.ReadToEnd());
                
            }
            catch (WebException e)
            {
                src = "";
                
            }

            Match m = LoginRegex.Match(src);

            if (!m.Success || m.Groups[1].Value == "null")
                return false;
            else
                return true;
        }

    }
    



        [global::System.Serializable]
        public class WikiBotException : Exception
        {
            //
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


