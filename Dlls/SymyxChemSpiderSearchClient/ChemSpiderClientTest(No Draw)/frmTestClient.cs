using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ChemSpiderClient.ChemSpiderSearch;
using ChemSpiderClient;
using ChemSpiderClientTest_No_Draw_.Properties;

namespace ChemSpiderClientTest_No_Draw_
{
    public partial class Form1 : Form
    {
        private readonly string _token = Settings.Default.Token;
        private readonly ChemSpiderClient.Search _search;
        private readonly TransferType _transferType;

        public Form1()
        {
            InitializeComponent();
            _transferType = TransferType.All;
            if (String.IsNullOrEmpty(_token))
                _token = ChemSpiderClient.Search.SetToken(_token, this);
            if (!String.IsNullOrEmpty(_token))
            {
                _search = new ChemSpiderClient.Search(_token, this, _transferType, true);
                _search.StructureTransfered += (search_StructureTransfered);
            }
            else
            {
                MessageBox.Show(this, "ChemSpider token not set", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
             PerformSearch(textBox1.Text, SearchType.Simple);
        }

        private void subStructureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformSearch(textBox1.Text, SearchType.SubStructure);
        }

        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformSearch(textBox1.Text, SearchType.Simple);
        }

        private void structureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformSearch(textBox1.Text, SearchType.ExactStructure);
        }

        private void similarityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformSearch(textBox1.Text, SearchType.Similarity);
        }

        private void predictedPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new Dictionary<string, object>();
            PerformPropertySearch(SearchType.PredictedProperty, settings);
        }

        private void lassoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformSearch(textBox1.Text, SearchType.Lasso);
        }

        private void intrinsicPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new Dictionary<string, object>();
            PerformPropertySearch(SearchType.IntrinsicProperty, settings);
        }

        private void elementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformSearch(textBox1.Text, SearchType.Elements);
        }

        private void PerformPropertySearch(SearchType searchType, Dictionary<string, object> settings)
        {
            bool cancel;
            var options = _search.SetPropertySearchOptions(searchType, settings, out cancel);
            if (!cancel)
            {
                Cursor = Cursors.AppStarting;
                _search.ExecuteSearch(string.Empty, searchType, options);
            }
        }

        private void PerformSearch(string queryString, SearchType searchType)
        {
            bool cancel;
            var options = _search.SetSearchOptions(searchType, out cancel);
            if (!cancel)
            {
                Cursor = Cursors.AppStarting;
                _search.ExecuteSearch(queryString, searchType, options);
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _search.SetCommonSearchOptions();
        }

        // ReSharper disable InconsistentNaming
        void search_StructureTransfered(object sender, StructureTransferEventsArgs e)
        // ReSharper restore InconsistentNaming
        {
            pictureBox1.Image = e.ThumbNail;
        }

        private void setTokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Token = ChemSpiderClient.Search.SetToken(Settings.Default.Token, this);
            Settings.Default.Save();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.chemspider.com/Search.asmx");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_token))
                Close();
        }
    }
}
