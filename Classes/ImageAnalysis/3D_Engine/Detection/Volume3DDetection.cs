using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HCSAnalyzer.Classes._3D;
using ImageAnalysis;
using System.Drawing;
using HCSAnalyzer.Classes.Base_Classes.DataStructures;

namespace HCSAnalyzer.Classes.ImageAnalysis._3D_Engine.Detection
{




    public class cMeshSmoother
    {
        public int Type = 0;
        public int NumberOfIterations = 1;

        //public vtkSmoothPolyDataFilter smoother = new vtkSmoothPolyDataFilter();
        // public vtkWindowedSincPolyDataFilter smoother = new vtkWindowedSincPolyDataFilter();

        public cMeshSmoother(int NumIter)
        {
            this.NumberOfIterations = NumIter;
        }
    }

    public abstract class c3DDetection
    {
        public int Channel;
        public cImage CurrentImage;
        protected List<cInteractive3DObject> Containers = null;
        protected float IntensityAttenuation = -1;
        protected int ContainerMode = 0;
        protected cPoint3D Shift = new cPoint3D(0, 0, 0);
        protected int MedianKernel = -1;

        public cPoint3D Resolution;
        //public double XRes;
        //public double YRes;
        //public double ZRes;

        public void SetContainers(List<cInteractive3DObject> Containers)
        {
            this.Containers = Containers;
        }

        /// <summary>
        /// Set the container mode
        /// </summary>
        /// <param name="Mode">0: inside; 1: outside</param>
        public void SetContainersMode(int Mode)
        {
            this.ContainerMode = Mode;
        }

        public void SetShift(cPoint3D Shift)
        {
            this.Shift.X = Shift.X;
            this.Shift.Y = Shift.Y;
            this.Shift.Z = Shift.Z;
        }

        public void SetMedian(int KernelSize)
        {
            this.MedianKernel = KernelSize;
        }


        /// <summary>
        /// Warning : this function doesn't work properly for the borders
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputBand"></param>
        /// <param name="output"></param>
        /// <param name="outputBand"></param>
        /// <param name="radius"></param>
        protected void Median3D(cImage input, int inputBand, cImage output, int outputBand, int radius)
        {
           // MathTools math = new MathTools();

            //float[] inp = input.SingleChannelImage[inputBand].Data, outp = output.SingleChannelImage[outputBand].Data;

            //float[] neighborvalues = new float[((radius << 1) + 1) * ((radius << 1) + 1) * ((radius << 1) + 1)];
            //int i, j, k, ki, kj, kk, kindex, index = 0;

            //for (k = 0; k < input.Depth; k++)
            //    for (j = 0; j < input.Height; j++)
            //        for (i = 0; i < input.Width; i++, index++)
            //        {
            //            kindex = 0;
            //            for (kk = -radius; kk <= radius; kk++)
            //                for (kj = -radius; kj <= radius; kj++)
            //                    for (ki = -radius; ki <= radius; ki++, kindex++)
            //                        neighborvalues[kindex] = (i + ki < 0 || i + ki > input.Width - 1 ||
            //                                                  j + kj < 0 || j + kj > input.Height - 1 ||
            //                                                  k + kk < 0 || k + kk > input.Depth - 1) ?
            //                                                  0 : inp[index + ki + kj * input.Width + kk * input.SliceSize];

            //            outp[index] = math.Median(neighborvalues, false);
            //        }
        }

    }




    public class cSpotDetection : c3DDetection
    {

        cImage OriginalcImage = null;


        /// <summary>
        /// Spot Detector object
        /// </summary>
        /// <param name="SeqToProcess">Sequence to process</param>
        /// <param name="Channel">Image Channel</param>
        public cSpotDetection(cImage SeqToProcess, int Channel)
        {
            this.OriginalcImage = SeqToProcess;
            this.Channel = Channel;

            this.Resolution = new cPoint3D(SeqToProcess.Resolution);

        }


        /// <summary> Normalize a kernel to 1
        /// </summary>
        /// <param name="kernel">the kernel to normalize</param>
        private void Normalize(cImage kernel)
        {
            float C = 0;

            foreach (float f in kernel.SingleChannelImage[0].Data) C += f;

            if (C != 1) for (int i = 0; i < kernel.ImageSize; i++) kernel.SingleChannelImage[0].Data[i] /= C;
        }


        public cImage MakeGaussianKernel(float sigma)
        {
            float sigma2 = sigma * sigma;
            int k = (int)Math.Ceiling(sigma * 3.0f);

            double[][] data = { new double[(2 * k + 1)] };

            for (int i = -k; i <= k; i++)
                data[0][(i + k)] = 1f / (double)(Math.Sqrt(2 * Math.PI) * sigma * Math.Exp(((i * i) / sigma2) * 0.5f));

            cImage gaussian1d = new cImage(new cExtendedTable(data));
            Normalize(gaussian1d);
            return gaussian1d;
        }


        /// <summary>
        /// Local maximum of curvature spot detection
        /// </summary>
        /// <param name="KernelSize">Standard deviation of the Gaussian Kernel</param>
        /// <param name="Threshold">Intensity Threshold</param>
        /// <param name="Locality">Locality of the maxima</param>
        /// <param name="Radius">Radius of the spot (for display purpose only)</param>
        /// <returns></returns>
        public List<cInteractive3DObject> HessianDetection(float KernelSize, float Threshold, int Locality, double Radius)
        {

            if (this.MedianKernel != -1)
            {
                this.CurrentImage = new cImage(OriginalcImage.Width, OriginalcImage.Height, OriginalcImage.Depth, 1);
                this.Median3D(OriginalcImage, Channel, CurrentImage, 0, MedianKernel);
                this.Channel = 0;

            }
            else
            {
                this.CurrentImage = this.OriginalcImage;
            }


            List<cInteractive3DObject> ListSpots = new List<cInteractive3DObject>();

            // create the kernel
            cImage Kernel3DX = MakeGaussianKernel((float)(KernelSize / this.Resolution.X));
            cImage Kernel3DY = MakeGaussianKernel((float)(KernelSize / this.Resolution.Y));
            cImage Kernel3DZ = MakeGaussianKernel((float)(KernelSize / this.Resolution.Z));
            // convolve the image
            cImage ConvolvedImage = ConvolveFast3D(this.CurrentImage, Channel, Kernel3DX, Kernel3DY, Kernel3DZ, Threshold);

            cImage ImCurvature = ComputeCurvatureIntensity3d(ConvolvedImage, 0, 1.0f);

            //  new MathTools().Multiply(ref ImCurvature.Data[0], ImCurvature.Data[0], -1000.0f);

            //  cBiological3DVolume CurrentVolume;
            //if (Containers == null)
            //    CurrentVolume = new cBiological3DVolume(TmpSeq, Color.DodgerBlue, new cPoint3D(StartX * (float)CurrentSequence.XResolution, StartY * (float)CurrentSequence.YResolution, StartZ * (float)CurrentSequence.ZResolution));
            //else
            //    CurrentVolume = new cBiological3DVolume(TmpSeq, Color.DodgerBlue, new cPoint3D(StartX * (float)CurrentSequence.XResolution, StartY * (float)CurrentSequence.YResolution, StartZ * (float)CurrentSequence.ZResolution), this.Containers);

            //if (CurrentVolume.Detected)
            //    CurrentList3DObject.Add(CurrentVolume);



            int AddedPt = 0;
            float[] input = ImCurvature.SingleChannelImage[0].Data;
            float Currentvalue;
            int halfSize = (Locality - 1) >> 1;
            int CurrIdxPos;

            for (int k = 0; k < ImCurvature.Depth; k++)
                for (int j = 0; j < ImCurvature.Height; j++)
                    for (int i = 0; i < ImCurvature.Width; i++)
                    {
                        CurrIdxPos = i + j * ImCurvature.Width + k * ImCurvature.SliceSize;
                        Currentvalue = input[CurrIdxPos];

                        if (this.CurrentImage.SingleChannelImage[Channel].Data[CurrIdxPos] < Threshold) goto next;

                        for (int nz = -halfSize; nz <= halfSize; nz++)
                            for (int ny = -halfSize; ny <= halfSize; ny++)
                                for (int nx = -halfSize; nx <= halfSize; nx++)
                                {
                                    if (((i + nx) >= 0) && ((i + nx) < ImCurvature.Width) && ((j + ny) >= 0) && ((j + ny) < ImCurvature.Height) && ((k + nz) >= 0) && ((k + nz) < ImCurvature.Depth))
                                    {
                                        if ((nx == 0) && (ny == 0)) continue;
                                        if (Currentvalue > input[(i + nx) + (j + ny) * ImCurvature.Width + (k + nz) * ImCurvature.SliceSize]) goto next;
                                    }
                                }


                        cPoint3D Pt3d = new cPoint3D((float)(i * this.Resolution.X), (float)(j * this.Resolution.Y), (float)(k * this.Resolution.Z));
                        cBiologicalSpot CurrentLocalMax;

                        if (Containers == null)
                            CurrentLocalMax = new cBiologicalSpot(Color.AliceBlue, new cPoint3D(Pt3d.X + Shift.X, Pt3d.Y + Shift.Y, Pt3d.Z + Shift.Z), Currentvalue, Radius);
                        else
                            CurrentLocalMax = new cBiologicalSpot(Color.AliceBlue, new cPoint3D(Pt3d.X + Shift.X, Pt3d.Y + Shift.Y, Pt3d.Z + Shift.Z), Currentvalue, Radius, Containers, ContainerMode);

                        if (CurrentLocalMax.Detected)
                        {
                            ListSpots.Add(CurrentLocalMax);
                            AddedPt++;
                        }

                    next: ;
                    }
            return ListSpots;
        }

        cImage ConvolveFast3D(cImage inputImage, int inputBand, cImage kernel1DX, cImage kernel1DY, cImage kernel1DZ, float Threshold)
        {

            GC.Collect();

            cImage outputImage = new cImage(inputImage.Width, inputImage.Height, inputImage.Depth, 1);

            cImage bufferImage = new cImage(inputImage.Width, inputImage.Height, inputImage.Depth, 1);

            // To avoid array copyings everywhere, the data transfer is organized as input -X-> output -Y-> buffer -Z-> output

            int i, halfKernelLength, halfKernelLengthPixelStride, offset;
            float[] input = inputImage.SingleChannelImage[inputBand].Data;
            float[] output = outputImage.SingleChannelImage[0].Data;
            float[] buffer = bufferImage.SingleChannelImage[0].Data;
            float[] kernelX = kernel1DX.SingleChannelImage[0].Data;
            float[] kernelY = kernel1DY.SingleChannelImage[0].Data;
            float[] kernelZ = kernel1DZ.SingleChannelImage[0].Data;

            // convolve horizontal lines (X)
            halfKernelLength = kernel1DX.ImageSize >> 1;
            halfKernelLengthPixelStride = halfKernelLength;

            for (offset = 0; offset < inputImage.ImageSize; offset += inputImage.SliceSize)
                convolveLineMirror(input, output, kernelX, halfKernelLengthPixelStride, offset, 1, inputImage.Width);

            // convolve vertical lines (Y)
            halfKernelLength = kernel1DY.ImageSize >> 1;
            halfKernelLengthPixelStride = halfKernelLength * inputImage.SliceSize;

            for (offset = 0; offset < inputImage.ImageSize; offset += inputImage.SliceSize)
                for (i = 0; i < inputImage.SliceSize; i++)
                    convolveLineMirror(output, buffer, kernelY, halfKernelLengthPixelStride, offset + i, inputImage.Width, inputImage.Height, Threshold);

            // convolve depth lines (Z)
            halfKernelLength = kernel1DZ.ImageSize >> 1;
            halfKernelLengthPixelStride = halfKernelLength * inputImage.SliceSize;

            for (offset = 0; offset < inputImage.SliceSize; offset++)
                convolveLineMirror(buffer, output, kernelZ, halfKernelLengthPixelStride, offset, inputImage.SliceSize, inputImage.Depth, Threshold);


            return outputImage;

        }

        void convolveLineMirror(float[] input, float[] output, float[] kernel, int halfKernelLengthPixelStride, int offset, int pixelStride, int nbElements, float Threshold)
        {
            float element;
            int i, endLineOffset = offset + nbElements * pixelStride;

            for (int x = offset; x < endLineOffset; x += pixelStride)
            {
                if (input[x] < Threshold) continue;
                element = 0f;
                i = x - halfKernelLengthPixelStride;

                foreach (float k in kernel)
                {
                    if (i >= offset && i < endLineOffset) element += input[i] * k;

                    else if (i >= endLineOffset) element += input[(endLineOffset << 1) - pixelStride - i] * k;

                    else element += input[(offset << 1) - i] * k;

                    i += pixelStride;
                }

                output[x] = element;
            }
        }

        void convolveLineMirror(float[] input, float[] output, float[] kernel, int halfKernelLengthPixelStride, int offset, int pixelStride, int nbElements)
        {
            // (AD) This method used to call convolveElement, but the two methods have been merged to avoid too many function calls.

            float element;
            int i, endLineOffset = offset + nbElements * pixelStride;

            for (int x = offset; x < endLineOffset; x += pixelStride)
            {
                element = 0f;
                i = x - halfKernelLengthPixelStride;

                foreach (float k in kernel)
                {
                    if (i >= offset && i < endLineOffset) element += input[i] * k;

                    else if (i >= endLineOffset) element += input[(endLineOffset << 1) - pixelStride - i] * k;

                    else element += input[(offset << 1) - i] * k;

                    i += pixelStride;
                }

                output[x] = element;
            }
        }

        cImage ComputeCurvatureIntensity3d(cImage input, int inputBand, float weightz)
        {
            cImage output = new cImage(input.Width, input.Height, input.Depth, 1);
            int i, j, k, index = 0;
            float[] data = input.SingleChannelImage[inputBand].Data;

            float gx, gx1, gx2, gx4, gy, gy1, gy2, gy4, gz, gz1, gz2, gz4, gxx, gyy, gzz, gxy, gxz, gyz, gyx, gzx, gzy;

            for (k = 0; k < input.Depth; k++)
                for (j = 0; j < input.Height; j++)
                    for (i = 0; i < input.Width; i++, index++)
                    {
                        //case refractive condition:

                        // simple gradient
                        gx = (i == input.Width - 1 ? 0f : data[index + 1] - data[index]);
                        gx1 = (i == input.Width - 1 || i + 1 == input.Width - 1 ? 0f : data[index + 2] - data[index + 1]);
                        gx2 = (i == input.Width - 1 || j == input.Height - 1 ? 0f : data[index + input.SliceSize + 1] - data[index + input.SliceSize]);
                        gx4 = (i == input.Width - 1 || k == input.Depth - 1 ? 0f : data[index + input.SliceSize + 1] - data[index + input.SliceSize]);

                        gy = (j == input.Height - 1 ? 0f : data[index + input.SliceSize] - data[index]);
                        gy1 = (j == input.Height - 1 || i == input.Width - 1 ? 0f : data[index + 1 + input.SliceSize] - data[index + 1]);
                        gy2 = (j == input.Height - 1 || j + 1 == input.Height - 1 ? 0f : data[index + 2 * input.SliceSize] - data[index + input.SliceSize]);
                        gy4 = (j == input.Height - 1 || k == input.Depth - 1 ? 0f : data[index + input.SliceSize + input.SliceSize] - data[index + input.SliceSize]);

                        gz = (k == input.Depth - 1 ? 0f : (data[index + input.SliceSize] - data[index]) / weightz);
                        gz1 = (k == input.Depth - 1 || i == input.Width - 1 ? 0f : (data[index + input.SliceSize + 1] - data[index + 1]) / weightz);
                        gz2 = (k == input.Depth - 1 || j == input.Height - 1 ? 0f : (data[index + input.SliceSize + input.SliceSize] - data[index + input.SliceSize]) / weightz);
                        gz4 = (k == input.Depth - 1 || k + 1 == input.Depth - 1 ? 0f : (data[index + 2 * input.SliceSize] - data[index + input.SliceSize]) / weightz);

                        // simple gradient of gradiant: xy,xz,yx,yz,zx,zy,xx,yy,zz

                        gxx = gx1 - gx;
                        gyy = gy2 - gy;
                        gzz = (gz4 - gz) / weightz;
                        gxy = gx2 - gx;
                        gxz = (gx4 - gx) / weightz;
                        gyx = gy1 - gy;
                        gyz = (gy4 - gy) / weightz;
                        gzx = gz1 - gz;
                        gzy = gz2 - gz;

                        //sum xx*yy*zz + xz*yx*zy + xy*yz*zx - xx*yz*zy - zz*xy*yx -yy*xz*zx;
                        output.SingleChannelImage[0].Data[index] = gxx * gyy * gzz + gxz * gyx * gzy + gxy * gyz * gzx - gxx * gyz * gzy - gzz * gxy * gyx - gyy * gxz * gzx;
                    }

            return output;
        }
    }

    public class cVolumeDetection : c3DDetection
    {
        private bool IsBorderKill;

        public cMeshSmoother MeshSmoother = null;
        public cImage OriginalBinaryImage = null;

        cImage OriginalcImage = null;

        public cVolumeDetection(cImage SeqToProcess, int Channel, bool IsBordeKill)
        {
            this.OriginalcImage = SeqToProcess;
            this.Channel = Channel;
            this.Resolution = new cPoint3D(SeqToProcess.Resolution);
            this.IsBorderKill = IsBordeKill;
        }

        public cVolumeDetection(cImage SeqToProcess, int Channel, bool IsBordeKill, float IntensityAttenuation)
        {
            this.OriginalcImage = SeqToProcess;
            this.Channel = Channel;
            this.Resolution = new cPoint3D(SeqToProcess.Resolution);
            this.IsBorderKill = IsBordeKill;
            this.IntensityAttenuation = IntensityAttenuation;
        }

        public List<cInteractive3DObject> IntensityThreshold(float Threshold, float MinVoxelVolume, float MaxVoxelVolume)
        {


            // cImage CurrentImage = CurrentSequence[0];

            if (this.MedianKernel != -1)
            {
                CurrentImage = new cImage(this.OriginalcImage.Width, this.OriginalcImage.Height, this.OriginalcImage.Depth, this.OriginalcImage.GetNumChannels());

                this.Median3D(this.OriginalcImage, this.Channel, CurrentImage, this.Channel, MedianKernel);

                for (int IdxChannel = 0; IdxChannel < CurrentImage.GetNumChannels(); IdxChannel++)
                {
                    if (IdxChannel != this.Channel)
                        Array.Copy(this.OriginalcImage.SingleChannelImage[IdxChannel].Data, CurrentImage.SingleChannelImage[IdxChannel].Data, CurrentImage.ImageSize);

                }

                //   this.Channel = 0;
            }
            else
            {
                this.CurrentImage = this.OriginalcImage;
            }
            this.OriginalBinaryImage = new cImage(this.CurrentImage.Width, this.CurrentImage.Height, this.CurrentImage.Depth, 1);

            if (IntensityAttenuation == -1)
            {
                for (int IDx = 0; IDx < this.CurrentImage.ImageSize; IDx++)
                    if (CurrentImage.SingleChannelImage[Channel].Data[IDx] >= Threshold) this.OriginalBinaryImage.SingleChannelImage[0].Data[IDx] = 1;
                    else
                        this.OriginalBinaryImage.SingleChannelImage[0].Data[IDx] = 0;
            }
            else
            {
                float RealDepth = 0;
                float CurrentThreshold = Threshold;
                float StartingZposForCorrection = 6;

                for (int Z = 0; Z < this.CurrentImage.Depth; Z++)
                {
                    RealDepth = Z * (float)this.Resolution.Z;
                    //  if(RealDepth
                    if (RealDepth < StartingZposForCorrection)
                    {
                        CurrentThreshold = Threshold * (float)Math.Exp((StartingZposForCorrection - RealDepth) * this.IntensityAttenuation);
                    }
                    for (int IDx = 0; IDx < this.CurrentImage.SliceSize; IDx++)
                    {
                        if ((CurrentImage.SingleChannelImage[Channel].Data[IDx + Z * this.CurrentImage.SliceSize]) >= CurrentThreshold) this.OriginalBinaryImage.SingleChannelImage[0].Data[IDx + Z * this.CurrentImage.SliceSize] = 1;
                        else
                            this.OriginalBinaryImage.SingleChannelImage[0].Data[IDx + Z * this.CurrentImage.SliceSize] = 0;
                    }
                }
            }
            IntensityAttenuation = -1;
            return IntensityThreshold(MinVoxelVolume, MaxVoxelVolume);
        }

        public List<cInteractive3DObject> RegionGrowing(List<cInteractive3DObject> ListSeeds, float Threshold, float MinVoxelVolume, float MaxVoxelVolume, int NumIterations, float MergingRatio)
        {

            if (this.MedianKernel != -1)
            {
                CurrentImage = new cImage(this.OriginalcImage.Width, this.OriginalcImage.Height, this.OriginalcImage.Depth, 1);

                this.Median3D(this.OriginalcImage, this.Channel, CurrentImage, 0, MedianKernel);
                this.Channel = 0;
            }
            else
            {
                this.CurrentImage = this.OriginalcImage;
            }
            this.OriginalBinaryImage = new cImage(this.CurrentImage.Width, this.CurrentImage.Height, this.CurrentImage.Depth, 1);



            //if (IntensityAttenuation == -1)
            //{
            //    for (int IDx = 0; IDx < CurrentSequence.ImageSize; IDx++)
            //        if (CurrentSequence[0].Data[Channel][IDx] >= Threshold) BinaryImage.Data[0][IDx] = 1;
            //        else
            //            BinaryImage.Data[0][IDx] = 0;
            //}
            //else
            //{
            //    float RealDepth = 0;
            //    float CurrentThreshold = Threshold;
            //    float StartingZposForCorrection = 6;

            //    for (int Z = 0; Z < CurrentSequence.Depth; Z++)
            //    {
            //        RealDepth = Z * (float)CurrentSequence.ZResolution;
            //        //  if(RealDepth
            //        if (RealDepth < StartingZposForCorrection)
            //        {
            //            CurrentThreshold = Threshold * (float)Math.Exp((StartingZposForCorrection - RealDepth) * this.IntensityAttenuation);
            //        }
            //        for (int IDx = 0; IDx < CurrentSequence.SliceSize; IDx++)
            //        {
            //            if ((CurrentSequence[0].Data[Channel][IDx + Z * CurrentSequence.SliceSize]) >= CurrentThreshold) BinaryImage.Data[0][IDx + Z * CurrentSequence.SliceSize] = 1;
            //            else
            //                BinaryImage.Data[0][IDx + Z * CurrentSequence.SliceSize] = 0;
            //        }
            //    }
            //}
            //IntensityAttenuation = -1;

            cRegionGrowing RegionGrowing = new cRegionGrowing(ListSeeds, this, Threshold, (int)MaxVoxelVolume);
            RegionGrowing.RatioForMergingConnectedRegions = MergingRatio;
            RegionGrowing.MininumObjectVolume = (int)MinVoxelVolume;
            RegionGrowing.IsBorderKill = this.IsBorderKill;


            int FinalNumIteration = 0;
            if (NumIterations == -1)
                FinalNumIteration = RegionGrowing.Start(out this.OriginalBinaryImage);
            else
                FinalNumIteration = RegionGrowing.Grow(out this.OriginalBinaryImage, NumIterations);

            return IntensityThreshold(MinVoxelVolume, MaxVoxelVolume);
        }

        public List<cInteractive3DObject> RegionGrowing(ConnectedComponentSet ListVolumes, float Threshold, float MinVoxelVolume, float MaxVoxelVolume, int NumIterations, float MergingRatio)
        {


            if (this.MedianKernel != -1)
            {
                CurrentImage = new cImage(this.OriginalcImage.Width, this.OriginalcImage.Height, this.OriginalcImage.Depth, 1);

                this.Median3D(this.OriginalcImage, this.Channel, CurrentImage, 0, MedianKernel);
                this.Channel = 0;
            }
            else
            {
                this.CurrentImage = this.OriginalcImage;
            }

            cImage BinaryImage = new cImage(this.CurrentImage.Width, this.CurrentImage.Height, this.CurrentImage.Depth, 1);
            //if (IntensityAttenuation == -1)
            //{
            //    for (int IDx = 0; IDx < CurrentSequence.ImageSize; IDx++)
            //        if (CurrentSequence[0].Data[Channel][IDx] >= Threshold) BinaryImage.Data[0][IDx] = 1;
            //        else
            //            BinaryImage.Data[0][IDx] = 0;
            //}
            //else
            //{
            //    float RealDepth = 0;
            //    float CurrentThreshold = Threshold;
            //    float StartingZposForCorrection = 6;

            //    for (int Z = 0; Z < CurrentSequence.Depth; Z++)
            //    {
            //        RealDepth = Z * (float)CurrentSequence.ZResolution;
            //        //  if(RealDepth
            //        if (RealDepth < StartingZposForCorrection)
            //        {
            //            CurrentThreshold = Threshold * (float)Math.Exp((StartingZposForCorrection - RealDepth) * this.IntensityAttenuation);
            //        }
            //        for (int IDx = 0; IDx < CurrentSequence.SliceSize; IDx++)
            //        {
            //            if ((CurrentSequence[0].Data[Channel][IDx + Z * CurrentSequence.SliceSize]) >= CurrentThreshold) BinaryImage.Data[0][IDx + Z * CurrentSequence.SliceSize] = 1;
            //            else
            //                BinaryImage.Data[0][IDx + Z * CurrentSequence.SliceSize] = 0;
            //        }
            //    }
            //}
            //IntensityAttenuation = -1;

            cRegionGrowing RegionGrowing = new cRegionGrowing(ListVolumes, this, Threshold, (int)MaxVoxelVolume);
            RegionGrowing.RatioForMergingConnectedRegions = MergingRatio;
            RegionGrowing.MininumObjectVolume = (int)MinVoxelVolume;
            RegionGrowing.IsBorderKill = this.IsBorderKill;


            int FinalNumIteration = 0;
            if (NumIterations == -1)
                FinalNumIteration = RegionGrowing.Start(out BinaryImage);
            else
                FinalNumIteration = RegionGrowing.Grow(out BinaryImage, NumIterations);

            return IntensityThreshold(MinVoxelVolume, MaxVoxelVolume);
        }

        #region Private functions

        private List<cInteractive3DObject> IntensityThreshold(float MinVolexVolume, float MaxVolexVolume)
        {
            //  return null;
            List<cInteractive3DObject> CurrentList3DObject = new List<cInteractive3DObject>();

            // Then Connected Component detection
            cImage LabelImage = new cImage(this.CurrentImage.Width, this.CurrentImage.Height, this.CurrentImage.Depth, 1);
            ConnectedComponentSet CurrentCCS = new ConnectedComponentSet(OriginalBinaryImage, 0, LabelImage, 0, this.CurrentImage, Channel,
                        eConnectivity.THREED_6,
                        MinVolexVolume,
                        MaxVolexVolume);

            if (CurrentCCS.Count == 0)
            {
                Console.WriteLine("No object detected");
                return CurrentList3DObject;
            }

            // Copy the exact labeled object to a sub image
            int RealLabel = 0;
            for (int IdxObj = 0; IdxObj < CurrentCCS.Count; IdxObj++)
            {
                if ((this.IsBorderKill) && ((CurrentCCS[IdxObj].IsOn2DEdge) || (CurrentCCS[IdxObj].MinZ <= 0) || (CurrentCCS[IdxObj].MinZ + CurrentCCS[IdxObj].Depth) >= OriginalBinaryImage.Depth))
                    continue;

                RealLabel++;

                cImage BinarySubImage = new cImage(CurrentCCS[IdxObj].Width + 2, CurrentCCS[IdxObj].Height + 2, CurrentCCS[IdxObj].Depth + 2, 1);
                int CurrentLabel;

                int StartX = CurrentCCS[IdxObj].MinX;
                int StartY = CurrentCCS[IdxObj].MinY;
                int StartZ = CurrentCCS[IdxObj].MinZ;

                int ZShift;
                int YShift;
                int XShift;

                for (int Z = 0; Z < BinarySubImage.Depth; Z++)
                {
                    ZShift = (StartZ + Z) * LabelImage.SliceSize;
                    for (int Y = 0; Y < BinarySubImage.Height; Y++)
                    {
                        YShift = (StartY + Y) * LabelImage.Width;
                        for (int X = 0; X < BinarySubImage.Width; X++)
                        {
                            XShift = X + StartX;
                            if (((StartZ + Z) >= 0) && ((StartZ + Z) < LabelImage.Depth) && ((StartY + Y) >= 0) && ((StartY + Y) < LabelImage.Height) && ((StartX + X) >= 0) && ((StartX + X) < LabelImage.Width))
                            {
                                CurrentLabel = (int)LabelImage.SingleChannelImage[0].Data[XShift + YShift + ZShift];
                                if (CurrentLabel == (IdxObj + 1))
                                    BinarySubImage.SingleChannelImage[0].Data[X + 1 + (Y + 1) * BinarySubImage.Width + (Z + 1) * BinarySubImage.SliceSize] = RealLabel;
                            }
                        }
                    }
                }
                cImage TmpSeq = new cImage(BinarySubImage, true);
                //TmpSeq.Add(BinarySubImage);
                //TmpSeq.XResolution = this.XRes;
                //TmpSeq.YResolution = this.YRes;
                //TmpSeq.ZResolution = this.ZRes;

                cBiological3DVolume CurrentVolume = new cBiological3DVolume();
                CurrentVolume.MeshSmoother = this.MeshSmoother;

                if (this.Containers != null)
                    CurrentVolume.Containers = new cContainer(this.Containers, this.ContainerMode);

                //CurrentVolume.Containers = this.Containers;


                //if (Containers == null)
                //{
                CurrentVolume.Generate(TmpSeq, Color.DodgerBlue, new cPoint3D(StartX * (float)this.Resolution.X + Shift.X, StartY * (float)this.Resolution.Y + Shift.Y, StartZ * (float)this.Resolution.Z + Shift.Z));
                //}
                //else
                //{
                //    CurrentVolume.Generate(TmpSeq, Color.DodgerBlue, new cPoint3D(StartX * (float)CurrentSequence.XResolution + Shift.X, StartY * (float)CurrentSequence.YResolution + Shift.Y, StartZ * (float)CurrentSequence.ZResolution + Shift.Z), this.Containers, ContainerMode);
                //}

                if (CurrentVolume.Detected)
                {
                    CurrentVolume.AssociatedConnectedComponent = CurrentCCS[IdxObj];
                    CurrentList3DObject.Add(CurrentVolume);
                }
            }
            return CurrentList3DObject;
        }


        #endregion
    }

    #region Specific Classes for Region growing

    public class cPtsInContact : cListPoints3D
    {
        public int IdxRegion;

        public cPtsInContact(cPoint3D FirstPt, int IdxRegion)
        {
            this.IdxRegion = IdxRegion;
            this.Add(FirstPt);
        }
    }

    public class cListPtsInContact : List<cPtsInContact>
    {
        public cPtsInContact Contains(int IdxRegion)
        {
            foreach (cPtsInContact PtsInContact in this)
            {
                if (PtsInContact.IdxRegion == IdxRegion) return PtsInContact;
            }
            return null;
        }
    }

    #endregion

    /// <summary>
    /// Region class
    /// </summary>
    public class cRegion
    {
        cPoint3D OriginalSeed = null;
        int Idx;
        public cRegionGrowing GlobalRegionList;
        cListPoints3D CurrentBoundaries = null;
        cListPoints3D ListPtsVolume = null;
        public cListPtsInContact ContactPts = null;
        public bool IsFinished = false;
        public bool IsTouchingBorder = false;

        /// <summary>
        /// cRegion constructor
        /// </summary>
        /// <param name="SeedPt">Initial seed point</param>
        /// <param name="Idx">Region index</param>
        /// <param name="GlobalRegionList">Global region growing</param>
        public cRegion(cPoint3D SeedPt, int Idx, cRegionGrowing GlobalRegionList)
        {
            OriginalSeed = new cPoint3D(SeedPt.X, SeedPt.Y, SeedPt.Z);
            this.Idx = Idx;
            this.GlobalRegionList = GlobalRegionList;

            this.GlobalRegionList.AlreadyDetectedRegions.SingleChannelImage[0].Data[(int)SeedPt.X + ((int)SeedPt.Y * this.GlobalRegionList.ImageSource.SliceSize) + ((int)SeedPt.Z * this.GlobalRegionList.ImageSource.SliceSize)] = Idx;

            CurrentBoundaries = new cListPoints3D();
            CurrentBoundaries.Add(new cPoint3D(OriginalSeed.X, OriginalSeed.Y, OriginalSeed.Z));

            ListPtsVolume = new cListPoints3D();
            ListPtsVolume.Add(new cPoint3D(OriginalSeed.X, OriginalSeed.Y, OriginalSeed.Z));

            //  IdxRegionToBeMergedWith = new List<int>();
            ContactPts = new cListPtsInContact();
        }


        /// <summary>
        /// cRegion constructor
        /// </summary>
        /// <param name="SeedPt">Initial seed point</param>
        /// <param name="Idx">Region index</param>
        /// <param name="GlobalRegionList">Global region growing</param>
        public cRegion(ConnectedVoxels InitialRegion, int Idx, cRegionGrowing GlobalRegionList)
        {
            OriginalSeed = new cPoint3D(InitialRegion.X, InitialRegion.Y, InitialRegion.Z);
            this.Idx = Idx;
            this.GlobalRegionList = GlobalRegionList;
            CurrentBoundaries = new cListPoints3D();

            for (int IdxPtContour = 0; IdxPtContour < InitialRegion.SurfacePoints.Count; IdxPtContour++)
            {
                CurrentBoundaries.Add(new cPoint3D(InitialRegion.SurfacePoints[IdxPtContour][0], InitialRegion.SurfacePoints[IdxPtContour][1], InitialRegion.SurfacePoints[IdxPtContour][2]));
            }
            ListPtsVolume = new cListPoints3D();

            for (int IdxPt = 0; IdxPt < InitialRegion.Points.Count; IdxPt++)
            {
                ListPtsVolume.Add(new cPoint3D(InitialRegion.Points[IdxPt][0], InitialRegion.Points[IdxPt][1], InitialRegion.Points[IdxPt][2]));
                this.GlobalRegionList.AlreadyDetectedRegions.SingleChannelImage[0].Data[(int)InitialRegion.Points[IdxPt][0] + ((int)InitialRegion.Points[IdxPt][1] * this.GlobalRegionList.ImageSource.SliceSize) + ((int)InitialRegion.Points[IdxPt][2] * this.GlobalRegionList.ImageSource.SliceSize)] = Idx;
            }

            ContactPts = new cListPtsInContact();
        }

        #region Information extraction
        double NormalizedIntensity = -1;

        public double ComputeIntensity(int Channel)
        {
            double Intensity = 0;
            foreach (cPoint3D CurrtPt in ListPtsVolume)
            {
                Intensity += GlobalRegionList.ImageSource.SingleChannelImage[Channel].Data[(int)CurrtPt.X + ((int)CurrtPt.Y * GlobalRegionList.GetDetectedRegions().SliceSize) + ((int)CurrtPt.Z * GlobalRegionList.GetDetectedRegions().SliceSize)];
            }
            return Intensity;
        }

        public double ComputeAverageIntensity(int Channel)
        {
            double Intensity = 0;
            foreach (cPoint3D CurrtPt in ListPtsVolume)
            {
                Intensity += GlobalRegionList.ImageSource.SingleChannelImage[Channel].Data[(int)CurrtPt.X + ((int)CurrtPt.Y * GlobalRegionList.GetDetectedRegions().SliceSize) + ((int)CurrtPt.Z * GlobalRegionList.GetDetectedRegions().SliceSize)];
            }
            return Intensity / (double)ListPtsVolume.Count;
        }

        public double GetNormalizedIntensity()
        {
            return this.NormalizedIntensity;
        }

        public double ComputeNormalizedIntensity(int Channel)
        {
            NormalizedIntensity = 0;

            double IntensityForNormalization = 0;

            if (GlobalRegionList.ImageForNormalization != null)
            {
                foreach (cPoint3D CurrtPt in ListPtsVolume)
                {
                    IntensityForNormalization += GlobalRegionList.ImageForNormalization[(int)CurrtPt.X + ((int)CurrtPt.Y * GlobalRegionList.GetDetectedRegions().SliceSize) + ((int)CurrtPt.Z * GlobalRegionList.GetDetectedRegions().SliceSize)];
                }
                //IntensityForNormalization /= (double)ListPtsVolume.Count;
            }
            foreach (cPoint3D CurrtPt in ListPtsVolume)
            {
                NormalizedIntensity += GlobalRegionList.ImageSource.SingleChannelImage[Channel].Data[(int)CurrtPt.X + ((int)CurrtPt.Y * GlobalRegionList.GetDetectedRegions().SliceSize) + ((int)CurrtPt.Z * GlobalRegionList.GetDetectedRegions().SliceSize)];
            }

            if (IntensityForNormalization >= 0)
            {
                NormalizedIntensity /= IntensityForNormalization;
                // NormalizedIntensity /= (double)ListPtsVolume.Count;
                NormalizedIntensity *= 100;
            }
            return NormalizedIntensity;
        }
        #endregion

        #region cRegion I/O
        public cListPoints3D GetListPtsVolume()
        {
            return this.ListPtsVolume;
        }

        public int GetIdx()
        {
            return this.Idx;
        }

        public void SetIndex(int NewIndex)
        {
            this.Idx = NewIndex;
            cImage RegionsDetected = GlobalRegionList.GetDetectedRegions();

            foreach (cPoint3D CurrtPt in ListPtsVolume)
            {
                RegionsDetected.SingleChannelImage[0].Data[(int)CurrtPt.X + ((int)CurrtPt.Y * GlobalRegionList.GetDetectedRegions().SliceSize) + ((int)CurrtPt.Z * GlobalRegionList.GetDetectedRegions().SliceSize)] = this.Idx;
            }
        }

        public int GetVolume()
        {
            return this.ListPtsVolume.Count;
        }

        public cPoint3D GetOriginalSeed()
        {
            return OriginalSeed;
        }

        public void CleanIdxPtsInContact()
        {
            // return;
            for (int Idx0 = 0; Idx0 < ContactPts.Count; Idx0++)
            {
                for (int Idx1 = 0; Idx1 < ContactPts.Count; Idx1++)
                {
                    // if there is a redundant Idx
                    if ((Idx0 != Idx1) && (ContactPts[Idx0].IdxRegion == ContactPts[Idx1].IdxRegion))
                    {
                        ContactPts[Idx0].AddRange(ContactPts[Idx1]);
                        ContactPts.Remove(ContactPts[Idx1]);
                    }
                }
            }
        }

        #endregion

        #region Region growing process
        private cListPoints3D SinglePtProgress(int idxPt)
        {
            // bool IsMoved = false;
            // if (this.IdxRegionToBeMergedWith == null) this.IdxRegionToBeMergedWith = new List<int>();
            cPoint3D CurrtPt = CurrentBoundaries[idxPt];

            // check all the the surrounding pts
            int PosSurrounding = (int)CurrtPt.X + ((int)CurrtPt.Y * GlobalRegionList.GetDetectedRegions().SliceSize) + ((int)CurrtPt.Z * GlobalRegionList.GetDetectedRegions().SliceSize);//;

            cImage RegionsDetected = GlobalRegionList.GetDetectedRegions();
            cImage ImageSource = GlobalRegionList.ImageSource;
            cListPoints3D NewPts = new cListPoints3D();

            float IntensityValue;
            int RegionClass;

            if (this.GlobalRegionList.ImageSource.Depth > 1)
            {
                // Top
                if (CurrtPt.Z < RegionsDetected.Depth - 1)
                {
                    RegionClass = (int)RegionsDetected.SingleChannelImage[0].Data[PosSurrounding + RegionsDetected.SliceSize];
                    if (RegionClass <= 0)
                    {
                        IntensityValue = ImageSource.SingleChannelImage[GlobalRegionList.ChannelSource].Data[PosSurrounding + RegionsDetected.SliceSize];
                        if (IntensityValue >= GlobalRegionList.IntensityThreshold)
                        {
                            RegionsDetected.SingleChannelImage[0].Data[PosSurrounding + RegionsDetected.SliceSize] = this.Idx;
                            NewPts.Add(new cPoint3D(CurrtPt.X, CurrtPt.Y, CurrtPt.Z + 1));
                        }
                    }
                    else if (RegionClass != this.Idx)
                    {
                        cPtsInContact TmpPtsInContact = ContactPts.Contains(GlobalRegionList.GetRegion(RegionClass).GetIdx());

                        if (TmpPtsInContact == null)
                            ContactPts.Add(new cPtsInContact(new cPoint3D(CurrtPt.X, CurrtPt.Y, CurrtPt.Z + 1), GlobalRegionList.GetRegion(RegionClass).GetIdx()));
                        else
                            TmpPtsInContact.Add(new cPoint3D(CurrtPt.X, CurrtPt.Y, CurrtPt.Z + 1));
                    }
                }
                else this.IsTouchingBorder = true;

                // bottom
                if (CurrtPt.Z > 0)
                {
                    // Left
                    RegionClass = (int)RegionsDetected.SingleChannelImage[0].Data[PosSurrounding - RegionsDetected.SliceSize];
                    if (RegionClass <= 0)
                    {
                        IntensityValue = ImageSource.SingleChannelImage[GlobalRegionList.ChannelSource].Data[PosSurrounding - RegionsDetected.SliceSize];
                        if (IntensityValue >= GlobalRegionList.IntensityThreshold)
                        {
                            RegionsDetected.SingleChannelImage[0].Data[PosSurrounding - RegionsDetected.SliceSize] = this.Idx;
                            NewPts.Add(new cPoint3D(CurrtPt.X, CurrtPt.Y, CurrtPt.Z - 1));
                        }
                    }
                    else if (RegionClass != this.Idx)
                    {
                        cPtsInContact TmpPtsInContact = ContactPts.Contains(GlobalRegionList.GetRegion(RegionClass).GetIdx());

                        if (TmpPtsInContact == null)
                            ContactPts.Add(new cPtsInContact(new cPoint3D(CurrtPt.X, CurrtPt.Y, CurrtPt.Z - 1), GlobalRegionList.GetRegion(RegionClass).GetIdx()));
                        else
                            TmpPtsInContact.Add(new cPoint3D(CurrtPt.X, CurrtPt.Y, CurrtPt.Z - 1));
                    }
                }
                else this.IsTouchingBorder = true;
            }

            // Right
            if (CurrtPt.X < RegionsDetected.Width - 1)
            {
                RegionClass = (int)RegionsDetected.SingleChannelImage[0].Data[PosSurrounding + 1];
                if (RegionClass <= 0)
                {
                    IntensityValue = ImageSource.SingleChannelImage[GlobalRegionList.ChannelSource].Data[PosSurrounding + 1];
                    if (IntensityValue >= GlobalRegionList.IntensityThreshold)
                    {
                        RegionsDetected.SingleChannelImage[0].Data[PosSurrounding + 1] = this.Idx;
                        NewPts.Add(new cPoint3D(CurrtPt.X + 1, CurrtPt.Y, CurrtPt.Z));
                    }
                }
                else if (RegionClass != this.Idx)
                {
                    cPtsInContact TmpPtsInContact = ContactPts.Contains(GlobalRegionList.GetRegion(RegionClass).GetIdx());

                    if (TmpPtsInContact == null)
                        ContactPts.Add(new cPtsInContact(new cPoint3D(CurrtPt.X + 1, CurrtPt.Y, CurrtPt.Z), GlobalRegionList.GetRegion(RegionClass).GetIdx()));
                    else
                        TmpPtsInContact.Add(new cPoint3D(CurrtPt.X + 1, CurrtPt.Y, CurrtPt.Z));
                }
            }
            else this.IsTouchingBorder = true;

            if (CurrtPt.X > 0)
            {
                // Left
                RegionClass = (int)RegionsDetected.SingleChannelImage[0].Data[PosSurrounding - 1];
                if (RegionClass <= 0)
                {
                    IntensityValue = ImageSource.SingleChannelImage[GlobalRegionList.ChannelSource].Data[PosSurrounding - 1];
                    if (IntensityValue >= GlobalRegionList.IntensityThreshold)
                    {
                        RegionsDetected.SingleChannelImage[0].Data[PosSurrounding - 1] = this.Idx;
                        NewPts.Add(new cPoint3D(CurrtPt.X - 1, CurrtPt.Y, CurrtPt.Z));
                    }
                }
                else if (RegionClass != this.Idx)
                {
                    cPtsInContact TmpPtsInContact = ContactPts.Contains(GlobalRegionList.GetRegion(RegionClass).GetIdx());

                    if (TmpPtsInContact == null)
                        ContactPts.Add(new cPtsInContact(new cPoint3D(CurrtPt.X + 1, CurrtPt.Y, CurrtPt.Z), GlobalRegionList.GetRegion(RegionClass).GetIdx()));
                    else
                        TmpPtsInContact.Add(new cPoint3D(CurrtPt.X + 1, CurrtPt.Y, CurrtPt.Z));

                }
            }
            else this.IsTouchingBorder = true;

            if (CurrtPt.Y < RegionsDetected.Height - 1)
            {
                // Top
                RegionClass = (int)RegionsDetected.SingleChannelImage[0].Data[PosSurrounding + RegionsDetected.Width];
                if (RegionClass <= 0)
                {
                    IntensityValue = ImageSource.SingleChannelImage[GlobalRegionList.ChannelSource].Data[PosSurrounding + RegionsDetected.Width];
                    if (IntensityValue >= GlobalRegionList.IntensityThreshold)
                    {
                        RegionsDetected.SingleChannelImage[0].Data[PosSurrounding + RegionsDetected.Width] = this.Idx;
                        NewPts.Add(new cPoint3D(CurrtPt.X, CurrtPt.Y + 1, CurrtPt.Z));
                    }
                }
                else if (RegionClass != this.Idx)
                {
                    cPtsInContact TmpPtsInContact = ContactPts.Contains(GlobalRegionList.GetRegion(RegionClass).GetIdx());

                    if (TmpPtsInContact == null)
                        ContactPts.Add(new cPtsInContact(new cPoint3D(CurrtPt.X + 1, CurrtPt.Y, CurrtPt.Z), GlobalRegionList.GetRegion(RegionClass).GetIdx()));
                    else
                        TmpPtsInContact.Add(new cPoint3D(CurrtPt.X + 1, CurrtPt.Y, CurrtPt.Z));
                }
            }
            else this.IsTouchingBorder = true;

            if (CurrtPt.Y > 0)
            {
                // Bottom
                RegionClass = (int)RegionsDetected.SingleChannelImage[0].Data[PosSurrounding - RegionsDetected.Width];
                if (RegionClass <= 0)
                {
                    IntensityValue = ImageSource.SingleChannelImage[GlobalRegionList.ChannelSource].Data[PosSurrounding - RegionsDetected.Width];
                    if (IntensityValue >= GlobalRegionList.IntensityThreshold)
                    {
                        RegionsDetected.SingleChannelImage[0].Data[PosSurrounding - RegionsDetected.Width] = this.Idx;
                        NewPts.Add(new cPoint3D(CurrtPt.X, CurrtPt.Y - 1, CurrtPt.Z));
                    }
                }
                else if (RegionClass != this.Idx)
                {
                    cPtsInContact TmpPtsInContact = ContactPts.Contains(GlobalRegionList.GetRegion(RegionClass).GetIdx());

                    if (TmpPtsInContact == null)
                        ContactPts.Add(new cPtsInContact(new cPoint3D(CurrtPt.X + 1, CurrtPt.Y, CurrtPt.Z), GlobalRegionList.GetRegion(RegionClass).GetIdx()));
                    else
                        TmpPtsInContact.Add(new cPoint3D(CurrtPt.X + 1, CurrtPt.Y, CurrtPt.Z));
                }
            }
            else this.IsTouchingBorder = true;

            return NewPts;
        }

        public void Progress()
        {
            cListPoints3D GlobalNewPts = new cListPoints3D();

            //IdxRegionToBeMergedWith = null;

            // a loop over all the pts from the contour
            for (int idxPt = 0; idxPt < CurrentBoundaries.Count; idxPt++)
            {
                // extract new point from the growing around this contour point
                cListPoints3D NewPtsFromGrowing = this.SinglePtProgress(idxPt);

                // increase the list
                GlobalNewPts.AddRange(NewPtsFromGrowing);

                // and also the volume
                foreach (cPoint3D NewPt in NewPtsFromGrowing)
                {
                    this.ListPtsVolume.Add(new cPoint3D(NewPt));
                }
                //this.ListPtsVolume.AddRange(NewPtsFromGrowing);

                // whatever happened : removed the pt from the list
                this.CurrentBoundaries.Remove(this.CurrentBoundaries[idxPt]);
            }

            if (GlobalNewPts.Count > 0)
                this.CurrentBoundaries.AddRange(GlobalNewPts);
            else
                IsFinished = true;

            //    if(this.Volume>GlobalRegionList.MaxVolume)
            //      IsFinished = true;

        }
        #endregion
    }

    /// <summary>
    /// Region Growing Class
    /// </summary>
    public class cRegionGrowing : List<cRegion>
    {
        public cImage AlreadyDetectedRegions = null;
        public cImage ImageSource = null;
        public int ChannelSource;
        public float IntensityThreshold;
        public int MaxVolume = 0;
        public int MininumObjectVolume = 0;
        public float RatioForMergingConnectedRegions = -1;
        public int NumIterations = 0;
        public bool IsBorderKill = false;
        public bool IsMergeConnectedRegions = false;


        public float[] ImageForNormalization = null;

        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="ListSeeds">initial seed points</param>
        /// <param name="ImageSource">image data (for intensity)</param>
        /// <param name="ChannelSource">Image source band</param>
        /// <param name="IntensityThreshold">Maximum intensity</param>
        /// <param name="MaxVolume">Maximum object volume</param>
        public cRegionGrowing(cListPoints3D ListSeeds, cImage ImageSource, int ChannelSource, float IntensityThreshold, int MaxVolume)
        {
            AlreadyDetectedRegions = new cImage(ImageSource.Width, ImageSource.Height, ImageSource.Depth, 1);
            this.IntensityThreshold = IntensityThreshold;
            this.ImageSource = ImageSource;
            this.MaxVolume = MaxVolume;
            this.ChannelSource = ChannelSource;
            int IdxRegion = 1;
            foreach (cPoint3D CurrentPt in ListSeeds)
            {
                this.Add(new cRegion(CurrentPt, IdxRegion, this));
                //AlreadyDetectedRegions.Data[0][(int)CurrentPt.X + ((int)CurrentPt.Y * ImageSource.SliceSize) + ((int)CurrentPt.Z * ImageSource.SliceSize)] = IdxRegion;
                IdxRegion++;
            }
        }



        public cRegionGrowing(List<cInteractive3DObject> ListSeeds, c3DDetection Detection3D, float IntensityThreshold, int MaxVolume)
        {
            this.ImageSource = Detection3D.CurrentImage;
            AlreadyDetectedRegions = new cImage(ImageSource.Width, ImageSource.Height, ImageSource.Depth, 1);
            this.IntensityThreshold = IntensityThreshold;

            this.MaxVolume = MaxVolume;
            this.ChannelSource = Detection3D.Channel;
            int IdxRegion = 1;
            foreach (cBiological3DVolume CurrentObj in ListSeeds)
            {
                cPoint3D NewPt = new cPoint3D(CurrentObj.GetCentroid());
                NewPt.X /= (float)Detection3D.Resolution.X;
                NewPt.Y /= (float)Detection3D.Resolution.Y;
                NewPt.Z /= (float)Detection3D.Resolution.Z;

                this.Add(new cRegion(NewPt, IdxRegion, this));
                //AlreadyDetectedRegions.Data[0][(int)CurrentPt.X + ((int)CurrentPt.Y * ImageSource.SliceSize) + ((int)CurrentPt.Z * ImageSource.SliceSize)] = IdxRegion;
                IdxRegion++;
            }
        }
        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="ListSeeds">initial Connected component list</param>
        /// <param name="ImageSource">image data (for intensity)</param>
        /// <param name="ChannelSource">Image source band</param>
        /// <param name="IntensityThreshold">Maximum intensity</param>
        /// <param name="MaxVolume">Maximum object volume</param>
        public cRegionGrowing(ConnectedComponentSet InitialConnectedComponentSet, c3DDetection Detection3D, float IntensityThreshold, int MaxVolume)
        {
            AlreadyDetectedRegions = new cImage(ImageSource.Width, ImageSource.Height, ImageSource.Depth, 1);
            this.IntensityThreshold = IntensityThreshold;
            this.ImageSource = Detection3D.CurrentImage;
            this.MaxVolume = MaxVolume;
            this.ChannelSource = Detection3D.Channel;
            int IdxRegion = 1;
            foreach (ConnectedVoxels CurrentConnectedComponent in InitialConnectedComponentSet)
            {
                this.Add(new cRegion(CurrentConnectedComponent, IdxRegion, this));
                //AlreadyDetectedRegions.Data[0][(int)CurrentPt.X + ((int)CurrentPt.Y * ImageSource.SliceSize) + ((int)CurrentPt.Z * ImageSource.SliceSize)] = IdxRegion;
                IdxRegion++;
            }
        }

        #region Information extraction section
        cExtendedList ListNormalizedItensity = null;

        public cExtendedList ComputeListIntensity(int Channel)
        {
            cExtendedList ToReturn = new cExtendedList();

            foreach (cRegion TmpRegion in this)
            {
                ToReturn.Add(TmpRegion.ComputeIntensity(Channel));
            }
            return ToReturn;
        }

        public cExtendedList ComputeListAverageIntensity(int Channel)
        {
            cExtendedList ToReturn = new cExtendedList();

            foreach (cRegion TmpRegion in this)
            {
                ToReturn.Add(TmpRegion.ComputeAverageIntensity(Channel));
            }
            return ToReturn;
        }

        public cExtendedList GetListNormalizedIntensity()
        {
            return this.ListNormalizedItensity;
        }

        public cExtendedList ComputeListNormalizedIntensity(int Channel)
        {
            ListNormalizedItensity = new cExtendedList();

            foreach (cRegion TmpRegion in this)
            {
                ListNormalizedItensity.Add(TmpRegion.ComputeNormalizedIntensity(Channel));
            }
            return ListNormalizedItensity;
        }

        public double ComputeBackGroundIntensity(int Channel, out int NumPixelBackground)
        {
            NumPixelBackground = 0;
            double IntensityBackGround = 0;
            for (int IdxPix = 0; IdxPix < ImageSource.ImageSize; IdxPix++)
            {
                if (AlreadyDetectedRegions.SingleChannelImage[0].Data[IdxPix] == 0)
                {
                    NumPixelBackground++;
                    IntensityBackGround += ImageSource.SingleChannelImage[Channel].Data[IdxPix];
                }
            }
            return IntensityBackGround;
        }

        public cExtendedList GetListVolumes()
        {
            cExtendedList ToReturn = new cExtendedList();

            foreach (cRegion TmpRegion in this)
            {
                ToReturn.Add((double)TmpRegion.GetVolume());
            }
            return ToReturn;
        }

        /// <summary>
        /// return the region associated to a certain index
        /// </summary>
        /// <param name="IdxRegion">index of the region</param>
        /// <returns>the region (null if no region have been identified)</returns>
        public cRegion GetRegion(int IdxRegion)
        {
            foreach (cRegion CurrRegion in this)
            {
                if (CurrRegion.GetIdx() == IdxRegion) return CurrRegion;
            }
            return null;
        }

        public cImage GetDetectedRegions()
        {
            return this.AlreadyDetectedRegions;
        }

        #endregion

        #region Region growing procedures
        /// <summary>
        /// single iteration of the region growing
        /// </summary>
        /// <returns></returns>
        private bool Progress()
        {
            bool IsFinished = false;

            int NumProcessed = 0;

            int NumRegions = this.Count;

            for (int IdxRegion = 0; IdxRegion < this.Count; IdxRegion++)
            {
                // if the region has to progress again
                if (this[IdxRegion].IsFinished == false)
                {
                    this[IdxRegion].Progress();
                    NumProcessed++;
                    // return true;
                    // region is too big ... remove from the list
                    if (this[IdxRegion].GetVolume() > this.MaxVolume)
                    {
                        this.RemoveRegion(this[IdxRegion]);
                        IdxRegion--;
                    }
                }
            }

            // Update Region Index
            if (IsMergeConnectedRegions)
            {
                int Idx_Region = 0;
                foreach (cRegion TmpRegion in this)
                {
                    TmpRegion.SetIndex(Idx_Region);
                }
            }

            if (NumProcessed == 0)
                IsFinished = true;

            return IsFinished;

        }

        /// <summary>
        /// Perform a region growing until the end
        /// </summary>
        /// <param name="Result">Return an image of the regions</param>
        /// <returns></returns>
        public int Start(out cImage Result)
        {
            NumIterations = 0;
            while (!this.Progress())
            {
                NumIterations++;
            }

            if (RatioForMergingConnectedRegions > -1)
                this.MergeConnectedRegions(RatioForMergingConnectedRegions);

            if (IsBorderKill) this.BorderKill();

            if (MininumObjectVolume > 0)
                this.RemoveSmallRegions(MininumObjectVolume);

            Result = new cImage(this.GetDetectedRegions().Width, this.GetDetectedRegions().Height, this.GetDetectedRegions().Depth, 1);

            int IdxPix = 0;
            foreach (cRegion TmpRegion in this)
            {
                IdxPix++;
                foreach (cPoint3D PtTemp in TmpRegion.GetListPtsVolume())
                    Result.SingleChannelImage[0].Data[(int)PtTemp.X + (int)PtTemp.Y * Result.SliceSize + (int)PtTemp.Z * Result.SliceSize] = TmpRegion.GetIdx();
            }
            return this.Count;

        }

        /// <summary>
        /// Perform a region growing for a defined number of iterations
        /// </summary>
        /// <param name="Result">Resulting segmented image</param>
        /// <param name="Num_Iterations">Number of iterations</param>
        /// <returns></returns>
        public int Grow(out cImage Result, int Num_Iterations)
        {
            NumIterations = 0;
            for (int i = 0; i < Num_Iterations; i++)
            {
                this.Progress();
                NumIterations++;
            }
            if (RatioForMergingConnectedRegions > -1)
                this.MergeConnectedRegions(RatioForMergingConnectedRegions);

            if (IsBorderKill) this.BorderKill();

            if (MininumObjectVolume > 0)
                this.RemoveSmallRegions(MininumObjectVolume);

            Result = new cImage(this.GetDetectedRegions().Width, this.GetDetectedRegions().Height, this.GetDetectedRegions().Depth, 1);

            int IdxPix = 0;
            foreach (cRegion TmpRegion in this)
            {
                IdxPix++;
                foreach (cPoint3D PtTemp in TmpRegion.GetListPtsVolume())
                    Result.SingleChannelImage[0].Data[(int)PtTemp.X + (int)PtTemp.Y * Result.SliceSize + (int)PtTemp.Z * Result.SliceSize] = TmpRegion.GetIdx();
            }
            return this.Count;
        }
        #endregion

        #region regions management
        private void RemoveRegion(cRegion RegionToBeRemoved)
        {
            foreach (cPoint3D Pt in RegionToBeRemoved.GetListPtsVolume())
            {
                this.AlreadyDetectedRegions.SingleChannelImage[0].Data[(int)Pt.X + ((int)Pt.Y) * AlreadyDetectedRegions.SliceSize + ((int)Pt.Z) * AlreadyDetectedRegions.SliceSize] = 0;
            }
            this.Remove(RegionToBeRemoved);
        }

        private void MergeRegions(cRegion RegionSource, cRegion RegionDest)
        {
            // first update the map
            foreach (cPoint3D CurrtPt in RegionDest.GetListPtsVolume())
            {
                AlreadyDetectedRegions.SingleChannelImage[0].Data[(int)CurrtPt.X + ((int)CurrtPt.Y * AlreadyDetectedRegions.SliceSize) + ((int)CurrtPt.Z * AlreadyDetectedRegions.SliceSize)] = RegionSource.GetIdx();
            }

            RegionSource.GetListPtsVolume().AddRange(RegionDest.GetListPtsVolume());

            // first Remove contact points with the suppressed region
            for (int i = 0; i < RegionSource.ContactPts.Count; i++)
            {
                if (RegionSource.ContactPts[i].IdxRegion == RegionDest.GetIdx())
                {
                    RegionSource.ContactPts.Remove(RegionSource.ContactPts[i]);
                    break;
                }
            }

            for (int i = 0; i < RegionDest.ContactPts.Count; i++)
            {
                if (RegionDest.ContactPts[i].IdxRegion == RegionSource.GetIdx())
                {
                    RegionDest.ContactPts.Remove(RegionDest.ContactPts[i]);
                    break;
                }
            }

            if (RegionDest.IsTouchingBorder) RegionSource.IsTouchingBorder = true;

            // then add the contact points from the region that will be suppressed to the newly created region
            for (int i = 0; i < RegionDest.ContactPts.Count; i++)
            {
                for (int NewIdx = 0; NewIdx < RegionSource.ContactPts.Count; NewIdx++)
                {
                    cPtsInContact TmpListPtsInContact = RegionSource.ContactPts[NewIdx];
                    if (TmpListPtsInContact.IdxRegion == RegionDest.ContactPts[i].IdxRegion)
                    {
                        TmpListPtsInContact.AddRange(RegionDest.ContactPts[i]);
                        break;
                    }
                    else
                    {
                        RegionSource.ContactPts.Add(RegionDest.ContactPts[i]);
                        break;
                    }
                }
            }

            // finally find all the region containing the removed index and change it
            //foreach (cRegion TmpRegion in this)
            //{
            //    if ((TmpRegion == RegionSource) || (TmpRegion == RegionDest)) continue;
            //    foreach (cPtsInContact TmpListPtsInContact in TmpRegion.ContactPts)
            //    {
            //        if (TmpListPtsInContact.IdxRegion == RegionDest.GetIdx())
            //            TmpListPtsInContact.IdxRegion = RegionSource.GetIdx();
            //    }
            //    TmpRegion.CleanIdxPtsInContact();
            //}

            this.Remove(RegionDest);

        }
        #endregion

        #region Post-Processings
        /// <summary>
        /// PostProcessing : remove small regions
        /// </summary>
        /// <param name="MinVolume"></param>
        private void RemoveSmallRegions(int MinVolume)
        {
            //int NumRegions = this.Count;
            for (int RegionIdx = 0; RegionIdx < this.Count; RegionIdx++)
            {
                cRegion CurrentRegion = this[RegionIdx];
                if (CurrentRegion.GetVolume() <= MinVolume)
                {
                    this.RemoveRegion(CurrentRegion);
                    RegionIdx--;
                }
            }
        }

        /// <summary>
        /// Merge regions connected and sharing a common number of pixels 
        /// defined by the ratio between the volume and the pixels in contact
        /// </summary>
        /// <param name="Ratio">Ratio (Contact / Volume) (typically 3%)</param>
        public void MergeConnectedRegions(float Ratio)
        {
            //int NumRegions = this.Count;

            float NewRatio;
            int RegionRemoved = 0;

            // loop until no region is merged
            do
            {
                RegionRemoved = 0;
                for (int RegionIdx = 0; RegionIdx < this.Count; RegionIdx++)
                {
                    cRegion CurrentRegion = this[RegionIdx];

                    cListPtsInContact PtTypesInContact = CurrentRegion.ContactPts;

                    if (PtTypesInContact.Count == 0) continue;

                    // loop over all the regions in contact
                    foreach (cPtsInContact PtsInContact in PtTypesInContact)
                    {
                        NewRatio = (float)PtsInContact.Count / (float)CurrentRegion.GetVolume();
                        cRegion DestRegion = this.GetRegion(PtsInContact.IdxRegion);
                        if ((NewRatio >= Ratio) && (DestRegion != null) && (CurrentRegion != DestRegion))
                        {
                            MergeRegions(CurrentRegion, DestRegion);

                            RegionRemoved++;
                            break;
                        }
                    }
                }
            } while (RegionRemoved > 0);
        }

        /// <summary>
        /// PostProcessing : remove regions connected to a border
        /// </summary>
        private void BorderKill()
        {
            for (int RegionIdx = 0; RegionIdx < this.Count; RegionIdx++)
            {
                cRegion CurrentRegion = this[RegionIdx];
                if (CurrentRegion.IsTouchingBorder)
                {
                    this.RemoveRegion(CurrentRegion);
                    RegionIdx--;
                }
            }
        }
        #endregion
    }




}
