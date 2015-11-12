using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HCSAnalyzer.Classes.ImageAnalysis._2D_Engine
{

    public class c2DBiologicalObject
    {
    
    
    }


    public class cListPixel : List<Point>
    {
        
    
    }


    public abstract class c2DBasicBiologicalObject
    {
        public string Name;
        PointF Centroid;
        cListPixel ListPixels = null;

        public PointF GetCentroid()
        {
            return Centroid;
        }


        public cListPixel GetListPixel()
        {
            return this.ListPixels;
        }

    }

    public class c2DObjectRegion : c2DBasicBiologicalObject
    {
        public List<c2DObjectClosedCurve> getListContours()
        {
            List<c2DObjectClosedCurve> ToReturn = new List<c2DObjectClosedCurve>();

            return ToReturn;

        }


    }

    public class c2DObjectPoint : c2DBasicBiologicalObject
    {
      //  public 
    }

    public class c2DObjectCurve : c2DBasicBiologicalObject
    {
        public PointF GetNextPoint()
        {
            PointF ToReturn = new PointF();

            return ToReturn;
        }
    }

    public class c2DObjectClosedCurve : c2DObjectCurve
    {
        //public override PointF GetNextPoint()
        //{
        //    PointF ToReturn;



        //    return ToReturn;
        //}
    }


}
