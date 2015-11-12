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
    class cGetImageFromWells : cComponent
    {
        cListWells Input;
        cImage Output;

        public cGetImageFromWells()
        {
            this.Title = "Get Wells List Image";

            cPropertyType PT = new cPropertyType("Field", eDataType.INTEGER);
            cProperty Prop1 = new cProperty(PT, null);
            Prop1.Info = "Field";
            Prop1.SetNewValue((int)0);
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
        }


        public cImage GetOutPut()
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

            object _field = base.ListProperties.FindByName("Field");
            int Field = 0;
            if (_field == null)
            {
                base.GenerateError("-Field- not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_field;
                Field = (int)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("-Field- cast didn't work");
                return base.FeedBackMessage;
            }

            //this.Output = this.Input[0].GetImage(Field);
            cGlobalInfo.ImageAccessor.Field = Field;// (int)this.numericUpDownField.Value;
            
            List<cImageMetaInfo> ImageMetaInfo = cGlobalInfo.ImageAccessor.GetImageInfo(this.Input[0]);
            if(ImageMetaInfo==null) 
            {
                FeedBackMessage.IsSucceed = false;
                base.End();
                return FeedBackMessage;
            }
            //string FileName = ImageMetaInfo[0].FileName;

            this.Output = new cImage(ImageMetaInfo);

            //this.Output = new cImage(30000, 5000, 1, 1);

            //for (int i = 0; i < this.Output.SliceSize; i++)
            //{
            //    this.Output.SingleChannelImage[0].Data[i] = i;
            //}


            base.End();

            return FeedBackMessage;

        }

        public void SetInputData(cListWells Input)
        {
            this.Input = Input;
        }
    }
}
