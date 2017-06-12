#region Version Header
/************************************************************************
 *		$Id: Window.cs, 177+1 2010/10/20 08:28:39 HongKee $
 *		$Description: Plugin Template for IM 3.0 $
 *		$Author: HongKee $
 *		$Date: 2010/10/20 08:28:39 $
 *		$Revision: 177+1 $
 /************************************************************************/
#endregion

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IM3_Plugin3;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Xml;
using System.Threading;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Forms.FormsForImages;
using ImageAnalysis;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.Detection;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine;
using HCSAnalyzer.Classes.Base_Classes.Viewers;




//////////////////////////////////////////////////////////////////////////
// If you want to change Menu & Name of plugin
// Go to "Properties->Resources" in Solution Explorer
// Change Menu & Name
//
// You can also use your own Painter & Mouse event handler
// 
//////////////////////////////////////////////////////////////////////////

namespace IM3_Plugin3
{
    public partial class Plugin3D : Form
    {
        public static DataGridView MainDataGridView;

        c3DNewWorld Current3DWorld;
        // static DataTable CurrentTable;
        FormForPostProcessings PostProcessWindow = new FormForPostProcessings();
        //List<FormForControl> ListFormForControl = new List<FormForControl>();
        Stack<FormForControl> ListFormForControl = new Stack<FormForControl>();
        //List<Control> ListCtrl = new List<Control>();
        Stack<Control> ListCtrl = new Stack<Control>();

        cListInfoObject3D ListInfoObj3D = new cListInfoObject3D();
        static FormForDataGridView DataGrid = null;
        static DataTable CurrentTable;
        public cImage AssociatedImage = null;

      //  Image ImageToDisp = new Bitmap(@"C:\Workspace\IM3\IM3 Plugin3\bin\Background.png");


        ///// <summary>
        ///// Optional method to override. Init() is called before the loop of Process() call
        ///// </summary>
        //public override void Init()
        //{

        //}

        /// <summary>
        /// Process() is called in a loop when the user start a process.
        /// </summary>
        /// <param name="experiment"></param>
        //public override void Process(Experiment experiment)
        //{
        //    // Use Experiment as you wish!
        //    //Experiment e = experiment;
        //    for (int i = 0; i < experiment.Sequences;i++)
        //    {
        //        Sequence SeqToProcess = experiment.SequenceList[i];
        //        AnalyseSequence(experiment, SeqToProcess);

        //    }

        //}

        ///// <summary>
        ///// Optional method to override. Conclude() is called after the loop of Process() call
        ///// </summary>
        //public override void Conclude()
        //{

        //}

        public Plugin3D(cImage AssociatedImage)
        {
            InitializeComponent();
            this.AssociatedImage = AssociatedImage;
            Show();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //panelFor3DControls.Controls.Clear();
            ////UserControl1 animationLayer = new UserControl1();

            //WPFForLUT LUT = new WPFForLUT();

            ///* container that will host our WPF control, we set it using 
            // * the Child property */
            //ElementHost host = new ElementHost()
            //{
            //    BackColor = Color.Transparent,
            //    Child = LUT,
            //    Dock = DockStyle.Fill,
            //};
            //panelFor3DControls.Controls.Add(host);

        }

        public enum eColorMode
        {
            Regular = 0,
            Random = 1,
            Indexed = 2,
        }
      

        private void buttonProcessImage_Click(object sender, EventArgs e)
        {
           Form  FormTmpform = Form.ActiveForm;
            //IntPtr handle = GetActiveWindow();

            //Sequence SeqToAnalyse = IMGlobal.CurrentSequence;




            FormForInfoBeforeStarting WindowForInfo = new FormForInfoBeforeStarting();
            WindowForInfo.numericUpDownResolutionX.Value = (decimal)this.AssociatedImage.Resolution.X;
            WindowForInfo.numericUpDownResolutionY.Value = (decimal)this.AssociatedImage.Resolution.Y;
            WindowForInfo.numericUpDownResolutionZ.Value = (decimal)this.AssociatedImage.Resolution.Z;

            if (WindowForInfo.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            this.AssociatedImage.Resolution.X = (double)WindowForInfo.numericUpDownResolutionX.Value;
            this.AssociatedImage.Resolution.Y = (double)WindowForInfo.numericUpDownResolutionY.Value;
            this.AssociatedImage.Resolution.Z = (double)WindowForInfo.numericUpDownResolutionZ.Value;

            AnalyseSequence(this.AssociatedImage);
        }

        private void AnalyseSequence(cImage SeqToAnalyse)
        {
        //    //FormForThumbnails WindThumbnail = new FormForThumbnails();

        //    //for (int k = 0; k < ListFormForControl.Count; k++)
        //    //{
        //    //        FormForControl Obj = ListFormForControl.ElementAt(ListFormForControl.Count - k-1);
        //    //        Obj.DisplayThumbnail(WindThumbnail);
        //    //}
        //    //WindThumbnail.Show();

        //    //return;


            #region initialization

            if (SeqToAnalyse == null) return;


            // create World
            if (Current3DWorld == null)
            {
                Current3DWorld = new c3DNewWorld(new cPoint3D(SeqToAnalyse.Width, SeqToAnalyse.Height, SeqToAnalyse.Depth),
                                                 new cPoint3D(SeqToAnalyse.Resolution));
            }
          else
            {
        //     //   Current3DWorld.ren1.
              //  Current3DWorld.Terminate();
                Current3DWorld = null;
                 Current3DWorld = new c3DNewWorld(new cPoint3D(SeqToAnalyse.Width, SeqToAnalyse.Height, SeqToAnalyse.Depth),
                                                         new cPoint3D(SeqToAnalyse.Resolution));
        
            }

            for (int k = 0; k < ListFormForControl.Count; k++)
            {
                FormForControl Obj = ListFormForControl.ElementAt(ListFormForControl.Count - k-1);
                if (Obj.numericUpDownChannel.Value >= SeqToAnalyse.GetNumChannels())
                {
                    MessageBox.Show("Wrong number of channels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        //   // Current3DWorld.ListObjControl = ListFormForControl;
        //    //   dataGridViewForClassif.DataSource = CurrentTable;
        //    if (DataGrid == null)
        //    {
        //        DataGrid = new FormForDataGridView();
        //        DataGrid.Show();
        //    }
        //    Current3DWorld.SetLinkToDataGridView(DataGrid.dataGridViewForResults);

            Current3DWorld.ListMetaObjectList = new List<cMetaBiologicalObjectList>();
            cMetaBiologicalObjectList ListMetacells = new cMetaBiologicalObjectList("List Meta Objects");
            Current3DWorld.ListMetaObjectList.Add(ListMetacells);
            #endregion

            #region Detection
            for (int k = 0; k < ListFormForControl.Count; k++)
            {
                FormForControl Obj = ListFormForControl.ElementAt(ListFormForControl.Count - k -1);

                if (Obj.radioButtonIsVolume.Checked)    // we have to detect a volume !
                {
                    cVolumeDetection Detection = new cVolumeDetection(SeqToAnalyse, (int)Obj.numericUpDownChannel.Value, Obj.WindowForVolumeDetection.checkBoxIsBorderKill.Checked);
                    Detection.SetShift(new cPoint3D((float)Obj.WindowForPreProcessing.numericUpDownShiftX.Value, (float)Obj.WindowForPreProcessing.numericUpDownShiftY.Value, (float)Obj.WindowForPreProcessing.numericUpDownShiftZ.Value));

                    if ((Obj.checkBoxPreProcessings.Checked == false) || (Obj.WindowForPreProcessing.checkBoxMedianIsDisabled.Checked))
                    {
                        Detection.SetMedian(-1);
                        Detection.SetShift(new cPoint3D(0, 0, 0));
                    }
                    else
                    {
                        Detection.SetMedian((int)Obj.WindowForPreProcessing.numericUpDownMedianKernel.Value);
                        Detection.SetShift(new cPoint3D((float)Obj.WindowForPreProcessing.numericUpDownShiftX.Value, (float)Obj.WindowForPreProcessing.numericUpDownShiftY.Value, (float)Obj.WindowForPreProcessing.numericUpDownShiftZ.Value));
                    }


                    if (Obj.WindowForVolumeDetection.checkBoxVolumeSmooth.Checked)
                        Detection.MeshSmoother = new cMeshSmoother((int)Obj.WindowForVolumeDetection.numericUpDownSmoothIterations.Value);

                    if ((Obj.comboBoxContainer.SelectedItem !=null) && (Obj.comboBoxContainer.SelectedItem.ToString() != ""))
                    {
                        Detection.SetContainers(Current3DWorld.GetListBiologicalObjectsOfType(Obj.comboBoxContainer.Items[0].ToString()));
                        if (Obj.InOrOut.radioButtonIn.Checked) Detection.SetContainersMode(0);
                        else Detection.SetContainersMode(1);
                    }
                    else
                        Detection.SetContainers(null);


                    if (Obj.WindowForVolumeDetection.checkBoxIsRegionGrowing.Checked == false)  // no region growing direct detection
                    {
                        Obj.AssociatedBiologicalObjectList = Detection.IntensityThreshold((float)Obj.WindowForVolumeDetection.numericUpDownIntensityThreshold.Value,
                                                                                          (float)Obj.WindowForVolumeDetection.numericUpDownVolumeMinVol.Value, 
                                                                                          (float)Obj.WindowForVolumeDetection.numericUpDownVolumeMaxVol.Value);
                    }
                   else // region growing
                   {
                        if (Obj.WindowForVolumeDetection.radioButtonRegionsBased.Checked) // start from a segmentation
                        {
                            if (Obj.WindowForVolumeDetection.radioButtonVolumeDetectionNew.Checked) // a new segmentation has to be performed
                            {
        //                        // Image3D CurrentPatch = new Image3D(2 * Radius, 2 * Radius, 1, CurrentImageToProcess.NumBands);
                                cImage CurrentBinary = new cImage(SeqToAnalyse.Width, SeqToAnalyse.Height, SeqToAnalyse.Depth, 1);

                                for (int i = 0; i < CurrentBinary.ImageSize; i++)
                                {
                                    if (SeqToAnalyse.SingleChannelImage[(int)Obj.numericUpDownChannel.Value].Data[i] >= (float)Obj.WindowForVolumeDetection.numericUpDownIntensityThreshold.Value)
                                        CurrentBinary.SingleChannelImage[0].Data[i] = 1;
                                }

                                eConnectivity CurrentConnectivity = eConnectivity.TWOD_4;
                                if (CurrentBinary.Depth > 1)
                                    CurrentConnectivity = eConnectivity.THREED_6;

                                ConnectedComponentSet OFNucleus = new ConnectedComponentSet(CurrentBinary, null, null, 0, CurrentConnectivity, (int)Obj.WindowForVolumeDetection.numericUpDownRegionBasedMinArea.Value, (int)Obj.WindowForVolumeDetection.numericUpDownRegionBasedMaxArea.Value);
        //                      //  RegionGrowing = new cRegionGrowing(OFNucleus, NewSeq[0], (int)numericUpDownChannelForRegionGrowing.Value, (float)numericUpDownIntensityForRegionGrowing.Value, (int)numericUpDownMaxNucleusArea.Value);
                                int NumIterations = -1;
                                if (Obj.WindowForVolumeDetection.checkBoxIsConvergence.Checked == false) NumIterations = (int)Obj.WindowForVolumeDetection.numericUpDownIterationNumber.Value;

                                float MergingRatio = (float)((Obj.WindowForVolumeDetection.trackBarMergingStrength.Maximum - Obj.WindowForVolumeDetection.trackBarMergingStrength.Value) / 100.0);
                                Obj.AssociatedBiologicalObjectList = Detection.RegionGrowing(OFNucleus,
                                                                                            (float)Obj.WindowForVolumeDetection.numericUpDownIntensityForRegionGrowing.Value,
                                                                                            (float)Obj.WindowForVolumeDetection.numericUpDownVolumeMinVol.Value,
                                                                                            (float)Obj.WindowForVolumeDetection.numericUpDownVolumeMaxVol.Value, NumIterations, MergingRatio);

                            }
        //                    else
        //                    {
        //                        throw new System.ArgumentException("Not implemented", "Error");
        //                    }
        //                }
        //                else // start from seeds points
        //                {

        //                        // first gather the seeds
        //                        if (Obj.WindowForVolumeDetection.comboBoxExistingSpotsDetected.Items.Count == 0) break;
        //                       List<cBiological3DObject> ListSeeds = Current3DWorld.GetListBiologicalObjectsOfType(Obj.WindowForVolumeDetection.comboBoxExistingSpotsDetected.Items[0].ToString());

        //                       int NumIterations = -1;
        //                       if (Obj.WindowForVolumeDetection.checkBoxIsConvergence.Checked == false) NumIterations = (int)Obj.WindowForVolumeDetection.numericUpDownIterationNumber.Value;

        //                       float MergingRatio = (float)((Obj.WindowForVolumeDetection.trackBarMergingStrength.Maximum - Obj.WindowForVolumeDetection.trackBarMergingStrength.Value) / 100.0);
        //                       Obj.AssociatedBiologicalObjectList = Detection.RegionGrowing(ListSeeds,
        //                                                                                   (float)Obj.WindowForVolumeDetection.numericUpDownIntensityForRegionGrowing.Value, 
        //                                                                                   (float)Obj.WindowForVolumeDetection.numericUpDownVolumeMinVol.Value,
        //                                                                                   (float)Obj.WindowForVolumeDetection.numericUpDownVolumeMaxVol.Value, NumIterations, MergingRatio);
                        }
                    }
                    if (Obj.AssociatedBiologicalObjectList != null)
                    {
                        Obj.richTextBoxInfo.AppendText(Obj.AssociatedBiologicalObjectList.Count + " objects detected.");

                        for (int i = 0; i < Obj.AssociatedBiologicalObjectList.Count; i++)
                        {
                            cBiological3DVolume TmpVol = (cBiological3DVolume)Obj.AssociatedBiologicalObjectList[i];
                            Obj.AssociatedBiologicalObjectList[i].SetType(Obj.textBoxName.Text);
        //                    TmpVol.ThumbnailnewImage = imageListForThumbnail.Images[Obj.AssociatedBiologicalObjectList[i].GetType() + ".jpg"];
                            Obj.AssociatedBiologicalObjectList[i].Name = Obj.textBoxName.Text + " " + i;
          //                  Obj.AssociatedBiologicalObjectList[i].IsMaster = Obj.IsMasterObject();
        //                    List<double> Res = TmpVol.Information.GetInformation();

                            if (Obj.checkBoxIsDisplayName.Checked)
                            {
                                cGeometric3DObject AssociatedText = TmpVol.AttachText(TmpVol.Name, 10, Color.White);
                                AssociatedText.IsStayInFrontOfCamera = true;
                                Current3DWorld.AddGeometric3DObject(AssociatedText);

                                //                        TmpVol.AddText(TmpVol.Name, Current3DWorld, (double)Obj.ForText.numericUpDownArrowScale.Value, Obj.ForText.buttonChangeColorPositive.BackColor);
                            }

                            if (Obj.checkIsBoxPointingArrow.Checked)
                            {
                                cGeometric3DObject AssociatedArrow = TmpVol.AttachPointingArrow((double)Obj.ForArrow.numericUpDownArrowScale.Value,
                                                            Obj.ForArrow.buttonChangeColorPositive.BackColor);

                                Current3DWorld.AddGeometric3DObject(AssociatedArrow);

                            }

                            Obj.AssociatedBiologicalObjectList[i].SetOpacity((double)Obj.ColorOpacity.numericUpDownValue.Value);

                            // if the object it the Master then create a meta-object and put it inside
                            if (Obj.IsMasterObject())
                            {
                                cMetaBiologicalObject Cell = new cMetaBiologicalObject(Obj.WindowForMaster.textBoxName.Text + i, Current3DWorld.ListMetaObjectList[0], Obj.AssociatedBiologicalObjectList[i]);
        //                        // Cell.AddObject(Obj.AssociatedBiologicalObjectList[i]);
                                Current3DWorld.ListMetaObjectList[0].Add(Cell);
                            }
                        }

                        if (Obj.radioButtonColorSingle.Checked)
                            Current3DWorld.AddBiological3DObjects(Obj.AssociatedBiologicalObjectList, Obj.buttonChangeColorPositive.BackColor, eColorMode.Regular);
                        else if (Obj.radioButtonColorRandom.Checked)
                            Current3DWorld.AddBiological3DObjects(Obj.AssociatedBiologicalObjectList, Obj.buttonChangeColorPositive.BackColor, eColorMode.Random);
                        else if (Obj.radioButtonColorIndexed.Checked)
                            Current3DWorld.AddBiological3DObjects(Obj.AssociatedBiologicalObjectList, Obj.buttonChangeColorPositive.BackColor, eColorMode.Indexed);
                    }

                }
                else if (Obj.RadioButtonIsSpot.Checked)
                {
                    cSpotDetection Detection = new cSpotDetection(SeqToAnalyse, (int)Obj.numericUpDownChannel.Value);

                    if ((Obj.checkBoxPreProcessings.Checked == false) || (Obj.WindowForPreProcessing.checkBoxMedianIsDisabled.Checked))
                    {
                        Detection.SetMedian(-1);
                        Detection.SetShift(new cPoint3D(0, 0, 0));
                    }
                    else
                    {
                        Detection.SetMedian((int)Obj.WindowForPreProcessing.numericUpDownMedianKernel.Value);
                        Detection.SetShift(new cPoint3D((float)Obj.WindowForPreProcessing.numericUpDownShiftX.Value, (float)Obj.WindowForPreProcessing.numericUpDownShiftY.Value, (float)Obj.WindowForPreProcessing.numericUpDownShiftZ.Value));
                    }


                    if ((Obj.comboBoxContainer.Items.Count > 0) && (Obj.comboBoxContainer.SelectedItem != null))
                    {
                        Detection.SetContainers(Current3DWorld.GetListBiologicalObjectsOfType(Obj.comboBoxContainer.SelectedItem.ToString()));
                        if (Obj.InOrOut.radioButtonIn.Checked) Detection.SetContainersMode(0);
                        else Detection.SetContainersMode(1);
                    }

                    Obj.AssociatedBiologicalObjectList = Detection.HessianDetection((float)Obj.WindowForSpotDetection.numericUpDownSpotRadius.Value, 
                                                                                    (float)Obj.WindowForSpotDetection.numericUpDownIntensityThreshold.Value, 
                                                                                    (int)Obj.WindowForSpotDetection.numericUpDownSpotLocality.Value, 
                                                                                    (double)Obj.WindowForSpotDetection.numericUpDownSphereDisplayRadius.Value);

                    Obj.richTextBoxInfo.AppendText(Obj.AssociatedBiologicalObjectList.Count + " objects detected.");
                    for (int i = 0; i < Obj.AssociatedBiologicalObjectList.Count; i++)
                    {
                        cBiologicalSpot TmpSpot = (cBiologicalSpot)Obj.AssociatedBiologicalObjectList[i];
                        TmpSpot.Name = Obj.textBoxName.Text + " " + i;
                        TmpSpot.SetType(Obj.textBoxName.Text);
        //                TmpSpot.ThumbnailnewImage = imageListForThumbnail.Images[Obj.AssociatedBiologicalObjectList[i].GetType() + ".jpg"];

        //                if (Obj.checkBoxIsDisplayName.Checked)
        //                    TmpSpot.AddText(TmpSpot.Name, Current3DWorld, (double)Obj.ForText.numericUpDownArrowScale.Value, Obj.ForText.buttonChangeColorPositive.BackColor);

                            if (Obj.checkIsBoxPointingArrow.Checked)
                            {
                                cGeometric3DObject AssociatedArrow = TmpSpot.AttachPointingArrow((double)Obj.ForArrow.numericUpDownArrowScale.Value, Obj.ForArrow.buttonChangeColorPositive.BackColor);
                                Current3DWorld.AddGeometric3DObject(AssociatedArrow);
                            }

        //                TmpSpot.SetOpacity((double)Obj.ColorOpacity.numericUpDownValue.Value);


        //                // if the object it the Master then create a meta-object and put it inside
                        if (Obj.IsMasterObject())
                        {
                            cMetaBiologicalObject Cell = new cMetaBiologicalObject("Cell " + i, Current3DWorld.ListMetaObjectList[0], Obj.AssociatedBiologicalObjectList[i]);
        //                 //   Cell.AddObject(Obj.AssociatedBiologicalObjectList[i]);
                            Current3DWorld.ListMetaObjectList[0].Add(Cell);
                        }

        //                /*cMetaBiologicalObject Cell = */
        //                //Current3DWorld.ListMetaObjectList[0].AssociateWith(Current3DWorld.ListMetaObjectList[0].FindTheClosestVolumeFrom(TmpSpot.GetCentroid(), 20), TmpSpot);
                    }


        //            if (Obj.radioButtonColorSingle.Checked)
        //                Current3DWorld.AddBiological3DObjects(Obj.AssociatedBiologicalObjectList, Obj.buttonChangeColorPositive.BackColor, eColorMode.Regular);
        //            else if (Obj.radioButtonColorRandom.Checked)
        //                Current3DWorld.AddBiological3DObjects(Obj.AssociatedBiologicalObjectList, Obj.buttonChangeColorPositive.BackColor, eColorMode.Random);
        //            else if (Obj.radioButtonColorIndexed.Checked)
        //                Current3DWorld.AddBiological3DObjects(Obj.AssociatedBiologicalObjectList, Obj.buttonChangeColorPositive.BackColor, eColorMode.Indexed);
                    

                }
                else if (Obj.radioButtonIsVolumeRendering.Checked)
                {
                    Current3DWorld.AddVolume3D(new cVolumeRendering3D(this.AssociatedImage.SingleChannelImage[(int)Obj.numericUpDownChannel.Value],
                                               new cPoint3D((float)Obj.WindowForPreProcessing.numericUpDownShiftX.Value, (float)Obj.WindowForPreProcessing.numericUpDownShiftY.Value, (float)Obj.WindowForPreProcessing.numericUpDownShiftZ.Value),
                                               null, Current3DWorld));

                  //  cVolume3D Volume = new cVolume3D(SeqToAnalyse, (int)Obj.numericUpDownChannel.Value, new cPoint3D((float)Obj.WindowForPreProcessing.numericUpDownShiftX.Value, (float)Obj.WindowForPreProcessing.numericUpDownShiftY.Value, (float)Obj.WindowForPreProcessing.numericUpDownShiftZ.Value));
                  //  Current3DWorld.AddVolume3D(Volume, Color.Black, Obj.buttonChangeColorPositive.BackColor);
                }

        //        Thread oThread = new Thread(new ThreadStart(Obj.DisplayDone));


            }
            #endregion

            #region Objects Association
            cMetaBiologicalObjectList MetaCellList = Current3DWorld.ListMetaObjectList[0];

            // let's associate the objects
            for (int k = 0; k < ListFormForControl.Count; k++)
            {
                FormForControl Obj = ListFormForControl.ElementAt(ListFormForControl.Count - k -1);

                // that's a volume rendering ... no connection
                if (Obj.radioButtonIsVolumeRendering.Checked) continue;

                // the object is the master
                if (Obj.IsMasterObject()) continue;

                // this object has not connections ... is not associated to the master
                if (Obj.comboBoxLinkToTheMaster.SelectedIndex == 0) continue;

                cPoint3D ClosestPt = new cPoint3D(0, 0, 0);

                foreach (cBiological3DVolume CurrentSubObj in Obj.AssociatedBiologicalObjectList)
                {
                    cBiological3DVolume ObjectToIdentify = null;
                    switch (Obj.comboBoxLinkToTheMaster.SelectedIndex)
                    {
                        case 1:         // association by distance to the master centroid
                            ObjectToIdentify = MetaCellList.FindTheClosestVolumeCentroidFrom(CurrentSubObj.GetCentroid(), (double)Obj.WindowDistanceToMaster.numericUpDownDistanceMaxToMaster.Value);
                            break;
                        case 2:
        //                    //cBiological3DVolume CurrentVolume
                            ObjectToIdentify = MetaCellList.FindTheClosestVolumeFrom(CurrentSubObj, (double)Obj.WindowDistanceToMaster.numericUpDownDistanceMaxToMaster.Value, out ClosestPt);
                            break;
                        default:
                            break;
                   }

                    if (ObjectToIdentify == null) continue;
                    cMetaBiologicalObject Cell = MetaCellList.AssociateWith(ObjectToIdentify, CurrentSubObj);

        //            // --------------  draw information and links between the objects ---------------------
                    if (Obj.WindowDistanceToMaster.checkBoxDrawLinkToMasterCenter.Checked)
                    {
                        c3DLine CurrLine = new c3DLine(CurrentSubObj.GetCentroid(), ObjectToIdentify.GetCentroid());
                        Current3DWorld.AddGeometric3DObject(CurrLine);

                        //if (Obj.WindowDistanceToMaster.checkBoxDisplayBranchToCenterDistance.Checked)
                        //    CurrLine.DisplayLenght(Current3DWorld, 0.4);
                    }

        //            // --------------  draw information and links between the objects ---------------------
                    if (Obj.WindowDistanceToMaster.checkBoxDrawLinkToMasterEdges.Checked)
                    {
                        c3DLine CurrLine1 = new c3DLine(CurrentSubObj.GetCentroid(), ClosestPt);
                        
                        Current3DWorld.AddGeometric3DObject(CurrLine1);

                        //if(Obj.WindowDistanceToMaster.checkBoxDisplayBranchToEdgesDistance.Checked)
                        //    CurrLine1.DisplayLenght(Current3DWorld, 0.4);
                    }
                }
            }
            #endregion

            #region Post Processings
            for (int k = 0; k < ListFormForControl.Count; k++)
            {
                FormForControl Obj = ListFormForControl.ElementAt(ListFormForControl.Count - k - 1);
                // the object is the master
        //      //  if (!Obj.IsMasterObject()) continue;

                if (!Obj.WindowForMaster.checkBoxDrawAssociatedDelaunay.Checked) continue;
                foreach (cMetaBiologicalObject CurrentMeta in MetaCellList)
                {
                    Current3DWorld.AddGeometric3DObject(CurrentMeta.GenerateDelaunay(2.0f,true));
                }
            }

            if (PostProcessWindow.checkBoxRemoveUnAssociatedObjects.Checked)
            {
                int RemovedObjects = Current3DWorld.RemoveNonAssociatedObjects();
                Console.WriteLine(RemovedObjects + " objects removed.");
            }


        //    if (PostProcessWindow.checkBoxExportMetaObjectSignatures.Checked)
        //    {
        //        if (CurrentTable == null)
        //        {
        //            CurrentTable = new DataTable();

        //            if (CurrentExperiment == null)
        //                CurrentTable.Columns.Add(new DataColumn("Image Idx", typeof(int)));
        //            else
        //            {
        //                CurrentTable.Columns.Add(new DataColumn("Image Col.", typeof(int)));
        //                CurrentTable.Columns.Add(new DataColumn("Image Row", typeof(int)));
        //            }

        //            CurrentTable.Columns.Add(new DataColumn("Meta Object Name", typeof(string)));

        //            foreach (string DescName in MetaCellList[0].GetSignatureNames())
        //                CurrentTable.Columns.Add(new DataColumn(DescName, typeof(double)));

        //            CurrentTable.Columns.Add(new DataColumn("Class", typeof(double)));
        //        }

        //        foreach (cMetaBiologicalObject CurrentMeta in MetaCellList)
        //        {
        //            List<double> CurrentSignature = CurrentMeta.GetSignature();

        //            CurrentTable.Rows.Add();
        //            if (CurrentExperiment == null)
        //            {
        //                CurrentTable.Rows[CurrentTable.Rows.Count - 1][0] = IdxImageProcessed;
        //                CurrentTable.Rows[CurrentTable.Rows.Count - 1][1] = CurrentMeta.Name;

        //                for (int Idx = 0; Idx < CurrentSignature.Count; Idx++)
        //                    CurrentTable.Rows[CurrentTable.Rows.Count - 1][Idx + 2] = CurrentSignature[Idx];
        //            }
        //            else
        //            {
        //                CurrentTable.Rows[CurrentTable.Rows.Count - 1][0] = CurrentExperiment.Column;
        //                CurrentTable.Rows[CurrentTable.Rows.Count - 1][1] = CurrentExperiment.Row;
        //                CurrentTable.Rows[CurrentTable.Rows.Count - 1][2] = CurrentMeta.Name;

        //                for (int Idx = 0; Idx < CurrentSignature.Count; Idx++)
        //                    CurrentTable.Rows[CurrentTable.Rows.Count - 1][Idx + 3] = CurrentSignature[Idx];
        //            }

        //            // Current3DWorld.CopyMetaObjectSignatureToTable(CurrentMeta, 0);
        //        }
        //        if (DataGrid != null)
        //        {
        //            DataGrid.dataGridViewForResults.DataSource = CurrentTable;
        //            DataGrid.Update();
        //            IdxImageProcessed++;
        //        }
        //    }

            #endregion

        //    //CurrentTable = new DataTable();
        //    if (CurrentExperiment == null)
        //    {
        //        if (PostProcessWindow.checkBoxDisplayBottomPlate.Checked) Current3DWorld.DisplayBottom(Color.FromArgb(255, 255, 255));
        //        Current3DWorld.SetBackgroundColor(PostProcessWindow.buttonChangeColorPositive.BackColor);
        //        Current3DWorld.Render();
        //    }
        //    // Current3DWorld.SetLinkToDataGridView(dataGridViewForClassif);



            cViewer3D V3D = new cViewer3D();
            V3D.SetInputData(Current3DWorld);
            V3D.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(V3D.GetOutPut());
            DTW.Run();
            DTW.Display();

        }

        static int IdxImageProcessed=0;

        public void UpDateContainers()
        {
            for (int i = 1; i < ListFormForControl.Count; i++)
            {
                List<String> ListContainers = new List<string>();
                for (int j = 0; j < i; j++)
                {
                    if (ListFormForControl.ElementAt(ListFormForControl.Count - j -1).radioButtonIsVolume.Checked)
                        ListContainers.Add(ListFormForControl.ElementAt(ListFormForControl.Count - j - 1).textBoxName.Text);
                }
                ListFormForControl.ElementAt(ListFormForControl.Count - i -1).UpdateContainers(ListContainers);
            }
        }

        public void UpDateSpotList()
        {
           // this.ListDetectedSpots = new List<string>();

            for (int i = 1; i < ListFormForControl.Count; i++)
            {
                List<String> ListSpotsAvalaible = new List<string>();
                for (int j = 0; j < i; j++)
                {
                    if (ListFormForControl.ElementAt(ListFormForControl.Count - j - 1).RadioButtonIsSpot.Checked)
                        ListSpotsAvalaible.Add(ListFormForControl.ElementAt(ListFormForControl.Count - j -1).textBoxName.Text);
                }
                ListFormForControl.ElementAt(ListFormForControl.Count - i - 1).ListDetectedSpots.Clear();
                ListFormForControl.ElementAt(ListFormForControl.Count - i - 1).ListDetectedSpots.AddRange(ListSpotsAvalaible);
            }
        }

        public void UpDateMasters(FormForControl SenderCtrl)
        {
            foreach (FormForControl CurrentFormForCtrl in ListFormForControl)
            {
                if (CurrentFormForCtrl == SenderCtrl) continue;
                if (CurrentFormForCtrl.radioButtonIsVolumeRendering.Checked) continue;
                CurrentFormForCtrl.SetAsNonMaster();
            }
        }

        private void postProcessingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PostProcessWindow.Show();
        }

        private void panelFor3DControls_MouseDown(object sender, MouseEventArgs e)
        {
            contextMenuStripFor3D.Show(Control.MousePosition);
        }

        FinalGlobalStatus GlobalStatus;

        private void saveConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalStatus = new FinalGlobalStatus();

            for(int IdxCtrl=0;IdxCtrl<ListFormForControl.Count;IdxCtrl++)
            {

                GlobalStatus.GlobalStatus.Add(ListFormForControl.ElementAt(ListFormForControl.Count - IdxCtrl-1).GenerateCurrentStatus());
            }

            SaveFileDialog CurrSaveFileDialog = new SaveFileDialog();
            CurrSaveFileDialog.Filter = "binary files (*.xml)|*.xml";
            System.Windows.Forms.DialogResult Res = CurrSaveFileDialog.ShowDialog();
            if (Res != System.Windows.Forms.DialogResult.OK) return;
            string PathName = CurrSaveFileDialog.FileName;

            GlobalStatus.Save(PathName);
        }

        private void loadConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog CurrOpenFileDialog = new OpenFileDialog();
            CurrOpenFileDialog.Filter = "binary files (*.xml)|*.xml";
            System.Windows.Forms.DialogResult Res = CurrOpenFileDialog.ShowDialog();
            if (Res != System.Windows.Forms.DialogResult.OK) return;
            string fileName = CurrOpenFileDialog.FileName;

            GlobalStatus = new FinalGlobalStatus();
           GlobalStatus = GlobalStatus.Load(fileName);

            ListFormForControl.Clear();
            ListCtrl.Clear();
            panelFor3DControls.Controls.Clear();

            for (int IdxNewObj = 0; IdxNewObj < GlobalStatus.GlobalStatus.Count; IdxNewObj++)
            {
                FormForControl CurrentFormFor3DObject = new FormForControl(this, GlobalStatus.GlobalStatus[IdxNewObj]);

                CurrentFormFor3DObject.ImList = imageListForThumbnail;
                CurrentFormFor3DObject.panelGroup.Location = new Point(0, IdxNewObj * CurrentFormFor3DObject.panelGroup.Height /*- panelFor3DControls.VerticalScroll.Value*/);
                CurrentFormFor3DObject.panelGroup.Text = "Object " + IdxNewObj;
                CurrentFormFor3DObject.textBoxName.Text = "Object " + IdxNewObj;

               // CurrentFormFor3DObject.panelGroup.BackgroundImage = ImageToDisp;
                CurrentFormFor3DObject.panelGroup.BackgroundImageLayout = ImageLayout.None;
                CurrentFormFor3DObject.Parent = this;

                ListFormForControl.Push(CurrentFormFor3DObject);
                ListCtrl.Push(CurrentFormFor3DObject.panelGroup);
            }   
            
            
            panelFor3DControls.Controls.AddRange(ListCtrl.ToArray());
            textBoxNumberOfObjects.Text = panelFor3DControls.Controls.Count.ToString();
            UpDateContainers();
            UpDateSpotList();
        }

        private void buttonAddObject_Click(object sender, EventArgs e)
        {
            int NewIDx = (int)(int.Parse(textBoxNumberOfObjects.Text) + 1);

            FormForControl CurrentFormFor3DObject = new FormForControl(this,new cPoint3D(50*NewIDx,50,0));

            CurrentFormFor3DObject.ImList = imageListForThumbnail;
            CurrentFormFor3DObject.panelGroup.Location = new Point(0, (NewIDx - 1) * CurrentFormFor3DObject.panelGroup.Height - panelFor3DControls.VerticalScroll.Value);
            CurrentFormFor3DObject.panelGroup.Text = "Object " + NewIDx;
            CurrentFormFor3DObject.textBoxName.Text = "Object " + NewIDx;

          //  CurrentFormFor3DObject.panelGroup.BackgroundImage = ImageToDisp;
            CurrentFormFor3DObject.panelGroup.BackgroundImageLayout = ImageLayout.None;
            CurrentFormFor3DObject.Parent = this;

            ListFormForControl.Push(CurrentFormFor3DObject);
            ListCtrl.Push(CurrentFormFor3DObject.panelGroup);
            panelFor3DControls.Controls.AddRange(ListCtrl.ToArray());
            textBoxNumberOfObjects.Text = panelFor3DControls.Controls.Count.ToString();
            UpDateContainers();
            UpDateSpotList();
            

        }

        private void buttonRemoveObject_Click(object sender, EventArgs e)
        {
            if (panelFor3DControls.Controls.Count == 0) return;
            panelFor3DControls.Controls.Remove(ListCtrl.Pop());

            ListFormForControl.Pop();
            textBoxNumberOfObjects.Text = panelFor3DControls.Controls.Count.ToString();
            UpDateContainers();
            UpDateSpotList();
        }

        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    FormForLUT ForLUT = new FormForLUT();

        //    ForLUT.Controls.Clear();
        //   // UserControl1 animationLayer = new UserControl1();


        //    WPFForLUT LUT = new WPFForLUT();

        //    /* container that will host our WPF control, we set it using 
        //     * the Child property */
        //    ElementHost host = new ElementHost()
        //    {
        //        BackColor = Color.Transparent,
        //        Child = LUT,
        //        Dock = DockStyle.Fill,
        //    };
        //    ForLUT.Controls.Add(host);
        //    ForLUT.ShowDialog();

        //}
    }

    /// <summary>
    /// If you want keep your version information,
    /// Put your version information in the AssemblyInfo.cs file
    /// [assembly: AssemblyVersion("1.0.*")]
    /// [assembly: AssemblyFileVersion("1.0.0.0")]
    /// </summary>
    //public static class PluginVersion
    //{
    //    public static string Info
    //    {
    //        get
    //        {
    //            System.Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
    //            bool isDaylightSavingsTime = TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now);
    //            DateTime MyTime = new DateTime(2000, 1, 1).AddDays(v.Build).AddSeconds(v.Revision * 2).AddHours(isDaylightSavingsTime ? 1 : 0);

    //            return string.Format("Version:{0}.{1} - Compiled:{2:s}", v.Major, v.MajorRevision, MyTime);
    //        }
    //    }
    //}
}