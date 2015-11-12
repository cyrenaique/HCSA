using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Kitware.VTK;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine;
using HCSAnalyzer.Classes.Base_Classes.GUI;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Components.Viewers._3D;
using ImageAnalysis;

namespace HCSAnalyzer.Classes._3D
{

    public enum eMesh3DMode
    {
        SOLID, WIREFRAME, POINT
    }

    /// <summary>
    /// cPoint3D class
    /// </summary>
    [Serializable]
    public class cPoint3D
    {
        public object Tag;

        public cPoint3D(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public cPoint3D(cPoint3D Source)
        {
            this.X = Source.X;
            this.Y = Source.Y;
            this.Z = Source.Z;
        }

        public double X;
        public double Y;
        public double Z;


        // The overloaded + operator
        public static cPoint3D operator +(cPoint3D First, cPoint3D Second)
        {
            return new cPoint3D(First.X + Second.X, First.Y + Second.Y, First.Z + Second.Z);
        }
        public static cPoint3D operator -(cPoint3D First, cPoint3D Second)
        {
            return new cPoint3D(First.X - Second.X, First.Y - Second.Y, First.Z - Second.Z);
        }
        public static cPoint3D operator +(cPoint3D First, double Second)
        {
            return new cPoint3D(First.X + Second, First.Y + Second, First.Z + Second);
        }
        public static cPoint3D operator -(cPoint3D First, double Second)
        {
            return new cPoint3D(First.X - Second, First.Y - Second, First.Z - Second);
        }
        public static cPoint3D operator /(cPoint3D First, double Second)
        {
            return new cPoint3D(First.X / Second, First.Y / Second, First.Z / Second);
        }
        public static cPoint3D operator *(cPoint3D First, double Second)
        {
            return new cPoint3D(First.X * Second, First.Y * Second, First.Z * Second);
        }
        public static cPoint3D operator *(cPoint3D First, cPoint3D Second)
        {
            return new cPoint3D(First.X * Second.X, First.Y * Second.Y, First.Z * Second.Z);
        }
        public static cPoint3D operator /(cPoint3D First, cPoint3D Second)
        {
            return new cPoint3D(First.X / Second.X, First.Y / Second.Y, First.Z / Second.Z);
        }
        public double DistTo(cPoint3D DestPoint)
        {
            return Math.Sqrt((DestPoint.X - this.X) * (DestPoint.X - this.X) + (DestPoint.Y - this.Y) * (DestPoint.Y - this.Y) + (DestPoint.Z - this.Z) * (DestPoint.Z - this.Z));
        }
    }

    public class cListPoints3D : List<cPoint3D>
    {
        public string Name = "List 3D points";

        public cExtendedTable GetDataTable()
        {
            cExtendedTable ToReturn = new cExtendedTable();
            ToReturn.Name = this.Name;
            ToReturn.ListRowNames = new List<string>();
            ToReturn.Add(new cExtendedList("X"));
            ToReturn.Add(new cExtendedList("Y"));
            ToReturn.Add(new cExtendedList("Z"));

            int Idx = 0;
            foreach (var item in this)
            {
                ToReturn[0].Add(item.X);
                ToReturn[1].Add(item.Y);
                ToReturn[2].Add(item.Z);
                ToReturn.ListRowNames.Add("Vertex_" + Idx++);
            }

            return ToReturn;
        }


    }

    /// <summary>
    /// cObject3D
    /// </summary>
    public abstract class cObject3D
    {
        public c3DNewWorld AssociatedWorld;
        cPoint3D Position;

        public cPoint3D GetPosition()
        {
            return Position;
        }

        public void SetPosition(cPoint3D Position)
        {
            this.Position = new cPoint3D(Position);
            this.vtk_Actor.SetPosition(Position.X, Position.Y, Position.Z);
            Redraw();
        }

        public void Redraw()
        {
            if(this.AssociatedWorld!=null)
                this.AssociatedWorld.Redraw();
        }

        public Color Colour;
        public vtkPolyDataMapper vtk_PolyDataMapper;
        public vtkActor vtk_Actor;
        public string ObjectType;
        public object Tag;
        public object ParentTag;
        protected List<vtkActor> ListActors;
        string Name;

        public string GetName()
        {
            return this.Name;
        }

        public void SetName(string Name)
        {
           
            this.Name = Name;
        }

        public string GetBasicInfo()
        {
            string ToReturn = "Name: " + Name + "\n\n";
            ToReturn += "Object Type: " + ObjectType + "\n";
            ToReturn += "Position: [X=" + Position.X + ", Y=" + Position.Y + ", Z=" + Position.Z + "]\n";
            ToReturn += "Colour: " + Colour.ToString() + "\n";

            if (ObjectType == "Geometrical")
                ToReturn += "Opacity: " + this.vtk_Actor.GetProperty().GetOpacity() + "\n";

            if (Tag != null)
                ToReturn += "Tag: " + Tag.ToString() + "\n";
            else
                ToReturn += "Tag: Null\n";
            if (ParentTag != null)

                ToReturn += "Parent Tag: " + ParentTag.ToString() + "\n";
            else
                ToReturn += "Parent Tag: Null\n";

            return ToReturn;
        }

        public vtkActor GetActor()
        {
            return vtk_Actor;
        }

        public cObject3D()
        {
            vtk_Actor = vtkActor.New();
        }

        public void AddMeToTheWorld(vtkRenderer World)
        {
            if (vtk_Actor != null)
                World.AddViewProp(this.vtk_Actor);

            if (ListActors != null)
                for (int i = 0; i < ListActors.Count; i++) World.AddViewProp(this.ListActors[i]);
        }

        protected void BackfaceCulling(bool IsDoubleFaced)
        {
            if (IsDoubleFaced)
                vtk_Actor.GetProperty().BackfaceCullingOff();
            else
                vtk_Actor.GetProperty().BackfaceCullingOn();
        }

        /// <summary>
        /// Create a vtkActor object, with a specific interpolation mode
        /// </summary>
        /// <param name="InterpolationMode">0 : flat, 1 : Gouraud, 2 : Phong</param>
        protected void CreateVTK3DObject(int InterpolationMode)
        {
            vtk_Actor.SetMapper(vtk_PolyDataMapper);


            if (InterpolationMode == 0)
            {
                vtk_Actor.GetProperty().SetColor(this.Colour.R / 255.0, this.Colour.G / 255.0, this.Colour.B / 255.0);
                vtk_Actor.GetProperty().SetInterpolationToFlat();
            }
            else if (InterpolationMode == 1)
            {
                vtk_Actor.GetProperty().SetColor(this.Colour.R / 255.0, this.Colour.G / 255.0, this.Colour.B / 255.0);
                vtk_Actor.GetProperty().SetInterpolationToGouraud();
            }
            else if (InterpolationMode == 2)
            {
                vtk_Actor.GetProperty().SetSpecularColor(this.Colour.R / 255.0, this.Colour.G / 255.0, this.Colour.B / 255.0);
                vtk_Actor.GetProperty().SetAmbient(0.8);
                vtk_Actor.GetProperty().SetDiffuse(0.1);
                vtk_Actor.GetProperty().SetInterpolationToPhong();
                vtk_Actor.GetProperty().SetSpecular(0.5);
                vtk_Actor.GetProperty().SetSpecularPower(4.0);
            }
            else if (InterpolationMode == 3)
            {
                // vtk_Actor.GetProperty().SetSpecularColor(this.Colour.R / 255.0, this.Colour.G / 255.0, this.Colour.B / 255.0);
                vtk_Actor.GetProperty().SetAmbient(0.8);
                vtk_Actor.GetProperty().SetDiffuse(0.1);
                vtk_Actor.GetProperty().SetInterpolationToPhong();
                vtk_Actor.GetProperty().SetSpecular(0.5);
                vtk_Actor.GetProperty().SetSpecularPower(4.0);
            }

            //                vtk_Actor.GetProperty().SetInterpolationToGouraud();
            vtk_Actor.SetPosition(Position.X, Position.Y, Position.Z);
            vtk_Actor.DragableOn();
            vtk_Actor.PickableOn();
            //ListObject
        }

        public void SetOpacity(double Opacity)
        {
            vtk_Actor.GetProperty().SetOpacity(Opacity);
          //  Redraw();
        }

        public void SetScale(double Scale)
        {
            if (this.GetType() == typeof(c3DLine)) return;
            if (this.GetType() == typeof(c3DPointCloud))
            {
                vtk_Actor.GetProperty().SetPointSize((float)Scale * 10.0f);
                return;
            }
            if (this.GetType() == typeof(c3DElevationMap))
            {
                //vtk_Actor.GetProperty().SetPointSize((float)Scale * 10.0f);
                c3DElevationMap TmpMap = (c3DElevationMap)(this);
                
                    int NumPts = (int)TmpMap.points.GetNumberOfPoints();
             
                
                for (int i = 0; i < NumPts; i++)
                {
                    double[] CurrentValue = TmpMap.points.GetPoint(i);
                    CurrentValue[2] *= (10.0 * Scale);
                    TmpMap.points.SetPoint(i, CurrentValue[0], CurrentValue[1], CurrentValue[2]);
                }
                vtk_Actor.GetMapper().Modified();
                vtk_Actor.GetMapper().Update();

                vtk_Actor.Modified();

                return;
            }

            vtk_Actor.SetScale(Scale);
        }

        public double GetScale()
        {
            return vtk_Actor.GetScale()[0];
        }

        public eMesh3DMode GetMode()
    {
        if (vtk_Actor.GetProperty().GetRepresentation() == 0)
            return eMesh3DMode.POINT;
        else if (vtk_Actor.GetProperty().GetRepresentation() == 1)
            return eMesh3DMode.WIREFRAME;
        else return eMesh3DMode.SOLID;
    }

        public double GetOpacity()
        {
            return vtk_Actor.GetProperty().GetOpacity();
        }

        public void SetColor(Color Colour)
        {
            vtk_Actor.GetProperty().SetColor(Colour.R / 255.0, Colour.G / 255.0, Colour.B / 255.0);
            this.Colour = Colour;
            // vtk_Actor.GetProperty().SetOpacity(Colour.A / 255.0);
            // vtk_Actor.GetProperty().SetAmbient(0.9);
        }

        public Color GetColor()
        {
            double[] ColorValue = vtk_Actor.GetProperty().GetColor();
            return Color.FromArgb((int)(ColorValue[0] * 255), (int)(ColorValue[1] * 255), (int)(ColorValue[2] * 255));
        }

        public void SetToPoints()
        {
            vtk_Actor.GetProperty().SetRepresentationToPoints();
        }

        public void SetToWireFrame()
        {
            vtk_Actor.GetProperty().SetRepresentationToWireframe();
        }

        public void SetToSurface()
        {
            vtk_Actor.GetProperty().SetRepresentationToSurface();
        }

        public void IsPickable(bool Pickable)
        {
            if (Pickable)
                vtk_Actor.PickableOn();
            else
                vtk_Actor.PickableOff();
        }

        #region Text Display associated
        vtkFollower TextActor;
        vtkPolyDataMapper TextMapper;
        vtkVectorText TextVTK;

        public void AddText(String Text, c3DWorld CurrentWorld, double scale)
        {
            if (TextActor == null)
            {
                TextActor = vtkFollower.New();
                TextMapper = vtkPolyDataMapper.New();
                TextVTK = vtkVectorText.New();

                TextVTK.SetText(Text);
                TextMapper.SetInputConnection(TextVTK.GetOutputPort());
                TextActor.SetMapper(TextMapper);

                //TextActor.SetPosition(this.GetActor().GetCenter()[0]-1, this.GetActor().GetCenter()[1]-1, this.GetActor().GetCenter()[2]-2);
                TextActor.SetPosition(Position.X, Position.Y, Position.Z - 1);
                TextActor.SetPickable(0);
                CurrentWorld.ren1.AddActor(TextActor);
                TextActor.SetCamera(CurrentWorld.ren1.GetActiveCamera());
            }
            else
            {
                TextVTK.SetText(Text);
            }
            TextActor.SetScale(scale);
        }

        public void AddText(String Text, c3DWorld CurrentWorld, double scale, Color colour)
        {
            AddText(Text, CurrentWorld, scale);
            TextActor.GetProperty().SetColor(Colour.R / 255.0, Colour.G / 255.0, Colour.B / 255.0);
        }

        public void HideText()
        {
            if (TextActor != null)
                TextActor.VisibilityOff();
        }


        public void ShowText()
        {
            if (TextActor != null)
                TextActor.VisibilityOn();
        }

        #endregion

        public List<ToolStripMenuItem> GetExtendedContextMenu()
        {
            List<ToolStripMenuItem> ListToReturn = new List<ToolStripMenuItem>();

            ToolStripMenuItem MainMenu = new ToolStripMenuItem("[" + this.Name + "]");

            ToolStripMenuItem _3DObjectProperties_Scale = new ToolStripMenuItem("Scale");
            MainMenu.DropDownItems.Add(_3DObjectProperties_Scale);
            _3DObjectProperties_Scale.Click += new System.EventHandler(this._3DObjectProperties_Scale);

            ToolStripMenuItem ToolStripMenuItem_Position = new ToolStripMenuItem("Display Options");
            ToolStripMenuItem_Position.Click += new System.EventHandler(this.ToolStripMenuItem_Position);
            MainMenu.DropDownItems.Add(ToolStripMenuItem_Position);

            MainMenu.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem _3DObjectProperties_AddNotation = new ToolStripMenuItem("Add Notation");
            MainMenu.DropDownItems.Add(_3DObjectProperties_AddNotation);
            _3DObjectProperties_AddNotation.Click += new System.EventHandler(this._3DObjectProperties_AddNotation);

            MainMenu.DropDownItems.Add(new ToolStripSeparator());

            #region object info
            ToolStripMenuItem _3DObjectProperties_INFO = new ToolStripMenuItem("Info");

            ToolStripMenuItem _3DObjectProperties_GetVerticesList = new ToolStripMenuItem("Vertices List");

            _3DObjectProperties_INFO.DropDownItems.Add(_3DObjectProperties_GetVerticesList);
            _3DObjectProperties_GetVerticesList.Click += new System.EventHandler(this._3DObjectProperties_GetVerticesList);

            _3DObjectProperties_INFO.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem _3DObjectProperties_GetInfo = new ToolStripMenuItem("Info");

            _3DObjectProperties_INFO.DropDownItems.Add(_3DObjectProperties_GetInfo);
            _3DObjectProperties_GetInfo.Click += new System.EventHandler(this._3DObjectProperties_GetInfo);

            MainMenu.DropDownItems.Add(_3DObjectProperties_INFO);
            #endregion

            ListToReturn.Add(MainMenu);

            return ListToReturn;
        }

        private void _3DObjectProperties_AddNotation(object sender, EventArgs e)
        {
            //cObject3D Obj = (cObject3D)((ToolStripMenuItem)sender).Tag;
            //if (Obj == null) return;



            // Obj.GetActor().GetProperty().

            //Obj.AddText(
            //  Obj.SetToPoints();
        }

        private void _3DObjectProperties_GetVerticesList(object sender, EventArgs e)
        {
            try
            {
                cExtendedTable ET = ((cGeometric3DObject)(this)).GetListVertex().GetDataTable();
                ET.Name = "[" + this.GetName() + "] Vertices List";
                cDisplayExtendedTable DET = new cDisplayExtendedTable();
                DET.SetInputData(ET);
                DET.Run();
            }
            catch (Exception)
            {

                return;
            }

        }

        private void _3DObjectProperties_GetInfo(object sender, EventArgs e)
        {
            try
            {
                cExtendedTable ET = ((cGeometric3DObject)(this)).GetInfo();

                cDisplayExtendedTable DET = new cDisplayExtendedTable();
                DET.SetInputData(ET);
                DET.Run();
            }
            catch (Exception)
            {

                return;
            }

        }

        private void _3DObjectProperties_Scale(object sender, EventArgs e)
        {
            FormForSingleSlider SliderForOpacity = new FormForSingleSlider("Object Scale");
            SliderForOpacity.numericUpDown.Maximum = 1000;
            SliderForOpacity.numericUpDown.Value = (decimal)(this.GetScale() * 100.0);

            if (SliderForOpacity.ShowDialog() != DialogResult.OK) return;

            this.SetScale((double)SliderForOpacity.numericUpDown.Value / 100.0);
           // this.AssociatedWorld.AssociatedVTKRenderer.GetRenderWindow().GetInteractor().Render();
        }

        FormFor3DObjectParam MyFormFor3DObjectParam;

        private void ToolStripMenuItem_Position(object sender, EventArgs e)
        {
           
            MyFormFor3DObjectParam = new FormFor3DObjectParam(this);
            MyFormFor3DObjectParam.Show();
           // MyFormFor3DObjectParam.ShowDialog();
        }




    }
}