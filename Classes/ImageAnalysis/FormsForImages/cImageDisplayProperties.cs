using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Forms.FormsForImages;

namespace HCSAnalyzer.Classes.ImageAnalysis.FormsForImages
{
    public class cImageDisplayProperties
    {
        public List<double> ListMin = null;
        public List<double> ListMax = null;
        public List<bool> ListActive = null;
        public List<string> ListLUTNames = null;
        public List<double> ListOpacity = null;
        public List<double> ListGamma = null;


        public void UpdateFromLUTManager(FormForLUTManager LUTManager)
        {
            ListMin = new List<double>();
            ListMax = new List<double>();
            ListActive = new List<bool>();
            ListLUTNames = new List<string>();
            ListGamma = new List<double>();
            ListOpacity = new List<double>();

            for (int IdxChannel = 0; IdxChannel < LUTManager.panelForLUTS.Controls.Count; IdxChannel++)
            {
                UserControlSingleLUT SingleLUT = (UserControlSingleLUT)LUTManager.panelForLUTS.Controls[IdxChannel];
                ListMin.Add((double)SingleLUT.numericUpDownMinValue.Value);
                ListMax.Add((double)SingleLUT.numericUpDownMaxValue.Value);

                ListActive.Add(SingleLUT.checkBoxIsActive.Checked);

                ListLUTNames.Add(SingleLUT.comboBoxForLUT.Text);

                ListGamma.Add(SingleLUT.trackBarGamma.Value);
                ListOpacity.Add(SingleLUT.trackBarOpacity.Value);
            }

            
        
        }


    }
}
