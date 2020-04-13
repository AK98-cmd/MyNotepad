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
    public partial class Goto : Form
    {
        public Goto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.linenumber = Convert.ToInt16(textBox1.Text);
            if (MainForm.linenumber <= MainForm.linescount)
                this.Close();
            else
            {

                DialogResult dialog=MessageBox.Show("The line number is beyond the total number of lines","Notepad - Goto Line");
                if (dialog == DialogResult.OK)
                    textBox1.Text = "1";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Goto_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1";
        }
    }
}
