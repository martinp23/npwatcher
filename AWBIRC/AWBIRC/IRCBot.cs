/*
AWBBot
Copyright (C) 2007 Martin Peeks

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

//You'll notice that checkSVN() and checkSVN1() output different stuff.  This is an
// intentional bug, for now ;).

using System;
using System.Web;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Timers;


class IrcBot
{
    public static TcpClient ircClient;
    public static NetworkStream ircConn;
    public static StreamReader ircreader;
    public static StreamWriter ircwriter;
    public static string server = "irc.freenode.net";
    private static int port = 6667;
    private static string user = "USER AWBBot 8 * :Owner Martinp23";
    private static string botNick = "AWBBot";
    private static string channel = "#autowikibrowser";
    public static string inputLine;
    public static string nickname;
    public static System.Timers.Timer timer = new System.Timers.Timer(90000);
    public static System.Timers.Timer pingtimer = new System.Timers.Timer(15000);
    public static string startrev = "";
    public static bool stop;

    static void Main(string[] args)
    {
        WebClient wc = new WebClient();
        string src = wc.DownloadString("http://autowikibrowser.svn.sourceforge.net/viewvc/autowikibrowser/AWB/");
        string rx = "&amp;revision=([0-9]*)\">";
        startrev = Regex.Match(src, rx).ToString();
        startrev = startrev.Replace("&amp;revision=", "");
        startrev = startrev.Replace("\">", "");
       // int startrev1 = int.Parse(startrev) - 1;
       // startrev = startrev1.ToString();
        if (!stop)
        {
            try
            {
                ircClient = new TcpClient(server, port);
                ircConn = ircClient.GetStream();
                ircreader = new StreamReader(ircConn);
                ircwriter = new StreamWriter(ircConn);

                ircwriter.WriteLine(user);
                ircwriter.Flush();
                ircwriter.WriteLine("NICK " + botNick);
                ircwriter.Flush();

                ircwriter.WriteLine("JOIN " + channel);
                ircwriter.Flush();
                PingSender ping = new PingSender();
                ping.Start();

                CheckSVN svn = new CheckSVN();
                svn.Start();

                Console.WriteLine(ircConn);

                while (true)
                {
                    if (stop)
                    { break; }
                    ircwriter.WriteLine("NickServ IDENTIFY NO-YOU-CAN'T-HAVE-IT-:P");
                    ircwriter.Flush();

                    System.Timers.Timer timer = new System.Timers.Timer(90000);

                    //timerstart();
                //    pingtimerstart();

                    while ((inputLine = ircreader.ReadLine()) != null)
                    {
                        if (stop)
                        { break; }
                        string t = inputLine;
                        Console.WriteLine(t);

                        if (inputLine.Contains("!"))
                        {
                            Regex reg1 = new Regex(@"!(?<Code>\w+)$");
                            Match n = reg1.Match(inputLine);
                            if (n.Success == true)
                            {
                                string command = n.Groups[1].ToString(); //command
                                nickname = inputLine.Substring(1, inputLine.IndexOf("!") - 1);

                                talkNormal(command);
                            }
                        }

                    }
                }

            }

            #region exception handler
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                talkNormal("reset");
                stop = true;
            }
        }
    }


            #endregion

    #region Specialist code

    public static void talkNormal(string command)
    {
        if (Regex.Match(command, "bugs", RegexOptions.IgnoreCase).Success)
        {

            ircwriter.WriteLine("PRIVMSG #autowikibrowser :http://en.wikipedia.org/wiki/Wikipedia_talk:AutoWikiBrowser/Bugs ");
            ircwriter.Flush();
            Thread.Sleep(100);

        }

        if (Regex.Match(command, "features", RegexOptions.IgnoreCase).Success)
        {

            ircwriter.WriteLine("PRIVMSG #autowikibrowser :http://en.wikipedia.org/wiki/Wikipedia_talk:AutoWikiBrowser/Feature_requests ");
            ircwriter.Flush();
            Thread.Sleep(100);

        }

        if (Regex.Match(command, "dev", RegexOptions.IgnoreCase).Success)
        {

            ircwriter.WriteLine("PRIVMSG #autowikibrowser :http://en.wikipedia.org/wiki/Wikipedia_talk:AutoWikiBrowser/Dev ");
            ircwriter.Flush();
            Thread.Sleep(100);

        }

        if (Regex.Match(command, "checkpage", RegexOptions.IgnoreCase).Success)
        {

            ircwriter.WriteLine("PRIVMSG #autowikibrowser :http://en.wikipedia.org/wiki/Wikipedia_talk:AutoWikiBrowser/CheckPage ");
            ircwriter.Flush();
            Thread.Sleep(100);

        }

        if (Regex.Match(command, "checksvn1", RegexOptions.IgnoreCase).Success)
        {
        
            WebClient wc = new WebClient();
            string src = wc.DownloadString("http://autowikibrowser.svn.sourceforge.net/viewvc/autowikibrowser/AWB/");
            string rx = "&amp;revision=([0-9]*)\">";
            string newrev = Regex.Match(src, rx).ToString();
            newrev = newrev.Replace("&amp;revision=", "");
            newrev = newrev.Replace("\">", "");
            if (newrev != startrev)
            {
                int oldr = int.Parse(startrev);
                int newr = int.Parse(newrev);
                int intermediateRevs = newr - oldr;

                for (int i = 1; i <= intermediateRevs; i++)
                {
                    int rev = oldr + i;
                    string url = "http://autowikibrowser.svn.sourceforge.net/viewvc/autowikibrowser?view=rev&revision=" + rev.ToString();
                    string diff = wc.DownloadString(url);

                    //string comment = Regex.Match(diff, "<pre class=\"vc_log\">(.+?)*</pre>", RegexOptions.Multiline).ToString();

                    int start = diff.IndexOf("<pre class=\"vc_log\">");
                    int end = diff.IndexOf("</pre>", start);

                    string comment = diff.Substring(start, end - start);
                    comment = comment.Replace("<pre class=\"vc_log\">", "");
                    comment = comment.Replace("</pre>", "");
                    comment = HttpUtility.HtmlDecode(comment);
                    string user = Regex.Match(diff, "<th>Author:</th>\n<td>(.+?)*</td>").ToString();
                    user = user.Replace("<th>Author:</th>\n<td>", "");
                    user = user.Replace("</td>", "");
                    user = HttpUtility.HtmlDecode(user);
                    //comment = comment.Replace('\r', ' ');
                    //comment = comment.Replace("\n\n ", "\n\nPRIVMSG #autowikibrowser :");
                    string[] s2 = { "\n" };
                    
                    string[] commentar = comment.Split(s2, StringSplitOptions.RemoveEmptyEntries);

                    ircwriter.WriteLine("PRIVMSG #autowikibrowser :Revision {0} committed by {1} with comment:", rev.ToString(), user);
                    ircwriter.Flush();

                    foreach (string line in commentar)
                    {
                        ircwriter.WriteLine("PRIVMSG #autowikibrowser :" + line);
                        ircwriter.Flush();
                        Thread.Sleep(1000);
                    }

                    ircwriter.WriteLine("PRIVMSG #autowikibrowser :Review here: {0}", url);
                    ircwriter.Flush();
                    Thread.Sleep(1000);
                }
                startrev = newrev;
            }
           
        }

        if (Regex.Match(command, "checkSVN", RegexOptions.IgnoreCase).Success)
        {
            WebClient wc = new WebClient();
            string src = wc.DownloadString("http://autowikibrowser.svn.sourceforge.net/viewvc/autowikibrowser/AWB/");
            string rx = "&amp;revision=([0-9]*)\">";
            string newrev = Regex.Match(src, rx).ToString();
            newrev = newrev.Replace("&amp;revision=", "");
            newrev = newrev.Replace("\">", "");
            if (newrev != startrev)
            {
                int oldr = int.Parse(startrev);
                int newr = int.Parse(newrev);
                int intermediateRevs = newr - oldr;

                for (int i = 1; i <= intermediateRevs; i++)
                {
                    int rev = oldr + i;
                    string url = "http://autowikibrowser.svn.sourceforge.net/viewvc/autowikibrowser?view=rev&revision=" + rev.ToString();
                    string diff = wc.DownloadString(url);

                    //string comment = Regex.Match(diff, "<pre class=\"vc_log\">(.+?)*</pre>", RegexOptions.Multiline).ToString();
              
                    int start = diff.IndexOf("<pre class=\"vc_log\">");
                    int end = diff.IndexOf("</pre>", start);

                    string comment = diff.Substring(start, end - start);
                    comment = comment.Replace("<pre class=\"vc_log\">", "");
                    comment = comment.Replace("</pre>", "");
                    comment = HttpUtility.HtmlDecode(comment);
                    string user = Regex.Match(diff, "<th>Author:</th>\n<td>(.+?)*</td>").ToString();
                    user = user.Replace("<th>Author:</th>\n<td>", "");
                    user = user.Replace("</td>", "");
                    user = HttpUtility.HtmlDecode(user);
                    comment = comment.Replace('\n', ' ');
                    ircwriter.WriteLine("PRIVMSG #autowikibrowser :Revision {0} committed by {1} with comment \"{2}\".  Review here: {3}", rev.ToString(), user, comment, url);
                    ircwriter.Flush();
                    Thread.Sleep(1000);
                }
                startrev = newrev;
            }
            else
            {
                ircwriter.WriteLine("PRIVMSG #autowikibrowser :No changes committed to SVN since last report");
                ircwriter.Flush();
            }
            Thread.Sleep(100);

        }
        if (Regex.Match(command, "help", RegexOptions.IgnoreCase).Success)
        {
            ircwriter.WriteLine("PRIVMSG " + nickname + " :Place \"!\" in front of the following commands to run them:");
            ircwriter.Flush();
            Thread.Sleep(1500);
            ircwriter.WriteLine("PRIVMSG " + nickname + " :\"bugs\" - get link to bug list");
            ircwriter.Flush();
            Thread.Sleep(1500);
            ircwriter.WriteLine("PRIVMSG " + nickname + " :\"features\" - get link to feature request list");
            ircwriter.Flush();
            Thread.Sleep(1500);
            ircwriter.WriteLine("PRIVMSG " + nickname + " :\"dev\" - get link to dev talk page");
            ircwriter.Flush();
            Thread.Sleep(1500);
            ircwriter.WriteLine("PRIVMSG " + nickname + " :\"checkpage\" - get link to checkpage requests");
            ircwriter.Flush();
            Thread.Sleep(1500);
            ircwriter.WriteLine("PRIVMSG " + nickname + " :\"checkSVN\" - have the bot report any recent SVN commits");
            ircwriter.Flush();
            Thread.Sleep(1500);
        }


        if (Regex.Match(command, "quit", RegexOptions.IgnoreCase).Success)
        {
            nickname = inputLine.Substring(inputLine.IndexOf("!") + 3, (inputLine.Length - inputLine.IndexOf(" PRIVMSG") - 4));
            if (nickname == "martin@wikipedia/Martinp23")
            {
                ircreader.Close();
                ircwriter.Close();
                ircClient.Close();
                Environment.Exit(0x000);
            }

        }

        if (Regex.Match(command, "reset", RegexOptions.IgnoreCase).Success)
        {
            ircClient.Close();
            ircwriter.Close();
            stop = true;
        }

    }
    #endregion

    public static void OnTimerEvent(object source, EventArgs e)
    {
        talkNormal("checkSVN1");
        timer.EndInit();
        timer.BeginInit();
    }

    public static void timerstart()
    {
        timer.Start();
        timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerEvent);

    }

    
}

