using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using HCSAnalyzer.Classes._3D;

namespace IM3_Plugin3
{



    public partial class FormForControl : Form
    {
        bool IsMaster = false;
        public cListCtrls AssociatedCtrlsValue = new cListCtrls();


        public List<string> ListDetectedSpots = new List<string>();
        private cThumbnail AssociatedThumbnail;


        public void DisplayThumbnail(Form FormDestination)
        {
            AssociatedThumbnail.Display(FormDestination);
        }


        public void DisplayDone()
        {
            this.richTextBoxInfo.AppendText(this.Name + ": done");

        }

        public bool IsMasterObject()
        {
            return this.IsMaster;
        }

        public List<cInteractive3DObject> AssociatedBiologicalObjectList = new List<cInteractive3DObject>();
        public Plugin3D Parent;


        public List<Form> ListFormsAssociated = new List<Form>();

        public FormForArrow ForArrow = new FormForArrow();
        public FormForArrow ForText = new FormForArrow();
        //public FormForSingleValue SpotRadius = new FormForSingleValue();
        public FormForSingleValue ColorOpacity = new FormForSingleValue();
        public FormForLinkToMasterInfo WindowDistanceToMaster = new FormForLinkToMasterInfo();
        public FormForMaster WindowForMaster = new FormForMaster();
        public FormForInOut InOrOut = new FormForInOut();
        public FormForVolumeDetection WindowForVolumeDetection;// = new FormForVolumeDetection();
        public FormForPreProcessings WindowForPreProcessing;
        public FormForSpotDetection WindowForSpotDetection;
        public FormFor3DVolumeRendering WindowOpacity;
        // public IM3_Plugin3 analysis3D;

        private void BuilEmptyControl(Plugin3D Parent)
        {
            InitializeComponent();
            this.Parent = Parent;

            ForText.numericUpDownArrowScale.Value = (decimal)0.9;
            ForText.Text = "Text Scale";

            ForArrow.numericUpDownArrowScale.Value = (decimal)4;

            ColorOpacity.Text = "Opacity";

            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;

            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.buttonChangeColorPositive, "Right click to change opacity");
            toolTip1.SetToolTip(this.checkBoxIsDisplayName, "Right click to change scale and color");
            toolTip1.SetToolTip(this.checkIsBoxPointingArrow, "Right click to change scale and color");
            toolTip1.SetToolTip(this.RadioButtonIsSpot, "Right click to change spot radius");
            toolTip1.SetToolTip(this.comboBoxContainer, "Right click to change the mode");
            toolTip1.SetToolTip(this.labelLinkToTheMaster, "Right click to change parameter");


            WindowForVolumeDetection = new FormForVolumeDetection(this);
            WindowForPreProcessing = new FormForPreProcessings(this);
            WindowForSpotDetection = new FormForSpotDetection(this);
            WindowOpacity = new FormFor3DVolumeRendering(this);
            //panelForVolumeRendering.Controls.Clear();
            //WPF_LutCtrl animationLayer = new WPF_LutCtrl();

            ///* container that will host our WPF control, we set it using 
            // * the Child property */
            //ElementHost host = new ElementHost()
            //{
            //    BackColor = Color.Transparent,
            //    Child = animationLayer,
            //    Dock = DockStyle.Fill,
            //};
            //panelForVolumeRendering.Controls.Add(host);

            this.ListFormsAssociated.Add(this);
            this.ListFormsAssociated.Add(ForArrow);
            this.ListFormsAssociated.Add(ForText);
            this.ListFormsAssociated.Add(ColorOpacity);
            this.ListFormsAssociated.Add(WindowDistanceToMaster);
            this.ListFormsAssociated.Add(WindowForMaster);
            this.ListFormsAssociated.Add(InOrOut);
            this.ListFormsAssociated.Add(WindowForVolumeDetection);
            this.ListFormsAssociated.Add(WindowForPreProcessing);
            this.ListFormsAssociated.Add(WindowForSpotDetection);
            this.ListFormsAssociated.Add(WindowOpacity);
        }


        public FormForControl(Plugin3D Parent, cListCtrls NewListCtrls)
        {
            BuilEmptyControl(Parent);


            System.Windows.Forms.Panel TmpFormPanel = this.panelGroup;

            foreach (cControl CurrentControl in NewListCtrls)
            {
                if (CurrentControl.ParentFormName == TmpFormPanel.Name) // we are on the right form
                {
                    for (int IdxCtrl = 0; IdxCtrl < TmpFormPanel.Controls.Count; IdxCtrl++)
                    {
                        if (TmpFormPanel.Controls[IdxCtrl].Name == CurrentControl.CurrentCtrlName)
                        {
                            if (CurrentControl.CurrentCtrlType == "NumericUpDown")
                                ((NumericUpDown)TmpFormPanel.Controls[IdxCtrl]).Value = (decimal)CurrentControl.Value;
                            if (CurrentControl.CurrentCtrlType == "TextBox")
                                ((TextBox)TmpFormPanel.Controls[IdxCtrl]).Text = CurrentControl.Text;
                            if (CurrentControl.CurrentCtrlType == "CheckBox")
                                ((CheckBox)TmpFormPanel.Controls[IdxCtrl]).Checked = CurrentControl.Status;
                            if (CurrentControl.CurrentCtrlType == "RadioButton")
                                ((RadioButton)TmpFormPanel.Controls[IdxCtrl]).Checked = CurrentControl.Status;
                            if (CurrentControl.CurrentCtrlType == "TrackBar")
                                ((TrackBar)TmpFormPanel.Controls[IdxCtrl]).Value = (int)CurrentControl.Value;

                        }

                        foreach (Control ChildContrl in TmpFormPanel.Controls[IdxCtrl].Controls)
                        {
                            if (ChildContrl.Name == CurrentControl.CurrentCtrlName)
                            {
                                if (CurrentControl.CurrentCtrlType == "NumericUpDown")
                                    ((NumericUpDown)ChildContrl).Value = (decimal)CurrentControl.Value;
                                if (CurrentControl.CurrentCtrlType == "TextBox")
                                    ((TextBox)ChildContrl).Text = CurrentControl.Text;
                                if (CurrentControl.CurrentCtrlType == "CheckBox")
                                    ((CheckBox)ChildContrl).Checked = CurrentControl.Status;
                                if (CurrentControl.CurrentCtrlType == "RadioButton")
                                    ((RadioButton)ChildContrl).Checked = CurrentControl.Status;
                                if (CurrentControl.CurrentCtrlType == "TrackBar")
                                    ((TrackBar)ChildContrl).Value = (int)CurrentControl.Value;
                            }

                        }
                    }
                }
            }









            foreach (Form TmpForm in this.ListFormsAssociated)
            {
                foreach (cControl CurrentControl in NewListCtrls)
                {
                    if (CurrentControl.ParentFormName == TmpForm.Name) // we are on the right form
                    {
                        for (int IdxCtrl = 0; IdxCtrl < TmpForm.Controls.Count; IdxCtrl++)
                        {
                            if (TmpForm.Controls[IdxCtrl].Name == CurrentControl.CurrentCtrlName)
                            {
                                if (CurrentControl.CurrentCtrlType == "NumericUpDown")
                                    ((NumericUpDown)TmpForm.Controls[IdxCtrl]).Value = (decimal)CurrentControl.Value;
                                if (CurrentControl.CurrentCtrlType == "TextBox")
                                    ((TextBox)TmpForm.Controls[IdxCtrl]).Text = CurrentControl.Text;
                                if (CurrentControl.CurrentCtrlType == "CheckBox")
                                    ((CheckBox)TmpForm.Controls[IdxCtrl]).Checked = CurrentControl.Status;
                                if (CurrentControl.CurrentCtrlType == "RadioButton")
                                    ((RadioButton)TmpForm.Controls[IdxCtrl]).Checked = CurrentControl.Status;
                                if (CurrentControl.CurrentCtrlType == "TrackBar")
                                    ((TrackBar)TmpForm.Controls[IdxCtrl]).Value = (int)CurrentControl.Value;

                            }

                            foreach (Control ChildContrl in TmpForm.Controls[IdxCtrl].Controls)
                            {
                                if (ChildContrl.Name == CurrentControl.CurrentCtrlName)
                                {
                                    if (CurrentControl.CurrentCtrlType == "NumericUpDown")
                                        ((NumericUpDown)ChildContrl).Value = (decimal)CurrentControl.Value;
                                    if (CurrentControl.CurrentCtrlType == "TextBox")
                                        ((TextBox)ChildContrl).Text = CurrentControl.Text;
                                    if (CurrentControl.CurrentCtrlType == "CheckBox")
                                        ((CheckBox)ChildContrl).Checked = CurrentControl.Status;
                                    if (CurrentControl.CurrentCtrlType == "RadioButton")
                                        ((RadioButton)ChildContrl).Checked = CurrentControl.Status;
                                    if (CurrentControl.CurrentCtrlType == "TrackBar")
                                        ((TrackBar)ChildContrl).Value = (int)CurrentControl.Value;
                                }

                            }
                        }
                    }
                    //  if(TmpForm.Name
                    //CurrentControl.GetType().Name
                }
            }


        }


        public FormForControl(Plugin3D Parent, cPoint3D ThumbnailPos)
        {
            BuilEmptyControl(Parent);
            AssociatedThumbnail = new cThumbnail(ThumbnailPos, this, this);
        }

        public System.Windows.Forms.ImageList ImList;

        public void UpdateContainers(List<string> AvailableContainers)
        {
            comboBoxContainer.Items.Clear();

            foreach (string container in AvailableContainers)
            {
                comboBoxContainer.Items.Add(container);
            }
        }


        public cListCtrls GenerateCurrentStatus()
        {
            AssociatedCtrlsValue.Clear();



            for (int ContrlPanelIdx = 0; ContrlPanelIdx < this.panelGroup.Controls.Count; ContrlPanelIdx++)
            {

                AssociatedCtrlsValue.Add(new cControl(this.panelGroup.Controls[ContrlPanelIdx], this.panelGroup));
            }


            int IdxForm = 0;
            foreach (Form CurrentForm in this.ListFormsAssociated)
            {
                int IdxCtrl = 0;
                foreach (Control CurrentControl in CurrentForm.Controls)
                {
                    foreach (Control ChildContrl in CurrentControl.Controls)
                    {
                        if ((ChildContrl.GetType().Name == "NumericUpDown") || (ChildContrl.GetType().Name == "TextBox") || (ChildContrl.GetType().Name == "CheckBox") || (ChildContrl.GetType().Name == "RadioButton") || (ChildContrl.GetType().Name == "TrackBar"))
                        {
                            AssociatedCtrlsValue.Add(new cControl(ChildContrl, CurrentForm));
                        }
                    }

                    if ((CurrentControl.GetType().Name == "NumericUpDown") || (CurrentControl.GetType().Name == "TextBox") || (CurrentControl.GetType().Name == "CheckBox") || (CurrentControl.GetType().Name == "RadioButton") || (CurrentControl.GetType().Name == "TrackBar"))
                    {
                        AssociatedCtrlsValue.Add(new cControl(CurrentControl, CurrentForm));
                    }

                    IdxCtrl++;
                }

                IdxForm++;
            }
            return this.AssociatedCtrlsValue;
        }



        private void AddinfoToRichTextBox()
        {
            AssociatedCtrlsValue.Clear();

            richTextBoxInfo.AppendText("\t" + this.ListFormsAssociated.Count + " forms associated.\n");

            int IdxForm = 0;
            foreach (Form CurrentForm in this.ListFormsAssociated)
            {
                richTextBoxInfo.AppendText("\tForm " + IdxForm + " : " + CurrentForm.GetType() + "\n");

                int IdxCtrl = 0;
                foreach (Control CurrentControl in CurrentForm.Controls)
                {
                    richTextBoxInfo.AppendText("\t\tControl " + IdxCtrl + " : " + CurrentControl.GetType().Name);


                    foreach (Control ChildContrl in CurrentControl.Controls)
                    {
                        if ((ChildContrl.GetType().Name == "NumericUpDown") || (ChildContrl.GetType().Name == "TextBox") || (ChildContrl.GetType().Name == "CheckBox") || (ChildContrl.GetType().Name == "RadioButton") || (ChildContrl.GetType().Name == "TrackBar"))
                        {
                            //cControl TmpCtrl = new cControl(CurrentControl, CurrentForm);
                            AssociatedCtrlsValue.Add(new cControl(ChildContrl, CurrentForm));
                        }
                    }

                    if ((CurrentControl.GetType().Name == "NumericUpDown") || (CurrentControl.GetType().Name == "TextBox") || (CurrentControl.GetType().Name == "CheckBox") || (CurrentControl.GetType().Name == "RadioButton") || (CurrentControl.GetType().Name == "TrackBar"))
                    {
                        //cControl TmpCtrl = new cControl(CurrentControl, CurrentForm);
                        AssociatedCtrlsValue.Add(new cControl(CurrentControl, CurrentForm));
                    }



                    //richTextBoxInfo.AppendText("\t\t Name: " + CurrentControl.Name +   "\n");

                    //  if(CurrentControl.GetType().Attributes

                    //    richTextBoxInfo.AppendText("\t\t Value: " + CurrentControl. + "\n");


                    richTextBoxInfo.AppendText("\n");

                    IdxCtrl++;
                }

                richTextBoxInfo.AppendText("\n");
                //    richTextBoxInfo.AppendText("\t\tName  : " + CurrentForm.Name + "\n");
                IdxForm++;
            }

        }

        private void UpdateDisplay()
        {
            richTextBoxInfo.Clear();

            // List<Control> LChart = new List<Control>();

            if (radioButtonIsVolume.Checked)
            {
                richTextBoxInfo.Clear();
                richTextBoxInfo.AppendText("Volume detection:");
                //panelForVolume.Location = new Point(20, 5);
                //LChart.Add(panelForVolume);
                checkBoxIsDisplayName.Visible = true;
                checkIsBoxPointingArrow.Visible = true;
                comboBoxContainer.Enabled = true;
                ColorOpacity.numericUpDownValue.Value = (decimal)0.6;
            }
            else if (RadioButtonIsSpot.Checked)
            {

                richTextBoxInfo.Clear();
                richTextBoxInfo.AppendText("Spot detection:");
                //panelForSpot.Location = new Point(20,5);
                //LChart.Add(panelForSpot);
                checkBoxIsDisplayName.Visible = true;
                checkIsBoxPointingArrow.Visible = true;
                comboBoxContainer.Enabled = true;
                ColorOpacity.numericUpDownValue.Value = (decimal)1;
            }
            else if (radioButtonIsVolumeRendering.Checked)
            {

                richTextBoxInfo.Clear();
                richTextBoxInfo.AppendText("Volume Rendering:");
                //panelForVolumeRendering.Location = new Point(20, 5);
                //LChart.Add(panelForVolumeRendering);
                checkBoxIsDisplayName.Visible = false;
                checkIsBoxPointingArrow.Visible = false;
                comboBoxContainer.Enabled = false;
                // ColorOpacity.numericUpDownValue.Value = (decimal)0.4;

            }
            //panelForInfo.Controls.AddRange(LChart.ToArray());

            if(this.AssociatedThumbnail!=null)  this.AssociatedThumbnail.UpdateAppearance();

            this.Parent.UpDateContainers();
            this.Parent.UpDateSpotList();

            AddinfoToRichTextBox();
        }

        private void radioButtonIsVolumeRendering_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
            this.SetAsNotMasterItem(sender, e);
        }

        private void buttonChangeColorPositive_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() != DialogResult.OK) return;
            Color backgroundColor = colorDialog.Color;

            this.buttonChangeColorPositive.BackColor = backgroundColor;
            this.buttonChangeColorPositive.Refresh();

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (ImList == null) return;
            Image ImageToDisp = ImList.Images[textBoxName.Text + ".png"];
            if (ImageToDisp == null)
                ImageToDisp = ImList.Images["what.png"];
            panelforImage.BackgroundImage = ImageToDisp;
            panelforImage.BackgroundImageLayout = ImageLayout.Stretch;
            if (Parent != null) Parent.UpDateContainers();

            if (textBoxName.Text == "centriole") RadioButtonIsSpot.Checked = true;
            if (textBoxName.Text == "Nucleus") radioButtonIsVolume.Checked = true;
            if (textBoxName.Text == "foci") RadioButtonIsSpot.Checked = true;

        }

        private void checkIsBoxPointingArrow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) ForArrow.ShowDialog();
        }

        private void checkBoxIsDisplayName_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) ForText.ShowDialog();
        }


        private void buttonChangeColorPositive_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) ColorOpacity.ShowDialog();
        }

        private void comboBoxContainer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) InOrOut.ShowDialog();
        }

        private void label8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) InOrOut.ShowDialog();
        }

        private void panelGroup_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Right) return;

            // a volume render cannot be set as master
            if (this.radioButtonIsVolumeRendering.Checked) return;

            ContextMenuStrip contextMenuStripActorPicker = new ContextMenuStrip();

            ToolStripMenuItem SetAsMasterItem = new ToolStripMenuItem("Set as master");
            SetAsMasterItem.Click += new System.EventHandler(this.SetAsMasterItem);

            ToolStripMenuItem SetAsNotMasterItem = new ToolStripMenuItem("un-master");
            SetAsNotMasterItem.Click += new System.EventHandler(this.SetAsNotMasterItem);


            //ToolStripMenuItem SelectAllItem = new ToolStripMenuItem("Select all");
            //SelectAllItem.Click += new System.EventHandler(this.SelectAllItem);

            //ToolStripSeparator SepratorStrip = new ToolStripSeparator();

            //Point locationOnForm = checkedListBoxActiveDescriptors.FindForm().PointToClient(Control.MousePosition);

            //int VertPos = locationOnForm.Y - 163;
            //int ItemHeight = checkedListBoxActiveDescriptors.GetItemHeight(0);
            //int IdxItem = VertPos / ItemHeight;
            //if ((IdxItem < CompleteScreening.ListDescriptors.Count) && ((IdxItem >= 0)))
            //{
            //    ToolStripMenuItem ToolStripMenuItems = new ToolStripMenuItem(CompleteScreening.ListDescriptors[IdxItem].GetName());

            //    ToolStripMenuItem InfoDescItem = new ToolStripMenuItem("Info");
            //    IntToTransfer = IdxItem;
            //    InfoDescItem.Click += new System.EventHandler(this.InfoDescItem);
            //    ToolStripMenuItems.DropDownItems.Add(InfoDescItem);

            //    if (CompleteScreening.ListDescriptors.Count >= 2)
            //    {
            //        ToolStripMenuItem RemoveDescItem = new ToolStripMenuItem("Remove");
            //        RemoveDescItem.Click += new System.EventHandler(this.RemoveDescItem);
            //        ToolStripMenuItems.DropDownItems.Add(RemoveDescItem);
            //    }

            //    if (CompleteScreening.ListDescriptors[IntToTransfer].GetBinNumber() > 1)
            //    {
            //        ToolStripMenuItem SplitDescItem = new ToolStripMenuItem("Split");
            //        SplitDescItem.Click += new System.EventHandler(this.SplitDescItem);
            //        ToolStripMenuItems.DropDownItems.Add(SplitDescItem);
            //    }
            //    contextMenuStripActorPicker.Items.AddRange(new ToolStripItem[] { UnselectItem, SelectAllItem, ToolStripMenuItems });
            //}
            //else
            //{

            contextMenuStripActorPicker.Items.AddRange(new ToolStripItem[] { SetAsMasterItem, SetAsNotMasterItem });
            //}
            contextMenuStripActorPicker.Show(Control.MousePosition);
        }

        public void SetAsMaster()
        {
            IsMaster = true;
            panelForMaster.Visible = true;
            Image ImageToDisp = ImList.Images["master.png"];
            panelForMaster.BackgroundImage = ImageToDisp;
            panelForMaster.BackgroundImageLayout = ImageLayout.Stretch;
        }

        void SetAsMasterItem(object sender, EventArgs e)
        {
            SetAsMaster();
            if (Parent != null) Parent.UpDateMasters(this);
        }

        public void SetAsNonMaster()
        {
            IsMaster = false;
            panelForMaster.Visible = false;
        }

        void SetAsNotMasterItem(object sender, EventArgs e)
        {
            SetAsNonMaster();
        }

        private void labelLinkToTheMaster_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) WindowDistanceToMaster.ShowDialog();
        }

        private void panelForMaster_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == System.Windows.Forms.MouseButtons.Right) && (this.IsMaster)) WindowForMaster.ShowDialog();
        }


        private void UpDateDisplayMasterLinks()
        {
            if (comboBoxLinkToTheMaster.SelectedItem.ToString() == "Distance to the centroid")
            {
                WindowDistanceToMaster.groupBoxCentroid.Visible = true;
                WindowDistanceToMaster.groupBoxEdges.Visible = false;
            }
            else if (comboBoxLinkToTheMaster.SelectedItem.ToString() == "Distance to the edges")
            {
                WindowDistanceToMaster.groupBoxCentroid.Visible = false;
                WindowDistanceToMaster.groupBoxEdges.Visible = true;
            }
            else
            {
                WindowDistanceToMaster.groupBoxCentroid.Visible = false;
                WindowDistanceToMaster.groupBoxEdges.Visible = false;
            }
        }

        private void comboBoxLinkToTheMaster_SelectedValueChanged(object sender, EventArgs e)
        {
            UpDateDisplayMasterLinks();
        }

        private void comboBoxLinkToTheMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpDateDisplayMasterLinks();
        }




        private void checkBoxPreProcessings_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPreProcessings.Checked) WindowForPreProcessing.ShowDialog();
        }

        private void radioButtonIsVolume_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            WindowForVolumeDetection.ShowDialog();
            UpdateDisplay();
        }

        private void RadioButtonIsSpot_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            WindowForSpotDetection.ShowDialog();
            UpdateDisplay();

        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBoxInfo.Clear();
        }







    }


    [Serializable()]
    public class cControl
    {
        public string ParentFormName;
        public string CurrentCtrlType;
        public string CurrentCtrlName;

        public bool Status;
        public double Value;
        public string Text;


        public cControl()
        {
        }

        public cControl(Control CurrentCtrl, Form ParentForm)
        {
            this.ParentFormName = ParentForm.Name;
            this.CurrentCtrlName = CurrentCtrl.Name;
            this.CurrentCtrlType = CurrentCtrl.GetType().Name.ToString();

            if (this.CurrentCtrlType == "NumericUpDown")
            {
                this.Value = (double)((NumericUpDown)(CurrentCtrl)).Value;
            }
            if (this.CurrentCtrlType == "TextBox")
            {
                this.Text = ((TextBox)(CurrentCtrl)).Text;
            }
            if (this.CurrentCtrlType == "CheckBox")
            {
                this.Status = ((CheckBox)(CurrentCtrl)).Checked;
            }
            if (this.CurrentCtrlType == "RadioButton")
            {
                this.Status = ((RadioButton)(CurrentCtrl)).Checked;
            }
            if (this.CurrentCtrlType == "TrackBar")
            {
                this.Value = ((TrackBar)(CurrentCtrl)).Value;
            }
        }

        public cControl(Control CurrentCtrl, System.Windows.Forms.Panel PanelForm)
        {
            this.ParentFormName = PanelForm.Name;
            this.CurrentCtrlName = CurrentCtrl.Name;
            this.CurrentCtrlType = CurrentCtrl.GetType().Name.ToString();

            if (this.CurrentCtrlType == "NumericUpDown")
            {
                this.Value = (double)((NumericUpDown)(CurrentCtrl)).Value;
            }
            if (this.CurrentCtrlType == "TextBox")
            {
                this.Text = ((TextBox)(CurrentCtrl)).Text;
            }
            if (this.CurrentCtrlType == "CheckBox")
            {
                this.Status = ((CheckBox)(CurrentCtrl)).Checked;
            }
            if (this.CurrentCtrlType == "RadioButton")
            {
                this.Status = ((RadioButton)(CurrentCtrl)).Checked;
            }
            if (this.CurrentCtrlType == "TrackBar")
            {
                this.Value = ((TrackBar)(CurrentCtrl)).Value;
            }
        }


    }

    [Serializable()]
    public class cListCtrls : List<cControl>
    {

    }

    [Serializable()]
    public class cGlobalStatus : List<cListCtrls>
    {



    }

    [Serializable()]
    public class FinalGlobalStatus : System.Runtime.Serialization.SerializationBinder
    {

        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;

            String currentAssembly = Assembly.GetExecutingAssembly().FullName;

            // In this case we are always using the current assembly
            assemblyName = currentAssembly;

            // Get the type using the typeName and assemblyName
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
                typeName, assemblyName));

            return typeToDeserialize;
        }

        public void Save(string FileName)
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, this);
            }
            catch
            {
                // do nothing, just ignore any possible errors
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }
        }


        public FinalGlobalStatus Load(string fileName)
        {
            Stream stream = null;
            FinalGlobalStatus BrainDataBase = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                formatter.Binder = this;
                BrainDataBase = (FinalGlobalStatus)formatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // do nothing, just ignore any possible errors
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }
            return BrainDataBase;
        }


        public cGlobalStatus GlobalStatus = new cGlobalStatus();
    }


    public class cThumbnail : Panel
    {
        int PosX;
        int PosY;

        FormForControl Parent;

        public cThumbnail(cPoint3D Pos, FormForControl Ctrl, FormForControl Parent)
        {
            this.PosX = (int)Pos.X * 5;
            this.PosY = (int)Pos.Y * 5;
            //Test = Ctrl.panelForThumbnail;
            this.Location = new Point(PosX, PosY);
            this.BackColor = Color.Red;
            System.Windows.Forms.Label LabelText = new Label();
            LabelText.Location = new Point(0, 0);
            LabelText.Text = "Object";
            this.Controls.Add(LabelText);
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ForeColor = Color.Black;
            this.Parent = Parent;
        }



        public void UpdateAppearance()
        {
            if (Parent.RadioButtonIsSpot.Checked)
            {
                this.BackColor = Color.Red;
            }
            else if (Parent.radioButtonIsVolume.Checked)
            {
                this.BackColor = Color.LightGreen;
            }
            else if (Parent.radioButtonIsVolumeRendering.Checked)
            {
                this.BackColor = Color.Blue;
            }

            System.Windows.Forms.Label LabelText = new Label();
            LabelText.Location = new Point(0, 0);
            LabelText.Text = "Object";
            this.Controls.Add(LabelText);
        }



        public void Display(Form FormToDisplay)
        {
            base.Location = new Point(PosX, PosY);
            FormToDisplay.Controls.Add(this);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelForThumbnail_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelForThumbnail_MouseUp);
            this.MouseMove += new MouseEventHandler(this.panelForThumbnail_MouseMove);
        }

        static bool isMouseDown;
        private void panelForThumbnail_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
        }

        private void panelForThumbnail_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void panelForThumbnail_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown) return;

            Point Pt = this.FindForm().PointToClient(Control.MousePosition);
            Pt.X -= (this.Width / 2);
            Pt.Y -= (this.Height / 2);
            this.Location = Pt;
        }
    }

}
