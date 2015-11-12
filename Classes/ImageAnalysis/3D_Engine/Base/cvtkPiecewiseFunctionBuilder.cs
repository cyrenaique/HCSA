using System;
using System.Collections.Generic;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.General_Types;
using Kitware.VTK;

// once implemented, you can then call it like this:
// 
//cComponent_Sample1 DB = new cComponent_Sample1();
//double DefaultValue = 3;
//DB.ListProperties.FindByName("First Paramater").SetNewValue(DefaultValue);
//DB.ListProperties.FindByName("First Paramater").IsGUIforValue = false;

//cFeedBackMessage FeedBackMessage = DB.Run();
//richTextBoxConsole.AppendText(FeedBackMessage.GetFullFeedBack());
//if (FeedBackMessage.IsSucceed == false) return;

//cDisplayExtendedTable DET = new cDisplayExtendedTable();
//DET.SetInputData(DB.GetOutPut());
//DET.Run();

namespace HCSAnalyzer.Classes.Base_Classes
{
    class cvtkPiecewiseFunctionBuilder : cComponent
    {
        cExtendedTable Input = null;
        vtkPiecewiseFunction ToReturn;

        public cvtkPiecewiseFunctionBuilder()
        {
            base.Title = "vtkPiecewiseFunction builder";


            cPropertyType PT = new cPropertyType("Max. Opacity", eDataType.DOUBLE);
            PT.Max = 1;
            cProperty Prop1 = new cProperty(PT, null);
            Prop1.Info = "Maximum Opacity";
            Prop1.SetNewValue((double)0.1);
            base.ListProperties.Add(Prop1);


            cPropertyType PT1 = new cPropertyType("Min. Opacity", eDataType.DOUBLE);
            PT1.Max = 1;
            cProperty Prop2 = new cProperty(PT1, null);
            Prop2.Info = "Minimum Opacity";
            Prop2.SetNewValue((double)0.1);
            base.ListProperties.Add(Prop2);



        }

        public vtkPiecewiseFunction GetOutPut()
        {
            return this.ToReturn;
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }


        public cFeedBackMessage Run()
        {
            if (base.Start() == false)
            {
                base.FeedBackMessage.IsSucceed = false;
                return base.FeedBackMessage;
            }




            object _firstValue = base.ListProperties.FindByName("Max. Opacity");
            double MaxOpacity = 0;
            if (_firstValue == null)
            {
                base.GenerateError("Max. Opacity not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                MaxOpacity = (double)TmpProp.GetValue();
                Input[1][1] = MaxOpacity;
            }
            catch (Exception)
            {
                base.GenerateError("Max. Opacity cast didn't work");
                return base.FeedBackMessage;
            }

            object _ScdValue = base.ListProperties.FindByName("Min. Opacity");
            double MinOpacity = 0;
            if (_ScdValue == null)
            {
                base.GenerateError("Min. Opacity not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_ScdValue;
                MinOpacity = (double)TmpProp.GetValue();
                Input[0][1] = MaxOpacity;
            }
            catch (Exception)
            {
                base.GenerateError("Min. Opacity cast didn't work");
                return base.FeedBackMessage;
            }








            ToReturn = vtkPiecewiseFunction.New();

            for (int i = 0; i < this.Input[0].Count; i++)
            {
                ToReturn.AddPoint(this.Input[0][i], this.Input[1][i]);    
            }



            // Here is the right way for accessing the parameters...
            // a little laborious, but that's the price to pay for a complete interface
            //object _firstValue = base.ListProperties.FindByName("First Paramater");
            //double firstValue = 0;
            //if (_firstValue == null)
            //{
            //    base.GenerateError("First Paramater not found !");
            //    return base.FeedBackMessage;
            //}
            //try
            //{
            //    cProperty TmpProp = (cProperty)_firstValue;
            //    firstValue = (double)TmpProp.GetValue();
            //}
            //catch (Exception)
            //{
            //    base.GenerateError("First Paramater cast didn't work");
            //    return base.FeedBackMessage;
            //}

            //ToReturn = new cExtendedTable((int)firstValue, (int)firstValue, 0);

            base.End();
            return FeedBackMessage;
        }


    }
}
