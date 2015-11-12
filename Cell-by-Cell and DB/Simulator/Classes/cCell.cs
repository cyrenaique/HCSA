using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Classes;
using System.Drawing;
using HCSAnalyzer.Simulator.Forms;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Simulator.Classes
{
    [Serializable]
    public class cCellPopulation : List<cCell>
    {
        public string Name;
        public string Information;

        /// <summary>
        /// Variables for changing parameter within the plates
        /// </summary>
        public cListVariables AssociatedVariables;

        public cCellPopulation(string Name)
        {
            this.Name = Name;
        }

        public cExtendedList GetVolumes()
        {
            cExtendedList ToReturn = new cExtendedList();
            foreach (var Cell in this)
            {
                ToReturn.Add(Cell.GetVolume());
            }
            return ToReturn;
        }

        public void AddNewCell()
        {
            //cCell NewCell = new cCell(this[0].Type,
        }

    }

    /// <summary>
    /// Define the cell velocity
    /// </summary>
    /// 
    [Serializable]
    public class cVelocity
    {
        cPoint3D Displacement = new cPoint3D(0, 0, 0);
        public double Speed = 1;
        public double Weight_Left; 
        public double Weight_Right;
        public double Weight_Top; 
        public double Weight_Bottom;
        public double Weight_Front;
        public double Weight_Back;
        

        public cVelocity(double[] DisplacementWeights)
        {
            this.Weight_Left = DisplacementWeights[0];
            this.Weight_Right = DisplacementWeights[1];

            this.Weight_Front = DisplacementWeights[2];
            this.Weight_Back = DisplacementWeights[3];

            this.Weight_Top = DisplacementWeights[4];
            this.Weight_Bottom = DisplacementWeights[5];
        }

        public cVelocity(double Front, double Back, double Left, double Right, double Top, double Bottom)
        {
            this.Weight_Left =Left;
            this.Weight_Right = Right;

            this.Weight_Top = Top;
            this.Weight_Bottom = Bottom;

            this.Weight_Front = Front;
            this.Weight_Back = Back;
        }

        public cVelocity()
        {
            this.Weight_Left = 1;
            this.Weight_Right = 1;

            this.Weight_Front = 1;
            this.Weight_Back = 1;

            this.Weight_Top = 0;
            this.Weight_Bottom = 0;
        }

        public cPoint3D GetDisplacement(Random RND)
        {
            this.Displacement.X = this.Speed*(this.Weight_Right * RND.NextDouble() - this.Weight_Left * RND.NextDouble());
            this.Displacement.Y = this.Speed*(this.Weight_Front * RND.NextDouble() - this.Weight_Back * RND.NextDouble());
            this.Displacement.Z = this.Speed*(this.Weight_Top * RND.NextDouble() - this.Weight_Bottom * RND.NextDouble());

            return Displacement;
        }


    }


        /// <summary>
    /// Define the cell cycle
    /// </summary>
    [Serializable]
    public class cCellCycleStage
    {
        public string Name;
       // public List<ce

        public cCellCycleStage(string Name)
        {
            this.Name = Name;
        }




    }

    /// <summary>
    /// Define the cell cycle
    /// </summary>
    [Serializable]
    public class cCellCycle
    {
        public List<cCellCycleStage> ListStages;

        public cCellCycle(List<cCellCycleStage> ListStages)
        {
            this.ListStages.AddRange(ListStages);
        }

        //public 

        public cCellCycle()
        {
        //   // this.ListProba = new List<double>();
        //   //// List<double> ListVolumeOverTime = new List<double>();
        //   // for (int t = 0; t < 20; t++)
        //   //     ListProba.Add(5);
        //   // for (int t = 0; t < 60; t++)
        //   //     ListProba.Add(1);
        //   // for (int t = 0; t < 20; t++)
        //   //     ListProba.Add(3);
        
        }
    }

    //public enum eCellType { REGULAR, CANCER, SCENESCENT, APOPTOTIC, NECROTIC};

    public class cListCellType : List<cCellType>
    {
        public cCellType FindType(string Name)
        {
            foreach (cCellType item in this)
            {
                if (item.Name == Name) return item;
            }
            return null;
        }

        public int FindIdxType(cCellType Type)
        {
            int Idx = -1;
            foreach (cCellType item in this)
            {
                Idx++;
                if (item == Type) return Idx;
            }
            return Idx;
        
        }
    }

    [Serializable]
    public class cTransitionValue
    {
        public cCellType DestType;
        public double Value;

        public cTransitionValue(cCellType DestCellType, double Value)
        {
            this.Value = Value;
            this.DestType = DestCellType;
        }
    }

    [Serializable]
    public class cListTransition : List<cTransitionValue>
    {
        public cTransitionValue FindTransitionFromDestination(string DestTransitionName)
        {
            foreach (cTransitionValue item in this)
            {
                if (item.DestType.Name == DestTransitionName) return item;
            }

            return null;
        }
    
    }

    [Serializable]
    public class cCellType
    {
        public cCellCycle Cycle { get; private set; }
        public Color TypeColor;

        public int CurrentType;
        private List<double> CycleProba = new List<double>();
        public cListTransition ListInitialTransitions = new cListTransition();
        public cVelocity Velocity;

        public string Name { get; private set; }

        public void SetCellCyle(cCellCycle NewCycle)
        {
            this.Cycle = NewCycle;
        }

        public cCellType(int CellType, FormForSimuGenerator Parent)
        {
            this.CurrentType = CellType;

           // for(int i=0;i<100;i++) CycleProba.Add(i);

            switch (CellType)
            {
                case 0:
                    this.TypeColor = Color.LightGreen;
                    this.Name = "Regular";
                    this.Cycle = new cCellCycle();
                    this.Velocity = new cVelocity();
                    this.ListInitialTransitions.Add(new cTransitionValue(this,1));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Cancer"), 0.0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Scenescent"), 0.0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Apoptotic"), 0.0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Necrotic"), 0));
                    this.Velocity.Speed = 20;
                    break;

                case 1:
                    this.TypeColor = Color.Blue;
                    this.Name = "Cancer";
                    this.Cycle = new cCellCycle();
                    this.Velocity = new cVelocity(new double[] { 1, 1, 1, 1, 1, 1 });
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Regular"),0));
                    this.ListInitialTransitions.Add(new cTransitionValue(this, 1));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Scenescent"), 0.0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Apoptotic"), 0.0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Necrotic"), 0.0));
                    this.Velocity.Speed = 40;
                    break;

                case 2:
                    this.TypeColor = Color.Yellow;
                    this.Name = "Scenescent";
                    this.Cycle = new cCellCycle();
                    this.Velocity = new cVelocity(new double[] { 1, 0.95, 1, 1, 1, 1 });
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Regular"),0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Cancer"), 0.0));
                    this.ListInitialTransitions.Add(new cTransitionValue(this, 1));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Apoptotic"), 0.0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Necrotic"), 0));
                    this.Velocity.Speed = 10;
                    break;

                case 3:
                    this.TypeColor = Color.Red;
                    this.Name = "Apoptotic";
                    this.Cycle = new cCellCycle();
                    this.Velocity = new cVelocity(new double[] { 0, 0, 0, 0, 0, 0.1});
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Regular"),0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Cancer"), 0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Scenescent"), 0));
                    this.ListInitialTransitions.Add(new cTransitionValue(this, 0.1));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Necrotic"), 0.0));
                    this.Velocity.Speed = 1;
                    break;

                case 4:
                    this.TypeColor = Color.Wheat;
                    this.Name = "Necrotic";
                    this.Cycle = new cCellCycle();
                    this.Velocity = new cVelocity(new double[] { 0, 0, 0, 0, 0, 0.1});
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Regular"),0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Cancer"), 0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Scenescent"), 0));
                    this.ListInitialTransitions.Add(new cTransitionValue(Parent.ListCellTypes.FindType("Apoptotic"), 0));
                    this.ListInitialTransitions.Add(new cTransitionValue(this, 1));

                    this.Velocity.Speed = 1;
                    break;

             }
        
        }


        public cCellType(string Name, cVelocity Velocity, Color Color, cCellCycle NewCycle, List<cTransitionValue> NewListInitialTransitions)
        {
            this.Velocity = Velocity;
            this.Name = Name;
            this.TypeColor = Color;
            this.Cycle = NewCycle;
            this.ListInitialTransitions.AddRange(NewListInitialTransitions);
        }



    }


    public class cInfoCell
    {
        cCell Cell;

        public cInfoCell(cCell Cell)
        {
            this.Cell = Cell;
        }

        public double GetDistPath()
        {
            double Dist = 0;
            for(int Mem=0;Mem<Cell.PreviousStates.Count-1;Mem++)
            {
                Dist += Cell.PreviousStates[Mem + 1].CentroidPosition.DistTo(Cell.PreviousStates[Mem].CentroidPosition);
            }
            Dist+=Cell.PreviousStates[Cell.PreviousStates.Count-1].CentroidPosition.DistTo(Cell.CentroidPosition);
            return Dist;
        }
    }

    [Serializable]
    public class cCell
    {
        public cPoint3D CentroidPosition;
        public double InitialVolume;
        public cCellType Type;
        public double CurrentCyclePos;
        public string Name = "Cell";

        public List<cCell> PreviousStates = new List<cCell>();

        public bool MemoryOn = false;
   
        public cCell(cCellType intialCellType, cPoint3D InitialPosition, double CurrentCyclePos)
        {
            BasicInit(intialCellType, InitialPosition);
            this.CurrentCyclePos = CurrentCyclePos;
        }

        public cCell(cCell CellToCopy)
        {
            this.CentroidPosition = new cPoint3D(CellToCopy.CentroidPosition.X,
                                                 CellToCopy.CentroidPosition.Y,
                                                 CellToCopy.CentroidPosition.Z);
            this.CurrentCyclePos = CellToCopy.CurrentCyclePos;
        }

        void BasicInit(cCellType intialCellType, cPoint3D InitialPosition)
        {
            this.CentroidPosition = new cPoint3D(InitialPosition.X, InitialPosition.Y, InitialPosition.Z);
            //this.InitialVolume = InitialVolume;
            this.Type = intialCellType;
            //this.CurrentListProbaTransitions = this.Type.ListInitialTransitions;
           // InitProba(intialCellType);
        }

        public c3DSphere Get3DObj()
        {
            return new c3DSphere(this.CentroidPosition,Math.Sqrt(this.GetVolume()), this.Type.TypeColor , 8);
        }

        public double GetVolume()
        {
            double VolumeToReturn = InitialVolume;// *Type.Cycle.ListProba[((int)CurrentCyclePos) % Type.Cycle.ListProba.Count];
            return VolumeToReturn;
        }

       // public cExtendedList CurrentListProbaTransitions = new cExtendedList();

        //void InitProba(cCellType intialCellType)
        //{
        //  //switch (intialCellType.Name)
        //  //  {
        //  //    case "regular":
        //  //          this.ListProbaCellType = new cExtendedList { 1, 0, 0, 0, 0};
        //  //          break;
        //  //      case 1: // cancer
        //  //          this.ListProbaCellType = new cExtendedList { 0, 1, 0, 0, 0};
        //  //          break;
        //  //      case 2: // scenescent
        //  //          this.ListProbaCellType = new cExtendedList { 0, 0, 1, 0, 0};
        //  //          break;
        //  //      case 3: // apoptotic
        //  //          this.ListProbaCellType = new cExtendedList { 0,  0,  0, 1, 0};
        //  //          break;
        //  //      case 4: // necrotic
        //  //          this.ListProbaCellType = new cExtendedList { 0,  0,  0, 0, 1};
        //  //          break;
        //  //  }
        //}

        void ChangeType(cCellType NewType)
        {
            this.Type = NewType;
        }

        public void RunSingleTick(Random RND, FormForSimuGenerator Parent)
        {
            if (MemoryOn)
            {
                cCell CopyCell = new cCell(this);

                this.PreviousStates.Add(CopyCell);
            }

            double RNDForCellState = RND.NextDouble();

            List<double> Cumulated = new List<double>();
            Cumulated.Add(this.Type.ListInitialTransitions[0].Value);
            for (int Idx = 1; Idx < this.Type.ListInitialTransitions.Count; Idx++)
            {
                if (RNDForCellState <= Cumulated[Idx - 1])
                {
                    if((Idx-1)!=this.Type.CurrentType)
                        this.ChangeType(Parent.ListCellTypes[Idx - 1]);
                    //this.Type = new cCellType(Idx-1);
                    //this.Type = eCellType[0];
                    break;
                }
                Cumulated.Add(Cumulated[Idx - 1] + this.Type.ListInitialTransitions[Idx].Value);
            }
            
            cPoint3D CurrentMvt = this.Type.Velocity.GetDisplacement(RND);
            if ((CurrentMvt.X + this.CentroidPosition.X < 0) || (CurrentMvt.X + this.CentroidPosition.X >= Parent.NewWorld.Dimensions.X))
                CurrentMvt.X = 0;
            if ((CurrentMvt.Y + this.CentroidPosition.Y < 0) || (CurrentMvt.Y + this.CentroidPosition.Y >= Parent.NewWorld.Dimensions.Y))
                CurrentMvt.Y = 0;
            if ((CurrentMvt.Z + this.CentroidPosition.Z < 0) || (CurrentMvt.Z + this.CentroidPosition.Z >= Parent.NewWorld.Dimensions.Z))
                CurrentMvt.Z = 0;

            this.CentroidPosition += CurrentMvt;
            CurrentCyclePos++;
        }


    }
}
