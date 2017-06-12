using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.Base_Classes.DataProcessing
{

    public enum eBinaryOperationType { ADD, SUBSTRACT, MULTIPLY, DIVIDE, UNDEFINED }
    public enum eUnaryOperationType { LOG, SQRT, ABS, EXP, UNDEFINED }

    class cArithmetic_DualOperator : cArithmeticOperation
    {

        public eBinaryOperationType OperationType = eBinaryOperationType.ADD;

        public cArithmetic_DualOperator()
        {
            this.Title = base.Title + ":BinaryOperator()";
        }

        public void SetInputData(cExtendedTable Data1, cExtendedTable Data2)
        {
            this.Input1 = Data1;
            this.Input2 = Data2;
        }

        public cFeedBackMessage Run()
        {
            

            if ((this.Input1 == null) || (this.Input2 == null))
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Two input have to be defined";
                return FeedBackMessage;
            }
            if ((this.Input1.Count != this.Input2.Count) || (this.Input1[0].Count != this.Input2[0].Count))
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "Table dimensions do not match";
                return FeedBackMessage;
            }

            Process();

            return FeedBackMessage;
        }

        void Process()
        {
            if (this.OperationType == eBinaryOperationType.ADD)
            {
                this.Output = new cExtendedTable(this.Input1);
                this.Output.Name = "(" + this.Input1.Name + "+" + this.Input1.Name +")";

                for (int Col = 0; Col < this.Input1.Count; Col++)
                    for (int Row = 0; Row < this.Input1[0].Count; Row++)
                        this.Output[Col][Row] += this.Input2[Col][Row];

                return;
            }
            if (this.OperationType == eBinaryOperationType.SUBSTRACT)
            {
                this.Output = new cExtendedTable(this.Input1);
                this.Output.Name = "(" + this.Input1.Name + "-" + this.Input1.Name + ")";

                for (int Col = 0; Col < this.Input1.Count; Col++)
                    for (int Row = 0; Row < this.Input1[0].Count; Row++)
                        this.Output[Col][Row] -= this.Input2[Col][Row];

                return;
            }
            if (this.OperationType == eBinaryOperationType.MULTIPLY)
            {
                this.Output = new cExtendedTable(this.Input1);
                this.Output.Name = "(" + this.Input1.Name + "x" + this.Input1.Name + ")";

                for (int Col = 0; Col < this.Input1.Count; Col++)
                    for (int Row = 0; Row < this.Input1[0].Count; Row++)
                        this.Output[Col][Row] *= this.Input2[Col][Row];

                return;
            }
            if (this.OperationType == eBinaryOperationType.SUBSTRACT)
            {
                this.Output = new cExtendedTable(this.Input1);
                this.Output.Name = "(" + this.Input1.Name + "/" + this.Input1.Name + ")";

                for (int Col = 0; Col < this.Input1.Count; Col++)
                    for (int Row = 0; Row < this.Input1[0].Count; Row++)
                        this.Output[Col][Row] /= this.Input2[Col][Row];

                return;
            }





        }

    }
}
