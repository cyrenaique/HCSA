using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using weka.core;


namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    class cProjectorPCA : cProjectorComp
    {
        cExtendedTable Input = null;

        public cProjectorPCA()
        {
            this.Title = "PCA projection coefficients";
        }

        public void SetInputData(cExtendedTable MyData)
        {
            this.Input = MyData;
        }

        private void Process()
        {
            double[,] DataForPCA = Input.CopyToArray();

            double[,] Basis;
            double[] s2;
            int Info;

            /*************************************************************************
            Principal components analysis

            Subroutine  builds  orthogonal  basis  where  first  axis  corresponds  to
            direction with maximum variance, second axis maximizes variance in subspace
            orthogonal to first axis and so on.

            It should be noted that, unlike LDA, PCA does not use class labels.

            INPUT PARAMETERS:
                X           -   dataset, array[0..NPoints-1,0..NVars-1].
                                matrix contains ONLY INDEPENDENT VARIABLES.
                NPoints     -   dataset size, NPoints>=0
                NVars       -   number of independent variables, NVars>=1

            ----------------------------------------------
             * Info        -   return code:
                                * -4, if SVD subroutine haven't converged
                                * -1, if wrong parameters has been passed (NPoints<0,
                                      NVars<1)
                                *  1, if task is solved
                S2          -   array[0..NVars-1]. variance values corresponding
                                to basis vectors.
                V           -   array[0..NVars-1,0..NVars-1]
                                matrix, whose columns store basis vectors.

              -- ALGLIB --
                 Copyright 25.08.2008 by Bochkanov Sergey
            *************************************************************************/
            alglib.pcabuildbasis(DataForPCA, Input[0].Count, Input.Count, out Info, out s2, out Basis);

            base.NewBasis = new cExtendedTable(Basis);
            base.NewBasis.Name = "PCA coeff. values of (" + Input.Name + ")";

            foreach (var item in base.NewBasis)
            {
                item.ListTags = new List<object>();

                for (int i = 0; i < base.NewBasis[0].Count; i++)
                {
                    item.ListTags.Add(this.Input[i].Tag);
                }
            }



            for (int IdxPCA = 0; IdxPCA < base.NewBasis.Count; IdxPCA++)
            {
                NewBasis[IdxPCA].Name = "PCA_"+(IdxPCA+1);
                NewBasis.ListRowNames.Add(Input[IdxPCA].Name);
            }


        }

        public cFeedBackMessage Run()
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No Data Table defined.";
                return FeedBackMessage;
            }

            // ------------- now proceed ------------- 

            Process();

            return FeedBackMessage;
        }

    }
}
