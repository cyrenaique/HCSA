using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.General_Types;
using ImageAnalysis;
using HCSAnalyzer.Classes.ImageAnalysis.FormsForImages;

namespace HCSAnalyzer.Classes.MetaComponents
{
    class cGetListWellValues : cComponent
    {
        cListWells Input;
        cExtendedTable Output;

        public cGetListWellValues()
        {
            this.Title = "Get Well Values List";

            cPropertyType PT = new cPropertyType("Include Images?", eDataType.BOOL);
            cProperty Prop1 = new cProperty(PT, null);
            Prop1.Info = "Include Images Within the Data (if avalaible).";
            Prop1.SetNewValue((bool)false);
            base.ListProperties.Add(Prop1);

        }

        public cFeedBackMessage Run()
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }
            return Process();
            
            //return FeedBackMessage;
        }


        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }

        cFeedBackMessage Process()
        {

            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }

            object _firstValue = base.ListProperties.FindByName("Include Images?");
            bool IsIncludeImages = false;
            if (_firstValue == null)
            {
                base.GenerateError("-Include Images?- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                IsIncludeImages = (bool)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Include Images?- cast didn't work");
                return base.FeedBackMessage;
            }



            this.Output = this.Input.GetAverageDescriptorValuesFull();


            if (IsIncludeImages)
            {
                this.Output.ListTags = new List<object>();


                foreach (cWell item in Input)
                {
                    cGetImageFromWells GIFW = new cGetImageFromWells();
                    GIFW.SetInputData(new cListWells(this));
                    if (!GIFW.Run().IsSucceed) continue;
                    cImage AccessedImage = GIFW.GetOutPut();
                
                    List<byte[][]> ListLUTs = new List<byte[][]>();
                    cLUT LUT = new cLUT();
                    ListLUTs.Add(LUT.LUT_LINEAR_RED);
                    ListLUTs.Add(LUT.LUT_LINEAR_GREEN);
                    ListLUTs.Add(LUT.LUT_LINEAR_BLUE);

                    List<double> ListMin = new List<double>();
                    ListMin.Add(200);
                    ListMin.Add(200);


                    List<double> ListMax = new List<double>();
                    ListMax.Add(65530);
                    ListMax.Add(5000);


                    cImageDisplayProperties IP = new cImageDisplayProperties();
                    IP.ListMax = ListMax;
                    IP.ListMin = ListMin;

                    this.Output.ListTags.Add(AccessedImage.GetBitmap(0.2f, IP, ListLUTs));
                }
            
            
            }



            base.End();

            return FeedBackMessage;

            //// here is the core of the meta component ...
            //// just a list of Component steps
            //foreach (cWell item in Input)
            //{
            //    item.AssociatedPlate.DBConnection = new cDBConnection(item.AssociatedPlate, item.SQLTableName);
            //    cListSingleBiologicalObjects ListPhenotypes = item.AssociatedPlate.DBConnection.GetBiologicalPhenotypes(item);
            //    cExtendedTable ET = item.AssociatedPlate.DBConnection.GetWellValues(item, cGlobalInfo.CurrentScreening.ListDescriptors.GetActiveDescriptors());
            //    item.AssociatedPlate.DBConnection.CloseConnection();

            //    ET.ListTags = new List<object>();

            //    for (int i = 0; i < ET.Count; i++)
            //    {
            //        ET[i].ListTags = new List<object>();

            //        for (int j = 0; j < ListPhenotypes.Count; j++)
            //            ET[i].ListTags.Add(ListPhenotypes[j]);
            //    }

            //    for (int j = 0; j < ListPhenotypes.Count; j++)
            //        ET.ListTags.Add(ListPhenotypes[j]);


            //    if (Output == null)
            //        Output = new cExtendedTable(ET);
            //    else
            //    {
            //        cMerge M = new cMerge();
            //        M.IsHorizontal = false;
            //        M.SetInputData(Output, ET);
            //        M.Run();

            //        Output = M.GetOutPut();
            //    }
            //}
        }

        public void SetInputData(cListWells Input)
        {
            this.Input = Input;
        }
    }
}
