using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using System.Windows.Forms;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.General_Types;
using System.Diagnostics;
using HCSAnalyzer.Classes.Base_Components.GUI;
using System.IO;

namespace HCSAnalyzer.Classes
{

    /// <summary>
    /// Contains low level information and GUI for component display
    /// </summary>
    public abstract class cComponent
    {

        protected object InputData = null;

        //  public static cGlobalInfo GlobalInfo;
        protected cFeedBackMessage FeedBackMessage;

        protected Color CurrentColor;
        //  public cPoint3D Position;

        public string Title;
        protected string Info;
        Stopwatch watch;
        public bool IsWriteLog = true;

        public cListProperty ListProperties = new cListProperty();

        public cComponent()
        {
            FeedBackMessage = new cFeedBackMessage(true, this);
        }

        public string GetInfo()
        {
            return Info;
        }


        void WriteToLog()
        {

        //    string FileName = "HCSA.log";
        //    StreamWriter stream = new StreamWriter(FileName, true, System.Text.Encoding.ASCII);

        //    stream.WriteLine(this.Title);
        //    stream.WriteLine(DateTime.Now);
        //    if(this.GetInfo()!=null)
        //        stream.WriteLine(this.Info);

        //    if ((ListProperties != null) && (ListProperties.Count > 0))
        //    {
        //        foreach (var item in ListProperties)
        //        {
        //            item.PropertyType.GetInfo();
        //            stream.WriteLine(item.PropertyType.GetInfo());
        //            if(item.GetValue()==null)
        //                stream.WriteLine(" Value: NULL");
        //            else
        //             stream.WriteLine(" Value: " + item.GetValue().ToString());

        //        }
        //    }
        //    stream.WriteLine();

        //    stream.Dispose();
        }

        protected bool Start()
        {
            bool IsGUIRequested = false;

            if (this.IsWriteLog) this.WriteToLog();

            foreach (var item in ListProperties)
            {
                if (item.IsGUIforValue)
                {
                    IsGUIRequested = true;
                    break;
                }
            }

            double MaxWidth = 0;
            double TotalHeight = 20;

            #region GUI
            if (IsGUIRequested)
            {
                FormGUIForProperties GUI = new FormGUIForProperties();
                GroupBox GB = new GroupBox();
                GB.Text = this.Title;

                foreach (var item in ListProperties)
                {
                    if (item.IsGUIforValue)
                    {
                        Control C = item.GetAssociatedGUI();
                        double TmpWidth = C.Width;
                        C.Location = new Point(6, (int)TotalHeight);
                        // GUI.panelForControls.Controls.Add(C);
                        GB.Controls.Add(C);

                        TotalHeight += C.Height + 5;
                        if (TmpWidth > MaxWidth) MaxWidth = TmpWidth;
                    }
                }

                GB.Width = (int)MaxWidth + 20;
                GB.Height = (int)TotalHeight + 10;

                GUI.panelForControls.Controls.Add(GB);

                GUI.panelForControls.Width = (int)(MaxWidth);
                GUI.panelForControls.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                                                | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
                GUI.Width = GB.Width + 54;

                if (GUI.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in /*GUI.panelForControls.Controls*/GB.Controls)
                    {
                        if (item.GetType() == typeof(Panel))
                        {
                            Panel tmpPanel = (Panel)item;
                            if ((tmpPanel.Tag != null) && (tmpPanel.Tag.GetType() == typeof(cProperty)))
                            {
                                cProperty TmpProp = (cProperty)tmpPanel.Tag;
                                if (TmpProp.PropertyType.Type == eDataType.DOUBLE) TmpProp.SetNewValue((double)(((NumericUpDown)tmpPanel.Controls[1]).Value));
                                else if (TmpProp.PropertyType.Type == eDataType.INTEGER) TmpProp.SetNewValue((int)(((NumericUpDown)tmpPanel.Controls[1]).Value));
                                else if (TmpProp.PropertyType.Type == eDataType.BOOL) TmpProp.SetNewValue(((CheckBox)tmpPanel.Controls[1]).Checked);
                            }
                        }
                    }
                    watch = new Stopwatch();
                    watch.Start();
                    return true;
                }
                else
                {
                    watch = new Stopwatch();
                    watch.Start();
                    return false;
                }
            }
            #endregion

            watch = new Stopwatch();
            watch.Start();

            cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(this.Title + ": Start...\n");

            return true;

        }

        protected void End()
        {
            watch.Stop();
            FeedBackMessage.ExecutionTime = watch.Elapsed.Milliseconds;
            cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(this.Title + ": End (" + FeedBackMessage.ExecutionTime + " ms)\n");

        }

        protected void GenerateError(string ErrorMessage)
        {
            this.FeedBackMessage.IsSucceed = false;
            this.FeedBackMessage.Message = ErrorMessage;

            cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(this.Title + ": " + ErrorMessage + "\n");

            this.End();
        }


    }



    //public class cInstancecComponent : cComponent
    //{
    //    public cInstancecComponent(cGlobalInfo GI)
    //    {
    //        cComponent.GlobalInfo
    //    }


    //}


    /// <summary>
    /// Data Filtering Component
    /// </summary>
    public abstract class cDataAnalysisComponent : cComponent
    {
        public cDataAnalysisComponent()
        {
            CurrentColor = Color.DarkGreen;
        }
    }

    public abstract class cDataDisplay : cComponent
    {
        protected int Width = 200;
        protected int Height = 100;
        protected cExtendedControl CurrentPanel;
        public ContextMenuStrip CompleteMenu = new ContextMenuStrip();
        public bool IsDisplayInfo = false;
        public cComponent Sender;
        //public cDesignerSplitter SplitterForInfo = new cDesignerSplitter();

        public cDataDisplay()
        {
            CurrentColor = Color.DarkRed;
            this.CurrentPanel = new cExtendedControl();
            this.CurrentPanel.Width = Width;
            this.CurrentPanel.Height = Height;
        }



        public cExtendedControl GetOutPut()
        {
            if ((IsDisplayInfo) && (Sender != null))
            {
                cDesignerSplitter SplitterForInfo = new cDesignerSplitter();
                SplitterForInfo.Orientation = Orientation.Horizontal;

                SplitterForInfo.SetInputData(CurrentPanel);

                cViewertext VT = new cViewertext();
                VT.SetInputData(Sender.GetInfo());
                VT.Run();

                SplitterForInfo.SetInputData(VT.GetOutPut());
                SplitterForInfo.Run();

                return SplitterForInfo.GetOutPut();
            }

            return CurrentPanel;
        }

    }

    public abstract class cDataGenerator : cComponent
    {
        public cDataGenerator()
        {
            CurrentColor = Color.DarkBlue;
        }
    }





}
