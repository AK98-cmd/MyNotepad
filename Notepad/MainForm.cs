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
    public partial class MainForm : Form
    {
        SaveFileDialog save = new SaveFileDialog();
        OpenFileDialog open = new OpenFileDialog();
        public static string FindText="";
        public static string ReplaceText;
        public static int linenumber;
        public static Boolean matchcase;
        public static int linescount;
        public static bool direction;
        string path = "";
        int d=-2;
        public MainForm()
        {
            InitializeComponent();
         
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                cutToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
            }
            else
            {
                cutToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
            }
            int index = richTextBox1.SelectionStart;
            int line = richTextBox1.GetLineFromCharIndex(index);
            Ln.Text ="Ln" + " " + (line + 1).ToString() + ",";
            //Col.Text = Col + " " + index.ToString();
            int firstChar = richTextBox1.GetFirstCharIndexFromLine(line);
            int column = index - firstChar;
            Col.Text = "Col" + " " + (column+1).ToString();
        }
        

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified == true)
            {
                DialogResult dr = MessageBox.Show("Do you want to save changes to the opened file ?", "Notepad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {

                    richTextBox1.Modified = false;

                }
                else
                {
                    if (this.Text == "Untitled - Notepad")//checking form Title to Untitled - Notepad      
                    {
                        if(save.ShowDialog() == DialogResult.OK)
                         richTextBox1.SaveFile(this.Text);
                    }
                    else
                    {
                        DialogResult dr1 = MessageBox.Show("The text in the file has been changed. Do you want to save the changes", "Open", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr1 == DialogResult.Yes)
                        {
                            richTextBox1.SaveFile(this.Text);

                        }


                    }
                }
            }
            open.Title="Open";
            open.Filter="Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(open.FileName, RichTextBoxStreamType.PlainText);
                this.Text = open.FileName;
            }
            path = open.FileName;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != "")
                richTextBox1.SaveFile(path);
            else
            {
              
                save.Title = "Save";
                save.Filter = "Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(save.FileName, RichTextBoxStreamType.PlainText);
                    this.Text = save.FileName;
                    path = save.FileName;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            statusStrip1.Visible = false;
            int line = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
            Ln.Text ="Ln" + " " + (line + 1).ToString() + ",";
            int firstChar = richTextBox1.GetFirstCharIndexFromLine(line);
            int column = line - firstChar;
            Col.Text = "Col" + " " + (column+1).ToString();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        public void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Find r = new Find();
            r.ShowDialog();
            if (FindText != "")
            {
                if (matchcase == true)
                    d = richTextBox1.Find(FindText, richTextBox1.Text.Length, RichTextBoxFinds.MatchCase);
                else
                    d = richTextBox1.Find(FindText);
            }
            if (d == -1)
                MessageBox.Show("Cannot find \"" + FindText + "\"");
            
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindText != "")
           {
               if (matchcase == true)
                   d = richTextBox1.Find(FindText, (d + 1), richTextBox1.Text.Length,RichTextBoxFinds.MatchCase);
                else
                   d = richTextBox1.Find(FindText, (d + 1), richTextBox1.Text.Length,RichTextBoxFinds.None);
            }
           
            if (d == -1)
                MessageBox.Show("Cannot find \"" + FindText+"\"");

            if (d == -2)
                MessageBox.Show("Search textbox uninitialized");
            
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replace r = new Replace();
            r.ShowDialog();
            if(richTextBox1.Find(FindText)==-1)
                MessageBox.Show("Cannot find \"" + FindText + "\"");
            else
                richTextBox1.SelectedText = ReplaceText;
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            linescount = richTextBox1.Lines.Count();
            Goto r = new Goto();
            r.ShowDialog();
            if (linenumber <= linescount)
            {
                int index;
                index = richTextBox1.GetFirstCharIndexFromLine(linenumber - 1);
                richTextBox1.Select(index, 0);
                richTextBox1.Focus();
            }
        }
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + " "+System.DateTime.Now.ToString();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save";
            save.Filter = "Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(save.FileName, RichTextBoxStreamType.PlainText);
                this.Text = save.FileName;
                path = save.FileName;
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetupDialog pageSetup = new PageSetupDialog();
            pageSetup.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            pageSetup.PageSettings = new System.Drawing.Printing.PageSettings();
            pageSetup.EnableMetric = false;
            pageSetup.ShowDialog();
        }
       

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDialog print = new PrintDialog();
            print.ShowDialog();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wordWrapToolStripMenuItem.Checked == false)
            {
                richTextBox1.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
            }
            else
            {
                richTextBox1.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
                richTextBox1.Font = font.Font;

        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StatusBar.Checked == true)
            {
                StatusBar.Checked = false;

            }
            else
                StatusBar.Checked = true;

            if (StatusBar.Checked == true)
            {
                statusStrip1.Visible = true;
                //Size size = new Size();
                //size = richTextBox1.Size;
                //size.Height -= 22;
                //richTextBox1.Size = Size;
                Size size = new Size();
                size = richTextBox1.Size;
                size.Height -= 22;
                richTextBox1.Size = size;

            }
            else
            {
                statusStrip1.Visible = false;
                //Size size = new Size();
                //size = richTextBox1.Size;
                //size.Height += 22;
                //richTextBox1.Size = Size;
                Size size = new Size();
                size = richTextBox1.Size;
                size.Height += 22;
                richTextBox1.Size = size;
            }
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                //richTextBox1.ForeColor = color.Color;
                richTextBox1.SelectionColor = color.Color;
            }
           
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
                richTextBox1.BackColor = color.Color;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if(this.Text=="Untitled - Notepad")
               {
                    save.ShowDialog();
                    if(save.ShowDialog() == DialogResult.OK)
                         richTextBox1.SaveFile(this.Text);
                    richTextBox1.Clear();
                    this.Text = "Untitled - Notepad";
                    richTextBox1.Modified = false;
               }
            else
            {
                if (richTextBox1.Modified == true)
                {
                    DialogResult dr = MessageBox.Show("Do you want to save the file", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)          
                    {
                        richTextBox1.SaveFile(this.Text);
                        richTextBox1.Clear();
                        this.Text = "Untitled - Notepad";
                        richTextBox1.Modified = false;
                    }
                    else if (dr == DialogResult.No)//statament that execute when user click on no button of dialog           
                    {
                        richTextBox1.Clear();
                        this.Text = "Untitled - Notepad";
                        richTextBox1.Modified = false;
                    }
                }
            }
           
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            int index = richTextBox1.SelectionStart;
            int line = richTextBox1.GetLineFromCharIndex(index);
            Ln.Text ="Ln" + " " + (line + 1).ToString() + ",";
            int firstChar = richTextBox1.GetFirstCharIndexFromLine(line);
            int column = index - firstChar;
            Col.Text = "Col" + " " + (column+1).ToString();
        }

        private void richTextBox1_SizeChanged(object sender, EventArgs e)
        {
            //richTextBox1.Dock = DockStyle.Fill;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
