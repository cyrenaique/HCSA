using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageAnalysis;
using System.IO;
using HCSAnalyzer.ObjectForNotations;
using HCSAnalyzer.Classes;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.Viewers;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using ImageAnalysisFiltering;
using HCSAnalyzer.Classes.Base_Classes;
using HCSAnalyzer.Classes.ImageAnalysis.FormsForImages;
using System.Drawing.Imaging;
using HCSAnalyzer.Classes._3D;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.Detection;
using HCSAnalyzer.Classes.ImageAnalysis._3D_Engine;
using IM3_Plugin3;
using System.Drawing.Text;
using System.Threading;

namespace HCSAnalyzer.Forms.FormsForImages
{
    public class cImagePanel : Panel
    {
        public int DefaultZoom = 0;
        //public partial class FormForImageDisplay : Form
        //{
        public FormForLUTManager LUTManager = null;
        public FormForNavigator ZNavigator = null;

        public cImage AssociatedImage { get; private set; }
        int Zoom = 100;
        //int ZPos = 0;// 36;
        protected int ViewDimX;
        protected int ViewDimY;
        int StartViewX = 0;
        int StartViewY = 0;

        protected Graphics ThisGraph;
        protected Bitmap BT = null;

        public ToolStripMenuItem _ToolStripMenuItemDisplayScale;
        Rectangle mRect;


        bool IsZoomLocked = false;

        public void SetIsZoomLocked(bool IsLocked)
        {
            this.IsZoomLocked = IsLocked;
            ToolStripMenuItem_ZoomLockToFit.Checked = this.IsZoomLocked;
        }

        public cGlobalInfo GlobalInfo;
        ToolStripMenuItem ToolStripMenuItem_ZoomLockToFit;

        //   cImagePanel NewImagePanel;


        #region Constructor
        //protected FormForImageDisplay()
        public cImagePanel()
        {
            this.DoubleBuffered = true;

            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();


            this.Height = this.Width = 0;
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Height = this.Width = 0;

            //      this.SetStyle(
            //ControlStyles.DoubleBuffer |
            //ControlStyles.AllPaintingInWmPaint |
            //ControlStyles.UserPaint |
            //ControlStyles.ResizeRedraw, true);

            //InitializeComponent();

            #region Initialize context Menu
            ContextMenuStrip ImageContextMenu = new ContextMenuStrip();

            ToolStripMenuItem ToolStripMenuItem_DisplayHistograms = new ToolStripMenuItem("Histograms");
            ImageContextMenu.Items.Add(ToolStripMenuItem_DisplayHistograms);
            ToolStripMenuItem_DisplayHistograms.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayHistograms);

            ToolStripMenuItem ToolStripMenuItem_DisplayAS = new ToolStripMenuItem("Display as...");

            ToolStripMenuItem ToolStripMenuItem_DisplayDataTable = new ToolStripMenuItem("Data Table");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_DisplayDataTable);
            ToolStripMenuItem_DisplayDataTable.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayDataTable);

            ToolStripMenuItem ToolStripMenuItem_Display3DTexture = new ToolStripMenuItem("3D Texture");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_Display3DTexture);
            ToolStripMenuItem_Display3DTexture.Click += new System.EventHandler(this.ToolStripMenuItem_Display3DTexture);

            ToolStripMenuItem ToolStripMenuItem_DisplayElevationMaps = new ToolStripMenuItem("Elevation Maps");
            ToolStripMenuItem_DisplayAS.DropDownItems.Add(ToolStripMenuItem_DisplayElevationMaps);
            ToolStripMenuItem_DisplayElevationMaps.Click += new System.EventHandler(this.ToolStripMenuItem_DisplayElevationMaps);

            ImageContextMenu.Items.Add(ToolStripMenuItem_DisplayAS);

            #region Image Processing
            ToolStripMenuItem ToolStripMenuItem_ImageProcessing = new ToolStripMenuItem("Image Processing");

            ToolStripMenuItem ToolStripMenuItem_Filtering = new ToolStripMenuItem("Filtering");

            ToolStripMenuItem ToolStripMenuItem_Filtering_GaussianConvolution = new ToolStripMenuItem("Gaussian Convolution");
            ToolStripMenuItem_Filtering.DropDownItems.Add(ToolStripMenuItem_Filtering_GaussianConvolution);
            ToolStripMenuItem_Filtering_GaussianConvolution.Click += new System.EventHandler(this.ToolStripMenuItem_Filtering_GaussianConvolution);

            ToolStripMenuItem ToolStripMenuItem_Filtering_Median = new ToolStripMenuItem("Median (x5)");
            ToolStripMenuItem_Filtering.DropDownItems.Add(ToolStripMenuItem_Filtering_Median);
            ToolStripMenuItem_Filtering_Median.Click += new System.EventHandler(this.ToolStripMenuItem_Filtering_Median);

            ToolStripMenuItem_Filtering.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_Morpho_Erode = new ToolStripMenuItem("Erode (x3)");
            ToolStripMenuItem_Filtering.DropDownItems.Add(ToolStripMenuItem_Morpho_Erode);
            ToolStripMenuItem_Morpho_Erode.Click += new System.EventHandler(this.ToolStripMenuItem_Morpho_Erode);

            ToolStripMenuItem ToolStripMenuItem_Morpho_Dilate = new ToolStripMenuItem("Dilate (x3)");
            ToolStripMenuItem_Filtering.DropDownItems.Add(ToolStripMenuItem_Morpho_Dilate);
            ToolStripMenuItem_Morpho_Dilate.Click += new System.EventHandler(this.ToolStripMenuItem_Morpho_Dilate);

            ToolStripMenuItem_ImageProcessing.DropDownItems.Add(ToolStripMenuItem_Filtering);

            ToolStripMenuItem ToolStripMenuItem_Edges = new ToolStripMenuItem("Edge Detection");

            ToolStripMenuItem ToolStripMenuItem_Edges_Canny = new ToolStripMenuItem("Canny (10,60)");
            ToolStripMenuItem_Edges.DropDownItems.Add(ToolStripMenuItem_Edges_Canny);
            ToolStripMenuItem_Edges_Canny.Click += new System.EventHandler(this.ToolStripMenuItem_Edges_Canny);
            ToolStripMenuItem_Edges_Canny.Enabled = false;

            ToolStripMenuItem ToolStripMenuItem_Edges_Sobel = new ToolStripMenuItem("Sobel (1,1,3)");
            ToolStripMenuItem_Edges.DropDownItems.Add(ToolStripMenuItem_Edges_Sobel);
            ToolStripMenuItem_Edges_Sobel.Click += new System.EventHandler(this.ToolStripMenuItem_Edges_Sobel);

            ToolStripMenuItem ToolStripMenuItem_Edges_Laplace = new ToolStripMenuItem("Laplace (x3)");
            ToolStripMenuItem_Edges.DropDownItems.Add(ToolStripMenuItem_Edges_Laplace);
            ToolStripMenuItem_Edges_Laplace.Click += new System.EventHandler(this.ToolStripMenuItem_Edges_Laplace);

            ToolStripMenuItem_ImageProcessing.DropDownItems.Add(ToolStripMenuItem_Edges);

            ToolStripMenuItem ToolStripMenuItem_GeometricalManip = new ToolStripMenuItem("Geometrical Processings");

            ToolStripMenuItem ToolStripMenuItem_GeometricalResize = new ToolStripMenuItem("Resize");
            ToolStripMenuItem_GeometricalManip.DropDownItems.Add(ToolStripMenuItem_GeometricalResize);
            ToolStripMenuItem_GeometricalResize.Click += new System.EventHandler(this.ToolStripMenuItem_GeometricalResize);

            ToolStripMenuItem ToolStripMenuItem_GeometricalBinning = new ToolStripMenuItem("Binning");
            ToolStripMenuItem_GeometricalManip.DropDownItems.Add(ToolStripMenuItem_GeometricalBinning);
            ToolStripMenuItem_GeometricalBinning.Click += new System.EventHandler(this.ToolStripMenuItem_GeometricalBinning);

            ToolStripMenuItem ToolStripMenuItem_GeometricalFlip = new ToolStripMenuItem("Flip");
            ToolStripMenuItem_GeometricalManip.DropDownItems.Add(ToolStripMenuItem_GeometricalFlip);
            ToolStripMenuItem_GeometricalFlip.Click += new System.EventHandler(this.ToolStripMenuItem_GeometricalFlip);

            ToolStripMenuItem ToolStripMenuItem_GeometricalCrop = new ToolStripMenuItem("Crop");
            ToolStripMenuItem_GeometricalManip.DropDownItems.Add(ToolStripMenuItem_GeometricalCrop);
            ToolStripMenuItem_GeometricalCrop.Click += new System.EventHandler(this.ToolStripMenuItem_GeometricalCrop);

            ToolStripMenuItem_ImageProcessing.DropDownItems.Add(ToolStripMenuItem_GeometricalManip);

            ToolStripMenuItem ToolStripMenuItem_Segmentation = new ToolStripMenuItem("Segmentation");

            ToolStripMenuItem ToolStripMenuItem_SegmentationThreshold = new ToolStripMenuItem("Threshold");
            ToolStripMenuItem_Segmentation.DropDownItems.Add(ToolStripMenuItem_SegmentationThreshold);
            ToolStripMenuItem_SegmentationThreshold.Click += new System.EventHandler(this.ToolStripMenuItem_SegmentationThreshold);

            ToolStripMenuItem ToolStripMenuItem_SegmentationKMeans = new ToolStripMenuItem("K-Means");
            ToolStripMenuItem_Segmentation.DropDownItems.Add(ToolStripMenuItem_SegmentationKMeans);
            ToolStripMenuItem_SegmentationKMeans.Click += new System.EventHandler(this.ToolStripMenuItem_SegmentationKMeans);

            ToolStripMenuItem ToolStripMenuItem_FindContours = new ToolStripMenuItem("Find Contours");
            ToolStripMenuItem_Segmentation.DropDownItems.Add(ToolStripMenuItem_FindContours);
            ToolStripMenuItem_FindContours.Click += new System.EventHandler(this.ToolStripMenuItem_FindContours);

            ToolStripMenuItem ToolStripMenuItem_SegmentationMeanShift = new ToolStripMenuItem("Mean Shift");
            ToolStripMenuItem_Segmentation.DropDownItems.Add(ToolStripMenuItem_SegmentationMeanShift);
            ToolStripMenuItem_SegmentationMeanShift.Click += new System.EventHandler(this.ToolStripMenuItem_SegmentationMeanShift);
            //ToolStripMenuItem_SegmentationMeanShift.Enabled = false;

            ToolStripMenuItem_ImageProcessing.DropDownItems.Add(ToolStripMenuItem_Segmentation);

            ToolStripMenuItem ToolStripMenuItem_Transforms = new ToolStripMenuItem("Transforms");

            ToolStripMenuItem ToolStripMenuItem_TransformsDistanceMap = new ToolStripMenuItem("Distance Map (L2)");
            ToolStripMenuItem_Transforms.DropDownItems.Add(ToolStripMenuItem_TransformsDistanceMap);
            ToolStripMenuItem_TransformsDistanceMap.Click += new System.EventHandler(this.ToolStripMenuItem_TransformsDistanceMap);

            ToolStripMenuItem ToolStripMenuItem_FindLocalMax = new ToolStripMenuItem("Find Local Max");
            ToolStripMenuItem_Transforms.DropDownItems.Add(ToolStripMenuItem_FindLocalMax);
            ToolStripMenuItem_FindLocalMax.Click += new System.EventHandler(this.ToolStripMenuItem_FindLocalMax);

            ToolStripMenuItem ToolStripMenuItem_AddNoise = new ToolStripMenuItem("Add Gaussian Noise (0-10)");
            ToolStripMenuItem_Transforms.DropDownItems.Add(ToolStripMenuItem_AddNoise);
            ToolStripMenuItem_AddNoise.Click += new System.EventHandler(this.ToolStripMenuItem_AddNoise);

            ToolStripMenuItem ToolStripMenuItem_3DTest = new ToolStripMenuItem("3D Analysis Test");
            ToolStripMenuItem_Transforms.DropDownItems.Add(ToolStripMenuItem_3DTest);
            ToolStripMenuItem_3DTest.Click += new System.EventHandler(this.ToolStripMenuItem_3DTest);

            ToolStripMenuItem_Transforms.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem ToolStripMenuItem_BitmapToImage = new ToolStripMenuItem("Bitmap -> RGB-Image");
            ToolStripMenuItem_Transforms.DropDownItems.Add(ToolStripMenuItem_BitmapToImage);
            ToolStripMenuItem_BitmapToImage.Click += new System.EventHandler(this.ToolStripMenuItem_BitmapToImage);

            ToolStripMenuItem ToolStripMenuItem_SplitByChannels = new ToolStripMenuItem("Slip by Channels");
            ToolStripMenuItem_Transforms.DropDownItems.Add(ToolStripMenuItem_SplitByChannels);
            ToolStripMenuItem_SplitByChannels.Click += new System.EventHandler(this.ToolStripMenuItem_SplitByChannels);

            ToolStripMenuItem_ImageProcessing.DropDownItems.Add(ToolStripMenuItem_Transforms);

            ImageContextMenu.Items.Add(ToolStripMenuItem_ImageProcessing);
            #endregion

            ToolStripMenuItem copyToClipboardToolStripMenuItem = new ToolStripMenuItem("Copy To Clipboard");
            ImageContextMenu.Items.Add(copyToClipboardToolStripMenuItem);
            copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem);
            copyToClipboardToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;

            ToolStripMenuItem saveAsToolStripMenuItem = new ToolStripMenuItem("Save as...");
            ImageContextMenu.Items.Add(saveAsToolStripMenuItem);
            saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem);
            saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;

            ToolStripMenuItem lUTManagerToolStripMenuItem = new ToolStripMenuItem("LUT Manager");
            ImageContextMenu.Items.Add(lUTManagerToolStripMenuItem);
            lUTManagerToolStripMenuItem.Click += new System.EventHandler(this.lUTManagerToolStripMenuItem);

            ToolStripMenuItem NavigatorToolStripMenuItem = new ToolStripMenuItem("Navigator");
            ImageContextMenu.Items.Add(NavigatorToolStripMenuItem);
            NavigatorToolStripMenuItem.Click += new System.EventHandler(this.NavigatorToolStripMenuItem);

            ImageContextMenu.Items.Add(new ToolStripSeparator());

            this._ToolStripMenuItemDisplayScale = new ToolStripMenuItem("Display Scale");
            _ToolStripMenuItemDisplayScale.CheckOnClick = true;
            ImageContextMenu.Items.Add(_ToolStripMenuItemDisplayScale);
            _ToolStripMenuItemDisplayScale.Click += new System.EventHandler(this.ToolStripMenuItemDisplayScale);

            ImageContextMenu.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem _ToolStripMenuItemInfo = new ToolStripMenuItem("Info");
            ImageContextMenu.Items.Add(_ToolStripMenuItemInfo);
            _ToolStripMenuItemInfo.Click += new System.EventHandler(this._ToolStripMenuItemInfo);

            this.ContextMenuStrip = ImageContextMenu;
            this.ContextMenuStrip.BringToFront();


            #region Zoom
            ToolStripMenuItem ToolStripMenuItem_Zoom = new ToolStripMenuItem("Zoom");

            ToolStripMenuItem ToolStripMenuItem_Zoom100 = new ToolStripMenuItem("100 %");
            ToolStripMenuItem_Zoom.DropDownItems.Add(ToolStripMenuItem_Zoom100);
            ToolStripMenuItem_Zoom100.Click += new System.EventHandler(this.ToolStripMenuItem_Zoom100);

            ToolStripMenuItem ToolStripMenuItem_ZoomToFit = new ToolStripMenuItem("Fit Window");
            ToolStripMenuItem_Zoom.DropDownItems.Add(ToolStripMenuItem_ZoomToFit);
            ToolStripMenuItem_ZoomToFit.Click += new System.EventHandler(this.ToolStripMenuItem_ZoomToFit);

            ToolStripMenuItem ToolStripMenuItem_ZoomToSelection = new ToolStripMenuItem("Selection");
            ToolStripMenuItem_Zoom.DropDownItems.Add(ToolStripMenuItem_ZoomToSelection);
            ToolStripMenuItem_ZoomToSelection.Click += new System.EventHandler(this.ToolStripMenuItem_ZoomToSelection);

            ToolStripMenuItem_Zoom.DropDownItems.Add(new ToolStripSeparator());

            ToolStripMenuItem_ZoomLockToFit = new ToolStripMenuItem("Locked to the Window");
            ToolStripMenuItem_ZoomLockToFit.CheckOnClick = true;
            ToolStripMenuItem_ZoomLockToFit.Checked = IsZoomLocked;
            ToolStripMenuItem_Zoom.DropDownItems.Add(ToolStripMenuItem_ZoomLockToFit);
            ToolStripMenuItem_ZoomLockToFit.Click += new System.EventHandler(_ToolStripMenuItem_ZoomLockToFit);

            ImageContextMenu.Items.Add(ToolStripMenuItem_Zoom);
            #endregion

            #region Modes
            ToolStripMenuItem ToolStripMenuItem_UIMode = new ToolStripMenuItem("UI Mode");

            ToolStripMenuItem_UIModeIntensity = new ToolStripMenuItem("Intensity");
            ToolStripMenuItem_UIMode.DropDownItems.Add(ToolStripMenuItem_UIModeIntensity);
            ToolStripMenuItem_UIModeIntensity.CheckOnClick = true;
            ToolStripMenuItem_UIModeIntensity.Checked = false;
            ToolStripMenuItem_UIModeIntensity.Click += new System.EventHandler(this._ToolStripMenuItem_UIModeIntensity);

            ToolStripMenuItem_UIModeSelection = new ToolStripMenuItem("Selection");
            ToolStripMenuItem_UIMode.DropDownItems.Add(ToolStripMenuItem_UIModeSelection);
            ToolStripMenuItem_UIModeSelection.CheckOnClick = true;
            ToolStripMenuItem_UIModeSelection.Checked = false;
            ToolStripMenuItem_UIModeSelection.Click += new System.EventHandler(this._ToolStripMenuItem_UIModeSelection);

            ToolStripMenuItem_UIModePointer = new ToolStripMenuItem("Pointer");
            ToolStripMenuItem_UIMode.DropDownItems.Add(ToolStripMenuItem_UIModePointer);
            ToolStripMenuItem_UIModePointer.CheckOnClick = true;
            ToolStripMenuItem_UIModePointer.Checked = true;
            ToolStripMenuItem_UIModePointer.Click += new System.EventHandler(this._ToolStripMenuItem_UIModePointer);


            ImageContextMenu.Items.Add(ToolStripMenuItem_UIMode);
            #endregion

            #endregion

            SetIsZoomLocked(true);


            this.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForImage_Paint);

            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelForImage_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelForImage_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelForImage_MouseMove);
            ////  this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.panelForImage_MouseWheel);
            this.Resize += new EventHandler(this.panelForImage_Resize);

            this.OnPaint(null);

        }

        #endregion

        ToolStripMenuItem ToolStripMenuItem_UIModeIntensity;
        ToolStripMenuItem ToolStripMenuItem_UIModeSelection;
        ToolStripMenuItem ToolStripMenuItem_UIModePointer;

        int FirstClickPosX;
        int FirstClickPosY;

        int ShiftX = 0;
        int ShiftY = 0;
        int ZoomedWidth;
        int ZoomedHeight;
        byte[] rgbValues;
        IntPtr ptr;
        System.Drawing.Imaging.BitmapData bmpData;
        int bytes;

        protected Dictionary<string, cObjectForAnnotation> DictionaryForNotations = new Dictionary<string, cObjectForAnnotation>();

        public Bitmap GetBitmap()
        {
            return BT;
        }

        // protected override void on


        //protected override void OnFormClosing(FormClosingEventArgs e)
        //{
        //    AssociatedImage.Dispose();
        //   // base.OnFormClosing(e);
        //}

        public void Unsafe_SetAssociatedImage(cImage Im)
        {
            this.AssociatedImage = Im;
        }

        #region Image processing
        private void ToolStripMenuItem_GeometricalResize(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageGeometricResize GR = new cImageGeometricResize();
            GR.SetInputData(this.AssociatedImage);
            GR.SliceIndex = (int)ZNavigator.numericUpDownZPos.Value;
            GR.InterpolationType = Emgu.CV.CvEnum.Inter.Linear;

            GR.ListProperties.FindByName("Scale").SetNewValue((double)0.5);
            GR.ListProperties.FindByName("Scale").IsGUIforValue = true;

            GR.ListProperties.FindByName("Maximum Width").SetNewValue((int)0);
            GR.ListProperties.FindByName("Maximum Width").IsGUIforValue = true;

            cFeedBackMessage FeedBackMessage = GR.Run();

            cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(FeedBackMessage.GetFullFeedBack());
            if (FeedBackMessage.IsSucceed == false) return;



            cImageDisplayProperties TmpProp = new cImageDisplayProperties();
            TmpProp.UpdateFromLUTManager(this.LUTManager);

            NewView.SetInputData(GR.GetOutPut());
            NewView.Run();

            //NewView.LUTManager.UpdateFromDisplayProperties(TmpProp);
        }

        private void ToolStripMenuItem_GeometricalBinning(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageGeometricBinning GB = new cImageGeometricBinning();
            GB.SetInputData(this.AssociatedImage);

            GB.ListProperties.FindByName("Binning").SetNewValue((int)2);
            GB.ListProperties.FindByName("Binning").IsGUIforValue = true;

            cFeedBackMessage FeedBackMessage = GB.Run();

            cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(FeedBackMessage.GetFullFeedBack());
            if (FeedBackMessage.IsSucceed == false) return;



            cImageDisplayProperties TmpProp = new cImageDisplayProperties();
            TmpProp.UpdateFromLUTManager(this.LUTManager);

            NewView.SetInputData(GB.GetOutPut());
            NewView.Run();

            //NewView.LUTManager.UpdateFromDisplayProperties(TmpProp);
        }
        private void ToolStripMenuItem_GeometricalFlip(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageGeometricFlip GF = new cImageGeometricFlip();
            GF.SetInputData(this.AssociatedImage);

            GF.ListProperties.FindByName("Horizontal").SetNewValue((bool)true);
            GF.ListProperties.FindByName("Horizontal").IsGUIforValue = true;
            cFeedBackMessage FeedBackMessage = GF.Run();

            cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(FeedBackMessage.GetFullFeedBack());
            if (FeedBackMessage.IsSucceed == false) return;

            cImageDisplayProperties TmpProp = new cImageDisplayProperties();
            TmpProp.UpdateFromLUTManager(this.LUTManager);

            NewView.SetInputData(GF.GetOutPut());
            NewView.Run();

            //NewView.LUTManager.UpdateFromDisplayProperties(TmpProp);
        }

        Point ConvertPanelCoordToImageCoord(Point PanelCoord)
        {
            float ZoomFactor = this.Zoom / 100.0f;
            int RealX = (int)((PanelCoord.X - ShiftX) / ZoomFactor);
            int RealY = (int)((PanelCoord.Y - ShiftY) / ZoomFactor);
            return new Point(RealX, RealY);
        }


        private void ToolStripMenuItem_GeometricalCrop(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            Point Bottom = new Point(mRect.Left, mRect.Top);
            Point ConvertedBottom = ConvertPanelCoordToImageCoord(Bottom);

            Point Top = new Point(mRect.Right, mRect.Bottom);
            Point ConvertedTop = ConvertPanelCoordToImageCoord(Top);


            cImage CroppedImage = this.AssociatedImage.Crop(new cPoint3D(ConvertedBottom.X, ConvertedBottom.Y, 0), new cPoint3D(ConvertedTop.X, ConvertedTop.Y, 0));

            cImageDisplayProperties TmpProp = new cImageDisplayProperties();
            TmpProp.UpdateFromLUTManager(this.LUTManager);

            NewView.SetInputData(CroppedImage);
            NewView.Run();
            NewView.MyImageViewer.IP.LUTManager.UpdateFromDisplayProperties(TmpProp);
        }
        private void ToolStripMenuItem_Edges_Laplace(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageFilterLaplace FL = new cImageFilterLaplace();
            FL.SetInputData(this.AssociatedImage);
            FL.Aperture = 3;
            FL.Run();
            NewView.SetInputData(FL.GetOutPut());

            NewView.Run();
        }

        private void ToolStripMenuItem_Edges_Canny(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageFilterCanny FC = new cImageFilterCanny();
            FC.SetInputData(this.AssociatedImage);
            FC.GreyThreshold = 10;
            FC.GreyThresholdLinkin = 60;
            FC.Run();
            NewView.SetInputData(FC.GetOutPut());

            NewView.Run();
        }

        private void ToolStripMenuItem_SegmentationThreshold(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageSegmentationThreshold ME = new cImageSegmentationThreshold();
            ME.SetInputData(this.AssociatedImage);
            ME.ListProperties.FindByName("Threshold").SetNewValue((double)100);
            ME.ListProperties.FindByName("Threshold").IsGUIforValue = true;
            ME.IsInvert = true;
            ME.Run();
            NewView.SetInputData(ME.GetOutPut());

            NewView.Run();
        }

        private void ToolStripMenuItem_SegmentationKMeans(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageSegmentationKMeans KM = new cImageSegmentationKMeans();

            KM.SetInputData(this.AssociatedImage);
            //ME.IsInvert = true;
            // KM.ListChannelsToBeProcessed = this.AssociatedImage.GetListChannels();
            KM.ListProperties.FindByName("Number of clusters").SetNewValue((int)3);
            KM.ListProperties.FindByName("Number of clusters").IsGUIforValue = true;

            KM.Run();
            NewView.SetInputData(KM.GetOutPut());

            NewView.Run();
        }

        private void ToolStripMenuItem_FindContours(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageFindContours FC = new cImageFindContours();
            FC.SetInputData(this.AssociatedImage);

            FC.Run();
            NewView.SetInputData(FC.GetOutPut());

            NewView.Run();
        }

        private void ToolStripMenuItem_SegmentationMeanShift(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageSegmentationMeanShift MS = new cImageSegmentationMeanShift();
            MS.SetInputData(this.AssociatedImage);
            //ME.IsInvert = true;
            MS.Run();
            NewView.SetInputData(MS.GetOutPut());

            NewView.Run();
        }

        private void ToolStripMenuItem_TransformsDistanceMap(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageDistanceMap DM = new cImageDistanceMap();
            DM.SetInputData(this.AssociatedImage);
            DM.DistanceType = Emgu.CV.CvEnum.DistType.L2;
            DM.Run();
            NewView.SetInputData(DM.GetOutPut());

            NewView.Run();
        }

        private void ToolStripMenuItem_FindLocalMax(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageFindLocalMax FLM = new cImageFindLocalMax();
            FLM.SetInputData(this.AssociatedImage);
            //FLM.DistanceType = Emgu.CV.CvEnum.DIST_TYPE.CV_DIST_L2;
            FLM.Run();
            NewView.SetInputData(FLM.GetOutPut());

            NewView.Run();
        }

        private void ToolStripMenuItem_AddNoise(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageAddNoise AN = new cImageAddNoise();
            AN.SetInputData(this.AssociatedImage);
            AN.DistributionType = Classes.Base_Classes.Data.eRandDistributionType.GAUSSIAN;
            AN.Mean = 0;
            AN.Stdev = 10;
            //FLM.DistanceType = Emgu.CV.CvEnum.DIST_TYPE.CV_DIST_L2;
            AN.Run();
            NewView.SetInputData(AN.GetOutPut());

            NewView.Run();

            cImageDisplayProperties TmpProp = new cImageDisplayProperties();
            TmpProp.UpdateFromLUTManager(this.LUTManager);

            NewView.MyImageViewer.IP.LUTManager.UpdateFromDisplayProperties(TmpProp);



        }

        private void ToolStripMenuItem_SplitByChannels(object sender, EventArgs e)
        {

            for (int i = 0; i < this.AssociatedImage.GetNumChannels(); i++)
            {
                cDisplaySingleImage NewView = new cDisplaySingleImage();
                cImage NewImage = new cImage(this.AssociatedImage.Width, this.AssociatedImage.Height, this.AssociatedImage.Depth, 1);
                NewImage.Name = this.AssociatedImage.Name + " [" + this.AssociatedImage.SingleChannelImage[i].Name + "]";
                NewImage.Resolution = new cPoint3D(this.AssociatedImage.Resolution);

                Array.Copy(this.AssociatedImage.SingleChannelImage[i].Data, NewImage.SingleChannelImage[0].Data, NewImage.ImageSize);

                NewView.SetInputData(NewImage);
                NewView.Run();
            }
        }

        private void ToolStripMenuItem_BitmapToImage(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            Bitmap BMP = this.GetBitmap();
            if (BMP == null) return;

            cImage NewImage = new cImage(BMP.Width, BMP.Height, 1, 3);
            NewImage.Name = "RGB_Bitmap(" + this.AssociatedImage.Name + ")";
            NewImage.Resolution = new cPoint3D(this.AssociatedImage.Resolution);


            for (int j = 0; j < BMP.Height; j++)
            {
                for (int i = 0; i < BMP.Width; i++)
                {
                    Color C = BMP.GetPixel(i, j);
                    NewImage.SingleChannelImage[0].Data[i + j * NewImage.Width] = C.R;
                    NewImage.SingleChannelImage[1].Data[i + j * NewImage.Width] = C.G;
                    NewImage.SingleChannelImage[2].Data[i + j * NewImage.Width] = C.B;
                }
            }

            NewView.SetInputData(NewImage);
            NewView.Run();
        }

        private void ToolStripMenuItem_3DTest(object sender, EventArgs e)
        {

            Plugin3D NewPlug = new Plugin3D(this.AssociatedImage);
            NewPlug.Show();

            return;

            c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(this.AssociatedImage.Width, this.AssociatedImage.Height, this.AssociatedImage.Depth),
                                                    new cPoint3D(1, 1, 1));


            cVolumeDetection Detection = new cVolumeDetection(this.AssociatedImage, 2, false);
            Detection.SetShift(new cPoint3D(0, 0, 0));
            Detection.SetContainers(null);

            List<cInteractive3DObject> AssociatedBiologicalObjectList = Detection.IntensityThreshold(150f,
                                         10,
                                         float.MaxValue);

            for (int i = 0; i < AssociatedBiologicalObjectList.Count; i++)
            {
                cBiological3DVolume TmpVol = (cBiological3DVolume)AssociatedBiologicalObjectList[i];

                // MyWorld.AddGeometric3DObject(TmpVol.AttachText("Object " + i, 5, Color.White));

                //    //if (Obj.checkIsBoxPointingArrow.Checked)
                cGeometric3DObject AssociatedArrow = TmpVol.AttachPointingArrow(5f, Color.Tomato);
                MyWorld.AddGeometric3DObject(AssociatedArrow);

                AssociatedBiologicalObjectList[i].SetOpacity(/*(double)Obj.ColorOpacity.numericUpDownValue.Value*/0.5);
                AssociatedBiologicalObjectList[i].Colour = Color.Red;

                MyWorld.AddBiological3DObject(AssociatedBiologicalObjectList[i]);

            }

            MyWorld.AddVolume3D(new cVolumeRendering3D(this.AssociatedImage.SingleChannelImage[0], new cPoint3D(0, 0, 0), null, MyWorld));

            MyWorld.BackGroundColor = Color.LightGray;// GhostWhite;

            cViewer3D V3D = new cViewer3D();
            V3D.SetInputData(MyWorld);
            V3D.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(V3D.GetOutPut());
            DTW.Run();
            DTW.Display();



        }

        private void ToolStripMenuItem_Morpho_Erode(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageMorphoErode ME = new cImageMorphoErode();
            ME.SetInputData(this.AssociatedImage);
            ME.Iterations = 3;
            ME.Run();
            NewView.SetInputData(ME.GetOutPut());

            NewView.Run();

            cImageDisplayProperties TmpProp = new cImageDisplayProperties();
            TmpProp.UpdateFromLUTManager(this.LUTManager);
            NewView.MyImageViewer.IP.LUTManager.UpdateFromDisplayProperties(TmpProp);

        }

        private void ToolStripMenuItem_Morpho_Dilate(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageMorphoDilate ME = new cImageMorphoDilate();
            ME.SetInputData(this.AssociatedImage);
            ME.Iterations = 3;
            ME.Run();
            NewView.SetInputData(ME.GetOutPut());

            NewView.Run();

            cImageDisplayProperties TmpProp = new cImageDisplayProperties();
            TmpProp.UpdateFromLUTManager(this.LUTManager);
            NewView.MyImageViewer.IP.LUTManager.UpdateFromDisplayProperties(TmpProp);

        }

        private void ToolStripMenuItem_Edges_Sobel(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageFilterSobel FS = new cImageFilterSobel();
            FS.SetInputData(this.AssociatedImage);
            FS.Aperture = 3;
            FS.X_Order = 1;
            FS.Y_Order = 1;
            FS.Run();
            NewView.SetInputData(FS.GetOutPut());

            NewView.Run();
        }

        private void ToolStripMenuItem_Filtering_Median(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            cImageFilterMedian FM = new cImageFilterMedian();
            FM.SetInputData(this.AssociatedImage);


            FM.ListProperties.FindByName("Kernel Size").SetNewValue((int)5);
            FM.ListProperties.FindByName("Kernel Size").IsGUIforValue = true;
            cFeedBackMessage FeedBackMessage = FM.Run();

            //GlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText(FeedBackMessage.GetFullFeedBack());
            if (FeedBackMessage.IsSucceed == false) return;

            NewView.SetInputData(FM.GetOutPut());


            NewView.Run();

            cImageDisplayProperties TmpProp = new cImageDisplayProperties();
            TmpProp.UpdateFromLUTManager(this.LUTManager);
            NewView.MyImageViewer.IP.LUTManager.UpdateFromDisplayProperties(TmpProp);


            //GlobalInfo.DisplayViewer(NewView);
        }

        private void _ToolStripMenuItem_ZoomLockToFit(object sender, EventArgs e)
        {
            if (IsZoomLocked)
                IsZoomLocked = false;
            else
            {
                IsZoomLocked = true;
                ZoomToFit(2);
            }

        }
        private void _ToolStripMenuItem_UIModePointer(object sender, EventArgs e)
        {
            ToolStripMenuItem_UIModeSelection.Checked = false;
            ToolStripMenuItem_UIModeIntensity.Checked = false;
            ToolStripMenuItem_UIModePointer.Checked = true;
            mRect.Width = mRect.Height = -1;
            RefreshBMP();
        }


        private void _ToolStripMenuItem_UIModeSelection(object sender, EventArgs e)
        {
            ToolStripMenuItem_UIModeSelection.Checked = true;
            ToolStripMenuItem_UIModeIntensity.Checked = false;
            ToolStripMenuItem_UIModePointer.Checked = false;
            mRect.Width = mRect.Height = -1;
            RefreshBMP();
        }

        private void _ToolStripMenuItem_UIModeIntensity(object sender, EventArgs e)
        {
            ToolStripMenuItem_UIModeSelection.Checked = false;
            ToolStripMenuItem_UIModeIntensity.Checked = true;
            ToolStripMenuItem_UIModePointer.Checked = false;
            mRect.Width = mRect.Height = -1;
            RefreshBMP();
        }

        private void ToolStripMenuItem_ZoomToFit(object sender, EventArgs e)
        {
            ZoomToFit(2);
        }

        private void ToolStripMenuItem_Zoom100(object sender, EventArgs e)
        {
            ChangeZoomValue(100);
        }

        private void ToolStripMenuItem_ZoomToSelection(object sender, EventArgs e)
        {
            // ChangeZoomValue(100);
            ZoomToSelection();
        }

        private void ToolStripMenuItem_Filtering_GaussianConvolution(object sender, EventArgs e)
        {
            cDisplaySingleImage NewView = new cDisplaySingleImage();

            ImageAnalysisFiltering.cImageFilterGaussianConvolution GaussianBlur = new ImageAnalysisFiltering.cImageFilterGaussianConvolution();
            GaussianBlur.SetInputData(this.AssociatedImage);
            GaussianBlur.ListProperties.FindByName("Kernel Size").SetNewValue((int)3);
            GaussianBlur.ListProperties.FindByName("Kernel Size").IsGUIforValue = true;
            cFeedBackMessage FeedBackMessage = GaussianBlur.Run();
            if (FeedBackMessage.IsSucceed == false) return;
            NewView.SetInputData(GaussianBlur.GetOutPut());


            NewView.Run();

            cImageDisplayProperties TmpProp = new cImageDisplayProperties();
            TmpProp.UpdateFromLUTManager(this.LUTManager);
            NewView.MyImageViewer.IP.LUTManager.UpdateFromDisplayProperties(TmpProp);
        }
        #endregion

        private void ToolStripMenuItem_DisplayHistograms(object sender, EventArgs e)
        {
            cDesignerTab DT = new cDesignerTab();

            cListExtendedTable ListTables = new cListExtendedTable(this.AssociatedImage);

            for (int i = 0; i < ListTables.Count; i++)
            {
                cLinearize LI = new cLinearize();
                LI.SetInputData(ListTables[i]);
                LI.Run();

                cViewerStackedHistogram VH = new cViewerStackedHistogram();
                VH.SetInputData(LI.GetOutPut());
                VH.Title = ListTables[i].Name;
                if (!VH.Run().IsSucceed) return;

                DT.SetInputData(VH.GetOutPut());
            }
            DT.Run();

            cDisplayToWindow MyDisplay = new cDisplayToWindow();
            MyDisplay.SetInputData(DT.GetOutPut());
            MyDisplay.Title = "Histograms(" + this.AssociatedImage.Name + ")";
            MyDisplay.Run();
            MyDisplay.Display();

        }

        private void ToolStripMenuItem_DisplayElevationMaps(object sender, EventArgs e)
        {
            //cListExtendedTable LTable = new cListExtendedTable(this.AssociatedImage);

            //cExtendedTable Table = new cExtendedTable(.SingleChannelImage[0],0);

            cViewerElevationMap3D VE = new cViewerElevationMap3D();
            VE.SetInputData(this.AssociatedImage);

            // UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.LUTManager.panelForLUTS.Controls[0];
            // VE.LUT = SingleLUT.SelectedLUT;


            if (VE.Run().IsSucceed == false) return;

            cDesignerSinglePanel CD = new cDesignerSinglePanel();
            CD.SetInputData(VE.GetOutPut());
            if (CD.Run().IsSucceed == false) return;

            cDisplayToWindow CDW = new cDisplayToWindow();
            CDW.SetInputData(CD.GetOutPut());
            CDW.Title = "Elevation Map [" + this.AssociatedImage.Name + "]";
            if (CDW.Run().IsSucceed == false) return;
            CDW.Display();
        }

        private void ToolStripMenuItem_Display3DTexture(object sender, EventArgs e)
        {
            cViewer3D V3D = new cViewer3D();
            c3DNewWorld MyWorld = new c3DNewWorld(new cPoint3D(1, 1, 1), new cPoint3D(1, 1, 1));

            cListGeometric3DObject GlobalList = new cListGeometric3DObject("2D Texture Metaobject");

            c3DTexturedPlan _3DPlan = new c3DTexturedPlan(new cPoint3D(0, 0, 0), this.AssociatedImage);

            //   c3DElevationMap _3DMap = new c3DElevationMap(new cPoint3D(0, 0, 0), this.Input, this.LUT);

            _3DPlan.SetName("2D Texture");
            _3DPlan.Run();
            GlobalList.Add(_3DPlan);

            foreach (var item in GlobalList)
                MyWorld.AddGeometric3DObject(item);

            //  MyWorld.AddLight(Color.White);

            V3D.SetInputData(MyWorld);
            V3D.Run();

            cDisplayToWindow DTW = new cDisplayToWindow();
            DTW.SetInputData(V3D.GetOutPut());
            DTW.Title = "2D texture (" + this.AssociatedImage.Name + ")";
            DTW.Run();
            DTW.Display();
        }

        private void ToolStripMenuItem_DisplayDataTable(object sender, EventArgs e)
        {
            cDisplayListExtendedTable DLET = new cDisplayListExtendedTable();
            DLET.SetInputData(new Classes.Base_Classes.DataStructures.cListExtendedTable(this.AssociatedImage));
            DLET.Run();
        }

        public void ChangeZoomValue(int NewZoom)
        {
            if (ThisGraph == null) return;


            Zoom = NewZoom;
            if (Zoom < 1) Zoom = 1;
            //toolStripStatusLabelForZoom.Text = "Zoom: " + Zoom.ToString() + " %";

            ReDrawPic();

            ThisGraph.DrawString("Zoom: " + this.Zoom + " %",
                        cGlobalInfo.FontForImageDisplay,
                        new SolidBrush(Color.Yellow),
                        (float)0,
                        (float)0);

        }

        /// <summary>
        /// change the zoom in order the fit the window dimensions
        /// </summary>
        public void ZoomToSelection()
        {
            Point Bottom = new Point(mRect.Left, mRect.Top);
            Point ConvertedBottom = ConvertPanelCoordToImageCoord(Bottom);

            Point Top = new Point(mRect.Right, mRect.Bottom);
            Point ConvertedTop = ConvertPanelCoordToImageCoord(Top);

            this.StartingX = ConvertedBottom.X;
            this.StartingY = ConvertedBottom.Y;
            this.EndingX = ConvertedTop.X;
            this.EndingY = ConvertedTop.Y;

            ReDrawPic();

            // cImage CroppedImage = this.AssociatedImage.Crop(new cPoint3D(ConvertedBottom.X, ConvertedBottom.Y, 0), new cPoint3D(ConvertedTop.X, ConvertedTop.Y, 0));
        }

        /// <summary>
        /// change the zoom in order the fit the window dimensions
        /// </summary>
        /// <param name="Axis">0: X, 1: Y, 2: X/Y</param>
        public void ZoomToFit(int Axis)
        {
            if ((this.AssociatedImage.Width == 0) || (this.AssociatedImage.Height == 0)) return;

            this.StartingX = this.StartingY = this.EndingX = this.EndingY = -1;

            if (Axis == 0)
                Zoom = (int)((100 * this.Width) / this.AssociatedImage.Width);
            else if (Axis == 1)
                Zoom = (int)((100 * this.Height) / this.AssociatedImage.Height);
            else if (Axis == 2)
            {
                int ZoomX = (int)((100 * this.Width) / this.AssociatedImage.Width);
                int ZoomY = (int)((100 * this.Height) / this.AssociatedImage.Height);

                if (ZoomX > ZoomY) Zoom = ZoomY;
                else Zoom = ZoomX;
            }
            else
                return;

            if (Zoom < 1) Zoom = 1;
            //   toolStripStatusLabelForZoom.Text = "Zoom: " + Zoom.ToString() + " %";
            ReDrawPic();

            ThisGraph.DrawString("Zoom: " + this.Zoom + " %",
                        cGlobalInfo.FontForImageDisplay,
                        new SolidBrush(Color.Yellow),
                        (float)0,
                        (float)0);
        }

        public void panelForImage_MouseWheel(object sender, MouseEventArgs e)
        {
            Zoom += e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            ChangeZoomValue(Zoom);
        }

        double GetZoomFactor()
        {
            return Zoom / 100.0f;
        }

        bool CheckIfPointInActiveArea(Point Location)
        {
            if ((Location.X >= ShiftX) && (Location.Y >= ShiftY) && (Location.X < ShiftX + ZoomedWidth) && (Location.Y < ShiftY + ZoomedHeight))
                return true;
            else return false;
        }

        private void copyToClipboardToolStripMenuItem(object sender, EventArgs e)
        {
            if (BT == null) return;
            MemoryStream ms = new MemoryStream();
            Clipboard.SetImage(BT);
        }

        private void saveAsToolStripMenuItem(object sender, EventArgs e)
        {
            cImageToFile IF = new cImageToFile();
            IF.IsDisplayUIForFilePath = true;
            IF.SetInputData(this.AssociatedImage);
            IF.Run();
        }

        int StartingX = -1;
        int EndingX = -1;
        int StartingY = -1;
        int EndingY = -1;

        public void ReDrawPic()
        {
            if (this.LUTManager == null) return;
            if (this.ZNavigator == null) return;

            if (this.DefaultZoom != 0)
            {
                this.Zoom = this.DefaultZoom;
                this.DefaultZoom = 0;
            }

            int ZPos = ZNavigator.trackBarForZPos.Value;

            ThisGraph = this.CreateGraphics();
            ThisGraph.TextRenderingHint = TextRenderingHint.AntiAlias;
            ThisGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            float ZoomFactor = Zoom / 100.0f;

            ZoomedWidth = (int)(AssociatedImage.Width * ZoomFactor);
            ZoomedHeight = (int)(AssociatedImage.Height * ZoomFactor);

            if (ZoomedWidth < this.Width)
                ShiftX = (this.Width - ZoomedWidth) / 2;

            if (ZoomedHeight < this.Height)
                ShiftY = (this.Height - ZoomedHeight) / 2;


            int PanelWidth = this.Width;// (int)(AssociatedImage.Width * ZoomFactor);
            int PanelHeight = this.Height;// (int)(AssociatedImage.Height * ZoomFactor);
            //this.Width = NewWidth;
            //this.Height = NewHeight;

            if ((PanelHeight == 0) || (PanelWidth == 0)) return;

            int PosViewX = (this.Width - PanelWidth) / 2;
            int PosViewY = (this.Height - PanelHeight) / 2;
            //   this.Location = new Point(PosViewX,PosViewY);

            BT = new Bitmap(PanelWidth, PanelHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, PanelWidth, PanelHeight);
            bmpData = BT.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, BT.PixelFormat);

            // Get the address of the first line.
            ptr = bmpData.Scan0;

            int scanline = Math.Abs(bmpData.Stride);

            byte CurrentRed;
            byte CurrentGreen;
            byte CurrentBlue;

            int RealX;
            int RealY;

            int NewStartX = (int)(this.StartViewX / ZoomFactor);
            int NewStartY = (int)(this.StartViewY / ZoomFactor);

            List<float> Min = new List<float>();
            List<float> Max = new List<float>();
            List<double> OpacityList = new List<double>();
            List<double> GammaList = new List<double>();


            for (int IdxChannel = 0; IdxChannel < this.AssociatedImage.GetNumChannels(); IdxChannel++)
            {
                if (this.LUTManager != null)
                {
                    UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.LUTManager.panelForLUTS.Controls[IdxChannel];

                    Min.Add((float)SingleLUT.numericUpDownMinValue.Value);
                    Max.Add((float)SingleLUT.numericUpDownMaxValue.Value);
                    GammaList.Add((double)SingleLUT.trackBarGamma.Value / 100.0);
                    OpacityList.Add((double)SingleLUT.trackBarOpacity.Value / 100.0);

                }
            }


            // Declare an array to hold the bytes of the bitmap. 
            bytes = scanline * PanelHeight;
            rgbValues = new byte[bytes];

            //int RightShiftX = ShiftX + PanelWidth;

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            if (StartingX == -1)
            {
                NewStartX = 0;
                NewStartY = 0;
            }
            else
            {
                NewStartX = StartingX;
                NewStartY = StartingY;
                float TmpZoomFactor = PanelWidth / (EndingX - StartingX);
                ZoomFactor = PanelHeight / (EndingY - StartingY);

                if (TmpZoomFactor < ZoomFactor) ZoomFactor = TmpZoomFactor;

            }

            for (int IdxChannel = 0; IdxChannel < this.AssociatedImage.GetNumChannels(); IdxChannel++)
            {
                UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.LUTManager.panelForLUTS.Controls[IdxChannel];
                if (SingleLUT.checkBoxIsActive.Checked == false) continue;

                byte[][] CurrentLUT = SingleLUT.SelectedLUT;

                //ConvertImageFloatToRGB(AssociatedImage, 0, 255, SingleLUT.SelectedLUT, 0);
                #region if Gamma != 1
                if (GammaList[IdxChannel] != 1)
                {
                    for (int PixY = 0; PixY < PanelHeight; PixY++)
                    {
                        RealY = (int)((PixY - ShiftY) / ZoomFactor) + NewStartY;

                        if ((RealY < 0) || (RealY >= AssociatedImage.Height))
                            continue;

                        for (int PixX = 0; PixX < PanelWidth; PixX++)
                        {
                            RealX = (int)((PixX - ShiftX) / ZoomFactor) + NewStartX;


                            if ((RealX < 0) || (RealX >= AssociatedImage.Width))
                                continue;

                            float Value = AssociatedImage.SingleChannelImage[IdxChannel].Data[RealX + RealY * AssociatedImage.Width + ZPos * AssociatedImage.SliceSize];
                            Value = (float)Math.Pow(Value, GammaList[IdxChannel]);

                            int ConvertedValue = (int)((((CurrentLUT[0].Length - 1) * (Value - Min[IdxChannel])) / (Max[IdxChannel] - Min[IdxChannel])));

                            if (ConvertedValue < 0) ConvertedValue = 0;
                            if (ConvertedValue >= CurrentLUT[0].Length) ConvertedValue = CurrentLUT[0].Length - 1;

                            CurrentRed = (byte)(OpacityList[IdxChannel] * CurrentLUT[0][ConvertedValue]);
                            CurrentGreen = (byte)(OpacityList[IdxChannel] * CurrentLUT[1][ConvertedValue]);
                            CurrentBlue = (byte)(OpacityList[IdxChannel] * CurrentLUT[2][ConvertedValue]);

                            double NewValue = rgbValues[3 * PixX + PixY * scanline] + CurrentBlue;
                            if (NewValue > 255)
                                rgbValues[3 * PixX + PixY * scanline] = 255;
                            else
                                rgbValues[3 * PixX + PixY * scanline] += CurrentBlue;

                            NewValue = rgbValues[3 * PixX + 1 + PixY * scanline] + CurrentGreen;
                            if (NewValue > 255)
                                rgbValues[3 * PixX + 1 + PixY * scanline] = 255;
                            else
                                rgbValues[3 * PixX + 1 + PixY * scanline] += CurrentGreen;

                            NewValue = rgbValues[3 * PixX + 2 + PixY * scanline] + CurrentRed;
                            if (NewValue > 255)
                                rgbValues[3 * PixX + 2 + PixY * scanline] = 255;
                            else
                                rgbValues[3 * PixX + 2 + PixY * scanline] += CurrentRed;

                        }
                    }

                }
                else
                #endregion
                {
                    for (int PixY = 0; PixY < PanelHeight; PixY++)
                    {
                        RealY = (int)((PixY - ShiftY) / ZoomFactor) + NewStartY;

                        if ((RealY < 0) || (RealY >= AssociatedImage.Height))
                            continue;

                        for (int PixX = 0; PixX < PanelWidth; PixX++)
                        {
                            RealX = (int)((PixX - ShiftX) / ZoomFactor) + NewStartX;


                            if ((RealX < 0) || (RealX >= AssociatedImage.Width))
                                continue;

                            float Value = AssociatedImage.SingleChannelImage[IdxChannel].Data[RealX + RealY * AssociatedImage.Width + ZPos * AssociatedImage.SliceSize];

                            int ConvertedValue = (int)((((CurrentLUT[0].Length - 1) * (Value - Min[IdxChannel])) / (Max[IdxChannel] - Min[IdxChannel])));

                            if (ConvertedValue < 0) ConvertedValue = 0;
                            if (ConvertedValue >= CurrentLUT[0].Length) ConvertedValue = CurrentLUT[0].Length - 1;

                            CurrentRed = (byte)(OpacityList[IdxChannel] * CurrentLUT[0][ConvertedValue]);
                            CurrentGreen = (byte)(OpacityList[IdxChannel] * CurrentLUT[1][ConvertedValue]);
                            CurrentBlue = (byte)(OpacityList[IdxChannel] * CurrentLUT[2][ConvertedValue]);

                            //int NewFullX = PixX - ShiftX;
                            //int NewFullY = PixY - ShiftY;

                            double NewValue = rgbValues[3 * PixX + PixY * scanline] + CurrentBlue;
                            if (NewValue > 255)
                                rgbValues[3 * PixX + PixY * scanline] = 255;
                            else
                                rgbValues[3 * PixX + PixY * scanline] += CurrentBlue;

                            NewValue = rgbValues[3 * PixX + 1 + PixY * scanline] + CurrentGreen;
                            if (NewValue > 255)
                                rgbValues[3 * PixX + 1 + PixY * scanline] = 255;
                            else
                                rgbValues[3 * PixX + 1 + PixY * scanline] += CurrentGreen;

                            NewValue = rgbValues[3 * PixX + 2 + PixY * scanline] + CurrentRed;
                            if (NewValue > 255)
                                rgbValues[3 * PixX + 2 + PixY * scanline] = 255;
                            else
                                rgbValues[3 * PixX + 2 + PixY * scanline] += CurrentRed;

                        }
                    }
                }
            }
            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            BT.UnlockBits(bmpData);

            //   DrawLayers();

            // Draw the modified image.
            RefreshBMP();
            //ThisGraph.DrawImage(BT, this.StartViewX, this.StartViewY);
        }

        private Bitmap _backBuffer;





        public void RefreshBMP()
        {
            if ((BT == null) || (ThisGraph == null))
                ReDrawPic();
            else
            {
                ////ThisGraph.DrawImage(BT, this.StartViewX, this.StartViewY);
                ThisGraph.DrawImageUnscaled(BT, this.StartViewX, this.StartViewY);
            }

            // Thread.CurrentThread.Name = "Main";
            //Thread TH = new Thread(new ThreadStart(this.DrawLayers));
            //TH.Start();

            DrawLayers();

            //if (ToolStripMenuItem_UIModeSelection.Checked)
            //{
            //    if ((mRect.Width < 0) || (mRect.Height < 0))
            //        return;

            //        Brush NewBrush = new SolidBrush(Color.FromArgb(50, Color.LightYellow));
            //        Pen MyPen = new Pen(new SolidBrush(Color.Red), 1);

            //        int RealWidth = (int)((100 * mRect.Width) / (float)Zoom);
            //        int RealHeight = (int)((100 * mRect.Height ) / (float)Zoom);

            //        string Unit = "Pix";

            //        string FinalText = "[" + RealWidth + ";" + RealHeight + "] " + Unit;
            //        SizeF ForRect = ThisGraph.MeasureString(FinalText, cGlobalInfo.FontForImageDisplay);

            //        ThisGraph.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.Gray)), new Rectangle((int)mRect.X, (int)mRect.Y - 15, (int)ForRect.Width, (int)ForRect.Height));

            //        ThisGraph.DrawString(FinalText,   cGlobalInfo.FontForImageDisplay,
            //                                new SolidBrush(Color.Yellow),
            //                                (float)mRect.X,
            //                                (float)mRect.Y - 15);

            //        ThisGraph.DrawRectangle(MyPen, mRect);
            //        ThisGraph.FillRectangle(NewBrush, mRect);
            //        NewBrush.Dispose();

            //        this.Invalidate();
            //        return;
            //}
        }

        //public void DrawPic()
        //{
        //    if (this.LUTManager == null) return;
        //    if (this.ZNavigator == null) return;

        //    int ZPos = ZNavigator.trackBarForZPos.Value;

        //    ThisGraph = this.CreateGraphics();

        //    float ZoomFactor = Zoom / 100.0f;
        //    int NewWidth = (int)(AssociatedImage.Width * ZoomFactor);
        //    int NewHeight = (int)(AssociatedImage.Height * ZoomFactor);
        //    this.Width = NewWidth;
        //    this.Height = NewHeight;

        //    if ((NewHeight == 0) || (NewWidth == 0)) return;

        //    int PosViewX = (this.Width - NewWidth) / 2;
        //    int PosViewY = (this.Height - NewHeight) / 2;
        //    //   this.Location = new Point(PosViewX,PosViewY);

        //    BT = new Bitmap(NewWidth, NewHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

        //    // Lock the bitmap's bits.  
        //    Rectangle rect = new Rectangle(0, 0, NewWidth, NewHeight);
        //    System.Drawing.Imaging.BitmapData bmpData = BT.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, BT.PixelFormat);

        //    // Get the address of the first line.
        //    IntPtr ptr = bmpData.Scan0;

        //    int scanline = Math.Abs(bmpData.Stride);

        //    byte CurrentRed;
        //    byte CurrentGreen;
        //    byte CurrentBlue;

        //    int RealX;
        //    int RealY;

        //    int NewStartX = (int)(this.StartViewX / ZoomFactor);
        //    int NewStartY = (int)(this.StartViewY / ZoomFactor);

        //    List<float> Min = new List<float>();
        //    List<float> Max = new List<float>();
        //    List<double> GammaList = new List<double>();


        //    for (int IdxChannel = 0; IdxChannel < this.AssociatedImage.GetNumChannels(); IdxChannel++)
        //    {
        //        if (this.LUTManager != null)
        //        {
        //            UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.LUTManager.panelForLUTS.Controls[IdxChannel];

        //            Min.Add((float)SingleLUT.numericUpDownMinValue.Value);
        //            Max.Add((float)SingleLUT.numericUpDownMaxValue.Value);
        //            GammaList.Add((double)SingleLUT.trackBarGamma.Value / 100.0);
        //        }
        //    }

        //    #region Obsolete
        //    // test for new display
        //    //List<byte[][]> ListLut = new List<byte[][]>();
        //    //for (int IdxChannel = 0; IdxChannel < this.AssociatedImage.NumChannels; IdxChannel++)
        //    //{
        //    //    UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.LUTManager.panelForLUTS.Controls[IdxChannel];
        //    //    if (SingleLUT.checkBoxIsActive.Checked == false) continue;

        //    //    ListLut.Add(SingleLUT.SelectedLUT);
        //    //}
        //    //ConvertImageFloatToRGB(AssociatedImage, 0, 255, ListLut, 0);
        //    #endregion

        //    // Declare an array to hold the bytes of the bitmap. 
        //    int bytes = scanline * NewHeight;
        //    byte[] rgbValues = new byte[bytes];

        //    // Copy the RGB values into the array.
        //    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);



        //    for (int IdxChannel = 0; IdxChannel < this.AssociatedImage.GetNumChannels(); IdxChannel++)
        //    {
        //        UserControlSingleLUT SingleLUT = (UserControlSingleLUT)this.LUTManager.panelForLUTS.Controls[IdxChannel];
        //        if (SingleLUT.checkBoxIsActive.Checked == false) continue;

        //        byte[][] CurrentLUT = SingleLUT.SelectedLUT;
        //        //ConvertImageFloatToRGB(AssociatedImage, 0, 255, SingleLUT.SelectedLUT, 0);
        //        if (GammaList[IdxChannel] != 1)
        //        {
        //            for (int FullY = 0; FullY < NewHeight; FullY++)
        //            {
        //                RealY = (int)(FullY / ZoomFactor) + NewStartY;
        //                if (RealY >= AssociatedImage.Height) RealY = AssociatedImage.Height - 1;

        //                for (int FullX = 0; FullX < NewWidth; FullX++)
        //                {
        //                    RealX = (int)(FullX / ZoomFactor) + NewStartX;
        //                    if (RealX >= AssociatedImage.Width) RealX = AssociatedImage.Width - 1;

        //                    float Value = AssociatedImage.SingleChannelImage[IdxChannel].Data[RealX + RealY * AssociatedImage.Width + ZPos * AssociatedImage.SliceSize];


        //                    Value = (float)Math.Pow(Value, GammaList[IdxChannel]);


        //                    int ConvertedValue = (int)((((CurrentLUT[0].Length - 1) * (Value - Min[IdxChannel])) / (Max[IdxChannel] - Min[IdxChannel])));

        //                    if (ConvertedValue < 0) ConvertedValue = 0;
        //                    if (ConvertedValue >= CurrentLUT[0].Length) ConvertedValue = CurrentLUT[0].Length - 1;

        //                    CurrentRed = (byte)CurrentLUT[0][ConvertedValue];
        //                    CurrentGreen = (byte)CurrentLUT[1][ConvertedValue];
        //                    CurrentBlue = (byte)CurrentLUT[2][ConvertedValue];


        //                    double NewValue = rgbValues[3 * FullX + FullY * scanline] + CurrentBlue;
        //                    if (NewValue > 255)
        //                        rgbValues[3 * FullX + FullY * scanline] = 255;
        //                    else
        //                        rgbValues[3 * FullX + FullY * scanline] += CurrentBlue;

        //                    NewValue = rgbValues[3 * FullX + 1 + FullY * scanline] + CurrentGreen;
        //                    if (NewValue > 255)
        //                        rgbValues[3 * FullX + 1 + FullY * scanline] = 255;
        //                    else
        //                        rgbValues[3 * FullX + 1 + FullY * scanline] += CurrentGreen;

        //                    NewValue = rgbValues[3 * FullX + 2 + FullY * scanline] + CurrentRed;
        //                    if (NewValue > 255)
        //                        rgbValues[3 * FullX + 2 + FullY * scanline] = 255;
        //                    else
        //                        rgbValues[3 * FullX + 2 + FullY * scanline] += CurrentRed;
        //                }
        //            }

        //        }
        //        else
        //        {
        //            for (int FullY = 0; FullY < NewHeight; FullY++)
        //            {
        //                RealY = (int)(FullY / ZoomFactor) + NewStartY;
        //                if (RealY >= AssociatedImage.Height) RealY = AssociatedImage.Height - 1;

        //                for (int FullX = 0; FullX < NewWidth; FullX++)
        //                {
        //                    RealX = (int)(FullX / ZoomFactor) + NewStartX;
        //                    if (RealX >= AssociatedImage.Width) RealX = AssociatedImage.Width - 1;

        //                    float Value = AssociatedImage.SingleChannelImage[IdxChannel].Data[RealX + RealY * AssociatedImage.Width + ZPos * AssociatedImage.SliceSize];

        //                    int ConvertedValue = (int)((((CurrentLUT[0].Length - 1) * (Value - Min[IdxChannel])) / (Max[IdxChannel] - Min[IdxChannel])));

        //                    if (ConvertedValue < 0) ConvertedValue = 0;
        //                    if (ConvertedValue >= CurrentLUT[0].Length) ConvertedValue = CurrentLUT[0].Length - 1;

        //                    CurrentRed = (byte)CurrentLUT[0][ConvertedValue];
        //                    CurrentGreen = (byte)CurrentLUT[1][ConvertedValue];
        //                    CurrentBlue = (byte)CurrentLUT[2][ConvertedValue];


        //                    double NewValue = rgbValues[3 * FullX + FullY * scanline] + CurrentBlue;
        //                    if (NewValue > 255)
        //                        rgbValues[3 * FullX + FullY * scanline] = 255;
        //                    else
        //                        rgbValues[3 * FullX + FullY * scanline] += CurrentBlue;

        //                    NewValue = rgbValues[3 * FullX + 1 + FullY * scanline] + CurrentGreen;
        //                    if (NewValue > 255)
        //                        rgbValues[3 * FullX + 1 + FullY * scanline] = 255;
        //                    else
        //                        rgbValues[3 * FullX + 1 + FullY * scanline] += CurrentGreen;

        //                    NewValue = rgbValues[3 * FullX + 2 + FullY * scanline] + CurrentRed;
        //                    if (NewValue > 255)
        //                        rgbValues[3 * FullX + 2 + FullY * scanline] = 255;
        //                    else
        //                        rgbValues[3 * FullX + 2 + FullY * scanline] += CurrentRed;
        //                }
        //            }
        //        }
        //    }
        //    // Copy the RGB values back to the bitmap
        //    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

        //    // Unlock the bits.
        //    BT.UnlockBits(bmpData);

        //    // Draw the modified image.
        //    ThisGraph.DrawImage(BT, this.StartViewX, this.StartViewY);
        //    DrawLayers(ZoomFactor);
        //}

        public Bitmap ConvertImageFloatToRGB(cImage ImageFloat, float Min, float Max, List<byte[][]> ListSelectedLUTs, int Z)
        {
            Bitmap BMP = new Bitmap(ImageFloat.Width, ImageFloat.Height, PixelFormat.Format24bppRgb);
            BitmapData bData = BMP.LockBits(new Rectangle(0, 0, ImageFloat.Width, ImageFloat.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            /*the size of the image in bytes */
            int size = bData.Stride * bData.Height;

            /*Allocate buffer for image*/
            byte[] ConvertedValues = new byte[size];

            int Zpos = Z * ImageFloat.Width * ImageFloat.Height;
            int Ypos;

            float MAXMIN = Max - Min;
            double NewValue;
            byte CurrentRed;
            byte CurrentGreen;
            byte CurrentBlue;

            for (int Channel = 0; Channel < ImageFloat.GetNumChannels(); Channel++)
            {
                byte[][] SelectedLUT = ListSelectedLUTs[Channel];
                int LUTLength = SelectedLUT[0].Length - 1;
                for (int j = 0; j < ImageFloat.Height; j++)
                {
                    Ypos = j * AssociatedImage.Width;
                    for (int i = 0; i < ImageFloat.Width; i++)
                    {
                        float Value = ImageFloat.SingleChannelImage[Channel].Data[i + Ypos + Zpos];
                        int ConvertedValue = (int)((LUTLength * (Value - Min)) / MAXMIN);
                        if (ConvertedValue < 0) ConvertedValue = 0;
                        else if (ConvertedValue > LUTLength) ConvertedValue = LUTLength;

                        CurrentRed = (byte)SelectedLUT[0][ConvertedValue];
                        CurrentGreen = (byte)SelectedLUT[1][ConvertedValue];
                        CurrentBlue = (byte)SelectedLUT[2][ConvertedValue];

                        NewValue = ConvertedValues[i * 3 + j * bData.Stride] + CurrentBlue;
                        if (NewValue > 255)
                            ConvertedValues[i * 3 + j * bData.Stride] = 255;
                        else
                            ConvertedValues[i * 3 + j * bData.Stride] += CurrentBlue;

                        NewValue = ConvertedValues[i * 3 + j * bData.Stride + 1] + CurrentGreen;
                        if (NewValue > 255)
                            ConvertedValues[i * 3 + j * bData.Stride + 1] = 255;
                        else
                            ConvertedValues[i * 3 + j * bData.Stride + 1] += CurrentGreen;

                        NewValue = ConvertedValues[i * 3 + j * bData.Stride + 2] + CurrentRed;
                        if (NewValue > 255)
                            ConvertedValues[i * 3 + j * bData.Stride + 2] = 255;
                        else
                            ConvertedValues[i * 3 + j * bData.Stride + 2] += CurrentRed;


                        //for (int RGBIdx = 0; RGBIdx < 3; RGBIdx++)
                        //{

                        //    NewValue = (ConvertedValues[i * 3 + j * bData.Stride + RGBIdx] + ConvertedValue);
                        //    if (NewValue >= LUTLength) NewValue = LUTLength - 1;

                        //    // now go through the LUT
                        //    //ConvertedValues[i * 3 + j * bData.Stride + 0] = SelectedLUT[2][NewValue];
                        //    //ConvertedValues[i * 3 + j * bData.Stride + 1] = SelectedLUT[1][NewValue];
                        //    ConvertedValues[i * 3 + j * bData.Stride + RGBIdx] = SelectedLUT[RGBIdx][NewValue];
                        //}

                        //ConvertedValues[i * 3 + j * bData.Stride + Channel] = (byte)NewValue;
                    }
                }
            }

            //for (int j = 0; j < ImageFloat.Height; j++)
            //{
            //    Ypos = j * AssociatedImage.Width;
            //    for (int i = 0; i < ImageFloat.Width; i++)
            //    {
            //        BMP.SetPixel(i, j, Color.FromArgb(SelectedLUT[0][ConvertedValues[i+Ypos]],
            //                               SelectedLUT[1][ConvertedValues[i + Ypos]],
            //                               SelectedLUT[2][ConvertedValues[i + Ypos]]));
            //    }
            //}


            /*This overload copies data of /size/ into /data/ from location specified (/Scan0/)*/
            //System.Runtime.InteropServices.Marshal.Copy(bData.Scan0, ConvertedValues, 0, size);

            //for (int i = 0; i < size; i += bitsPerPixel / 8)
            //{
            //    double magnitude = 1 / 3d * (ConvertedValues[i] + ConvertedValues[i + 1] + ConvertedValues[i + 2]);

            //    //data[i] is the first of 3 bytes of color

            //}

            /* This override copies the data back into the location specified */
            System.Runtime.InteropServices.Marshal.Copy(ConvertedValues, 0, bData.Scan0, ConvertedValues.Length);

            BMP.UnlockBits(bData);




            Form1 F = new Form1();
            F.pictureBox1.Image = BMP;
            F.ShowDialog();
            return BMP;
        }

        void DrawLayers()
        {
            float ZoomFactor = this.Zoom / 100.0f;
            System.Drawing.Graphics formGraphics = this.CreateGraphics();


            #region Annotations
            List<string> list = new List<string>(DictionaryForNotations.Keys);
            // Loop through list
            foreach (string k in list)
            {
                cObjectForAnnotation TmpObj = DictionaryForNotations[k];

                int TmpShiftX = ShiftX;
                int TmpShiftY = ShiftY;

                if (!TmpObj.OnlyOnImage)
                {
                    TmpShiftX = 0;
                    TmpShiftY = 0;
                }

                if (TmpObj.GetType() == typeof(cString))
                {
                    cString CurrentString = ((cString)TmpObj);
                    ThisGraph.DrawString(CurrentString.Text,
                        new Font(FontFamily.GenericSansSerif, (float)CurrentString.Size.X),
                                        new SolidBrush(CurrentString.ObjectColor),
                                        (float)CurrentString.Pos.X * ZoomFactor + TmpShiftX,
                                        (float)CurrentString.Pos.Y * ZoomFactor + TmpShiftY);

                }
                if (TmpObj.GetType() == typeof(cDisk))
                {
                    cDisk CurrentDisk = ((cDisk)TmpObj);

                    Brush NewBrush = new SolidBrush(CurrentDisk.ObjectColor);

                    float Width = (float)CurrentDisk.Size.X * ZoomFactor;
                    float Height = (float)CurrentDisk.Size.Y * ZoomFactor;
                    float PosX = (float)(CurrentDisk.Pos.X * ZoomFactor - Width / 2) + TmpShiftX;
                    float PosY = (float)(CurrentDisk.Pos.Y * ZoomFactor - Height / 2) + TmpShiftY;


                    ThisGraph.FillEllipse(NewBrush, PosX, PosY, Width, Height);
                    NewBrush.Dispose();
                }
                if (TmpObj.GetType() == typeof(cSquare))
                {
                    cSquare CurrentDisk = ((cSquare)TmpObj);

                    Brush NewBrush = new SolidBrush(CurrentDisk.ObjectColor);

                    float Width = (float)CurrentDisk.Size.X * ZoomFactor;
                    float Height = (float)CurrentDisk.Size.Y * ZoomFactor;
                    float PosX = (float)CurrentDisk.Pos.X * ZoomFactor + TmpShiftX;
                    float PosY = (float)CurrentDisk.Pos.Y * ZoomFactor + TmpShiftY;

                    Pen MyPen = new Pen(NewBrush);

                    ThisGraph.DrawRectangle(MyPen, new Rectangle((int)PosX, (int)PosY, (int)Width, (int)Height));
                    NewBrush.Dispose();
                }
                if (TmpObj.GetType() == typeof(cLine))
                {
                    cLine CurrentLine = ((cLine)TmpObj);

                    Brush NewBrush = new SolidBrush(CurrentLine.ObjectColor);

                    Pen MyPen = new Pen(NewBrush);

                    ThisGraph.DrawLine(MyPen, new Point((int)CurrentLine.Pos.X, (int)CurrentLine.Pos.Y), new Point((int)CurrentLine.Size.X, (int)CurrentLine.Size.Y));
                    NewBrush.Dispose();
                }
            }
            #endregion

            if (_ToolStripMenuItemDisplayScale.Checked)
            {
                int LineLenght = (this.Width - ShiftX * 2) / 8;
                double RealLenght = LineLenght / ZoomFactor;

                // int iRealLenght = (int)RealLenght;
                //LineLenght = (int)(RealLenght);
                int PosLineY = this.Height - ShiftY - (int)(20);


                string ToDisp = LineLenght.ToString() + " pix";

                if (AssociatedImage.Resolution.X != 0)
                {
                    RealLenght = RealLenght / AssociatedImage.Resolution.X;
                    ToDisp = RealLenght.ToString("N1") + " " + ((Char)(181)).ToString() + "m";
                }
                else
                {
                    RealLenght = LineLenght;
                    ToDisp = RealLenght.ToString() + " pix.";
                }

                SizeF FontSize = ThisGraph.MeasureString(ToDisp, cGlobalInfo.FontForImageDisplay);
                float ShiftString = (LineLenght - FontSize.Width) / 2;
                if (ShiftString < 0) ShiftString = 0;



                Color ToDraw = Color.FromArgb(200, 200, 200);

                // draw the LUT'
                for (int IdxChannel = 0; IdxChannel < AssociatedImage.GetNumChannels(); IdxChannel++)
                {
                    if (!((UserControlSingleLUT)LUTManager.panelForLUTS.Controls[IdxChannel]).checkBoxIsActive.Checked) continue;
                    Size SizeRectLUT = new Size(10, 30);
                    //Rectangle RectForFill0 = new Rectangle(new Point(ShiftX + 5, PosLineY - 20), SizeRectLUT);
                    //ThisGraph.FillRectangle(new SolidBrush(Color.FromArgb(80, Color.White)), RectForFill0);
                    Rectangle RectForLUT = new Rectangle(new Point(10, 10 + (SizeRectLUT.Height + 15) * IdxChannel), SizeRectLUT);

                    Image TmpIm = ((UserControlSingleLUT)LUTManager.panelForLUTS.Controls[IdxChannel]).pictureBoxForColorSample.Image;
                    Image NewIm = (Image)TmpIm.Clone();
                    NewIm.RotateFlip(RotateFlipType.Rotate270FlipNone);

                    ThisGraph.DrawImage(NewIm, RectForLUT);
                    string MyString = ((UserControlSingleLUT)LUTManager.panelForLUTS.Controls[IdxChannel]).numericUpDownMinValue.Value.ToString();
                    ThisGraph.DrawString(MyString,
                                         cGlobalInfo.FontForImageDisplay,
                                         new SolidBrush(ToDraw),
                                         new Point(RectForLUT.Location.X + RectForLUT.Width + 3,
                                                        RectForLUT.Location.Y + RectForLUT.Height - (int)(ThisGraph.MeasureString(MyString, cGlobalInfo.FontForImageDisplay).Height / 2)));

                    MyString = ((UserControlSingleLUT)LUTManager.panelForLUTS.Controls[IdxChannel]).numericUpDownMaxValue.Value.ToString();
                    ThisGraph.DrawString(MyString,
                                         cGlobalInfo.FontForImageDisplay,
                                         new SolidBrush(ToDraw),
                                         new Point(RectForLUT.Location.X + RectForLUT.Width + 3,
                                                        RectForLUT.Location.Y - (int)(ThisGraph.MeasureString(MyString, cGlobalInfo.FontForImageDisplay).Height / 2)));

                }

                ToDraw = Color.FromArgb(20, 20, 20);
                // now draw the scale
                Rectangle RectForFill = new Rectangle(new Point(ShiftX + 5, PosLineY - 20), new Size(LineLenght + 10, 25));
                ThisGraph.FillRectangle(new SolidBrush(Color.FromArgb(80, Color.White)), RectForFill);
                ThisGraph.DrawLine(new Pen(ToDraw, 2), new Point(ShiftX + 10, PosLineY),
                                                       new Point(ShiftX + 10 + LineLenght, PosLineY));
                ThisGraph.DrawString(ToDisp, cGlobalInfo.FontForImageDisplay, new SolidBrush(ToDraw), new Point(ShiftX + 10 + (int)ShiftString, PosLineY - 15));

                // Clipboard.SetDataObject(ThisGraph, true);

            }

            formGraphics.Dispose();
        }


        #region Events
        private void _ToolStripMenuItemInfo(object sender, EventArgs e)
        {
            cExtendedTable ET = this.AssociatedImage.GetPropertyTable();
            cDisplayExtendedTable DET = new cDisplayExtendedTable();
            DET.SetInputData(ET);
            DET.Run();
        }

        private void ToolStripMenuItemDisplayScale(object sender, EventArgs e)
        {
            DrawLayers();
        }

        public void AddNotation(cObjectForAnnotation ObjectForNotation, string Name)
        {
            //this.ListObjectForNotations.Add(ObjectForNotation);

            //  this.DictionaryForNotations.
            this.DictionaryForNotations.Add(Name, ObjectForNotation);

        }

        public void RemoveNotation(string Name)
        {
            //this.ListObjectForNotations.Add(ObjectForNotation);
            this.DictionaryForNotations.Remove(Name);
        }

        private void NavigatorToolStripMenuItem(object sender, EventArgs e)
        {
            if (this.ZNavigator == null)
                this.ZNavigator.Show();
            else
            {
                this.ZNavigator.Visible = true;
                this.ZNavigator.BringToFront();
            }
        }

        private void lUTManagerToolStripMenuItem(object sender, EventArgs e)
        {
            if (this.LUTManager == null)
                LUTManager.Show();
            else
            {
                this.LUTManager.Visible = true;
                this.LUTManager.BringToFront();
            }
        }

        public void CurrentPanel_DragDrop(object sender, DragEventArgs e)
        {
            cImage SourceImage = (cImage)e.Data.GetData(typeof(cImage));

            cDisplaySingleImage NewView = new cDisplaySingleImage();
            cImage NewIm = new cImage(SourceImage.Width, SourceImage.Height, SourceImage.Depth, SourceImage.GetNumChannels() + this.AssociatedImage.GetNumChannels());

            int IdxChannel = 0;
            for (int i = 0; i < SourceImage.GetNumChannels(); i++)
                Array.Copy(SourceImage.SingleChannelImage[i].Data, NewIm.SingleChannelImage[IdxChannel++].Data, NewIm.SliceSize);

            for (int i = 0; i < this.AssociatedImage.GetNumChannels(); i++)
                Array.Copy(this.AssociatedImage.SingleChannelImage[i].Data, NewIm.SingleChannelImage[IdxChannel++].Data, NewIm.SliceSize);

            NewView.SetInputData(NewIm);

            NewView.Run();


            cGlobalInfo.WindowHCSAnalyzer.richTextBoxConsole.AppendText("DragNDrop: from " + SourceImage.Name + " to " + this.AssociatedImage.Name + "\n");
        }

        public void CurrentPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(cImage)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public void FormForImageDisplay_Disposed(object sender, EventArgs e)
        {
            this.LUTManager.Close();
            this.ZNavigator.Close();

            this.AssociatedImage.Dispose();
        }

        public void FormForImageDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.LUTManager.Close();
            this.ZNavigator.Close();

            this.AssociatedImage.Dispose();
        }

        public void panelForImage_Paint(object sender, PaintEventArgs e)
        {
            //ReDrawPic();
            RefreshBMP();
        }

        public void panelForImage_Resize(object sender, EventArgs e)
        {
            if (IsZoomLocked)
                ZoomToFit(2);
            else
                ReDrawPic();
        }

        public void panelForImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (ToolStripMenuItem_UIModeSelection.Checked == false)
            {
                ChangeZoomValue(100);

            }
            else
            {
                if (CheckIfPointInActiveArea(e.Location))
                {
                    RefreshBMP();
                    ChangeZoomValue(100);
                    SetIsZoomLocked(false);
                }
                else
                {
                    mRect.Height = mRect.Width = -1;
                    RefreshBMP();
                }
            }
        }

        public void panelForImage_MouseDown(object sender, MouseEventArgs e)
        {


            FirstClickPosX = e.X;
            FirstClickPosY = e.Y;



            if (e.Clicks != 1) return;
            //if (e.Button == MouseButtons.Left)
            //{
            //    cListWells ListWells = new cListWells(null);

            //    if (Control.ModifierKeys == (Keys.Control | Keys.Shift))
            //    {
            //        foreach (cPlate TmpPlate in cGlobalInfo.CurrentScreening.ListPlatesActive)
            //        {
            //            foreach (var item in TmpPlate.ListActiveWells)
            //            {
            //                if (item.GetCurrentClassIdx() == this.GetCurrentClassIdx())
            //                    ListWells.Add(item);
            //            }
            //        }
            //    }
            //    else if (Control.ModifierKeys == Keys.Shift)
            //    {
            //        foreach (var item in AssociatedPlate.ListActiveWells)
            //        {
            //            if (item.GetCurrentClassIdx() == this.GetCurrentClassIdx())
            //                ListWells.Add(item);
            //        }
            //    }
            //    else
            //        ListWells.Add(this);

            //    this.AssociatedChart.DoDragDrop(ListWells, DragDropEffects.Copy);
            //    return;
            //}

            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                this.DoDragDrop(this.AssociatedImage, DragDropEffects.Copy);
            }
            //this.Invalidate();
        }

        public void panelForImage_MouseMove(object sender, MouseEventArgs e)
        {
            //   if (!base.Focused) return;
            if (ToolStripMenuItem_UIModeSelection.Checked)
            {

                if (e.Button == MouseButtons.Left)
                {
                    int TopX = 0, TopY = 0, BottomX = 0, BottomY = 0;

                    if (e.X > FirstClickPosX)
                    {
                        BottomX = FirstClickPosX;
                        TopX = e.X;
                    }
                    else
                    {
                        BottomX = e.X;
                        TopX = FirstClickPosX;
                    }
                    if (e.Y > FirstClickPosY)
                    {
                        BottomY = FirstClickPosY;
                        TopY = e.Y;
                    }
                    else
                    {
                        BottomY = e.Y;
                        TopY = FirstClickPosY;
                    }

                    mRect = new Rectangle(BottomX, BottomY, TopX - BottomX, TopY - BottomY);

                    Brush NewBrush = new SolidBrush(Color.FromArgb(50, Color.LightYellow));
                    Pen MyPen = new Pen(new SolidBrush(Color.Red), 1);

                    //int RealX = (int)((PosX - ShiftX) / ZoomFactor);
                    //int RealY = (int)((PosY - ShiftY) / ZoomFactor);

                    int RealWidth = (int)((100 * (mRect.Width)) / (float)Zoom);
                    int RealHeight = (int)((100 * (mRect.Height)) / (float)Zoom);

                    string Unit = "Pix";

                    RefreshBMP();

                    string FinalText = "";

                    if (!cGlobalInfo.OptionsWindow.FFAllOptions.radioButtonUISelectionModeDim.Checked)
                    {
                        cImageDisplayProperties IDP = this.AssociatedImage.AssociatedImagePanel.LUTManager.GetImageDisplayProperties();

                        

                        for (int i = 0; i < this.AssociatedImage.GetNumChannels(); i++)
                        {
                            if (IDP.ListActive[i])
                            {
                                double ValueChannel0 = this.AssociatedImage.SingleChannelImage[0].GetTotalIntensity(new cPoint3D(BottomX - ShiftX, BottomY - ShiftY, 0), new cPoint3D(TopX - ShiftX, TopY - ShiftY, 0));
                                FinalText += ValueChannel0.ToString()+"\n";
                            }

                        }
                    }
                    else
                    {

                        FinalText = "[" + RealWidth + ";" + RealHeight + "] " + Unit;
                        SizeF ForRect = ThisGraph.MeasureString(FinalText, cGlobalInfo.FontForImageDisplay);

                        ThisGraph.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.Gray)), new Rectangle((int)BottomX, (int)BottomY - 15, (int)ForRect.Width, (int)ForRect.Height));


                    }

                    ThisGraph.DrawString(FinalText,
                                            cGlobalInfo.FontForImageDisplay,
                                            new SolidBrush(Color.Yellow),
                                            (float)BottomX,
                                            (float)BottomY - 15);


                    ThisGraph.DrawRectangle(MyPen, mRect);
                    ThisGraph.FillRectangle(NewBrush, mRect);
                    NewBrush.Dispose();

                    //  this.Invalidate();   
                    //  RefreshBMP();
                    return;
                }
            }
            else if (ToolStripMenuItem_UIModeIntensity.Checked)
            {
                float ZoomFactor = Zoom / 100.0f;

                int PosX = e.X;
                int PosY = e.Y;
                int PosZ = ZNavigator.trackBarForZPos.Value;

                if ((PosX >= ShiftX) && (PosY >= ShiftY) && (PosX < ShiftX + ZoomedWidth) && (PosY < ShiftY + ZoomedHeight))
                {
                    int RealX = (int)((PosX - ShiftX) / ZoomFactor);
                    int RealY = (int)((PosY - ShiftY) / ZoomFactor);

                    string FinalText = "[" + RealX + ";" + RealY + "]\n(";
                    cImageDisplayProperties IDP = this.AssociatedImage.AssociatedImagePanel.LUTManager.GetImageDisplayProperties();
                    
                    for (int IdxChannel = 0; IdxChannel < AssociatedImage.GetNumChannels(); IdxChannel++)
                    {
                        if (IDP.ListActive[IdxChannel])
                        {
                            float Value = AssociatedImage.SingleChannelImage[IdxChannel].Data[RealX + RealY * AssociatedImage.Width + PosZ * AssociatedImage.SliceSize];
                            FinalText += Value.ToString("N3") + ";";
                        }
                    }
                    FinalText = FinalText.Remove(FinalText.Length - 1);
                    FinalText += ")";

                    RefreshBMP();

                    SizeF ForRect = ThisGraph.MeasureString(FinalText, cGlobalInfo.FontForImageDisplay);
                    ThisGraph.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.Gray)), new Rectangle((int)e.X + 12, (int)e.Y, (int)ForRect.Width, (int)ForRect.Height));

                    ThisGraph.DrawString(FinalText, cGlobalInfo.FontForImageDisplay, new SolidBrush(Color.Yellow), (float)e.X + 12, (float)e.Y);

                }
                else
                {
                    RefreshBMP();
                }
            }
            else
            {
                RefreshBMP();
            }



            //DrawLayers();
            //else
            //{
            //    toolStripStatusLabelForPosition.Text = "(#,#)";
            //}
        }



        #endregion
    }
}
