using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cSigmoidFitting : cComponent
    {
        cExtendedTable Input = null;
        cExtendedTable Output;
        public int ConcentrationPos = 0;

      
        public cSigmoidFitting()
        {
            this.Title = "Sigmoid Fitting";
        }

        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        public cFeedBackMessage Run()
        {
         
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data table defined.";
                return FeedBackMessage;
            }
            if (this.Input.Count < 2)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "The input data table should contain at least 2 columns (Concentrations + feature values)";
                return FeedBackMessage;
            }

            // ------------- now proceed ------------- 
            return Process();


            //FeedBackMessage = new cFeedBackMessage(true);
            //return FeedBackMessage;
        }

        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }

        private static void function_SigmoidInhibition(double[] c, double[] x, ref double func, object obj)
        {
            double Concentration =x[0];

            func = c[0] + (c[1] - c[0]) / (1 + Math.Pow(((Math.Pow(10, c[2]) / Math.Pow(10, Concentration))), c[3]));
        }

        public static void function_SigmoidInhibitionForIntegration(double x, double xminusa, double bminusx, ref double func, object obj)
        {
            // this callback calculates f(x)=exp(x)
           // func = Math.Exp(x);
            double Concentration = x;
          
            cExtendedTable TmpValuesParam = (cExtendedTable)(obj);

            
            int IdxDesc = 0;
            double EC50 = Math.Log10(TmpValuesParam[IdxDesc][2]);
            //    double Concentration = Math.Log10(item);



            func = TmpValuesParam[IdxDesc][0] + (TmpValuesParam[IdxDesc][1] - TmpValuesParam[IdxDesc][0])
                              / (1 + Math.Pow((Math.Pow(10, EC50) / Math.Pow(10, Concentration)), TmpValuesParam[IdxDesc][3]));

            //    FittedValues[1].Add(Value);

        }

        public cExtendedTable GetFittedRawValues(cExtendedList ListConcentrations)
        {
            cExtendedTable FittedValues = new cExtendedTable(ListConcentrations);
            FittedValues[0].Name = "Concentration";
            FittedValues.Add(new cExtendedList());
            FittedValues[1].Name = "";//Fitted Values";

            int IdxDesc = 0;
            double EC50 = Math.Log10(this.Output[IdxDesc][2]);
            foreach (var item in ListConcentrations)
            {
                double Concentration = Math.Log10(item);
                //  double Value = this.Output[IdxDesc][0] + (this.Output[IdxDesc][1] - this.Output[IdxDesc][0]) / (1 + Math.Pow((this.Output[IdxDesc][2] / item), this.Output[IdxDesc][3]));
                double Value = this.Output[IdxDesc][0] + (this.Output[IdxDesc][1] - this.Output[IdxDesc][0])
                              / (1 + Math.Pow((Math.Pow(10, EC50) / Math.Pow(10, Concentration)), this.Output[IdxDesc][3]));

                FittedValues[1].Add(Value);
            }
            return FittedValues;
        }


        private cFeedBackMessage Process()
        {
           
            if (this.Input[0].Count == 0)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "Input Data Corrupted";
                return base.FeedBackMessage;
            }


            int IDxDesc = 1;
            if (ConcentrationPos == 1)
                IDxDesc = 0;


            double RelativeError;
            double Top;
            double Bottom;
            double EC50;
            double Slope;
            double AUC;


            double MinConcentration = Math.Log10(this.Input[ConcentrationPos].Min());
            double MaxConcentration = Math.Log10(this.Input[ConcentrationPos].Max());

            if (double.IsInfinity(MinConcentration))
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "Concentration cannot be null";
                return base.FeedBackMessage;
            }

            
            if (MinConcentration == MaxConcentration)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "Min and Max concentrations are similar";
                return base.FeedBackMessage;
            }

            double GlobalMax = this.Input[IDxDesc].Max();
            double GlobalMin = this.Input[IDxDesc].Min();
            if (GlobalMax == GlobalMin)
            {
                base.FeedBackMessage.IsSucceed = false;
                base.FeedBackMessage.Message = "Min and Max readout values are similar";
                return base.FeedBackMessage;
            }

            double BaseEC50 = MaxConcentration - Math.Abs(MaxConcentration - MinConcentration) / 2.0;

            double[] c = new double[] { GlobalMin, GlobalMax , BaseEC50, 1 };
            double epsf = 0;
            double epsx = 0;
            int maxits = 0;
            int info;

            // boundaries
            // bottom / top / EC50 / Slope
            // boundaries
            double[] bnd_up = new double[] { GlobalMax, GlobalMax, MaxConcentration, 50 };
            double[] bnd_low = new double[] { GlobalMin, GlobalMin, MinConcentration, -50 };


            //BaseEC50 = MaxConcentration - Math.Abs(MaxConcentration - MinConcentration) / 2.0;


            // boundaries
            // bottom / top / EC50 / Slope
           // double[] bndl = null;
           // double[] bndu = null;

            // boundaries
           // bndu = new double[] { GlobalMax, GlobalMax, MaxConcentration, 5 };
           // bndl = new double[] { GlobalMin, GlobalMin, MinConcentration, 0.1 };

            alglib.lsfitstate state;
            alglib.lsfitreport rep;
            double diffstep = 1e-12;

            // Fitting without weights
            //alglib.lsfitcreatefg(Concentrations, Values.ToArray(), c, false, out state);

            int NumDimension = 1;
            double[,] ConcentrationAlglib = new double[this.Input[0].Count, NumDimension];
            double[] RawValuesForAlglib = new double[this.Input[0].Count];

            for (int i = 0; i < this.Input[0].Count; i++)
            {
                ConcentrationAlglib[i, 0] = Math.Log10( this.Input[ConcentrationPos][i]);
                RawValuesForAlglib[i] = this.Input[IDxDesc][i];
            }

            alglib.lsfitcreatef(ConcentrationAlglib, RawValuesForAlglib, c, diffstep, out state);
            alglib.lsfitsetcond(state, epsf, epsx, maxits);
            alglib.lsfitsetbc(state, bnd_low , bnd_up);
           //  alglib.lsfitsetscale(state, s);

            alglib.lsfitfit(state, function_SigmoidInhibition, null, null);
            alglib.lsfitresults(state, out info, out c, out rep);
            RelativeError = rep.avgrelerror;
            
            if (c[0] >= c[1])
            {
                Top = c[0];
                Bottom = c[1];
                Slope = -c[3];
              //  c[3] *= -1;
            }
            else
            {
                Top = c[1];
                Bottom = c[0];
                Slope = c[3];
            }
            EC50 = Math.Pow(10,c[2]);
            

            this.Output = new cExtendedTable();
            this.Output.Name = this.Title + "(" + this.Input.Name + ")";
            this.Output.ListRowNames = new List<string>();
            this.Output.ListRowNames.Add("Bottom");
            this.Output.ListRowNames.Add("Top");
            this.Output.ListRowNames.Add("EC50");
            this.Output.ListRowNames.Add("Slope");
            this.Output.ListRowNames.Add("Relative Error");

            this.Output.Add(new cExtendedList());

            this.Output[this.Output.Count - 1].Name = this.Input[this.Output.Count].Name;
            this.Output[this.Output.Count - 1].Tag = this.Input[this.Output.Count].Tag;

            this.Output[this.Output.Count - 1].Add(Bottom);
            this.Output[this.Output.Count - 1].Add(Top);
            this.Output[this.Output.Count - 1].Add(EC50);
            this.Output[this.Output.Count - 1].Add(Slope);
            this.Output[this.Output.Count - 1].Add(RelativeError);


            alglib.autogkstate s;
            
            alglib.autogkreport repInt;

            alglib.autogksmooth(MinConcentration, MaxConcentration, out s);
            alglib.autogkintegrate(s, function_SigmoidInhibitionForIntegration, this.Output);
            alglib.autogkresults(s, out AUC, out repInt);
            
            this.Output.ListRowNames.Add("Area Under Curve");
            this.Output[this.Output.Count - 1].Add(AUC);



            return base.FeedBackMessage;
        }

    }
}
