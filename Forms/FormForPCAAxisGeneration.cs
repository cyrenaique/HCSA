using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Classes;

namespace HCSAnalyzer.Forms
{
    public partial class FormForProjections : Form
    {
        //cScreening CurrentScreening;
        public cListPlates PlatesToProcess;
        public bool IsPCA;

        public FormForProjections()
        {
            InitializeComponent();
            //this.CurrentScreening = CurrentScreening;
        }

        private void comboBoxForNeutralClass_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            SolidBrush BrushForColor = new SolidBrush(cGlobalInfo.ListWellClasses[e.Index].ColourForDisplay);
            e.Graphics.FillRectangle(BrushForColor, e.Bounds.X + 1, e.Bounds.Y + 1, 10, 10);
            e.Graphics.DrawString(comboBoxForNeutralClass.Items[e.Index].ToString(), comboBoxForNeutralClass.Font,
                System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.DrawFocusRectangle();
        }

        private void buttonClassification_Click(object sender, EventArgs e)
        {
            int NeutralClass = comboBoxForNeutralClass.SelectedIndex;
            string Result;
            if(IsPCA)
                Result = cGlobalInfo.WindowHCSAnalyzer.GeneratePCADescriptor(PlatesToProcess, (int)numericUpDownNumberOfAxis.Value, NeutralClass);
            else
                Result = cGlobalInfo.WindowHCSAnalyzer.GenerateLDADescriptor(PlatesToProcess, NeutralClass);

         
            this.Close();
        }


        //weka.filters.unsupervised.attribute.RandomProjection RP = new weka.filters.unsupervised.attribute.RandomProjection();
        //Instances Linstances = CompleteScreening.ListPlatesActive[CompleteScreening.CurrentDisplayPlateIdx].CreateInstancesWithoutClass();
        //RP.setInputFormat(Linstances);
        //RP.setNumberOfAttributes(2);
        //RP.setPercent(0);         
        //for (int i = 0; i < Linstances.numInstances(); i++)
        //{
        //    RP.input(Linstances.instance(i));
        //}

        //RP.batchFinished();
        //int NumAtt = RP.getNumberOfAttributes();
        //bool outF = RP.isOutputFormatDefined();
        //Instances newData = RP.getOutputFormat();

        //Instance processed;
        //while ((processed = RP.output()) != null)
        //{
        //    newData.add(processed);
        //}


    }
}
