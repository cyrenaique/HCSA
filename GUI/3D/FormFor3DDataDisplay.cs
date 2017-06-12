using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kitware.VTK;
using HCSAnalyzer.Classes._3D;
using LibPlateAnalysis;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using HCSAnalyzer.Forms._3D;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.General_Types;

namespace HCSAnalyzer.Forms
{
    public partial class FormFor3DDataDisplay : Form
    {
        FormFor3DVizuOptions WindowFormFor3DVizuOptions = null;
        public cScreening CompleteScreening = null;
        vtkOrientationMarkerWidget widget;
       public double RadiusSphere = 0.01;
        public double SphereOpacity = 1;
        private bool IsFullScreen;
        c3DWorld CurrentWorld = null;
        vtkAxesActor axes;
        double FontSize = 5;
        PanelForClassSelection ClassSelectionPanel;
        public vtkActor actorSpheres;
        public vtkSphereSource SphereSource;

        #region Lighting parameters
        public vtkLight light;
        bool IsLightAutomated = true;
        bool IsDisplayLightSource = false;
        public double LightIntensity;
        public double LightDiffuse;
        public double LightAmbient;
        public double LightSpecular;

        #endregion


        ToolTip ToolTipForX = new ToolTip();
        ToolTip ToolTipForY = new ToolTip();
        ToolTip ToolTipForZ = new ToolTip();

        public FormFor3DDataDisplay(bool IsFullScreen, cScreening CompleteScreening)
        {
            this.CompleteScreening = CompleteScreening;
            InitializeComponent();

            // toolTip1.SetToolTip(comboBoxDescriptorX,comboBoxDescriptorX.Text+"aaaaaa");
            //comboBoxDescriptorX.MouseHover += new System.EventHandler(this.comboBoxDescriptorX_MouseHover);

            //this.comboBoxDescriptorX.tool
            // Set up the ToolTip text for the Button and Checkbox.
            // toolTip1.SetToolTip(, "Unsupervised feature selection.\nThese approaches use all the active wells as data for the dimensionality reduction.");


            this.IsFullScreen = IsFullScreen;
            //  WindowFormFor3DVizuOptions.Parent = this;

            for (int i = 0; i < (int)CompleteScreening.ListDescriptors.Count; i++)
            {
                ListScales.Add(1);
            }

            ClassSelectionPanel = new PanelForClassSelection(true, Classes.Base_Classes.GUI.eClassType.WELL);
            ClassSelectionPanel.Height = panelForClasses.Height;
            ClassSelectionPanel.SelectAll();
            panelForClasses.Controls.Add(ClassSelectionPanel);

            foreach (var CurrentCheckBox in ClassSelectionPanel.ListCheckBoxes)
            {
                CurrentCheckBox.CheckedChanged += new EventHandler(CurrentCheckBox_CheckedChanged);
            }

            ToolTipForX.AutoPopDelay = ToolTipForY.AutoPopDelay = ToolTipForZ.AutoPopDelay = 5000;
            ToolTipForX.InitialDelay = ToolTipForY.InitialDelay = ToolTipForZ.InitialDelay = 500;
            ToolTipForX.ReshowDelay = ToolTipForY.ReshowDelay = ToolTipForZ.ReshowDelay = 500;
            ToolTipForX.ShowAlways = ToolTipForY.ShowAlways = ToolTipForZ.ShowAlways = true;
            ToolTipForX.SetToolTip(comboBoxDescriptorX, comboBoxDescriptorX.Text);


            this.WindowFormFor3DVizuOptions = new FormFor3DVizuOptions(this);

            this.LightIntensity = (double)this.WindowFormFor3DVizuOptions.numericUpDownLightIntensity.Value;
            this.LightAmbient = (double)this.WindowFormFor3DVizuOptions.numericUpDownLightAmbient.Value;
            this.LightDiffuse = (double)this.WindowFormFor3DVizuOptions.numericUpDownLightIntensity.Value;
            this.LightSpecular = (double)this.WindowFormFor3DVizuOptions.numericUpDownLightIntensity.Value;

            SphereSource = vtkSphereSource.New();


        }

        private void CurrentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DisplayXYZ();
        }

        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            DisplayXYZ();
        }

        public List<double> ListScales = new List<double>();
       
        public void DisplayXYZ()
        {
            if (CompleteScreening == null) return;

            int DescX = this.comboBoxDescriptorX.SelectedIndex;
            int DescY = this.comboBoxDescriptorY.SelectedIndex;
            int DescZ = this.comboBoxDescriptorZ.SelectedIndex;

            if (DescX < 0) DescX = 0;
            if (DescY < 0) DescY = 0;
            if (DescZ < 0) DescZ = 0;

            int[] Pos = new int[2];
            Pos[0] = 0;
            Pos[1] = 0;

            if (CurrentWorld == null)
            {
                CurrentWorld = new c3DWorld(new cPoint3D(1000, 1000, 1000), new cPoint3D(ListScales[DescX], ListScales[DescY], ListScales[DescZ]), this.renderWindowControl1, Pos, CompleteScreening);
                light = vtkLight.New(); 
                CurrentWorld.SetBackgroundColor(Color.Black);
               // CurrentWorld.ren1.AddLight(light);
                //CurrentWorld.ren1.RemoveAllLights();
            }

           
            CurrentWorld.ren1.RemoveAllViewProps();

            //  if (widget != null) widget.SetEnabled(0);

            Series CurrentSeries = new Series("ScatterPoints");

            double MinX = double.MaxValue;
            double MinY = double.MaxValue;
            double MinZ = double.MaxValue;
            double MaxZ = double.MinValue;
            double MaxX = double.MinValue;
            double MaxY = double.MinValue;

            double TempX, TempY, TempZ;
            int Idx = 0;

            cListPlates ListPlate = new cListPlates();

            cMetaBiologicalObjectList ListMeta = new cMetaBiologicalObjectList("Test");
            cBiologicalSpot CurrentSpot1 = new cBiologicalSpot(Color.White, new cPoint3D(0, 0, 0), 1, 4);
            cMetaBiologicalObject Plate3D = new cMetaBiologicalObject("Data", ListMeta, CurrentSpot1);

            if (!IsFullScreen)
                ListPlate.Add(CompleteScreening.GetCurrentDisplayPlate());
            else
                ListPlate = CompleteScreening.ListPlatesActive;

            vtkUnsignedCharArray colors = vtkUnsignedCharArray.New();
            colors.SetName("colors");
            colors.SetNumberOfComponents(3);
            vtkPoints Allpoints = vtkPoints.New();

            cExtendedList ListPtX = new cExtendedList();
            cExtendedList ListPtY = new cExtendedList();
            cExtendedList ListPtZ = new cExtendedList();

            List<bool> ListCheckBoxes = ClassSelectionPanel.GetListSelectedClass();

            for (int i = 0; i < ListPlate.Count; i++)
            {
                cPlate CurrentPlate = ListPlate[i];
                for (int IdxValue = 0; IdxValue < CompleteScreening.Columns; IdxValue++)
                    for (int IdxValue0 = 0; IdxValue0 < CompleteScreening.Rows; IdxValue0++)
                    {
                        cWell TmpWell = CurrentPlate.GetWell(IdxValue, IdxValue0, true);
                        if ((TmpWell != null) && (ListCheckBoxes[TmpWell.GetCurrentClassIdx()]))
                        {

                            TempX = TmpWell.ListSignatures[DescX].GetValue();
                            if (TempX < MinX) MinX = TempX;
                            if (TempX > MaxX) MaxX = TempX;


                            TempY = TmpWell.ListSignatures[DescY].GetValue();
                            if (TempY < MinY) MinY = TempY;
                            if (TempY > MaxY) MaxY = TempY;

                            TempZ = TmpWell.ListSignatures[DescZ].GetValue();
                            if (TempZ < MinZ) MinZ = TempZ;
                            if (TempZ > MaxZ) MaxZ = TempZ;

                            //   cBiologicalSpot CurrentSpot = new cBiologicalSpot(TmpWell.GetColor(), new cPoint3D(TempX, TempY, TempZ), 1, 4);

                            List<char> Col = new List<char>();

                            Col.Add((char)(TmpWell.GetClassColor().R));
                            Col.Add((char)(TmpWell.GetClassColor().G));
                            Col.Add((char)(TmpWell.GetClassColor().B));

                            // IntPtr unmanagedPointer = Marshal.UnsafeAddrOfPinnedArrayElement(Col.ToArray(), 0);

                            //colors.InsertNextTupleValue(unmanagedPointer);
                            colors.InsertNextTuple3(Col[0], Col[1], Col[2]);

                            ListPtX.Add(TempX);
                            ListPtY.Add(TempY);
                            ListPtZ.Add(TempZ);

                            //     CurrentSpot.Name = TmpWell.AssociatedPlate.Name + " - " + TmpWell.GetPosX() + "x" + TmpWell.GetPosY() + " :" + TmpWell.Name;
                            //    CurrentSpot.ObjectType = TmpWell.AssociatedPlate.Name + " - " + TmpWell.GetPosX() + "x" + TmpWell.GetPosY() + " :" + TmpWell.Name;
                            //    Plate3D.AddObject(CurrentSpot);
                            // CurrentWorld.AddBiological3DObject(CurrentSpot);
                            //CurrentSeries.Points.Add(TempX, TempY);

                            //                                if (IsFullScreen)
                            //                                    CurrentSeries.Points[Idx].ToolTip = TmpWell.AssociatedPlate.Name + "\n" + TmpWell.GetPosX() + "x" + TmpWell.GetPosY() + " :" + TmpWell.Name;
                            //                                else
                            //                                    CurrentSeries.Points[Idx].ToolTip = TmpWell.GetPosX() + "x" + TmpWell.GetPosY() + " :" + TmpWell.Name;

                            Idx++;
                        }
                    }
            }


            double MinValueX = ListPtX.Min();
            double MaxValueX = ListPtX.Max();
            cExtendedList NormX = ListPtX.Normalize(eNormalizationType.MIN_MAX);
            if (NormX == null) return;

            double MinValueY = ListPtY.Min();
            double MaxValueY = ListPtY.Max();
            cExtendedList NormY = ListPtY.Normalize(eNormalizationType.MIN_MAX);
            if (NormY == null) return;

            double MinValueZ = ListPtZ.Min();
            double MaxValueZ = ListPtZ.Max();
            cExtendedList NormZ = ListPtZ.Normalize(eNormalizationType.MIN_MAX);
            if (NormZ == null) return;


            for (int IdxPt = 0; IdxPt < ListPtX.Count; IdxPt++)
                Allpoints.InsertNextPoint(NormX[IdxPt], NormY[IdxPt], NormZ[IdxPt]);


            vtkPolyData polydata = vtkPolyData.New();
            polydata.SetPoints(Allpoints);
            polydata.GetPointData().SetScalars(colors);
           // vtkSphereSource SphereSource = vtkSphereSource.New();
            SphereSource.SetRadius(RadiusSphere);
            //SphereSource
            vtkGlyph3D glyph3D = vtkGlyph3D.New();
            glyph3D.SetColorModeToColorByScalar();
            glyph3D.SetSourceConnection(SphereSource.GetOutputPort());

            glyph3D.SetInput(polydata);
            glyph3D.ScalingOff();
            glyph3D.Update();


            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(glyph3D.GetOutputPort());
            actorSpheres = vtkActor.New();
            actorSpheres.SetMapper(mapper);
            actorSpheres.GetProperty().SetOpacity(this.SphereOpacity);
            CurrentWorld.ren1.AddActor(actorSpheres);
            
            if (this.IsLightAutomated)
            {
                CurrentWorld.ren1.RemoveAllLights();
                CurrentWorld.ren1.AutomaticLightCreationOn();
               // this.light.SetIntensity(0);
            }
            else
            {  
                CurrentWorld.ren1.RemoveAllLights();
                CurrentWorld.ren1.AddLight(light);
                this.light.SetIntensity(this.LightIntensity);
                CurrentWorld.ren1.AutomaticLightCreationOff();
                actorSpheres.GetProperty().SetInterpolationToPhong();
                actorSpheres.GetProperty().SetAmbient(this.LightAmbient);
                actorSpheres.GetProperty().SetDiffuse(this.LightDiffuse);
                actorSpheres.GetProperty().SetSpecular(this.LightSpecular);
                

                
                light.SetFocalPoint(0.5, 0.5, 1);
                light.SetPosition(0.5, 0.5, -1);

                light.SetDiffuseColor(1, 1, 1);

                if (this.IsDisplayLightSource)
                {
                    light.SetPositional(1);
                    vtkLightActor lightActor = vtkLightActor.New();
                    lightActor.SetLight(light);
                    CurrentWorld.ren1.AddViewProp(lightActor);
                }

               

               
            } 
            
            
            #region Connect For DRC
            if ((CompleteScreening.GetCurrentDisplayPlate().ListDRCRegions != null) && (cGlobalInfo.OptionsWindow.checkBoxConnectDRCPts.Checked))
            {
                foreach (cDRC_Region TmpRegion in CompleteScreening.GetCurrentDisplayPlate().ListDRCRegions)
                {
                    int cpt = 0;

                    cWell[][] ListWells = TmpRegion.GetListWells();

                    foreach (cWell[] item in ListWells)
                    {
                        for (int IdxWell = 0; IdxWell < item.Length - 1; IdxWell++)
                        {
                            //cWell TmpWell0 = CompleteScreening.GetCurrentDisplayPlate().GetWell(item[IdxWell], IdxValue0, true);

                            if ((item[IdxWell] != null) && (item[IdxWell + 1] != null) && (item[IdxWell].GetCurrentClassIdx() >= -1))
                            {
                                double StartX = (item[IdxWell].ListSignatures[DescX].GetValue() - MinValueX) / (MaxValueX - MinValueX);
                                double StartY = (item[IdxWell].ListSignatures[DescY].GetValue() - MinValueY) / (MaxValueY - MinValueY);
                                double StartZ = (item[IdxWell].ListSignatures[DescZ].GetValue() - MinValueZ) / (MaxValueZ - MinValueZ);
                                double EndX = (item[IdxWell + 1].ListSignatures[DescX].GetValue() - MinValueX) / (MaxValueX - MinValueX);
                                double EndY = (item[IdxWell + 1].ListSignatures[DescY].GetValue() - MinValueY) / (MaxValueY - MinValueY);
                                double EndZ = (item[IdxWell + 1].ListSignatures[DescZ].GetValue() - MinValueZ) / (MaxValueZ - MinValueZ);
                                cPoint3D StartPt = new cPoint3D(StartX, StartY, StartZ);
                                cPoint3D EndPt = new cPoint3D(EndX, EndY, EndZ);
                                c3DLine NewLine = new c3DLine(StartPt, EndPt);
                                CurrentWorld.AddGeometric3DObject(NewLine);
                            }

                        }
                    }
                    /*List<cDRC> ListDRC = new List<cDRC>();
                    for (int i = 0; i < CompleteScreening.ListDescriptors.Count; i++)
                    {
                        if (CompleteScreening.ListDescriptors[i].IsActive())
                        {
                            cDRC CurrentDRC = new cDRC(TmpRegion, CompleteScreening.ListDescriptors[i]);

                            ListDRC.Add(CurrentDRC);
                            cpt++;
                        }

                    }
                    */
                    //cDRCDisplay DRCDisplay = new cDRCDisplay(ListDRC, GlobalInfo);

                    //if (DRCDisplay.CurrentChart.Series.Count == 0) continue;

                    //DRCDisplay.CurrentChart.Location = new Point((DRCDisplay.CurrentChart.Width + 50) * 0, (DRCDisplay.CurrentChart.Height + 10 + DRCDisplay.CurrentRichTextBox.Height) * h++);
                    //DRCDisplay.CurrentRichTextBox.Location = new Point(DRCDisplay.CurrentChart.Location.X, DRCDisplay.CurrentChart.Location.Y + DRCDisplay.CurrentChart.Height + 5);

                    //WindowforDRCsDisplay.LChart.Add(DRCDisplay.CurrentChart);
                    //WindowforDRCsDisplay.LRichTextBox.Add(DRCDisplay.CurrentRichTextBox);
                }
            }
            #endregion

            #region Build axis
            // vtkAxesActor axis = vtkAxesActor.New();
            vtkAxisActor axisX = vtkAxisActor.New();
            axisX.SetPoint1(0, 0, 0);
            axisX.SetPoint2(1, 0, 0);
            axisX.SetTickLocationToBoth();
            axisX.SetDeltaMajor(0.1);
            axisX.SetMajorTickSize(0);
            axisX.MinorTicksVisibleOff();
            //axisX.Maj
            CurrentWorld.ren1.AddActor(axisX);


            vtkAxisActor axisY = vtkAxisActor.New();
            axisY.SetPoint1(0, 0, 0);
            axisY.SetPoint2(0, 1, 0);
            axisY.SetTickLocationToBoth();
            axisY.SetDeltaMajor(0.1);
            axisY.SetMajorTickSize(0.05);
            axisY.MinorTicksVisibleOff();
            CurrentWorld.ren1.AddActor(axisY);

            vtkAxisActor axisZ = vtkAxisActor.New();
            axisZ.SetPoint1(0, 0, 0);
            axisZ.SetPoint2(0, 0, 1);
            axisZ.SetTickLocationToBoth();
            axisZ.SetDeltaMajor(0.1);
            axisZ.SetMajorTickSize(0.05);
            axisZ.MinorTicksVisibleOff();
            CurrentWorld.ren1.AddActor(axisZ);



            if (widget == null)
            {
                widget = vtkOrientationMarkerWidget.New();

                axes = vtkAxesActor.New();
                widget.SetOutlineColor(0.9300, 0.5700, 0.1300);

                widget.SetInteractor(CurrentWorld.iren);
                widget.SetViewport(0.0, 0.0, 0.4, 0.4);
                widget.SetEnabled(0);
                // widget.InteractiveOn();

                if (this.comboBoxDescriptorX.SelectedItem == null)
                    axes.SetXAxisLabelText(this.comboBoxDescriptorX.Items[0].ToString());
                else
                    axes.SetXAxisLabelText(this.comboBoxDescriptorX.SelectedItem.ToString());

                if (this.comboBoxDescriptorY.SelectedItem == null)
                    axes.SetYAxisLabelText(this.comboBoxDescriptorY.Items[0].ToString());
                else
                    axes.SetYAxisLabelText(this.comboBoxDescriptorY.SelectedItem.ToString());

                if (this.comboBoxDescriptorZ.SelectedItem == null)
                    axes.SetZAxisLabelText(this.comboBoxDescriptorZ.Items[0].ToString());
                else
                    axes.SetZAxisLabelText(this.comboBoxDescriptorZ.SelectedItem.ToString());

                widget.SetOrientationMarker(axes);
            }
            else
            {
                if (this.comboBoxDescriptorX.SelectedItem != null)
                    axes.SetXAxisLabelText(this.comboBoxDescriptorX.SelectedItem.ToString());

                if (this.comboBoxDescriptorY.SelectedItem != null)
                    axes.SetYAxisLabelText(this.comboBoxDescriptorY.SelectedItem.ToString());

                if (this.comboBoxDescriptorZ.SelectedItem != null)
                    axes.SetZAxisLabelText(this.comboBoxDescriptorZ.SelectedItem.ToString());

                widget.SetOrientationMarker(axes);
            }

            if ((this.checkBoxForDisplayAxesInformation.Checked) && (ListPtX.Count > 0))
            {
                double CurrentFontSize = this.FontSize / 400.0;// 0.02;

                c3DText MaxAxeX = new c3DText(CurrentWorld, MaxValueX.ToString("N2"), new cPoint3D(1, 0, 0), Color.White, CurrentFontSize);
                CurrentWorld.AddGeometric3DObject(MaxAxeX);

                c3DText MaxAxeY = new c3DText(CurrentWorld, MaxValueY.ToString("N2"), new cPoint3D(0, 1, 0), Color.White, CurrentFontSize);
                CurrentWorld.AddGeometric3DObject(MaxAxeY);

                c3DText MaxAxeZ = new c3DText(CurrentWorld, MaxValueZ.ToString("N2"), new cPoint3D(0, 0, 1), Color.White, CurrentFontSize);
                CurrentWorld.AddGeometric3DObject(MaxAxeZ);

                c3DText MinAxeX = new c3DText(CurrentWorld, MinValueX.ToString("N2"), new cPoint3D(0, -0.1, -0.1), Color.White, CurrentFontSize);
                CurrentWorld.AddGeometric3DObject(MinAxeX);

                c3DText MinAxeY = new c3DText(CurrentWorld, MinValueY.ToString("N2"), new cPoint3D(-0.1, -0.1, 0), Color.White, CurrentFontSize);
                CurrentWorld.AddGeometric3DObject(MinAxeY);

                c3DText MinAxeZ = new c3DText(CurrentWorld, MinValueZ.ToString("N2"), new cPoint3D(-0.1, 0, -0.1), Color.White, CurrentFontSize);
                CurrentWorld.AddGeometric3DObject(MinAxeZ);
            }
            #endregion

            //vtkCameraWidget Wid = vtkCameraWidget.New();
            //Wid.SetInteractor(CurrentWorld.iren);
            //Wid.SetEnabled(1);
            //  Wid.InteractiveOn();

            //vtkDistanceWidget distanceWidget = vtkDistanceWidget.New();
            //distanceWidget.SetInteractor(CurrentWorld.iren);
            //distanceWidget.SetEnabled(1);
            //distanceWidget.CreateDefaultRepresentation();
            //((vtkDistanceRepresentation)distanceWidget.GetRepresentation()).SetLabelFormat("%-#6.3g mm");
            /*static_cast<vtkDistanceRepresentation*>(distanceWidget->GetRepresentation())
              ->SetLabelFormat("%-#6.3g mm");

                      */
            //  Plate3D.GenerateAndDisplayBoundingBox(1, Color.White, false, CurrentWorld);
            //c3DText CaptionX = new c3DText(CurrentWorld, CompleteScreening.ListDescriptors[DescX].GetName(), new cPoint3D(MaxX, MinY, MinZ), Color.DarkRed, this.FontSize);
            //c3DLine LineX = new c3DLine(new cPoint3D(MinX, MinY, MinZ), new cPoint3D(MaxX, MinY, MinZ), Color.DarkRed);
            //CurrentWorld.AddGeometric3DObject(LineX);

            //c3DText CaptionY = new c3DText(CurrentWorld, CompleteScreening.ListDescriptors[DescY].GetName(), new cPoint3D(MinX, MaxY, MinZ), Color.DarkGreen, this.FontSize);
            //c3DLine LineY = new c3DLine(new cPoint3D(MinX, MinY, MinZ), new cPoint3D(MinX, MaxY, MinZ), Color.DarkGreen);
            //CurrentWorld.AddGeometric3DObject(LineY);

            //c3DText CaptionZ = new c3DText(CurrentWorld, CompleteScreening.ListDescriptors[DescZ].GetName(), new cPoint3D(MinX, MinY, MaxZ), Color.DarkBlue, this.FontSize);
            //c3DLine LineZ = new c3DLine(new cPoint3D(MinX, MinY, MinZ), new cPoint3D(MinX, MinY, MaxZ), Color.DarkBlue);
            //CurrentWorld.AddGeometric3DObject(LineZ);

            #region Update ComboBoxes
            if (comboBoxDescriptorX.Text == "")
                ToolTipForX.SetToolTip(comboBoxDescriptorX, comboBoxDescriptorX.Items[0].ToString());
            else
                ToolTipForX.SetToolTip(comboBoxDescriptorX, comboBoxDescriptorX.Text);

            if (comboBoxDescriptorY.Text == "")
                ToolTipForY.SetToolTip(comboBoxDescriptorY, comboBoxDescriptorY.Items[0].ToString());
            else
                ToolTipForY.SetToolTip(comboBoxDescriptorY, comboBoxDescriptorY.Text);

            if (comboBoxDescriptorZ.Text == "")
                ToolTipForZ.SetToolTip(comboBoxDescriptorZ, comboBoxDescriptorZ.Items[0].ToString());
            else
                ToolTipForZ.SetToolTip(comboBoxDescriptorZ, comboBoxDescriptorZ.Text);
            #endregion

            CurrentWorld.SimpleRender();// Render();
        }

        private void comboBoxDescriptorX_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolTipForX.SetToolTip(comboBoxDescriptorX, comboBoxDescriptorX.Text);
            DisplayXYZ();
        }

        private void comboBoxDescriptorY_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolTipForY.SetToolTip(comboBoxDescriptorY, comboBoxDescriptorY.Text);
            DisplayXYZ();
        }

        private void comboBoxDescriptorZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolTipForZ.SetToolTip(comboBoxDescriptorZ, comboBoxDescriptorZ.Text);
            DisplayXYZ();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WindowFormFor3DVizuOptions.numericUpDownRadiusSphere.Value = (decimal)(this.RadiusSphere * 100.0);
            WindowFormFor3DVizuOptions.numericUpDownFontSize.Value = (decimal)this.FontSize;

            if (WindowFormFor3DVizuOptions.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.RadiusSphere = (double)WindowFormFor3DVizuOptions.numericUpDownRadiusSphere.Value / 100.0;
                this.FontSize = (double)WindowFormFor3DVizuOptions.numericUpDownFontSize.Value;
                this.SphereOpacity = (double)WindowFormFor3DVizuOptions.numericUpDownSphereOpacity.Value;
                this.IsDisplayLightSource = WindowFormFor3DVizuOptions.checkBoxLightDisplaySource.Checked;
                this.IsLightAutomated = WindowFormFor3DVizuOptions.radioButtonLightAutomated.Checked;
                DisplayXYZ();
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayXYZ();
        }

        private void axisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (axisToolStripMenuItem.Checked)
                widget.SetEnabled(1);
            else
                widget.SetEnabled(0);
        }

        private void selectedDescriptorsAsActiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cGlobalInfo.WindowHCSAnalyzer.checkedListBoxActiveDescriptors.Items.Count; i++)
                CompleteScreening.ListDescriptors.SetItemState(i, false);

            int DescX = this.comboBoxDescriptorX.SelectedIndex;
            int DescY = this.comboBoxDescriptorY.SelectedIndex;
            int DescZ = this.comboBoxDescriptorZ.SelectedIndex;
            CompleteScreening.ListDescriptors.SetItemState(DescX, true);
            CompleteScreening.ListDescriptors.SetItemState(DescY, true);
            CompleteScreening.ListDescriptors.SetItemState(DescZ, true);

            cGlobalInfo.WindowHCSAnalyzer.RefreshInfoScreeningRichBox();

            if (cGlobalInfo.ViewMode == eViewMode.PIE)
                CompleteScreening.GetCurrentDisplayPlate().DisplayDistribution(cGlobalInfo.CurrentScreening.ListDescriptors[0], false);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            DisplayXYZ();
        }

        private void checkBoxForDisplayAxesInformation_CheckedChanged(object sender, EventArgs e)
        {
            DisplayXYZ();
        }


    }




}
