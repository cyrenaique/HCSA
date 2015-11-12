using System;
using System.Collections.Generic;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.General_Types;

// once implemented, you can then call it like this:
// 
//cBetaComponent DB = new cBetaComponent();
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
    class cBetaComponent : cComponent
    {
        cExtendedTable ToReturn = null;

        public cBetaComponent()
        {
            base.Title = "Sample Component";

            // this how you can create parameters
            cProperty Prop1 = new General_Types.cProperty(new cPropertyType("First Paramater", eDataType.DOUBLE), null);
            Prop1.Info = "This is the first parameter info";    // for the tool tips...
            base.ListProperties.Add(Prop1);
        }

        public cExtendedTable GetOutPut()
        {
            return this.ToReturn;
        }


        public cFeedBackMessage Run()
        {
            base.Start();

            // Here is the right way for accessing the parameters...
            // a little laborious, but that's the price to pay for a complete interface
            object _firstValue = base.ListProperties.FindByName("First Paramater");
            double firstValue = 0;
            if (_firstValue == null)
            {
                base.GenerateError("First Paramater not found !");
                return base.FeedBackMessage;
            }
            try
            {
                cProperty TmpProp = (cProperty)_firstValue;
                firstValue = (double)TmpProp.GetValue();
            }
            catch (Exception)
            {
                base.GenerateError("First Paramater cast didn't work");
                return base.FeedBackMessage;
            }

            ToReturn = new cExtendedTable((int)firstValue, (int)firstValue, 0);

            base.End();
            return FeedBackMessage;
        }


    }
}
