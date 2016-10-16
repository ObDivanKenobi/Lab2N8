using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2N8
{
    public partial class Search : Form
    {
        string[] text;
        char[] separators = { ' ', ',', '.', ':', ';', '-', '—', '–', '!', '?', '\n', '\r'};
        public Search()
        {
            InitializeComponent();
        }

        public Search(string txt)
        {
            InitializeComponent();
            text = txt.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            string[] line = textBoxWord.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (line.Length == 0)
            {
                MessageBox.Show("Некорректное слово!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (line.Length > 1)
            {
                MessageBox.Show("Несколько слов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string word = line[0];

            List<KeyValuePair<string, int>> words = new List<KeyValuePair<string, int>>();
            foreach(string w in text)
                words.Add(new KeyValuePair<string, int>(w, CombinatorialAlgorithms.LevenshteinDistance(word, w)));

            words.Distinct();
            words.Sort((a, b) => a.Value.CompareTo(b.Value));

            textBoxResults.Clear();
            foreach (var w in words)
                textBoxResults.Text += string.Format("{0} ({1}){2}", w.Key, w.Value, Environment.NewLine);

        }
    }
}
