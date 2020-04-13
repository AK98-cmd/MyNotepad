using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Find : Form
    {
        public Find()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
                MainForm.matchcase=true;
            else
                MainForm.matchcase=false;
            if (radioButton2.Checked == true)
                MainForm.direction = true;
            else
                MainForm.direction = false;
            MainForm.FindText = textBox1.Text;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MainForm.FindText = "";
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void Find_Load(object sender, EventArgs e)
        {

        }
    }
}
