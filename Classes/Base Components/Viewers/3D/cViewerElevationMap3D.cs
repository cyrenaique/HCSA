using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.MetaComponents;
using Kitware.VTK;
using System.Windows.Forms;
using System.Drawing;
using HCSAnalyzer.Classes._3D;
using ImageAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{
    class cViewerElevationMap3D : cDataDisplay
    {
        public cViewerElevationMap3D()
        {
            base.Title = "3D Elevation Map Viewer";
        }

        cListExtendedTable Input;
        cImage InputImage = null;

        #region public Parameters
        public Color BackGroundColor = Color.White;
        public byte[][] LUT = null;
        #endregion

        public void SetInputData(cListExtendedTable Input)
        {
            base.Title += ": " + Input.Name;
            this.Input = Input;
        }

        public void SetInputData(cImage InputImage)
        {
            base.Title += ": " + InputImage.Name;
            this.InputImage = InputImage;
        }

        public cFeedBackMessage Run()
        {
            GenerateGraph();

            V3D.GetOutPut().Width = base.CurrentPanel.Width;
            V3D.GetOutPut().Height = base.CurrentPanel.Height;

            base.CurrentPanel.Title = this.Title;
            base.CurrentPanel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right);


            V3D.GetOutPut().Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                        | System.Windows.Forms.AnchorStyles.Left
                                        | System.Windows.Forms.AnchorStyles.Right);

            ContextMenuStrip HeatMapContextMenu = new ContextMenuStrip();
            ToolStripMenuItem ToolStripMenuItem_DisplayTable = new ToolStripMenuItem("Display Table");
            HeatMapContextMenu.Items.Add(ToolStripMenuItem_DisplayTable);
            ToolStripMenuItem_DisplayTable.Click += new System.EventHandler(this.DisplayTable);
            CurrentPanel.ContextMenuStrip = HeatMapContextMenu;

            CurrentPanel.Controls.Add(V3D.GetOutPut());

            return base.FeedBackMessage;
        }

        private void DisplayTable(object sender, EventArgs e)
        {
            ////cDisplayExtendedTable CDET = new cDisplayExtendedTable();
            ////CDET.SetInputData(this.Input);
            ////CDET.Run();

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

        cViewer3D V3D;
        public c3DNewWorld MyWorld;

        private void GenerateGraph()
        {
            V3D = new cViewer3D();
            MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));

            cListGeometric3DObject GlobalList = new cListGeometric3DObject("Elevation Map MetaObject");

            if(this.Input!=null)
            foreach (var item in this.Input)
            {
                c3DElevationMap _3DMap = new c3DElevationMap(new cPoint3D(-this.Input[0].Count / 2, -this.Input[0][0].Count / 2, 0), item, this.LUT);
                _3DMap.SetName("Elevation Map [" + item.Name +"]");
                GlobalList.Add(_3DMap);
            }

            if (this.InputImage != null)
            {
                for (int i = 0; i < this.InputImage.GetNumChannels(); i++)
                {
                    c3DElevationMap _3DMapFromImage = new c3DElevationMap(new cPoint3D(0, 0, 0), this.InputImage, i, 0);
                    if(this.InputImage.AssociatedImagePanel!=null)
                    _3DMapFromImage.SetOpacity(this.InputImage.AssociatedImagePanel.LUTManager.GetImageDisplayProperties().ListOpacity[i]/100.0);
                    _3DMapFromImage.SetName("Elevation Map [" + this.InputImage.SingleChannelImage[i].Name+"]");
                    GlobalList.Add(_3DMapFromImage);
                }
            }

            foreach (var item in GlobalList)
            {
                MyWorld.AddGeometric3DObject(item);
            }

            MyWorld.BackGroundColor = cGlobalInfo.OptionsWindow.FFAllOptions.panelFor3DBackColor.BackColor; 
            V3D.SetInputData(MyWorld);
            V3D.Run();

            #region obsolete
            //// get a reference to the renderwindow of our renderWindowControl1
            //vtkRenderWindow RenderWindow = renderWindowControl1.RenderWindow;
            //// get a reference to the renderer
            //vtkRenderer Renderer = RenderWindow.GetRenderers().GetFirstRenderer();
            //vtkPoints points = vtkPoints.New();


            //double MaxZ = this.Input.Max();
            //double MinZ = this.Input.Min();


            //for (int IdxX = 0; IdxX < this.Input.Count; IdxX++)
            //    for (int IdxY = 0; IdxY < this.Input[IdxX].Count; IdxY++)
            //    {
            //        double Value = ((this.Input[IdxX][this.Input[IdxX].Count- IdxY-1] - MinZ) / (MaxZ - MinZ) * this.Input.Count) / 5;
            //        points.InsertNextPoint(IdxX, IdxY, Value);
            //    }

            //double[] bounds = points.GetBounds();

            //// Add the grid points to a polydata object
            //vtkPolyData inputPolyData = vtkPolyData.New();
            //inputPolyData.SetPoints(points);

            //// Triangulate the grid points
            //vtkDelaunay2D delaunay = vtkDelaunay2D.New();
            //delaunay.SetInput(inputPolyData);
            //delaunay.Update();

            //vtkElevationFilter elevationFilter = vtkElevationFilter.New();
            //elevationFilter.SetInputConnection(delaunay.GetOutputPort());
            //elevationFilter.SetLowPoint(0.0, 0.0, bounds[5]);
            //elevationFilter.SetHighPoint(0.0, 0.0, bounds[4]);
            //elevationFilter.Update();

            //vtkPolyData output = vtkPolyData.New();
            //output.ShallowCopy(vtkPolyData.SafeDownCast(elevationFilter.GetOutput()));

         
            //// Generate the colors for each point based on the color map
            //vtkUnsignedCharArray colors = vtkUnsignedCharArray.New();
            //colors.SetNumberOfComponents(3);
            //colors.SetName("Colors");
            //output.GetPointData().AddArray(colors);
   
            //// Visualize
            //vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            //mapper.SetInput(output);


            //// Create the color map
            //vtkLookupTable colorLookupTable = vtkLookupTable.New();
            //colorLookupTable.Build();


            //if (LUT == null)
            //{
            //    cLUT MyLut = new cLUT();
            //    LUT = MyLut.LUT_JET;
            //}

            ////colorLookupTable.SetValueRange(1,0);//bounds[0], bounds[1]);

            //double LUTSize = LUT[0].Length;
            //colorLookupTable.SetNumberOfTableValues((int)LUTSize - 1);

            //for (int i = 0; i < (int)LUTSize - 1; i++)
            //{
            //    colorLookupTable.SetTableValue((int)LUTSize - 1 - i-1, this.LUT[0][i] / LUTSize, this.LUT[1][i] / LUTSize, this.LUT[2][i] / LUTSize, 1);
            //}


            ////colorLookupTable.SetSaturationRange(0, 0); 
            ////colorLookupTable.SetTableRange(bounds[4], bounds[5]);
            ////table.SetRange(range[0], range[1]); //shoul here not be the minimum/maximum possible of "data"? 
            //// colorLookupTable.SetRampToLinear();
            //colorLookupTable.Build();

            //mapper.SetLookupTable(colorLookupTable);

            //vtkActor NewActor = vtkActor.New();
            //NewActor.SetMapper(mapper);
            //Renderer.AddActor(NewActor);



            //// set background color
            //Renderer.SetBackground(BackGroundColor.R / 255.0, BackGroundColor.G / 255.0, BackGroundColor.B / 255.0);

            //// ensure all actors are visible (in this example not necessarely needed,
            //// but in case more than one actor needs to be shown it might be a good idea)
            //Renderer.ResetCamera();
            #endregion
        }
    }
}
