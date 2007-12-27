/*
From: http://svn.martinp23.com/npw/

Copyright 2007 Martin Peeks
Copyright 2007 Reedy_Boy
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

*/
namespace NPWatcher
{
    partial class CustomReason
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
            this.label1 = new System.Windows.Forms.Label();
            this.ReasonTxt = new System.Windows.Forms.TextBox();
            this.OKBtn = new System.Windows.Forms.Button();
            this.Cancelbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 0;
            string textLabel1 = "Please enter a reason for deletion";
            this.label1.Text = textLabel1;
                
            // 
            // ReasonTxt
            // 
            this.ReasonTxt.Location = new System.Drawing.Point(15, 34);
            this.ReasonTxt.Name = "ReasonTxt";
            this.ReasonTxt.Size = new System.Drawing.Size(185, 20);
            this.ReasonTxt.TabIndex = 1;
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(44, 60);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 2;
            string OKBtnTxt = "OK"; 
            this.OKBtn.Text = OKBtnTxt;
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // Cancelbtn
            // 
            this.Cancelbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelbtn.Location = new System.Drawing.Point(125, 60);
            this.Cancelbtn.Name = "Cancelbtn";
            this.Cancelbtn.Size = new System.Drawing.Size(75, 23);
            this.Cancelbtn.TabIndex = 3;
            string CancelBtnTxt = "Cancel";
            this.Cancelbtn.Text = CancelBtnTxt;
            this.Cancelbtn.UseVisualStyleBackColor = true;
            this.Cancelbtn.Click += new System.EventHandler(this.Cancelbtn_Click);
            // 
            // CustomReason
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancelbtn;
            this.ClientSize = new System.Drawing.Size(212, 95);
            this.ControlBox = false;
            this.Controls.Add(this.Cancelbtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.ReasonTxt);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomReason";
            string FormName = "Deletion Reason";
            this.Text = FormName;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ReasonTxt;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button Cancelbtn;
    }
}