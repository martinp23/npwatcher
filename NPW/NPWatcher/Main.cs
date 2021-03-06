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
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Globalization;
using System.Web;

namespace NPWatcher
{
    public partial class Main : Form
    {
        internal static string username = "", password = "";
        internal static bool dialogcancel;
        internal static bool success;
        internal WikiFunctions wf = new WikiFunctions();
        internal string page2 = "";
        internal static string prodreasonstr = "";
        internal static bool doprod, asAdmin, crsuc, afdsuc, nnchoicessuc;
        internal static string dbReason, afdCat, afdReason, nntag = "", wikitextpage2 = "";
        internal static bool editsuccess;
        internal static string cwr;
        private static int refreshInterval;

        string currentSettingsFile;

        internal static Settings settings = new Settings();
        private readonly ListSource listsource = new ListSource();

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            LoadSettings("DefaultSettings.xml");
            string listofv = wf.GetWikiText("User:Martinp23/NPWatcher/Checkpage/Versions");
            string versioncurstr = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            Cursor = Cursors.Default;
            if (!listofv.Contains(versioncurstr))
            {
                MessageBox.Show("Please download the latest version of NPWatcher", "New Version", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                Close();
            }

            //LOGIN AND QUIT ON CANCEL CODE
            LogOn login = new LogOn();
            login.ShowDialog();

            while (!success)
            {
                Cursor = Cursors.WaitCursor;
                success = wf.Login(username, password);
                if (!success)
                {
                    LogOn login1 = new LogOn();
                    login1.ShowDialog();
                    if (dialogcancel)
                        Close();
                }
                else
                {
                    username = Regex.Escape(username);
                    username = username.Replace("-", "[-]");
                    Regex r = new Regex(username, RegexOptions.IgnoreCase);
                    Match m = r.Match(wf.GetWikiText("User:Martinp23/NPWatcher/Checkpage/Users"));

                    Cursor = Cursors.Default;
                    if (!m.Success)
                    {
                        MessageBox.Show(
                            "You are not approved to use NPWatcher.  Please request approval from Martinp23",
                            "Not Approved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                    else
                    {
                        foreach (string admin in wf.adminslist)
                        {
                            if (!asAdmin)
                                asAdmin = r.Match(admin).Success;
                        }
                        //asAdmin = wf.CheckIfAdmin();

                        MessageBox.Show("Logged in", "Logged in", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                //LOGIN DONE
                if (!asAdmin)
                {
                    rmvBtn.Visible = false;
                    tabPage3.Dispose();
                    tabPage4.Dispose();
                }
            }
        }

        private void stubCombo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(stubCombo.Text) && !stubCombo.Items.Contains(stubCombo.Text))
                stubCombo.Items.Add(stubCombo.Text);
        }

        private void getlistBtn_Click(object sender, EventArgs e)
        {
            PopulateList(true);
            SetTimer();
        }

        private void PopulateList(bool frombtn)
        {
            StringCollection nps = new StringCollection();

            string limit;
            if (frombtn)
                listsource.ShowDialog();
            string category = listsource.category;
            settings.hidePatrolled = listsource.hidepatrolled;
            settings.hideBots = listsource.hidebot;
            settings.hideAdmins = listsource.hideadmin;

            switch (category)
            {
                case "NPRad":
                case "NPRadOld":
                    {
                        if (limitCB.SelectedItem != null)
                            limit = limitCB.SelectedItem.ToString();
                        else
                        {
                            limit = "20";
                            limitCB.SelectedItem = "20";
                        }

                        nps = wf.GetNPs(limit, category.Contains("Old"));
                        break;
                    }
                case "CSDRad":
                    {
                        if (limitCB.SelectedItem != null)
                            limit = limitCB.SelectedItem.ToString();
                        else
                        {
                            limit = "100";
                            limitCB.SelectedItem = "100";
                        }
                        nps = wf.GetCat(limit, "Candidates for speedy deletion");
                        break;
                    }
                default:
                    {
                        if (category != null)
                        {
                            if (limitCB.SelectedItem != null)
                                limit = limitCB.SelectedItem.ToString();
                            else
                            {
                                limit = "20";
                                limitCB.SelectedItem = "20";
                            }
                            nps = wf.GetCat(limit, category);
                        }
                        else
                        {
                            MessageBox.Show("Please choose a list source");
                        }
                        break;
                    }
            }

            pageList.Items.Clear();
            foreach (string p in nps)
                pageList.Items.Add(p);
        }

        private void pageList_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //should fix the ampersands issue...
                page2 = HttpUtility.UrlEncode(pageList.SelectedItem.ToString());
                webBrowser1.Navigate(wf.Url + page2);
                wikitextpage2 = wf.GetWikiText(page2);
            }
            catch (NullReferenceException) { }
        }

        #region speedy
        private void dbBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
            {
                CustomReason cr = new CustomReason();
                cr.ShowDialog();
                if (crsuc)
                {
                    if (dbReason == null) { Delete("db", "No reason given"); }
                    else
                    {
                        string reason1 = "db|" + dbReason;
                        Delete(reason1, dbReason);
                    }
                    crsuc = false;
                }
            }
            Greyin();
        }

        private void dbbioBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
            {
                if (!asAdmin)
                {
                    NNChoices nn = new NNChoices();
                    nn.ShowDialog();

                    if (nnchoicessuc)
                    {
                        Delete(nntag, "Article about a non-notable individual, band, service, website or other entity");
                        nnchoicessuc = false;
                    }
                }
                else
                {
                    Delete(nntag, "Article about a non-notable individual, band, service, website or other entity");
                    nnchoicessuc = false;
                }
            }
            Greyin();
        }

        private void dbspamBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
                Delete("db-spam", "Blatant advertising, [[WP:CSD#G11]]");
            Greyin();
        }

        private void db_userreq_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
                Delete("db-spam", "Author request, [[WP:CSD#G7]]");
            Greyin();
        }

        private void dbforeignBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
                Delete("db-foreign", "Article is not in English");
            Greyin();
        }

        private void dbRepostBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
                Delete("db-repost", "Repost of previously deleted material");
            Greyin();
        }

        private void dbNonsBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
            {
                Msgnons();
                Delete("db-nonsense", "Nonsense page");
            }
            Greyin();
        }

        private void dbTest_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
            {
                Msgtest();
                Delete("db-test", "Vandalism/Test");
            }
            Greyin();
        }

        private void dbvandBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
            {
                Msgvand();
                Delete("db-vandalism", "Vandalism");
            }
            Greyin();
        }

        private void dbBlankBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
                Delete("db-blank", "Page blanked by only editor");
            Greyin();
        }

        private void dbtalkBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
                Delete("db-talk", "Talk page of non-existant article");

            Greyin();
        }

        private void dbattackBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
            {
                Msgattack();
                Delete("db-attack", "Attack page");
            }
            Greyin();
        }

        private void dbemptyBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
                Delete("db-empty", "Empty page");

            Greyin();
        }

        private void dbR1Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
                Delete("db-redirnone", "Redirect to non-existant page, [[WP:CSD#R1]]");

            Greyin();
        }

        private void dbR2Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
                Delete("db-rediruser", "Redirect to user space [[WP:CSD#R2]]");

            Greyin();
        }

        private void dbR3Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!string.IsNullOrEmpty(page2))
                Delete("db-redirtypo", "Implausibe typo, [[WP:CSD#R3]]");

            Greyin();
        }

        private void dbCvBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!asAdmin)
            {
                if (!string.IsNullOrEmpty(page2))
                {
                    if (cvLinkTxt.Text != null)
                    {
                        Delete("db-copyvio|url=" + cvLinkTxt.Text, "[[WP:C|Copyright-violation]] - [[WP:CSD#G12]]");
                    }
                    else
                    {
                        MessageBox.Show("Please enter the original source of the copy-vioed text (as a URL)");
                    }
                }
            }
            else
            {
                if (cvLinkTxt.Text != null)
                {
                    Delete("Copyvio", "Copyright violation from " + cvLinkTxt.Text);
                }
                else
                {
                    MessageBox.Show("Please enter the location of the copy-vioed text (as a URL)");
                }
            }
            Greyin();
        }

        private void Delete(string p, string r)
        {
            if (!asAdmin)
            {
                string wikitext = wf.GetWikiText(page2);
                string newtxt = "{{" + p + "}}\r\n" + wikitext;
                Save(page2, newtxt, "Marking page for deletion using [[WP:NPW|NPWatcher]]");
                if (editsuccess)
                {
                    MarkPatrolled();
                    Msgfa();
                    Msgnn();
                    Msgcw();
                }
            }
            else
            {
                Msgfa();
                Msgnn();
                Msgcw();
                if (SortLogin())
                    wf.Deletepg(page2, "Deleting page - reason was: \"" + r + "\" using [[WP:NPW|NPWatcher]]");

                if (!page2.StartsWith("Image:"))
                {
                    string talktxt = wf.GetWikiText("Talk:" + page2);
                    if (!string.IsNullOrEmpty(talktxt))
                    {
                        DialogResult dr = MessageBox.Show("Would you like to delete the article talk page too?", "Talk page",
                            MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            if (SortLogin())
                                wf.Deletepg("Talk:" + page2, "Deleting page as talk of deleted article using [[WP:NPW|NPWatcher]]");
                        }
                    }
                }
                else
                {
                    string talktitle = "Image talk:" + page2.Substring(6);
                    string talktxt = wf.GetWikiText("Talk:" + page2);
                    if (!string.IsNullOrEmpty(talktxt))
                    {
                        DialogResult dr = MessageBox.Show("Would you like to delete the image talk page too?", "Talk page",
                            MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            if (SortLogin())
                                wf.Deletepg(talktitle, "Deleting page as talk of deleted image using [[WP:NPW|NPWatcher]]");
                        }
                    }
                }
            }
        }

        private void Save(string page3, string newtxt, string summary)
        {
            string wikitextcur = wf.GetWikiText(page2);
            if (wikitextcur != wikitextpage2)
            {
                if (string.IsNullOrEmpty(wikitextcur))
                {
                    MessageBox.Show("The page appears to have been deleted by an administrator (or blanked).  The page will now reload so that you can verify this");
                }
                else
                {
                    MessageBox.Show("The page have been changed since you loaded it.  It will now reload so that you can (re)consider your proposed changes");
                }
                editsuccess = false;
            }
            else
            {
                if (SortLogin())
                    if (SortLogin())
                        wf.Save(page3, newtxt, summary);
                editsuccess = true;
            }
            wikitextpage2 = wf.GetWikiText(page2);

            webBrowser1.Refresh();
        }

        #endregion

        #region warnings, prods and AfD (application thereof)
        private void Msgfa()
        {
            if (firstarticle.Checked)
            {
                string creator = wf.GetCreator(page2);
                creator = "User_talk:" + creator;
                string wikitext = wf.GetWikiText(creator);
                string newtxt;
                if (!asAdmin)
                {
                    newtxt = wikitext + "\r\n\r\n{{subst:firstarticle|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (SortLogin())
                        wf.Save(creator, newtxt, "Posting {{firstarticle}} using [[WP:NPW|NPWatcher]]");
                }
                else
                {
                    if (!wikitext.Contains("<!-- Template:Firstarticle -->"))
                    {
                        newtxt = wikitext + "\r\n\r\n{{subst:firstarticle|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                        if (SortLogin())
                            wf.Save(creator, newtxt, "Posting {{firstarticle}} using [[WP:NPW|NPWatcher]]");
                    }
                }
            }
        }

        private void Msgnn()
        {
            if (notabilitywarn.Checked)
            {
                string creator = wf.GetCreator(page2);
                creator = "User_talk:" + creator;
                string wikitext = wf.GetWikiText(creator);
                string newtxt;

                if (!asAdmin)
                {
                    newtxt = wikitext + "\r\n\r\n{{subst:nn-warn|" + HttpUtility.UrlDecode(page2) + "|header=1}} ~~~~";
                    if (SortLogin())
                        wf.Save(creator, newtxt, "Posting {{nn-warn}} using [[WP:NPW|NPWatcher]].");
                }
                else
                {
                    newtxt = wikitext + "\r\n\r\n{{subst:nn-warn-deletion|" + HttpUtility.UrlDecode(page2) + "|header=1}} ~~~~";
                    if (SortLogin())
                        wf.Save(creator, newtxt, "Posting {{nn-warn-deletion}} using [[WP:NPW|NPWatcher]].");
                }
            }
        }

        private void Msgcw()
        {
            if (customWarnCB.Checked)
            {
                CustomWarning cw = new CustomWarning();
                cw.ShowDialog();
                if (cw.DialogResult == DialogResult.OK)
                {
                    string creator = wf.GetCreator(page2);
                    creator = "User_talk:" + creator;
                    string wikitext = wf.GetWikiText(creator);
                    string newtxt;
                    string warning = cwr;

                    if (!asAdmin)
                    {
                        newtxt = wikitext + "\r\n\r\n{{subst:" + warning + "|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                        if (SortLogin())
                            wf.Save(creator, newtxt, "Posting {{" + warning + "}} using [[WP:NPW|NPWatcher]].");
                    }
                    else
                    {
                        if (!wikitext.Contains("<!-- Template:" + warning + " -->"))
                        {
                            newtxt = wikitext + "\r\n\r\n{{subst:" + warning + "|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                            if (SortLogin())
                                wf.Save(creator, newtxt, "Posting {{" + warning + "}} using [[WP:NPW|NPWatcher]].");

                        }
                    }
                }
                cw.Close();
            }
        }

        private void Msgnons()
        {
            string creator = wf.GetCreator(page2);
            if (!string.IsNullOrEmpty(creator))
            {
                creator = "User_talk:" + creator;
                string wikitext = wf.GetWikiText(creator);
                string newtxt;
                if (!asAdmin)
                {
                    newtxt = wikitext + "\r\n{{subst:Nonsensepages|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (SortLogin())
                        wf.Save(creator, newtxt, "Posting {{Nonsensepages}} using [[WP:NPW|NPWatcher]].");
                }
                else
                {
                    if (!wikitext.Contains("<!-- Template:Nonsensepages -->"))
                    {
                        newtxt = wikitext + "\r\n{{subst:Nonsensepages|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                        if (SortLogin())
                            wf.Save(creator, newtxt, "Posting {{Nonsensepages}} using [[WP:NPW|NPWatcher]].");
                    }
                }
            }
        }

        private void Msgtest()
        {
            Greyout();
            string creator = wf.GetCreator(page2);
            creator = "User_talk:" + creator;
            string wikitext = wf.GetWikiText(creator);
            string newtxt;
            if (!asAdmin)
            {
                newtxt = wikitext + "\r\n{{subst:test1article|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                if (SortLogin())
                    wf.Save(creator, newtxt, "Posting {{test1article}} using [[WP:NPW|NPWatcher]].");
            }
            else
            {
                if (!wikitext.Contains("<!-- Template:Test1article (first level warning) -->"))
                {
                    newtxt = wikitext + "\r\n{{subst:test1article|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (SortLogin())
                        wf.Save(creator, newtxt, "Posting {{test1article}} using [[WP:NPW|NPWatcher]].");
                }
            }
        }

        private void Msgvand()
        {
            Greyout();
            string creator = wf.GetCreator(page2);
            creator = "User_talk:" + creator;
            string wikitext = wf.GetWikiText(creator);
            string newtxt;
            if (!asAdmin)
            {
                newtxt = wikitext + "\r\n{{subst:test2article-n|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                if (SortLogin())
                    wf.Save(creator, newtxt, "Posting {{test2article}} using [[WP:NPW|NPWatcher]].");
            }
            else
            {
                if (!wikitext.Contains("<!-- Template:Test2article (second level warning) -->"))
                {
                    newtxt = wikitext + "\r\n{{subst:test2article-n|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (SortLogin())
                        wf.Save(creator, newtxt, "Posting {{test2article}} using [[WP:NPW|NPWatcher]].");
                }
            }
        }

        private void Msgattack()
        {
            string creator = wf.GetCreator(page2);
            if (!string.IsNullOrEmpty(creator))
            {
                creator = "User_talk:" + creator;
                string wikitext = wf.GetWikiText(creator);
                string newtxt;
                if (!asAdmin)
                {
                    newtxt = wikitext + "\r\n{{subst:Attack|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (SortLogin())
                        wf.Save(creator, newtxt, "Posting {{Attack|" + HttpUtility.UrlDecode(page2) + "}} using [[WP:NPW|NPWatcher]].");
                }
                else
                {
                    if (!wikitext.Contains("<!-- Template:Attack -->"))
                    {
                        newtxt = wikitext + "\r\n{{subst:Attack|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                        if (SortLogin())
                            wf.Save(creator, newtxt, "Posting {{Attack|" + HttpUtility.UrlDecode(page2) + "}} using [[WP:NPW|NPWatcher]].");
                    }
                }
            }
        }

        private void prodBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            Removetags();
            Prod prodreason = new Prod();
            prodreason.ShowDialog();

            if (doprod)
            {
                MarkPatrolled();
                string message = prodreasonstr;

                string oldtxt = wf.GetWikiText(page2);
                string newtxt = oldtxt;
                string prod = "{{subst:prod|" + message + "}}\r\n";
                newtxt = prod + newtxt;

                Save(page2, newtxt, "[[WP:PROD|PRODDING]] article with [[WP:NPW|NPWatcher]]");

                if (editsuccess)
                {
                    string user = wf.GetCreator(page2);
                    user = "User_talk:" + user;
                    string userpage = wf.GetWikiText(user);

                    string userpagenew = userpage + "\r\n{{subst:PRODWarning|" + HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (SortLogin())
                        wf.Save(user, userpagenew, "Warning user of prodding using [[WP:NPW|NPWatcher]]");
                    doprod = false;
                }
            }
            doprod = false;

            Greyin();
        }

        private void AfDBtn_Click(object sender, EventArgs e)
        {
            Greyout();

            AFDForm afd = new AFDForm();
            afd.ShowDialog();
            string wikitextnew = wf.GetWikiText(page2);
            if (wikitextnew != wikitextpage2)
            {
                if (string.IsNullOrEmpty(wikitextnew))
                {
                    MessageBox.Show("The page appears to have been deleted by an administrator (or blanked).  The page will now reload so that you can verify this");
                    editsuccess = false;
                }
                else
                {
                    MessageBox.Show("The page have been changed since you loaded it.  It will now reload so that you can (re)consider your proposed changes");
                    editsuccess = false;
                }
            }
            else
            {
                if (afdsuc)
                {
                    //add AfD tag, and determine AfD number
                    Removetags();
                    MarkPatrolled();
                    string txt = wf.GetWikiText("Wikipedia:Articles for deletion/" + page2);
                    int number = 1;
                    while (!string.IsNullOrEmpty(txt))
                    {
                        number += 1;
                        switch (number)
                        {
                            case 2:
                                txt = wf.GetWikiText("Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)");
                                break;
                            case 3:
                                txt = wf.GetWikiText("Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)");
                                break;
                            default:
                                txt = wf.GetWikiText("Wikipedia:Articles for deletion/" + page2 + " (" + number + "th nomination)");
                                break;
                        }
                    }

                    string pgtxt = wf.GetWikiText(page2);
                    string numbertxt;
                    if (number == 2) { numbertxt = "2nd"; }
                    else if (number == 3) { numbertxt = "3nd"; }
                    else { numbertxt = number + "nd"; }

                    if (number == 1)
                    {
                        pgtxt = "{{subst:afd1}}\r\n" + pgtxt;
                    }
                    else
                    {
                        pgtxt = "{{subst:afdx|" + numbertxt + "\r\n" + pgtxt;
                    }
                    Save(page2, pgtxt, "Nominating page for deletion using [[WP:NPW|NPWatcher]]");


                    string afdnom = "{{subst:afd2|pg=" + HttpUtility.UrlDecode(page2) + "|cat=" + afdCat + "|text=" + afdReason + "}} ~~~~";
                    string afdnompg = "";
                    if (number == 1)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2;
                        if (SortLogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2, afdnom, "Nominating [[" + HttpUtility.UrlDecode(page2) + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }
                    if (number == 2)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)";
                        if (SortLogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)", afdnom, "Nominating [[" + HttpUtility.UrlDecode(page2) + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }
                    else if (number == 3)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)";
                        if (SortLogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)", afdnom, "Nominating [[" + HttpUtility.UrlDecode(page2) + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }
                    else if (number != 1 && number != 2 && number != 3)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (" + number + "th nomination)";
                        if (SortLogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2 + " (" + number + "th nomination)", afdnom, "Nominating [[" + HttpUtility.UrlDecode(page2) + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }

                    DateTime now = DateTime.UtcNow;

                    DateTimeFormatInfo dfi = new DateTimeFormatInfo();
                    dfi.FullDateTimePattern = "yyyy MMMM dd";
                    string datetoday = now.ToString("F", dfi);
                    string date2 = datetoday.Substring(datetoday.Length - 1);
                    datetoday = datetoday.Remove(datetoday.Length - 1);
                    if (datetoday.EndsWith("0"))
                    {
                        datetoday = datetoday.Remove(datetoday.Length - 1);
                    }
                    datetoday = datetoday + date2;

                    string logpg = wf.GetWikiText("Wikipedia:Articles for deletion/Log/" + datetoday);
                    logpg = Regex.Replace(logpg, "<!-- Add new entries to the TOP of the following list -->", "<!-- Add new entries to the TOP of the following list -->" + "\r\n{{" + afdnompg + "}}");
                    if (SortLogin())
                        wf.Save("Wikipedia:Articles for deletion/Log/" + datetoday, logpg, "Adding [[" + HttpUtility.UrlDecode(page2) + "]] to list using [[WP:NPW|NPWatcher]]");


                    afdsuc = false;

                }
            }
            webBrowser1.Refresh();
            Greyin();
        }
        #endregion

        #region Maintainance tags
        //Doesn't use Mark(..)

        //private void Mark(string template)
        //{
        //    string txt = wf.GetWikiText(page2);
        //    string newtxt = template + "\r\n" + txt;
        //    Save(page2, newtxt, "Marking page with " + template + " using [[WP:NPW|NPWatcher]]");
        //}

        #endregion

        #region Admin only prod stuff (and speedy tag removal)

        private void rmvBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            Removetags();
            Greyin();
        }

        private void Removetags()
        {
            string txt = wf.GetWikiText(page2);
            Regex rx = new Regex("{{db[^}]*}}", RegexOptions.IgnoreCase);
            string txt1 = rx.Replace(txt, "");
            Regex hangonrx = new Regex("{{hang-?on}}", RegexOptions.IgnoreCase);
            txt1 = hangonrx.Replace(txt1, "");
            Save(page2, txt1, "Removing deletion tag(s) using [[WP:NPW|NPWatcher]]");
        }

        //Auto prod delete
        //Tries to get prod reason and deletes using that reason as the rationale
        private void button1_Click(object sender, EventArgs e)
        {
            Greyout();
            string wikitext = wf.GetWikiText(page2);
            if (wikitext.Contains("{{dated prod|concern = "))
            {
                try
                {
                    int startindex = wikitext.IndexOf("{{dated prod|concern = ");
                    int endindex;
                    if (wikitext.Contains("<!-- Do not use the \"dated prod\" tem")) { endindex = wikitext.IndexOf("<!-- Do not use the \"dated prod\" tem"); }
                    else
                    {
                        endindex = wikitext.Contains("{{prod2a|") ? wikitext.IndexOf("{{prod2a|") : wikitext.IndexOf("{{prod2|");
                    }
                    string prod = wikitext.Substring(startindex, endindex - startindex);
                    startindex = prod.IndexOf("{{{concern");
                    startindex += 11;
                    endindex = prod.IndexOf("}}}|month");
                    string reason = prod.Substring(startindex, endindex - startindex);
                    reason = reason + " ([[WP:PROD|outdated prod]])";
                    Delete(page2, reason);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Prod reason not found - please choose another option");
                }
            }
            else
            {
                MessageBox.Show("Prod reason not found - please choose another option");
            }
            Greyin();
        }

        private void RmvProd_Click(object sender, EventArgs e)
        {
            Greyout();
            string wikitext = wf.GetWikiText(page2);
            if (wikitext.Contains("{{dated prod|concern = "))
            {
                try
                {
                    int startindex = wikitext.IndexOf("{{dated prod|concern = ");
                    int endindex = wikitext.IndexOf("emplate directly; the above line is generated by \"subst:prod|reason\" -->" + 72);
                    string prod = wikitext.Substring(startindex, endindex - startindex);
                    // wikitext.Remove(startindex, endindex - startindex);
                    wikitext = wikitext.Replace(prod, "");

                    Prod prod1 = new Prod();
                    prod1.ShowDialog();
                    if (doprod)
                    {
                        string reason = prodreasonstr;
                        prodreasonstr = "";
                        if (string.IsNullOrEmpty(reason))
                        {
                            reason = "No reason given";
                        }
                        Save(page2, wikitext, "Removing prod tag using [[WP:NPW|NPWatcher]].  Reason given was \"" + reason + "\"");
                        doprod = false;
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Prod reason not found - please choose another option");
                }
            }
            else
            {
                MessageBox.Show("Prod reason not found - please choose another option");
            }
            Greyin();
        }

        private void AfDCustom_Click(object sender, EventArgs e)
        {
            Greyout();

            AFDForm afd = new AFDForm();
            afd.ShowDialog();
            string wikitextnew = wf.GetWikiText(page2);
            if (wikitextnew != wikitextpage2)
            {
                if (string.IsNullOrEmpty(wikitextnew))
                {
                    MessageBox.Show("The page appears to have been deleted by an administrator (or blanked).  The page will now reload so that you can verify this");
                    editsuccess = false;
                }
                else
                {
                    MessageBox.Show("The page have been changed since you loaded it.  It will now reload so that you can (re)consider your proposed changes");
                    editsuccess = false;
                }
            }
            else
            {
                if (afdsuc)
                {
                    Removetags();
                    string txt = wf.GetWikiText("Wikipedia:Articles for deletion/" + page2);
                    int number = 1;
                    while (!string.IsNullOrEmpty(txt))
                    {
                        number += 1;
                        switch (number)
                        {
                            case 2:
                                txt = wf.GetWikiText("Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)");
                                break;
                            case 3:
                                txt = wf.GetWikiText("Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)");
                                break;
                            default:
                                txt = wf.GetWikiText("Wikipedia:Articles for deletion/" + page2 + " (" + number + "th nomination)");
                                break;
                        }
                    }
                    string afdnom = "{{subst:afd2|pg=" + page2 + "|cat=" + afdCat + "|text=" + afdReason + "}} ~~~~";
                    string afdnompg = "";
                    if (number == 1)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2;
                        if (SortLogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2, afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }
                    if (number == 2)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)";
                        if (SortLogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)", afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }
                    else if (number == 3)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)";
                        if (SortLogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)", afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }
                    else if (number != 1 && number != 2 && number != 3)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (" + number + "th nomination)";
                        if (SortLogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2 + " (" + number + "th nomination)", afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }

                    string pgtxt = wf.GetWikiText(page2);
                    string numbertxt;
                    if (number == 2) { numbertxt = "2nd"; }
                    else if (number == 3) { numbertxt = "3nd"; }
                    else { numbertxt = number + "nd"; }

                    if (number == 1)
                    {
                        pgtxt = "{{subst:afd1}}\r\n" + pgtxt;
                    }
                    else
                    {
                        pgtxt = "{{subst:afdx|" + numbertxt + "\r\n" + pgtxt;
                    }
                    if (SortLogin())
                        wf.Save(page2, pgtxt, "Nominating page for deletion using [[WP:NPW|NPWatcher]]", true);

                    DateTime now = DateTime.UtcNow;

                    DateTimeFormatInfo dfi = new DateTimeFormatInfo();
                    dfi.FullDateTimePattern = "yyyy MMMM dd";
                    string datetoday = now.ToString("F", dfi);
                    string date2 = datetoday.Substring(datetoday.Length - 1);
                    datetoday = datetoday.Remove(datetoday.Length - 1);
                    if (datetoday.EndsWith("0"))
                    {
                        datetoday = datetoday.Remove(datetoday.Length - 1);
                    }
                    datetoday = datetoday + date2;

                    string logpg = wf.GetWikiText("Wikipedia:Articles for deletion/Log/" + datetoday);
                    logpg = logpg + "\r\n{{" + afdnompg + "}}";
                    if (SortLogin())
                        wf.Save("Wikipedia:Articles for deletion/Log/" + datetoday, logpg, "Adding [[" + page2 + "]] to list using [[WP:NPW|NPWatcher]]");
                    Greyin();

                    afdsuc = false;
                }
            }
            webBrowser1.Refresh();
        }

        private void DelCustom_Click(object sender, EventArgs e)
        {
            Greyout();
            Prod prod1 = new Prod();
            prod1.prod();
            prod1.ShowDialog();

            if (doprod)
            {
                string reason = prodreasonstr;
                prodreasonstr = "";
                if (string.IsNullOrEmpty(reason))
                {
                    reason = "No reason given";
                }
                if (SortLogin())
                    wf.Deletepg(page2, "Old [[WP:PROD]] - reason given was \"" + reason + "\".  Using [[WP:NPW|NPWatcher]]");

                Greyin();
                doprod = false;
            }
        }

        #endregion

        #region images

        private void I1Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (page2.Contains("Image:"))
            {
                const string rat = "Per [[WP:CSD#I1|CSD I1]] - image is redundant";
                if (orphanCB.Checked) { OrphanImage(page2, rat); }
                Delete(page2, rat);
                webBrowser1.Refresh();
            }
            else
            {
                MessageBox.Show("Please select an image from the list on the left");
            }
            Greyin();
        }

        private void I2Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (page2.Contains("Image:"))
            {
                const string rat = "Per [[WP:CSD#I2|CSD I2]] - image is corrupt/empty";
                if (orphanCB.Checked) { OrphanImage(page2, rat); }
                Delete(page2, rat);
                webBrowser1.Refresh();
            }
            else
            {
                MessageBox.Show("Please select an image from the list on the left");
            }
            Greyin();
        }

        private void I3Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (page2.Contains("Image:"))
            {
                const string rat = "Per [[WP:CSD#I3|CSD I3]] - image has an invalid license";
                if (orphanCB.Checked) { OrphanImage(page2, rat); }
                I3Warn(page2);
                Delete(page2, rat);
                webBrowser1.Refresh();
            }
            else
            {
                MessageBox.Show("Please select an image from the list on the left");
            }
            Greyin();
        }

        private void I3Warn(string page)
        {
            string towarn = wf.GetCreator(page);
            string userpg = wf.GetWikiText("User_talk:" + towarn);
            userpg = userpg + "\r\n{{subst:Idw-noncom-deleted|" + HttpUtility.UrlDecode(page) + "}}";
            if (SortLogin())
                wf.Save("User_talk:" + towarn, userpg, "Warning user with {{Idw-noncom-deleted}} using [[WP:NPW|NPWatcher]]");
        }

        private void I4Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (page2.Contains("Image:"))
            {
                const string rat = "Per [[WP:CSD#I4|CSD I4]] - image has no license";
                if (orphanCB.Checked) { OrphanImage(page2, rat); }
                Delete(page2, rat);
                webBrowser1.Refresh();
            }
            else
            {
                MessageBox.Show("Please select an image from the list on the left");
            }
            Greyin();
        }

        private void I5Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (page2.Contains("Image:"))
            {
                const string rat = "Per [[WP:CSD#I5|CSD I5]] - image is unfree and is unused";
                if (orphanCB.Checked) { OrphanImage(page2, rat); }
                Delete(page2, rat);
                webBrowser1.Refresh();
            }
            else
            {
                MessageBox.Show("Please select an image from the list on the left");
            }
            Greyin();
        }

        private void I6Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (page2.Contains("Image:"))
            {
                const string rat = "Per [[WP:CSD#I6|CSD I6]] - image has no fair use rationale";
                if (orphanCB.Checked) { OrphanImage(page2, rat); }
                Delete(page2, rat);
                webBrowser1.Refresh();
            }
            else
            {
                MessageBox.Show("Please select an image from the list on the left");
            }
            Greyin();
        }

        private void I7Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (page2.Contains("Image:"))
            {
                const string rat = "Per [[WP:CSD#I7|CSD I7]] - image has an invalid fair use claim";
                if (orphanCB.Checked) { OrphanImage(page2, rat); }
                Delete(page2, rat);
                webBrowser1.Refresh();
            }
            else
            {
                MessageBox.Show("Please select an image from the list on the left");
            }
            Greyin();
        }

        private void I8Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (page2.Contains("Image:"))
            {
                const string rat = "Per [[WP:CSD#I8|CSD I8]] - image exists on commons";

                Delete(page2, rat);
                webBrowser1.Refresh();
            }
            else
            {
                MessageBox.Show("Please select an image from the list on the left");
            }
            Greyin();
        }

        private void IotherBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (page2.Contains("Image:"))
            {
                Prod reason = new Prod();
                reason.ShowDialog();
                if (doprod)
                {
                    string rat = prodreasonstr;
                    prodreasonstr = "";
                    if (string.IsNullOrEmpty(rat))
                    {
                        rat = "No reason given";
                    }

                    if (orphanCB.Checked) { OrphanImage(page2, rat); }
                    Delete(page2, rat);
                    webBrowser1.Refresh();
                    doprod = false;
                }
            }
            else
            {
                MessageBox.Show("Please select an image from the list on the left");
            }
            Greyin();
        }

        public void OrphanImage(string image, string reason)
        {
            StringCollection imagelinks = wf.GetImgLinks(page2);

            foreach (string p in imagelinks)
            {
                string wikitextp = wf.GetWikiText(p);
                string wtq = RemoveImage(image, wikitextp);
                if (wtq.StartsWith("\n"))
                {
                    wtq = wtq.Substring(1, wtq.Length - 1);
                }
                if (SortLogin())
                    wf.Save(p, wtq, "Removing image using [[WP:NPW|NPWatcher]].  Reason given was: \"" + reason + "\".");
            }

        }

        public string RemoveImage(string image, string articleText)
        {
            //remove image prefix
            image = Regex.Replace(image, "^image:", "", RegexOptions.IgnoreCase).Replace("_", " ");
            image = Regex.Escape(image).Replace("\\ ", "[ _]");

            Regex r = new Regex("\\[\\[[Ii]mage:" + image + ".*\\]\\]", RegexOptions.IgnoreCase);
            MatchCollection n = r.Matches(articleText);

            if (n.Count > 0)
            {
                foreach (Match m in n)
                {
                    string match = m.Value;

                    int i = 0;
                    int j = 0;

                    foreach (char c in match)
                    {
                        if (c == '[')
                            j++;
                        else if (c == ']')
                            j--;

                        i++;

                        if (j == 0)
                        {
                            if (match.Length > i)
                                match = match.Remove(i);

                            Regex t = new Regex(Regex.Escape(match));

                            articleText = t.Replace(articleText, "", 1);

                            break;
                        }
                    }
                }
            }
            else
            {
                r = new Regex("([Ii]mage:)?" + image);
                n = r.Matches(articleText);

                foreach (Match m in n)
                {
                    Regex t = new Regex(Regex.Escape(m.Value));
                    articleText = t.Replace(articleText, "", 1, m.Index);
                }
            }

            return articleText;
        }
        #endregion

        private void Greyout()
        {
            Grey(false);
        }

        private void Greyin()
        {
            Grey(true);
            checkAdvert.Checked = checkCleanup.Checked = checkContext.Checked = checkCopyedit.Checked =
            checkCopypase.Checked = checkDeadend.Checked = checkHowto.Checked = checkInline.Checked =
            checkIntrorewrite.Checked = checkInUniverse.Checked = checkNotability.Checked = checknotEnglish.Checked =
            checkNpov.Checked = checkOrphan.Checked = checkRefImprove.Checked = checkSections.Checked =
            checkStub.Checked = checkTone.Checked = checkuncat.Checked = checkUnsourced.Checked =
            checkWikify.Checked = firstarticle.Checked = notabilitywarn.Checked = false;
            checkPatrolled.Checked = true;
        }

        private void Grey(bool enabled)
        {
            dbattackBtn.Enabled = dbbioBtn.Enabled = dbBlankBtn.Enabled =
            dbBtn.Enabled = dbemptyBtn.Enabled = dbforeignBtn.Enabled =
            dbNonsBtn.Enabled = dbR1Btn.Enabled = dbR2Btn.Enabled =
            dbR3Btn.Enabled = dbRepostBtn.Enabled = dbspamBtn.Enabled =
            dbtalkBtn.Enabled = dbTest.Enabled = dvCvBtn.Enabled = dbvandBtn.Enabled =
            db_userreq.Enabled = prodBtn.Enabled = rmvBtn.Enabled =
             AfDBtn.Enabled = delGivReasonBtn.Enabled =
            DelCustom.Enabled = AfDCustom.Enabled = RmvProd.Enabled = I1Btn.Enabled = I2Btn.Enabled =
            I3Btn.Enabled = I4Btn.Enabled = I5Btn.Enabled = I6Btn.Enabled = I7Btn.Enabled =
            I8Btn.Enabled = IotherBtn.Enabled =
                //maintenance
            checkAdvert.Enabled = checkCleanup.Enabled = checkContext.Enabled = checkCopyedit.Enabled =
            checkCopypase.Enabled = checkDeadend.Enabled = checkHowto.Enabled = checkInline.Enabled =
            checkIntrorewrite.Enabled = checkInUniverse.Enabled = checkNotability.Enabled = checknotEnglish.Enabled =
            checkNpov.Enabled = checkOrphan.Enabled = checkRefImprove.Enabled = checkSections.Enabled =
            checkStub.Enabled = checkTone.Enabled = checkuncat.Enabled = checkUnsourced.Enabled =
            checkWikify.Enabled =
            checkPatrolled.Enabled = enabled;
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = (int)((e.CurrentProgress / e.MaximumProgress) * 100);
        }

        private bool SortLogin()
        {
            if (wf.GetLogInStatus())
                return true;

            wf.Login(username, password);

            if (!wf.GetLogInStatus())
            {
                MessageBox.Show("Login error - unable to log in.  Please report this error!");
                return false;
            }
            return true;
        }

        #region Context Menu
        private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageList.Items.Remove(pageList.SelectedItem);
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageList.Items.Clear();
        }
        #endregion
        #region Article Issues
        private void doMarkButton_Click(object sender, EventArgs e)
        {
            Greyout();
            ArrayList articleIssues = new ArrayList();
            ArrayList templateIssues = new ArrayList();
            //there is probably a more elegant way to handle this all those ifs.
            if (checkAdvert.Checked)
            {
                articleIssues.Add(new Issue("advert"));
            }
            if (checkCleanup.Checked)
            {
                articleIssues.Add(new Issue("cleanup"));
            }
            if (checkContext.Checked)
            {
                articleIssues.Add(new Issue("context"));
            }
            if (checkCopyedit.Checked)
            {
                articleIssues.Add(new Issue("copyedit"));
            }
            if (checkCopypase.Checked)
            {
                templateIssues.Add(new Issue("copypaste"));
            }
            if (checkDeadend.Checked)
            {
                articleIssues.Add(new Issue("deadend"));
            }
            if (checkHowto.Checked)
            {
                articleIssues.Add(new Issue("howto"));
            }
            if (checkInline.Checked)
            {
                templateIssues.Add(new Issue("inline"));
            }
            if (checkIntrorewrite.Checked)
            {
                templateIssues.Add(new Issue("introrewrite"));
            }
            if (checkInUniverse.Checked)
            {
                templateIssues.Add(new Issue("in-universe"));
            }
            if (checkNotability.Checked)
            {
                articleIssues.Add(new Issue("notability"));
            }
            if (checknotEnglish.Checked)
            {
                frmLangChoose lc = new frmLangChoose();
                if (lc.ShowDialog(this) == DialogResult.OK)
                {
                    templateIssues.Add(new Issue("notenglish|" + lc.lang));
                }
            }
            if (checkNpov.Checked)
            {
                articleIssues.Add(new Issue("pov"));
            }
            if (checknotEnglish.Checked)
            {
                articleIssues.Add(new Issue("notable"));
            }
            if (checkOrphan.Checked)
            {
                articleIssues.Add(new Issue("orphan"));
            }
            if (checkRefImprove.Checked)
            {
                articleIssues.Add(new Issue("refimprove"));
            }
            if (checkSections.Checked)
            {
                articleIssues.Add(new Issue("sections"));
            }
            if (checkStub.Checked)
            {
                //A stub combined with other issues requires an additional pageload. This should be fixed in the
                //future, creating a whole new page content string.
                Greyout();
                if (string.IsNullOrEmpty(stubCombo.Text))
                {
                    DialogResult dr = MessageBox.Show(this, "You didn't enter a stub type.  Would you like to use the generic tag?",
                        "Stub types", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.No)
                    {
                        MessageBox.Show("Please enter a stub-type in the test box, and then click the stub button again", "Custom stubs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        const string template = "{{stub}}";
                        string txt = wf.GetWikiText(page2);
                        string newtxt = txt + "\r\n" + template;
                        Save(page2, newtxt, "Marking page with " + template + " using [[WP:NPW|NPWatcher]]");
                    }
                }
                else
                {
                    string template = "{{" + stubCombo.Text + "-stub}}";
                    string txt = wf.GetWikiText(page2);
                    string newtxt = txt + "\r\n" + template;
                    Save(page2, newtxt, "Marking page with " + template + " using [[WP:NPW|NPWatcher]]");
                }
                Greyin();
            }
            if (checkTone.Checked)
            {
                articleIssues.Add(new Issue("tone"));
            }
            if (checkuncat.Checked)
            {
                templateIssues.Add(new Issue("uncategorised"));
            }
            if (checkUnsourced.Checked)
            {
                articleIssues.Add(new Issue("unreferenced"));
            }
            if (checkWikify.Checked)
            {
                articleIssues.Add(new Issue("wikify"));
            }
            if (checkPatrolled.Checked)
            {
                MarkPatrolled();
            }
            MarkBoxes(articleIssues, templateIssues);
            Greyin();
        }

        private void MarkBoxes(ArrayList articleIssues, ArrayList templateIssues)
        {
            if (articleIssues.Count == 0 && templateIssues.Count == 0)
            {
                return;
            }
            string templates = "";
            string issues = "";
            if (articleIssues.Count > 2)
            {
                issues += "{{Articleissues\r\n";
                foreach (Issue issue in articleIssues)
                {
                    issues += issue.getIssueLine();
                }
                issues += "}}";
            }
            else
            {
                templateIssues.AddRange(articleIssues);
                articleIssues.Clear();
            }

            foreach (Issue issue in templateIssues)
            {
                templates += issue.getTemplate();
                templates += "\r\n";
            }

            string txt = wf.GetWikiText(page2);
            string newtxt = templates + issues + "\r\n" + txt;
            if (articleIssues.Count + templateIssues.Count > 3)
            {
                Save(page2, newtxt, "Marking page for more than 3 issues using [[WP:NPW|NPWatcher]]");
            }
            else
            {
                string issuestring = "";
                foreach (Issue issue in articleIssues)
                {
                    issuestring += issue.getName() + " ";
                }
                foreach (Issue issue in templateIssues)
                {
                    issuestring += issue.getName() + " ";
                }
                Save(page2, newtxt, "Marking page for the following issues: " + issuestring + "using [[WP:NPW|NPWatcher]]");
            }
        }

        #endregion

        #region Settings

        private void ResetSettings()
        {
            settings = new Settings();
            LoadSettings("", false);
        }

        private void LoadSettings(string file)
        {
            LoadSettings(file, true);
        }

        private void LoadSettings(string file, bool loadFromFile)
        {
            try
            {
                if (loadFromFile)
                {
                    settings = Settings.LoadPrefs(file);
                    currentSettingsFile = file;
                }
            }
            catch
            {
                MessageBox.Show("There is a problem with your settings file. Loading default settings");
                currentSettingsFile = "";
                settings = new Settings();
            }
            finally
            {
                stubCombo.Items.Clear();
                foreach (string s in settings.stubTypes)
                {
                    stubCombo.Items.Add(s);
                }
                hidePatrolledEditsToolStripMenuItem.Checked = settings.hidePatrolled;
                hideAdminEditsToolStripMenuItem.Checked = settings.hideAdmins;
                hideBotEditsToolStripMenuItem.Checked = settings.hideBots;
                toolStripRefreshTxt.Text = settings.refreshinterval.ToString();
                limitCB.Text = settings.pagelimit;
            }
        }

        private void SaveSettings(string file)
        {
            try
            {
                if (settings.stubTypes != null)
                    settings.stubTypes.Clear();
                foreach (string s in stubCombo.Items)
                {
                    settings.stubTypes.Add(s);
                }

                Settings.SavePrefs(settings, file);
            }
            catch
            {
                MessageBox.Show("There is a problem with your settings file. Settings not saved");
            }
        }
        #endregion

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Settings file|*.xml";
            save.InitialDirectory = Application.StartupPath;
            save.FileName = currentSettingsFile;

            save.ShowDialog();

            if (!string.IsNullOrEmpty(save.FileName))
                SaveSettings(save.FileName);
        }

        private void loadSettToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Settings file|*.xml";
            open.InitialDirectory = Application.StartupPath;

            open.ShowDialog();

            if (!string.IsNullOrEmpty(open.FileName))
                LoadSettings(open.FileName);
        }

        private void resetSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetSettings();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPatrol_Click(object sender, EventArgs e)
        {
            MarkPatrolled();
        }

        private void MarkPatrolled()
        {
            if (!string.IsNullOrEmpty(page2))
            {
                string rcid = wf.Getrcid(page2);
                if (rcid == "not found")
                    MessageBox.Show("Was unable to mark page as patrolled.  Please try on another page.");
                else
                {
                    webBrowser1.Navigate("http://en.wikipedia.org/w/index.php?title=" + page2 + "&action=markpatrolled&rcid=" + rcid);
                }
            }
        }

        private void hideBotEditsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            settings.hideBots = hideBotEditsToolStripMenuItem.Checked;
        }

        private void hideAdminEditsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            settings.hideAdmins = hideAdminEditsToolStripMenuItem.Checked;
        }

        private void hidePatrolledEditsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            settings.hidePatrolled = hidePatrolledEditsToolStripMenuItem.Checked;
        }

        private void SetTimer()
        {
            if (refreshInterval != 0)
            {
                timerRefresh.Interval = 1000 * refreshInterval;
                timerRefresh.Start();
            }
            else
                timerRefresh.Stop();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            PopulateList(false);
            Cursor = Cursors.Default;
        }

        private void toolStripRefreshTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                try
                {
                    refreshInterval = int.Parse(toolStripRefreshTxt.Text);
                    settings.refreshinterval = refreshInterval;
                }
                catch (FormatException)
                {
                    refreshInterval = 0;
                    MessageBox.Show("Please only enter an integer as the refresh interval.", "Refresh interval", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                SetTimer();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WikiFunctions.LoadLink("User:Martinp23/NPWatcher/Manual");
        }
    }
}
