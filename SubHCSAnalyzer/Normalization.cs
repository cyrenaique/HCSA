using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weka.core;
using System.Data;
using weka.clusterers;
using LibPlateAnalysis;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;
using weka.classifiers;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer
{
    public partial class HCSAnalyzer
    {
        #region User interface
        private void comboBoxMethodForNormalization_SelectedIndexChanged(object sender, EventArgs e)
        {

            richTextBoxInfoForNormalization.Clear();

            switch (comboBoxMethodForNormalization.SelectedIndex)
            {
                case 0:
                    richTextBoxInfoForNormalization.AppendText("Single Ctrl based.\nData are all normalized to the average value of the selected class.");
                    richTextBoxInfoForNormalization.AppendText("\nFor more information, go to: http://www.nature.com/nbt/journal/v24/n2/abs/nbt1186.html");
                    comboBoxNormalizationPositiveCtrl.Enabled = false;
                    break;
                case 1:
                    richTextBoxInfoForNormalization.AppendText("Double Control Based.\nData are shifted to the average value of the negative class. Then, the values are normalized by the absolute difference between the negative and the positive means.");
                     richTextBoxInfoForNormalization.AppendText("\nFor more information, go to: http://www.nature.com/nbt/journal/v24/n2/abs/nbt1186.html");
                     comboBoxNormalizationPositiveCtrl.Enabled = true;
                    break;
                case 2:
                    richTextBoxInfoForNormalization.AppendText("Standardization.\nData are shifted to the average value of the negative class, then divided by the standard deviation of the negative class data distribution.");
                    richTextBoxInfoForNormalization.AppendText("\nFor more information, go to: http://en.wikipedia.org/wiki/Standard_score");
                    comboBoxNormalizationPositiveCtrl.Enabled = false;
                    break;

            }
        }

        private void richTextBoxInfoForNormalization_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            ClickOnLink(e.LinkText);
        }

        private void comboBoxNormalizationNegativeCtrl_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            SolidBrush BrushForColor = new SolidBrush(cGlobalInfo.ListWellClasses[e.Index].ColourForDisplay);
            e.Graphics.FillRectangle(BrushForColor, e.Bounds.X + 1, e.Bounds.Y + 1, 10, 10);
            if (e.Index == 0)
                e.Graphics.DrawString("Inactive", comboBoxClass.Font,
                                   System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            else
                e.Graphics.DrawString(cGlobalInfo.ListWellClasses[e.Index - 1].Name, comboBoxClass.Font,
                                        System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.DrawFocusRectangle();
        }

        private void comboBoxNormalizationPositiveCtrl_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            SolidBrush BrushForColor = new SolidBrush(cGlobalInfo.ListWellClasses[e.Index].ColourForDisplay);
            e.Graphics.FillRectangle(BrushForColor, e.Bounds.X + 1, e.Bounds.Y + 1, 10, 10);
            if (e.Index == 0)
                e.Graphics.DrawString("Inactive", comboBoxClass.Font,
                                   System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            else
                e.Graphics.DrawString(cGlobalInfo.ListWellClasses[e.Index - 1].Name, comboBoxClass.Font,
                                        System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + 15, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.DrawFocusRectangle();
        }

        private void buttonNormalize_Click(object sender, EventArgs e)
        {
            switch (comboBoxMethodForNormalization.SelectedIndex)
            {
                case 0:
                    NegativeBasedNormalization();
                    break;
                case 1:
                    NegativePositiveBasedNormalization();
                    break;
                case 2:
                    StandardNormalization();
                    break;
                case 3:
                    MinMaxNormalization();
                    break;
                default:
                    break;
            }

            MessageBox.Show("Process finished !", "Operation sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion

        #region Main Functions

                        
                   private void MinMaxNormalization()
                    {
                    }
                    


        private void StandardNormalization()
        {
            System.Windows.Forms.DialogResult Res = MessageBox.Show("By applying this process, data will be definitively modified ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Res == System.Windows.Forms.DialogResult.No) return;

            richTextBoxInfoForNormalization.Clear();

            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;
            int NumberOfProcessedPlates = 0;
            // loop on all the plate
            for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(PlateIdx);

                if (CurrentPlateToProcess.GetNumberOfWellOfClass(comboBoxNormalizationNegativeCtrl.SelectedIndex) < 2)
                {
                    richTextBoxInfoForNormalization.AppendText("\n" + CurrentPlateToProcess.GetName() + " does not contain enough well of the selected class. Plate skipped.");
                    continue;
                }


               // GlobalInfo.ConsoleWriteLine("Plate: " + CurrentPlateToProcess.Name);
                cExtendedList Neg = new cExtendedList();
                int NumDesc = cGlobalInfo.CurrentScreening.ListDescriptors.Count;

                cWell TempWell;
                // loop on all the desciptors
                for (int Desc = 0; Desc < NumDesc; Desc++)
                {
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsConnectedToDatabase == true)
                    {
                        cGlobalInfo.ConsoleWriteLine("Cell by cell normalization not implemented yet. " + cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + " skipped");
                        continue;
                    }

                    Neg.Clear();

                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsActive() == false) continue;

                    for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
                        for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
                        {
                            TempWell = CurrentPlateToProcess.GetWell(col, row, true);
                            if (TempWell == null) continue;
                            if (TempWell.GetCurrentClassIdx() == comboBoxNormalizationNegativeCtrl.SelectedIndex) Neg.Add(TempWell.ListSignatures[Desc].GetValue());
                        }

                    double CurrentMean = Neg.Mean();
                    double CurrentStd = Neg.Std();
                    if (CurrentStd == 0.0)
                    {
                        richTextBoxInfoForNormalization.AppendText("\n" + CurrentPlateToProcess.GetName() + " - " + cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + ", Standard deviation = 0, process cancelled");
                        goto NEXTPLATE;

                    }
                    cGlobalInfo.ConsoleWriteLine(cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + ", average = " + CurrentMean);

                    for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
                        for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
                        {
                            TempWell = CurrentPlateToProcess.GetWell(col, row, true);
                            if (TempWell == null) continue;
                           // for (int i = 0; i < TempWell.ListDescriptors[Desc].GetAssociatedType().GetBinNumber(); i++)
                            {
                                double Value = TempWell.ListSignatures[Desc].GetValue() - CurrentMean;
                                
                                TempWell.ListSignatures[Desc].SetHistoValues(Value / CurrentStd);
                            }

                            TempWell.ListSignatures[Desc].UpDateDescriptorStatistics();
                        }

                    CurrentPlateToProcess.UpDataMinMax();
                 
                    
                }
                richTextBoxInfoForNormalization.AppendText("\n" + CurrentPlateToProcess.GetName() + " successfully normalized.");
                NumberOfProcessedPlates++;
            NEXTPLATE: ;
            }
            richTextBoxInfoForNormalization.AppendText("\n" + NumberOfProcessedPlates + " / " + NumberOfPlates + " successfully normalized.");
        }

        private void NegativeBasedNormalization()
        {
            System.Windows.Forms.DialogResult Res = MessageBox.Show("By applying this process, data will be definitively modified ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Res == System.Windows.Forms.DialogResult.No) return;

            richTextBoxInfoForNormalization.Clear();

            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;


            int NumberOfProcessedPlates = 0;
            // loop on all the plate
            for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());
             //   GlobalInfo.ConsoleWriteLine("Plate: " + CurrentPlateToProcess.Name);

                if (CurrentPlateToProcess.GetNumberOfWellOfClass(comboBoxNormalizationNegativeCtrl.SelectedIndex) == 0)
                {
                    richTextBoxInfoForNormalization.AppendText("\n" + CurrentPlateToProcess.GetName() + " does not contain well of the selected class. Plate skipped.");
                    continue;
                }


                cExtendedList Neg = new cExtendedList();

                int NumDesc = cGlobalInfo.CurrentScreening.ListDescriptors.Count;

                cWell TempWell;
                // loop on all the desciptors
                for (int Desc = 0; Desc < NumDesc; Desc++)
                {

                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsConnectedToDatabase == true)
                    {
                        cGlobalInfo.ConsoleWriteLine("Cell by cell normalization not implemented yet. " + cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + " skipped");
                        continue;
                    }

                    Neg.Clear();

                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsActive() == false) continue;

                    for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
                        for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
                        {
                            TempWell = CurrentPlateToProcess.GetWell(col, row, true);
                            if (TempWell == null) continue;
                            if (TempWell.GetCurrentClassIdx() == comboBoxNormalizationNegativeCtrl.SelectedIndex) Neg.Add(TempWell.ListSignatures[Desc].GetValue());

                           
                        }

                    double CurrentMean = Neg.Mean();
                    cGlobalInfo.ConsoleWriteLine(cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + ", average = " + CurrentMean);

                    if (CurrentMean == 0)
                    {
                        richTextBoxInfoForNormalization.AppendText("\n" + CurrentPlateToProcess.GetName() + " / " + cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + " average is null!\n");
                        richTextBoxInfoForNormalization.AppendText("\nNormalization skipped.");
                        continue;
                    }

                    for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
                        for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
                        {
                            TempWell = CurrentPlateToProcess.GetWell(col, row, true);
                            if (TempWell == null) continue;
                         //   for (int i = 0; i < TempWell.ListDescriptors[Desc].GetAssociatedType().GetBinNumber(); i++)
                            {
                                double Val = TempWell.ListSignatures[Desc].GetValue();
                                Val /= CurrentMean;

                                TempWell.ListSignatures[Desc].SetHistoValues(Val * 100);

                                //TempWell.ListDescriptors[Desc].SetHistoValues(i,Val*100);
                            }

                            TempWell.ListSignatures[Desc].UpDateDescriptorStatistics();
                        }

                    CurrentPlateToProcess.UpDataMinMax();
                  
                }
                richTextBoxInfoForNormalization.AppendText("\n" + CurrentPlateToProcess.GetName() + " successfully normalized.");
                NumberOfProcessedPlates++;
            }
            richTextBoxInfoForNormalization.AppendText("\n" + NumberOfProcessedPlates + " / " + NumberOfPlates + " successfully normalized.");
        }

        private void NegativePositiveBasedNormalization()
        {
            System.Windows.Forms.DialogResult Res = MessageBox.Show("By applying this process, data will be definitively modified ! Proceed ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Res == System.Windows.Forms.DialogResult.No) return;

            richTextBoxInfoForNormalization.Clear();

            int NumberOfPlates = cGlobalInfo.CurrentScreening.ListPlatesActive.Count;
            int NumberOfProcessedPlates = 0;
            // loop on all the plate
            for (int PlateIdx = 0; PlateIdx < NumberOfPlates; PlateIdx++)
            {
                cPlate CurrentPlateToProcess = cGlobalInfo.CurrentScreening.ListPlatesActive.GetPlate(cGlobalInfo.CurrentScreening.ListPlatesActive[PlateIdx].GetName());
               // GlobalInfo.ConsoleWriteLine("Plate: " + CurrentPlateToProcess.Name);

                if ((CurrentPlateToProcess.GetNumberOfWellOfClass(comboBoxNormalizationNegativeCtrl.SelectedIndex)==0)||((CurrentPlateToProcess.GetNumberOfWellOfClass(comboBoxNormalizationPositiveCtrl.SelectedIndex)==0)))
                {
                    richTextBoxInfoForNormalization.AppendText("\n" + CurrentPlateToProcess.GetName() + " does not contain well of the selected classes. Plate skipped.");
                    continue;
                }

                cExtendedList Neg = new cExtendedList();
                cExtendedList Pos = new cExtendedList();
                int NumDesc = cGlobalInfo.CurrentScreening.ListDescriptors.Count;

                cWell TempWell;
                // loop on all the desciptors
                for (int Desc = 0; Desc < NumDesc; Desc++)
                {
                    Neg.Clear();
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsActive() == false) continue;
                    if (cGlobalInfo.CurrentScreening.ListDescriptors[Desc].IsConnectedToDatabase == true)
                    {
                        cGlobalInfo.ConsoleWriteLine("Cell by cell normalization not implemented yet. "+ cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName() + " skipped");
                        continue;
                    }

                    for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
                        for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
                        {
                            TempWell = CurrentPlateToProcess.GetWell(col, row, true);
                            if (TempWell == null) continue;
                            if (TempWell.GetCurrentClassIdx() == comboBoxNormalizationPositiveCtrl.SelectedIndex) Pos.Add(TempWell.ListSignatures[Desc].GetValue());
                            if (TempWell.GetCurrentClassIdx() == comboBoxNormalizationNegativeCtrl.SelectedIndex) Neg.Add(TempWell.ListSignatures[Desc].GetValue());
                        }

                    double CurrentMeanNeg = Neg.Mean();
                    double CurrentMeanPos = Pos.Mean();

                    if (CurrentMeanNeg == CurrentMeanPos)
                    {
                        cGlobalInfo.ConsoleWriteLine("Negative and positive are similar, no normalization operated on " + cGlobalInfo.CurrentScreening.ListDescriptors[Desc].GetName());
                        continue;
                    }
                    double Denominator = (Math.Abs(CurrentMeanPos - CurrentMeanNeg));

                    for (int row = 0; row < cGlobalInfo.CurrentScreening.Rows; row++)
                        for (int col = 0; col < cGlobalInfo.CurrentScreening.Columns; col++)
                        {
                            TempWell = CurrentPlateToProcess.GetWell(col, row, true);
                            if (TempWell == null) continue;
                            //for (int i = 0; i < TempWell.ListDescriptors[Desc].GetAssociatedType().GetBinNumber(); i++)
                            {
                                double CurrValue = TempWell.ListSignatures[Desc].GetValue();
                                TempWell.ListSignatures[Desc].SetHistoValues((CurrValue - CurrentMeanNeg) / Denominator);
                                //TempWell.ListDescriptors[Desc].Histogram.SetYvalues((CurrValue - CurrentMeanNeg) / Denominator,0);
                            }
                            //TempWell.ListDescriptors[Desc].Histogram.
                            TempWell.ListSignatures[Desc].UpDateDescriptorStatistics();
                        }

                    CurrentPlateToProcess.UpDataMinMax();
                   
                }
                richTextBoxInfoForNormalization.AppendText("\n" + CurrentPlateToProcess.GetName() + " successfully normalized.");
                NumberOfProcessedPlates++;
            }
            richTextBoxInfoForNormalization.AppendText("\n" + NumberOfProcessedPlates + " / " + NumberOfPlates + " successfully normalized.");

        }
        #endregion
    }
}
