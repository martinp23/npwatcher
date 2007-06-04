using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NPWatcher
{
    public partial class CustomWarning : Form
    {
        public CustomWarning()
        {
            InitializeComponent();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (ReasonTxt.Text != null)
            {
                if (!ReasonTxt.Text.Contains("{"))
                {
                    if (!ReasonTxt.Text.Contains("|"))
                    {
                        Main.cwr = ReasonTxt.Text;
                        Main.cwsuc = true;
                        this.DialogResult = DialogResult.OK;
                        
                        this.Hide();
                    }
                    else
                    {
                        //do nothing
                    }
                }
                else
                {
                    MessageBox.Show("The program will automatically add the {{ and }} around the template before putting it on the page, so it's safe to leave them out here");

                }
            }
            else
            {
                MessageBox.Show("Please enter a warning template name, or click cancel");
            }


        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Main.cwsuc = false;
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }
    }
}