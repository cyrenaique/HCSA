using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cProjectorLDA : cProjectorComp
    {
        cExtendedList ListClass = null;
        cExtendedTable Input = null;

        public cProjectorLDA()
        {
            this.Title = "LDA projection coefficients";
        }


        public void SetInputData(cExtendedTable Input)
        {
            this.Input = Input;
        }

        public cFeedBackMessage Run()
        {
            //cFeedBackMessage FeedBackMessage;// = new cFeedBackMessage();

            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data table defined.";
                return FeedBackMessage;
            }

            // ------------- now proceed ------------- 
            Process();

            if (NewBasis.Count == 0)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "LDA basis construction not achieved.\nCheck your data validity.";
                return FeedBackMessage;
            
            }

            return FeedBackMessage;
        }


        private void Process()
        {
            double[,] DataForLDA = Input.CopyToArray();

            double[,] Basis;
            //double[] s2;
            int Info;

            /*************************************************************************
            N-dimensional multiclass Fisher LDA

            Subroutine finds coefficients of linear combinations which optimally separates
            training set on classes. It returns N-dimensional basis whose vector are sorted
            by quality of training set separation (in descending order).

            INPUT PARAMETERS:
                XY          -   training set, array[0..NPoints-1,0..NVars].
                                First NVars columns store values of independent
                                variables, next column stores number of class (from 0
                                to NClasses-1) which dataset element belongs to. Fractional
                                values are rounded to nearest integer.
                NPoints     -   training set size, NPoints>=0
                NVars       -   number of independent variables, NVars>=1
                NClasses    -   number of classes, NClasses>=2


            OUTPUT PARAMETERS:
                Info        -   return code:
                                * -4, if internal EVD subroutine hasn't converged
                                * -2, if there is a point with class number
                                      outside of [0..NClasses-1].
                                * -1, if incorrect parameters was passed (NPoints<0,
                                      NVars<1, NClasses<2)
                                *  1, if task has been solved
                                *  2, if there was a multicollinearity in training set,
                                      but task has been solved.
                W           -   basis, array[0..NVars-1,0..NVars-1]
                                columns of matrix stores basis vectors, sorted by
                                quality of training set separation (in descending order)

              -- ALGLIB --
                 Copyright 31.05.2008 by Bochkanov Sergey
            *************************************************************************/
           // alglib.pcabuildbasis(DataForLDA, MyData[0].Count, MyData.Count, out Info, out s2, out Basis);
            alglib.fisherldan(DataForLDA, this.Input[0].Count, this.Input.Count-1, (int)this.Input[this.Input.Count-1].Max()+1, out Info, out Basis);


            base.NewBasis = new cExtendedTable(Basis);
            base.NewBasis.Name = "LDA coeff. values of (" + this.Input.Name + ")";

            
            foreach (var item in base.NewBasis)
            {
                item.ListTags = new List<object>();

                for (int i = 0; i < base.NewBasis[0].Count; i++)
                {
                   item.ListTags.Add(this.Input[i].Tag);
                }
            }


            for (int IdxLDA = 0; IdxLDA < base.NewBasis.Count; IdxLDA++)
            {
                NewBasis[IdxLDA].Name = "LDA_" + (IdxLDA + 1);
                NewBasis.ListRowNames.Add(this.Input[IdxLDA].Name);
            }


        }


    }
}
