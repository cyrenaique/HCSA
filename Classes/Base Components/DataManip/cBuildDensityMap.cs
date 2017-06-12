using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Windows.Forms;
using System.Data;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataManip;
using HCSAnalyzer.Classes.Base_Classes.Viewers._2D;
using System.IO;
using HCSAnalyzer.Classes._3D;
using ImageAnalysis;
using ImageAnalysisFiltering;
using HCSAnalyzer.Classes.General_Types;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    public class cBuildDensityMap : cComponent
    {
        cImage Output = null;
        cListExtendedTable Input;
        cListGeometric3DObject ListObjects;

       // public int Width = 256;
       // public int Height = 256;
      //  public int Depth = 256;
     //   public int KernelSize = 10;
        public bool IsNormalized = true;

        public cBuildDensityMap()
        {
            Title = "Build Density Map";

            cPropertyType CPT = new cPropertyType("Kernel size", eDataType.INTEGER);
            CPT.Min = 1;
            cProperty Prop1 = new cProperty(CPT, null);
            Prop1.Info = "Size of the kernel";
            Prop1.SetNewValue((int)10);
            base.ListProperties.Add(Prop1);

            cPropertyType CPT2 = new cPropertyType("Image width", eDataType.INTEGER);
            CPT2.Min = 1;
            cProperty Prop2 = new cProperty(CPT2, null);
            Prop2.Info = "Width of the density map image";
            Prop2.SetNewValue((int)256);
            base.ListProperties.Add(Prop2);

            cPropertyType CPT3 = new cPropertyType("Image height", eDataType.INTEGER);
            CPT3.Min = 1;
            cProperty Prop3 = new cProperty(CPT3, null);
            Prop3.Info = "Height of the density map image";
            Prop3.SetNewValue((int)256);
            base.ListProperties.Add(Prop3);

            cPropertyType CPT4 = new cPropertyType("Image depth", eDataType.INTEGER);
            CPT4.Min = 1;
            cProperty Prop4 = new cProperty(CPT4, null);
            Prop4.Info = "Depth of the density map image";
            Prop4.SetNewValue((int)256);
            base.ListProperties.Add(Prop4);



        }

        public void SetInputData(cListExtendedTable Input)
        {
            this.Input = Input;
        }

        public cImage GetOutPut()
        {
            return this.Output;
        }

        public cFeedBackMessage Run()
        {
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }

            #region parameters initilization
            object _firstValue = base.ListProperties.FindByName("Kernel size");
            int KernelSize = 0;
            if (_firstValue == null)
            {
                base.GenerateError("Kernel size not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                KernelSize = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("Kernel size cast didn't work");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Image width");
            int ImWidth = 0;
            if (_firstValue == null)
            {
                base.GenerateError("Image width not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                ImWidth = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("Image width cast didn't work");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Image height");
            int ImHeight = 0;
            if (_firstValue == null)
            {
                base.GenerateError("Image height not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                ImHeight = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("Image height cast didn't work");
                return base.FeedBackMessage;
            }

            _firstValue = base.ListProperties.FindByName("Image depth");
            int ImDepth = 0;
            if (_firstValue == null)
            {
                base.GenerateError("Image depth not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                ImDepth = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("Image depth cast didn't work");
                return base.FeedBackMessage;
            }
            #endregion

            if (this.Input[0].Count == 2) ImDepth = 1;
            if (this.Input[0].Count == 1)
            {
                ImDepth = 1;
                ImHeight = 1;
            }

            this.Output = new cImage(ImWidth, ImHeight, ImDepth, this.Input.Count);
            
            cImageDrawKernel GK = new cImageDrawKernel();
            GK.sigma_x = KernelSize;
            GK.sigma_y = KernelSize;
            GK.sigma_z = KernelSize;
            GK.Run();

            cImage K = GK.GetOutPut();

            for (int IdxChannel = 0; IdxChannel < this.Input.Count; IdxChannel++)
            {
                cExtendedTable CurrentTable = this.Input[IdxChannel];
                cExtendedList ListVolumes = new cExtendedList();

                double MaxX = CurrentTable[0].Max();
                double MinX = CurrentTable[0].Min();

                double MaxY = CurrentTable[1].Max();
                double MinY = CurrentTable[1].Min();

                double MaxZ = 0;
                double MinZ = 0;

                if (CurrentTable.Count>2)
                {
                    MaxZ = CurrentTable[2].Max();
                    MinZ = CurrentTable[2].Min();
                }

                this.Output.SingleChannelImage[IdxChannel].Resolution.X = (MaxX - MinX) / (double)ImWidth;
                this.Output.SingleChannelImage[IdxChannel].Resolution.Y = (MaxY - MinY) / (double)ImHeight;
                this.Output.SingleChannelImage[IdxChannel].Resolution.Z = (MaxZ - MinZ) / (double)ImDepth;

                if (CurrentTable.Count > 2)
                {
                    for (int j = 0; j < CurrentTable[0].Count; j++)
                    {
                        double TmpValueX = (ImWidth * (CurrentTable[0][j] - MinX)) / (MaxX - MinX) - K.Width / 2;
                        double TmpValueY = (ImHeight * (CurrentTable[1][j] - MinY)) / (MaxY - MinY) - K.Height / 2;
                        double TmpValueZ = (ImDepth * (CurrentTable[2][j] - MinZ)) / (MaxZ - MinZ) - K.Depth / 2;

                        this.Output.AddInto(K, (int)TmpValueX,  (int)TmpValueY, (int)TmpValueZ, IdxChannel);
                    }
                }
                else
                {
                    for (int j = 0; j < CurrentTable[0].Count; j++)
                    {
                        double TmpValueX = (ImWidth * (CurrentTable[0][j] - MinX)) / (MaxX - MinX) - K.Width / 2;
                        double TmpValueY = ImHeight - (ImHeight * (CurrentTable[1][j] - MinY)) / (MaxY - MinY) - K.Height / 2;

                        this.Output.AddInto(K, (int)TmpValueX, (int)TmpValueY, 0, IdxChannel);
                    }
                }
                cListExtendedTable TablesForDensity = new cListExtendedTable(this.Output);

                for (int idxChannel = 0; idxChannel < this.Output.GetNumChannels(); idxChannel++)
                {
                    this.Output.SingleChannelImage[idxChannel].Name = this.Input.Name;//cGlobalInfo.ListCellularPhenotypes[idxChannel].Name;
                    float Sum = 1;
                    if (IsNormalized)
                    {
                        Sum = (float)TablesForDensity[idxChannel].Sum();
                        if (Sum <= 0.0) continue;
                        for (int i = 0; i < this.Output.SingleChannelImage[idxChannel].Data.Length; i++)
                        {
                            this.Output.SingleChannelImage[idxChannel].Data[i] = (10000000 * this.Output.SingleChannelImage[idxChannel].Data[i]) / Sum;
                        }
                    }
                    //else
                    //{
                    //    for (int i = 0; i < this.Output.SingleChannelImage[idxChannel].Data.Length; i++)
                    //    {
                    //        // if (ClassPopulations[idxChannel] > 0)
                    //        this.Output.SingleChannelImage[idxChannel].Data[i] = this.Output.SingleChannelImage[idxChannel].Data[i];
                    //    }
                    //}
                }
            }


            this.Output.SingleChannelImage[0].UpDateMax();
            this.Output.SingleChannelImage[0].UpDateMin();
            base.End();
            return FeedBackMessage;
        }
    }
}
