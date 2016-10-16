using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2N8
{
    /* В текстовом файле задан набор слов. Найти и упорядочить по степени похожести N слов, наиболее похожих на заданное. */

    public partial class Form1 : Form
    {
        const string help = "В текстовом файле задан набор слов. Найти и упорядочить по степени похожести N слов, наиболее похожих на заданное.";
        const string savedialog = "Сохранить изменения в тексте перед закрытием?";
        string currentFile;
        bool isChanged;

        public Form1()
        {
            InitializeComponent();
            isChanged = false;
            currentFile = null;
        }

        private void Help_Click(object sender, EventArgs e)
        {
            MessageBox.Show(help, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal void OnIdle(object sender, EventArgs e)
        {
            toolStripMenuItemFind.Enabled = textBoxText.Text != "";
        }

        private void textBoxText_TextChanged(object sender, EventArgs e)
        {
            isChanged = true;
        }

        bool CanClose()
        {
            if (!isChanged)
                return true;

            switch (MessageBox.Show("Сохранить изменения в документе перед закрытием?", "TrieTree", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        Save();
                        return !isChanged; //если сохранилось, то Modified будет false;
                    }
                case DialogResult.Cancel:
                    return false;
                default: return true;
            }
        }

        //сохранить
        public void Save()
        {
            if (currentFile == null)
                SaveAs();
            else
            {
                SaveToFile(currentFile);
                isChanged = false;
            }
        }

        //сохранить как
        void SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveToFile(sfd.FileName);
                isChanged = false;
            }
        }

        //запись в файл
        void SaveToFile(string filename)
        {
            StreamWriter write = new StreamWriter(filename);
            write.WriteLine(textBoxText.Text);
            write.Close();
        }

        //открытие файла
        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            if ((CanClose() && OpenFile.ShowDialog() == DialogResult.OK))
            {
                textBoxText.Clear();
                StreamReader read = new StreamReader(OpenFile.FileName);
                while (!read.EndOfStream)
                {
                    string line = read.ReadLine();
                    textBoxText.Text += line + Environment.NewLine;
                }
                read.Close();
                isChanged = false;
                currentFile = OpenFile.FileName;
            }
        }

        private void oolStripMenuItemCreate_Click(object sender, EventArgs e)
        {
            if(CanClose())
            {
                currentFile = null;
                textBoxText.Clear();
                isChanged = false;
            }
        }

        private void toolStripMenuItemFind_Click(object sender, EventArgs e)
        {
            Search s = new Search(textBoxText.Text);
            s.ShowDialog();
        }

        private void toolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            if (currentFile == null)
                SaveAs();
            else
                SaveToFile(currentFile);
        }
    }
}
