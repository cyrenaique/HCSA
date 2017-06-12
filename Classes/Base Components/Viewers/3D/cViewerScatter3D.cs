using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Forms;
using System.Windows.Forms;
using HCSAnalyzer.Forms._3D;
using LibPlateAnalysis;
using Kitware.VTK;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewerScatter3D : cDataDisplay
    {
        cGlobalInfo GlobalInfo;

        public cViewerScatter3D(cGlobalInfo GlobalInfo)
        {
            Title = "New Viewer Scatter 3D";
            this.GlobalInfo = GlobalInfo;
        }

       // FormFor3DDataDisplay FormToDisplayXYZ;

        //public void SetInputData(List<cExtendedList> Input)
        //{
        //    //FDT = new FormToDisplayDataTable(MyData);
        //}

        //public void Run()
        //{
        //    FormToDisplayXYZ = new FormFor3DDataDisplay(false, this.GlobalInfo.CurrentScreening);
        //    for (int i = 0; i < (int)this.GlobalInfo.CurrentScreening.ListDescriptors.Count; i++)
        //    {
        //        FormToDisplayXYZ.comboBoxDescriptorX.Items.Add(this.GlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
        //        FormToDisplayXYZ.comboBoxDescriptorY.Items.Add(this.GlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
        //        FormToDisplayXYZ.comboBoxDescriptorZ.Items.Add(this.GlobalInfo.CurrentScreening.ListDescriptors[i].GetName());
        //    }
        //}

        //public void Display()
        //{
        //    FormToDisplayXYZ.Show();
        //}


        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            try
            {
                GenerateGraph();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }


        cExtendedTable Input;

        public void SetInputData(cExtendedTable Input)
        {
            //  base.Title += ": " + InputVolume.Name;
            this.Input = Input;
        }

        RenderWindowControl renderWindowControl1;

        public cFeedBackMessage Run()
        {

            //CurrentLUT = LUT.LUT_JET;
            //  Kitware.VTK.RenderWindowControl VTKView = GenerateGraph();
            renderWindowControl1 = new RenderWindowControl();
            renderWindowControl1.Load += new EventHandler(renderWindowControl1_Load);

            renderWindowControl1.Width = base.CurrentPanel.Width;
            renderWindowControl1.Height = base.CurrentPanel.Height;

            base.CurrentPanel.Title = this.Title;
            base.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right);


            renderWindowControl1.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right);

            //ContextMenuStrip HeatMapContextMenu = new ContextMenuStrip();
            //ToolStripMenuItem ToolStripMenuItem_DisplayTable = new ToolStripMenuItem("Display Table");
            //HeatMapContextMenu.Items.Add(ToolStripMenuItem_DisplayTable);
            //ToolStripMenuItem_DisplayTable.Click += new System.EventHandler(this.DisplayTable);
            //CurrentPanel.ContextMenuStrip = HeatMapContextMenu;

            CurrentPanel.Controls.Add(renderWindowControl1);

            return base.FeedBackMessage;
        }


        c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(100, 100, 100),
                                        new cPoint3D(1, 1, 1));

        private void GenerateGraph()
        {






            MyWorld.BackGroundColor = Color.Orange;// GhostWhite;

            cViewer3D V3D = new cViewer3D();
            V3D.SetInputData(MyWorld);
            V3D.Run();
















            //// get a reference to the renderwindow of our renderWindowControl1
            //vtkRenderWindow RenderWindow = renderWindowControl1.RenderWindow;
            //// get a reference to the renderer
            //vtkRenderer Renderer = RenderWindow.GetRenderers().GetFirstRenderer();


            //int IdxItem = 0;

            //c3DSphere NewSPhere = new c3DSphere(new cPoint3D(0, 0, 0), 10);

            ////Renderer.AddActor(NewSPhere.

            ////foreach (var item in ListVTKData)
            ////{
            ////    vtkImageActor imageActor = vtkImageActor.New();
            ////    imageActor.SetInput(item);
            ////    imageActor.SetPosition(item.GetDimensions()[0] * (IdxItem++), 0, 0);

            ////    Renderer.AddActor2D(imageActor);
            ////}

            ////if ((this.InputVolume != null) && (this.InputVolume.vtk_volume != null))
            ////{
            ////    Renderer.AddVolume(this.InputVolume.vtk_volume);
            ////}

            //Renderer.SetBackground(0.3, 0.3, 0.3);
            //Renderer.ResetCamera();

        }

    }
}
