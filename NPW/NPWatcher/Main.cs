using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace NPWatcher
{
    public partial class Main : Form
    {
        internal static string username = "";
        internal static string password = "";
        internal static bool dialogcancel;
        internal static bool success;
        internal WikiFunctions wf = new WikiFunctions();
        internal string page2 = "";
        internal static string prodreasonstr = "";
        internal static bool doprod;
        internal static bool asAdmin;
        internal static string dbReason;
        internal static string afdCat;
        internal static string afdReason;
        internal static string nntag = "";
        internal static bool crsuc;
        internal static bool afdsuc;
        internal static bool nnchoicessuc;
        internal static string wikitextpage2 = "";
        internal static bool editsuccess;
        internal static string cwr;

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            string versionpg = wf.getWikiText("User:Martinp23/NPWatcher/Checkpage/Versions");
            string listofv = versionpg;
            string versioncurstr = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Cursor = Cursors.Default;
            if (!listofv.Contains(versioncurstr))
            {
                MessageBox.Show("Please download the latest version of NPWatcher", "New Version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            //LOGIN AND QUIT ON CANCEL CODE
            LogOn login = new LogOn();
            login.ShowDialog();

            if (dialogcancel)
            {
                Close();
                Application.Exit();
            }
            else
            {
                while (!success)
                {
                    if (!dialogcancel)
                    {
                        success = wf.login(username, password);
                        if (!success)
                        {
                            LogOn login1 = new LogOn();
                            login1.ShowDialog();
                            if (dialogcancel)
                                Close();
                        }
                        else
                        {
                            string approved = wf.getWikiText("User:Martinp23/NPWatcher/Checkpage/Users");

                            username = Regex.Escape(username);
                            Regex r = new Regex(username, RegexOptions.IgnoreCase);
                            Match m = r.Match(approved);

                            if (!m.Success)
                            {
                                MessageBox.Show("You are not approved to use NPWatcher.  Please request approval from Martinp23", "Not Approved", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                Close();
                            }
                            else
                                asAdmin = wf.CheckIfAdmin();
                            MessageBox.Show("Logged in");
                        }
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

        private void getlistBtn_Click(object sender, EventArgs e)
        {
            if (asAdmin)
            {
                ListSource listsource = new ListSource();
                string category = "";

                listsource.ShowDialog();
                category = listsource.category;

                StringCollection nps = new StringCollection();
                if (category == "NPRad")
                {
                    string limit = "20";
                    try
                    {
                        limit = limitCB.SelectedItem.ToString();
                    }
                    catch (NullReferenceException)
                    {
                        limit = "20";
                        limitCB.SelectedItem = "20";
                    }

                    nps = wf.getNPs(limit);
                    pageList.Items.Clear();
                    foreach (string p in nps)
                    { pageList.Items.Add(p); }
                }
                else if (category == "CSDRad")
                {
                    string limit = "500";
                    nps = wf.getCat(limit, "Candidates for speedy deletion");
                    pageList.Items.Clear();
                    foreach (string p in nps)
                    { pageList.Items.Add(p); }
                }
                else
                {
                    if (category != null)
                    {
                        string limit = "20";
                        try
                        {
                            limit = limitCB.SelectedItem.ToString();
                        }
                        catch (NullReferenceException)
                        {
                            limit = "20";
                            limitCB.SelectedItem = "20";
                        }
                        nps = wf.getCat(limit, category);
                        pageList.Items.Clear();
                        foreach (string p in nps)
                        { pageList.Items.Add(p); }
                    }
                    else
                    {
                        MessageBox.Show("Please choose a list source");
                    }
                }
            }
            else
            {
                StringCollection nps = new StringCollection();

                string limit = "20";
                try
                {
                    limit = limitCB.SelectedItem.ToString();
                }
                catch (NullReferenceException)
                {
                    limit = "20";
                    limitCB.SelectedItem = "20";
                }

                nps = wf.getNPs(limit);
                pageList.Items.Clear();
                foreach (string p in nps)
                { pageList.Items.Add(p); }
            }
        }

        private void pageList_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string page = pageList.SelectedItem.ToString();
                //should fix the ampersands issue...
                page2 = System.Web.HttpUtility.UrlEncode(page);
                webBrowser1.Navigate(wf.Url + page2);
                wikitextpage2 = wf.getWikiText(page2);
            }
            catch (NullReferenceException)
            { }
        }

        #region speedy
        private void dbBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
            {
                CustomReason cr = new CustomReason();
                cr.ShowDialog();
                if (crsuc)
                {
                    if (dbReason == null)
                    { Delete("db", "No reason given"); }
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
            if (pageList.SelectedItem != null)
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
            if (pageList.SelectedItem != null)
                Delete("db-spam", "Blatant advertising, [[WP:CSD#G11]]");
            Greyin();
        }

        private void db_userreq_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
                Delete("db-spam", "Author request, [[WP:CSD#G7]]");
            Greyin();
        }

        private void dbforeignBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
                Delete("db-foreign", "Article is not in English");
            Greyin();
        }

        private void dbRepostBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
                Delete("db-repost", "Repost of previously deleted material");
            Greyin();
        }

        private void dbNonsBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
            {
                Msgnons();
                Delete("db-nonsense", "Nonsense page");
            }
            Greyin();
        }

        private void dbTest_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
            {
                Msgtest();
                Delete("db-test", "Vandalism/Test");
            }
            Greyin();
        }

        private void dbvandBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
            {
                Msgvand();
                Delete("db-vandalism", "Vandalism");
            }
            Greyin();
        }

        private void dbBlankBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
                Delete("db-blank", "Page blanked by only editor");
            Greyin();
        }

        private void dbtalkBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
                Delete("db-talk", "Talk page of non-existant article");

            Greyin();
        }

        private void dbattackBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
            {
                Msgattack();
                Delete("db-attack", "Attack page");
            }
            Greyin();
        }

        private void dbemptyBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
                Delete("db-empty", "Empty page");

            Greyin();
        }

        private void dbR1Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
                Delete("db-redirnone", "Redirect to non-existant page, [[WP:CSD#R1]]");

            Greyin();
        }

        private void dbR2Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
                Delete("db-rediruser", "Redirect to user space [[WP:CSD#R2]]");

            Greyin();
        }

        private void dbR3Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (pageList.SelectedItem != null)
                Delete("db-redirtypo", "Implausibe typo, [[WP:CSD#R3]]");

            Greyin();
        }

        private void dbCvBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (!asAdmin)
            {
                if (pageList.SelectedItem != null)
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
                string wikitext = wf.getWikiText(page2);
                string newtxt = "";
                newtxt = "{{" + p + "}}\r\n" + wikitext;
                Save(page2, newtxt, "Marking page for deletion using [[WP:NPW|NPWatcher]]");
                if (editsuccess)
                {
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
                if (sortlogin())
                    wf.Deletepg(page2, "Deleting page - reason was: \"" + r + "\" using [[WP:NPW|NPWatcher]]");

                if (!page2.StartsWith("Image:"))
                {
                    string talktxt = wf.getWikiText("Talk:" + page2);
                    if (string.IsNullOrEmpty(talktxt))
                    {
                        DialogResult dr = MessageBox.Show("Would you like to delete the article talk page too?", "Talk page",
                            MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            if (sortlogin())
                                wf.Deletepg("Talk:" + page2, "Deleting page as talk of deleted article using [[WP:NPW|NPWatcher]]");
                        }
                    }
                }
                else
                {
                    string talktitle = "Image talk:" + page2.Substring(6);
                    string talktxt = wf.getWikiText("Talk:" + page2);
                    if (!string.IsNullOrEmpty(talktxt))
                    {
                        DialogResult dr = MessageBox.Show("Would you like to delete the image talk page too?", "Talk page",
                            MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            if (sortlogin())
                                wf.Deletepg(talktitle, "Deleting page as talk of deleted image using [[WP:NPW|NPWatcher]]");
                        }
                    }
                }
            }
        }

        private void Save(string page3, string newtxt, string summary)
        {
            string wikitextcur = wf.getWikiText(page2);
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
                if (sortlogin())
                    if (sortlogin())
                        wf.Save(page3, newtxt, summary);
                editsuccess = true;
            }
            wikitextpage2 = wf.getWikiText(page2);

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
                string wikitext = wf.getWikiText(creator);
                string newtxt = "";
                if (!asAdmin)
                {
                    newtxt = wikitext + "\r\n\r\n{{subst:firstarticle|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (sortlogin())
                        wf.Save(creator, newtxt, "Posting {{firstarticle}} using [[WP:NPW|NPWatcher]]");
                }
                else
                {
                    if (!wikitext.Contains("<!-- Template:Firstarticle -->"))
                    {
                        newtxt = wikitext + "\r\n\r\n{{subst:firstarticle|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                        if (sortlogin())
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
                string wikitext = wf.getWikiText(creator);
                string newtxt = "";

                if (!asAdmin)
                {
                    newtxt = wikitext + "\r\n\r\n{{subst:nn-warn|" + System.Web.HttpUtility.UrlDecode(page2) + "|header=1}} ~~~~";
                    if (sortlogin())
                        wf.Save(creator, newtxt, "Posting {{nn-warn}} using [[WP:NPW|NPWatcher]].");
                }
                if (asAdmin)
                {
                    newtxt = wikitext + "\r\n\r\n{{subst:nn-warn-deletion|" + System.Web.HttpUtility.UrlDecode(page2) + "|header=1}} ~~~~";
                    if (sortlogin())
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
                    string wikitext = wf.getWikiText(creator);
                    string newtxt = "";
                    string warning = cwr;

                    if (!asAdmin)
                    {
                        newtxt = wikitext + "\r\n\r\n{{subst:" + warning + "|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                        if (sortlogin())
                            wf.Save(creator, newtxt, "Posting {{" + warning + "}} using [[WP:NPW|NPWatcher]].");
                    }
                    if (asAdmin)
                    {
                        if (!wikitext.Contains("<!-- Template:" + warning + " -->"))
                        {
                            newtxt = wikitext + "\r\n\r\n{{subst:" + warning + "|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                            if (sortlogin())
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
                string wikitext = wf.getWikiText(creator);
                string newtxt = "";
                if (!asAdmin)
                {
                    newtxt = wikitext + "\r\n{{subst:Nonsensepages|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (sortlogin())
                        wf.Save(creator, newtxt, "Posting {{Nonsensepages}} using [[WP:NPW|NPWatcher]].");
                }
                else
                {
                    if (!wikitext.Contains("<!-- Template:Nonsensepages -->"))
                    {
                        newtxt = wikitext + "\r\n{{subst:Nonsensepages|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                        if (sortlogin())
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
            string wikitext = wf.getWikiText(creator);
            string newtxt = "";
            if (!asAdmin)
            {
                newtxt = wikitext + "\r\n{{subst:test1article|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                if (sortlogin())
                    wf.Save(creator, newtxt, "Posting {{test1article}} using [[WP:NPW|NPWatcher]].");
            }
            else
            {
                if (!wikitext.Contains("<!-- Template:Test1article (first level warning) -->"))
                {
                    newtxt = wikitext + "\r\n{{subst:test1article|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (sortlogin())
                        wf.Save(creator, newtxt, "Posting {{test1article}} using [[WP:NPW|NPWatcher]].");
                }
            }
        }

        private void Msgvand()
        {
            Greyout();
            string creator = wf.GetCreator(page2);
            creator = "User_talk:" + creator;
            string wikitext = wf.getWikiText(creator);
            string newtxt = "";
            if (!asAdmin)
            {
                newtxt = wikitext + "\r\n{{subst:test2article-n|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                if (sortlogin())
                    wf.Save(creator, newtxt, "Posting {{test2article}} using [[WP:NPW|NPWatcher]].");
            }
            else
            {

                if (!wikitext.Contains("<!-- Template:Test2article (second level warning) -->"))
                {
                    newtxt = wikitext + "\r\n{{subst:test2article-n|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (sortlogin())
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
                string wikitext = wf.getWikiText(creator);
                string newtxt = "";
                if (!asAdmin)
                {
                    newtxt = wikitext + "\r\n{{subst:Attack|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (sortlogin())
                        wf.Save(creator, newtxt, "Posting {{Attack|" + System.Web.HttpUtility.UrlDecode(page2) + "}} using [[WP:NPW|NPWatcher]].");
                }
                else
                {
                    if (!wikitext.Contains("<!-- Template:Attack -->"))
                    {
                        newtxt = wikitext + "\r\n{{subst:Attack|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                        if (sortlogin())
                            wf.Save(creator, newtxt, "Posting {{Attack|" + System.Web.HttpUtility.UrlDecode(page2) + "}} using [[WP:NPW|NPWatcher]].");
                    }
                }
            }
        }

        private void prodBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            removetags(page2);
            Prod prodreason = new Prod();
            prodreason.ShowDialog();

            if (doprod)
            {
                string message = prodreasonstr;

                string oldtxt = wf.getWikiText(page2);
                string newtxt = oldtxt;
                string prod = "{{subst:prod|" + message + "}}\r\n";
                newtxt = prod + newtxt;

                Save(page2, newtxt, "[[WP:PROD|PRODDING]] article with [[WP:NPW|NPWatcher]]");

                if (editsuccess)
                {
                    string user = wf.GetCreator(page2);
                    user = "User_talk:" + user;
                    string userpage = wf.getWikiText(user);

                    string userpagenew = userpage + "\r\n{{subst:PRODWarning|" + System.Web.HttpUtility.UrlDecode(page2) + "}} ~~~~";
                    if (sortlogin())
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
            string wikitextnew = wf.getWikiText(page2);
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
                    {
                        removetags(page2);
                        string txt = wf.getWikiText("Wikipedia:Articles for deletion/" + page2);
                        int number = 1;
                        while (!string.IsNullOrEmpty(txt))
                        {
                            number += 1;
                            if (number == 2)
                            { txt = wf.getWikiText("Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)"); }
                            else if (number == 3)
                            { txt = wf.getWikiText("Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)"); }
                            else
                            { txt = wf.getWikiText("Wikipedia:Articles for deletion/" + page2 + " (" + number.ToString() + "th nomination)"); }
                        }

                        string pgtxt = wf.getWikiText(page2);
                        string numbertxt;
                        if (number == 2)
                        { numbertxt = "2nd"; }
                        else if (number == 3)
                        { numbertxt = "3nd"; }
                        else
                        { numbertxt = number.ToString() + "nd"; }

                        if (number == 1)
                        {
                            pgtxt = "{{subst:afd1}}\r\n" + pgtxt;
                        }
                        else
                        {
                            pgtxt = "{{subst:afdx|" + numbertxt + "\r\n" + pgtxt;
                        }
                        Save(page2, pgtxt, "Nominating page for deletion using [[WP:NPW|NPWatcher]]");


                        string afdnom = "";

                        afdnom = "{{subst:afd2|pg=" + page2 + "|cat=" + afdCat + "|text=" + afdReason + "}} ~~~~";
                        string afdnompg = "";
                        if (number == 1)
                        {
                            afdnompg = "Wikipedia:Articles for deletion/" + page2;
                            if (sortlogin())
                                wf.Save("Wikipedia:Articles for deletion/" + page2, afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                        }
                        if (number == 2)
                        {
                            afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)";
                            if (sortlogin())
                                wf.Save("Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)", afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                        }
                        else if (number == 3)
                        {
                            afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)";
                            if (sortlogin())
                                wf.Save("Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)", afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                        }
                        else if (number != 1 && number != 2 && number != 3)
                        {
                            afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (" + number.ToString() + "th nomination)";
                            if (sortlogin())
                                wf.Save("Wikipedia:Articles for deletion/" + page2 + " (" + number.ToString() + "th nomination)", afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                        }

                        DateTime now = new DateTime();
                        now = DateTime.UtcNow;

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

                        string logpg = wf.getWikiText("Wikipedia:Articles for deletion/Log/" + datetoday);
                        logpg = logpg + "\r\n{{" + afdnompg + "}}";
                        if (sortlogin())
                            wf.Save("Wikipedia:Articles for deletion/Log/" + datetoday, logpg, "Adding [[" + page2 + "]] to list using [[WP:NPW|NPWatcher]]");


                        afdsuc = false;
                    }
                }
            }
            webBrowser1.Refresh();
            Greyin();
        }
        #endregion

        #region Maintainance tags

        private void cleanupBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{subst:Cleanup-now}}";
            Mark(template);

            Greyin();
        }

        private void toneBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{tone}}";
            Mark(template);
            Greyin();
        }

        private void sourcesBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{unsourced}}";
            Mark(template);
            Greyin();
        }

        //Doesn't use Mark(..)
        private void stubBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (string.IsNullOrEmpty(stubTxt.Text))
            {
                DialogResult dr = MessageBox.Show(this, "You didn't enter a stub type.  Would you like to use the generic tag?",
                    "Stub types", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    MessageBox.Show("Please enter a stub-type in the test box, and then click the stub button again", "Custom stubs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string template = "{{stub}}";
                    string txt = wf.getWikiText(page2);
                    string newtxt = txt + "\r\n" + template;
                    Save(page2, newtxt, "Marking page with " + template + " using [[WP:NPW|NPWatcher]]");
                }
            }
            else
            {
                string template = "{{" + stubTxt.Text + "-stub}}";
                string txt = wf.getWikiText(page2);
                string newtxt = txt + "\r\n" + template;
                Save(page2, newtxt, "Marking page with " + template + " using [[WP:NPW|NPWatcher]]");
            }
            Greyin();
        }

        private void advertBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{advert}}";
            Mark(template);
            Greyin();
        }

        private void nnBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{notability}}";
            Mark(template);
            Greyin();
        }

        private void wikifyBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{Wikify|date={{subst:CURRENTMONTHNAME}} {{subst:CURRENTYEAR}}}}";
            Mark(template);
            Greyin();
        }

        private void npovBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{npov}}";
            Mark(template);
            Greyin();
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{copypaste}}";
            Mark(template);
            Greyin();
        }

        private void ceBtn_Click(object sender, EventArgs e)
        {
            //ce = copyedit!
            Greyout();
            string template = "{{copyedit}}";
            Mark(template);
            Greyin();
        }

        private void howtoBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{howto}}";
            Mark(template);
            Greyin();
        }

        private void sectionsBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{sections}}";
            Mark(template);
            Greyin();
        }

        private void introBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{introrewrite}}";
            Mark(template);
            Greyin();
        }

        private void contextBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{context}}";
            Mark(template);
            Greyin();
        }

        private void notEngBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            frmLangChoose LC = new frmLangChoose();

            if (LC.ShowDialog(this) == DialogResult.OK)
            {
                string template = "{{notenglish|" + LC.lang + "}}";
                Mark(template);
            }
            LC = null;

            Greyin();
        }

        private void uncatBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            string template = "{{uncategorized}}";
            string txt = wf.getWikiText(page2);
            string newtxt = txt + "\r\n" + template;
            Save(page2, newtxt, "Marking page with " + template + " using [[WP:NPW|NPWatcher]]");
            Greyin();

        }

        private void Mark(string template)
        {
            string txt = wf.getWikiText(page2);
            string newtxt = template + "\r\n" + txt;
            Save(page2, newtxt, "Marking page with " + template + " using [[WP:NPW|NPWatcher]]");
        }

        #endregion

        #region Admin only prod stuff (and speedy tag removal)

        private void rmvBtn_Click(object sender, EventArgs e)
        {
            Greyout();
            removetags(page2);
            Greyin();
        }

        private void removetags(string page)
        {
            string txt = wf.getWikiText(page2);
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
            string wikitext = wf.getWikiText(page2);
            if (wikitext.Contains("{{dated prod|concern = "))
            {
                try
                {
                    int startindex = wikitext.IndexOf("{{dated prod|concern = ");
                    int endindex;
                    if (wikitext.Contains("<!-- Do not use the \"dated prod\" tem"))
                    { endindex = wikitext.IndexOf("<!-- Do not use the \"dated prod\" tem"); }
                    else
                    {
                        if (wikitext.Contains("{{prod2a|"))
                        { endindex = wikitext.IndexOf("{{prod2a|"); }
                        else
                        { endindex = wikitext.IndexOf("{{prod2|"); }
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
            string wikitext = wf.getWikiText(page2);
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
                        string reason = "";
                        reason = prodreasonstr;
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
            string wikitextnew = wf.getWikiText(page2);
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
                    removetags(page2);
                    string txt = wf.getWikiText("Wikipedia:Articles for deletion/" + page2);
                    int number = 1;
                    while (!string.IsNullOrEmpty(txt))
                    {
                        number += 1;
                        if (number == 2)
                        { txt = wf.getWikiText("Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)"); }
                        else if (number == 3)
                        { txt = wf.getWikiText("Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)"); }
                        else
                        { txt = wf.getWikiText("Wikipedia:Articles for deletion/" + page2 + " (" + number.ToString() + "th nomination)"); }
                    }
                    string afdnom = "";

                    afdnom = "{{subst:afd2|pg=" + page2 + "|cat=" + afdCat + "|text=" + afdReason + "}} ~~~~";
                    string afdnompg = "";
                    if (number == 1)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2;
                        if (sortlogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2, afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }
                    if (number == 2)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)";
                        if (sortlogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2 + " (2nd nomination)", afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }
                    else if (number == 3)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)";
                        if (sortlogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2 + " (3rd nomination)", afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }
                    else if (number != 1 && number != 2 && number != 3)
                    {
                        afdnompg = "Wikipedia:Articles for deletion/" + page2 + " (" + number.ToString() + "th nomination)";
                        if (sortlogin())
                            wf.Save("Wikipedia:Articles for deletion/" + page2 + " (" + number.ToString() + "th nomination)", afdnom, "Nominating [[" + page2 + "]] for deletion using [[WP:NPW|NPWatcher]]");
                    }

                    string pgtxt = wf.getWikiText(page2);
                    string numbertxt;
                    if (number == 2)
                    { numbertxt = "2nd"; }
                    else if (number == 3)
                    { numbertxt = "3nd"; }
                    else
                    { numbertxt = number.ToString() + "nd"; }

                    if (number == 1)
                    {
                        pgtxt = "{{subst:afd1}}\r\n" + pgtxt;
                    }
                    else
                    {
                        pgtxt = "{{subst:afdx|" + numbertxt + "\r\n" + pgtxt;
                    }
                    if (sortlogin())
                        wf.Save(page2, pgtxt, "Nominating page for deletion using [[WP:NPW|NPWatcher]]", true);

                    DateTime now = new DateTime();
                    now = DateTime.UtcNow;

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

                    string logpg = wf.getWikiText("Wikipedia:Articles for deletion/Log/" + datetoday);
                    logpg = logpg + "\r\n{{" + afdnompg + "}}";
                    if (sortlogin())
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
                string reason = "";
                reason = prodreasonstr;
                prodreasonstr = "";
                if (string.IsNullOrEmpty(reason))
                {
                    reason = "No reason given";
                }
                if (sortlogin())
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
                string rat = "";
                rat = "Per [[WP:CSD#I1|CSD I1]] - image is redundant";
                if (orphanCB.Checked)
                { OrphanImage(page2, rat); }
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
                string rat = "";
                rat = "Per [[WP:CSD#I2|CSD I2]] - image is corrupt/empty";
                if (orphanCB.Checked)
                { OrphanImage(page2, rat); }
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
                string rat = "";
                rat = "Per [[WP:CSD#I3|CSD I3]] - image has an invalid license";
                if (orphanCB.Checked)
                { OrphanImage(page2, rat); }
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

        private void I3Warn(string page2)
        {
            string towarn = wf.GetCreator(page2);
            string userpg = wf.getWikiText("User_talk:" + towarn);
            userpg = userpg + "\r\n{{subst:Idw-noncom-deleted|" + System.Web.HttpUtility.UrlDecode(page2) + "}}";
            if (sortlogin())
                wf.Save("User_talk:" + towarn, userpg, "Warning user with {{Idw-noncom-deleted}} using [[WP:NPW|NPWatcher]]");
        }

        private void I4Btn_Click(object sender, EventArgs e)
        {
            Greyout();
            if (page2.Contains("Image:"))
            {
                string rat = "";
                rat = "Per [[WP:CSD#I4|CSD I4]] - image has no license";
                if (orphanCB.Checked)
                { OrphanImage(page2, rat); }
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
                string rat = "";
                rat = "Per [[WP:CSD#I5|CSD I5]] - image is unfree and is unused";
                if (orphanCB.Checked)
                { OrphanImage(page2, rat); }
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
                string rat = "";
                rat = "Per [[WP:CSD#I6|CSD I6]] - image has no fair use rationale";
                if (orphanCB.Checked)
                { OrphanImage(page2, rat); }
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
                string rat = "";
                rat = "Per [[WP:CSD#I7|CSD I7]] - image has an invalid fair use claim";
                if (orphanCB.Checked)
                { OrphanImage(page2, rat); }
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
                string rat = "";
                rat = "Per [[WP:CSD#I8|CSD I8]] - image exists on commons";

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

                    if (orphanCB.Checked)
                    { OrphanImage(page2, rat); }
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
            StringCollection imagelinks = new StringCollection();
            imagelinks = wf.getImgLinks("1000", page2);

            foreach (string p in imagelinks)
            {
                string wikitextp = wf.getWikiText(p);
                string wtq = RemoveImage(image, wikitextp);
                if (wtq.StartsWith("\n"))
                {
                    wtq = wtq.Substring(1, wtq.Length - 1);
                }
                if (sortlogin())
                    wf.Save(p, wtq, "Removing image using [[WP:NPW|NPWatcher]].  Reason given was: \"" + reason + "\".");
            }

        }

        public string RemoveImage(string Image, string ArticleText)
        {
            //remove image prefix
            Image = Regex.Replace(Image, "^Image:", "", RegexOptions.IgnoreCase).Replace("_", " ");
            Image = Regex.Escape(Image).Replace("\\ ", "[ _]");


            Regex r = new Regex("\\[\\[[Ii]mage:" + Image + ".*\\]\\]", RegexOptions.IgnoreCase);
            MatchCollection n = r.Matches(ArticleText);

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

                            ArticleText = t.Replace(ArticleText, "", 1);

                            break;
                        }
                    }
                }
            }
            else
            {
                r = new Regex("([Ii]mage:)?" + Image);
                n = r.Matches(ArticleText);

                foreach (Match m in n)
                {
                    Regex t = new Regex(Regex.Escape(m.Value));
                    ArticleText = t.Replace(ArticleText, "", 1, m.Index);
                }
            }

            return ArticleText;
        }
        #endregion

        private void Greyout()
        {
            Grey(false);
        }

        private void Greyin()
        {
            Grey(true);
            firstarticle.Checked = notabilitywarn.Checked = false;
        }

        private void Grey(bool Enabled)
        {
            dbattackBtn.Enabled = dbbioBtn.Enabled = dbBlankBtn.Enabled =
            dbBtn.Enabled = dbemptyBtn.Enabled = dbforeignBtn.Enabled =
            dbNonsBtn.Enabled = dbR1Btn.Enabled = dbR2Btn.Enabled =
            dbR3Btn.Enabled = dbRepostBtn.Enabled = dbspamBtn.Enabled =
            dbtalkBtn.Enabled = dbTest.Enabled = dvCvBtn.Enabled = dbvandBtn.Enabled =
            db_userreq.Enabled = prodBtn.Enabled = cleanupBtn.Enabled = toneBtn.Enabled =
            sourcesBtn.Enabled = stubBtn.Enabled = advertBtn.Enabled = nnBtn.Enabled =
            wikifyBtn.Enabled = notEngBtn.Enabled = uncatBtn.Enabled = rmvBtn.Enabled =
            npovBtn.Enabled = AfDBtn.Enabled = copyBtn.Enabled = delGivReasonBtn.Enabled =
            DelCustom.Enabled = AfDCustom.Enabled = RmvProd.Enabled = I1Btn.Enabled = I2Btn.Enabled =
            I3Btn.Enabled = I4Btn.Enabled = I5Btn.Enabled = I6Btn.Enabled = I7Btn.Enabled =
            I8Btn.Enabled = IotherBtn.Enabled = Enabled;
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = (int)((e.CurrentProgress / e.MaximumProgress) * 100);
        }

        private bool sortlogin()
        {
            bool loggedin = wf.getLogInStatus();

            if (loggedin)
                return true;
            else
            {
                wf.login(username, password);
                loggedin = wf.getLogInStatus();
                if (!loggedin)
                {
                    MessageBox.Show("Login error - unable to log in.  Please report this error!");
                    return false;
                }
                return true;
            }

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
    }
}
