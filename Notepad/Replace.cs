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
    public partial class Replace : Form
    {
        public Replace()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm.FindText = textBox1.Text;
            MainForm.ReplaceText = textBox2.Text;
            this.Close();
        }

        private void Replace_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.FindText = textBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainForm.FindText = textBox1.Text;
            MainForm.ReplaceText = textBox2.Text;
            this.Close();
        }
    }
}
