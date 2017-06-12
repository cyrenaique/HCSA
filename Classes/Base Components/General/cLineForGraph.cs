using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace HCSAnalyzer.Classes.Base_Classes.General
{
    public class cLineVerticalForGraph : VerticalLineAnnotation
    {
        public double Position;
        public bool IsAllowMoving = false;

        public void AddText(string Text)
        {
            TA = new TextAnnotation();
            TA.X = this.Position;
            TA.Text = Text;
        }

        TextAnnotation TA = null;

        public cLineVerticalForGraph(double Position)
        {
           this.Position = Position;
        }

        public void Run(Chart CurrentChart)
        {
            
            this.IsInfinitive = true;
            this.LineWidth = 1;
           // this.AllowMoving = false;
            this.LineColor = Color.Gray;
            this.LineDashStyle = ChartDashStyle.Dash;
            base.AllowMoving = this.IsAllowMoving;
            this.ClipToChartArea = CurrentChart.ChartAreas[0].Name;

            this.AxisX = CurrentChart.ChartAreas[0].AxisX;
            this.X = Position;

            if (this.TA != null)
            {
                this.TA.AxisX = CurrentChart.ChartAreas[0].AxisX;
                this.TA.Y = 10;
                CurrentChart.Annotations.Add(this.TA);
            }
            CurrentChart.Annotations.Add(this);
        }
    }

    public class cLineHorizontalForGraph : HorizontalLineAnnotation
    {
        public double Position;
        public bool IsAllowMoving = false;

        public void AddText(string Text)
        {
            TA = new TextAnnotation();
            TA.X = this.Position;
            TA.Text = Text;
        }

        TextAnnotation TA = null;

        public cLineHorizontalForGraph(double Position)
        {
            this.Position = Position;
          
        }

        public void Run(Chart CurrentChart)
        {  
            this.IsInfinitive = true;
            this.LineWidth = 1;
            base.AllowMoving = this.IsAllowMoving;
            this.LineColor = Color.Gray;
            this.LineDashStyle = ChartDashStyle.Dash;
            this.ClipToChartArea = CurrentChart.ChartAreas[0].Name;

            this.AxisY = CurrentChart.ChartAreas[0].AxisY;
            this.Y = Position;

            if (this.TA != null)
            {
                this.TA.AxisX = CurrentChart.ChartAreas[0].AxisX;
                this.TA.Y = 10;
                CurrentChart.Annotations.Add(this.TA);
            }
            CurrentChart.Annotations.Add(this);
        }
    }

}
