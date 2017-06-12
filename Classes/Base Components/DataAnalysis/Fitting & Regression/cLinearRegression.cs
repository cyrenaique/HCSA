using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cLinearRegression : cComponent
    {
        cExtendedList ListClass = null;
        cExtendedTable Input = null;
        cExtendedTable Output;

        public cLinearRegression()
        {
            this.Title = "Linear regression";
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

            // ------------- now proceed ------------- 
            Process();

            return FeedBackMessage;
        }


        public cExtendedTable GetOutPut()
        {
            return this.Output;
        }

        private void Process()
        {
            double[,] DataForLDA = Input.CopyToArray();

            //double[,] Basis;
            //double[] s2;
            int Info;
            /*************************************************************************
            Linear least squares fitting.

            QR decomposition is used to reduce task to MxM, then triangular solver  or
            SVD-based solver is used depending on condition number of the  system.  It
            allows to maximize speed and retain decent accuracy.

            INPUT PARAMETERS:
                Y       -   array[0..N-1] Function values in  N  points.
                FMatrix -   a table of basis functions values, array[0..N-1, 0..M-1].
                            FMatrix[I, J] - value of J-th basis function in I-th point.
                N       -   number of points used. N>=1.
                M       -   number of basis functions, M>=1.

            OUTPUT PARAMETERS:
                Info    -   error code:
                            * -4    internal SVD decomposition subroutine failed (very
                                    rare and for degenerate systems only)
                            *  1    task is solved
                C       -   decomposition coefficients, array[0..M-1]
                Rep     -   fitting report. Following fields are set:
                            * Rep.TaskRCond     reciprocal of condition number
                            * R2                non-adjusted coefficient of determination
                                                (non-weighted)
                            * RMSError          rms error on the (X,Y).
                            * AvgError          average error on the (X,Y).
                            * AvgRelError       average relative error on the non-zero Y
                            * MaxError          maximum error
                                                NON-WEIGHTED ERRORS ARE CALCULATED

            ERRORS IN PARAMETERS

            This  solver  also  calculates different kinds of errors in parameters and
            fills corresponding fields of report:
            * Rep.CovPar        covariance matrix for parameters, array[K,K].
            * Rep.ErrPar        errors in parameters, array[K],
                                errpar = sqrt(diag(CovPar))
            * Rep.ErrCurve      vector of fit errors - standard deviations of empirical
                                best-fit curve from "ideal" best-fit curve built  with
                                infinite number of samples, array[N].
                                errcurve = sqrt(diag(F*CovPar*F')),
                                where F is functions matrix.
            * Rep.Noise         vector of per-point estimates of noise, array[N]

            NOTE:       noise in the data is estimated as follows:
                        * for fitting without user-supplied  weights  all  points  are
                          assumed to have same level of noise, which is estimated from
                          the data
                        * for fitting with user-supplied weights we assume that  noise
                          level in I-th point is inversely proportional to Ith weight.
                          Coefficient of proportionality is estimated from the data.

            NOTE:       we apply small amount of regularization when we invert squared
                        Jacobian and calculate covariance matrix. It  guarantees  that
                        algorithm won't divide by zero  during  inversion,  but  skews
                        error estimates a bit (fractional error is about 10^-9).

                        However, we believe that this difference is insignificant  for
                        all practical purposes except for the situation when you  want
                        to compare ALGLIB results with "reference"  implementation  up
                        to the last significant digit.

              -- ALGLIB --
                 Copyright 17.08.2009 by Bochkanov Sergey
            *************************************************************************/


            double[] weights = null;
            int fitResult = 0;
            double[] resultData = new double[this.Input[0].Count];

            //alglib.lsfit.lsfitreport rep = new alglib.lsfit.lsfitreport();
            //alglib.lsfit.lsfitlinear(resultData, DataForLDA, this.Input[0].Count, this.Input.Count, ref fitResult, ref weights, rep);

            //alglib.lsfitstate state;
            int info;
            //double[] c = new double[] { 0, 0, 0, 0 };


            //alglib.lsfitreport Nrep;
            //alglib.lsfitresults(state, out info, out c, out Nrep);
            
           // alglib.fisherldan(DataForLDA, this.Input[0].Count, this.Input.Count - 1, (int)this.Input[this.Input.Count - 1].Max() + 1, out Info, out Basis);
            //Output = new cExtendedTable(Basis);
            alglib.linearmodel LM = null;
            alglib.lrreport Lreport = null;
            alglib.lrbuild(DataForLDA, this.Input[0].Count, this.Input.Count-1, out info, out LM, out Lreport);


                //RelativeError = rep.avgrelerror;
            //LM.innerobj.w[0] = 1;

            double[] Coeff = null;
            int NVars;

            alglib.lrunpack(LM, out Coeff, out NVars);

            cExtendedList CL = new  cExtendedList();
            CL.AddRange(Coeff);
            CL.Name = "Coefficients";

            this.Output = new cExtendedTable(CL);

            this.Output.ListRowNames = new List<string>();
            
            for (int i = 0; i < this.Output[0].Count; i++)
                this.Output.ListRowNames.Add("Coeff_" + i);			 

            double RelativeError = Lreport.avgrelerror;
            this.Output.ListRowNames.Add("Relative Error");
            CL.Add(RelativeError);

            Output.Name = "Linear regression coeff. of (" + this.Input.Name + ")";

            //foreach (var item in Output)
            //{
            //    item.ListTags = new List<object>();
            //    for (int i = 0; i < Output[0].Count; i++)
            //        item.ListTags.Add(this.Input[i].Tag);
            //}


            //for (int IdxLDA = 0; IdxLDA < Output.Count; IdxLDA++)
            //{
            //    Output[IdxLDA].Name = "LDA_" + (IdxLDA + 1);
            //    Output.ListRowNames.Add(this.Input[IdxLDA].Name);
            //}


        }


    }
}
