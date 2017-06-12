using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;
using HCSAnalyzer.Forms.FormsForGraphsDisplay;
using System.Windows.Forms;
using System.Data;
using LibPlateAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataProcessing;
using HCSAnalyzer.Classes.MetaComponents;
using HCSAnalyzer.Classes.Base_Classes.DataAnalysis;
using HCSAnalyzer.Classes.DataAnalysis;
using HCSAnalyzer.Classes.Base_Classes.DataManip;
using HCSAnalyzer.Classes.Base_Classes.Viewers._2D;
using System.IO;
using ImageAnalysis;

namespace HCSAnalyzer.Classes.Base_Classes.Viewers
{

    public enum eImageFileType { TIF, JPG };

    public class cImageToFile : cComponent
    {
        cImage Input = null;

        //public int DigitNumber = 2;
        //public string Separator = ",";
        //public string FileExtension = ".csv";
        public string FilePath = "";
        public bool IsDisplayUIForFilePath = false;
        //public bool IsRunEXCEL = false;
        public eImageFileType ImageFileType = eImageFileType.TIF;

        public cImageToFile()
        {
            Title = "Image to File";
        }

        public void SetInputData(cImage MyData)
        {
            this.Input = MyData;
        }

        public cFeedBackMessage Run()
        {
            if (this.Input == null)
            {
                FeedBackMessage.IsSucceed = false;
                FeedBackMessage.Message = "No input data defined.";
                return FeedBackMessage;
            }

            if (IsDisplayUIForFilePath)
            {
                SaveFileDialog CurrSaveFileDialog = new SaveFileDialog();

                CurrSaveFileDialog.Filter = "tif files (*.tif)|*.tif";
                System.Windows.Forms.DialogResult Res = CurrSaveFileDialog.ShowDialog();
                if (Res != System.Windows.Forms.DialogResult.OK)
                {
                    FeedBackMessage.IsSucceed = false;
                    FeedBackMessage.Message = "Incorrect File Name.";
                    return FeedBackMessage;
                }
                FilePath = CurrSaveFileDialog.FileName;
            }

            loci.formats.@out.TiffWriter MyWritter = new loci.formats.@out.TiffWriter();

            //MyWritter.setSeries(1);
            loci.formats.meta.IMetadata metadata = loci.formats.MetadataTools.createOMEXMLMetadata();
            metadata.createRoot();
            metadata.setImageID("Image:0", 0);
            metadata.setPixelsID("Pixels:0", 0);
            metadata.setPixelsBinDataBigEndian(java.lang.Boolean.TRUE, 0, 0);
            metadata.setPixelsDimensionOrder(ome.xml.model.enums.DimensionOrder.XYCZT, 0);
            metadata.setPixelsType(ome.xml.model.enums.PixelType.UINT8, 0);


            //java.lang.Integer a = new 

            ome.xml.model.primitives.PositiveInteger imageWidth = new ome.xml.model.primitives.PositiveInteger(new java.lang.Integer(this.Input.Width));
            ome.xml.model.primitives.PositiveInteger imageHeight = new ome.xml.model.primitives.PositiveInteger(new java.lang.Integer(this.Input.Height));
            ome.xml.model.primitives.PositiveInteger numZSections = new ome.xml.model.primitives.PositiveInteger(new java.lang.Integer(this.Input.Depth));
            ome.xml.model.primitives.PositiveInteger numChannels = new ome.xml.model.primitives.PositiveInteger(new java.lang.Integer(this.Input.GetNumChannels()));
            ome.xml.model.primitives.PositiveInteger numTimepoints = new ome.xml.model.primitives.PositiveInteger(new java.lang.Integer(1));
            ome.xml.model.primitives.PositiveInteger samplesPerPixel = new ome.xml.model.primitives.PositiveInteger(new java.lang.Integer(1));



            metadata.setPixelsSizeX(imageWidth, 0);
            metadata.setPixelsSizeY(imageHeight, 0);
            metadata.setPixelsSizeZ(numZSections, 0);
            metadata.setPixelsSizeC(numChannels, 0);
            metadata.setPixelsSizeT(numTimepoints, 0);

            for (int channel = 0; channel < this.Input.GetNumChannels(); channel++)
            {
                metadata.setChannelID("Channel:0:" + channel, 0, channel);
                metadata.setChannelSamplesPerPixel( new ome.xml.model.primitives.PositiveInteger(new java.lang.Integer(1)), 0, channel);
            }

            MyWritter.setMetadataRetrieve(metadata);


            MyWritter.setId(FilePath);

            for (int IdxChannel = 0; IdxChannel < this.Input.GetNumChannels(); IdxChannel++)
            {
                List<byte> rgbValues = new List<byte>();
                for (int IdxZ = 0; IdxZ < this.Input.Depth; IdxZ++)
                    for (int IdxY = 0; IdxY < this.Input.Height; IdxY++)
                        for (int IdxX = 0; IdxX < this.Input.Width; IdxX++)
                        {
                            rgbValues.Add((byte)this.Input.SingleChannelImage[IdxChannel].Data[IdxX + IdxY * this.Input.Width + IdxZ * this.Input.SliceSize]);

                        }
                MyWritter.saveBytes(IdxChannel, rgbValues.ToArray());
            }


            //            writer.saveBytes(0, plane); % channel 0, timepoint 0
            //writer.saveBytes(1, plane); % channel 1, timepoint 0
            //writer.saveBytes(2, plane); % channel 0, timepoint 1
            //writer.saveBytes(3, plane); % channel 1, timepoint 1

            MyWritter.close();

            return FeedBackMessage;
        }
    }
}
