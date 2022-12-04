using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DoTable
{
    public partial class Form1 : Form
    {
        public string script;
        public int width;
        public int height;

        public Form1()
        {
            InitializeComponent();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(script))
            {
                string[] l = File.ReadAllLines(script);
                for (int i = 0; i < l.Length; i++)
                {
                    try
                    {
                        if (l[i].StartsWith("setsize-"))
                        {
                            string def = l[i].Remove(0, 8);
                            string size1 = def.Substring(0, def.IndexOf("+"));
                            string size2 = def.Substring(def.IndexOf("+") + 1);
                            width = int.Parse(size1);
                            height = int.Parse(size2);
                        }
                        else if (l[i] == "create")
                        {
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    panel2.Controls.Add(new TextBox
                                    {
                                        Size = new Size(64, 24),
                                        Location = new Point(x * 64, y * 20)
                                    });
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        File.WriteAllText(@"errorlog_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".txt", "Line: " + i + ";\nName: " + ex.Source + ";\nMethod: " + ex.TargetSite + ";\nClass: " + ex.InnerException + ";\nMessage: " + ex.Message + ";\nEMERALD(DoTableLang) AUTO-LOG 0.1");
                    }
                }
            }
        }

        void SOFIC()
        {
            if (File.Exists(script + ".inf"))
                this.Text = "DoTable — " + File.ReadAllLines(script + ".inf")[0];
            else
                this.Text = "DoTable — " + script;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            script = openFileDialog1.FileName;
            openFileDialog1.FileName = "";
            SOFIC();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
        }
    }
}
