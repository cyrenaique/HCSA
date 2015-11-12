using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageAnalysis;
using System.Collections;
using HCSAnalyzer.Classes._3D;

namespace HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.Detection
{

    public enum eConnectivity
    {
        TWOD_4, TWOD_8, TWOD_24, THREED_6, THREED_26
    }


    public class ConnectedComponentSet : IEnumerable, IDisposable
    {
        #region Private members

        private float min, max;
        private int width, height, depth, scanSliceSize, twoTimesWidth;
        private eConnectivity connectivity;

        private cImage labeledInputimage, labeledOutputImage, image;
        private float[] labeledInput, labeledOutput;
        private ObjectGraph graph;
        private ConnectedVoxels[] cc;

        #endregion

        #region External Accessors

        /// <summary> The number of detected objects 
        /// </summary>
        public int Count
        {
            get
            {
                return (cc != null) ? cc.Length : 0;
            }
        }

        /// <summary> The detected objects
        /// </summary>
        public ConnectedVoxels this[int index]
        {
            get { return cc[index]; }
        }

        /// <summary>
        /// Get the internal labeled image corresponding to this set 
        /// </summary>
        public cImage LabeledImage
        {
            get { return labeledOutputImage; }
        }

        #endregion

        #region Constructors

        /// <summary> 3D connected components detector
        /// Computes various information on the detected objects
        /// </summary>
        /// <param name="labeledInputimage">Gray level input image (background value must be 0)</param>
        /// <param name="band">the image band</param>
        /// <param name="connectivity">Connectivity. MUST be 6 or 26</param>
        /// <param name="minVolume">Minimum object volume to consider</param>
        /// <param name="maxVolume">Maximum object volume to consider</param>
        public ConnectedComponentSet(cImage labeledInputimage, int band, eConnectivity connectivity, float minVolume, float maxVolume)
            : this(labeledInputimage, null, null, band, connectivity, minVolume, maxVolume) { }

        /// <summary> 3D connected components detector
        /// Computes various information on the detected objects
        /// </summary>
        /// <param name="labeledInputimage">Gray level input image (background value must be 0)</param>
        /// <param name="labeledOutputImage">Empty input image of same size that labeledInput image (can be null)</param>
        /// <param name="image">Original greyscale image (can be null)</param>
        /// <param name="band">the image band</param>
        /// <param name="connectivity">Connectivity. MUST be 6 or 26</param>
        /// <param name="minVolume">Minimum object volume to consider</param>
        /// <param name="maxVolume">Maximum object volume to consider</param>
        public ConnectedComponentSet(cImage labeledInputimage, cImage labeledOutputImage, cImage image, int band, eConnectivity connectivity, float minVolume, float maxVolume)
        {
            if (image != null && (image.Width != labeledInputimage.Width || image.Height != labeledInputimage.Height || image.Depth != labeledInputimage.Depth))
            {
                if (connectivity == eConnectivity.THREED_6 || connectivity == eConnectivity.THREED_26)
                {
                    //  throw new DescriptorException("Uncompatible Image sizes");
                }
            }

            if (labeledOutputImage != null && (labeledOutputImage.Width != labeledInputimage.Width || labeledOutputImage.Height != labeledInputimage.Height || labeledOutputImage.Depth != labeledInputimage.Depth))
            {
                if (connectivity == eConnectivity.THREED_6 || connectivity == eConnectivity.THREED_26)
                {
                    // throw new DescriptorException("Uncompatible Image sizes");
                }
                if (labeledOutputImage.GetNumChannels() != 1)
                {
                    //  throw new DescriptorException("Output label image must be single band");
                }
            }

            if (labeledInputimage.Depth == 1)
            {
                if (connectivity == eConnectivity.THREED_6 || connectivity == eConnectivity.THREED_26)
                {
                    //  throw new DescriptorException("Given connectivity is incompatible with the image depth");
                }
            }
            else if (connectivity == eConnectivity.TWOD_4 || connectivity == eConnectivity.TWOD_8 || connectivity == eConnectivity.TWOD_24)
            {
                //  throw new DescriptorException("Given connectivity is incompatible with the image depth");
            }

            this.min = minVolume;
            this.max = maxVolume;
            this.connectivity = connectivity;
            this.width = labeledInputimage.Width;
            this.height = labeledInputimage.Height;
            this.depth = labeledInputimage.Depth;
            this.scanSliceSize = width * height;
            this.twoTimesWidth = 2 * width;

            if (labeledOutputImage == null)
            {
                this.labeledOutputImage = new cImage(labeledInputimage.Width, labeledInputimage.Height, labeledInputimage.Depth, 1);
            }
            else
            {
                this.labeledOutputImage = labeledOutputImage;
            }
            this.labeledOutput = this.labeledOutputImage.SingleChannelImage[0].Data;
            this.labeledInputimage = labeledInputimage;
            this.labeledInput = labeledInputimage.SingleChannelImage[band].Data;
            this.image = image;

            FindConnectedComponents();
        }

        /// <summary> 3D connected components detector
        /// Computes various information on the detected objects
        /// </summary>
        /// <param name="labeledInputimage">Gray level input image (background value must be 0)</param>
        /// <param name="BandLabelInput">Channel for the K-Mean Image</param>
        /// <param name="labeledOutputImage">Empty input image of same size that labeledInput image (can be null)</param>
        /// <param name="image">Original greyscale image (can be null)</param>
        /// <param name="band">the image band</param>
        /// <param name="connectivity">Connectivity. MUST be 6 or 26</param>
        /// <param name="minVolume">Minimum object volume to consider</param>
        /// <param name="maxVolume">Maximum object volume to consider</param>
        public ConnectedComponentSet(cImage labeledInputimage, int BandLabelInput, cImage labeledOutputImage, cImage image, int band, eConnectivity connectivity, float minVolume, float maxVolume)
        {
            if (image != null && (image.Width != labeledInputimage.Width || image.Height != labeledInputimage.Height || image.Depth != labeledInputimage.Depth))
            {
                if (connectivity == eConnectivity.THREED_6 || connectivity == eConnectivity.THREED_26)
                {
                    // throw new DescriptorException("Uncompatible Image sizes");
                }
            }

            if (labeledOutputImage != null && (labeledOutputImage.Width != labeledInputimage.Width || labeledOutputImage.Height != labeledInputimage.Height || labeledOutputImage.Depth != labeledInputimage.Depth))
            {
                if (connectivity == eConnectivity.THREED_6 || connectivity == eConnectivity.THREED_26)
                {
                    //  throw new DescriptorException("Uncompatible Image sizes");
                }
                if (labeledOutputImage.GetNumChannels() != 1)
                {
                    // throw new DescriptorException("Output label image must be single band");
                }
            }

            if (labeledInputimage.Depth == 1)
            {
                if (connectivity == eConnectivity.THREED_6 || connectivity == eConnectivity.THREED_26)
                {
                    //throw new DescriptorException("Given connectivity is incompatible with the image depth");
                }
            }
            else if (connectivity == eConnectivity.TWOD_4 || connectivity == eConnectivity.TWOD_8 || connectivity == eConnectivity.TWOD_24)
            {
                // throw new DescriptorException("Given connectivity is incompatible with the image depth");
            }

            this.min = minVolume;
            this.max = maxVolume;
            this.connectivity = connectivity;
            this.width = labeledInputimage.Width;
            this.height = labeledInputimage.Height;
            this.depth = labeledInputimage.Depth;
            this.scanSliceSize = width * height;
            this.twoTimesWidth = 2 * width;

            if (labeledOutputImage == null)
            {
                this.labeledOutputImage = new cImage(labeledInputimage.Width, labeledInputimage.Height, labeledInputimage.Depth, 1);
            }
            else
            {
                this.labeledOutputImage = labeledOutputImage;
            }
            this.labeledOutput = this.labeledOutputImage.SingleChannelImage[0].Data;
            this.labeledInputimage = labeledInputimage;
            this.labeledInput = labeledInputimage.SingleChannelImage[BandLabelInput].Data;
            this.image = image;

            FindConnectedComponents();
        }


        public ConnectedComponentSet(cImage labeledInputimage, int BandLabelInput, cImage labeledOutputImage, int LabelBand, cImage image, int band, eConnectivity connectivity, float minVolume, float maxVolume)
        {
            if (image != null && (image.Width != labeledInputimage.Width || image.Height != labeledInputimage.Height || image.Depth != labeledInputimage.Depth))
            {
                if (connectivity == eConnectivity.THREED_6 || connectivity == eConnectivity.THREED_26)
                {
                    // throw new DescriptorException("Uncompatible Image sizes");
                }
            }

            if (labeledOutputImage != null && (labeledOutputImage.Width != labeledInputimage.Width || labeledOutputImage.Height != labeledInputimage.Height || labeledOutputImage.Depth != labeledInputimage.Depth))
            {
                if (connectivity == eConnectivity.THREED_6 || connectivity == eConnectivity.THREED_26)
                {
                    //  throw new DescriptorException("Uncompatible Image sizes");
                }
                if (labeledOutputImage.GetNumChannels() != 1)
                {
                    //   throw new DescriptorException("Output label image must be single band");
                }
            }

            if (labeledInputimage.Depth == 1)
            {
                if (connectivity == eConnectivity.THREED_6 || connectivity == eConnectivity.THREED_26)
                {
                    // throw new DescriptorException("Given connectivity is incompatible with the image depth");
                }
            }
            else if (connectivity == eConnectivity.TWOD_4 || connectivity == eConnectivity.TWOD_8 || connectivity == eConnectivity.TWOD_24)
            {
                // throw new DescriptorException("Given connectivity is incompatible with the image depth");
            }

            this.min = minVolume;
            this.max = maxVolume;
            this.connectivity = connectivity;
            this.width = labeledInputimage.Width;
            this.height = labeledInputimage.Height;
            this.depth = labeledInputimage.Depth;
            this.scanSliceSize = width * height;
            this.twoTimesWidth = 2 * width;

            if (labeledOutputImage == null)
            {
                this.labeledOutputImage = new cImage(labeledInputimage.Width, labeledInputimage.Height, labeledInputimage.Depth, 1);
            }
            else
            {
                this.labeledOutputImage = labeledOutputImage;
            }
            this.labeledOutput = this.labeledOutputImage.SingleChannelImage[LabelBand].Data;
            this.labeledInputimage = labeledInputimage;
            this.labeledInput = labeledInputimage.SingleChannelImage[BandLabelInput].Data;
            this.image = image;

            FindConnectedComponents();
        }


        /// <summary>
        /// Grab a set of already computed ConnectedComponent into a ConnectedComponentSet
        /// </summary>
        /// <param name="list">The list of ConnectedComponent</param>
        public ConnectedComponentSet(List<ConnectedVoxels> list)
        {
            /// TO DO: CHECK THE CONSISTENCY OF THE LABELOUTPUT IMAGE AND THE IMAGE WITH THE ACUAL 
            /// VALUES OF THE CONNECTED COMPONENTS

            if (list.Count == 0)
                return;

            this.image = list[0].Image;
            this.labeledOutputImage = list[0].LabelImage;
            this.connectivity = list[0].Connectivity;
            this.width = labeledOutputImage.Width;
            this.height = labeledOutputImage.Height;
            this.scanSliceSize = labeledOutputImage.SliceSize;
            this.twoTimesWidth = 2 * this.width;

            int i = 0;
            cc = new ConnectedVoxels[list.Count];
            foreach (ConnectedVoxels cci in list)
            {
                if (cci.LabelImage != labeledOutputImage)
                    throw new ArgumentException("Connected components were not defined from the same image");
                if (cci.Connectivity != connectivity)
                    throw new ArgumentException("Connected components were not defined with the same connectivity");
                cc[i++] = cci;
            }
        }


        public void Add(ConnectedVoxels cc)
        {

        }

        #endregion

        #region Object detection

        /// <summary> Looks for the connected components and extracts data
        /// </summary>
        private void FindConnectedComponents()
        {
            graph = new ObjectGraph();

            switch (connectivity)
            {
                case eConnectivity.TWOD_4: detect4(); break;
                case eConnectivity.THREED_6: detect6(); break;
                case eConnectivity.TWOD_8: detect8(); break;
                case eConnectivity.TWOD_24: detect24(); break;
                case eConnectivity.THREED_26: detect26(); break;
                default: throw new Exception("Connectivity must be 4, 6, 8, 24 or 26");
            }


            /* determine the final labels */
            int numCom = 0;
            int index = -1;

            Dictionary<int, ConnectedVoxels> hm = new Dictionary<int, ConnectedVoxels>();
            int key;
            for (int k = 0; k < depth; k++)
                for (int j = 0; j < height; j++)
                    for (int i = 0; i < width; i++)
                        if (labeledOutput[++index] != 0f)
                        {
                            key = graph.getAncestorLabel((int)labeledOutput[index]);

                            if (!hm.ContainsKey(key)) hm.Add(key, new ConnectedVoxels(++numCom,
                                                                                    labeledOutputImage,
                                                                                    image,
                                                                                    connectivity));

                            labeledOutput[index] = hm[key].Label;
                            hm[key].Add(index, i, j, k);
                        }

            //display(labeledOutput);
            if (numCom == 0) return;

            /* use the volume constraint */
            ConnectedVoxels[] foundObjectsBefore = new ConnectedVoxels[numCom];
            ArrayList foundObjectsAfter = new ArrayList();
            foreach (ConnectedVoxels fo in hm.Values)
                foundObjectsBefore[fo.Label - 1] = fo;
            int[] relb = new int[numCom]; // relabel array
            for (int i = 0; i < numCom; i++)
                relb[i] = i + 1;
            int reNumCom = numCom;
            for (int i = 0; i < numCom; i++)
                if (foundObjectsBefore[i].Volume < min || foundObjectsBefore[i].Volume > max)// || coarr[i].IsOnEdge)
                {
                    relb[i] = 0;
                    for (int j = i + 1; j < numCom; j++)
                    {
                        relb[j]--;
                        foundObjectsBefore[j].Label--;
                    }
                    reNumCom--;
                }
                else
                {
                    foundObjectsAfter.Add(foundObjectsBefore[i]);
                }

            // relabel with the constraint
            for (int i = 0; i < labeledOutput.Length; i++)
            {
                index = (int)labeledOutput[i];
                if (index > 0 && index != relb[index - 1])
                    labeledOutput[i] = relb[index - 1];
            }


            if (reNumCom == 0) return;

            this.cc = (ConnectedVoxels[])foundObjectsAfter.ToArray(typeof(ConnectedVoxels));
        }

        /// <summary> Detect the connected component in 4-connectivity space (2D)
        /// </summary>
        private void detect4()
        {
            int nbObjects = 0;
            float[] v4 = new float[2];
            float[] v4_in = new float[2];
            int v4i;

            int i = 0;
            //  line 1 -> .x.
            //  line 2 -> xo.
            //  line 3 -> ...

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    v4i = 0;
                    if (labeledInput[i] != 0f)
                    {
                        // there is an object here
                        v4[0] = (0 <= y - 1) ? labeledOutput[i - width] : 0f;
                        v4[1] = (0 <= x - 1) ? labeledOutput[i - 1] : 0f;

                        v4_in[0] = (0 <= y - 1) ? labeledInput[i - width] : 0f;
                        v4_in[1] = (0 <= x - 1) ? labeledInput[i - 1] : 0f;

                        v4i = conclude(i, v4, v4_in, ref nbObjects);
                    }
                    labeledOutput[i] = v4i;
                    i++;
                }
            }
        }


        /// <summary> Detect the connected component in 24-connectivity space (2D)
        /// </summary>
        private void detect24()
        {
            int nbObjects = 0;
            float[] v12 = new float[12];
            float[] v12_in = new float[12];
            int v12i;

            int i = 0;

            //  line 1 -> xxx
            //  line 2 -> xo.
            //  line 3 -> ...

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    v12i = 0;
                    if (labeledInput[i] != 0f)
                    {
                        // there is an object here
                        v12[0] = (0 <= y - 2 && 0 <= x - 2) ? labeledOutput[i - twoTimesWidth - 2] : 0f;
                        v12[1] = (0 <= y - 2 && 0 <= x - 1) ? labeledOutput[i - twoTimesWidth - 1] : 0f;
                        v12[2] = (0 <= y - 2) ? labeledOutput[i - 2 * width] : 0f;
                        v12[3] = (0 <= y - 2 && 0 <= x + 1) ? labeledOutput[i - twoTimesWidth + 1] : 0f;
                        v12[4] = (0 <= y - 2 && 0 <= x - 2) ? labeledOutput[i - twoTimesWidth + 2] : 0f;
                        v12[5] = (0 <= y - 1 && 0 <= x - 2) ? labeledOutput[i - width - 2] : 0f;
                        v12[6] = (0 <= y - 1 && 0 <= x - 1) ? labeledOutput[i - width - 1] : 0f;
                        v12[7] = (0 <= y - 1) ? labeledOutput[i - width] : 0f;
                        v12[8] = (0 <= y - 1 && 0 <= x + 1) ? labeledOutput[i - width + 1] : 0f;
                        v12[9] = (0 <= y - 1 && 0 <= x - 2) ? labeledOutput[i - width + 2] : 0f;
                        v12[10] = (0 <= x - 2) ? labeledOutput[i - 2] : 0f;
                        v12[11] = (0 <= x - 1) ? labeledOutput[i - 1] : 0f;

                        v12_in[0] = (0 <= y - 2 && 0 <= x - 2) ? labeledInput[i - twoTimesWidth - 2] : 0f;
                        v12_in[1] = (0 <= y - 2 && 0 <= x - 1) ? labeledInput[i - twoTimesWidth - 1] : 0f;
                        v12_in[2] = (0 <= y - 2) ? labeledInput[i - 2 * width] : 0f;
                        v12_in[3] = (0 <= y - 2 && 0 <= x + 1) ? labeledInput[i - twoTimesWidth + 1] : 0f;
                        v12_in[4] = (0 <= y - 2 && 0 <= x - 2) ? labeledInput[i - twoTimesWidth + 2] : 0f;
                        v12_in[5] = (0 <= y - 1 && 0 <= x - 2) ? labeledInput[i - width - 2] : 0f;
                        v12_in[6] = (0 <= y - 1 && 0 <= x - 1) ? labeledInput[i - width - 1] : 0f;
                        v12_in[7] = (0 <= y - 1) ? labeledInput[i - width] : 0f;
                        v12_in[8] = (0 <= y - 1 && 0 <= x + 1) ? labeledInput[i - width + 1] : 0f;
                        v12_in[9] = (0 <= y - 1 && 0 <= x - 2) ? labeledInput[i - width + 2] : 0f;
                        v12_in[10] = (0 <= x - 2) ? labeledInput[i - 2] : 0f;
                        v12_in[11] = (0 <= x - 1) ? labeledInput[i - 1] : 0f;

                        v12i = conclude(i, v12, v12_in, ref nbObjects);

                    }
                    labeledOutput[i] = v12i;
                    i++;
                }
            }
            return;
        }


        /// <summary> Detect the connected component in 8-connectivity space (2D)
        /// </summary>
        private void detect8()
        {
            int nbObjects = 0;
            float[] v8 = new float[4];
            float[] v8_in = new float[4];
            int v8i;

            int i = 0;

            //  line 1 -> xxx
            //  line 2 -> xo.
            //  line 3 -> ...

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    v8i = 0;
                    if (labeledInput[i] != 0f)
                    {
                        // there is an object here
                        v8[0] = (0 <= y - 1 && 0 <= x - 1) ? labeledOutput[i - width - 1] : 0f;
                        v8[1] = (0 <= y - 1) ? labeledOutput[i - width] : 0f;
                        v8[2] = (0 <= y - 1 && x + 1 < width) ? labeledOutput[i - width + 1] : 0f;
                        v8[3] = (0 <= x - 1) ? labeledOutput[i - 1] : 0f;

                        v8_in[0] = (0 <= y - 1 && 0 <= x - 1) ? labeledInput[i - width - 1] : 0f;
                        v8_in[1] = (0 <= y - 1) ? labeledInput[i - width] : 0f;
                        v8_in[2] = (0 <= y - 1 && x + 1 < width) ? labeledInput[i - width + 1] : 0f;
                        v8_in[3] = (0 <= x - 1) ? labeledInput[i - 1] : 0f;

                        v8i = conclude(i, v8, v8_in, ref nbObjects);

                    }
                    labeledOutput[i] = v8i;
                    i++;
                }
            }
        }



        /// <summary> Detect the connected component in 6-connectivity space (3D)
        /// </summary>
        private void detect6()
        {
            int nbObjects = 0;
            float[] v6 = new float[3];
            float[] v6_in = new float[3];
            int v6i;

            int i = 0;
            //  line 1 -> ... //  line 4 -> .x. //  line 7 -> ...
            //  line 2 -> .x. //  line 5 -> xo. //  line 8 -> ...
            //  line 3 -> ... //  line 6 -> ... //  line 9 -> ...

            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        v6i = 0;
                        if (labeledInput[i] != 0f)
                        {
                            // there is an object here
                            v6[0] = (0 <= x - 1) ? labeledOutput[i - 1] : 0f;
                            v6[1] = (0 <= y - 1) ? labeledOutput[i - width] : 0f;
                            v6[2] = (0 <= z - 1) ? labeledOutput[i - scanSliceSize] : 0f;

                            v6_in[0] = (0 <= x - 1) ? labeledInput[i - 1] : 0f;
                            v6_in[1] = (0 <= y - 1) ? labeledInput[i - width] : 0f;
                            v6_in[2] = (0 <= z - 1) ? labeledInput[i - scanSliceSize] : 0f;

                            v6i = conclude(i, v6, v6_in, ref nbObjects);
                        }
                        labeledOutput[i] = v6i;
                        i++;
                    }
                }
            }
        }

        /// <summary> Detect the connected component in 26-connectivity space (3D)
        /// </summary>
        private void detect26()
        {
            int nbObjects = 0;
            float[] v26 = new float[13];
            float[] v26_in = new float[13];
            int v26i;

            int i = 0;
            //  line 1 -> xxx //  line 4 -> xxx //  line 7 -> ...
            //  line 2 -> xxx //  line 5 -> xo. //  line 8 -> ...
            //  line 3 -> xxx //  line 6 -> ... //  line 9 -> ...

            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        v26i = 0;
                        if (labeledInput[i] != 0f)
                        {

                            // there is an object here
                            v26[0] = (0 <= z - 1 && 0 <= y - 1 && 0 <= x - 1) ? labeledOutput[i - scanSliceSize - width - 1] : 0f;
                            v26[1] = (0 <= z - 1 && 0 <= y - 1) ? labeledOutput[i - scanSliceSize - width] : 0f;
                            v26[2] = (0 <= z - 1 && 0 <= y - 1 && x < width) ? labeledOutput[i - scanSliceSize - width + 1] : 0f;
                            v26[3] = (0 <= z - 1 && 0 <= x - 1) ? labeledOutput[i - scanSliceSize - 1] : 0f;
                            v26[4] = (0 <= z - 1) ? labeledOutput[i - scanSliceSize] : 0f;
                            v26[5] = (0 <= z - 1 && x + 1 < width) ? labeledOutput[i - scanSliceSize + 1] : 0f;
                            v26[6] = (0 <= z - 1 && y + 1 < height && x - 1 <= 0) ? labeledOutput[i - scanSliceSize + width - 1] : 0f;
                            v26[7] = (0 <= z - 1 && y + 1 < height) ? labeledOutput[i - scanSliceSize + width] : 0f;
                            v26[8] = (0 <= z - 1 && y + 1 < height && x + 1 < width) ? labeledOutput[i - scanSliceSize + width + 1] : 0f;
                            v26[9] = (0 <= y - 1 && 0 <= x - 1) ? labeledOutput[i - width - 1] : 0f;
                            v26[10] = (0 <= y - 1) ? labeledOutput[i - width] : 0f;
                            v26[11] = (0 <= y - 1 && x + 1 < width) ? labeledOutput[i - width + 1] : 0f;
                            v26[12] = (0 <= x - 1) ? labeledOutput[i - 1] : 0f;

                            v26_in[0] = (0 <= z - 1 && 0 <= y - 1 && 0 <= x - 1) ? labeledInput[i - scanSliceSize - width - 1] : 0f;
                            v26_in[1] = (0 <= z - 1 && 0 <= y - 1) ? labeledInput[i - scanSliceSize - width] : 0f;
                            v26_in[2] = (0 <= z - 1 && 0 <= y - 1 && x < width) ? labeledInput[i - scanSliceSize - width + 1] : 0f;
                            v26_in[3] = (0 <= z - 1 && 0 <= x - 1) ? labeledInput[i - scanSliceSize - 1] : 0f;
                            v26_in[4] = (0 <= z - 1) ? labeledInput[i - scanSliceSize] : 0f;
                            v26_in[5] = (0 <= z - 1 && x + 1 < width) ? labeledInput[i - scanSliceSize + 1] : 0f;
                            v26_in[6] = (0 <= z - 1 && y + 1 < height && x - 1 <= 0) ? labeledInput[i - scanSliceSize + width - 1] : 0f;
                            v26_in[7] = (0 <= z - 1 && y + 1 < height) ? labeledInput[i - scanSliceSize + width] : 0f;
                            v26_in[8] = (0 <= z - 1 && y + 1 < height && x + 1 < width) ? labeledInput[i - scanSliceSize + width + 1] : 0f;
                            v26_in[9] = (0 <= y - 1 && 0 <= x - 1) ? labeledInput[i - width - 1] : 0f;
                            v26_in[10] = (0 <= y - 1) ? labeledInput[i - width] : 0f;
                            v26_in[11] = (0 <= y - 1 && x + 1 < width) ? labeledInput[i - width + 1] : 0f;
                            v26_in[12] = (0 <= x - 1) ? labeledInput[i - 1] : 0f;

                            v26i = conclude(i, v26, v26_in, ref nbObjects);
                        }
                        labeledOutput[i] = v26i;
                        i++;
                    }
                }
            }
        }

        private int conclude(int i, float[] v, float[] v_in, ref int nbObjects)
        {
            int vi = 0;

            for (int j = 0; j < v_in.Length; j++)
                if (v[j] > 0 && v_in[j] == labeledInput[i])
                {
                    vi = (int)v[j];
                    break;
                }

            if (vi == 0) // create a new label
                graph.add(vi = ++nbObjects);
            else          // associate with the lowest neighbor label
                for (int j = 0; j < v.Length; j++)
                    if (v_in[j] == labeledInput[i])
                        graph.Merge((int)v[j], vi);
            return vi;
        }

        private class ObjectGraph
        {
            public void Dispose()
            {
                map.Clear();
                map = null;
            }

            protected Dictionary<int, Node> map = new Dictionary<int, Node>();

            /// <summary> Adds a new label (new node) in the object graph
            /// </summary>
            /// <param name="label">The label to add</param>
            public void add(int label)
            {
                if (!exists(label)) map.Add(label, new Node(label, null, 0));
            }

            /// <summary> Merges two labels (i.e. two graph nodes)
            /// </summary>
            /// <param name="label1">the first label to merge</param>
            /// <param name="label2">the second label to merge</param>
            public void Merge(int label1, int label2)
            {
                if (exists(label1) && exists(label2))
                {
                    Node item1 = findAncestor(map[label1]);
                    Node item2 = findAncestor(map[label2]);
                    if (!item1.Equals(item2))
                    {
                        if (item1.NbChildren < item2.NbChildren)
                            item1.Father = item2;
                        else
                            item2.Father = item1;
                    }
                }
            }

            /// <summary>Gets the root label of a given label
            /// </summary>
            /// <param name="label">the input object label</param>
            /// <returns>The label of the oldest father of the input</returns>
            public int getAncestorLabel(int label)
            {
                return (findAncestor(map[label])).Label;
            }

            /// <summary> Checks if a label already exists in the dictionnary
            /// </summary>
            /// <param name="label">the label to look for</param>
            /// <returns>true if th label exists in the dictionnary</returns>
            public bool exists(int label)
            {
                return map.ContainsKey(label);
            }

            /// <summary> Node of the object graph representing a detected object and its hierarchy
            /// </summary>
            protected internal class Node
            {
                private int label;
                private Node father;
                private int nbChildren; // number of sons

                internal Node(int theLabel, Node theFather, int theSize)
                {
                    label = theLabel;
                    Father = theFather;
                    NbChildren = theSize;
                }

                internal int Label { get { return label; } }

                internal Node Father
                {
                    get { return father; }
                    set
                    {
                        if (father != null) father.NbChildren = father.nbChildren - nbChildren - 1; // inform the pre-father
                        father = value;
                        if (father != null) father.NbChildren = father.nbChildren + nbChildren + 1; // inform the post-father
                    }
                }

                internal int NbChildren
                {
                    get { return nbChildren; }
                    set
                    {
                        if (father != null) father.NbChildren = father.nbChildren - nbChildren - 1;
                        nbChildren = value;
                        if (father != null) father.NbChildren = father.nbChildren + nbChildren + 1; // inform the father
                    }
                }
            }

            private static Node findAncestor(Node item)
            {
                Node ancestor = item;
                while (ancestor.Father != null) ancestor = ancestor.Father;
                return ancestor;
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cc.GetEnumerator();
        }
        #endregion

        void IDisposable.Dispose()
        {
        }


    }


    /// <summary> A subset of connected voxels in an image.
    /// </summary>
    public class ConnectedVoxels : VoxelList
    {
        #region Private members
        private float[] radii;
        #endregion

        #region External accessors

        /// <summary> The radii of the object (distance between the mass center and every surface point)
        /// </summary>
        public float[] Radii
        {
            get
            {
                if (radii != null) return radii;

                if (SurfacePoints.Count == 0)
                    FindSurface();

                float x = X, y = Y, z = Z;
                radii = new float[SurfacePoints.Count];
                for (int i = 0; i < radii.Length; i++) radii[i] = (float)Math.Sqrt((SurfacePoints[i][0] - x) * (SurfacePoints[i][0] - x) +
                                                                    (SurfacePoints[i][1] - y) * (SurfacePoints[i][1] - y) +
                                                                    (SurfacePoints[i][2] - z) * (SurfacePoints[i][2] - z));
                return radii;
            }
        }

        public List<cPoint3D> GetExtremaPoints()
        {
            List<cPoint3D> ToBeReturned = new List<cPoint3D>();

            ToBeReturned.Add(new cPoint3D(this.MinX, this.MinY, this.MinZ));
            ToBeReturned.Add(new cPoint3D(this.MinX + this.Width, this.MinY + this.Height, this.MinZ + this.Depth));

            return ToBeReturned;
        }


        #endregion

        #region Constructor

        internal ConnectedVoxels(int label, cImage labeledImage, cImage image, eConnectivity connectivity)
            :
            base(label, labeledImage, image, connectivity)
        {

        }

        #endregion
    }

    /// <summary> A subset of connected voxels in an image.
    /// </summary>
    public class Component : VoxelList
    {
        #region Constructor

        public Component(int label, cImage labeledImage, cImage image, eConnectivity connectivity)
            :
            base(label, labeledImage, image, connectivity)
        {

        }

        #endregion

        #region Public methods

        public void Add(int imageIndex, int x, int y, int z)
        {
            base.Add(imageIndex, x, y, z);
        }

        #endregion
    }


    public class VoxelList : IDisposable
    {
        #region Private members

        private cImage image, labeledImage;
        private float[] labeledImageData;
        private int imageWidth, imageHeight, imageDepth, scanSliceSize;
        private eConnectivity connectivity;

        private int sumx;
        private int sumy;
        private int sumz;

        private int minx = Int32.MaxValue;
        private int miny = Int32.MaxValue;
        private int minz = Int32.MaxValue;
        private int maxx = Int32.MinValue;
        private int maxy = Int32.MinValue;
        private int maxz = Int32.MinValue;

        private int numBands;
        private List<int[]> points;
        private List<float[]> values;
        private List<int[]> surfacePoints;
        private List<int> indices;
        private int volume;
        private float[] intensity;
        private float[] surfaceIntensity;
        private float[] maxIntensity;
        private float[] minIntensity;
        private int label;
        private bool isOn2DEdge;

        private Hashtable properties = new Hashtable();

        #endregion

        #region External accessors

        /// <summary> The connectivity used to built this ConnectedComponent
        /// </summary>
        public eConnectivity Connectivity { get { return connectivity; } }

        /// <summary> The number of band
        /// </summary>
        public int NumBands { get { return numBands; } }

        /// <summary> The X coordinate of the object centroid
        /// </summary>
        public float X { get { return (float)sumx / volume; } }

        /// <summary> The Y coordinate of the object centroid
        /// </summary>
        public float Y { get { return (float)sumy / volume; } }

        /// <summary> The Z coordinate of the object centroid
        /// </summary>
        public float Z { get { return (float)sumz / volume; } }

        /// <summary> The X coordinate of the bounding box of this object
        /// </summary>
        public int MinX { get { return minx; } }

        /// <summary> The Y coordinate of the bounding box of this object
        /// </summary>
        public int MinY { get { return miny; } }

        /// <summary> The Z coordinate of the bounding box of this object
        /// </summary>
        public int MinZ { get { return minz; } }

        public int Width { get { return maxx - minx + 1; } }

        public int Height { get { return maxy - miny + 1; } }

        public int Depth { get { return maxz - minz + 1; } }

        /// <summary> The label of this object
        /// </summary>
        public int Label { get { return label; } internal set { label = value; } }

        /// <summary> The volume of this object
        /// </summary>
        public int Volume { get { return volume; } }

        /// <summary> The average intensities of this object
        /// </summary>
        public float[] Intensity { get { return intensity; } }

        /// <summary> The min intensity of this object
        /// </summary>
        public float[] MinIntensity { get { return minIntensity; } }

        /// <summary> The max intensity of this object
        /// </summary>
        public float[] MaxIntensity { get { return maxIntensity; } }

        /// <summary> The list of 3D points of this object (list of int[]{x,y,z})
        /// </summary>
        public List<int[]> Points { get { return points; } }

        /// <summary> The list of index of the voxels of this object in the cImage.Data arrays (list of int)
        /// </summary>
        public List<int> Indices { get { return indices; } }

        /// <summary> The list of values of this object (list of float[]{band0, band1, ...})
        /// </summary>
        public List<float[]> Values { get { return values; } }

        /// <summary> The list of 3D points of the surface of this object (list of int[]{x,y,z})
        /// </summary>
        public List<int[]> SurfacePoints
        {
            get
            {
                if (surfacePoints.Count == 0)
                    FindSurface();
                return surfacePoints;
            }
        }

        /// <summary>  The average intensities of this object
        /// </summary>
        public float[] SurfaceIntensity
        {
            get
            {
                if (surfacePoints.Count == 0)
                    FindSurface();
                return surfaceIntensity;
            }
        }

        /// <summary> Indicate whenever this object is on the edge of the picture
        /// </summary>
        public bool IsOn2DEdge
        {
            get
            {
                if (surfacePoints.Count == 0)
                    FindSurface();

                return isOn2DEdge;
            }
        }

        /// <summary> A free of use property hashtable
        /// </summary>
        public Hashtable Properties
        {
            get
            {
                return properties;
            }
        }

        #endregion

        #region Constructor

        protected VoxelList(int label, cImage labeledImage, cImage image, eConnectivity connectivity)
        {
            this.label = label;
            surfacePoints = new List<int[]>();
            points = new List<int[]>();
            indices = new List<int>();
            values = new List<float[]>();
            this.labeledImage = labeledImage;
            this.labeledImageData = labeledImage.SingleChannelImage[0].Data;
            this.image = image;
            if (image != null)
            {
                int NumChannels = image.GetNumChannels();
                this.numBands = NumChannels;
                this.intensity = new float[NumChannels];
                this.surfaceIntensity = new float[NumChannels];
                this.maxIntensity = new float[NumChannels];
                this.minIntensity = new float[NumChannels];
                for (int b = 0; b < NumChannels; b++)
                {
                    maxIntensity[b] = float.MinValue;
                    minIntensity[b] = float.MaxValue;
                }
            }
            this.imageWidth = labeledImage.Width;
            this.imageHeight = labeledImage.Height;
            this.imageDepth = labeledImage.Depth;
            this.scanSliceSize = labeledImage.SliceSize;
            this.connectivity = connectivity;
        }

        #endregion

        #region Public methods

        public bool IsContains(int x, int y, int z)
        {
            return minx <= x && x <= maxx && miny <= y && y <= maxy && minz <= z && z <= maxz && _BaseContains(x, y, z);
        }

        private bool _BaseContains(int x, int y, int z)
        {
            foreach (int[] p in Points)
            {
                if (p[0] == x && p[1] == y && p[2] == z)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Return the index (in SurfacePoints) of the closest point from the provided argument (x,y,z) and -1 if (x,y,z) is inside the set
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="z">z</param>
        /// <returns></returns>
        public int EuclidianDistanceIndex(int x, int y, int z)
        {
            if (this.IsContains(x, y, z))
                return -1;

            double d, min = double.MaxValue;
            int minIndex = -1;
            for (int i = 0; i < SurfacePoints.Count; i++)
            {
                int[] p = SurfacePoints[i];
                d = _SquaredEuclidianDistance(x, y, z, p[0], p[1], p[2]);
                if (d < min)
                {
                    min = d;
                    minIndex = i;
                }
            }
            return minIndex;
        }

        private double _SquaredEuclidianDistance(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2));
        }

        #endregion

        #region Internal methods and accessors

        /// <summary>
        /// Retrieve the label image internally (restricted to this namespace: should not be change to snsure safety)
        /// </summary>
        internal cImage LabelImage
        {
            get { return labeledImage; }
        }

        /// <summary>
        /// Retrieve the original image internally (restricted to this namespace: should not be change to snsure safety)
        /// </summary>
        internal cImage Image
        {
            get { return image; }
        }

        /// <summary>
        /// Internally add a voxel to the set. This method must remain internal in order to maintain the consistency in the
        /// case of a ConnectedComponent.
        /// </summary>
        /// <param name="imageIndex"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        internal void Add(int imageIndex, int x, int y, int z)
        {
            if (x < minx) minx = x;
            if (y < miny) miny = y;
            if (z < minz) minz = z;

            if (x > maxx) maxx = x;
            if (y > maxy) maxy = y;
            if (z > maxz) maxz = z;

            if (image != null)
            {
                float[] v = new float[image.GetNumChannels()];
                for (int b = 0; b < image.GetNumChannels(); b++)
                {
                    v[b] = image.SingleChannelImage[b].Data[imageIndex];
                    intensity[b] += v[b];
                    if (v[b] < minIntensity[b]) minIntensity[b] = v[b];
                    if (v[b] > maxIntensity[b]) maxIntensity[b] = v[b];
                }
                values.Add(v);
            }

            this.sumx += x;
            this.sumy += y;
            this.sumz += z;
            volume++;

            indices.Add(imageIndex);
            points.Add(new int[] { x, y, z });

        }

        #endregion

        #region Private methods, Surface points detection

        protected void FindSurface()
        {
            int[] v;
            int index;
            for (int i = 0; i < points.Count; i++)
            {
                v = points[i];
                index = indices[i];
                if (isSurface(index, v[0], v[1], v[2]))
                {
                    if (image != null)
                    {
                        for (int b = 0; b < image.GetNumChannels(); b++)
                            surfaceIntensity[b] += image.SingleChannelImage[b].Data[index];
                    }

                    surfacePoints.Add(new int[] { v[0], v[1], v[2] });

                    if (!isOn2DEdge && isOn2DEdgeN(v[0], v[1]))
                        isOn2DEdge = true;
                }
            }
        }

        private bool isOn2DEdgeN(int x, int y)
        {
            return (x <= 0 || y <= 0 || x >= imageWidth - 1 || y >= imageHeight - 1);
        }

        private bool isSurface(int index, int x, int y, int z)
        {
            if (labeledImageData[index] != label) return false;

            switch (connectivity)
            {
                case eConnectivity.TWOD_4: return isSurface4(index, x, y);
                case eConnectivity.TWOD_8: return isSurface8(index, x, y);
                case eConnectivity.THREED_6: return isSurface6(index, x, y, z);
                case eConnectivity.THREED_26: return isSurface26(index, x, y, z);
                default: return false;
            }
        }

        private bool isSurface4(int i, int x, int y)
        {
            return ((0 <= y - 1 && labeledImageData[i - imageWidth] != label) ||
                    (0 <= x - 1 && labeledImageData[i - 1] != label) ||
                    (x + 1 < imageWidth && labeledImageData[i + 1] != label) ||
                    (y + 1 < imageHeight && labeledImageData[i + imageWidth] != label));
        }

        private bool isSurface6(int i, int x, int y, int z)
        {
            return ((0 <= z - 1 && labeledImageData[i - scanSliceSize] != label) ||
                    (0 <= y - 1 && labeledImageData[i - imageWidth] != label) ||
                    (0 <= x - 1 && labeledImageData[i - 1] != label) ||
                    (x + 1 < imageWidth && labeledImageData[i + 1] != label) ||
                    (y + 1 < imageHeight && labeledImageData[i + imageWidth] != label) ||
                    (z + 1 < imageDepth && labeledImageData[i + scanSliceSize] != label));
        }

        private bool isSurface8(int i, int x, int y)
        {
            return ((0 <= y - 1 && 0 <= x - 1 && labeledImageData[i - imageWidth - 1] != label) ||
                    (0 <= y - 1 && labeledImageData[i - imageWidth] != label) ||
                    (0 <= y - 1 && x + 1 < imageWidth && labeledImageData[i - imageWidth + 1] != label) ||
                    (0 <= x - 1 && labeledImageData[i - 1] != label) ||
                    (x + 1 < imageWidth && labeledImageData[i + 1] != label) ||
                    (y + 1 < imageHeight && 0 <= x - 1 && labeledImageData[i + imageWidth - 1] != label) ||
                    (y + 1 < imageHeight && labeledImageData[i + imageWidth] != label) ||
                    (y + 1 < imageHeight && x + 1 < imageWidth && labeledImageData[i + imageWidth + 1] != label));

        }

        private bool isSurface26(int i, int x, int y, int z)
        {
            return (
                   (0 <= z - 1 && 0 <= y - 1 && 0 <= x - 1 && labeledImageData[i - scanSliceSize - imageWidth - 1] != label) ||
                   (0 <= z - 1 && 0 <= y - 1 && labeledImageData[i - scanSliceSize - imageWidth] != label) ||
                   (0 <= z - 1 && 0 <= y - 1 && x + 1 < imageWidth && labeledImageData[i - scanSliceSize - imageWidth + 1] != label) ||
                   (0 <= z - 1 && 0 <= x - 1 && labeledImageData[i - scanSliceSize - 1] != label) ||
                   (0 <= z - 1 && labeledImageData[i - scanSliceSize] != label) ||
                   (0 <= z - 1 && x + 1 < imageWidth && labeledImageData[i - scanSliceSize + 1] != label) ||
                   (0 <= z - 1 && y + 1 < imageHeight && 0 <= x - 1 && labeledImageData[i - scanSliceSize + imageWidth - 1] != label) ||
                   (0 <= z - 1 && y + 1 < imageHeight && labeledImageData[i - scanSliceSize + imageWidth] != label) ||
                   (0 <= z - 1 && y + 1 < imageHeight && x + 1 < imageWidth && labeledImageData[i - scanSliceSize + imageWidth + 1] != label) ||

                   (0 <= y - 1 && 0 <= x - 1 && labeledImageData[i - imageWidth - 1] != label) ||
                   (0 <= y - 1 && labeledImageData[i - imageWidth] != label) ||
                   (0 <= y - 1 && x + 1 < imageWidth && labeledImageData[i - imageWidth + 1] != label) ||
                   (0 <= x - 1 && labeledImageData[i - 1] != label) ||
                   (x + 1 < imageWidth && labeledImageData[i + 1] != label) ||
                   (y + 1 < imageHeight && 0 <= x - 1 && labeledImageData[i + imageWidth - 1] != label) ||
                   (y + 1 < imageHeight && labeledImageData[i + imageWidth] != label) ||
                   (y + 1 < imageHeight && x + 1 < imageWidth && labeledImageData[i + imageWidth + 1] != label) ||

                   (z + 1 < imageDepth && 0 <= y - 1 && 0 <= x - 1 && labeledImageData[i + scanSliceSize - imageWidth - 1] != label) ||
                   (z + 1 < imageDepth && 0 <= y - 1 && labeledImageData[i + scanSliceSize - imageWidth] != label) ||
                   (z + 1 < imageDepth && 0 <= y - 1 && x + 1 < imageWidth && labeledImageData[i + scanSliceSize - imageWidth + 1] != label) ||
                   (z + 1 < imageDepth && 0 <= x - 1 && labeledImageData[i + scanSliceSize - 1] != label) ||
                   (z + 1 < imageDepth && labeledImageData[i + scanSliceSize] != label) ||
                   (z + 1 < imageDepth && x + 1 < imageWidth && labeledImageData[i + scanSliceSize + 1] != label) ||
                   (z + 1 < imageDepth && y + 1 < imageHeight && 0 <= x - 1 && labeledImageData[i + scanSliceSize + imageWidth - 1] != label) ||
                   (z + 1 < imageDepth && y + 1 < imageHeight && labeledImageData[i + scanSliceSize + imageWidth] != label) ||
                   (z + 1 < imageDepth && y + 1 < imageHeight && x + 1 < imageWidth && labeledImageData[i + scanSliceSize + imageWidth + 1] != label));
        }

        #endregion

        void IDisposable.Dispose()
        {
            image = null;
            labeledImageData = null;
        }
    }

}
