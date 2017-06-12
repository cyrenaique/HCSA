using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.General_Types
{
    public class cPlateLUTManager
    {
        public double GeneralMax { get; private set; }
        public double GeneralMin { get; private set; }
        double LocalMax;
        double LocalMin;
        cDescriptorType CurrentDescriptorType = null;
        cPlate AssociatedPlate = null;


        public cPlateLUTManager(cPlate Sender)
        {
            this.AssociatedPlate = Sender;
        }


        void UpdateGeneralMax(double Max)
        {
            this.GeneralMax = Max;
        }

        void UpdateGeneralMin(double Min)
        {
            this.GeneralMin = Min;
        }


        public void AutomatedRefresh(cDescriptorType CurrentDescriptorType, bool IsFullScreen)
        {
            if (IsFullScreen == false)
            {
                cExtendedTable MinMax = this.AssociatedPlate.GetMinMax(CurrentDescriptorType);
                this.GeneralMin = MinMax[0][0];
                this.GeneralMax = MinMax[0][1];
            }
        
        }


    }
}
