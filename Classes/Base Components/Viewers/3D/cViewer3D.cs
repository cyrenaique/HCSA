using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.MetaComponents;
using Kitware.VTK;
using System.Windows.Forms;
using System.Drawing;
using ImageAnalysis;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine;
using HCSAnalyzer.Classes._3D;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.General_Types;
using HCSAnalyzer.Forms.FormsForImages;
using System.IO;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    public class cViewer3D : cDataDisplay
    {
        public cViewer3D()
        {
            base.Title = "3D Viewer";
        }

        c3DNewWorld Input = null;

        public c3DNewWorld GetAssociated3DWorld()
        {
            return Input;
        }

        vtkRenderWindowInteractor iren;
        RenderWindowControl renderWindowControl1;
        vtkLegendScaleActor legendScaleActor = null;
       // vtkRenderer Renderer;
        ToolStripMenuItem ToolStripMenuItem_ActorsInfo = new ToolStripMenuItem("Objects Manager");

        public void SetInputData(c3DNewWorld Input)
        {
            this.Input = Input;
        }

        public cFeedBackMessage Run()
        {

            renderWindowControl1 = new RenderWindowControl();
            renderWindowControl1.Load += new EventHandler(renderWindowControl1_Load);
            renderWindowControl1.Disposed += new EventHandler(renderWindowControl1_Disposed);


            renderWindowControl1.Width = base.CurrentPanel.Width;
            renderWindowControl1.Height = base.CurrentPanel.Height;

            base.CurrentPanel.Title = this.Title;
            base.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right);


            renderWindowControl1.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right);


           // CurrentPanel.MouseDown += new MouseEventHandler(renderWindowControl1_MouseDown);

            this.CurrentPanel.DragDrop += new DragEventHandler(CurrentPanel_DragDrop);
            this.CurrentPanel.DragEnter += new DragEventHandler(CurrentPanel_DragEnter);
            this.CurrentPanel.AllowDrop = true;


            CurrentPanel.Controls.Add(renderWindowControl1);


            ToolStripMenuItem_ActorsInfo.CheckOnClick = true;
            ToolStripMenuItem_ActorsInfo.Checked = false;

            return base.FeedBackMessage;
        }

        void renderWindowControl1_Disposed(object sender, EventArgs e)
        {
            this.Input.Dispose();
            this.Input = null;
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        public void CurrentPanel_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(typeof(cImage)) 
                || e.Data.GetDataPresent(typeof(cWell))
                || e.Data.GetDataPresent(typeof(cListWells)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public void CurrentPanel_DragDrop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(typeof(cImage)))
            {
                cImage SourceImage = (cImage)e.Data.GetData(typeof(cImage));

                if (SourceImage.Depth > 1)
                {
                    int CurrentChannel = 1;

                    UserControlSingleLUT SingleLUT = (UserControlSingleLUT)SourceImage.AssociatedImagePanel.LUTManager.panelForLUTS.Controls[CurrentChannel];
                    Input.AddVolume3D(new cVolumeRendering3D(SourceImage.SingleChannelImage[CurrentChannel], new cPoint3D(0, 0, 0), SingleLUT.SelectedLUT, Input));
                }
                else
                {

                    c3DTexturedPlan _3DPlan = new c3DTexturedPlan(new cPoint3D(0, 0, 0), SourceImage);
                    _3DPlan.Run();
                    Input.AddGeometric3DObject(_3DPlan);
                    
                }

                this.iren.Render();
            }
            else if (e.Data.GetDataPresent(typeof(cListWells)))
            {

                


                cListWells TmpList = (cListWells)e.Data.GetData(typeof(cListWells));

                if (MessageBox.Show("Do you want perform this opereation at a single cell level ?", "3D Drop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TmpList.BuildAndDisplaySingleCell3DScatterCloud(this);
                }
                else
                {
                    cExtendedTable TB = TmpList.GetAverageDescriptorValues(cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors(), false, false);

                    c3DPointCloud PC = new c3DPointCloud(TB);
                    PC.Create(new cPoint3D(0, 0, 0));
                    Input.AddGeometric3DObject(PC);
                    this.iren.Render();
                }

            }
        }

        #region change object display properties
      
        //private void _3DObjectProperties_GetVerticesList(object sender, EventArgs e)
        //{
        //    cObject3D Obj = (cObject3D)((ToolStripMenuItem)sender).Tag;
        //    if (Obj == null) return;

        //    try
        //    {
        //        cExtendedTable ET = ((cGeometric3DObject)(Obj)).GetListVertex().GetDataTable();
        //        ET.Name = "[" + Obj.GetName() + "] Vertices List"; 
        //        cDisplayExtendedTable DET = new cDisplayExtendedTable();
        //        DET.SetInputData(ET);
        //        DET.Run();
        //    }
        //    catch (Exception)
        //    {

        //        return;
        //    }

        //}

        //private void _3DObjectProperties_GetInfo(object sender, EventArgs e)
        //{
        //    cObject3D Obj = (cObject3D)((ToolStripMenuItem)sender).Tag;
        //    if (Obj == null) return;

        //    try
        //    {
        //        cExtendedTable ET = ((cGeometric3DObject)(Obj)).GetInfo();
               
        //        cDisplayExtendedTable DET = new cDisplayExtendedTable();
        //        DET.SetInputData(ET);
        //        DET.Run();
        //    }
        //    catch (Exception)
        //    {

        //        return;
        //    }

        //}

        //private void _3DObjectProperties_AddNotation(object sender, EventArgs e)
        //{
        //    cObject3D Obj = (cObject3D)((ToolStripMenuItem)sender).Tag;
        //    if (Obj == null) return;



        //    // Obj.GetActor().GetProperty().

        //    //Obj.AddText(
        //    //  Obj.SetToPoints();
        //}

        //private void _3DObjectProperties_WireFrame(object sender, EventArgs e)
        //{
        //    cObject3D Obj = (cObject3D)((ToolStripMenuItem)sender).Tag;
        //    if (Obj == null) return;

        //    Obj.SetToWireFrame();
        //}

        //private void _3DObjectProperties_Solid(object sender, EventArgs e)
        //{
        //    cObject3D Obj = (cObject3D)((ToolStripMenuItem)sender).Tag;
        //    if (Obj == null) return;

        //    Obj.SetToSurface();


        //}

        //private void _3DObjectProperties_Scale(object sender, EventArgs e)
        //{
        //    cObject3D Obj = (cObject3D)((ToolStripMenuItem)sender).Tag;

        //    if (Obj == null) return;
        //    FormForSingleSlider SliderForOpacity = new FormForSingleSlider("Object Scale");
        //    SliderForOpacity.numericUpDown.Maximum = 1000;
        //    SliderForOpacity.numericUpDown.Value = (decimal)(Obj.GetScale() * 100.0);

        //    if (SliderForOpacity.ShowDialog() != DialogResult.OK) return;

        //    Obj.SetScale((double)SliderForOpacity.numericUpDown.Value / 100.0);
        //}

        //private void _3DObjectProperties_Opacity(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        cVolumeRendering3D Obj = (cVolumeRendering3D)((ToolStripMenuItem)sender).Tag;
        //        if (Obj == null) return;
        //        Obj.ShowWindowVolumeRenderingOption();
        //    }
        //    catch (Exception)
        //    {
        //        cObject3D Obj = (cObject3D)((ToolStripMenuItem)sender).Tag;

        //        FormForSingleSlider SliderForOpacity = new FormForSingleSlider("Object Opacity");
        //        SliderForOpacity.numericUpDown.Value = (decimal)(Obj.GetOpacity() * 100.0);

        //        if (SliderForOpacity.ShowDialog() != DialogResult.OK) return;
        //        Obj.SetOpacity((double)SliderForOpacity.numericUpDown.Value / 100.0);
        //    }

        //}
        #endregion

        void RenderWindow_LeftButtonPressEvt(vtkObject sender, vtkObjectEventArgs e)
        {
            
        }

        void RenderWindow_RightButtonPressEvt(vtkObject sender, vtkObjectEventArgs e)
        {
            CompleteMenu = new ContextMenuStrip();

            ToolStripMenuItem ToolStripMenuItem_World = new ToolStripMenuItem("World");


            ToolStripMenuItem DisplayOptItem = new ToolStripMenuItem("Display Options");
            
            ToolStripMenuItem ScaleItem = new ToolStripMenuItem("Scale");
            DisplayOptItem.DropDownItems.Add(ScaleItem);
            ScaleItem.Click += new System.EventHandler(this.ScaleClicking);

            ToolStripMenuItem ToolStripMenuItem_ChangeBackgroundColor = new ToolStripMenuItem("Background Color");
            DisplayOptItem.DropDownItems.Add(ToolStripMenuItem_ChangeBackgroundColor);
            ToolStripMenuItem_ChangeBackgroundColor.Click += new System.EventHandler(this.ToolStripMenuItem_ChangeBackgroundColor);

            ToolStripMenuItem_World.DropDownItems.Add(DisplayOptItem);

            ToolStripMenuItem ToolStripMenuItem_SaveToImage = new ToolStripMenuItem("Save View");
            ToolStripMenuItem_World.DropDownItems.Add(ToolStripMenuItem_SaveToImage);
            ToolStripMenuItem_SaveToImage.Click += new System.EventHandler(this.ToolStripMenuItem_SaveToImage);



            ToolStripMenuItem_World.DropDownItems.Add(new ToolStripSeparator());

            //ToolStripMenuItem_ActorsInfo = new ToolStripMenuItem("Objects Manager");
            //ToolStripMenuItem_ActorsInfo.CheckOnClick = true;
            ToolStripMenuItem_World.DropDownItems.Add(ToolStripMenuItem_ActorsInfo);
            
            ToolStripMenuItem_ActorsInfo.Click += new System.EventHandler(_ToolStripMenuItem_ActorsInfo);


            string MetaObjectName = "";
            int[] Pos = iren.GetEventPosition();
            //vtkPicker Picker = vtkPicker.New();
            vtkPropPicker Pick = vtkPropPicker.New();
            Pick.Pick(Pos[0], Pos[1], 0, Input.AssociatedVTKRenderer);
            int numActors = Pick.Pick(Pos[0], Pos[1], 0, Input.AssociatedVTKRenderer);

            ToolStripMenuItem TypeItem = null;
            // ToolStripMenuItem ToolStripMenuItem_ObjectDisplay = new ToolStripMenuItem("Object Display");
            ToolStripMenuItem ToolStripMenuItem_DescriptorDisplay = new ToolStripMenuItem("Object Profil");

            if (numActors == 1)
            {
                vtkActor PickedActor = Pick.GetActor();

                for (int i = 0; i < this.Input.ListVolume.Count; i++)
                {

                    cVolumeRendering3D Obj = (cVolumeRendering3D)this.Input.ListVolume[i];

                   // ToolStripMenuItem ToolStripMenuItem_3DVolumeProperties = new ToolStripMenuItem("["+Obj.GetName()+"]");

                    foreach (var item in Obj.GetExtendedContextMenu())
                    {
                        CompleteMenu.Items.Add(item);
                    }
                }


                for (int i = 0; i < this.Input.ListObject.Count; i++)
                {
                    if (this.Input.ListObject[i].GetActor() == PickedActor)
                    {
                        if (this.Input.ListObject[i].GetType().BaseType.BaseType == typeof(cObject3D))
                        {
                            cObject3D Obj = (cObject3D)this.Input.ListObject[i];

                           // ToolStripMenuItem ToolStripMenuItem_3DObjectProperties = new ToolStripMenuItem("[" + Obj.GetName() + "] Properties");

                            //ToolStripMenuItem _3DObjectProperties_Opacity = new ToolStripMenuItem("Opacity");
                            //_3DObjectProperties_Opacity.Tag = Obj;
                            //ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(_3DObjectProperties_Opacity);
                            //_3DObjectProperties_Opacity.Click += new System.EventHandler(this._3DObjectProperties_Opacity);

                            //ToolStripMenuItem _3DObjectProperties_Color = new ToolStripMenuItem("Color");
                            //_3DObjectProperties_Color.Tag = Obj;
                            //ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(_3DObjectProperties_Color);
                            //_3DObjectProperties_Color.Click += new System.EventHandler(this._3DObjectProperties_Color);

                            //ToolStripMenuItem _3DObjectProperties_Scale = new ToolStripMenuItem("Scale");
                            //_3DObjectProperties_Scale.Tag = Obj;
                            //ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(_3DObjectProperties_Scale);
                            //_3DObjectProperties_Scale.Click += new System.EventHandler(this._3DObjectProperties_Scale);

                            //ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(new ToolStripSeparator());

                            //ToolStripMenuItem _3DObjectProperties_Solid = new ToolStripMenuItem("Surface");
                            //_3DObjectProperties_Solid.Tag = Obj;
                            //ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(_3DObjectProperties_Solid);
                            //_3DObjectProperties_Solid.Click += new System.EventHandler(this._3DObjectProperties_Solid);

                            //ToolStripMenuItem _3DObjectProperties_WireFrame = new ToolStripMenuItem("WireFrame");
                            //_3DObjectProperties_WireFrame.Tag = Obj;
                            //ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(_3DObjectProperties_WireFrame);
                            //_3DObjectProperties_WireFrame.Click += new System.EventHandler(this._3DObjectProperties_WireFrame);

                            //ToolStripMenuItem _3DObjectProperties_Point = new ToolStripMenuItem("Point");
                            //_3DObjectProperties_Point.Tag = Obj;
                            //ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(_3DObjectProperties_Point);
                            //_3DObjectProperties_Point.Click += new System.EventHandler(this._3DObjectProperties_Point);


                           // ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(new ToolStripSeparator());

                            foreach (var item in Obj.GetExtendedContextMenu())
                            {
                                CompleteMenu.Items.Add(item);
                                //ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(item);
                            }
                            


                            //ToolStripMenuItem _3DObjectProperties_AddNotation = new ToolStripMenuItem("Add Notation");
                            //_3DObjectProperties_AddNotation.Tag = Obj;
                            //ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(_3DObjectProperties_AddNotation);
                            //_3DObjectProperties_AddNotation.Click += new System.EventHandler(this._3DObjectProperties_AddNotation);



                            //ToolStripMenuItem _3DObjectProperties_INFO = new ToolStripMenuItem("Info");


                            //ToolStripMenuItem _3DObjectProperties_GetVerticesList = new ToolStripMenuItem("Vertices List");
                            //_3DObjectProperties_GetVerticesList.Tag = Obj;
                            //_3DObjectProperties_INFO.DropDownItems.Add(_3DObjectProperties_GetVerticesList);
                            //_3DObjectProperties_GetVerticesList.Click += new System.EventHandler(this._3DObjectProperties_GetVerticesList);

                            //_3DObjectProperties_INFO.DropDownItems.Add(new ToolStripSeparator());

                            //ToolStripMenuItem _3DObjectProperties_GetInfo = new ToolStripMenuItem("Info");
                            //_3DObjectProperties_GetInfo.Tag = Obj;
                            //_3DObjectProperties_INFO.DropDownItems.Add(_3DObjectProperties_GetInfo);
                            //_3DObjectProperties_GetInfo.Click += new System.EventHandler(this._3DObjectProperties_GetInfo);


                            //ToolStripMenuItem_3DObjectProperties.DropDownItems.Add(_3DObjectProperties_INFO);


                           // CompleteMenu.Items.Add(ToolStripMenuItem_3DObjectProperties);
                        }


                        if (this.Input.ListObject[i].GetType().BaseType != typeof(cGeometric3DObject))
                        {
                            cInteractive3DObject Obj = (cInteractive3DObject)this.Input.ListObject[i];
                            if (Obj.GetMetaObjectContainer() != null) MetaObjectName = Obj.GetMetaObjectContainer().Information.GetName() + " -> ";

                            Type T = this.Input.ListObject[i].GetType();
                            List<double> ListDesc = null;
                            List<string> ListProfilName = null;
                            //  ToolStripMenuItem ToolStripMenuItem_SelectedActor = new ToolStripMenuItem(T.ToString()+i);
                            //CompleteMenu.Items.Add(ToolStripMenuItem_SelectedActor);
                            //ToolStripMenuItem_ChangeBackgroundColor.Click += new System.EventHandler(this.ToolStripMenuItem_ChangeBackgroundColor);

                            if (T.Name == "cBiological3DVolume")
                            {
                                cBiological3DVolume TmpVol = (cBiological3DVolume)this.Input.ListObject[i];
                                ListProfilName = TmpVol.Information.GetDescriptorNames();
                                ListDesc = TmpVol.Information.GetInformation();
                                TypeItem = new ToolStripMenuItem(MetaObjectName + TmpVol.GetType());
                            }
                            else if (T.Name == "cBiologicalSpot")
                            {
                                cBiologicalSpot TmpSpot = (cBiologicalSpot)this.Input.ListObject[i];
                                ListProfilName = TmpSpot.Information.GetDescriptorNames();
                                ListDesc = TmpSpot.Information.GetInformation();
                                TypeItem = new ToolStripMenuItem(MetaObjectName + TmpSpot.GetType());
                            }

                            if (ListProfilName != null)
                                for (int idxName = 0; idxName < ListDesc.Count; idxName++)
                                {
                                    ToolStripMenuItem descName = new ToolStripMenuItem(ListProfilName[idxName] + " : " + ListDesc[idxName]);
                                    ToolStripMenuItem_DescriptorDisplay.DropDownItems.Add(descName);
                                }
                            else
                            {
                                ToolStripMenuItem descName = new ToolStripMenuItem("Null");
                                ToolStripMenuItem_DescriptorDisplay.DropDownItems.Add(descName);
                            }



                            //  ToolStripMenuItem_ObjectDisplay.DropDownItems.Add(TypeItem);

                            //  CompleteMenu.Items.Add(ToolStripMenuItem_ObjectDisplay);
                            CompleteMenu.Items.Add(ToolStripMenuItem_DescriptorDisplay);

                        }
                        else
                        {
                            cGeometric3DObject Obj = (cGeometric3DObject)this.Input.ListObject[i];


                            if ((Obj.ParentTag != null) && (Obj.ParentTag.GetType() == typeof(cListGeometric3DObject)))
                            {
                                CompleteMenu.Items.Add(((cListGeometric3DObject)Obj.ParentTag).GetContextMenu(Input));
                            }
                            //Obj.Tag;                        
                        }

                        if (this.Input.ListObject[i].GetType().BaseType.BaseType == typeof(cObject3D))
                        {
                            cObject3D TmpObj = (cObject3D)this.Input.ListObject[i];
                            if (TmpObj.Tag == null) continue;

                            if (TmpObj.Tag.GetType() == typeof(cWell))
                            {
                                cWell TmpWell = (cWell)(TmpObj.Tag);

                                foreach (var item in TmpWell.GetExtendedContextMenu())
                                    CompleteMenu.Items.Add(item);
                            }
                            else if (TmpObj.Tag.GetType() == typeof(cPlate))
                            {
                                cPlate TmpPlate = (cPlate)(TmpObj.Tag);

                                CompleteMenu.Items.Add(TmpPlate.GetExtendedContextMenu());


                            }
                            else if (TmpObj.Tag.GetType() == typeof(cDescriptorType))
                            {
                                cDescriptorType TmpDesc = (cDescriptorType)(TmpObj.Tag);

                                foreach (var item in TmpDesc.GetExtendedContextMenu())
                                    CompleteMenu.Items.Add(item);


                            }

                        }


                        break;
                    }

                }
            }

            CompleteMenu.Items.Add(ToolStripMenuItem_World);

            ToolStripMenuItem ToolStripMenuItem_refresh = new ToolStripMenuItem("Refresh");
            CompleteMenu.Items.Add(ToolStripMenuItem_refresh);
            ToolStripMenuItem_refresh.Click += new System.EventHandler(this.ToolStripMenuItem_refresh);
            
            CompleteMenu.Items.Add(ToolStripMenuItem_refresh);



            ToolStripMenuItem ToolStripMenuItem_CopyViewToClipBoard = new ToolStripMenuItem("Copy View To Clipboard");
            ToolStripMenuItem_CopyViewToClipBoard.ShowShortcutKeys = true;
            ToolStripMenuItem_CopyViewToClipBoard.ShortcutKeys = Keys.Control | Keys.C;
            CompleteMenu.Items.Add(ToolStripMenuItem_CopyViewToClipBoard);
            ToolStripMenuItem_CopyViewToClipBoard.Click += new System.EventHandler(this.ToolStripMenuItem_CopyViewToClipBoard);


            //ToolStripMenuItem ToolStripMenuItem_DisplayElevationMap = new ToolStripMenuItem("Display Elevation Map");
            //CompleteMenu.Items.Add(ToolStripMenuItem_DisplayElevationMap);
            //ToolStripMenuItem_DisplayElevationMap.Click += new System.EventHandler(this.DisplayElevationMap);


            //#region display options

            //ToolStripMenuItem ToolStripMenuItem_DisplayOptions = new ToolStripMenuItem("Display options");
            //CompleteMenu.Items.Add(ToolStripMenuItem_DisplayOptions);

            //this.ToolStripMenuItem_DisplayOptionsDispValues = new ToolStripMenuItem("Display values");
            //ToolStripMenuItem_DisplayOptionsDispValues.CheckOnClick = true;
            //ToolStripMenuItem_DisplayOptionsDispValues.Checked = this.IsDisplayValues;
            //ToolStripMenuItem_DisplayOptions.DropDownItems.Add(ToolStripMenuItem_DisplayOptionsDispValues);
            //ToolStripMenuItem_DisplayOptionsDispValues.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayOptionsDisValues);

            //#endregion


            CompleteMenu.Show(Control.MousePosition);
        }

        private void RenderWindow_KeyPressEvt(vtkObject sender, vtkObjectEventArgs e)
        {
            sbyte KeyCode = this.iren.GetKeyCode();

            int AltK = this.iren.GetAltKey();
            int CtrlK = this.iren.GetControlKey();

            //> >>        s = self.iren.GetShiftKey()
            //> >>        kc = self.iren.GetKeyCode()
            //> >>        ks = self.iren.GetKeySym()

            if (KeyCode == 103)
            {
                foreach (var item in this.Input.ListObject)
                {
                    if (item.ObjectType == "Geometrical")
                        item.vtk_Actor.GetProperty().SetOpacity(1);
                }
            }
            if (KeyCode == 98)
            {
                foreach (var item in this.Input.ListObject)
                {
                    if (item.ObjectType == "Geometrical")
                        item.vtk_Actor.GetProperty().SetOpacity(0);
                }
            }
            if (KeyCode == 104)
            {
                foreach (var item in this.Input.ListObject)
                {
                    if (item.ObjectType != "Geometrical")
                        item.vtk_Actor.GetProperty().SetOpacity(1);
                }
            }
            if (KeyCode == 110)
            {
                foreach (var item in this.Input.ListObject)
                {
                    if (item.ObjectType != "Geometrical")
                        item.vtk_Actor.GetProperty().SetOpacity(0);
                }
            }
            if (KeyCode == 106)
            {
                foreach (var item in this.Input.ListVolume)
                {
                    item.vtk_volume.VisibilityOn();
                }
            }
            if (KeyCode == 109)
            {
                foreach (var item in this.Input.ListVolume)
                {
                    item.vtk_volume.VisibilityOff();
                }
            }
            if (KeyCode == 45)
            {
                foreach (var item in this.Input.ListObject)
                {
                    if (item.ObjectType == "Geometrical") continue;
                    double CurrentOpacity = item.vtk_Actor.GetProperty().GetOpacity() - 0.01;
                    if (CurrentOpacity < 0.01) CurrentOpacity = 0.01;
                    item.vtk_Actor.GetProperty().SetOpacity(CurrentOpacity);
                }
                //renWin.Render();
            }

            if (KeyCode == 43)
            {
                foreach (var item in this.Input.ListObject)
                {
                    if (item.ObjectType == "Geometrical") continue;
                    double CurrentOpacity = item.vtk_Actor.GetProperty().GetOpacity() + 0.01;
                    if (CurrentOpacity > 1) CurrentOpacity = 1;
                    item.vtk_Actor.GetProperty().SetOpacity(CurrentOpacity);
                }
                //renWin.Render();
            }

            //if (KeyCode == 119)
            //{
            //    foreach (var item in this.Input.ListObject)
            //    {
            //        if (item.ObjectType == "Geometrical") continue;
            //        item.vtk_Actor.GetProperty().SetRepresentationToWireframe();
            //    }
            //}

            //if (KeyCode == 115)
            //{
            //    foreach (var item in this.Input.ListObject)
            //    {
            //        if (item.ObjectType == "Geometrical") continue;
            //        item.vtk_Actor.GetProperty().SetRepresentationToSurface();
            //    }
            //}

            //base.CurrentPanel.Refresh();
        }

        private void ToolStripMenuItem_refresh(object sender, EventArgs e)
        {
            this.Input.AssociatedVTKRenderer.ResetCamera();
            this.iren.Render();
        }

        private void ScaleClicking(object sender, EventArgs e)
        {
            if (this.legendScaleActor == null)
            {
                legendScaleActor = vtkLegendScaleActor.New();
                this.Input.AssociatedVTKRenderer.AddActor(legendScaleActor);
                this.legendScaleActor.SetVisibility(1);
            }
            else
            {

                if (this.legendScaleActor.GetVisibility() == 0)
                    this.legendScaleActor.SetVisibility(1);
                else
                    this.legendScaleActor.SetVisibility(0);
            }

            base.CurrentPanel.Refresh();
        }

        private void ToolStripMenuItem_ChangeBackgroundColor(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.OK) return;
            Color BackGroundColor = colorDialog.Color;

           this.Input.AssociatedVTKRenderer.SetBackground(BackGroundColor.R / 255.0, BackGroundColor.G / 255.0, BackGroundColor.B / 255.0);

            base.CurrentPanel.Refresh();

        }

        private void _ToolStripMenuItem_ActorsInfo(object sender, EventArgs e)
        {
            try
            {
                Input.AssociatedWindowFor3DTreeView.RefreshTree();
                Input.AssociatedWindowFor3DTreeView.Visible = ToolStripMenuItem_ActorsInfo.Checked;
            }
            catch (Exception)
            {
                return;
            }
                
        }

        private void ToolStripMenuItem_SaveToImage(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialogCurrScene = new SaveFileDialog();
            SaveFileDialogCurrScene.OverwritePrompt = true;
            SaveFileDialogCurrScene.AutoUpgradeEnabled = true;
            SaveFileDialogCurrScene.Filter = "TIFF files (*.tiff)|*.tiff";
            DialogResult result = SaveFileDialogCurrScene.ShowDialog();
            if (result != DialogResult.OK) return;

            string FileName = SaveFileDialogCurrScene.FileName;

            vtkWindowToImageFilter W2i = vtkWindowToImageFilter.New();
            W2i.SetInput(renderWindowControl1.RenderWindow);
            W2i.Update();

            vtkTIFFWriter TIFFWriter = vtkTIFFWriter.New();
            TIFFWriter.SetInputConnection(W2i.GetOutputPort());
            TIFFWriter.SetFileName(FileName);
            TIFFWriter.Write();

        }


        private void ToolStripMenuItem_CopyViewToClipBoard(object sender, EventArgs e)
        {
            vtkWindowToImageFilter W2i = vtkWindowToImageFilter.New();
            W2i.SetInput(renderWindowControl1.RenderWindow);
            W2i.Update();

            vtkImageData v = W2i.GetOutput(); 
            
            Bitmap BT = v.ToBitmap(); //Here I get Invalid Argument Exception

            if (BT == null) return;
            MemoryStream ms = new MemoryStream();
            Clipboard.SetImage(BT);
        }


        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            try
            {
                GenerateGraph();
                this.Input.AssociatedVTKRenderer.ResetCamera();
                this.iren.Render();
                
                //ToolStripMenuItem_refresh
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }

        //vtkRenderWindow RenderWindow;

        private void GenerateGraph()
        {
            // get a reference to the renderwindow of our renderWindowControl1
            vtkRenderWindow RenderWindow = renderWindowControl1.RenderWindow;


            //renWin = vtkRenderWindow.New();
            RenderWindow.LineSmoothingOn();
            RenderWindow.PointSmoothingOn();
            //  RenderWindow.SetWindowName("3D World");
            // RenderWindow.BordersOn();
            RenderWindow.DoubleBufferOn();
            //RenderWindow.SetSize(WinPos[0], WinPos[1]);

            //this.ren1 = vtkRenderer.New();

            // this.Input.renWin = RenderWindow;

            this.Input.AssociatedVTKRenderer = RenderWindow.GetRenderers().GetFirstRenderer();
            //Input.AssociatedVTKRenderer = this.Input.AssociatedVTKRenderer;
            Input.AssociatedrenderWindow = renderWindowControl1;

            foreach (var item in this.Input.ListObject)
            {
                item.AddMeToTheWorld(this.Input.AssociatedVTKRenderer);
            }

            foreach (var item in this.Input.ListVolume)
            {
                item.AddMeToTheWorld(this.Input.AssociatedVTKRenderer);
            }

            foreach (var item in this.Input.ListLights)
            {
                item.AddMeToTheWorld(this.Input.AssociatedVTKRenderer);
            }

            RenderWindow.AddRenderer(this.Input.AssociatedVTKRenderer);

            iren = new vtkRenderWindowInteractor();
            iren.SetRenderWindow(RenderWindow);

            // iren.SetInteractorStyle(vtkInteractorStyleJoystickCamera.New());
            iren.SetInteractorStyle(vtkInteractorStyleTrackballCamera.New());
           // iren.SetInteractorStyle(vtkInteractorStyleTrackballActor.New());
            iren.Start();
            //   iren.SetInteractorStyle(vtkInteractorStyleTerrain.New());


            // iren.LeftButtonPressEvt += new vtkObject.vtkObjectEventHandler(RenderWindow_LeftButtonPressEvt);
            iren.KeyPressEvt += new vtkObject.vtkObjectEventHandler(RenderWindow_KeyPressEvt);
            iren.RightButtonPressEvt += new vtkObject.vtkObjectEventHandler(RenderWindow_RightButtonPressEvt);
         
            this.Input.AssociatedVTKRenderer.SetBackground(this.Input.BackGroundColor.R / 255.0, this.Input.BackGroundColor.G / 255.0, this.Input.BackGroundColor.B / 255.0);

            this.Input.AssociatedVTKRenderer.SetActiveCamera(this.Input.Vtk_CameraView);

            // Setup offscreen rendering
            //vtkGraphicsFactory graphics_factory = vtkGraphicsFactory.New();
            //  graphics_factory.SetOffScreenOnlyMode( 1);
            //  graphics_factory.SetUseMesaClasses( 1 );

            //  vtkImagingFactory imaging_factory = vtkImagingFactory.New();
            //  imaging_factory.SetUseMesaClasses( 1 ); 


            // A renderer and render window
            // vtkRenderer renderer = vtkRenderer.New();
            //vtkRenderWindow renderWindow =
            //  vtkRenderWindow.New();
            //  RenderWindow.SetOffScreenRendering(1);
            //renderWindow.AddRenderer(renderer);


            RenderWindow.Render();

            //vtkWindowToImageFilter windowToImageFilter =
            //  vtkWindowToImageFilter.New();
            //windowToImageFilter.SetInput(RenderWindow);
            //windowToImageFilter.Update();

            //vtkPNGWriter writer =
            //  vtkPNGWriter.New();
            //writer.SetFileName("screenshot1.png");
            //writer.SetInputConnection(windowToImageFilter.GetOutputPort());
            //writer.Write();

            //Renderer.ResetCamera();

            // Input.Render();

        }
    }
}
