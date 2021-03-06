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

namespace NPWatcher
{
    partial class NNChoices
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
            this.genRB = new System.Windows.Forms.RadioButton();
            this.bandRB = new System.Windows.Forms.RadioButton();
            this.clubRB = new System.Windows.Forms.RadioButton();
            this.groupRB = new System.Windows.Forms.RadioButton();
            this.webRB = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.companyRB = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // genRB
            // 
            this.genRB.AutoSize = true;
            this.genRB.Location = new System.Drawing.Point(70, 32);
            this.genRB.Name = "genRB";
            this.genRB.Size = new System.Drawing.Size(80, 17);
            this.genRB.TabIndex = 0;
            this.genRB.TabStop = true;
            this.genRB.Text = "General Bio";
            this.genRB.UseVisualStyleBackColor = true;
            // 
            // bandRB
            // 
            this.bandRB.AutoSize = true;
            this.bandRB.Location = new System.Drawing.Point(70, 54);
            this.bandRB.Name = "bandRB";
            this.bandRB.Size = new System.Drawing.Size(50, 17);
            this.bandRB.TabIndex = 1;
            this.bandRB.TabStop = true;
            this.bandRB.Text = "Band";
            this.bandRB.UseVisualStyleBackColor = true;
            // 
            // clubRB
            // 
            this.clubRB.AutoSize = true;
            this.clubRB.Location = new System.Drawing.Point(70, 76);
            this.clubRB.Name = "clubRB";
            this.clubRB.Size = new System.Drawing.Size(46, 17);
            this.clubRB.TabIndex = 2;
            this.clubRB.TabStop = true;
            this.clubRB.Text = "Club";
            this.clubRB.UseVisualStyleBackColor = true;
            // 
            // groupRB
            // 
            this.groupRB.AutoSize = true;
            this.groupRB.Location = new System.Drawing.Point(70, 120);
            this.groupRB.Name = "groupRB";
            this.groupRB.Size = new System.Drawing.Size(54, 17);
            this.groupRB.TabIndex = 3;
            this.groupRB.TabStop = true;
            this.groupRB.Text = "Group";
            this.groupRB.UseVisualStyleBackColor = true;
            // 
            // webRB
            // 
            this.webRB.AutoSize = true;
            this.webRB.Location = new System.Drawing.Point(70, 142);
            this.webRB.Name = "webRB";
            this.webRB.Size = new System.Drawing.Size(64, 17);
            this.webRB.TabIndex = 4;
            this.webRB.TabStop = true;
            this.webRB.Text = "Website";
            this.webRB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select the most appropriate tag from below";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(28, 165);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 7;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(109, 165);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 8;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // companyRB
            // 
            this.companyRB.AutoSize = true;
            this.companyRB.Location = new System.Drawing.Point(70, 98);
            this.companyRB.Name = "companyRB";
            this.companyRB.Size = new System.Drawing.Size(69, 17);
            this.companyRB.TabIndex = 9;
            this.companyRB.TabStop = true;
            this.companyRB.Text = "Company";
            this.companyRB.UseVisualStyleBackColor = true;
            // 
            // NNChoices
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(213, 198);
            this.ControlBox = false;
            this.Controls.Add(this.companyRB);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.webRB);
            this.Controls.Add(this.groupRB);
            this.Controls.Add(this.clubRB);
            this.Controls.Add(this.bandRB);
            this.Controls.Add(this.genRB);
            this.Name = "NNChoices";
            this.Text = "Notability deletion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton genRB;
        private System.Windows.Forms.RadioButton bandRB;
        private System.Windows.Forms.RadioButton clubRB;
        private System.Windows.Forms.RadioButton groupRB;
        private System.Windows.Forms.RadioButton webRB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.RadioButton companyRB;
    }
}