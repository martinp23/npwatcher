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
    partial class AFDForm
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
            this.reasonTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.catTxt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nomBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // reasonTxt
            // 
            this.reasonTxt.Location = new System.Drawing.Point(99, 6);
            this.reasonTxt.Multiline = true;
            this.reasonTxt.Name = "reasonTxt";
            this.reasonTxt.Size = new System.Drawing.Size(263, 48);
            this.reasonTxt.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Deletion Reason";
            // 
            // catTxt
            // 
            this.catTxt.FormattingEnabled = true;
            this.catTxt.Items.AddRange(new object[] {
            "Media and music",
            "Organisation, corporation, or product",
            "Biographical",
            "Society topics",
            "Web or internet",
            "Games or sports",
            "Science and technology",
            "Fiction and the arts",
            "Places and transportation",
            "Indiscernable or unclassifiable topic",
            "Uncertain"});
            this.catTxt.Location = new System.Drawing.Point(99, 64);
            this.catTxt.Name = "catTxt";
            this.catTxt.Size = new System.Drawing.Size(121, 21);
            this.catTxt.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Category";
            // 
            // nomBtn
            // 
            this.nomBtn.Location = new System.Drawing.Point(206, 105);
            this.nomBtn.Name = "nomBtn";
            this.nomBtn.Size = new System.Drawing.Size(75, 23);
            this.nomBtn.TabIndex = 4;
            this.nomBtn.Text = "Nominate";
            this.nomBtn.UseVisualStyleBackColor = true;
            this.nomBtn.Click += new System.EventHandler(this.nomBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(287, 105);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // AfDForm
            // 
            this.AcceptButton = this.nomBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(374, 139);
            this.ControlBox = false;
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.nomBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.catTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reasonTxt);
            this.Name = "AfDForm";
            this.Text = "AfD Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox reasonTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox catTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button nomBtn;
        private System.Windows.Forms.Button cancelBtn;
    }
}