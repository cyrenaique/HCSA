using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace ChemSpiderClient
{
    // ReSharper disable InconsistentNaming
    public partial class frmResults : Form
    // ReSharper restore InconsistentNaming
    {
        private readonly int[] _results;

        private readonly ChemSpiderSearch.Search _cs;
        private readonly ChemSpiderInChI.InChI _ci;
        private readonly TransferType _transferType;
        private readonly String _token;
        private readonly bool _closeOnTransfer;

        private int _position;

        public frmResults(int[] results, ChemSpiderSearch.Search cs, ChemSpiderInChI.InChI ci, string token, TransferType transferType, bool closeOnTransfer)
        {
            _cs = cs;
            _ci = ci;
            _token = token;
            _results = results;
            _transferType = transferType;
            _closeOnTransfer = closeOnTransfer;

            InitializeComponent();

            tsbTransfer.Enabled = (transferType != TransferType.None);

            if (_results.Length == 1)
            {
                tsbMovePrevious.Enabled = false;
                tsbMoveNext.Enabled = false;
                tsbMoveFirst.Enabled = false;
                tsbMoveLast.Enabled = false;
            }
            else
            {
                tsbMoveFirst.Enabled = false;
                tsbMovePrevious.Enabled = false;
                tsbMoveLast.Enabled = true;
                tsbMoveNext.Enabled = true;
            }
            Navigate(0);
        }

        public event TransferHandler StructureTransfering;

        public delegate void TransferHandler(object sender, StructureTransferEventsArgs e);

        private void tsbMoveFirst_Click(object sender, EventArgs e)
        {
            if (_position != 0)
            {
                _position = 0;
                Navigate(_position);
            }
            tsbMoveFirst.Enabled = false;
            tsbMovePrevious.Enabled = false;
            tsbMoveLast.Enabled = true;
            tsbMoveNext.Enabled = true;
        }

        private void tsbMovePrevious_Click(object sender, EventArgs e)
        {
            _position--;
            if (_position < 0)
            {
                _position = 0;
                tsbMoveFirst.Enabled = false;
                tsbMovePrevious.Enabled = false;
                tsbMoveLast.Enabled = true;
                tsbMoveNext.Enabled = true;
            }
            else
            {
                Navigate(_position);
                tsbMovePrevious.Enabled = true;
                tsbMoveNext.Enabled = true;
                tsbMoveFirst.Enabled = true;
                tsbMoveLast.Enabled = true;
            }
        }

        private void tsbMoveNext_Click(object sender, EventArgs e)
        {
            _position++;
            if (_position > _results.Length - 1)
            {
                _position = _results.Length - 1;
                tsbMoveFirst.Enabled = true;
                tsbMovePrevious.Enabled = true;
                tsbMoveLast.Enabled = false;
                tsbMoveNext.Enabled = false;
            }
            else
            {
                Navigate(_position);
                tsbMovePrevious.Enabled = true;
                tsbMoveNext.Enabled = true;
                tsbMoveFirst.Enabled = true;
                tsbMoveLast.Enabled = true;
            }
        }

        private void tsbMoveLast_Click(object sender, EventArgs e)
        {
            if (_position != _results.Length - 1)
            {
                _position = _results.Length - 1;
                Navigate(_position);
            }
            tsbMoveFirst.Enabled = true;
            tsbMovePrevious.Enabled = true;
            tsbMoveLast.Enabled = false;
            tsbMoveNext.Enabled = false;
        }

        private void Navigate(int position)
        {
            webBrowser1.AllowNavigation = true;
            webBrowser1.Navigate(new Uri("http://www.chemspider.com/Chemical-Structure." + _results[position] + ".html"));
            toolStripStatusLabel1.Text = "Loading requested result...";
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void tsbTransfer_Click(object sender, EventArgs e)
        {
            Image thumbNail = null;
            var smiles = string.Empty;
            var inchi = string.Empty;
            var inchiKey = string.Empty;
            var molfileString = string.Empty;

            var html = webBrowser1.DocumentText;
            
            switch (_transferType)
            {
                case(TransferType.All):
                    {
                        var compoundInfo = _cs.GetCompoundInfo(_results[_position], _token);
                        molfileString = _ci.CSIDToMol(_results[_position].ToString(), _token);
                        thumbNail = ByteArrayToImage(_cs.GetCompoundThumbnail(_results[_position].ToString(), _token));
                        smiles = compoundInfo.SMILES;
                        inchi = compoundInfo.InChI;
                        inchiKey = compoundInfo.InChIKey;
                        break;
                    }

                case (TransferType.AllChemistry):
                    {
                        var compoundInfo = _cs.GetCompoundInfo(_results[_position], _token);
                        molfileString = _ci.CSIDToMol(_results[_position].ToString(), _token);
                        smiles = compoundInfo.SMILES;
                        inchi = compoundInfo.InChI;
                        inchiKey = compoundInfo.InChIKey;
                        break;
                    }

                case (TransferType.AllInChI):
                    {
                        var compoundInfo = _cs.GetCompoundInfo(_results[_position], _token);
                        inchi = compoundInfo.InChI;
                        inchiKey = compoundInfo.InChIKey;
                        break;
                    }

                case (TransferType.InChI):
                    {
                        var compoundInfo = _cs.GetCompoundInfo(_results[_position], _token);
                        inchi = compoundInfo.InChI;
                        break;
                    }

                case (TransferType.InChIKey):
                    {
                        var compoundInfo = _cs.GetCompoundInfo(_results[_position], _token);
                        inchiKey = compoundInfo.InChIKey;
                        break;
                    }

                case (TransferType.MolfileString):
                    {
                        molfileString = _ci.CSIDToMol(_results[_position].ToString(), _token);
                        break;
                    }

                case (TransferType.Smiles):
                    {
                        var compoundInfo = _cs.GetCompoundInfo(_results[_position], _token);
                        smiles = compoundInfo.SMILES;
                        break;
                    }
            }

            var structureTransferEventArgs = new StructureTransferEventsArgs
            {
                SMILES = smiles,
                CSID = _results[_position],
                InChI = inchiKey,
                InChIKey = inchiKey,
                MolfileString = molfileString,
                ThumbNail = thumbNail
            };
            StructureTransfering(this, structureTransferEventArgs);
            if (_closeOnTransfer)
                Close();
        }

        private void tsbOpenInBrowser_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(webBrowser1.Url.OriginalString);
        }

        private void frmResults_Shown(object sender, EventArgs e)
        {
            Owner.Cursor = Cursors.Default;
            Focus();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.AllowNavigation = false;
            toolStripStatusLabel1.Text = (_position + 1) + " of " + _results.Length + " hits";
        }
    }
}
