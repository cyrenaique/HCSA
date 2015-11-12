using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Cell_by_Cell_and_DB.Simulator.Classes
{
    public class cInternalProperty : Object
    {
        public string Name;
    }

    public class cInternalProperties : Dictionary<string, cInternalProperty>
    {
        public void ProcessStimuli(cStimuli Stimuli)
        {
        
        
        }


    }



    public class cListNewAgents : List<cNewAgent>
    {
        public void AddNewAgent(cNewAgent Agent)
        {
            Agent.Parent = (cNewAgent)this;
            this.Add(Agent);
        }

        public void AddNewAgents(cListNewAgents Agents)
        {
            foreach (cNewAgent item in Agents)
            {
                this.AddNewAgent(item);
            }

        }


    }

    public class cNewAgent : cListNewAgents
    {
        //protected cPoint3D Position;
        protected string Name;
        public cStimuli AssociatedStimuli = null;
        public cNewAgent Parent;
        public object Tag;

        public cInternalProperties InternalProperties = new cInternalProperties();

        public string GetName()
        {
            return this.Name;
        }


        public cNewAgent(cPoint3D InitialPosition, cPoint3D InitialVolume, string Name)
        {
            //this.Centroid = new cPoint3D(InitialPosition);
            // first associated two default properties
            //cAgentProperty PositionProperty = new cAgentProperty();
            //PositionProperty.Value = InitialPosition;

            cInternalProperty PositionProperty = new cInternalProperty();
            this.InternalProperties.Add("Position", PositionProperty);

           // cAgentProperty VolumeProperty = new cAgentProperty(); // should be boundaries in the future
            //VolumeProperty.Value = InitialVolume;
            cInternalProperty VolumeProperty = new cInternalProperty();
            this.InternalProperties.Add("Volume", VolumeProperty);

            this.Name = Name;
        }

        public void AddProperty(cInternalProperty NewProperty)
        {
            this.InternalProperties.Add(NewProperty.Name, NewProperty);
        }

        public cPoint3D GetAbsoluteGetPosition()
        {
            cInternalProperty TmpProp = this.InternalProperties["Position"];
            cPoint3D ToBeReturned = new cPoint3D(0,0,0) ;//(cPoint3D)

            cNewAgent TmpAgent = this;

            while (TmpAgent.Parent != null)
            {

               // ToBeReturned += (cPoint3D)TmpAgent.InternalProperties["Position"];
                TmpAgent = TmpAgent.Parent;
            }

            return ToBeReturned;
        }

        void ProcessStimuli()
        {
            cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText("Process " + this.Name + "\n");

            if (AssociatedStimuli == null) return; // no stimuli to be processed

            foreach (var CurrentStimulus in AssociatedStimuli)
            {
                ((cAgent_Cell)this).ProcessCellCycle();
            }
        }

        public void Run()
        {
            // recursive loop that will process all the objects
            
            // we are at the leaf of the tree
            if (this.Count == 0)
            {
            }

            foreach (cNewAgent SubObject in this)
            {
                this.ProcessStimuli();
                SubObject.Run();
            }
        }


 


    }

    public class cAgent_Cell : cNewAgent
    {
        public cAgent_Cell(cPoint3D InitialPosition, cPoint3D InitialVolume, string Name)
            : base(InitialPosition, InitialVolume, Name)
        {

        }

        public void ProcessCellCycle()
        {

        }



    }

}
