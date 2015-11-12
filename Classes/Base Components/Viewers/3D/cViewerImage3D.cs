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

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewerImage3D : cDataDisplay
    {
        public cViewerImage3D()
        {
            base.Title = "3D Image Viewer";
        }

        cSingleChannelImage Input;
        public byte[][] LUT = null;

        cVolumeRendering3D InputVolume;
        List<vtkImageData> ListVTKData = new List<vtkImageData>();
        #region public Parameters
        public Color BackGroundColor = Color.White;
        #endregion

        public void SetInputData(cSingleChannelImage InputImage)
        {   
            this.Input = InputImage;
            base.Title += ": " + Input.Name;

            this.InputVolume = new cVolumeRendering3D(InputImage, new Classes._3D.cPoint3D(0, 0, 0),null, null);
        }

        public void SetInputData(cVolumeRendering3D InputVolume)
        {
          //  base.Title += ": " + InputVolume.Name;
            this.InputVolume = InputVolume;
        }

        public void SetInputData(vtkImageData VTKData)
        {
          //  base.Title += ": " + InputVolume.Name;
            this.ListVTKData.Add(VTKData);
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


        private void GenerateGraph()
        {
            // get a reference to the renderwindow of our renderWindowControl1
            vtkRenderWindow RenderWindow = renderWindowControl1.RenderWindow;
            // get a reference to the renderer
            vtkRenderer Renderer = RenderWindow.GetRenderers().GetFirstRenderer();


            int IdxItem = 0;
            foreach (var item in ListVTKData)
            {
                vtkImageActor imageActor = vtkImageActor.New();
                imageActor.SetInput(item);
                imageActor.SetPosition(item.GetDimensions()[0] * (IdxItem++), 0, 0);

                Renderer.AddActor2D(imageActor);
            }

            if ((this.InputVolume!=null) && (this.InputVolume.vtk_volume != null))
            {
                Renderer.AddVolume(this.InputVolume.vtk_volume);
            }

            Renderer.SetBackground(0.3, 0.3, 0.3);
            Renderer.ResetCamera();

        }
    }
}
