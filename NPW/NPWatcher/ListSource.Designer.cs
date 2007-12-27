<!--
From: http://svn.martinp23.com/npw/

Copyright 2007 Martin Peeks
Copyright 2007 Reedy_boy
Copyright 2007 Martijn Hoekstra  

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

-->
namespace NPWatcher
{
    partial class ListSource
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NPRad = new System.Windows.Forms.RadioButton();
            this.CSDRad = new System.Windows.Forms.RadioButton();
            this.CustomRad = new System.Windows.Forms.RadioButton();
            this.CatTxt = new System.Windows.Forms.TextBox();
            this.loadBtn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.chkHidePatrolled = new System.Windows.Forms.CheckBox();
            this.chkHideBot = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // NPRad
            // 
            this.NPRad.AutoSize = true;
            this.NPRad.Location = new System.Drawing.Point(22, 59);
            this.NPRad.Name = "NPRad";
            this.NPRad.Size = new System.Drawing.Size(79, 17);
            this.NPRad.TabIndex = 0;
            this.NPRad.TabStop = true;
            this.NPRad.Text = "New pages";
            this.NPRad.UseVisualStyleBackColor = true;
            // 
            // CSDRad
            // 
            this.CSDRad.AutoSize = true;
            this.CSDRad.Location = new System.Drawing.Point(22, 82);
            this.CSDRad.Name = "CSDRad";
            this.CSDRad.Size = new System.Drawing.Size(71, 17);
            this.CSDRad.TabIndex = 1;
            this.CSDRad.TabStop = true;
            this.CSDRad.Text = "CAT:CSD";
            this.CSDRad.UseVisualStyleBackColor = true;
            // 
            // CustomRad
            // 
            this.CustomRad.AutoSize = true;
            this.CustomRad.Location = new System.Drawing.Point(22, 106);
            this.CustomRad.Name = "CustomRad";
            this.CustomRad.Size = new System.Drawing.Size(108, 17);
            this.CustomRad.TabIndex = 2;
            this.CustomRad.TabStop = true;
            this.CustomRad.Text = "Custom Category:";
            this.CustomRad.UseVisualStyleBackColor = true;
            // 
            // CatTxt
            // 
            this.CatTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CatTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.CatTxt.Location = new System.Drawing.Point(135, 105);
            this.CatTxt.Name = "CatTxt";
            this.CatTxt.Size = new System.Drawing.Size(164, 20);
            this.CatTxt.TabIndex = 3;
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(192, 140);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(108, 24);
            this.loadBtn.TabIndex = 4;
            this.loadBtn.Text = "Load";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(22, 5);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(278, 48);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "Choose from below where you would like to get the list of pages from.  Custom cat" +
                "egory can be used for image deletion and prod sorting.";
            // 
            // chkHidePatrolled
            // 
            this.chkHidePatrolled.AutoSize = true;
            this.chkHidePatrolled.Checked = true;
            this.chkHidePatrolled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHidePatrolled.Location = new System.Drawing.Point(136, 60);
            this.chkHidePatrolled.Name = "chkHidePatrolled";
            this.chkHidePatrolled.Size = new System.Drawing.Size(123, 17);
            this.chkHidePatrolled.TabIndex = 7;
            this.chkHidePatrolled.Text = "Hide patrolled pages";
            this.chkHidePatrolled.UseVisualStyleBackColor = true;
            // 
            // chkHideBot
            // 
            this.chkHideBot.AutoSize = true;
            this.chkHideBot.Checked = true;
            this.chkHideBot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHideBot.Location = new System.Drawing.Point(136, 82);
            this.chkHideBot.Name = "chkHideBot";
            this.chkHideBot.Size = new System.Drawing.Size(156, 17);
            this.chkHideBot.TabIndex = 8;
            this.chkHideBot.Text = "Hide pages created by bots";
            this.chkHideBot.UseVisualStyleBackColor = true;
            // 
            // ListSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 176);
            this.ControlBox = false;
            this.Controls.Add(this.chkHideBot);
            this.Controls.Add(this.chkHidePatrolled);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.CatTxt);
            this.Controls.Add(this.CustomRad);
            this.Controls.Add(this.CSDRad);
            this.Controls.Add(this.NPRad);
            this.Name = "ListSource";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ListSource";
            this.Load += new System.EventHandler(this.ListSource_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton NPRad;
        private System.Windows.Forms.RadioButton CSDRad;
        private System.Windows.Forms.RadioButton CustomRad;
        private System.Windows.Forms.TextBox CatTxt;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox chkHidePatrolled;
        private System.Windows.Forms.CheckBox chkHideBot;
    }
}