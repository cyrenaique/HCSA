using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataAnalysis
{
    public abstract class cProjectorComp : cDataAnalysisComponent
    {
        protected cExtendedTable NewBasis;

        public cExtendedTable GetOutPut()
        {
            return this.NewBasis;
        }


    }
}
