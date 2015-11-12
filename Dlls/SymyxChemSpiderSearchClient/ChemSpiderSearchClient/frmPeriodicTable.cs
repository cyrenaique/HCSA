    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.IO;
using ChemSpiderClient.Properties;

namespace ChemSpiderClient
{

    public partial class frmPeriodicTable : Form
    {
        List<ElementData> elementsList;
        List<LegendData> legendList;
        Button[] elebuttons;
        Button[] legButtons;

        public frmPeriodicTable()
        {
            InitializeComponent();
            //path
            //string fileName = "PeriodicalTable.txt";
            //string path = Environment.CurrentDirectory;
            //path = Path.Combine(path, fileName);

            //creating new list<T>:
            elementsList = new List<ElementData>();
            legendList = new List<LegendData>();

            //reading file line by line and adding data into list<T>:
            string type = null;
            foreach (string data in ReadingData(Settings.Default.Elements))
            {
                if (data == "ELEMENT_LIST")
                {
                    type = "elements";
                    continue;
                }
                else if (data == "LEGEND")
                {
                    type = "legend";
                    continue;
                }

                if (type == "elements")
                    SeperateElementData(data);
                else if (type == "legend")
                    SeperateLegendData(data);
            }
            //Creating phisical periodical table from the data:
            CreatingElements();
            //Creating legends:
            CreatingLegends();
        }

        private void CreatingElements()
        {
            elebuttons = new Button[elementsList.Count];
            //starting position for 1st upper left button:
            int x = 20;
            int y = 60;
            //and size of buttons:
            int size = 48;
            foreach (ElementData el in elementsList)
            {
                if (el.LocationX > 0)
                    x = 20 + size * el.LocationX;
                else
                    x = 20;
                if (el.LocationY > 0)
                    y = 60 + size * el.LocationY;

                elebuttons[el.SequenceNumber - 1] = new Button();
                string btnText = String.Format("{0}\r\n{1}\r\n{2}\r\n{3}", el.SequenceNumber, el.ShortName, el.FullName, el.AtomicMass);
                elebuttons[el.SequenceNumber - 1].Location = new Point(x, y);
                elebuttons[el.SequenceNumber - 1].Size = new Size(size, size);
                elebuttons[el.SequenceNumber - 1].Font = new Font("Arial", 6, FontStyle.Regular);
                elebuttons[el.SequenceNumber - 1].Text = btnText;
                elebuttons[el.SequenceNumber - 1].Tag = el.FullName;
                elebuttons[el.SequenceNumber - 1].BackColor = GetButtonColor(el.Color);
                elebuttons[el.SequenceNumber - 1].Click += new EventHandler(ButtonElements_Click);
                this.Controls.Add(elebuttons[el.SequenceNumber - 1]);
            }
        }

        private void CreatingLegends()
        {
            legButtons = new Button[legendList.Count];
            //starting position for 1st upper left button:
            int x = 164;
            int y = 60;
            decimal size = 48;
            foreach (LegendData le in legendList)
            {
                x = Convert.ToInt32(20 + size * le.LocationX);
                if (le.LocationY > 0)
                    y = Convert.ToInt32(60 + size * le.LocationY);
                else
                    y = 60;

                legButtons[le.Number - 1] = new Button();
                legButtons[le.Number - 1].Text = le.GroupName;
                legButtons[le.Number - 1].Size = new Size(120, 25);
                legButtons[le.Number - 1].Location = new Point(x, y);
                legButtons[le.Number - 1].Font = new Font("Arial", 8, FontStyle.Regular);
                legButtons[le.Number - 1].BackColor = GetButtonColor(le.Number);
                legButtons[le.Number - 1].Tag = le.GroupName;
                legButtons[le.Number - 1].Click += new EventHandler(ButtonLegends_Click);
                this.Controls.Add(legButtons[le.Number - 1]);
            }
        }

        private void ButtonElements_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show(String.Format("Element {0} was selected.", btn.Tag));
        }

        private void ButtonLegends_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

        }

        private void SeperateElementData(string data)
        {
            string[] array = data.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            //removing tabs
            for (int i = 0; i < array.Length; i++)
                if (array[i].Contains("\t"))
                    array[i] = array[i].Replace("\t", "");

            ElementData el = new ElementData();
            el.SequenceNumber = int.Parse(array[0]);
            el.ShortName = array[1];
            el.FullName = array[2];
            el.AtomicMass = Convert.ToDecimal(array[3]);
            el.Brackets = (array[4] == "1") ? true : false;
            el.Color = int.Parse(array[5]);
            decimal[] xy = SeperateLocation(array[6]);
            el.LocationX = (int)xy[0];
            el.LocationY = (int)xy[1];
            el.Details = array[7];

            //addind data of element to list<T>:
            elementsList.Add(el);
        }

        private void SeperateLegendData(string data)
        {
            string[] array = data.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            //removing tabs
            for (int i = 0; i < array.Length; i++)
                if (array[i].Contains("\t"))
                    array[i] = array[i].Replace("\t", "");

            LegendData le = new LegendData();
            le.Number = int.Parse(array[0]);
            le.GroupName = array[1];
            decimal[] xy = SeperateLocation(array[2]);
            le.LocationX = xy[0];
            le.LocationY = xy[1];

            legendList.Add(le);
        }

        private decimal[] SeperateLocation(string str)
        {
            string[] arr = str.Split('x');
            decimal x = decimal.Parse(arr[0]);
            decimal y = decimal.Parse(arr[1]);

            return new decimal[] { x, y };
        }

        private Color GetButtonColor(int value)
        {
            Color backColor = new Color();
            switch (value)
            {
                case 1:
                    backColor = Color.LimeGreen;
                    break;
                case 2:
                    backColor = Color.Blue;
                    break;
                case 3:
                    backColor = Color.Orange;
                    break;
                case 4:
                    backColor = Color.Yellow;
                    break;
                case 5:
                    backColor = Color.ForestGreen;
                    break;
                case 6:
                    backColor = Color.Aquamarine;
                    break;
                case 7:
                    backColor = Color.GreenYellow;
                    break;
                case 8:
                    backColor = Color.Red;
                    break;
                case 9:
                    backColor = Color.Pink;
                    break;
                case 10:
                    backColor = Color.Magenta;
                    break;
            }
            return backColor;
        }

        private IEnumerable<string> ReadingData(string PTable)
        {
            var textBox = new TextBox();
            textBox.Text = PTable;
            //using (StreamReader sr = new StreamReader(path))
            //{
            //string line;
            //while ((line = sr.ReadLine()) != null)
            foreach (var line in textBox.Lines)
            {
                if (!line.StartsWith("##") && line != "")
                    yield return line;
            }
            //}
        }
    }

    class ElementData
    {
        public int SequenceNumber { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public decimal AtomicMass { get; set; }
        public bool Brackets { get; set; }
        public int Color { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public string Details { get; set; }
    }

    class LegendData
    {
        public int Number { get; set; }
        public string GroupName { get; set; }
        public decimal LocationX { get; set; }
        public decimal LocationY { get; set; }
    }
}
