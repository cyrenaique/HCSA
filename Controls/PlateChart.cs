﻿using System.Windows.Forms.DataVisualization.Charting;

namespace HCSAnalyzer
{
    public class PlateChart : Chart
    {
        public PlateChart()
        {
            SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
        }
    }
}
