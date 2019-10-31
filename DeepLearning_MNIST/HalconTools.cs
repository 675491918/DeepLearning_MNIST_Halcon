using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace DeepLearning_MNIST
{
    public class HalconTools
    {
        // Procedures 

        // Local procedures
        // Welcome to change it when you need.
        #region Local procedures 
        // Local procedures, Welcome to change it when you need.
        /// <summary>
        /// 全窗口显示图像，会自动调整图像长宽比
        /// </summary>
        public void DispImageFullWindow(HObject ho_Image, HTuple hv_WindowHandle)
        {
            HTuple hv_width, hv_height;
            HOperatorSet.GetImageSize(ho_Image, out hv_width, out hv_height);
            HOperatorSet.ClearWindow(hv_WindowHandle);
            HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_height, hv_width);
            HOperatorSet.DispObj(ho_Image, hv_WindowHandle);
        }

        /// <summary>
        /// 自适应显示图像，不会改变图像长宽比
        /// </summary>
        public void DispImageAdaptively(ref HWindowControl hWindowControl, HObject ho_Image)
        {
            HOperatorSet.ClearWindow(hWindowControl.HalconWindow);
            int mW = hWindowControl.Width;
            int mH = hWindowControl.Height;
            HTuple hv_width = new HTuple(); HTuple hv_height = new HTuple();
            HOperatorSet.GetImageSize(ho_Image, out hv_width, out hv_height);
            int imageW = Convert.ToInt32(hv_width.ToString());
            int imageH = Convert.ToInt32(hv_height.ToString());
            if (mW > 0 && mH > 0)
            {
                double mScale_Window = Convert.ToDouble(mW) / Convert.ToDouble(mH);
                double mScale_Image = Convert.ToDouble(imageW) / Convert.ToDouble(imageH);
                double row1, column1, row2, column2;
                int mH_1 = Convert.ToInt32(mW / mScale_Image);
                System.Drawing.Rectangle rect = hWindowControl.ImagePart;

                if (mH_1 > mH)
                {
                    row1 = 0;
                    row2 = imageH;
                    double mImage_w = imageH * mScale_Window - imageW;
                    double mD_Image_W = Math.Abs(mImage_w / 2.0);
                    column1 = mD_Image_W;
                    column2 = imageW + mD_Image_W;

                    rect.X = -(int)Math.Round(mD_Image_W);
                    rect.Y = 0;
                    rect.Height = imageH;
                    rect.Width = (int)Math.Round(imageH * mScale_Window);
                }
                else
                {
                    column1 = 0;
                    column2 = imageW;
                    double mImage_h = Convert.ToDouble(imageW) / mScale_Window - imageH;
                    double mD_Image_H = Math.Abs(mImage_h / 2.0);
                    row1 = mD_Image_H;
                    row2 = imageH + mD_Image_H;

                    rect.X = 0;
                    rect.Y = -(int)Math.Round(mD_Image_H);
                    rect.Height = (int)Math.Round(Convert.ToDouble(imageW) / mScale_Window);
                    rect.Width = imageW;
                }
                hWindowControl.ImagePart = rect;
            }
            HOperatorSet.DispObj(ho_Image, hWindowControl.HalconWindow);
        }

        public void check_output_file_names_for_duplicates(HTuple hv_RawImageFiles, HTuple hv_ObjectFilesOut)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_CheckOutputFiles = new HTuple();
            HTuple hv_SortedImageFiles = new HTuple(), hv_I = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                hv_CheckOutputFiles.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_CheckOutputFiles = ((hv_ObjectFilesOut.TupleSort()
                        )).TupleUniq();
                }
                if ((int)(new HTuple((new HTuple(hv_CheckOutputFiles.TupleLength())).TupleNotEqual(
                    new HTuple(hv_RawImageFiles.TupleLength())))) != 0)
                {
                    hv_SortedImageFiles.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_SortedImageFiles = hv_ObjectFilesOut.TupleSort()
                            ;
                    }
                    for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_SortedImageFiles.TupleLength()
                        )) - 1); hv_I = (int)hv_I + 1)
                    {
                        if ((int)(new HTuple(((hv_SortedImageFiles.TupleSelect(hv_I))).TupleNotEqual(
                            hv_CheckOutputFiles.TupleSelect(hv_I)))) != 0)
                        {
                            throw new HalconException("Error some file(s) have the same output filenames: " + (hv_SortedImageFiles.TupleSelect(
                                hv_I)));
                        }
                    }
                }

                hv_CheckOutputFiles.Dispose();
                hv_SortedImageFiles.Dispose();
                hv_I.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_CheckOutputFiles.Dispose();
                hv_SortedImageFiles.Dispose();
                hv_I.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void dev_disp_error_text(HTuple hv_Exception)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_ErrorAndAdviceText = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                hv_ErrorAndAdviceText.Dispose();
                hv_ErrorAndAdviceText = "An error occurred during runtime initialization.";
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                            "");
                        hv_ErrorAndAdviceText.Dispose();
                        hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                            ((("Error " + (hv_Exception.TupleSelect(0))) + ": '") + (hv_Exception.TupleSelect(
                            2))) + "'");
                        hv_ErrorAndAdviceText.Dispose();
                        hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                    }
                }
                if ((int)(new HTuple(((hv_Exception.TupleSelect(0))).TupleEqual(4104))) != 0)
                {
                    //In case of out of device memory we can give advice.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                                "");
                            hv_ErrorAndAdviceText.Dispose();
                            hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                                "Install a GPU with more RAM or reduce the batch size.");
                            hv_ErrorAndAdviceText.Dispose();
                            hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                                "");
                            hv_ErrorAndAdviceText.Dispose();
                            hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                                "Note that changing the batch size will have an influence on the results.");
                            hv_ErrorAndAdviceText.Dispose();
                            hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                        }
                    }
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                }
                //Display text with line breaks after 60 characters.
                //if (HDevWindowStack.IsOpen())
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.DispText(HDevWindowStack.GetActive(), ((hv_ErrorAndAdviceText + " ")).TupleRegexpReplace(
                            (new HTuple("(.{0,60})\\s")).TupleConcat("replace_all"), "$1\n"), "window",
                            "center", "left", "red", new HTuple(), new HTuple());
                    }
                }

                hv_ErrorAndAdviceText.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_ErrorAndAdviceText.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void dev_disp_inference_text(HTuple hv_Runtime, HTuple hv_WindowHandle)
        {

            HTuple hv_Text = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                hv_Text.Dispose();
                hv_Text = "INFERENCE";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "This part of the program is a brief introduction on how to ";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "make use of your trained classifier. ";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "It is important that the same preprocessing as for the training ";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "of the classifier is applied to the raw images. ";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "";
                if ((int)(new HTuple(hv_Runtime.TupleEqual("cpu"))) != 0)
                {
                    hv_Text[new HTuple(hv_Text.TupleLength())] = "The 'cpu' runtime has been selected for inference.";
                }
                //
                HOperatorSet.ClearWindow(hv_WindowHandle);
                HOperatorSet.DispText(hv_WindowHandle, hv_Text, "window", "top", "left", "white", "box", "false");
                HOperatorSet.DispText(hv_WindowHandle, "The program will continue in 5 seconds",
                    "window", "bottom", "right", "black", new HTuple(), new HTuple());

                hv_Text.Dispose();
                HOperatorSet.WaitSeconds(5);
                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Text.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void dev_disp_missing_classifier_text()
        {


            // Local iconic variables 

            // Local control variables 

            HTuple hv_ErrorAndAdviceText = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                hv_ErrorAndAdviceText.Dispose();
                hv_ErrorAndAdviceText = "The classifier required for this example could not be found.";
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                            "");
                        hv_ErrorAndAdviceText.Dispose();
                        hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                            "This classifier is part of a separate installer. Please");
                        hv_ErrorAndAdviceText.Dispose();
                        hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                            "refer to the Installation Guide for more information on");
                        hv_ErrorAndAdviceText.Dispose();
                        hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                            "this topic!");
                        hv_ErrorAndAdviceText.Dispose();
                        hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                    }
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), hv_ErrorAndAdviceText,
                        "window", "center", "left", "red", new HTuple(), new HTuple());
                }

                hv_ErrorAndAdviceText.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_ErrorAndAdviceText.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void dev_disp_missing_images_text()
        {


            // Local iconic variables 

            // Local control variables 

            HTuple hv_ErrorAndAdviceText = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                hv_ErrorAndAdviceText.Dispose();
                hv_ErrorAndAdviceText = "The images required for this example could not be found.";
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                            "");
                        hv_ErrorAndAdviceText.Dispose();
                        hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                            "These images are part of a separate installer. Please");
                        hv_ErrorAndAdviceText.Dispose();
                        hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                            "refer to the Installation Guide for more information on");
                        hv_ErrorAndAdviceText.Dispose();
                        hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ErrorAndAdviceText = hv_ErrorAndAdviceText.TupleConcat(
                            "this topic!");
                        hv_ErrorAndAdviceText.Dispose();
                        hv_ErrorAndAdviceText = ExpTmpLocalVar_ErrorAndAdviceText;
                    }
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), hv_ErrorAndAdviceText,
                        "window", "center", "left", "red", new HTuple(), new HTuple());
                }

                hv_ErrorAndAdviceText.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_ErrorAndAdviceText.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void dev_disp_preprocessing_progress(HTuple hv_I, HTuple hv_RawImageFiles,
            HTuple hv_PreprocessedFolder, HTuple hv_WindowHandle)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Text = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                if ((int)(new HTuple(((hv_I % 10)).TupleEqual(9))) != 0)
                {
                    HOperatorSet.SetWindowParam(hv_WindowHandle, "flush", "false");
                    //if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                    }
                    hv_Text.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Text = ((("Preprocessing the data set... (" + (hv_I + 1)) + " of ") + (new HTuple(hv_RawImageFiles.TupleLength()
                            ))) + ")";
                    }
                    if (hv_Text == null)
                        hv_Text = new HTuple();
                    hv_Text[new HTuple(hv_Text.TupleLength())] = "";
                    if (hv_Text == null)
                        hv_Text = new HTuple();
                    hv_Text[new HTuple(hv_Text.TupleLength())] = "The preprocessed images are written into the folder";
                    if (hv_Text == null)
                        hv_Text = new HTuple();
                    hv_Text[new HTuple(hv_Text.TupleLength())] = ("'" + hv_PreprocessedFolder) + new HTuple("',");
                    if (hv_Text == null)
                        hv_Text = new HTuple();
                    hv_Text[new HTuple(hv_Text.TupleLength())] = "as specified by the variable PreprocessedFolder.";
                    if (hv_Text == null)
                        hv_Text = new HTuple();
                    hv_Text[new HTuple(hv_Text.TupleLength())] = "The preprocessed images will be deleted automatically";
                    if (hv_Text == null)
                        hv_Text = new HTuple();
                    hv_Text[new HTuple(hv_Text.TupleLength())] = "at the end of the program. ";
                    //if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispText(HDevWindowStack.GetActive(), hv_Text, "window", "top",
                            "left", "white", "box", "false");
                    }
                    HOperatorSet.FlushBuffer(hv_WindowHandle);
                    HOperatorSet.SetWindowParam(hv_WindowHandle, "flush", "true");
                }

                hv_Text.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Text.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void get_predicted_classes(HTuple hv_Images, HTuple hv_DLClassifierHandle,
            out HTuple hv_Top1PredictedClasses)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_DLClassifierResultIDsTest = new HTuple();
            HTuple hv_Index = new HTuple();

            HTupleVector hvec_PredictedClasses = new HTupleVector(1);
            HTupleVector hvec_Confidences = new HTupleVector(1);
            // Initialize local and output iconic variables 
            hv_Top1PredictedClasses = new HTuple();
            try
            {
                hv_DLClassifierResultIDsTest.Dispose(); hvec_PredictedClasses.Dispose(); hvec_Confidences.Dispose();
                apply_dl_classifier_batchwise(hv_Images, hv_DLClassifierHandle, out hv_DLClassifierResultIDsTest,
                    out hvec_PredictedClasses, out hvec_Confidences);
                //
                hv_Top1PredictedClasses.Dispose();
                hv_Top1PredictedClasses = new HTuple();
                HTuple end_val3 = new HTuple(hvec_PredictedClasses.Length) - 1;
                HTuple step_val3 = 1;
                for (hv_Index = 0; hv_Index.Continue(end_val3, step_val3); hv_Index = hv_Index.TupleAdd(step_val3))
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Top1PredictedClasses = hv_Top1PredictedClasses.TupleConcat(
                                (hvec_PredictedClasses[hv_Index].T).TupleSelect(0));
                            hv_Top1PredictedClasses.Dispose();
                            hv_Top1PredictedClasses = ExpTmpLocalVar_Top1PredictedClasses;
                        }
                    }
                }

                hv_DLClassifierResultIDsTest.Dispose();
                hv_Index.Dispose();
                hvec_PredictedClasses.Dispose();
                hvec_Confidences.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_DLClassifierResultIDsTest.Dispose();
                hv_Index.Dispose();
                hvec_PredictedClasses.Dispose();
                hvec_Confidences.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void dev_disp_introduction_text(HTuple hv_WindowHandle)
        {


            // Local iconic variables 

            // Local control variables 

            HTuple hv_Text = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_DLClassifierHandle = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                hv_Text.Dispose();
                hv_Text = "This example program demonstrates how to train a deep learning classifier ";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "capable of distinguishing different types of fruit. ";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "";
                hv_Text[new HTuple(hv_Text.TupleLength())] = new HTuple("Here, we only give a brief overview of the necessary steps. For more ");
                hv_Text[new HTuple(hv_Text.TupleLength())] = new HTuple("detailed explanations, please have a look at the example ");
                hv_Text[new HTuple(hv_Text.TupleLength())] = "classifiy_pill_defects_deep_learning.hdev. ";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "";
                hv_Text[new HTuple(hv_Text.TupleLength())] = "You need a compatible GPU to run this example (see system requirements).";
                //

                HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                dev_resize_window_fit_size(0, 0, 1024, 240, -1, -1);
                HOperatorSet.DispText(hv_WindowHandle, hv_Text, "window", "top", "left", "white", "box", "false");
                //Display example images,
                //display a warning in case the images are not found.
                try
                {
                    //dev_disp_images_of_classes();
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    if ((int)(new HTuple(((hv_Exception.TupleSelect(0))).TupleEqual(5200))) != 0)
                    {
                        dev_disp_missing_images_text();
                        // stop(...); only in hdevelop
                    }
                    else
                    {
                        throw new HalconException(hv_Exception);
                    }
                }
                //Check if the runtime can be initialized.
                try
                {
                    hv_DLClassifierHandle.Dispose();
                    HOperatorSet.ReadDlClassifier("pretrained_dl_classifier_compact.hdl", out hv_DLClassifierHandle);
                    HOperatorSet.SetDlClassifierParam(hv_DLClassifierHandle, "batch_size", 1);
                    HOperatorSet.SetDlClassifierParam(hv_DLClassifierHandle, "runtime_init",
                        "immediately");
                }
                // catch (Exception) 
                catch (HalconException HDevExpDefaultException1)
                {
                    HDevExpDefaultException1.ToHTuple(out hv_Exception);
                    if ((int)(new HTuple(((hv_Exception.TupleSelect(0))).TupleEqual(5200))) != 0)
                    {
                        dev_disp_missing_classifier_text();
                        // stop(...); only in hdevelop
                    }
                    else
                    {
                        dev_disp_error_text(hv_Exception);
                    }
                }

                hv_Text.Dispose();
                hv_Exception.Dispose();
                hv_DLClassifierHandle.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Text.Dispose();
                hv_Exception.Dispose();
                hv_DLClassifierHandle.Dispose();

                throw HDevExpDefaultException;
            }
        }

        public void dev_disp_images_of_classes()
        {


            // Local iconic variables 

            HObject ho_Image, ho_TiledImage;

            // Local control variables 

            HTuple hv_FileName = new HTuple(), hv_WindowHandle1 = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_TiledImage);
            try
            {
                hv_FileName.Dispose();
                hv_FileName = new HTuple();
                hv_FileName[0] = "food/apple_braeburn/apple_braeburn_002";
                hv_FileName[1] = "food/apple_golden_delicious/apple_golden_delicious_001";
                hv_FileName[2] = "food/apple_topaz/apple_topaz_002";
                hv_FileName[3] = "food/peach/peach_001";
                hv_FileName[4] = "food/pear/pear_001";

                ho_Image.Dispose();
                HOperatorSet.ReadImage(out ho_Image, hv_FileName);
                ho_TiledImage.Dispose();
                HOperatorSet.TileImages(ho_Image, out ho_TiledImage, 3, "horizontal");
                //
                hv_WindowHandle1.Dispose();
                dev_open_window_fit_image(ho_TiledImage, 245, 0, -1, 300, out hv_WindowHandle1);
                set_display_font(hv_WindowHandle1, 14, "mono", "true", "false");
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispObj(ho_TiledImage, HDevWindowStack.GetActive());
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), "Apple Braeburn", "window",
                        "top", "left", "black", new HTuple(), new HTuple());
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), "Apple Golden Delicious",
                        "window", "top", "center", "black", new HTuple(), new HTuple());
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), "Apple Topaz", "window",
                        "top", "right", "black", new HTuple(), new HTuple());
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), "Peach", "window", "center",
                        "left", "black", new HTuple(), new HTuple());
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), "Pear", "window", "center",
                        "center", "black", new HTuple(), new HTuple());
                }
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.DispText(HDevWindowStack.GetActive(), "Press Run (F5) to continue",
                        "window", "bottom", "right", "black", new HTuple(), new HTuple());
                }
                ho_Image.Dispose();
                ho_TiledImage.Dispose();

                hv_FileName.Dispose();
                hv_WindowHandle1.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Image.Dispose();
                ho_TiledImage.Dispose();

                hv_FileName.Dispose();
                hv_WindowHandle1.Dispose();

                throw HDevExpDefaultException;
            }
        }

        #endregion

        // External procedures
        //Do not change unless you are good at it. 
        #region External procedures
        // External procedures, Do not change unless you are good at it. 

        // Chapter: Image / Manipulation
        public static void apply_brightness_variation_spot(HObject ho_Image, out HObject ho_ImageSpot,
            HTuple hv_SpotSize, HTuple hv_SpotRow, HTuple hv_SpotColumn, HTuple hv_BrightnessVariation)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_Filter, ho_GaussImage, ho_GaussFilter;
            HObject ho_Gauss, ho_GaussTargetType, ho_AddImage;

            // Local control variables 

            HTuple hv_Direction = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_ShiftRow = new HTuple();
            HTuple hv_ShiftCol = new HTuple(), hv_Type = new HTuple();
            HTuple hv_NChannels = new HTuple(), hv_Index1 = new HTuple();
            HTuple hv_BrightnessVariation_COPY_INP_TMP = new HTuple(hv_BrightnessVariation);

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImageSpot);
            HOperatorSet.GenEmptyObj(out ho_Filter);
            HOperatorSet.GenEmptyObj(out ho_GaussImage);
            HOperatorSet.GenEmptyObj(out ho_GaussFilter);
            HOperatorSet.GenEmptyObj(out ho_Gauss);
            HOperatorSet.GenEmptyObj(out ho_GaussTargetType);
            HOperatorSet.GenEmptyObj(out ho_AddImage);
            try
            {
                //This procedure applies a brightness spot
                //of a given intensity and size at a given location
                //to the input image.
                //The modified image is returned in ImageSpot.
                //
                if ((int)(new HTuple(hv_BrightnessVariation_COPY_INP_TMP.TupleLess(0))) != 0)
                {
                    hv_Direction.Dispose();
                    hv_Direction = 0;
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_BrightnessVariation = -hv_BrightnessVariation_COPY_INP_TMP;
                            hv_BrightnessVariation_COPY_INP_TMP.Dispose();
                            hv_BrightnessVariation_COPY_INP_TMP = ExpTmpLocalVar_BrightnessVariation;
                        }
                    }
                }
                else
                {
                    hv_Direction.Dispose();
                    hv_Direction = 1;
                }
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                //Generate Gauss filter that simulates an illumination spot of size 'SpotSize'.
                ho_Filter.Dispose();
                HOperatorSet.GenGaussFilter(out ho_Filter, 1, 1, 0, "none", "dc_center", hv_SpotSize,
                    hv_SpotSize);
                //Shift the filter image to the given position.
                hv_ShiftRow.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ShiftRow = -((hv_SpotSize / 2) - hv_SpotRow);
                }
                hv_ShiftCol.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ShiftCol = -((hv_SpotSize / 2) - hv_SpotColumn);
                }
                ho_GaussImage.Dispose();
                HOperatorSet.TileImagesOffset(ho_Filter, out ho_GaussImage, hv_ShiftRow, hv_ShiftCol,
                    -1, -1, -1, -1, hv_Width, hv_Height);
                ho_GaussFilter.Dispose();
                HOperatorSet.FullDomain(ho_GaussImage, out ho_GaussFilter);
                //Convert Gauss filter to target image type and apply brightness variation.
                hv_Type.Dispose();
                HOperatorSet.GetImageType(ho_Image, out hv_Type);
                ho_Gauss.Dispose();
                HOperatorSet.ScaleImage(ho_GaussFilter, out ho_Gauss, hv_BrightnessVariation_COPY_INP_TMP,
                    0);
                ho_GaussTargetType.Dispose();
                HOperatorSet.ConvertImageType(ho_Gauss, out ho_GaussTargetType, hv_Type);
                //Add channels to fit input image.
                hv_NChannels.Dispose();
                HOperatorSet.CountChannels(ho_Image, out hv_NChannels);
                ho_AddImage.Dispose();
                HOperatorSet.CopyObj(ho_GaussTargetType, out ho_AddImage, 1, 1);
                HTuple end_val26 = hv_NChannels - 1;
                HTuple step_val26 = 1;
                for (hv_Index1 = 1; hv_Index1.Continue(end_val26, step_val26); hv_Index1 = hv_Index1.TupleAdd(step_val26))
                {
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.AppendChannel(ho_AddImage, ho_GaussTargetType, out ExpTmpOutVar_0
                            );
                        ho_AddImage.Dispose();
                        ho_AddImage = ExpTmpOutVar_0;
                    }
                }
                //Apply on image.
                if ((int)(hv_Direction) != 0)
                {
                    ho_ImageSpot.Dispose();
                    HOperatorSet.AddImage(ho_Image, ho_AddImage, out ho_ImageSpot, 1, 0);
                }
                else
                {
                    ho_ImageSpot.Dispose();
                    HOperatorSet.SubImage(ho_Image, ho_AddImage, out ho_ImageSpot, 1, 0);
                }
                ho_Filter.Dispose();
                ho_GaussImage.Dispose();
                ho_GaussFilter.Dispose();
                ho_Gauss.Dispose();
                ho_GaussTargetType.Dispose();
                ho_AddImage.Dispose();

                hv_BrightnessVariation_COPY_INP_TMP.Dispose();
                hv_Direction.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_ShiftRow.Dispose();
                hv_ShiftCol.Dispose();
                hv_Type.Dispose();
                hv_NChannels.Dispose();
                hv_Index1.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Filter.Dispose();
                ho_GaussImage.Dispose();
                ho_GaussFilter.Dispose();
                ho_Gauss.Dispose();
                ho_GaussTargetType.Dispose();
                ho_AddImage.Dispose();

                hv_BrightnessVariation_COPY_INP_TMP.Dispose();
                hv_Direction.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_ShiftRow.Dispose();
                hv_ShiftCol.Dispose();
                hv_Type.Dispose();
                hv_NChannels.Dispose();
                hv_Index1.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Deep Learning / Classification
        // Short Description: Return the classification results for the given images. 
        public static void apply_dl_classifier_batchwise(HTuple hv_ImageFiles, HTuple hv_DLClassifierHandle,
            out HTuple hv_DLClassifierResultIDs, out HTupleVector/*{eTupleVector,Dim=1}*/ hvec_PredictedClasses,
            out HTupleVector/*{eTupleVector,Dim=1}*/ hvec_Confidences)
        {



            // Local iconic variables 

            HObject ho_BatchImages = null;

            // Local control variables 

            HTuple hv_BatchSize = new HTuple(), hv_NumImages = new HTuple();
            HTuple hv_Sequence = new HTuple(), hv_BatchStartIndex = new HTuple();
            HTuple hv_BatchIndices = new HTuple(), hv_BatchImageFiles = new HTuple();
            HTuple hv_DLClassifierResultID = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_NumImagesInBatch = new HTuple(), hv_Index = new HTuple();
            HTuple hv_PredictedClass = new HTuple(), hv_ClassConfidence = new HTuple();
            HTuple hv_VectorIndex = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_BatchImages);
            hv_DLClassifierResultIDs = new HTuple();
            hvec_PredictedClasses = new HTupleVector(1);
            hvec_Confidences = new HTupleVector(1);
            try
            {
                //This procedure classifies the images given as paths
                //by ImageFiles using the operator apply_dl_classifier.
                //To avoid that the main memory is overloaded, the images
                //are classified batchwise, according to the hyperparameter 'batch_size',
                //which is stored in the DLClassifierHandle.
                //As result, the classification result handles for every batch
                //are returned in DLClassifierResultIDs.
                //Additionally, for every image the descending sorted
                //Confidences and corresponding PredictedClasses
                //are returned as vectors.
                //
                //Get batch size from handle.
                hv_BatchSize.Dispose();
                HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "batch_size", out hv_BatchSize);
                //
                //Check the input parameters.
                if ((int)(new HTuple((new HTuple(hv_ImageFiles.TupleLength())).TupleLess(1))) != 0)
                {
                    throw new HalconException("ImageFiles must not be empty.");
                }
                //
                //Sequence is used for easier indexing of the images.
                hv_NumImages.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumImages = new HTuple(hv_ImageFiles.TupleLength()
                        );
                }
                hv_Sequence.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Sequence = HTuple.TupleGenSequence(
                        0, hv_NumImages - 1, 1);
                }
                //
                //Loop through all selected images.
                hvec_PredictedClasses.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hvec_PredictedClasses = dh.Take(dh.Add(new HTupleVector(1)));
                }
                hvec_Confidences.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hvec_Confidences = dh.Take(dh.Add(new HTupleVector(1)));
                }
                hv_DLClassifierResultIDs.Dispose();
                hv_DLClassifierResultIDs = new HTuple();
                HTuple end_val27 = hv_NumImages - 1;
                HTuple step_val27 = hv_BatchSize;
                for (hv_BatchStartIndex = 0; hv_BatchStartIndex.Continue(end_val27, step_val27); hv_BatchStartIndex = hv_BatchStartIndex.TupleAdd(step_val27))
                {
                    //Select the data for the current batch.
                    hv_BatchIndices.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_BatchIndices = hv_Sequence.TupleSelectRange(
                            hv_BatchStartIndex, (((hv_BatchStartIndex + hv_BatchSize) - 1)).TupleMin2(
                            hv_NumImages - 1));
                    }
                    hv_BatchImageFiles.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_BatchImageFiles = hv_ImageFiles.TupleSelect(
                            hv_BatchIndices);
                    }
                    //Read the current batch images.
                    ho_BatchImages.Dispose();
                    HOperatorSet.ReadImage(out ho_BatchImages, hv_BatchImageFiles);
                    //Apply the classifier for this batch.
                    try
                    {
                        hv_DLClassifierResultID.Dispose();
                        HOperatorSet.ApplyDlClassifier(ho_BatchImages, hv_DLClassifierHandle, out hv_DLClassifierResultID);
                    }
                    // catch (Exception) 
                    catch (HalconException HDevExpDefaultException1)
                    {
                        HDevExpDefaultException1.ToHTuple(out hv_Exception);
                        if ((int)(((((hv_Exception.TupleSelect(0))).TupleEqualElem(((((new HTuple(2106)).TupleConcat(
                            2107)).TupleConcat(3122)).TupleConcat(9001)).TupleConcat(9003)))).TupleSum()
                            ) != 0)
                        {
                            throw new HalconException(new HTuple("Images need to fulfill the network requirements, please provide preprocessed images."));
                        }
                        else
                        {
                            throw new HalconException(hv_Exception);
                        }
                    }
                    //Get results from result handle.
                    hv_NumImagesInBatch.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_NumImagesInBatch = new HTuple(hv_BatchImageFiles.TupleLength()
                            );
                    }
                    HTuple end_val45 = hv_NumImagesInBatch - 1;
                    HTuple step_val45 = 1;
                    for (hv_Index = 0; hv_Index.Continue(end_val45, step_val45); hv_Index = hv_Index.TupleAdd(step_val45))
                    {
                        hv_PredictedClass.Dispose();
                        HOperatorSet.GetDlClassifierResult(hv_DLClassifierResultID, hv_Index, "predicted_classes",
                            out hv_PredictedClass);
                        hv_ClassConfidence.Dispose();
                        HOperatorSet.GetDlClassifierResult(hv_DLClassifierResultID, hv_Index, "confidences",
                            out hv_ClassConfidence);
                        //Store the classification results.
                        hv_VectorIndex.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_VectorIndex = hv_BatchStartIndex + hv_Index;
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hvec_PredictedClasses[hv_VectorIndex] = dh.Add(new HTupleVector(hv_PredictedClass));
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hvec_Confidences[hv_VectorIndex] = dh.Add(new HTupleVector(hv_ClassConfidence));
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_DLClassifierResultIDs = hv_DLClassifierResultIDs.TupleConcat(
                                hv_DLClassifierResultID);
                            hv_DLClassifierResultIDs.Dispose();
                            hv_DLClassifierResultIDs = ExpTmpLocalVar_DLClassifierResultIDs;
                        }
                    }
                }
                ho_BatchImages.Dispose();

                hv_BatchSize.Dispose();
                hv_NumImages.Dispose();
                hv_Sequence.Dispose();
                hv_BatchStartIndex.Dispose();
                hv_BatchIndices.Dispose();
                hv_BatchImageFiles.Dispose();
                hv_DLClassifierResultID.Dispose();
                hv_Exception.Dispose();
                hv_NumImagesInBatch.Dispose();
                hv_Index.Dispose();
                hv_PredictedClass.Dispose();
                hv_ClassConfidence.Dispose();
                hv_VectorIndex.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_BatchImages.Dispose();

                hv_BatchSize.Dispose();
                hv_NumImages.Dispose();
                hv_Sequence.Dispose();
                hv_BatchStartIndex.Dispose();
                hv_BatchIndices.Dispose();
                hv_BatchImageFiles.Dispose();
                hv_DLClassifierResultID.Dispose();
                hv_Exception.Dispose();
                hv_NumImagesInBatch.Dispose();
                hv_Index.Dispose();
                hv_PredictedClass.Dispose();
                hv_ClassConfidence.Dispose();
                hv_VectorIndex.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Image / Manipulation
        // Short Description: Augment/distort the given images. 
        public static void augment_images(HObject ho_Images, out HObject ho_ImagesAugmented,
            HTuple hv_GenParamName, HTuple hv_GenParamValue)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ImageSelected = null, ho_ImagePart = null;
            HObject ho_ImageRotated = null, ho_DomainRotated = null, ho_ImageScaled = null;
            HObject ho_ImageSpot = null;

            // Local control variables 

            HTuple hv_AugmentationPercentage = new HTuple();
            HTuple hv_CropPercentage = new HTuple(), hv_CropPixel = new HTuple();
            HTuple hv_Rotation = new HTuple(), hv_RotationRange = new HTuple();
            HTuple hv_Mirror = new HTuple(), hv_BrightnessVariation = new HTuple();
            HTuple hv_BrightnessVariationSpot = new HTuple(), hv_GenParamIndex = new HTuple();
            HTuple hv_CurrentParamName = new HTuple(), hv_CurrentParamValue = new HTuple();
            HTuple hv_NumAvailableDistortions = new HTuple(), hv_NumImages = new HTuple();
            HTuple hv_AugmentationRate = new HTuple(), hv_NumAugmentations = new HTuple();
            HTuple hv_ImageIndices = new HTuple(), hv_SelectedImages = new HTuple();
            HTuple hv_SelectedDistortions = new HTuple(), hv_IndexDistortion = new HTuple();
            HTuple hv_Index = new HTuple(), hv_ImageIndex = new HTuple();
            HTuple hv_CurrentDistortion = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_CropRate = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column1 = new HTuple(), hv_Column2 = new HTuple();
            HTuple hv_Length = new HTuple(), hv_RotationStep = new HTuple();
            HTuple hv_NumPossibleRotations = new HTuple(), hv_CurrentRotation = new HTuple();
            HTuple hv_HomMat2DIdentity = new HTuple(), hv_HomMat2DRotate = new HTuple();
            HTuple hv_NumMirrorMethods = new HTuple(), hv_PropabilityMethods = new HTuple();
            HTuple hv_StrMirror = new HTuple(), hv_StrIdx = new HTuple();
            HTuple hv_SelectedChar = new HTuple(), hv_BrightnessVariationValue = new HTuple();
            HTuple hv_SpotSize = new HTuple(), hv_SpotRow = new HTuple();
            HTuple hv_SpotColumn = new HTuple();

            HTupleVector hvec_AvailableDistortions = new HTupleVector(1);
            HTupleVector hvec_Distortions = new HTupleVector(1);
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImagesAugmented);
            HOperatorSet.GenEmptyObj(out ho_ImageSelected);
            HOperatorSet.GenEmptyObj(out ho_ImagePart);
            HOperatorSet.GenEmptyObj(out ho_ImageRotated);
            HOperatorSet.GenEmptyObj(out ho_DomainRotated);
            HOperatorSet.GenEmptyObj(out ho_ImageScaled);
            HOperatorSet.GenEmptyObj(out ho_ImageSpot);
            try
            {
                //This procedure can be used to augment given input Images
                //using different methods, which can be specified using
                //GenParamName and GenParamValue. The augmented images are returned
                //in ImagesAugmented.
                //
                //Set default parameters.
                //
                //The percentages of the images that are to be augmented.
                hv_AugmentationPercentage.Dispose();
                hv_AugmentationPercentage = new HTuple();
                hv_AugmentationPercentage[0] = "augmentation_percentage";
                hv_AugmentationPercentage[1] = 50;
                //Fraction of image length and width that remains after cropping (in %).
                hv_CropPercentage.Dispose();
                hv_CropPercentage = new HTuple();
                hv_CropPercentage[0] = "crop_percentage";
                hv_CropPercentage[1] = "off";
                //Image length and width that remains after cropping (in pixel).
                hv_CropPixel.Dispose();
                hv_CropPixel = new HTuple();
                hv_CropPixel[0] = "crop_pixel";
                hv_CropPixel[1] = "off";
                //Step size for possible rotations.
                hv_Rotation.Dispose();
                hv_Rotation = new HTuple();
                hv_Rotation[0] = "rotate";
                hv_Rotation[1] = 0;
                //Step range for rotations with step size 1.
                hv_RotationRange.Dispose();
                hv_RotationRange = new HTuple();
                hv_RotationRange[0] = "rotate_range";
                hv_RotationRange[1] = 0;
                //Allowed mirroring methods coded by 'r' (row), 'c' (column).
                hv_Mirror.Dispose();
                hv_Mirror = new HTuple();
                hv_Mirror[0] = "mirror";
                hv_Mirror[1] = "off";
                //The absolute brightness change can vary in the range[-value, +value].
                hv_BrightnessVariation.Dispose();
                hv_BrightnessVariation = new HTuple();
                hv_BrightnessVariation[0] = "brightness_variation";
                hv_BrightnessVariation[1] = 0;
                //The absolute brightness peak of a randomly positioned spot can vary in the range[-value, +value].
                hv_BrightnessVariationSpot.Dispose();
                hv_BrightnessVariationSpot = new HTuple();
                hv_BrightnessVariationSpot[0] = "brightness_variation_spot";
                hv_BrightnessVariationSpot[1] = 0;
                //
                //Parse the generic parameters.
                //
                //Check if GenParamName matches GenParamValue.
                if ((int)(new HTuple((new HTuple(hv_GenParamName.TupleLength())).TupleNotEqual(
                    new HTuple(hv_GenParamValue.TupleLength())))) != 0)
                {
                    throw new HalconException("Number of generic parameters does not match number of generic parameter values");
                }
                //Check for generic parameter names and overwrite defaults.
                for (hv_GenParamIndex = 0; (int)hv_GenParamIndex <= (int)((new HTuple(hv_GenParamName.TupleLength()
                    )) - 1); hv_GenParamIndex = (int)hv_GenParamIndex + 1)
                {
                    hv_CurrentParamName.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_CurrentParamName = hv_GenParamName.TupleSelect(
                            hv_GenParamIndex);
                    }
                    hv_CurrentParamValue.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_CurrentParamValue = hv_GenParamValue.TupleSelect(
                            hv_GenParamIndex);
                    }
                    //
                    if ((int)(new HTuple(hv_CurrentParamName.TupleEqual(hv_AugmentationPercentage.TupleSelect(
                        0)))) != 0)
                    {
                        //Set augmentation percentage.
                        if (hv_AugmentationPercentage == null)
                            hv_AugmentationPercentage = new HTuple();
                        hv_AugmentationPercentage[1] = hv_CurrentParamValue;
                        //Check if input value is in range of 1-100 %.
                        if ((int)(hv_CurrentParamValue.TupleIsNumber()) != 0)
                        {
                            if ((int)((new HTuple(hv_CurrentParamValue.TupleLess(1))).TupleOr(new HTuple(hv_CurrentParamValue.TupleGreater(
                                100)))) != 0)
                            {
                                throw new HalconException("The given value for augmentation_percentage has to be in the range 1-100.");
                            }
                        }
                        else
                        {
                            throw new HalconException("The given value for augmentation_percentage has to be in the range 1-100.");
                        }
                    }
                    else if ((int)(new HTuple(hv_CurrentParamName.TupleEqual(hv_Rotation.TupleSelect(
                        0)))) != 0)
                    {
                        //Set rotation.
                        if (hv_Rotation == null)
                            hv_Rotation = new HTuple();
                        hv_Rotation[1] = hv_CurrentParamValue;
                        //Check if input value is in range of 0-180 deg.
                        if ((int)(hv_CurrentParamValue.TupleIsNumber()) != 0)
                        {
                            if ((int)((new HTuple(hv_CurrentParamValue.TupleLess(0))).TupleOr(new HTuple(hv_CurrentParamValue.TupleGreater(
                                180)))) != 0)
                            {
                                throw new HalconException("The given value for rotate has to be in the range 0-180.");
                            }
                        }
                        else
                        {
                            throw new HalconException("The given value for rotate has to be in the range 0-180.");
                        }
                    }
                    else if ((int)(new HTuple(hv_CurrentParamName.TupleEqual(hv_RotationRange.TupleSelect(
                        0)))) != 0)
                    {
                        //Set rotation.
                        if (hv_RotationRange == null)
                            hv_RotationRange = new HTuple();
                        hv_RotationRange[1] = hv_CurrentParamValue;
                        //Check if input value is in range of 0-180 deg.
                        if ((int)(hv_CurrentParamValue.TupleIsNumber()) != 0)
                        {
                            if ((int)((new HTuple(hv_CurrentParamValue.TupleLess(0))).TupleOr(new HTuple(hv_CurrentParamValue.TupleGreater(
                                180)))) != 0)
                            {
                                throw new HalconException("The given value for rotate_range has to be in the range 0-180.\"");
                            }
                        }
                        else
                        {
                            throw new HalconException("The given value for rotate_range has to be in the range 0-180.");
                        }
                    }
                    else if ((int)(new HTuple(hv_CurrentParamName.TupleEqual(hv_Mirror.TupleSelect(
                        0)))) != 0)
                    {
                        //Set mirroring.
                        if (hv_Mirror == null)
                            hv_Mirror = new HTuple();
                        hv_Mirror[1] = hv_CurrentParamValue;
                        //Check if input is string and is 'off' or contains the wanted strings.
                        if ((int)(((hv_CurrentParamValue.TupleIsNumber())).TupleOr((new HTuple((new HTuple((new HTuple((new HTuple((new HTuple(hv_CurrentParamValue.TupleEqual(
                            "off"))).TupleOr(new HTuple(hv_CurrentParamValue.TupleEqual("c"))))).TupleOr(
                            new HTuple(hv_CurrentParamValue.TupleEqual("r"))))).TupleOr(new HTuple(hv_CurrentParamValue.TupleEqual(
                            "cr"))))).TupleOr(new HTuple(hv_CurrentParamValue.TupleEqual("rc"))))).TupleNot()
                            )) != 0)
                        {
                            throw new HalconException("Unknown mirror method.");
                        }
                    }
                    else if ((int)(new HTuple(hv_CurrentParamName.TupleEqual(hv_CropPercentage.TupleSelect(
                        0)))) != 0)
                    {
                        //Set cropping with percentage.
                        if (hv_CropPercentage == null)
                            hv_CropPercentage = new HTuple();
                        hv_CropPercentage[1] = hv_CurrentParamValue;
                        //Check if input value is in range of 1-100 %.
                        if ((int)(hv_CurrentParamValue.TupleIsNumber()) != 0)
                        {
                            if ((int)((new HTuple(hv_CurrentParamValue.TupleLess(1))).TupleOr(new HTuple(hv_CurrentParamValue.TupleGreater(
                                100)))) != 0)
                            {
                                throw new HalconException("The given value for crop_percentage has to be in the range 1-100.");
                            }
                        }
                        else
                        {
                            throw new HalconException("The given value for crop_percentage has to be in the range 1-100.");
                        }
                    }
                    else if ((int)(new HTuple(hv_CurrentParamName.TupleEqual(hv_CropPixel.TupleSelect(
                        0)))) != 0)
                    {
                        //Set cropping with pixels.
                        if (hv_CropPixel == null)
                            hv_CropPixel = new HTuple();
                        hv_CropPixel[1] = hv_CurrentParamValue;
                        //Check if input value is greater 0.
                        if ((int)(hv_CurrentParamValue.TupleIsNumber()) != 0)
                        {
                            if ((int)(new HTuple(hv_CurrentParamValue.TupleLess(1))) != 0)
                            {
                                throw new HalconException("The given value for crop_pixel has to be greater then or equal to 1.");
                            }
                        }
                        else
                        {
                            throw new HalconException("The given value for crop_pixel has to be a string.");
                        }
                    }
                    else if ((int)(new HTuple(hv_CurrentParamName.TupleEqual(hv_BrightnessVariation.TupleSelect(
                        0)))) != 0)
                    {
                        //Set brightness variation.
                        if (hv_BrightnessVariation == null)
                            hv_BrightnessVariation = new HTuple();
                        hv_BrightnessVariation[1] = hv_CurrentParamValue;
                        //Check if input value is in range of 0-255.
                        if ((int)(hv_CurrentParamValue.TupleIsNumber()) != 0)
                        {
                            if ((int)((new HTuple(hv_CurrentParamValue.TupleLess(0))).TupleOr(new HTuple(hv_CurrentParamValue.TupleGreater(
                                255)))) != 0)
                            {
                                throw new HalconException("The given value for brightness_variation has to be in the range 0-255.");
                            }
                        }
                        else
                        {
                            throw new HalconException("The given value for brightness_variation has to be in the range 0-255.");
                        }
                    }
                    else if ((int)(new HTuple(hv_CurrentParamName.TupleEqual(hv_BrightnessVariationSpot.TupleSelect(
                        0)))) != 0)
                    {
                        //Set brightness variation of spot.
                        if (hv_BrightnessVariationSpot == null)
                            hv_BrightnessVariationSpot = new HTuple();
                        hv_BrightnessVariationSpot[1] = hv_CurrentParamValue;
                        //Check if input value is in range of 0-255.
                        if ((int)(hv_CurrentParamValue.TupleIsNumber()) != 0)
                        {
                            if ((int)((new HTuple(hv_CurrentParamValue.TupleLess(0))).TupleOr(new HTuple(hv_CurrentParamValue.TupleGreater(
                                255)))) != 0)
                            {
                                throw new HalconException("The given value for brightness_variation_spot has to be in the range 0-255.");
                            }
                        }
                        else
                        {
                            throw new HalconException("The given value for brightness_variation_spot has to be in the range 0-255.");
                        }
                    }
                    else
                    {
                        throw new HalconException(("Unknown generic parameter: '" + (hv_GenParamName.TupleSelect(
                            hv_GenParamIndex))) + "'");
                    }
                }
                //
                //Aggregate all possible distortions and parameter values into a vector.
                //
                hvec_AvailableDistortions.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hvec_AvailableDistortions = dh.Take(dh.Add(new HTupleVector(1)));
                }
                //Cropping percentage.
                if ((int)(((hv_CropPercentage.TupleSelect(1))).TupleIsNumber()) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_AvailableDistortions[new HTuple(hvec_AvailableDistortions.Length)] = dh.Add(new HTupleVector(hv_CropPercentage));
                    }
                }
                //Cropping pixel.
                if ((int)(((hv_CropPixel.TupleSelect(1))).TupleIsNumber()) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_AvailableDistortions[new HTuple(hvec_AvailableDistortions.Length)] = dh.Add(new HTupleVector(hv_CropPixel));
                    }
                }
                //Rotation with a given angular step size.
                if ((int)(new HTuple(((hv_Rotation.TupleSelect(1))).TupleGreater(0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_AvailableDistortions[new HTuple(hvec_AvailableDistortions.Length)] = dh.Add(new HTupleVector(hv_Rotation));
                    }
                }
                //Rotation within a given range (step size 1).
                if ((int)(new HTuple(((hv_RotationRange.TupleSelect(1))).TupleGreater(0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_AvailableDistortions[new HTuple(hvec_AvailableDistortions.Length)] = dh.Add(new HTupleVector(hv_RotationRange));
                    }
                }
                //Mirroring: in row and column direction are allowed.
                if ((int)((new HTuple(((hv_Mirror.TupleSelect(1))).TupleRegexpTest("r"))).TupleOr(
                    ((hv_Mirror.TupleSelect(1))).TupleRegexpTest("c"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_AvailableDistortions[new HTuple(hvec_AvailableDistortions.Length)] = dh.Add(new HTupleVector(hv_Mirror));
                    }
                }
                //Brightness variation.
                if ((int)(new HTuple(((hv_BrightnessVariation.TupleSelect(1))).TupleGreater(
                    0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_AvailableDistortions[new HTuple(hvec_AvailableDistortions.Length)] = dh.Add(new HTupleVector(hv_BrightnessVariation));
                    }
                }
                //Brightness variation spot.
                if ((int)(new HTuple(((hv_BrightnessVariationSpot.TupleSelect(1))).TupleGreater(
                    0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hvec_AvailableDistortions[new HTuple(hvec_AvailableDistortions.Length)] = dh.Add(new HTupleVector(hv_BrightnessVariationSpot));
                    }
                }
                //Check number of available distortions
                hv_NumAvailableDistortions.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumAvailableDistortions = new HTuple(hvec_AvailableDistortions.Length);
                }
                if ((int)(new HTuple(hv_NumAvailableDistortions.TupleEqual(0))) != 0)
                {
                    ho_ImagesAugmented.Dispose();
                    ho_ImagesAugmented = new HObject(ho_Images);
                    ho_ImageSelected.Dispose();
                    ho_ImagePart.Dispose();
                    ho_ImageRotated.Dispose();
                    ho_DomainRotated.Dispose();
                    ho_ImageScaled.Dispose();
                    ho_ImageSpot.Dispose();

                    hv_AugmentationPercentage.Dispose();
                    hv_CropPercentage.Dispose();
                    hv_CropPixel.Dispose();
                    hv_Rotation.Dispose();
                    hv_RotationRange.Dispose();
                    hv_Mirror.Dispose();
                    hv_BrightnessVariation.Dispose();
                    hv_BrightnessVariationSpot.Dispose();
                    hv_GenParamIndex.Dispose();
                    hv_CurrentParamName.Dispose();
                    hv_CurrentParamValue.Dispose();
                    hv_NumAvailableDistortions.Dispose();
                    hv_NumImages.Dispose();
                    hv_AugmentationRate.Dispose();
                    hv_NumAugmentations.Dispose();
                    hv_ImageIndices.Dispose();
                    hv_SelectedImages.Dispose();
                    hv_SelectedDistortions.Dispose();
                    hv_IndexDistortion.Dispose();
                    hv_Index.Dispose();
                    hv_ImageIndex.Dispose();
                    hv_CurrentDistortion.Dispose();
                    hv_Width.Dispose();
                    hv_Height.Dispose();
                    hv_CropRate.Dispose();
                    hv_Row1.Dispose();
                    hv_Row2.Dispose();
                    hv_Column1.Dispose();
                    hv_Column2.Dispose();
                    hv_Length.Dispose();
                    hv_RotationStep.Dispose();
                    hv_NumPossibleRotations.Dispose();
                    hv_CurrentRotation.Dispose();
                    hv_HomMat2DIdentity.Dispose();
                    hv_HomMat2DRotate.Dispose();
                    hv_NumMirrorMethods.Dispose();
                    hv_PropabilityMethods.Dispose();
                    hv_StrMirror.Dispose();
                    hv_StrIdx.Dispose();
                    hv_SelectedChar.Dispose();
                    hv_BrightnessVariationValue.Dispose();
                    hv_SpotSize.Dispose();
                    hv_SpotRow.Dispose();
                    hv_SpotColumn.Dispose();
                    hvec_AvailableDistortions.Dispose();
                    hvec_Distortions.Dispose();

                    return;
                }
                //
                //Randomly choose images and augmentation methods.
                //
                //Number of images to be augmented.
                hv_NumImages.Dispose();
                HOperatorSet.CountObj(ho_Images, out hv_NumImages);
                if ((int)(new HTuple(hv_NumImages.TupleEqual(0))) != 0)
                {
                    throw new HalconException("There are no images to be processed.");
                }
                //Calculate how many images are to be augmented.
                hv_AugmentationRate.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_AugmentationRate = (hv_AugmentationPercentage.TupleSelect(
                        1)) * 0.01;
                }
                hv_NumAugmentations.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumAugmentations = ((((hv_AugmentationRate * hv_NumImages)).TupleCeil()
                        )).TupleInt();
                }
                //Select a random subset of images
                //that are to be augmented.
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ImageIndices.Dispose();
                    tuple_shuffle(HTuple.TupleGenSequence(0, hv_NumImages - 1, 1), out hv_ImageIndices);
                }
                hv_SelectedImages.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_SelectedImages = hv_ImageIndices.TupleSelectRange(
                        0, hv_NumAugmentations - 1);
                }
                //Select a random distortion method for each image.
                hv_SelectedDistortions.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_SelectedDistortions = ((((HTuple.TupleRand(
                        hv_NumAugmentations) * hv_NumAvailableDistortions)).TupleFloor())).TupleInt()
                        ;
                }
                //Fill up vector of distortions for all input images.
                hvec_Distortions.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hvec_Distortions = dh.Take(dh.Add(new HTupleVector(1)));
                }
                hv_IndexDistortion.Dispose();
                hv_IndexDistortion = 0;
                HTuple end_val181 = hv_NumImages - 1;
                HTuple step_val181 = 1;
                for (hv_Index = 0; hv_Index.Continue(end_val181, step_val181); hv_Index = hv_Index.TupleAdd(step_val181))
                {
                    //Check if Index corresponds to a selected image.
                    if ((int)(new HTuple(((((hv_SelectedImages.TupleEqualElem(hv_Index))).TupleSum()
                        )).TupleGreater(0))) != 0)
                    {
                        //Add a distortion method.
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hvec_Distortions[hv_Index] = hvec_AvailableDistortions[hv_SelectedDistortions.TupleSelect(
                                hv_IndexDistortion)];
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_IndexDistortion = hv_IndexDistortion + 1;
                                hv_IndexDistortion.Dispose();
                                hv_IndexDistortion = ExpTmpLocalVar_IndexDistortion;
                            }
                        }
                    }
                    else
                    {
                        //Image will not be distorted.
                        hvec_Distortions[hv_Index] = new HTupleVector((new HTuple("none")).TupleConcat(
                            0));
                    }
                }
                //
                //Augment (distort) the images.
                //
                //Generate output image array.
                ho_ImagesAugmented.Dispose();
                HOperatorSet.GenEmptyObj(out ho_ImagesAugmented);
                //Loop over all images and apply distortions.
                HTuple end_val198 = hv_NumImages - 1;
                HTuple step_val198 = 1;
                for (hv_ImageIndex = 0; hv_ImageIndex.Continue(end_val198, step_val198); hv_ImageIndex = hv_ImageIndex.TupleAdd(step_val198))
                {
                    //Get distortion method.
                    hv_CurrentDistortion.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_CurrentDistortion = new HTuple(hvec_Distortions[hv_ImageIndex].T);
                    }
                    //Get image to be processed.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_ImageSelected.Dispose();
                        HOperatorSet.SelectObj(ho_Images, out ho_ImageSelected, hv_ImageIndex + 1);
                    }
                    hv_Width.Dispose(); hv_Height.Dispose();
                    HOperatorSet.GetImageSize(ho_ImageSelected, out hv_Width, out hv_Height);
                    if ((int)(new HTuple(((hv_CurrentDistortion.TupleSelect(0))).TupleEqual(hv_CropPercentage.TupleSelect(
                        0)))) != 0)
                    {
                        //Cropping:
                        //Define cropping rectangle.
                        hv_CropRate.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_CropRate = (hv_CurrentDistortion.TupleSelect(
                                1)) * 0.01;
                        }
                        hv_Row1.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row1 = ((((1 - hv_CropRate) * hv_Height) * HTuple.TupleRand(
                                1))).TupleFloor();
                        }
                        hv_Row2.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row2 = hv_Row1 + (hv_CropRate * hv_Height);
                        }
                        hv_Column1.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Column1 = ((((1 - hv_CropRate) * hv_Width) * HTuple.TupleRand(
                                1))).TupleFloor();
                        }
                        hv_Column2.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Column2 = hv_Column1 + (hv_CropRate * hv_Width);
                        }
                        //Crop the image and add to output.
                        ho_ImagePart.Dispose();
                        HOperatorSet.CropRectangle1(ho_ImageSelected, out ho_ImagePart, hv_Row1,
                            hv_Column1, hv_Row2, hv_Column2);
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.ConcatObj(ho_ImagesAugmented, ho_ImagePart, out ExpTmpOutVar_0
                                );
                            ho_ImagesAugmented.Dispose();
                            ho_ImagesAugmented = ExpTmpOutVar_0;
                        }
                    }
                    else if ((int)(new HTuple(((hv_CurrentDistortion.TupleSelect(0))).TupleEqual(
                        hv_CropPixel.TupleSelect(0)))) != 0)
                    {
                        //Cropping:
                        //Define cropping rectangle.
                        hv_Length.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Length = hv_CurrentDistortion.TupleSelect(
                                1);
                        }
                        hv_Row1.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row1 = HTuple.TupleRand(
                                1) * (hv_Height - hv_Length);
                        }
                        hv_Row2.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row2 = (hv_Row1 + hv_Length) - 1;
                        }
                        hv_Column1.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Column1 = HTuple.TupleRand(
                                1) * (hv_Width - hv_Length);
                        }
                        hv_Column2.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Column2 = (hv_Column1 + hv_Length) - 1;
                        }
                        //Crop the image and add to output.
                        ho_ImagePart.Dispose();
                        HOperatorSet.CropRectangle1(ho_ImageSelected, out ho_ImagePart, hv_Row1,
                            hv_Column1, hv_Row2, hv_Column2);
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.ConcatObj(ho_ImagesAugmented, ho_ImagePart, out ExpTmpOutVar_0
                                );
                            ho_ImagesAugmented.Dispose();
                            ho_ImagesAugmented = ExpTmpOutVar_0;
                        }
                    }
                    else if ((int)((new HTuple(((hv_CurrentDistortion.TupleSelect(0))).TupleEqual(
                        hv_Rotation.TupleSelect(0)))).TupleOr(new HTuple(((hv_CurrentDistortion.TupleSelect(
                        0))).TupleEqual(hv_RotationRange.TupleSelect(0))))) != 0)
                    {
                        //Rotation:
                        if ((int)(new HTuple(((hv_CurrentDistortion.TupleSelect(0))).TupleEqual(
                            hv_Rotation.TupleSelect(0)))) != 0)
                        {
                            //Determine rotation angle for method 'rotate' (angle in range [0:CurrentDistortion[1]:360)).
                            hv_RotationStep.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_RotationStep = hv_CurrentDistortion.TupleSelect(
                                    1);
                            }
                            hv_NumPossibleRotations.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_NumPossibleRotations = 360.0 / hv_RotationStep;
                            }
                            hv_CurrentRotation.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_CurrentRotation = hv_RotationStep * ((((hv_NumPossibleRotations * HTuple.TupleRand(
                                    1))).TupleInt()) + 1);
                            }
                        }
                        else
                        {
                            //Determine rotation angle for method 'rotate_range' (angle in range [0:1:CurrentDistortion[1])).
                            hv_RotationStep.Dispose();
                            hv_RotationStep = 1;
                            hv_NumPossibleRotations.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_NumPossibleRotations = hv_CurrentDistortion.TupleSelect(
                                    1);
                            }
                            hv_CurrentRotation.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_CurrentRotation = hv_RotationStep * ((((hv_NumPossibleRotations * HTuple.TupleRand(
                                    1))).TupleInt()) + 1);
                            }
                            //Select direction of rotation randomly.
                            if ((int)(new HTuple((new HTuple((HTuple.TupleRand(1)).TupleRound())).TupleGreater(
                                0.5))) != 0)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_CurrentRotation = 360.0 - hv_CurrentRotation;
                                        hv_CurrentRotation.Dispose();
                                        hv_CurrentRotation = ExpTmpLocalVar_CurrentRotation;
                                    }
                                }
                            }
                        }
                        if ((int)((new HTuple(((hv_CurrentRotation.TupleInt())).TupleEqual(hv_CurrentRotation))).TupleAnd(
                            new HTuple((((hv_CurrentRotation.TupleInt()) % 90)).TupleEqual(0)))) != 0)
                        {
                            //Rotations around 90 degrees are faster with rotate_image
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_ImagePart.Dispose();
                                HOperatorSet.RotateImage(ho_ImageSelected, out ho_ImagePart, hv_CurrentRotation.TupleInt()
                                    , "constant");
                            }
                        }
                        else
                        {
                            //Create rotation matrix.
                            hv_HomMat2DIdentity.Dispose();
                            HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_HomMat2DRotate.Dispose();
                                HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, hv_CurrentRotation.TupleRad()
                                    , hv_Height / 2.0, hv_Width / 2.0, out hv_HomMat2DRotate);
                            }
                            //Apply rotation.
                            ho_ImageRotated.Dispose();
                            HOperatorSet.AffineTransImage(ho_ImageSelected, out ho_ImageRotated,
                                hv_HomMat2DRotate, "constant", "false");
                            //Remove potential undefined domain.
                            ho_DomainRotated.Dispose();
                            HOperatorSet.GetDomain(ho_ImageRotated, out ho_DomainRotated);
                            hv_Row1.Dispose(); hv_Column1.Dispose(); hv_Row2.Dispose(); hv_Column2.Dispose();
                            HOperatorSet.InnerRectangle1(ho_DomainRotated, out hv_Row1, out hv_Column1,
                                out hv_Row2, out hv_Column2);
                            ho_ImagePart.Dispose();
                            HOperatorSet.CropRectangle1(ho_ImageRotated, out ho_ImagePart, hv_Row1,
                                hv_Column1, hv_Row2, hv_Column2);
                        }
                        //Add the image to the output.
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.ConcatObj(ho_ImagesAugmented, ho_ImagePart, out ExpTmpOutVar_0
                                );
                            ho_ImagesAugmented.Dispose();
                            ho_ImagesAugmented = ExpTmpOutVar_0;
                        }
                    }
                    else if ((int)(new HTuple(((hv_CurrentDistortion.TupleSelect(0))).TupleEqual(
                        hv_Mirror.TupleSelect(0)))) != 0)
                    {
                        //Mirroring:
                        //If more than one method is allowed, chose mirroring method(s) to be applied.
                        hv_NumMirrorMethods.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_NumMirrorMethods = ((hv_CurrentDistortion.TupleSelect(
                                1))).TupleStrlen();
                        }
                        hv_PropabilityMethods.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_PropabilityMethods = 1.0 / hv_NumMirrorMethods;
                        }
                        hv_StrMirror.Dispose();
                        hv_StrMirror = "";
                        while ((int)(new HTuple(hv_StrMirror.TupleEqual(""))) != 0)
                        {
                            HTuple end_val266 = hv_NumMirrorMethods - 1;
                            HTuple step_val266 = 1;
                            for (hv_StrIdx = 0; hv_StrIdx.Continue(end_val266, step_val266); hv_StrIdx = hv_StrIdx.TupleAdd(step_val266))
                            {
                                hv_SelectedChar.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_SelectedChar = ((hv_CurrentDistortion.TupleSelect(
                                        1))).TupleStrBitSelect(hv_StrIdx);
                                }
                                if ((int)(new HTuple((HTuple.TupleRand(1)).TupleLess(hv_PropabilityMethods))) != 0)
                                {
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        {
                                            HTuple
                                              ExpTmpLocalVar_StrMirror = hv_StrMirror + hv_SelectedChar;
                                            hv_StrMirror.Dispose();
                                            hv_StrMirror = ExpTmpLocalVar_StrMirror;
                                        }
                                    }
                                }
                            }
                        }
                        //Apply the chosen mirroring method(s).
                        if ((int)(hv_StrMirror.TupleRegexpTest("c")) != 0)
                        {
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.MirrorImage(ho_ImageSelected, out ExpTmpOutVar_0, "column");
                                ho_ImageSelected.Dispose();
                                ho_ImageSelected = ExpTmpOutVar_0;
                            }
                        }
                        if ((int)(hv_StrMirror.TupleRegexpTest("r")) != 0)
                        {
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.MirrorImage(ho_ImageSelected, out ExpTmpOutVar_0, "row");
                                ho_ImageSelected.Dispose();
                                ho_ImageSelected = ExpTmpOutVar_0;
                            }
                        }
                        //Add the image to the output.
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.ConcatObj(ho_ImagesAugmented, ho_ImageSelected, out ExpTmpOutVar_0
                                );
                            ho_ImagesAugmented.Dispose();
                            ho_ImagesAugmented = ExpTmpOutVar_0;
                        }
                    }
                    else if ((int)(new HTuple(((hv_CurrentDistortion.TupleSelect(0))).TupleEqual(
                        hv_BrightnessVariation.TupleSelect(0)))) != 0)
                    {
                        //Brightness variation:
                        //Add random brightness variation.
                        hv_BrightnessVariationValue.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_BrightnessVariationValue = ((HTuple.TupleRand(
                                1) * 2) - 1) * (hv_CurrentDistortion.TupleSelect(1));
                        }
                        ho_ImageScaled.Dispose();
                        HOperatorSet.ScaleImage(ho_ImageSelected, out ho_ImageScaled, 1.0, hv_BrightnessVariationValue);
                        //Add the image to the output.
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.ConcatObj(ho_ImagesAugmented, ho_ImageScaled, out ExpTmpOutVar_0
                                );
                            ho_ImagesAugmented.Dispose();
                            ho_ImagesAugmented = ExpTmpOutVar_0;
                        }
                    }
                    else if ((int)(new HTuple(((hv_CurrentDistortion.TupleSelect(0))).TupleEqual(
                        hv_BrightnessVariationSpot.TupleSelect(0)))) != 0)
                    {
                        //Determine random brightness variation.
                        hv_BrightnessVariationValue.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_BrightnessVariationValue = ((HTuple.TupleRand(
                                1) * 2) - 1) * (hv_CurrentDistortion.TupleSelect(1));
                        }
                        //Determine random spot size between [0.5*Width, Width].
                        hv_SpotSize.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_SpotSize = hv_Width * ((HTuple.TupleRand(
                                1) / 2) + .5);
                        }
                        //Determine random spot position.
                        hv_SpotRow.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_SpotRow = HTuple.TupleRand(
                                1) * hv_Height;
                        }
                        hv_SpotColumn.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_SpotColumn = HTuple.TupleRand(
                                1) * hv_Width;
                        }
                        ho_ImageSpot.Dispose();
                        apply_brightness_variation_spot(ho_ImageSelected, out ho_ImageSpot, hv_SpotSize,
                            hv_SpotRow, hv_SpotColumn, hv_BrightnessVariationValue);
                        //Add the image to the output.
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.ConcatObj(ho_ImagesAugmented, ho_ImageSpot, out ExpTmpOutVar_0
                                );
                            ho_ImagesAugmented.Dispose();
                            ho_ImagesAugmented = ExpTmpOutVar_0;
                        }
                    }
                    else
                    {
                        //Add unchanged image to the output.
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.ConcatObj(ho_ImagesAugmented, ho_ImageSelected, out ExpTmpOutVar_0
                                );
                            ho_ImagesAugmented.Dispose();
                            ho_ImagesAugmented = ExpTmpOutVar_0;
                        }
                    }
                }
                ho_ImageSelected.Dispose();
                ho_ImagePart.Dispose();
                ho_ImageRotated.Dispose();
                ho_DomainRotated.Dispose();
                ho_ImageScaled.Dispose();
                ho_ImageSpot.Dispose();

                hv_AugmentationPercentage.Dispose();
                hv_CropPercentage.Dispose();
                hv_CropPixel.Dispose();
                hv_Rotation.Dispose();
                hv_RotationRange.Dispose();
                hv_Mirror.Dispose();
                hv_BrightnessVariation.Dispose();
                hv_BrightnessVariationSpot.Dispose();
                hv_GenParamIndex.Dispose();
                hv_CurrentParamName.Dispose();
                hv_CurrentParamValue.Dispose();
                hv_NumAvailableDistortions.Dispose();
                hv_NumImages.Dispose();
                hv_AugmentationRate.Dispose();
                hv_NumAugmentations.Dispose();
                hv_ImageIndices.Dispose();
                hv_SelectedImages.Dispose();
                hv_SelectedDistortions.Dispose();
                hv_IndexDistortion.Dispose();
                hv_Index.Dispose();
                hv_ImageIndex.Dispose();
                hv_CurrentDistortion.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_CropRate.Dispose();
                hv_Row1.Dispose();
                hv_Row2.Dispose();
                hv_Column1.Dispose();
                hv_Column2.Dispose();
                hv_Length.Dispose();
                hv_RotationStep.Dispose();
                hv_NumPossibleRotations.Dispose();
                hv_CurrentRotation.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_NumMirrorMethods.Dispose();
                hv_PropabilityMethods.Dispose();
                hv_StrMirror.Dispose();
                hv_StrIdx.Dispose();
                hv_SelectedChar.Dispose();
                hv_BrightnessVariationValue.Dispose();
                hv_SpotSize.Dispose();
                hv_SpotRow.Dispose();
                hv_SpotColumn.Dispose();
                hvec_AvailableDistortions.Dispose();
                hvec_Distortions.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_ImageSelected.Dispose();
                ho_ImagePart.Dispose();
                ho_ImageRotated.Dispose();
                ho_DomainRotated.Dispose();
                ho_ImageScaled.Dispose();
                ho_ImageSpot.Dispose();

                hv_AugmentationPercentage.Dispose();
                hv_CropPercentage.Dispose();
                hv_CropPixel.Dispose();
                hv_Rotation.Dispose();
                hv_RotationRange.Dispose();
                hv_Mirror.Dispose();
                hv_BrightnessVariation.Dispose();
                hv_BrightnessVariationSpot.Dispose();
                hv_GenParamIndex.Dispose();
                hv_CurrentParamName.Dispose();
                hv_CurrentParamValue.Dispose();
                hv_NumAvailableDistortions.Dispose();
                hv_NumImages.Dispose();
                hv_AugmentationRate.Dispose();
                hv_NumAugmentations.Dispose();
                hv_ImageIndices.Dispose();
                hv_SelectedImages.Dispose();
                hv_SelectedDistortions.Dispose();
                hv_IndexDistortion.Dispose();
                hv_Index.Dispose();
                hv_ImageIndex.Dispose();
                hv_CurrentDistortion.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_CropRate.Dispose();
                hv_Row1.Dispose();
                hv_Row2.Dispose();
                hv_Column1.Dispose();
                hv_Column2.Dispose();
                hv_Length.Dispose();
                hv_RotationStep.Dispose();
                hv_NumPossibleRotations.Dispose();
                hv_CurrentRotation.Dispose();
                hv_HomMat2DIdentity.Dispose();
                hv_HomMat2DRotate.Dispose();
                hv_NumMirrorMethods.Dispose();
                hv_PropabilityMethods.Dispose();
                hv_StrMirror.Dispose();
                hv_StrIdx.Dispose();
                hv_SelectedChar.Dispose();
                hv_BrightnessVariationValue.Dispose();
                hv_SpotSize.Dispose();
                hv_SpotRow.Dispose();
                hv_SpotColumn.Dispose();
                hvec_AvailableDistortions.Dispose();
                hvec_Distortions.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Deep Learning / Classification
        // Short Description: Compute the TopK error. 
        public static void compute_top_k_error(HTuple hv_DLClassifierHandle, HTuple hv_DLClassifierResultID,
            HTuple hv_GroundTruthLabels, HTuple hv_Indices, HTuple hv_K, out HTuple hv_TopKError)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_NumMatches = new HTuple(), hv_GroundTruthLabelsSelected = new HTuple();
            HTuple hv_BatchSize = new HTuple(), hv_IndexLabel = new HTuple();
            HTuple hv_CurrentLabel = new HTuple(), hv_ResultHandleIndex = new HTuple();
            HTuple hv_ResultIndex = new HTuple(), hv_PredictedClasses = new HTuple();
            // Initialize local and output iconic variables 
            hv_TopKError = new HTuple();
            try
            {
                //This procedure compares the GroundtruthLabels
                //with the K inferred classes of highest probability,
                //stored in DLClassifierResultID, and returns the TopKError.
                //Indices defines which images (and thus GroundTruthLabels
                //as well as inference results) are considered.
                hv_NumMatches.Dispose();
                hv_NumMatches = 0;
                //
                //Select the chosen GroundTruthLabels.
                hv_GroundTruthLabelsSelected.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_GroundTruthLabelsSelected = hv_GroundTruthLabels.TupleSelect(
                        hv_Indices);
                }
                //
                //Get the batch size from the classifier handle.
                hv_BatchSize.Dispose();
                HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "batch_size", out hv_BatchSize);
                //
                //Loop through all selected ground truth labels.
                for (hv_IndexLabel = 0; (int)hv_IndexLabel <= (int)((new HTuple(hv_GroundTruthLabelsSelected.TupleLength()
                    )) - 1); hv_IndexLabel = (int)hv_IndexLabel + 1)
                {
                    //Get ground truth label.
                    hv_CurrentLabel.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_CurrentLabel = hv_GroundTruthLabelsSelected.TupleSelect(
                            hv_IndexLabel);
                    }
                    hv_ResultHandleIndex.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ResultHandleIndex = (((((hv_Indices.TupleSelect(
                            hv_IndexLabel)) / hv_BatchSize)).TupleFloor())).TupleInt();
                    }
                    hv_ResultIndex.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ResultIndex = (hv_Indices.TupleSelect(
                            hv_IndexLabel)) % hv_BatchSize;
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_PredictedClasses.Dispose();
                        HOperatorSet.GetDlClassifierResult(hv_DLClassifierResultID.TupleSelect(hv_ResultHandleIndex),
                            hv_ResultIndex, "predicted_classes", out hv_PredictedClasses);
                    }
                    //Get the K best results.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_PredictedClasses = hv_PredictedClasses.TupleSelectRange(
                                0, hv_K - 1);
                            hv_PredictedClasses.Dispose();
                            hv_PredictedClasses = ExpTmpLocalVar_PredictedClasses;
                        }
                    }
                    //Count how often the ground truth label
                    //and K predicted classes match.
                    if ((int)(new HTuple(((hv_PredictedClasses.TupleFind(hv_CurrentLabel))).TupleNotEqual(
                        -1))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_NumMatches = hv_NumMatches + 1;
                                hv_NumMatches.Dispose();
                                hv_NumMatches = ExpTmpLocalVar_NumMatches;
                            }
                        }
                    }
                }
                hv_TopKError.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TopKError = 1.0 - ((hv_NumMatches.TupleReal()
                        ) / (new HTuple(hv_GroundTruthLabelsSelected.TupleLength())));
                }

                hv_NumMatches.Dispose();
                hv_GroundTruthLabelsSelected.Dispose();
                hv_BatchSize.Dispose();
                hv_IndexLabel.Dispose();
                hv_CurrentLabel.Dispose();
                hv_ResultHandleIndex.Dispose();
                hv_ResultIndex.Dispose();
                hv_PredictedClasses.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_NumMatches.Dispose();
                hv_GroundTruthLabelsSelected.Dispose();
                hv_BatchSize.Dispose();
                hv_IndexLabel.Dispose();
                hv_CurrentLabel.Dispose();
                hv_ResultHandleIndex.Dispose();
                hv_ResultIndex.Dispose();
                hv_PredictedClasses.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Develop
        // Short Description: Open a new graphics window that preserves the aspect ratio of the given image. 
        public static void dev_open_window_fit_image(HObject ho_Image, HTuple hv_Row, HTuple hv_Column,
            HTuple hv_WidthLimit, HTuple hv_HeightLimit, out HTuple hv_WindowHandle)
        {




            // Local iconic variables 

            // Local control variables 

            HTuple hv_MinWidth = new HTuple(), hv_MaxWidth = new HTuple();
            HTuple hv_MinHeight = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_ResizeFactor = new HTuple(), hv_ImageWidth = new HTuple();
            HTuple hv_ImageHeight = new HTuple(), hv_TempWidth = new HTuple();
            HTuple hv_TempHeight = new HTuple(), hv_WindowWidth = new HTuple();
            HTuple hv_WindowHeight = new HTuple();
            // Initialize local and output iconic variables 
            hv_WindowHandle = new HTuple();
            try
            {
                //This procedure opens a new graphics window and adjusts the size
                //such that it fits into the limits specified by WidthLimit
                //and HeightLimit, but also maintains the correct image aspect ratio.
                //
                //If it is impossible to match the minimum and maximum extent requirements
                //at the same time (f.e. if the image is very long but narrow),
                //the maximum value gets a higher priority,
                //
                //Parse input tuple WidthLimit
                if ((int)((new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                    0))).TupleOr(new HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
                {
                    hv_MinWidth.Dispose();
                    hv_MinWidth = 500;
                    hv_MaxWidth.Dispose();
                    hv_MaxWidth = 800;
                }
                else if ((int)(new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_MinWidth.Dispose();
                    hv_MinWidth = 0;
                    hv_MaxWidth.Dispose();
                    hv_MaxWidth = new HTuple(hv_WidthLimit);
                }
                else
                {
                    hv_MinWidth.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MinWidth = hv_WidthLimit.TupleSelect(
                            0);
                    }
                    hv_MaxWidth.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MaxWidth = hv_WidthLimit.TupleSelect(
                            1);
                    }
                }
                //Parse input tuple HeightLimit
                if ((int)((new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                    0))).TupleOr(new HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
                {
                    hv_MinHeight.Dispose();
                    hv_MinHeight = 400;
                    hv_MaxHeight.Dispose();
                    hv_MaxHeight = 600;
                }
                else if ((int)(new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_MinHeight.Dispose();
                    hv_MinHeight = 0;
                    hv_MaxHeight.Dispose();
                    hv_MaxHeight = new HTuple(hv_HeightLimit);
                }
                else
                {
                    hv_MinHeight.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MinHeight = hv_HeightLimit.TupleSelect(
                            0);
                    }
                    hv_MaxHeight.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MaxHeight = hv_HeightLimit.TupleSelect(
                            1);
                    }
                }
                //
                //Test, if window size has to be changed.
                hv_ResizeFactor.Dispose();
                hv_ResizeFactor = 1;
                hv_ImageWidth.Dispose(); hv_ImageHeight.Dispose();
                HOperatorSet.GetImageSize(ho_Image, out hv_ImageWidth, out hv_ImageHeight);
                //First, expand window to the minimum extents (if necessary).
                if ((int)((new HTuple(hv_MinWidth.TupleGreater(hv_ImageWidth))).TupleOr(new HTuple(hv_MinHeight.TupleGreater(
                    hv_ImageHeight)))) != 0)
                {
                    hv_ResizeFactor.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ResizeFactor = (((((hv_MinWidth.TupleReal()
                            ) / hv_ImageWidth)).TupleConcat((hv_MinHeight.TupleReal()) / hv_ImageHeight))).TupleMax()
                            ;
                    }
                }
                hv_TempWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TempWidth = hv_ImageWidth * hv_ResizeFactor;
                }
                hv_TempHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TempHeight = hv_ImageHeight * hv_ResizeFactor;
                }
                //Then, shrink window to maximum extents (if necessary).
                if ((int)((new HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HTuple(hv_MaxHeight.TupleLess(
                    hv_TempHeight)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()
                                ) / hv_TempWidth)).TupleConcat((hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin()
                                );
                            hv_ResizeFactor.Dispose();
                            hv_ResizeFactor = ExpTmpLocalVar_ResizeFactor;
                        }
                    }
                }
                hv_WindowWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_WindowWidth = hv_ImageWidth * hv_ResizeFactor;
                }
                hv_WindowHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_WindowHeight = hv_ImageHeight * hv_ResizeFactor;
                }
                //Resize window
                HOperatorSet.SetWindowAttr("background_color", "black");
                HOperatorSet.OpenWindow(hv_Row, hv_Column, hv_WindowWidth, hv_WindowHeight, 0, "visible", "", out hv_WindowHandle);
                HDevWindowStack.Push(hv_WindowHandle);
                //if (HDevWindowStack.IsOpen())
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_ImageHeight - 1,
                            hv_ImageWidth - 1);
                    }
                }

                hv_MinWidth.Dispose();
                hv_MaxWidth.Dispose();
                hv_MinHeight.Dispose();
                hv_MaxHeight.Dispose();
                hv_ResizeFactor.Dispose();
                hv_ImageWidth.Dispose();
                hv_ImageHeight.Dispose();
                hv_TempWidth.Dispose();
                hv_TempHeight.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_MinWidth.Dispose();
                hv_MaxWidth.Dispose();
                hv_MinHeight.Dispose();
                hv_MaxHeight.Dispose();
                hv_ResizeFactor.Dispose();
                hv_ImageWidth.Dispose();
                hv_ImageHeight.Dispose();
                hv_TempWidth.Dispose();
                hv_TempHeight.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Develop
        // Short Description: Open a new graphics window that preserves the aspect ratio of the given image size. 
        public static void dev_open_window_fit_size(HTuple hv_Row, HTuple hv_Column, HTuple hv_Width,
            HTuple hv_Height, HTuple hv_WidthLimit, HTuple hv_HeightLimit, out HTuple hv_WindowHandle)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_MinWidth = new HTuple(), hv_MaxWidth = new HTuple();
            HTuple hv_MinHeight = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_ResizeFactor = new HTuple(), hv_TempWidth = new HTuple();
            HTuple hv_TempHeight = new HTuple(), hv_WindowWidth = new HTuple();
            HTuple hv_WindowHeight = new HTuple();
            // Initialize local and output iconic variables 
            hv_WindowHandle = new HTuple();
            try
            {
                //This procedure open a new graphic window
                //such that it fits into the limits specified by WidthLimit
                //and HeightLimit, but also maintains the correct aspect ratio
                //given by Width and Height.
                //
                //If it is impossible to match the minimum and maximum extent requirements
                //at the same time (f.e. if the image is very long but narrow),
                //the maximum value gets a higher priority.
                //
                //Parse input tuple WidthLimit
                if ((int)((new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                    0))).TupleOr(new HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
                {
                    hv_MinWidth.Dispose();
                    hv_MinWidth = 500;
                    hv_MaxWidth.Dispose();
                    hv_MaxWidth = 800;
                }
                else if ((int)(new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_MinWidth.Dispose();
                    hv_MinWidth = 0;
                    hv_MaxWidth.Dispose();
                    hv_MaxWidth = new HTuple(hv_WidthLimit);
                }
                else
                {
                    hv_MinWidth.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MinWidth = hv_WidthLimit.TupleSelect(
                            0);
                    }
                    hv_MaxWidth.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MaxWidth = hv_WidthLimit.TupleSelect(
                            1);
                    }
                }
                //Parse input tuple HeightLimit
                if ((int)((new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                    0))).TupleOr(new HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
                {
                    hv_MinHeight.Dispose();
                    hv_MinHeight = 400;
                    hv_MaxHeight.Dispose();
                    hv_MaxHeight = 600;
                }
                else if ((int)(new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_MinHeight.Dispose();
                    hv_MinHeight = 0;
                    hv_MaxHeight.Dispose();
                    hv_MaxHeight = new HTuple(hv_HeightLimit);
                }
                else
                {
                    hv_MinHeight.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MinHeight = hv_HeightLimit.TupleSelect(
                            0);
                    }
                    hv_MaxHeight.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MaxHeight = hv_HeightLimit.TupleSelect(
                            1);
                    }
                }
                //
                //Test, if window size has to be changed.
                hv_ResizeFactor.Dispose();
                hv_ResizeFactor = 1;
                //First, expand window to the minimum extents (if necessary).
                if ((int)((new HTuple(hv_MinWidth.TupleGreater(hv_Width))).TupleOr(new HTuple(hv_MinHeight.TupleGreater(
                    hv_Height)))) != 0)
                {
                    hv_ResizeFactor.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ResizeFactor = (((((hv_MinWidth.TupleReal()
                            ) / hv_Width)).TupleConcat((hv_MinHeight.TupleReal()) / hv_Height))).TupleMax()
                            ;
                    }
                }
                hv_TempWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TempWidth = hv_Width * hv_ResizeFactor;
                }
                hv_TempHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TempHeight = hv_Height * hv_ResizeFactor;
                }
                //Then, shrink window to maximum extents (if necessary).
                if ((int)((new HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HTuple(hv_MaxHeight.TupleLess(
                    hv_TempHeight)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()
                                ) / hv_TempWidth)).TupleConcat((hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin()
                                );
                            hv_ResizeFactor.Dispose();
                            hv_ResizeFactor = ExpTmpLocalVar_ResizeFactor;
                        }
                    }
                }
                hv_WindowWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_WindowWidth = hv_Width * hv_ResizeFactor;
                }
                hv_WindowHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_WindowHeight = hv_Height * hv_ResizeFactor;
                }
                //Resize window
                HOperatorSet.SetWindowAttr("background_color", "black");
                HOperatorSet.OpenWindow(hv_Row, hv_Column, hv_WindowWidth, hv_WindowHeight, 0, "visible", "", out hv_WindowHandle);
                HDevWindowStack.Push(hv_WindowHandle);
                //if (HDevWindowStack.IsOpen())
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_Height - 1, hv_Width - 1);
                    }
                }

                hv_MinWidth.Dispose();
                hv_MaxWidth.Dispose();
                hv_MinHeight.Dispose();
                hv_MaxHeight.Dispose();
                hv_ResizeFactor.Dispose();
                hv_TempWidth.Dispose();
                hv_TempHeight.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_MinWidth.Dispose();
                hv_MaxWidth.Dispose();
                hv_MinHeight.Dispose();
                hv_MaxHeight.Dispose();
                hv_ResizeFactor.Dispose();
                hv_TempWidth.Dispose();
                hv_TempHeight.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Develop
        // Short Description: Changes the size of a graphics window with a given maximum and minimum extent such that it preserves the aspect ratio of the given image 
        public static void dev_resize_window_fit_image(HObject ho_Image, HTuple hv_Row, HTuple hv_Column,
            HTuple hv_WidthLimit, HTuple hv_HeightLimit)
        {




            // Local iconic variables 

            // Local control variables 

            HTuple hv_MinWidth = new HTuple(), hv_MaxWidth = new HTuple();
            HTuple hv_MinHeight = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_ResizeFactor = new HTuple(), hv_Pointer = new HTuple();
            HTuple hv_Type = new HTuple(), hv_ImageWidth = new HTuple();
            HTuple hv_ImageHeight = new HTuple(), hv_TempWidth = new HTuple();
            HTuple hv_TempHeight = new HTuple(), hv_WindowWidth = new HTuple();
            HTuple hv_WindowHeight = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                //This procedure adjusts the size of the current window
                //such that it fits into the limits specified by WidthLimit
                //and HeightLimit, but also maintains the correct image aspect ratio.
                //
                //If it is impossible to match the minimum and maximum extent requirements
                //at the same time (f.e. if the image is very long but narrow),
                //the maximum value gets a higher priority,
                //
                //Parse input tuple WidthLimit
                if ((int)((new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                    0))).TupleOr(new HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
                {
                    hv_MinWidth.Dispose();
                    hv_MinWidth = 500;
                    hv_MaxWidth.Dispose();
                    hv_MaxWidth = 800;
                }
                else if ((int)(new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_MinWidth.Dispose();
                    hv_MinWidth = 0;
                    hv_MaxWidth.Dispose();
                    hv_MaxWidth = new HTuple(hv_WidthLimit);
                }
                else
                {
                    hv_MinWidth.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MinWidth = hv_WidthLimit.TupleSelect(
                            0);
                    }
                    hv_MaxWidth.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MaxWidth = hv_WidthLimit.TupleSelect(
                            1);
                    }
                }
                //Parse input tuple HeightLimit
                if ((int)((new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                    0))).TupleOr(new HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
                {
                    hv_MinHeight.Dispose();
                    hv_MinHeight = 400;
                    hv_MaxHeight.Dispose();
                    hv_MaxHeight = 600;
                }
                else if ((int)(new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_MinHeight.Dispose();
                    hv_MinHeight = 0;
                    hv_MaxHeight.Dispose();
                    hv_MaxHeight = new HTuple(hv_HeightLimit);
                }
                else
                {
                    hv_MinHeight.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MinHeight = hv_HeightLimit.TupleSelect(
                            0);
                    }
                    hv_MaxHeight.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MaxHeight = hv_HeightLimit.TupleSelect(
                            1);
                    }
                }
                //
                //Test, if window size has to be changed.
                hv_ResizeFactor.Dispose();
                hv_ResizeFactor = 1;
                hv_Pointer.Dispose(); hv_Type.Dispose(); hv_ImageWidth.Dispose(); hv_ImageHeight.Dispose();
                HOperatorSet.GetImagePointer1(ho_Image, out hv_Pointer, out hv_Type, out hv_ImageWidth,
                    out hv_ImageHeight);
                //First, expand window to the minimum extents (if necessary).
                if ((int)((new HTuple(hv_MinWidth.TupleGreater(hv_ImageWidth))).TupleOr(new HTuple(hv_MinHeight.TupleGreater(
                    hv_ImageHeight)))) != 0)
                {
                    hv_ResizeFactor.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ResizeFactor = (((((hv_MinWidth.TupleReal()
                            ) / hv_ImageWidth)).TupleConcat((hv_MinHeight.TupleReal()) / hv_ImageHeight))).TupleMax()
                            ;
                    }
                }
                hv_TempWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TempWidth = hv_ImageWidth * hv_ResizeFactor;
                }
                hv_TempHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TempHeight = hv_ImageHeight * hv_ResizeFactor;
                }
                //Then, shrink window to maximum extents (if necessary).
                if ((int)((new HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HTuple(hv_MaxHeight.TupleLess(
                    hv_TempHeight)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()
                                ) / hv_TempWidth)).TupleConcat((hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin()
                                );
                            hv_ResizeFactor.Dispose();
                            hv_ResizeFactor = ExpTmpLocalVar_ResizeFactor;
                        }
                    }
                }
                hv_WindowWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_WindowWidth = hv_ImageWidth * hv_ResizeFactor;
                }
                hv_WindowHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_WindowHeight = hv_ImageHeight * hv_ResizeFactor;
                }
                //Resize window
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetWindowExtents(HDevWindowStack.GetActive(), hv_Row, hv_Column,
                        hv_WindowWidth, hv_WindowHeight);
                }
                if (HDevWindowStack.IsOpen())
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.SetPart(HDevWindowStack.GetActive(), 0, 0, hv_ImageHeight - 1,
                            hv_ImageWidth - 1);
                    }
                }

                hv_MinWidth.Dispose();
                hv_MaxWidth.Dispose();
                hv_MinHeight.Dispose();
                hv_MaxHeight.Dispose();
                hv_ResizeFactor.Dispose();
                hv_Pointer.Dispose();
                hv_Type.Dispose();
                hv_ImageWidth.Dispose();
                hv_ImageHeight.Dispose();
                hv_TempWidth.Dispose();
                hv_TempHeight.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_MinWidth.Dispose();
                hv_MaxWidth.Dispose();
                hv_MinHeight.Dispose();
                hv_MaxHeight.Dispose();
                hv_ResizeFactor.Dispose();
                hv_Pointer.Dispose();
                hv_Type.Dispose();
                hv_ImageWidth.Dispose();
                hv_ImageHeight.Dispose();
                hv_TempWidth.Dispose();
                hv_TempHeight.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Develop
        // Short Description: Resizes a graphics window with a given maximum extent such that it preserves the aspect ratio of a given width and height 
        public static void dev_resize_window_fit_size(HTuple hv_Row, HTuple hv_Column, HTuple hv_Width,
            HTuple hv_Height, HTuple hv_WidthLimit, HTuple hv_HeightLimit)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_MinWidth = new HTuple(), hv_MaxWidth = new HTuple();
            HTuple hv_MinHeight = new HTuple(), hv_MaxHeight = new HTuple();
            HTuple hv_ResizeFactor = new HTuple(), hv_TempWidth = new HTuple();
            HTuple hv_TempHeight = new HTuple(), hv_WindowWidth = new HTuple();
            HTuple hv_WindowHeight = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                //This procedure adjusts the size of the current window
                //such that it fits into the limits specified by WidthLimit
                //and HeightLimit, but also maintains the correct aspect ratio
                //given by Width and Height.
                //
                //If it is impossible to match the minimum and maximum extent requirements
                //at the same time (f.e. if the image is very long but narrow),
                //the maximum value gets a higher priority.
                //
                //Parse input tuple WidthLimit
                if ((int)((new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                    0))).TupleOr(new HTuple(hv_WidthLimit.TupleLess(0)))) != 0)
                {
                    hv_MinWidth.Dispose();
                    hv_MinWidth = 500;
                    hv_MaxWidth.Dispose();
                    hv_MaxWidth = 800;
                }
                else if ((int)(new HTuple((new HTuple(hv_WidthLimit.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_MinWidth.Dispose();
                    hv_MinWidth = 0;
                    hv_MaxWidth.Dispose();
                    hv_MaxWidth = new HTuple(hv_WidthLimit);
                }
                else
                {
                    hv_MinWidth.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MinWidth = hv_WidthLimit.TupleSelect(
                            0);
                    }
                    hv_MaxWidth.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MaxWidth = hv_WidthLimit.TupleSelect(
                            1);
                    }
                }
                //Parse input tuple HeightLimit
                if ((int)((new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                    0))).TupleOr(new HTuple(hv_HeightLimit.TupleLess(0)))) != 0)
                {
                    hv_MinHeight.Dispose();
                    hv_MinHeight = 400;
                    hv_MaxHeight.Dispose();
                    hv_MaxHeight = 600;
                }
                else if ((int)(new HTuple((new HTuple(hv_HeightLimit.TupleLength())).TupleEqual(
                    1))) != 0)
                {
                    hv_MinHeight.Dispose();
                    hv_MinHeight = 0;
                    hv_MaxHeight.Dispose();
                    hv_MaxHeight = new HTuple(hv_HeightLimit);
                }
                else
                {
                    hv_MinHeight.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MinHeight = hv_HeightLimit.TupleSelect(
                            0);
                    }
                    hv_MaxHeight.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MaxHeight = hv_HeightLimit.TupleSelect(
                            1);
                    }
                }
                //
                //Test, if window size has to be changed.
                hv_ResizeFactor.Dispose();
                hv_ResizeFactor = 1;
                //First, expand window to the minimum extents (if necessary).
                if ((int)((new HTuple(hv_MinWidth.TupleGreater(hv_Width))).TupleOr(new HTuple(hv_MinHeight.TupleGreater(
                    hv_Height)))) != 0)
                {
                    hv_ResizeFactor.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ResizeFactor = (((((hv_MinWidth.TupleReal()
                            ) / hv_Width)).TupleConcat((hv_MinHeight.TupleReal()) / hv_Height))).TupleMax()
                            ;
                    }
                }
                hv_TempWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TempWidth = hv_Width * hv_ResizeFactor;
                }
                hv_TempHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TempHeight = hv_Height * hv_ResizeFactor;
                }
                //Then, shrink window to maximum extents (if necessary).
                if ((int)((new HTuple(hv_MaxWidth.TupleLess(hv_TempWidth))).TupleOr(new HTuple(hv_MaxHeight.TupleLess(
                    hv_TempHeight)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ResizeFactor = hv_ResizeFactor * ((((((hv_MaxWidth.TupleReal()
                                ) / hv_TempWidth)).TupleConcat((hv_MaxHeight.TupleReal()) / hv_TempHeight))).TupleMin()
                                );
                            hv_ResizeFactor.Dispose();
                            hv_ResizeFactor = ExpTmpLocalVar_ResizeFactor;
                        }
                    }
                }
                hv_WindowWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_WindowWidth = hv_Width * hv_ResizeFactor;
                }
                hv_WindowHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_WindowHeight = hv_Height * hv_ResizeFactor;
                }
                //Resize window
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetWindowExtents(HDevWindowStack.GetActive(), hv_Row, hv_Column,
                        hv_WindowWidth, hv_WindowHeight);
                }
                //if (HDevWindowStack.IsOpen())
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.SetPart(HDevWindowStack.GetActive(), 0, 0, hv_Height - 1, hv_Width - 1);
                    }
                }

                hv_MinWidth.Dispose();
                hv_MaxWidth.Dispose();
                hv_MinHeight.Dispose();
                hv_MaxHeight.Dispose();
                hv_ResizeFactor.Dispose();
                hv_TempWidth.Dispose();
                hv_TempHeight.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_MinWidth.Dispose();
                hv_MaxWidth.Dispose();
                hv_MinHeight.Dispose();
                hv_MaxHeight.Dispose();
                hv_ResizeFactor.Dispose();
                hv_TempWidth.Dispose();
                hv_TempHeight.Dispose();
                hv_WindowWidth.Dispose();
                hv_WindowHeight.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Develop
        // Short Description: Switch dev_update_pc, dev_update_var and dev_update_window to 'off'. 
        public static void dev_update_off()
        {

            // Initialize local and output iconic variables 
            //This procedure sets different update settings to 'off'.
            //This is useful to get the best performance and reduce overhead.
            //
            // dev_update_pc(...); only in hdevelop
            // dev_update_var(...); only in hdevelop
            // dev_update_window(...); only in hdevelop


            return;
        }

        // Chapter: Deep Learning / Classification
        // Short Description: Evaluate the performance of a deep-learning-based classifier. 
        public static void evaluate_dl_classifier(HTuple hv_GroundTruthLabels, HTuple hv_DLClassifierHandle,
            HTuple hv_DLClassifierResultID, HTuple hv_EvaluationMeasureType, HTuple hv_ClassesToEvaluate,
            out HTuple hv_EvaluationMeasure)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_Classes = new HTuple(), hv_TestClassesToEvaluate = new HTuple();
            HTuple hv_NumEvalMeasureTypes = new HTuple(), hv_NumEvalClasses = new HTuple();
            HTuple hv_ComputePrecision = new HTuple(), hv_ComputeRecall = new HTuple();
            HTuple hv_ComputeFScore = new HTuple(), hv_ComputeConfusionMatrix = new HTuple();
            HTuple hv_PredictedClasses = new HTuple(), hv_Index = new HTuple();
            HTuple hv_PredictedClass = new HTuple(), hv_ConfusionMatrix = new HTuple();
            HTuple hv_EvalMeasureTypeIndex = new HTuple(), hv_CurrentEvalMeasure = new HTuple();
            HTuple hv_CurrentEvalClass = new HTuple(), hv_RegExpTopKError = new HTuple();
            HTuple hv_ComputeTopKError = new HTuple(), hv_K = new HTuple();
            HTuple hv_Indices = new HTuple(), hv_TopKError = new HTuple();
            HTuple hv_NumClasses = new HTuple(), hv_IndexClass = new HTuple();
            HTuple hv_ClassPrecisions = new HTuple(), hv_MatrixRowSumID = new HTuple();
            HTuple hv_TruePositive = new HTuple(), hv_SumPredictedClass = new HTuple();
            HTuple hv_ClassPrecision = new HTuple(), hv_Precision = new HTuple();
            HTuple hv_ClassRecalls = new HTuple(), hv_MatrixColumnSumID = new HTuple();
            HTuple hv_SumLabel = new HTuple(), hv_ClassRecall = new HTuple();
            HTuple hv_Recall = new HTuple(), hv_FScore = new HTuple();
            HTuple hv_ClassesToEvaluate_COPY_INP_TMP = new HTuple(hv_ClassesToEvaluate);
            HTuple hv_EvaluationMeasureType_COPY_INP_TMP = new HTuple(hv_EvaluationMeasureType);
            HTuple hv_GroundTruthLabels_COPY_INP_TMP = new HTuple(hv_GroundTruthLabels);

            // Initialize local and output iconic variables 
            hv_EvaluationMeasure = new HTuple();
            try
            {
                //This procedure can be used to compute various evaluation measures
                //to check the performance of your trained
                //deep-learning-based classifier DLClassifierHandle.
                //For this, the GroundTruthLabels must be given. Additionally,
                //the results of the classification must be given in DLClassifierResultID,
                //as returned by apply_dl_classifier and apply_dl_classifier_batchwise.
                //With EvaluationMeasureType, you can choose which evaluation measure
                //to return. With ClassesToEvaluate, you can choose whether to return
                //the result for a single class, a result for every class, or
                //for all classes combined. The result is returned in EvaluationMeasure.
                //
                //Check input.
                //Check whether ClassesToEvaluate is a class or 'global'.
                hv_Classes.Dispose();
                HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "classes", out hv_Classes);
                //
                //Convert the class indices to class labels if necessary
                if ((int)(new HTuple(((((hv_GroundTruthLabels_COPY_INP_TMP.TupleIsIntElem()
                    )).TupleFind(0))).TupleEqual(-1))) != 0)
                {
                    if ((int)((new HTuple(((hv_GroundTruthLabels_COPY_INP_TMP.TupleMin())).TupleLess(
                        0))).TupleOr(new HTuple(((hv_GroundTruthLabels_COPY_INP_TMP.TupleMax()
                        )).TupleGreater((new HTuple(hv_Classes.TupleLength())) - 1)))) != 0)
                    {
                        throw new HalconException("The Indices of the GroundTruthLabels exceed the range of classes. \nPlease check your data split.");
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GroundTruthLabels = hv_Classes.TupleSelect(
                                hv_GroundTruthLabels_COPY_INP_TMP);
                            hv_GroundTruthLabels_COPY_INP_TMP.Dispose();
                            hv_GroundTruthLabels_COPY_INP_TMP = ExpTmpLocalVar_GroundTruthLabels;
                        }
                    }
                }
                if ((int)(new HTuple(((((hv_GroundTruthLabels_COPY_INP_TMP.TupleSort())).TupleUniq()
                    )).TupleNotEqual(hv_Classes.TupleSort()))) != 0)
                {
                    throw new HalconException("Not all classes are represented in the GroundTruthLabels. \nPlease check your data split.");
                }
                hv_TestClassesToEvaluate.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TestClassesToEvaluate = new HTuple();
                    hv_TestClassesToEvaluate[0] = "global";
                    hv_TestClassesToEvaluate = hv_TestClassesToEvaluate.TupleConcat(hv_Classes);
                }
                if ((int)(new HTuple(((hv_ClassesToEvaluate_COPY_INP_TMP.TupleDifference(hv_TestClassesToEvaluate))).TupleNotEqual(
                    new HTuple()))) != 0)
                {
                    throw new HalconException("ClassesToEvaluate invalid.");
                }
                //
                //Count the measure types and modes of ClassesToEvaluate.
                hv_NumEvalMeasureTypes.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumEvalMeasureTypes = new HTuple(hv_EvaluationMeasureType_COPY_INP_TMP.TupleLength()
                        );
                }
                hv_NumEvalClasses.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumEvalClasses = new HTuple(hv_ClassesToEvaluate_COPY_INP_TMP.TupleLength()
                        );
                }
                //
                //If the numbers are not equal, extend the shorter one.
                if ((int)(new HTuple(hv_NumEvalMeasureTypes.TupleGreater(hv_NumEvalClasses))) != 0)
                {
                    if ((int)((new HTuple(hv_NumEvalMeasureTypes.TupleGreater(1))).TupleAnd(new HTuple(hv_NumEvalClasses.TupleGreater(
                        1)))) != 0)
                    {
                        throw new HalconException("Invalid number of elements in EvaluationMeasureType/ClassesToEvaluate.");
                    }
                    else
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_ClassesToEvaluate = HTuple.TupleGenConst(
                                    hv_NumEvalMeasureTypes, hv_ClassesToEvaluate_COPY_INP_TMP);
                                hv_ClassesToEvaluate_COPY_INP_TMP.Dispose();
                                hv_ClassesToEvaluate_COPY_INP_TMP = ExpTmpLocalVar_ClassesToEvaluate;
                            }
                        }
                    }
                }
                if ((int)(new HTuple(hv_NumEvalMeasureTypes.TupleLess(hv_NumEvalClasses))) != 0)
                {
                    if ((int)((new HTuple(hv_NumEvalMeasureTypes.TupleGreater(1))).TupleAnd(new HTuple(hv_NumEvalClasses.TupleGreater(
                        1)))) != 0)
                    {
                        throw new HalconException("Invalid number of elements in EvaluationMeasureType/ClassesToEvaluate.");
                    }
                    else
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_EvaluationMeasureType = HTuple.TupleGenConst(
                                    hv_NumEvalClasses, hv_EvaluationMeasureType_COPY_INP_TMP);
                                hv_EvaluationMeasureType_COPY_INP_TMP.Dispose();
                                hv_EvaluationMeasureType_COPY_INP_TMP = ExpTmpLocalVar_EvaluationMeasureType;
                            }
                        }
                    }
                }
                //
                //Check whether we need to compute a confusion matrix.
                //We want to do this only once to save run time.
                hv_ComputePrecision.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ComputePrecision = hv_EvaluationMeasureType_COPY_INP_TMP.TupleRegexpTest(
                        "precision");
                }
                hv_ComputeRecall.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ComputeRecall = hv_EvaluationMeasureType_COPY_INP_TMP.TupleRegexpTest(
                        "recall");
                }
                hv_ComputeFScore.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ComputeFScore = hv_EvaluationMeasureType_COPY_INP_TMP.TupleRegexpTest(
                        "f_score");
                }
                hv_ComputeConfusionMatrix.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ComputeConfusionMatrix = (hv_ComputePrecision + hv_ComputeRecall) + hv_ComputeFScore;
                }
                if ((int)(new HTuple(hv_ComputeConfusionMatrix.TupleGreater(0))) != 0)
                {
                    //Get the top-1 predicted classes from the result handle(s).
                    hv_PredictedClasses.Dispose();
                    hv_PredictedClasses = new HTuple();
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_DLClassifierResultID.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_PredictedClass.Dispose();
                            HOperatorSet.GetDlClassifierResult(hv_DLClassifierResultID.TupleSelect(
                                hv_Index), "all", "predicted_classes", out hv_PredictedClass);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_PredictedClasses = hv_PredictedClasses.TupleConcat(
                                    hv_PredictedClass);
                                hv_PredictedClasses.Dispose();
                                hv_PredictedClasses = ExpTmpLocalVar_PredictedClasses;
                            }
                        }
                    }
                    //Compute the confusion matrix.
                    hv_ConfusionMatrix.Dispose();
                    gen_confusion_matrix(hv_GroundTruthLabels_COPY_INP_TMP, hv_PredictedClasses,
                        "display_matrix", "none", new HTuple(), out hv_ConfusionMatrix);
                }
                //
                //Loop through all given measure types.
                hv_EvaluationMeasure.Dispose();
                hv_EvaluationMeasure = new HTuple();
                HTuple end_val69 = (((hv_NumEvalMeasureTypes.TupleConcat(
                    hv_NumEvalClasses))).TupleMax()) - 1;
                HTuple step_val69 = 1;
                for (hv_EvalMeasureTypeIndex = 0; hv_EvalMeasureTypeIndex.Continue(end_val69, step_val69); hv_EvalMeasureTypeIndex = hv_EvalMeasureTypeIndex.TupleAdd(step_val69))
                {
                    //Select the current combination.
                    hv_CurrentEvalMeasure.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_CurrentEvalMeasure = hv_EvaluationMeasureType_COPY_INP_TMP.TupleSelect(
                            hv_EvalMeasureTypeIndex);
                    }
                    hv_CurrentEvalClass.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_CurrentEvalClass = hv_ClassesToEvaluate_COPY_INP_TMP.TupleSelect(
                            hv_EvalMeasureTypeIndex);
                    }
                    //Set the output accordingly.
                    //Check whether to compute the top-k error.
                    hv_RegExpTopKError.Dispose();
                    hv_RegExpTopKError = "top([0-9]+)_error";
                    hv_ComputeTopKError.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ComputeTopKError = hv_CurrentEvalMeasure.TupleRegexpTest(
                            hv_RegExpTopKError);
                    }
                    //Check whether to compute the precision, recall, F-score.
                    hv_ComputePrecision.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ComputePrecision = hv_CurrentEvalMeasure.TupleRegexpTest(
                            "precision");
                    }
                    hv_ComputeRecall.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ComputeRecall = hv_CurrentEvalMeasure.TupleRegexpTest(
                            "recall");
                    }
                    hv_ComputeFScore.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ComputeFScore = hv_CurrentEvalMeasure.TupleRegexpTest(
                            "f_score");
                    }
                    //
                    if ((int)(hv_ComputeTopKError) != 0)
                    {
                        //Get the K from the input string 'topK_error'.
                        hv_K.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_K = ((hv_CurrentEvalMeasure.TupleRegexpMatch(
                                hv_RegExpTopKError))).TupleNumber();
                        }
                        //Select all labels or only the labels with the respective class.
                        if ((int)(new HTuple(hv_CurrentEvalClass.TupleEqual("global"))) != 0)
                        {
                            hv_Indices.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Indices = HTuple.TupleGenSequence(
                                    0, (new HTuple(hv_GroundTruthLabels_COPY_INP_TMP.TupleLength())) - 1, 1);
                            }
                        }
                        else
                        {
                            hv_Indices.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Indices = hv_GroundTruthLabels_COPY_INP_TMP.TupleFind(
                                    hv_CurrentEvalClass);
                            }
                        }
                        hv_TopKError.Dispose();
                        compute_top_k_error(hv_DLClassifierHandle, hv_DLClassifierResultID, hv_GroundTruthLabels_COPY_INP_TMP,
                            hv_Indices, hv_K, out hv_TopKError);
                        if (hv_EvaluationMeasure == null)
                            hv_EvaluationMeasure = new HTuple();
                        hv_EvaluationMeasure[hv_EvalMeasureTypeIndex] = hv_TopKError;
                    }
                    else if ((int)((new HTuple(hv_ComputePrecision.TupleOr(hv_ComputeRecall))).TupleOr(
                        hv_ComputeFScore)) != 0)
                    {
                        if ((int)(new HTuple(hv_CurrentEvalClass.TupleEqual("global"))) != 0)
                        {
                            //Compute the mean of the measures for all classes.
                            hv_NumClasses.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_NumClasses = new HTuple(hv_Classes.TupleLength()
                                    );
                            }
                            hv_IndexClass.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_IndexClass = HTuple.TupleGenSequence(
                                    0, hv_NumClasses - 1, 1);
                            }
                        }
                        else
                        {
                            //Compute the measures for a certain class.
                            hv_NumClasses.Dispose();
                            hv_NumClasses = 1;
                            hv_IndexClass.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_IndexClass = hv_Classes.TupleFind(
                                    hv_CurrentEvalClass);
                            }
                        }
                        if ((int)(hv_ComputePrecision.TupleOr(hv_ComputeFScore)) != 0)
                        {
                            hv_ClassPrecisions.Dispose();
                            hv_ClassPrecisions = new HTuple();
                            hv_MatrixRowSumID.Dispose();
                            HOperatorSet.SumMatrix(hv_ConfusionMatrix, "rows", out hv_MatrixRowSumID);
                            HTuple end_val106 = hv_NumClasses - 1;
                            HTuple step_val106 = 1;
                            for (hv_Index = 0; hv_Index.Continue(end_val106, step_val106); hv_Index = hv_Index.TupleAdd(step_val106))
                            {
                                //Compute the precision for every selected class.
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_TruePositive.Dispose();
                                    HOperatorSet.GetValueMatrix(hv_ConfusionMatrix, hv_IndexClass.TupleSelect(
                                        hv_Index), hv_IndexClass.TupleSelect(hv_Index), out hv_TruePositive);
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_SumPredictedClass.Dispose();
                                    HOperatorSet.GetValueMatrix(hv_MatrixRowSumID, hv_IndexClass.TupleSelect(
                                        hv_Index), 0, out hv_SumPredictedClass);
                                }
                                if ((int)(new HTuple(hv_SumPredictedClass.TupleEqual(0))) != 0)
                                {
                                    hv_ClassPrecision.Dispose();
                                    hv_ClassPrecision = 0;
                                }
                                else
                                {
                                    hv_ClassPrecision.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_ClassPrecision = hv_TruePositive / hv_SumPredictedClass;
                                    }
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_ClassPrecisions = hv_ClassPrecisions.TupleConcat(
                                            hv_ClassPrecision);
                                        hv_ClassPrecisions.Dispose();
                                        hv_ClassPrecisions = ExpTmpLocalVar_ClassPrecisions;
                                    }
                                }
                            }
                            hv_Precision.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Precision = hv_ClassPrecisions.TupleMean()
                                    ;
                            }
                            HOperatorSet.ClearMatrix(hv_MatrixRowSumID);
                            if ((int)(hv_ComputePrecision) != 0)
                            {
                                if (hv_EvaluationMeasure == null)
                                    hv_EvaluationMeasure = new HTuple();
                                hv_EvaluationMeasure[hv_EvalMeasureTypeIndex] = hv_Precision;
                            }
                        }
                        if ((int)(hv_ComputeRecall.TupleOr(hv_ComputeFScore)) != 0)
                        {
                            hv_ClassRecalls.Dispose();
                            hv_ClassRecalls = new HTuple();
                            hv_MatrixColumnSumID.Dispose();
                            HOperatorSet.SumMatrix(hv_ConfusionMatrix, "columns", out hv_MatrixColumnSumID);
                            HTuple end_val126 = hv_NumClasses - 1;
                            HTuple step_val126 = 1;
                            for (hv_Index = 0; hv_Index.Continue(end_val126, step_val126); hv_Index = hv_Index.TupleAdd(step_val126))
                            {
                                //Compute the recall for every class.
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_TruePositive.Dispose();
                                    HOperatorSet.GetValueMatrix(hv_ConfusionMatrix, hv_IndexClass.TupleSelect(
                                        hv_Index), hv_IndexClass.TupleSelect(hv_Index), out hv_TruePositive);
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_SumLabel.Dispose();
                                    HOperatorSet.GetValueMatrix(hv_MatrixColumnSumID, 0, hv_IndexClass.TupleSelect(
                                        hv_Index), out hv_SumLabel);
                                }
                                hv_ClassRecall.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_ClassRecall = hv_TruePositive / hv_SumLabel;
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_ClassRecalls = hv_ClassRecalls.TupleConcat(
                                            hv_ClassRecall);
                                        hv_ClassRecalls.Dispose();
                                        hv_ClassRecalls = ExpTmpLocalVar_ClassRecalls;
                                    }
                                }
                            }
                            hv_Recall.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Recall = hv_ClassRecalls.TupleMean()
                                    ;
                            }
                            HOperatorSet.ClearMatrix(hv_MatrixColumnSumID);
                            if ((int)(hv_ComputeRecall) != 0)
                            {
                                if (hv_EvaluationMeasure == null)
                                    hv_EvaluationMeasure = new HTuple();
                                hv_EvaluationMeasure[hv_EvalMeasureTypeIndex] = hv_Recall;
                            }
                        }
                        if ((int)(hv_ComputeFScore) != 0)
                        {
                            //Compute the F-score for a certain class or globally
                            //for the averaged precision and recall.
                            //Precision and recall were already computed above.
                            if ((int)(new HTuple(((hv_Precision + hv_Recall)).TupleEqual(0))) != 0)
                            {
                                hv_FScore.Dispose();
                                hv_FScore = 0.0;
                            }
                            else
                            {
                                hv_FScore.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_FScore = ((2 * hv_Precision) * hv_Recall) / (hv_Precision + hv_Recall);
                                }
                            }
                            if (hv_EvaluationMeasure == null)
                                hv_EvaluationMeasure = new HTuple();
                            hv_EvaluationMeasure[hv_EvalMeasureTypeIndex] = hv_FScore;
                        }
                    }
                    else
                    {
                        throw new HalconException(("Invalid option for EvaluationMeasureType: '" + hv_CurrentEvalMeasure) + "'");
                    }
                }
                if ((int)(hv_ComputeConfusionMatrix) != 0)
                {
                    HOperatorSet.ClearMatrix(hv_ConfusionMatrix);
                }

                hv_ClassesToEvaluate_COPY_INP_TMP.Dispose();
                hv_EvaluationMeasureType_COPY_INP_TMP.Dispose();
                hv_GroundTruthLabels_COPY_INP_TMP.Dispose();
                hv_Classes.Dispose();
                hv_TestClassesToEvaluate.Dispose();
                hv_NumEvalMeasureTypes.Dispose();
                hv_NumEvalClasses.Dispose();
                hv_ComputePrecision.Dispose();
                hv_ComputeRecall.Dispose();
                hv_ComputeFScore.Dispose();
                hv_ComputeConfusionMatrix.Dispose();
                hv_PredictedClasses.Dispose();
                hv_Index.Dispose();
                hv_PredictedClass.Dispose();
                hv_ConfusionMatrix.Dispose();
                hv_EvalMeasureTypeIndex.Dispose();
                hv_CurrentEvalMeasure.Dispose();
                hv_CurrentEvalClass.Dispose();
                hv_RegExpTopKError.Dispose();
                hv_ComputeTopKError.Dispose();
                hv_K.Dispose();
                hv_Indices.Dispose();
                hv_TopKError.Dispose();
                hv_NumClasses.Dispose();
                hv_IndexClass.Dispose();
                hv_ClassPrecisions.Dispose();
                hv_MatrixRowSumID.Dispose();
                hv_TruePositive.Dispose();
                hv_SumPredictedClass.Dispose();
                hv_ClassPrecision.Dispose();
                hv_Precision.Dispose();
                hv_ClassRecalls.Dispose();
                hv_MatrixColumnSumID.Dispose();
                hv_SumLabel.Dispose();
                hv_ClassRecall.Dispose();
                hv_Recall.Dispose();
                hv_FScore.Dispose();

                return;

            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_ClassesToEvaluate_COPY_INP_TMP.Dispose();
                hv_EvaluationMeasureType_COPY_INP_TMP.Dispose();
                hv_GroundTruthLabels_COPY_INP_TMP.Dispose();
                hv_Classes.Dispose();
                hv_TestClassesToEvaluate.Dispose();
                hv_NumEvalMeasureTypes.Dispose();
                hv_NumEvalClasses.Dispose();
                hv_ComputePrecision.Dispose();
                hv_ComputeRecall.Dispose();
                hv_ComputeFScore.Dispose();
                hv_ComputeConfusionMatrix.Dispose();
                hv_PredictedClasses.Dispose();
                hv_Index.Dispose();
                hv_PredictedClass.Dispose();
                hv_ConfusionMatrix.Dispose();
                hv_EvalMeasureTypeIndex.Dispose();
                hv_CurrentEvalMeasure.Dispose();
                hv_CurrentEvalClass.Dispose();
                hv_RegExpTopKError.Dispose();
                hv_ComputeTopKError.Dispose();
                hv_K.Dispose();
                hv_Indices.Dispose();
                hv_TopKError.Dispose();
                hv_NumClasses.Dispose();
                hv_IndexClass.Dispose();
                hv_ClassPrecisions.Dispose();
                hv_MatrixRowSumID.Dispose();
                hv_TruePositive.Dispose();
                hv_SumPredictedClass.Dispose();
                hv_ClassPrecision.Dispose();
                hv_Precision.Dispose();
                hv_ClassRecalls.Dispose();
                hv_MatrixColumnSumID.Dispose();
                hv_SumLabel.Dispose();
                hv_ClassRecall.Dispose();
                hv_Recall.Dispose();
                hv_FScore.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: XLD / Creation
        // Short Description: Creates an arrow shaped XLD contour. 
        public static void gen_arrow_contour_xld(out HObject ho_Arrow, HTuple hv_Row1, HTuple hv_Column1,
            HTuple hv_Row2, HTuple hv_Column2, HTuple hv_HeadLength, HTuple hv_HeadWidth)
        {



            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_TempArrow = null;

            // Local control variables 

            HTuple hv_Length = new HTuple(), hv_ZeroLengthIndices = new HTuple();
            HTuple hv_DR = new HTuple(), hv_DC = new HTuple(), hv_HalfHeadWidth = new HTuple();
            HTuple hv_RowP1 = new HTuple(), hv_ColP1 = new HTuple();
            HTuple hv_RowP2 = new HTuple(), hv_ColP2 = new HTuple();
            HTuple hv_Index = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_TempArrow);
            try
            {
                //This procedure generates arrow shaped XLD contours,
                //pointing from (Row1, Column1) to (Row2, Column2).
                //If starting and end point are identical, a contour consisting
                //of a single point is returned.
                //
                //input parameteres:
                //Row1, Column1: Coordinates of the arrows' starting points
                //Row2, Column2: Coordinates of the arrows' end points
                //HeadLength, HeadWidth: Size of the arrow heads in pixels
                //
                //output parameter:
                //Arrow: The resulting XLD contour
                //
                //The input tuples Row1, Column1, Row2, and Column2 have to be of
                //the same length.
                //HeadLength and HeadWidth either have to be of the same length as
                //Row1, Column1, Row2, and Column2 or have to be a single element.
                //If one of the above restrictions is violated, an error will occur.
                //
                //
                //Init
                ho_Arrow.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Arrow);
                //
                //Calculate the arrow length
                hv_Length.Dispose();
                HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);
                //
                //Mark arrows with identical start and end point
                //(set Length to -1 to avoid division-by-zero exception)
                hv_ZeroLengthIndices.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ZeroLengthIndices = hv_Length.TupleFind(
                        0);
                }
                if ((int)(new HTuple(hv_ZeroLengthIndices.TupleNotEqual(-1))) != 0)
                {
                    if (hv_Length == null)
                        hv_Length = new HTuple();
                    hv_Length[hv_ZeroLengthIndices] = -1;
                }
                //
                //Calculate auxiliary variables.
                hv_DR.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
                }
                hv_DC.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
                }
                hv_HalfHeadWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HalfHeadWidth = hv_HeadWidth / 2.0;
                }
                //
                //Calculate end points of the arrow head.
                hv_RowP1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
                }
                hv_ColP1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
                }
                hv_RowP2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
                }
                hv_ColP2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);
                }
                //
                //Finally create output XLD contour for each input point pair
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    if ((int)(new HTuple(((hv_Length.TupleSelect(hv_Index))).TupleEqual(-1))) != 0)
                    {
                        //Create_ single points for arrows with identical start and end point
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_TempArrow.Dispose();
                            HOperatorSet.GenContourPolygonXld(out ho_TempArrow, hv_Row1.TupleSelect(
                                hv_Index), hv_Column1.TupleSelect(hv_Index));
                        }
                    }
                    else
                    {
                        //Create arrow contour
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_TempArrow.Dispose();
                            HOperatorSet.GenContourPolygonXld(out ho_TempArrow, ((((((((((hv_Row1.TupleSelect(
                                hv_Index))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                                hv_RowP1.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                                hv_RowP2.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)),
                                ((((((((((hv_Column1.TupleSelect(hv_Index))).TupleConcat(hv_Column2.TupleSelect(
                                hv_Index)))).TupleConcat(hv_ColP1.TupleSelect(hv_Index)))).TupleConcat(
                                hv_Column2.TupleSelect(hv_Index)))).TupleConcat(hv_ColP2.TupleSelect(
                                hv_Index)))).TupleConcat(hv_Column2.TupleSelect(hv_Index)));
                        }
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConcatObj(ho_Arrow, ho_TempArrow, out ExpTmpOutVar_0);
                        ho_Arrow.Dispose();
                        ho_Arrow = ExpTmpOutVar_0;
                    }
                }
                ho_TempArrow.Dispose();

                hv_Length.Dispose();
                hv_ZeroLengthIndices.Dispose();
                hv_DR.Dispose();
                hv_DC.Dispose();
                hv_HalfHeadWidth.Dispose();
                hv_RowP1.Dispose();
                hv_ColP1.Dispose();
                hv_RowP2.Dispose();
                hv_ColP2.Dispose();
                hv_Index.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_TempArrow.Dispose();

                hv_Length.Dispose();
                hv_ZeroLengthIndices.Dispose();
                hv_DR.Dispose();
                hv_DC.Dispose();
                hv_HalfHeadWidth.Dispose();
                hv_RowP1.Dispose();
                hv_ColP1.Dispose();
                hv_RowP2.Dispose();
                hv_ColP2.Dispose();
                hv_Index.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Deep Learning / Classification
        // Short Description: Visualize and return the confusion matrix for the given labels.  
        public static void gen_confusion_matrix(HTuple hv_GroundTruthLabels, HTuple hv_PredictedClasses,
            HTuple hv_GenParamName, HTuple hv_GenParamValue, HTuple hv_WindowHandle, out HTuple hv_ConfusionMatrix)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_DisplayMatrix = new HTuple(), hv_ReturnMatrix = new HTuple();
            HTuple hv_DisplayColor = new HTuple(), hv_DisplayColumnWidth = new HTuple();
            HTuple hv_GenParamIndex = new HTuple(), hv_CalculateRelativeMatrix = new HTuple();
            HTuple hv_Classes = new HTuple(), hv_NumClasses = new HTuple();
            HTuple hv_AbsoluteMatrixID = new HTuple(), hv_RelativeMatrixID = new HTuple();
            HTuple hv_ColumnMatrix = new HTuple(), hv_Class = new HTuple();
            HTuple hv_ThisLabel = new HTuple(), hv_NumClassGroundTruth = new HTuple();
            HTuple hv_RowMatrix = new HTuple(), hv_PredictedClass = new HTuple();
            HTuple hv_ThisPredictedClass = new HTuple(), hv_NumMatches = new HTuple();
            HTuple hv_RelativeError = new HTuple(), hv_StringWidths = new HTuple();
            HTuple hv_StringIndex = new HTuple(), hv_String = new HTuple();
            HTuple hv_Ascent = new HTuple(), hv_Descent = new HTuple();
            HTuple hv_StringWidth = new HTuple(), hv_StringHeight = new HTuple();
            HTuple hv_MaxStringWidth = new HTuple(), hv_RowStart = new HTuple();
            HTuple hv_RowDistance = new HTuple(), hv_RowEnd = new HTuple();
            HTuple hv_ColumnStart = new HTuple(), hv_ColumnOffset = new HTuple();
            HTuple hv_ColumnEnd = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_WidthLimit = new HTuple();
            HTuple hv_HeightLimit = new HTuple(), hv_TextRow = new HTuple();
            HTuple hv_TextColumn = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Text = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_AbsoluteTransposedMatrixID = new HTuple(), hv_MatrixText = new HTuple();
            HTuple hv_MatrixMaxID = new HTuple(), hv_MaxValue = new HTuple();
            HTuple hv_StringConversion = new HTuple(), hv_RelativeTransposedMatrixID = new HTuple();
            HTuple hv_TextColor = new HTuple(), hv_RelativeValues = new HTuple();
            HTuple hv_Thresholds = new HTuple(), hv_Colors = new HTuple();
            HTuple hv_Greater = new HTuple(), hv_Indices = new HTuple();
            HTuple hv_DiagonalIndex = new HTuple(), hv_Value = new HTuple();
            // Initialize local and output iconic variables 
            hv_ConfusionMatrix = new HTuple();
            try
            {
                //This procedure computes a confusion matrix.
                //Therefore, it compares the classes
                //assigned in GroundTruthLabels and PredictedClasses.
                //The resulting confusion matrix can be
                //visualized, returned, or both.
                //In each case, the output can be changed
                //via generic parameters using GenParamName and GenParamValue.
                //For the visualization, the graphics window
                //must be specified with WindowHandle.
                //
                if ((int)(new HTuple((new HTuple(hv_GroundTruthLabels.TupleLength())).TupleNotEqual(
                    new HTuple(hv_PredictedClasses.TupleLength())))) != 0)
                {
                    throw new HalconException("Number of ground truth labels and predicted classes must be equal.");
                }
                //
                //Set generic parameter defaults.
                hv_DisplayMatrix.Dispose();
                hv_DisplayMatrix = "absolute";
                hv_ReturnMatrix.Dispose();
                hv_ReturnMatrix = "absolute";
                hv_DisplayColor.Dispose();
                hv_DisplayColor = "true";
                hv_DisplayColumnWidth.Dispose();
                hv_DisplayColumnWidth = "minimal";
                //
                //Parse generic parameters.
                for (hv_GenParamIndex = 0; (int)hv_GenParamIndex <= (int)((new HTuple(hv_GenParamName.TupleLength()
                    )) - 1); hv_GenParamIndex = (int)hv_GenParamIndex + 1)
                {
                    if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "display_matrix"))) != 0)
                    {
                        //Set 'display_matrix'.
                        hv_DisplayMatrix.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_DisplayMatrix = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "return_matrix"))) != 0)
                    {
                        //Set 'return_matrix'.
                        hv_ReturnMatrix.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_ReturnMatrix = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "display_color"))) != 0)
                    {
                        //Set 'display_color'.
                        hv_DisplayColor.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_DisplayColor = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "display_column_width"))) != 0)
                    {
                        //Set 'display_column_width'.
                        hv_DisplayColumnWidth.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_DisplayColumnWidth = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                    }
                    else
                    {
                        throw new HalconException(("Unknown generic parameter: '" + (hv_GenParamName.TupleSelect(
                            hv_GenParamIndex))) + "'");
                    }
                }
                //
                if ((int)((new HTuple((new HTuple(hv_DisplayMatrix.TupleEqual("relative"))).TupleOr(
                    new HTuple(hv_ReturnMatrix.TupleEqual("relative"))))).TupleOr(new HTuple(hv_DisplayColor.TupleEqual(
                    "true")))) != 0)
                {
                    hv_CalculateRelativeMatrix.Dispose();
                    hv_CalculateRelativeMatrix = 1;
                }
                else
                {
                    hv_CalculateRelativeMatrix.Dispose();
                    hv_CalculateRelativeMatrix = 0;
                }
                //
                //Calculate the confusion matrix with absolute values
                //and the confusion matrix with relative errors.
                //We start with an empty matrix
                //and add the number of matching labels.
                hv_Classes.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Classes = ((hv_GroundTruthLabels.TupleSort()
                        )).TupleUniq();
                }
                hv_NumClasses.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumClasses = new HTuple(hv_Classes.TupleLength()
                        );
                }
                hv_AbsoluteMatrixID.Dispose();
                HOperatorSet.CreateMatrix(hv_NumClasses, hv_NumClasses, 0, out hv_AbsoluteMatrixID);
                if ((int)(hv_CalculateRelativeMatrix) != 0)
                {
                    hv_RelativeMatrixID.Dispose();
                    HOperatorSet.CreateMatrix(hv_NumClasses, hv_NumClasses, 0, out hv_RelativeMatrixID);
                }
                HTuple end_val55 = hv_NumClasses - 1;
                HTuple step_val55 = 1;
                for (hv_ColumnMatrix = 0; hv_ColumnMatrix.Continue(end_val55, step_val55); hv_ColumnMatrix = hv_ColumnMatrix.TupleAdd(step_val55))
                {
                    hv_Class.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Class = hv_Classes.TupleSelect(
                            hv_ColumnMatrix);
                    }
                    hv_ThisLabel.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ThisLabel = hv_GroundTruthLabels.TupleEqualElem(
                            hv_Class);
                    }
                    if ((int)(hv_CalculateRelativeMatrix) != 0)
                    {
                        //Obtain the number of ground truth labels per class.
                        hv_NumClassGroundTruth.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_NumClassGroundTruth = hv_ThisLabel.TupleSum()
                                ;
                        }
                    }
                    HTuple end_val62 = hv_NumClasses - 1;
                    HTuple step_val62 = 1;
                    for (hv_RowMatrix = 0; hv_RowMatrix.Continue(end_val62, step_val62); hv_RowMatrix = hv_RowMatrix.TupleAdd(step_val62))
                    {
                        //Select classes for this row/column.
                        hv_PredictedClass.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_PredictedClass = hv_Classes.TupleSelect(
                                hv_RowMatrix);
                        }
                        //Check whether the input data
                        //corresponds to these classes.
                        hv_ThisPredictedClass.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_ThisPredictedClass = hv_PredictedClasses.TupleEqualElem(
                                hv_PredictedClass);
                        }
                        //Count the number of elements where the predicted class
                        //matches the ground truth label.
                        hv_NumMatches.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_NumMatches = ((((hv_ThisLabel + hv_ThisPredictedClass)).TupleEqualElem(
                                2))).TupleSum();
                        }
                        //Set value in matrix.
                        HOperatorSet.SetValueMatrix(hv_AbsoluteMatrixID, hv_RowMatrix, hv_ColumnMatrix,
                            hv_NumMatches);
                        if ((int)(hv_CalculateRelativeMatrix) != 0)
                        {
                            if ((int)(new HTuple(hv_NumClassGroundTruth.TupleGreater(0))) != 0)
                            {
                                hv_RelativeError.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_RelativeError = (hv_NumMatches.TupleReal()
                                        ) / hv_NumClassGroundTruth;
                                }
                            }
                            else
                            {
                                hv_RelativeError.Dispose();
                                hv_RelativeError = 0;
                            }
                            HOperatorSet.SetValueMatrix(hv_RelativeMatrixID, hv_RowMatrix, hv_ColumnMatrix,
                                hv_RelativeError);
                        }
                    }
                }
                //
                //Return the result.
                if ((int)(new HTuple(hv_ReturnMatrix.TupleEqual("absolute"))) != 0)
                {
                    hv_ConfusionMatrix.Dispose();
                    HOperatorSet.CopyMatrix(hv_AbsoluteMatrixID, out hv_ConfusionMatrix);
                }
                else if ((int)(new HTuple(hv_ReturnMatrix.TupleEqual("relative"))) != 0)
                {
                    hv_ConfusionMatrix.Dispose();
                    HOperatorSet.CopyMatrix(hv_RelativeMatrixID, out hv_ConfusionMatrix);
                }
                else if ((int)(new HTuple(hv_ReturnMatrix.TupleEqual("none"))) != 0)
                {
                    //No matrix is returned.
                }
                else
                {
                    throw new HalconException("Unsupported mode for 'return_matrix'");
                }
                //
                //Display the matrix.
                if ((int)(new HTuple(hv_DisplayMatrix.TupleNotEqual("none"))) != 0)
                {
                    //
                    //Find maximal string width and set display position parameters.
                    hv_StringWidths.Dispose();
                    hv_StringWidths = new HTuple();
                    //Get the string width of each class.
                    for (hv_StringIndex = 0; (int)hv_StringIndex <= (int)((new HTuple(hv_Classes.TupleLength()
                        )) - 1); hv_StringIndex = (int)hv_StringIndex + 1)
                    {
                        hv_String.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_String = hv_Classes.TupleSelect(
                                hv_StringIndex);
                        }
                        hv_Ascent.Dispose(); hv_Descent.Dispose(); hv_StringWidth.Dispose(); hv_StringHeight.Dispose();
                        HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String, out hv_Ascent,
                            out hv_Descent, out hv_StringWidth, out hv_StringHeight);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_StringWidths = hv_StringWidths.TupleConcat(
                                    hv_StringWidth);
                                hv_StringWidths.Dispose();
                                hv_StringWidths = ExpTmpLocalVar_StringWidths;
                            }
                        }
                    }
                    //The columns should have a minimum width for 4 characters.
                    hv_Ascent.Dispose(); hv_Descent.Dispose(); hv_StringWidth.Dispose(); hv_StringHeight.Dispose();
                    HOperatorSet.GetStringExtents(hv_WindowHandle, "test", out hv_Ascent, out hv_Descent,
                        out hv_StringWidth, out hv_StringHeight);
                    hv_MaxStringWidth.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_MaxStringWidth = ((hv_StringWidths.TupleMax()
                            )).TupleMax2(hv_StringWidth);
                    }
                    //Get the maximum string width
                    //and resize the window accordingly.
                    hv_RowStart.Dispose();
                    hv_RowStart = 80;
                    hv_RowDistance.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_RowDistance = hv_StringHeight + 10;
                    }
                    hv_RowEnd.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_RowEnd = hv_StringHeight * 7;
                    }
                    hv_ColumnStart.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        //
                        hv_ColumnStart = 50 + hv_MaxStringWidth;//
                    }
                    hv_ColumnOffset.Dispose();
                    hv_ColumnOffset = 40;
                    hv_ColumnEnd.Dispose();
                    hv_ColumnEnd = new HTuple(hv_ColumnOffset);
                    //
                    //Adapt the window size to fit the confusion matrix.
                    if ((int)(new HTuple(hv_DisplayColumnWidth.TupleEqual("minimal"))) != 0)
                    {
                        //Every column of the confusion matrix is as narrow as possible
                        //based to the respective string widths.
                        hv_Width.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Width = (((hv_StringWidths.TupleSum()
                                ) + (hv_ColumnOffset * hv_NumClasses)) + hv_ColumnStart) + hv_ColumnEnd;
                        }
                    }
                    else if ((int)(new HTuple(hv_DisplayColumnWidth.TupleEqual("equal"))) != 0)
                    {
                        //Every column of the confusion matrix should have the same width.
                        //based on the maximum string width.
                        hv_Width.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Width = (((hv_MaxStringWidth + hv_ColumnOffset) * hv_NumClasses) + hv_ColumnStart) + hv_ColumnEnd;
                        }
                    }
                    else
                    {
                        throw new HalconException("");
                    }
                    hv_Height.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Height = ((hv_RowDistance * hv_NumClasses) + hv_RowStart) + hv_RowEnd;
                    }
                    HDevWindowStack.SetActive(hv_WindowHandle);
                    //if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.ClearWindow(hv_WindowHandle);
                    }
                    //
                    //Set reasonable limits for graphics window (adapt if necessary).
                    hv_WidthLimit.Dispose();
                    hv_WidthLimit = new HTuple();
                    hv_WidthLimit[0] = 450;
                    hv_WidthLimit[1] = 1920;
                    hv_HeightLimit.Dispose();
                    hv_HeightLimit = new HTuple();
                    hv_HeightLimit[0] = 250;
                    hv_HeightLimit[1] = 1080;
                    if ((int)((new HTuple(hv_Width.TupleGreater(hv_WidthLimit.TupleSelect(1)))).TupleOr(
                        new HTuple(hv_Height.TupleGreater(hv_HeightLimit.TupleSelect(1))))) != 0)
                    {
                        throw new HalconException("Confusion Matrix does not fit into graphics window. Please adapt font and/or size limits.");
                    }
                    //dev_resize_window_fit_size(0, 0, hv_Width, hv_Height, hv_WidthLimit, hv_HeightLimit);
                    //
                    //Get display coordinates.
                    //Get row coordinates for display.
                    hv_TextRow.Dispose();
                    hv_TextRow = new HTuple();
                    HTuple end_val145 = hv_NumClasses - 1;
                    HTuple step_val145 = 1;
                    for (hv_ColumnMatrix = 0; hv_ColumnMatrix.Continue(end_val145, step_val145); hv_ColumnMatrix = hv_ColumnMatrix.TupleAdd(step_val145))
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_TextRow = hv_TextRow.TupleConcat(
                                    HTuple.TupleGenSequence(0, (hv_NumClasses - 1) * hv_RowDistance, hv_RowDistance));
                                hv_TextRow.Dispose();
                                hv_TextRow = ExpTmpLocalVar_TextRow;
                            }
                        }
                    }
                    //Get column coordinates for display.
                    hv_TextColumn.Dispose();
                    hv_TextColumn = new HTuple();
                    HTuple end_val150 = hv_NumClasses - 1;
                    HTuple step_val150 = 1;
                    for (hv_Index = 0; hv_Index.Continue(end_val150, step_val150); hv_Index = hv_Index.TupleAdd(step_val150))
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_TextColumn = hv_TextColumn.TupleConcat(
                                    HTuple.TupleGenConst(hv_NumClasses, hv_ColumnStart));
                                hv_TextColumn.Dispose();
                                hv_TextColumn = ExpTmpLocalVar_TextColumn;
                            }
                        }
                        if ((int)(new HTuple(hv_DisplayColumnWidth.TupleEqual("minimal"))) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_ColumnStart = (hv_ColumnStart + (hv_StringWidths.TupleSelect(
                                        hv_Index))) + hv_ColumnOffset;
                                    hv_ColumnStart.Dispose();
                                    hv_ColumnStart = ExpTmpLocalVar_ColumnStart;
                                }
                            }
                        }
                        else if ((int)(new HTuple(hv_DisplayColumnWidth.TupleEqual(
                            "equal"))) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_ColumnStart = (hv_ColumnStart + hv_MaxStringWidth) + hv_ColumnOffset;
                                    hv_ColumnStart.Dispose();
                                    hv_ColumnStart = ExpTmpLocalVar_ColumnStart;
                                }
                            }
                        }
                    }
                    //Display the confusion matrix with a margin from the top.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_TextRow = hv_TextRow + hv_RowStart;
                            hv_TextRow.Dispose();
                            hv_TextRow = ExpTmpLocalVar_TextRow;
                        }
                    }
                    //Display axis titles.
                    //if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispText(hv_WindowHandle, "Ground truth labels",
                            "window", "top", "right", "white", "box", "false");
                    }
                    //if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispText(hv_WindowHandle, "Predicted classes",
                            "window", "bottom", "left", "white", "box", "false");
                    }
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Classes.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        hv_Text.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Text = hv_Classes.TupleSelect(
                                hv_Index);
                        }
                        //Display predicted class names.
                        hv_Row.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row = hv_TextRow.TupleSelect(
                                hv_Index);
                            HOperatorSet.GetStringExtents(hv_WindowHandle, hv_Text, out hv_Ascent,
                                out hv_Descent, out hv_StringWidth, out hv_StringHeight);
                        }
                        hv_Column.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Column = ((hv_TextColumn.TupleSelect(
                                0)) - hv_MaxStringWidth)-hv_StringWidth;
                        }
                        //if (HDevWindowStack.IsOpen())
                        {
                            HOperatorSet.DispText(hv_WindowHandle, hv_Text, "window",
                                hv_Row, hv_Column, "light gray", "box", "false");
                        }
                        //Display ground truth label names.
                        hv_Row.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Row = (hv_TextRow.TupleSelect(
                                0)) - hv_RowDistance;
                        }
                        hv_Column.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Column = hv_TextColumn.TupleSelect(
                                hv_Index * hv_NumClasses)+20-hv_StringWidth;
                        }
                        //if (HDevWindowStack.IsOpen())
                        {
                            HOperatorSet.DispText(hv_WindowHandle, hv_Text, "window",
                                hv_Row, hv_Column, "light gray", "box", "false");
                        }
                    }
                    //
                    //Get the confusion matrix values for display.
                    if ((int)(new HTuple(hv_DisplayMatrix.TupleEqual("absolute"))) != 0)
                    {
                        //Displayed matrix corresponds to the transposed returned matrix.
                        hv_AbsoluteTransposedMatrixID.Dispose();
                        HOperatorSet.TransposeMatrix(hv_AbsoluteMatrixID, out hv_AbsoluteTransposedMatrixID);
                        hv_MatrixText.Dispose();
                        HOperatorSet.GetFullMatrix(hv_AbsoluteTransposedMatrixID, out hv_MatrixText);
                        HOperatorSet.ClearMatrix(hv_AbsoluteTransposedMatrixID);
                        //Align the numbers right.
                        hv_MatrixMaxID.Dispose();
                        HOperatorSet.MaxMatrix(hv_AbsoluteMatrixID, "full", out hv_MatrixMaxID);
                        hv_MaxValue.Dispose();
                        HOperatorSet.GetFullMatrix(hv_MatrixMaxID, out hv_MaxValue);
                        HOperatorSet.ClearMatrix(hv_MatrixMaxID);
                        hv_StringConversion.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_StringConversion = (((((hv_MaxValue.TupleLog10()
                                )).TupleCeil())).TupleInt()) + ".0f";
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_MatrixText = hv_MatrixText.TupleString(
                                    hv_StringConversion);
                                hv_MatrixText.Dispose();
                                hv_MatrixText = ExpTmpLocalVar_MatrixText;
                            }
                        }
                    }
                    else
                    {
                        //Displayed matrix corresponds to the transposed returned matrix.
                        hv_RelativeTransposedMatrixID.Dispose();
                        HOperatorSet.TransposeMatrix(hv_RelativeMatrixID, out hv_RelativeTransposedMatrixID);
                        hv_MatrixText.Dispose();
                        HOperatorSet.GetFullMatrix(hv_RelativeTransposedMatrixID, out hv_MatrixText);
                        HOperatorSet.ClearMatrix(hv_RelativeTransposedMatrixID);
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_MatrixText = hv_MatrixText.TupleString(
                                    ".2f");
                                hv_MatrixText.Dispose();
                                hv_MatrixText = ExpTmpLocalVar_MatrixText;
                            }
                        }
                    }
                    //Set color for displayed confusion matrix.
                    if ((int)(new HTuple(hv_DisplayColor.TupleEqual("true"))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_TextColor.Dispose();
                            HOperatorSet.TupleGenConst(new HTuple(hv_MatrixText.TupleLength()), "#666666",
                                out hv_TextColor);
                        }
                        //Use the relative values to adapt the color of the text.
                        hv_RelativeTransposedMatrixID.Dispose();
                        HOperatorSet.TransposeMatrix(hv_RelativeMatrixID, out hv_RelativeTransposedMatrixID);
                        hv_RelativeValues.Dispose();
                        HOperatorSet.GetFullMatrix(hv_RelativeTransposedMatrixID, out hv_RelativeValues);
                        HOperatorSet.ClearMatrix(hv_RelativeTransposedMatrixID);
                        //Set the colors and respective thresholds for the off-diagonal values.
                        hv_Thresholds.Dispose();
                        hv_Thresholds = new HTuple();
                        hv_Thresholds[0] = 0.0;
                        hv_Thresholds[1] = 0.05;
                        hv_Thresholds[2] = 0.1;
                        hv_Thresholds[3] = 0.2;
                        hv_Colors.Dispose();
                        hv_Colors = new HTuple();
                        hv_Colors[0] = "#8C4D4D";
                        hv_Colors[1] = "#B33333";
                        hv_Colors[2] = "#D91A1A";
                        hv_Colors[3] = "#FF0000";
                        for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Thresholds.TupleLength()
                            )) - 1); hv_Index = (int)hv_Index + 1)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Greater.Dispose();
                                HOperatorSet.TupleGreaterElem(hv_RelativeValues, hv_Thresholds.TupleSelect(
                                    hv_Index), out hv_Greater);
                            }
                            hv_Indices.Dispose();
                            HOperatorSet.TupleFind(hv_Greater, 1, out hv_Indices);
                            if ((int)(new HTuple(hv_Indices.TupleNotEqual(-1))) != 0)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HTuple ExpTmpOutVar_0;
                                    HOperatorSet.TupleReplace(hv_TextColor, hv_Indices, hv_Colors.TupleSelect(
                                        hv_Index), out ExpTmpOutVar_0);
                                    hv_TextColor.Dispose();
                                    hv_TextColor = ExpTmpOutVar_0;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        //Set the colors and respective thresholds for the diagonal values.
                        hv_Thresholds.Dispose();
                        hv_Thresholds = new HTuple();
                        hv_Thresholds[0] = -0.01;
                        hv_Thresholds[1] = 0.60;
                        hv_Thresholds[2] = 0.80;
                        hv_Thresholds[3] = 0.90;
                        hv_Thresholds[4] = 0.95;
                        hv_Thresholds[5] = 0.98;
                        hv_Colors.Dispose();
                        hv_Colors = new HTuple();
                        hv_Colors[0] = "#666666";
                        hv_Colors[1] = "#508650";
                        hv_Colors[2] = "#419C41";
                        hv_Colors[3] = "#2BBD2B";
                        hv_Colors[4] = "#15DE15";
                        hv_Colors[5] = "#00FF00";
                        HTuple end_val216 = hv_NumClasses - 1;
                        HTuple step_val216 = 1;
                        for (hv_DiagonalIndex = 0; hv_DiagonalIndex.Continue(end_val216, step_val216); hv_DiagonalIndex = hv_DiagonalIndex.TupleAdd(step_val216))
                        {
                            hv_Value.Dispose();
                            HOperatorSet.GetValueMatrix(hv_RelativeMatrixID, hv_DiagonalIndex, hv_DiagonalIndex,
                                out hv_Value);
                            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Thresholds.TupleLength()
                                )) - 1); hv_Index = (int)hv_Index + 1)
                            {
                                if ((int)(new HTuple(hv_Value.TupleGreater(hv_Thresholds.TupleSelect(
                                    hv_Index)))) != 0)
                                {
                                    if (hv_TextColor == null)
                                        hv_TextColor = new HTuple();
                                    hv_TextColor[hv_DiagonalIndex * (hv_NumClasses + 1)] = hv_Colors.TupleSelect(
                                        hv_Index);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //Default value for the text color.
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_TextColor.Dispose();
                            HOperatorSet.TupleGenConst(new HTuple(hv_MatrixText.TupleLength()), "white",
                                out hv_TextColor);
                        }
                    }
                    //
                    //Display confusion matrix.
                    //if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispText(hv_WindowHandle, hv_MatrixText, "window",
                            hv_TextRow, hv_TextColumn, hv_TextColor, "box", "false");
                    }
                    //
                    //Clean up.
                    if ((int)(hv_CalculateRelativeMatrix) != 0)
                    {
                        HOperatorSet.ClearMatrix(hv_RelativeMatrixID);
                    }
                    HOperatorSet.ClearMatrix(hv_AbsoluteMatrixID);
                }

                hv_DisplayMatrix.Dispose();
                hv_ReturnMatrix.Dispose();
                hv_DisplayColor.Dispose();
                hv_DisplayColumnWidth.Dispose();
                hv_GenParamIndex.Dispose();
                hv_CalculateRelativeMatrix.Dispose();
                hv_Classes.Dispose();
                hv_NumClasses.Dispose();
                hv_AbsoluteMatrixID.Dispose();
                hv_RelativeMatrixID.Dispose();
                hv_ColumnMatrix.Dispose();
                hv_Class.Dispose();
                hv_ThisLabel.Dispose();
                hv_NumClassGroundTruth.Dispose();
                hv_RowMatrix.Dispose();
                hv_PredictedClass.Dispose();
                hv_ThisPredictedClass.Dispose();
                hv_NumMatches.Dispose();
                hv_RelativeError.Dispose();
                hv_StringWidths.Dispose();
                hv_StringIndex.Dispose();
                hv_String.Dispose();
                hv_Ascent.Dispose();
                hv_Descent.Dispose();
                hv_StringWidth.Dispose();
                hv_StringHeight.Dispose();
                hv_MaxStringWidth.Dispose();
                hv_RowStart.Dispose();
                hv_RowDistance.Dispose();
                hv_RowEnd.Dispose();
                hv_ColumnStart.Dispose();
                hv_ColumnOffset.Dispose();
                hv_ColumnEnd.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_WidthLimit.Dispose();
                hv_HeightLimit.Dispose();
                hv_TextRow.Dispose();
                hv_TextColumn.Dispose();
                hv_Index.Dispose();
                hv_Text.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_AbsoluteTransposedMatrixID.Dispose();
                hv_MatrixText.Dispose();
                hv_MatrixMaxID.Dispose();
                hv_MaxValue.Dispose();
                hv_StringConversion.Dispose();
                hv_RelativeTransposedMatrixID.Dispose();
                hv_TextColor.Dispose();
                hv_RelativeValues.Dispose();
                hv_Thresholds.Dispose();
                hv_Colors.Dispose();
                hv_Greater.Dispose();
                hv_Indices.Dispose();
                hv_DiagonalIndex.Dispose();
                hv_Value.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_DisplayMatrix.Dispose();
                hv_ReturnMatrix.Dispose();
                hv_DisplayColor.Dispose();
                hv_DisplayColumnWidth.Dispose();
                hv_GenParamIndex.Dispose();
                hv_CalculateRelativeMatrix.Dispose();
                hv_Classes.Dispose();
                hv_NumClasses.Dispose();
                hv_AbsoluteMatrixID.Dispose();
                hv_RelativeMatrixID.Dispose();
                hv_ColumnMatrix.Dispose();
                hv_Class.Dispose();
                hv_ThisLabel.Dispose();
                hv_NumClassGroundTruth.Dispose();
                hv_RowMatrix.Dispose();
                hv_PredictedClass.Dispose();
                hv_ThisPredictedClass.Dispose();
                hv_NumMatches.Dispose();
                hv_RelativeError.Dispose();
                hv_StringWidths.Dispose();
                hv_StringIndex.Dispose();
                hv_String.Dispose();
                hv_Ascent.Dispose();
                hv_Descent.Dispose();
                hv_StringWidth.Dispose();
                hv_StringHeight.Dispose();
                hv_MaxStringWidth.Dispose();
                hv_RowStart.Dispose();
                hv_RowDistance.Dispose();
                hv_RowEnd.Dispose();
                hv_ColumnStart.Dispose();
                hv_ColumnOffset.Dispose();
                hv_ColumnEnd.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_WidthLimit.Dispose();
                hv_HeightLimit.Dispose();
                hv_TextRow.Dispose();
                hv_TextColumn.Dispose();
                hv_Index.Dispose();
                hv_Text.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_AbsoluteTransposedMatrixID.Dispose();
                hv_MatrixText.Dispose();
                hv_MatrixMaxID.Dispose();
                hv_MaxValue.Dispose();
                hv_StringConversion.Dispose();
                hv_RelativeTransposedMatrixID.Dispose();
                hv_TextColor.Dispose();
                hv_RelativeValues.Dispose();
                hv_Thresholds.Dispose();
                hv_Colors.Dispose();
                hv_Greater.Dispose();
                hv_Indices.Dispose();
                hv_DiagonalIndex.Dispose();
                hv_Value.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: File / Misc
        // Short Description: Get all image files under the given path 
        public static void list_image_files(HTuple hv_ImageDirectory, HTuple hv_Extensions, HTuple hv_Options,
            out HTuple hv_ImageFiles)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_ImageDirectoryIndex = new HTuple();
            HTuple hv_ImageFilesTmp = new HTuple(), hv_CurrentImageDirectory = new HTuple();
            HTuple hv_HalconImages = new HTuple(), hv_OS = new HTuple();
            HTuple hv_Directories = new HTuple(), hv_Index = new HTuple();
            HTuple hv_Length = new HTuple(), hv_NetworkDrive = new HTuple();
            HTuple hv_Substring = new HTuple(), hv_FileExists = new HTuple();
            HTuple hv_AllFiles = new HTuple(), hv_i = new HTuple();
            HTuple hv_Selection = new HTuple();
            HTuple hv_Extensions_COPY_INP_TMP = new HTuple(hv_Extensions);

            // Initialize local and output iconic variables 
            hv_ImageFiles = new HTuple();
            try
            {
                //This procedure returns all files in a given directory
                //with one of the suffixes specified in Extensions.
                //
                //Input parameters:
                //ImageDirectory: Directory or a tuple of directories with images.
                //   If a directory is not found locally, the respective directory
                //   is searched under %HALCONIMAGES%/ImageDirectory.
                //   See the Installation Guide for further information
                //   in case %HALCONIMAGES% is not set.
                //Extensions: A string tuple containing the extensions to be found
                //   e.g. ['png','tif',jpg'] or others
                //If Extensions is set to 'default' or the empty string '',
                //   all image suffixes supported by HALCON are used.
                //Options: as in the operator list_files, except that the 'files'
                //   option is always used. Note that the 'directories' option
                //   has no effect but increases runtime, because only files are
                //   returned.
                //
                //Output parameter:
                //ImageFiles: A tuple of all found image file names
                //
                if ((int)((new HTuple((new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(""))))).TupleOr(new HTuple(hv_Extensions_COPY_INP_TMP.TupleEqual(
                    "default")))) != 0)
                {
                    hv_Extensions_COPY_INP_TMP.Dispose();
                    hv_Extensions_COPY_INP_TMP = new HTuple();
                    hv_Extensions_COPY_INP_TMP[0] = "ima";
                    hv_Extensions_COPY_INP_TMP[1] = "tif";
                    hv_Extensions_COPY_INP_TMP[2] = "tiff";
                    hv_Extensions_COPY_INP_TMP[3] = "gif";
                    hv_Extensions_COPY_INP_TMP[4] = "bmp";
                    hv_Extensions_COPY_INP_TMP[5] = "jpg";
                    hv_Extensions_COPY_INP_TMP[6] = "jpeg";
                    hv_Extensions_COPY_INP_TMP[7] = "jp2";
                    hv_Extensions_COPY_INP_TMP[8] = "jxr";
                    hv_Extensions_COPY_INP_TMP[9] = "png";
                    hv_Extensions_COPY_INP_TMP[10] = "pcx";
                    hv_Extensions_COPY_INP_TMP[11] = "ras";
                    hv_Extensions_COPY_INP_TMP[12] = "xwd";
                    hv_Extensions_COPY_INP_TMP[13] = "pbm";
                    hv_Extensions_COPY_INP_TMP[14] = "pnm";
                    hv_Extensions_COPY_INP_TMP[15] = "pgm";
                    hv_Extensions_COPY_INP_TMP[16] = "ppm";
                    //
                }
                hv_ImageFiles.Dispose();
                hv_ImageFiles = new HTuple();
                //Loop through all given image directories.
                for (hv_ImageDirectoryIndex = 0; (int)hv_ImageDirectoryIndex <= (int)((new HTuple(hv_ImageDirectory.TupleLength()
                    )) - 1); hv_ImageDirectoryIndex = (int)hv_ImageDirectoryIndex + 1)
                {
                    hv_ImageFilesTmp.Dispose();
                    hv_ImageFilesTmp = new HTuple();
                    hv_CurrentImageDirectory.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_CurrentImageDirectory = hv_ImageDirectory.TupleSelect(
                            hv_ImageDirectoryIndex);
                    }
                    if ((int)(new HTuple(hv_CurrentImageDirectory.TupleEqual(""))) != 0)
                    {
                        hv_CurrentImageDirectory.Dispose();
                        hv_CurrentImageDirectory = ".";
                    }
                    hv_HalconImages.Dispose();
                    HOperatorSet.GetSystem("image_dir", out hv_HalconImages);
                    hv_OS.Dispose();
                    HOperatorSet.GetSystem("operating_system", out hv_OS);
                    if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_HalconImages = hv_HalconImages.TupleSplit(
                                    ";");
                                hv_HalconImages.Dispose();
                                hv_HalconImages = ExpTmpLocalVar_HalconImages;
                            }
                        }
                    }
                    else
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_HalconImages = hv_HalconImages.TupleSplit(
                                    ":");
                                hv_HalconImages.Dispose();
                                hv_HalconImages = ExpTmpLocalVar_HalconImages;
                            }
                        }
                    }
                    hv_Directories.Dispose();
                    hv_Directories = new HTuple(hv_CurrentImageDirectory);
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_HalconImages.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_Directories = hv_Directories.TupleConcat(
                                    ((hv_HalconImages.TupleSelect(hv_Index)) + "/") + hv_CurrentImageDirectory);
                                hv_Directories.Dispose();
                                hv_Directories = ExpTmpLocalVar_Directories;
                            }
                        }
                    }
                    hv_Length.Dispose();
                    HOperatorSet.TupleStrlen(hv_Directories, out hv_Length);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_NetworkDrive.Dispose();
                        HOperatorSet.TupleGenConst(new HTuple(hv_Length.TupleLength()), 0, out hv_NetworkDrive);
                    }
                    if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                    {
                        for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength()
                            )) - 1); hv_Index = (int)hv_Index + 1)
                        {
                            if ((int)(new HTuple(((((hv_Directories.TupleSelect(hv_Index))).TupleStrlen()
                                )).TupleGreater(1))) != 0)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_Substring.Dispose();
                                    HOperatorSet.TupleStrFirstN(hv_Directories.TupleSelect(hv_Index), 1,
                                        out hv_Substring);
                                }
                                if ((int)((new HTuple(hv_Substring.TupleEqual("//"))).TupleOr(new HTuple(hv_Substring.TupleEqual(
                                    "\\\\")))) != 0)
                                {
                                    if (hv_NetworkDrive == null)
                                        hv_NetworkDrive = new HTuple();
                                    hv_NetworkDrive[hv_Index] = 1;
                                }
                            }
                        }
                    }
                    hv_ImageFilesTmp.Dispose();
                    hv_ImageFilesTmp = new HTuple();
                    for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Directories.TupleLength()
                        )) - 1); hv_Index = (int)hv_Index + 1)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_FileExists.Dispose();
                            HOperatorSet.FileExists(hv_Directories.TupleSelect(hv_Index), out hv_FileExists);
                        }
                        if ((int)(hv_FileExists) != 0)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_AllFiles.Dispose();
                                HOperatorSet.ListFiles(hv_Directories.TupleSelect(hv_Index), (new HTuple("files")).TupleConcat(
                                    hv_Options), out hv_AllFiles);
                            }
                            hv_ImageFilesTmp.Dispose();
                            hv_ImageFilesTmp = new HTuple();
                            for (hv_i = 0; (int)hv_i <= (int)((new HTuple(hv_Extensions_COPY_INP_TMP.TupleLength()
                                )) - 1); hv_i = (int)hv_i + 1)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_Selection.Dispose();
                                    HOperatorSet.TupleRegexpSelect(hv_AllFiles, (((".*" + (hv_Extensions_COPY_INP_TMP.TupleSelect(
                                        hv_i))) + "$")).TupleConcat("ignore_case"), out hv_Selection);
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_ImageFilesTmp = hv_ImageFilesTmp.TupleConcat(
                                            hv_Selection);
                                        hv_ImageFilesTmp.Dispose();
                                        hv_ImageFilesTmp = ExpTmpLocalVar_ImageFilesTmp;
                                    }
                                }
                            }
                            {
                                HTuple ExpTmpOutVar_0;
                                HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("\\\\")).TupleConcat(
                                    "replace_all"), "/", out ExpTmpOutVar_0);
                                hv_ImageFilesTmp.Dispose();
                                hv_ImageFilesTmp = ExpTmpOutVar_0;
                            }
                            if ((int)(hv_NetworkDrive.TupleSelect(hv_Index)) != 0)
                            {
                                {
                                    HTuple ExpTmpOutVar_0;
                                    HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("//")).TupleConcat(
                                        "replace_all"), "/", out ExpTmpOutVar_0);
                                    hv_ImageFilesTmp.Dispose();
                                    hv_ImageFilesTmp = ExpTmpOutVar_0;
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_ImageFilesTmp = "/" + hv_ImageFilesTmp;
                                        hv_ImageFilesTmp.Dispose();
                                        hv_ImageFilesTmp = ExpTmpLocalVar_ImageFilesTmp;
                                    }
                                }
                            }
                            else
                            {
                                {
                                    HTuple ExpTmpOutVar_0;
                                    HOperatorSet.TupleRegexpReplace(hv_ImageFilesTmp, (new HTuple("//")).TupleConcat(
                                        "replace_all"), "/", out ExpTmpOutVar_0);
                                    hv_ImageFilesTmp.Dispose();
                                    hv_ImageFilesTmp = ExpTmpOutVar_0;
                                }
                            }
                            break;
                        }
                    }
                    //Concatenate the output image paths.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ImageFiles = hv_ImageFiles.TupleConcat(
                                hv_ImageFilesTmp);
                            hv_ImageFiles.Dispose();
                            hv_ImageFiles = ExpTmpLocalVar_ImageFiles;
                        }
                    }
                }

                hv_Extensions_COPY_INP_TMP.Dispose();
                hv_ImageDirectoryIndex.Dispose();
                hv_ImageFilesTmp.Dispose();
                hv_CurrentImageDirectory.Dispose();
                hv_HalconImages.Dispose();
                hv_OS.Dispose();
                hv_Directories.Dispose();
                hv_Index.Dispose();
                hv_Length.Dispose();
                hv_NetworkDrive.Dispose();
                hv_Substring.Dispose();
                hv_FileExists.Dispose();
                hv_AllFiles.Dispose();
                hv_i.Dispose();
                hv_Selection.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Extensions_COPY_INP_TMP.Dispose();
                hv_ImageDirectoryIndex.Dispose();
                hv_ImageFilesTmp.Dispose();
                hv_CurrentImageDirectory.Dispose();
                hv_HalconImages.Dispose();
                hv_OS.Dispose();
                hv_Directories.Dispose();
                hv_Index.Dispose();
                hv_Length.Dispose();
                hv_NetworkDrive.Dispose();
                hv_Substring.Dispose();
                hv_FileExists.Dispose();
                hv_AllFiles.Dispose();
                hv_i.Dispose();
                hv_Selection.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: File / Misc
        // Short Description: Parse a filename into directory, base filename, and extension 
        public static void parse_filename(HTuple hv_FileName, out HTuple hv_BaseName, out HTuple hv_Extension,
            out HTuple hv_Directory)
        {



            // Local control variables 

            HTuple hv_DirectoryTmp = new HTuple(), hv_Substring = new HTuple();
            // Initialize local and output iconic variables 
            hv_BaseName = new HTuple();
            hv_Extension = new HTuple();
            hv_Directory = new HTuple();
            try
            {
                //This procedure gets a filename (with full path) as input
                //and returns the directory path, the base filename and the extension
                //in three different strings.
                //
                //In the output path the path separators will be replaced
                //by '/' in all cases.
                //
                //The procedure shows the possibilities of regular expressions in HALCON.
                //
                //Input parameters:
                //FileName: The input filename
                //
                //Output parameters:
                //BaseName: The filename without directory description and file extension
                //Extension: The file extension
                //Directory: The directory path
                //
                //Example:
                //basename('C:/images/part_01.png',...) returns
                //BaseName = 'part_01'
                //Extension = 'png'
                //Directory = 'C:\\images\\' (on Windows systems)
                //
                //Explanation of the regular expressions:
                //
                //'([^\\\\/]*?)(?:\\.[^.]*)?$':
                //To start at the end, the '$' matches the end of the string,
                //so it is best to read the expression from right to left.
                //The part in brackets (?:\\.[^.}*) denotes a non-capturing group.
                //That means, that this part is matched, but not captured
                //in contrast to the first bracketed group ([^\\\\/], see below.)
                //\\.[^.]* matches a dot '.' followed by as many non-dots as possible.
                //So (?:\\.[^.]*)? matches the file extension, if any.
                //The '?' at the end assures, that even if no extension exists,
                //a correct match is returned.
                //The first part in brackets ([^\\\\/]*?) is a capture group,
                //which means, that if a match is found, only the part in
                //brackets is returned as a result.
                //Because both HDevelop strings and regular expressions need a '\\'
                //to describe a backslash, inside regular expressions within HDevelop
                //a backslash has to be written as '\\\\'.
                //[^\\\\/] matches any character but a slash or backslash ('\\' in HDevelop)
                //[^\\\\/]*? matches a string od 0..n characters (except '/' or '\\')
                //where the '?' after the '*' switches the greediness off,
                //that means, that the shortest possible match is returned.
                //This option is necessary to cut off the extension
                //but only if (?:\\.[^.]*)? is able to match one.
                //To summarize, the regular expression matches that part of
                //the input string, that follows after the last '/' or '\\' and
                //cuts off the extension (if any) after the last '.'.
                //
                //'\\.([^.]*)$':
                //This matches everything after the last '.' of the input string.
                //Because ([^.]) is a capturing group,
                //only the part after the dot is returned.
                //
                //'.*[\\\\/]':
                //This matches the longest substring with a '/' or a '\\' at the end.
                //
                hv_DirectoryTmp.Dispose();
                HOperatorSet.TupleRegexpMatch(hv_FileName, ".*[\\\\/]", out hv_DirectoryTmp);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Substring.Dispose();
                    HOperatorSet.TupleSubstr(hv_FileName, hv_DirectoryTmp.TupleStrlen(), (hv_FileName.TupleStrlen()
                        ) - 1, out hv_Substring);
                }
                hv_BaseName.Dispose();
                HOperatorSet.TupleRegexpMatch(hv_Substring, "([^\\\\/]*?)(?:\\.[^.]*)?$", out hv_BaseName);
                hv_Extension.Dispose();
                HOperatorSet.TupleRegexpMatch(hv_Substring, "\\.([^.]*)$", out hv_Extension);
                //
                //
                //Finally all found backslashes ('\\') are converted
                //to a slash to get consistent paths
                hv_Directory.Dispose();
                HOperatorSet.TupleRegexpReplace(hv_DirectoryTmp, (new HTuple("\\\\")).TupleConcat(
                    "replace_all"), "/", out hv_Directory);

                hv_DirectoryTmp.Dispose();
                hv_Substring.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_DirectoryTmp.Dispose();
                hv_Substring.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Deep Learning / Classification
        // Short Description: Plot the training error, validation error and learning rate during deep learning classifier training. 
        public static void plot_dl_classifier_training_progress(HTuple hv_TrainingErrors, HTuple hv_ValidationErrors,
            HTuple hv_LearningRates, HTuple hv_Epochs, HTuple hv_NumEpochs, HTuple hv_WindowHandle)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_TrainingErrorPercent = new HTuple();
            HTuple hv_ValidationErrorPercent = new HTuple(), hv_AxesColor = new HTuple();
            HTuple hv_TrainingErrorColor = new HTuple(), hv_ValidationErrorColor = new HTuple();
            HTuple hv_LearningRateColor = new HTuple(), hv_TrainingErrorFunction = new HTuple();
            HTuple hv_ValidationErrorFunction = new HTuple(), hv_LearningRateFunction = new HTuple();
            HTuple hv_GenParamName = new HTuple(), hv_GenParamValue = new HTuple();
            HTuple hv_EndYError = new HTuple(), hv_EndYLearningRate = new HTuple();
            HTuple hv_Style = new HTuple(), hv_Flush = new HTuple();
            HTuple hv_IndexMinValError = new HTuple(), hv_Text = new HTuple();
            HTuple hv_NumEpochs_COPY_INP_TMP = new HTuple(hv_NumEpochs);

            // Initialize local and output iconic variables 
            try
            {
                //This procedure plots the tuples training error and
                //validation error with the y-axis on the left side,
                //and the learning rate with the y-axis on the right side,
                //versus the epochs over batches on the x-axis.
                //The maximum number of epochs should be given by NumEpochs,
                //to scale the x-axis appropriately.
                //The plot is displayed in the graphics window given by WindowHandle.
                //
                //The procedure expects the input tuples TrainingErrors, ValidationErrors,
                //LearningRates, and Epochs with their values sorted in chronological order,
                //the current value in each case as last element.
                //
                //Check input parameters.
                if ((int)(new HTuple(hv_NumEpochs_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
                {
                    hv_NumEpochs_COPY_INP_TMP.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_NumEpochs_COPY_INP_TMP = hv_Epochs.TupleMax()
                            ;
                    }
                }
                else if ((int)(new HTuple(((hv_NumEpochs_COPY_INP_TMP.TupleIsNumber()
                    )).TupleNotEqual(1))) != 0)
                {
                    throw new HalconException("NumEpochs must be a number or an empty tuple.");
                }
                hv_TrainingErrorPercent.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TrainingErrorPercent = hv_TrainingErrors * 100;
                }
                hv_ValidationErrorPercent.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ValidationErrorPercent = hv_ValidationErrors * 100;
                }
                //
                //Set the colors of the axes, plots and texts.
                hv_AxesColor.Dispose();
                hv_AxesColor = "white";
                hv_TrainingErrorColor.Dispose();
                hv_TrainingErrorColor = "magenta";
                hv_ValidationErrorColor.Dispose();
                hv_ValidationErrorColor = "gold";
                hv_LearningRateColor.Dispose();
                hv_LearningRateColor = "dark turquoise";
                //
                //Create functions from the input tuples.
                hv_TrainingErrorFunction.Dispose();
                HOperatorSet.CreateFunct1dPairs(hv_Epochs, hv_TrainingErrorPercent, out hv_TrainingErrorFunction);
                hv_ValidationErrorFunction.Dispose();
                HOperatorSet.CreateFunct1dPairs(hv_Epochs, hv_ValidationErrorPercent, out hv_ValidationErrorFunction);
                hv_LearningRateFunction.Dispose();
                HOperatorSet.CreateFunct1dPairs(hv_Epochs, hv_LearningRates, out hv_LearningRateFunction);
                //
                //Assemble generic parameters for the plots.
                hv_GenParamName.Dispose();
                hv_GenParamName = new HTuple();
                hv_GenParamName[0] = "axis_location_x";
                hv_GenParamName[1] = "end_x";
                hv_GenParamName[2] = "ticks_x";
                hv_GenParamName[3] = "start_y";
                hv_GenParamName[4] = "margin_top";
                hv_GenParamName[5] = "margin_right";
                hv_GenParamValue.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_GenParamValue = new HTuple();
                    hv_GenParamValue[0] = "origin";
                    hv_GenParamValue = hv_GenParamValue.TupleConcat(hv_NumEpochs_COPY_INP_TMP);
                    hv_GenParamValue = hv_GenParamValue.TupleConcat((hv_NumEpochs_COPY_INP_TMP / 5) + 1);
                    hv_GenParamValue = hv_GenParamValue.TupleConcat(new HTuple(0, 70, 100));
                }
                hv_EndYError.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_EndYError = ((((hv_TrainingErrorPercent.TupleConcat(
                        hv_ValidationErrorPercent))).TupleConcat(0.1))).TupleMax();
                }
                //Round the maximum value of the left Y-axis
                //to an integer or a real value with one decimal.
                if ((int)(new HTuple(hv_EndYError.TupleGreaterEqual(1.0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_EndYError = ((hv_EndYError.TupleCeil()
                                )).TupleInt();
                            hv_EndYError.Dispose();
                            hv_EndYError = ExpTmpLocalVar_EndYError;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_EndYError = (((hv_EndYError * 10.0)).TupleCeil()
                                ) / 10.0;
                            hv_EndYError.Dispose();
                            hv_EndYError = ExpTmpLocalVar_EndYError;
                        }
                    }
                }
                hv_EndYLearningRate.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_EndYLearningRate = hv_LearningRates.TupleMax()
                        ;
                }
                //Display the first values as crosses
                //for better visibility.
                if ((int)(new HTuple((new HTuple(hv_Epochs.TupleLength())).TupleEqual(1))) != 0)
                {
                    hv_Style.Dispose();
                    hv_Style = "cross";
                }
                else
                {
                    hv_Style.Dispose();
                    hv_Style = "line";
                }
                //
                //Disable flushing the graphics window temporarily
                //to avoid flickering.
                hv_Flush.Dispose();
                HOperatorSet.GetWindowParam(hv_WindowHandle, "flush", out hv_Flush);
                HOperatorSet.SetWindowParam(hv_WindowHandle, "flush", "false");
                HOperatorSet.ClearWindow(hv_WindowHandle);
                //
                //Display plots.
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    plot_funct_1d(hv_WindowHandle, hv_TrainingErrorFunction, new HTuple(), "Error [%]",
                        hv_TrainingErrorColor, hv_GenParamName.TupleConcat((((new HTuple("axes_color")).TupleConcat(
                        "end_y")).TupleConcat("ticks_y")).TupleConcat("style")), ((((((hv_GenParamValue.TupleConcat(
                        hv_AxesColor))).TupleConcat(hv_EndYError))).TupleConcat(hv_EndYError / 5.0))).TupleConcat(
                        hv_Style));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    plot_funct_1d(hv_WindowHandle, hv_ValidationErrorFunction, new HTuple(), new HTuple(),
                        hv_ValidationErrorColor, hv_GenParamName.TupleConcat(((new HTuple("axes_color")).TupleConcat(
                        "end_y")).TupleConcat("style")), ((((hv_GenParamValue.TupleConcat("none"))).TupleConcat(
                        hv_EndYError))).TupleConcat(hv_Style));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    plot_funct_1d(hv_WindowHandle, hv_LearningRateFunction, new HTuple(), "Learning rate",
                        hv_LearningRateColor, hv_GenParamName.TupleConcat((((((new HTuple("axes_color")).TupleConcat(
                        "axis_location_y")).TupleConcat("end_y")).TupleConcat("ticks_y")).TupleConcat(
                        "format_y")).TupleConcat("style")), ((((((((hv_GenParamValue.TupleConcat(
                        hv_AxesColor))).TupleConcat("right"))).TupleConcat(hv_EndYLearningRate))).TupleConcat(
                        hv_EndYLearningRate / 5.0))).TupleConcat((new HTuple(".1e")).TupleConcat(
                        "step")));
                }
                //
                //Display current values in appropriate colors.
                hv_IndexMinValError.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_IndexMinValError = hv_ValidationErrorPercent.TupleFindLast(
                        hv_ValidationErrorPercent.TupleMin());
                }
                hv_Text.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Text = "Best validation error: " + (((hv_ValidationErrorPercent.TupleSelect(
                        hv_IndexMinValError))).TupleString(".1f"));
                }
                if (hv_Text == null)
                    hv_Text = new HTuple();
                hv_Text[1] = "Associated training error: " + (((hv_TrainingErrorPercent.TupleSelect(
                    hv_IndexMinValError))).TupleString(".1f"));
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispText(hv_WindowHandle, hv_Text + " %", "window",
                        "top", "left", hv_ValidationErrorColor.TupleConcat(hv_TrainingErrorColor),
                        "box", "false");
                }
                hv_Text.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Text = "Learning rate: " + (((hv_LearningRates.TupleSelect(
                        (new HTuple(hv_LearningRates.TupleLength())) - 1))).TupleString(".1e"));
                }
                HOperatorSet.DispText(hv_WindowHandle, hv_Text, "window", "top",
                    "right", hv_LearningRateColor, "box", "false");
                hv_Text.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Text = "Epoch: " + (((hv_Epochs.TupleSelect(
                        (new HTuple(hv_Epochs.TupleLength())) - 1))).TupleString(".1f"));
                }
                HOperatorSet.DispText(hv_WindowHandle, hv_Text, "window", "bottom",
                    "center", "white", "box", "false");
                //
                //Flush the buffer and re-enable flushing.
                HOperatorSet.FlushBuffer(hv_WindowHandle);
                HOperatorSet.SetWindowParam(hv_WindowHandle, "flush", hv_Flush);

                hv_NumEpochs_COPY_INP_TMP.Dispose();
                hv_TrainingErrorPercent.Dispose();
                hv_ValidationErrorPercent.Dispose();
                hv_AxesColor.Dispose();
                hv_TrainingErrorColor.Dispose();
                hv_ValidationErrorColor.Dispose();
                hv_LearningRateColor.Dispose();
                hv_TrainingErrorFunction.Dispose();
                hv_ValidationErrorFunction.Dispose();
                hv_LearningRateFunction.Dispose();
                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();
                hv_EndYError.Dispose();
                hv_EndYLearningRate.Dispose();
                hv_Style.Dispose();
                hv_Flush.Dispose();
                hv_IndexMinValError.Dispose();
                hv_Text.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_NumEpochs_COPY_INP_TMP.Dispose();
                hv_TrainingErrorPercent.Dispose();
                hv_ValidationErrorPercent.Dispose();
                hv_AxesColor.Dispose();
                hv_TrainingErrorColor.Dispose();
                hv_ValidationErrorColor.Dispose();
                hv_LearningRateColor.Dispose();
                hv_TrainingErrorFunction.Dispose();
                hv_ValidationErrorFunction.Dispose();
                hv_LearningRateFunction.Dispose();
                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();
                hv_EndYError.Dispose();
                hv_EndYLearningRate.Dispose();
                hv_Style.Dispose();
                hv_Flush.Dispose();
                hv_IndexMinValError.Dispose();
                hv_Text.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Graphics / Output
        // Short Description:  This procedure plots tuples representing functions or curves in a coordinate system. 
        public static void plot_funct_1d(HTuple hv_WindowHandle, HTuple hv_Function, HTuple hv_XLabel,
            HTuple hv_YLabel, HTuple hv_Color, HTuple hv_GenParamName, HTuple hv_GenParamValue)
        {



            // Local control variables 

            HTuple hv_XValues = new HTuple(), hv_YValues = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                //This procedure plots a function in a coordinate system.
                //
                //Input parameters:
                //
                //Function: 1D function
                //
                //XLabel: X-axis label
                //
                //YLabel: Y-axis label
                //
                //Color: Color of the plotted function
                //       If [] is given, the currently set display color is used.
                //       If 'none is given, the function is not plotted, but only
                //       the coordinate axes as specified.
                //
                //GenParamName:  Generic parameters to control the presentation
                //               The parameters are evaluated from left to right.
                //
                //               Possible Values:
                //   'axes_color': coordinate system color
                //                 Default: 'white'
                //                 If 'none' is given, no coordinate system is shown.
                //   'style': Graph style
                //            Possible values: 'line' (default), 'cross', 'step', 'filled'
                //   'clip': Clip graph to coordinate system area
                //           Possible values: 'yes' (default), 'no'
                //   'ticks': Control display of ticks on the axes
                //            If 'min_max_origin' is given (default), ticks are shown
                //            at the minimum and maximum values of the axes and at the
                //            intercept point of x- and y-axis.
                //            If 'none' is given, no ticks are shown.
                //            If any number != 0 is given, it is interpreted as distance
                //            between the ticks.
                //   'ticks_x': Control display of ticks on x-axis only
                //   'ticks_y': Control display of ticks on y-axis only
                //   'format_x': Format of the values next to the ticks of the x-axis
                //               (see tuple_string for more details).
                //   'format_y': Format of the values next to the ticks of the y-axis
                //               (see tuple_string for more details).
                //   'grid': Control display of grid lines within the coordinate system
                //           If 'min_max_origin' is given (default), grid lines are shown
                //           at the minimum and maximum values of the axes.
                //           If 'none' is given, no grid lines are shown.
                //           If any number != 0 is given, it is interpreted as distance
                //           between the grid lines.
                //   'grid_x': Control display of grid lines for the x-axis only
                //   'grid_y': Control display of grid lines for the y-axis only
                //   'grid_color': Color of the grid (default: 'dim gray')
                //   'margin': The distance in pixels of the coordinate system area
                //             to all four window borders.
                //   'margin_left': The distance in pixels of the coordinate system area
                //                  to the left window border.
                //   'margin_right': The distance in pixels of the coordinate system area
                //                   to the right window border.
                //   'margin_top': The distance in pixels of the coordinate system area
                //                 to the upper window border.
                //   'margin_bottom': The distance in pixels of the coordinate system area
                //                    to the lower window border.
                //   'start_x': Lowest x value of the x-axis
                //              Default: min(XValues)
                //   'end_x': Highest x value of the x-axis
                //            Default: max(XValues)
                //   'start_y': Lowest y value of the y-axis
                //              Default: min(YValues)
                //   'end_y': Highest y value of the y-axis
                //            Default: max(YValues)
                //   'axis_location_x': Either 'bottom', 'origin', or 'top'
                //               to position the x-axis conveniently,
                //               or the Y coordinate of the intercept point of x- and y-axis.
                //               Default: 'bottom'
                //               (Used to be called 'origin_y')
                //   'axis_location_y': Either 'left', 'origin', or 'right'
                //               to position the y-axis conveniently,
                //               or the X coordinate of the intercept point of x- and y-axis.
                //               Default: 'left'
                //               (Used to be called 'origin_x')
                //
                //GenParamValue: Values of the generic parameters of GenericParamName
                //
                //
                hv_XValues.Dispose(); hv_YValues.Dispose();
                HOperatorSet.Funct1dToPairs(hv_Function, out hv_XValues, out hv_YValues);
                plot_tuple(hv_WindowHandle, hv_XValues, hv_YValues, hv_XLabel, hv_YLabel, hv_Color,hv_GenParamName, hv_GenParamValue);

                hv_XValues.Dispose();
                hv_YValues.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_XValues.Dispose();
                hv_YValues.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Graphics / Output
        // Short Description:  This procedure plots tuples representing functions or curves in a coordinate system. 
        public static void plot_tuple(HTuple hv_WindowHandle, HTuple hv_XValues, HTuple hv_YValues,
            HTuple hv_XLabel, HTuple hv_YLabel, HTuple hv_Color, HTuple hv_GenParamName,
            HTuple hv_GenParamValue)
        {



            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ContourXGrid = null, ho_ContourYGrid = null;
            HObject ho_XArrow = null, ho_YArrow = null, ho_ContourXTick = null;
            HObject ho_ContourYTick = null, ho_Contour = null, ho_Cross = null;
            HObject ho_Filled = null, ho_Stair = null, ho_StairTmp = null;

            // Local control variables 

            HTuple hv_PreviousWindowHandle = new HTuple();
            HTuple hv_ClipRegion = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_PartRow1 = new HTuple();
            HTuple hv_PartColumn1 = new HTuple(), hv_PartRow2 = new HTuple();
            HTuple hv_PartColumn2 = new HTuple(), hv_Red = new HTuple();
            HTuple hv_Green = new HTuple(), hv_Blue = new HTuple();
            HTuple hv_DrawMode = new HTuple(), hv_OriginStyle = new HTuple();
            HTuple hv_XAxisEndValue = new HTuple(), hv_YAxisEndValue = new HTuple();
            HTuple hv_XAxisStartValue = new HTuple(), hv_YAxisStartValue = new HTuple();
            HTuple hv_XValuesAreStrings = new HTuple(), hv_XTickValues = new HTuple();
            HTuple hv_XTicks = new HTuple(), hv_YAxisPosition = new HTuple();
            HTuple hv_XAxisPosition = new HTuple(), hv_LeftBorder = new HTuple();
            HTuple hv_RightBorder = new HTuple(), hv_UpperBorder = new HTuple();
            HTuple hv_LowerBorder = new HTuple(), hv_AxesColor = new HTuple();
            HTuple hv_Style = new HTuple(), hv_Clip = new HTuple();
            HTuple hv_YTicks = new HTuple(), hv_XGrid = new HTuple();
            HTuple hv_YGrid = new HTuple(), hv_GridColor = new HTuple();
            HTuple hv_YPosition = new HTuple(), hv_FormatX = new HTuple();
            HTuple hv_FormatY = new HTuple(), hv_NumGenParamNames = new HTuple();
            HTuple hv_NumGenParamValues = new HTuple(), hv_GenParamIndex = new HTuple();
            HTuple hv_XGridTicks = new HTuple(), hv_YTickDirection = new HTuple();
            HTuple hv_XTickDirection = new HTuple(), hv_XAxisWidthPx = new HTuple();
            HTuple hv_XAxisWidth = new HTuple(), hv_XScaleFactor = new HTuple();
            HTuple hv_YAxisHeightPx = new HTuple(), hv_YAxisHeight = new HTuple();
            HTuple hv_YScaleFactor = new HTuple(), hv_YAxisOffsetPx = new HTuple();
            HTuple hv_XAxisOffsetPx = new HTuple(), hv_DotStyle = new HTuple();
            HTuple hv_XGridValues = new HTuple(), hv_XGridStart = new HTuple();
            HTuple hv_XCoord = new HTuple(), hv_IndexGrid = new HTuple();
            HTuple hv_YGridValues = new HTuple(), hv_YGridStart = new HTuple();
            HTuple hv_YCoord = new HTuple(), hv_Ascent = new HTuple();
            HTuple hv_Descent = new HTuple(), hv_TextWidthXLabel = new HTuple();
            HTuple hv_TextHeightXLabel = new HTuple(), hv_TextWidthYLabel = new HTuple();
            HTuple hv_TextHeightYLabel = new HTuple(), hv_XTickStart = new HTuple();
            HTuple hv_Indices = new HTuple(), hv_TypeTicks = new HTuple();
            HTuple hv_IndexTicks = new HTuple(), hv_Ascent1 = new HTuple();
            HTuple hv_Descent1 = new HTuple(), hv_TextWidthXTicks = new HTuple();
            HTuple hv_TextHeightXTicks = new HTuple(), hv_YTickValues = new HTuple();
            HTuple hv_YTickStart = new HTuple(), hv_TextWidthYTicks = new HTuple();
            HTuple hv_TextHeightYTicks = new HTuple(), hv_Num = new HTuple();
            HTuple hv_I = new HTuple(), hv_YSelected = new HTuple();
            HTuple hv_Y1Selected = new HTuple(), hv_X1Selected = new HTuple();
            HTuple hv_Index = new HTuple(), hv_Row1 = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Col1 = new HTuple();
            HTuple hv_Col2 = new HTuple();
            HTuple hv_XValues_COPY_INP_TMP = new HTuple(hv_XValues);
            HTuple hv_YValues_COPY_INP_TMP = new HTuple(hv_YValues);

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ContourXGrid);
            HOperatorSet.GenEmptyObj(out ho_ContourYGrid);
            HOperatorSet.GenEmptyObj(out ho_XArrow);
            HOperatorSet.GenEmptyObj(out ho_YArrow);
            HOperatorSet.GenEmptyObj(out ho_ContourXTick);
            HOperatorSet.GenEmptyObj(out ho_ContourYTick);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            HOperatorSet.GenEmptyObj(out ho_Cross);
            HOperatorSet.GenEmptyObj(out ho_Filled);
            HOperatorSet.GenEmptyObj(out ho_Stair);
            HOperatorSet.GenEmptyObj(out ho_StairTmp);
            try
            {
                #region
                //This procedure plots tuples representing functions
                //or curves in a coordinate system.
                //
                //Input parameters:
                //
                //XValues: X values of the function to be plotted
                //         If XValues is set to [], it is internally set to 0,1,2,...,|YValues|-1.
                //         If XValues is a tuple of strings, the values are taken as categories.
                //
                //YValues: Y values of the function(s) to be plotted
                //         If YValues is set to [], it is internally set to 0,1,2,...,|XValues|-1.
                //         The number of y values must be equal to the number of x values
                //         or an integral multiple. In the latter case,
                //         multiple functions are plotted, that share the same x values.
                //
                //XLabel: X-axis label
                //
                //YLabel: Y-axis label
                //
                //Color: Color of the plotted function
                //       If [] is given, the currently set display color is used.
                //       If 'none is given, the function is not plotted, but only
                //       the coordinate axes as specified.
                //       If more than one color is given, multiple functions
                //       can be displayed in different colors.
                //
                //GenParamName:  Generic parameters to control the presentation
                //               Possible Values:
                //   'axes_color': coordinate system color
                //                 Default: 'white'
                //                 If 'none' is given, no coordinate system is shown.
                //   'style': Graph style
                //            Possible values: 'line' (default), 'cross', 'step', 'filled'
                //   'clip': Clip graph to coordinate system area
                //           Possible values: 'yes', 'no' (default)
                //   'ticks': Control display of ticks on the axes
                //            If 'min_max_origin' is given (default), ticks are shown
                //            at the minimum and maximum values of the axes and at the
                //            intercept point of x- and y-axis.
                //            If 'none' is given, no ticks are shown.
                //            If any number != 0 is given, it is interpreted as distance
                //            between the ticks.
                //   'ticks_x': Control display of ticks on x-axis only
                //   'ticks_y': Control display of ticks on y-axis only
                //   'format_x': Format of the values next to the ticks of the x-axis
                //               (see tuple_string for more details).
                //   'format_y': Format of the values next to the ticks of the y-axis
                //               (see tuple_string for more details).
                //   'grid': Control display of grid lines within the coordinate system
                //           If 'min_max_origin' is given (default), grid lines are shown
                //           at the minimum and maximum values of the axes.
                //           If 'none' is given, no grid lines are shown.
                //           If any number != 0 is given, it is interpreted as distance
                //           between the grid lines.
                //   'grid_x': Control display of grid lines for the x-axis only
                //   'grid_y': Control display of grid lines for the y-axis only
                //   'grid_color': Color of the grid (default: 'dim gray')
                //   'margin': The distance in pixels of the coordinate system area
                //             to all four window borders.
                //   'margin_left': The distance in pixels of the coordinate system area
                //                  to the left window border.
                //   'margin_right': The distance in pixels of the coordinate system area
                //                   to the right window border.
                //   'margin_top': The distance in pixels of the coordinate system area
                //                 to the upper window border.
                //   'margin_bottom': The distance in pixels of the coordinate system area
                //                    to the lower window border.
                //   'start_x': Lowest x value of the x-axis
                //              Default: min(XValues)
                //   'end_x': Highest x value of the x-axis
                //            Default: max(XValues)
                //   'start_y': Lowest y value of the y-axis
                //              Default: min(YValues)
                //   'end_y': Highest y value of the y-axis
                //            Default: max(YValues)
                //   'axis_location_x': Either 'bottom', 'origin', or 'top'
                //               to position the x-axis conveniently,
                //               or the Y coordinate of the intercept point of x- and y-axis.
                //               Default: 'bottom'
                //               (Used to be called 'origin_y')
                //   'axis_location_y': Either 'left', 'origin', or 'right'
                //               to position the y-axis conveniently,
                //               or the X coordinate of the intercept point of x- and y-axis.
                //               Default: 'left'
                //               (Used to be called 'origin_x')
                //
                //GenParamValue: Values of the generic parameters of GenericParamName
                //
                //
                #endregion
                //Store current display settings
                //if (HDevWindowStack.IsOpen())
                {
                    hv_PreviousWindowHandle = hv_WindowHandle;
                }
                HDevWindowStack.SetActive(hv_WindowHandle);
                hv_ClipRegion.Dispose();
                HOperatorSet.GetSystem("clip_region", out hv_ClipRegion);
                hv_Row.Dispose(); hv_Column.Dispose(); hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_Row, out hv_Column, out hv_Width,
                    out hv_Height);
                hv_PartRow1.Dispose(); hv_PartColumn1.Dispose(); hv_PartRow2.Dispose(); hv_PartColumn2.Dispose();
                HOperatorSet.GetPart(hv_WindowHandle, out hv_PartRow1, out hv_PartColumn1,
                    out hv_PartRow2, out hv_PartColumn2);
                hv_Red.Dispose(); hv_Green.Dispose(); hv_Blue.Dispose();
                HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
                hv_DrawMode.Dispose();
                HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
                hv_OriginStyle.Dispose();
                HOperatorSet.GetLineStyle(hv_WindowHandle, out hv_OriginStyle);
                //
                //Set display parameters
                HOperatorSet.SetLineStyle(hv_WindowHandle, new HTuple());
                HOperatorSet.SetSystem("clip_region", "false");
                //if (HDevWindowStack.IsOpen())
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_Height - 1, hv_Width - 1);
                    }
                }
                //
                //Check input coordinates
                //
                if ((int)((new HTuple(hv_XValues_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleAnd(
                    new HTuple(hv_YValues_COPY_INP_TMP.TupleEqual(new HTuple())))) != 0)
                {
                    //Neither XValues nor YValues are given:
                    //Set axes to interval [0,1]
                    hv_XAxisEndValue.Dispose();
                    hv_XAxisEndValue = 1;
                    hv_YAxisEndValue.Dispose();
                    hv_YAxisEndValue = 1;
                    hv_XAxisStartValue.Dispose();
                    hv_XAxisStartValue = 0;
                    hv_YAxisStartValue.Dispose();
                    hv_YAxisStartValue = 0;
                    hv_XValuesAreStrings.Dispose();
                    hv_XValuesAreStrings = 0;
                }
                else
                {
                    #region
                    if ((int)(new HTuple(hv_XValues_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
                    {
                        //XValues are omitted:
                        //Set equidistant XValues
                        hv_XValues_COPY_INP_TMP.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XValues_COPY_INP_TMP = HTuple.TupleGenSequence(
                                0, (new HTuple(hv_YValues_COPY_INP_TMP.TupleLength())) - 1, 1);
                        }
                        hv_XValuesAreStrings.Dispose();
                        hv_XValuesAreStrings = 0;
                    }
                    else if ((int)(new HTuple(hv_YValues_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
                    {
                        //YValues are omitted:
                        //Set equidistant YValues
                        hv_YValues_COPY_INP_TMP.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_YValues_COPY_INP_TMP = HTuple.TupleGenSequence(
                                0, (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength())) - 1, 1);
                        }
                    }
                    if ((int)(new HTuple((new HTuple((new HTuple(hv_YValues_COPY_INP_TMP.TupleLength()
                        )) % (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength())))).TupleNotEqual(
                        0))) != 0)
                    {
                        //Number of YValues does not match number of XValues
                        throw new HalconException("Number of YValues is no multiple of the number of XValues!");
                        #region Dispose()
                        ho_ContourXGrid.Dispose();
                        ho_ContourYGrid.Dispose();
                        ho_XArrow.Dispose();
                        ho_YArrow.Dispose();
                        ho_ContourXTick.Dispose();
                        ho_ContourYTick.Dispose();
                        ho_Contour.Dispose();
                        ho_Cross.Dispose();
                        ho_Filled.Dispose();
                        ho_Stair.Dispose();
                        ho_StairTmp.Dispose();

                        hv_XValues_COPY_INP_TMP.Dispose();
                        hv_YValues_COPY_INP_TMP.Dispose();
                        hv_PreviousWindowHandle.Dispose();
                        hv_ClipRegion.Dispose();
                        hv_Row.Dispose();
                        hv_Column.Dispose();
                        hv_Width.Dispose();
                        hv_Height.Dispose();
                        hv_PartRow1.Dispose();
                        hv_PartColumn1.Dispose();
                        hv_PartRow2.Dispose();
                        hv_PartColumn2.Dispose();
                        hv_Red.Dispose();
                        hv_Green.Dispose();
                        hv_Blue.Dispose();
                        hv_DrawMode.Dispose();
                        hv_OriginStyle.Dispose();
                        hv_XAxisEndValue.Dispose();
                        hv_YAxisEndValue.Dispose();
                        hv_XAxisStartValue.Dispose();
                        hv_YAxisStartValue.Dispose();
                        hv_XValuesAreStrings.Dispose();
                        hv_XTickValues.Dispose();
                        hv_XTicks.Dispose();
                        hv_YAxisPosition.Dispose();
                        hv_XAxisPosition.Dispose();
                        hv_LeftBorder.Dispose();
                        hv_RightBorder.Dispose();
                        hv_UpperBorder.Dispose();
                        hv_LowerBorder.Dispose();
                        hv_AxesColor.Dispose();
                        hv_Style.Dispose();
                        hv_Clip.Dispose();
                        hv_YTicks.Dispose();
                        hv_XGrid.Dispose();
                        hv_YGrid.Dispose();
                        hv_GridColor.Dispose();
                        hv_YPosition.Dispose();
                        hv_FormatX.Dispose();
                        hv_FormatY.Dispose();
                        hv_NumGenParamNames.Dispose();
                        hv_NumGenParamValues.Dispose();
                        hv_GenParamIndex.Dispose();
                        hv_XGridTicks.Dispose();
                        hv_YTickDirection.Dispose();
                        hv_XTickDirection.Dispose();
                        hv_XAxisWidthPx.Dispose();
                        hv_XAxisWidth.Dispose();
                        hv_XScaleFactor.Dispose();
                        hv_YAxisHeightPx.Dispose();
                        hv_YAxisHeight.Dispose();
                        hv_YScaleFactor.Dispose();
                        hv_YAxisOffsetPx.Dispose();
                        hv_XAxisOffsetPx.Dispose();
                        hv_DotStyle.Dispose();
                        hv_XGridValues.Dispose();
                        hv_XGridStart.Dispose();
                        hv_XCoord.Dispose();
                        hv_IndexGrid.Dispose();
                        hv_YGridValues.Dispose();
                        hv_YGridStart.Dispose();
                        hv_YCoord.Dispose();
                        hv_Ascent.Dispose();
                        hv_Descent.Dispose();
                        hv_TextWidthXLabel.Dispose();
                        hv_TextHeightXLabel.Dispose();
                        hv_TextWidthYLabel.Dispose();
                        hv_TextHeightYLabel.Dispose();
                        hv_XTickStart.Dispose();
                        hv_Indices.Dispose();
                        hv_TypeTicks.Dispose();
                        hv_IndexTicks.Dispose();
                        hv_Ascent1.Dispose();
                        hv_Descent1.Dispose();
                        hv_TextWidthXTicks.Dispose();
                        hv_TextHeightXTicks.Dispose();
                        hv_YTickValues.Dispose();
                        hv_YTickStart.Dispose();
                        hv_TextWidthYTicks.Dispose();
                        hv_TextHeightYTicks.Dispose();
                        hv_Num.Dispose();
                        hv_I.Dispose();
                        hv_YSelected.Dispose();
                        hv_Y1Selected.Dispose();
                        hv_X1Selected.Dispose();
                        hv_Index.Dispose();
                        hv_Row1.Dispose();
                        hv_Row2.Dispose();
                        hv_Col1.Dispose();
                        hv_Col2.Dispose();
                        #endregion 
                        return;
                    }
                    hv_XValuesAreStrings.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_XValuesAreStrings = hv_XValues_COPY_INP_TMP.TupleIsStringElem()
                            ;
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_XValuesAreStrings = new HTuple(((hv_XValuesAreStrings.TupleSum()
                                )).TupleEqual(new HTuple(hv_XValuesAreStrings.TupleLength())));
                            hv_XValuesAreStrings.Dispose();
                            hv_XValuesAreStrings = ExpTmpLocalVar_XValuesAreStrings;
                        }
                    }
                    if ((int)(hv_XValuesAreStrings) != 0)
                    {
                        //XValues are given as strings:
                        //Show XValues as ticks
                        hv_XTickValues.Dispose();
                        hv_XTickValues = new HTuple(hv_XValues_COPY_INP_TMP);
                        hv_XTicks.Dispose();
                        hv_XTicks = 1;
                        //Set x-axis dimensions
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_XValues = HTuple.TupleGenSequence(
                                    1, new HTuple(hv_XValues_COPY_INP_TMP.TupleLength()), 1);
                                hv_XValues_COPY_INP_TMP.Dispose();
                                hv_XValues_COPY_INP_TMP = ExpTmpLocalVar_XValues;
                            }
                        }
                    }
                    //Set default x-axis dimensions
                    if ((int)(new HTuple((new HTuple(hv_XValues_COPY_INP_TMP.TupleLength())).TupleGreater(
                        1))) != 0)
                    {
                        hv_XAxisStartValue.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XAxisStartValue = hv_XValues_COPY_INP_TMP.TupleMin()
                                ;
                        }
                        hv_XAxisEndValue.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XAxisEndValue = hv_XValues_COPY_INP_TMP.TupleMax()
                                ;
                        }
                    }
                    else
                    {
                        hv_XAxisEndValue.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XAxisEndValue = (hv_XValues_COPY_INP_TMP.TupleSelect(
                                0)) + 0.5;
                        }
                        hv_XAxisStartValue.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XAxisStartValue = (hv_XValues_COPY_INP_TMP.TupleSelect(
                                0)) - 0.5;
                        }
                    }
                    #endregion
                }
                //Set default y-axis dimensions
                if ((int)(new HTuple((new HTuple(hv_YValues_COPY_INP_TMP.TupleLength())).TupleGreater(
                    1))) != 0)
                {
                    hv_YAxisStartValue.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_YAxisStartValue = hv_YValues_COPY_INP_TMP.TupleMin()
                            ;
                    }
                    hv_YAxisEndValue.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_YAxisEndValue = hv_YValues_COPY_INP_TMP.TupleMax()
                            ;
                    }
                }
                else if ((int)(new HTuple((new HTuple(hv_YValues_COPY_INP_TMP.TupleLength()
                    )).TupleEqual(1))) != 0)
                {
                    hv_YAxisStartValue.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_YAxisStartValue = (hv_YValues_COPY_INP_TMP.TupleSelect(
                            0)) - 0.5;
                    }
                    hv_YAxisEndValue.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_YAxisEndValue = (hv_YValues_COPY_INP_TMP.TupleSelect(
                            0)) + 0.5;
                    }
                }
                else
                {
                    hv_YAxisStartValue.Dispose();
                    hv_YAxisStartValue = 0;
                    hv_YAxisEndValue.Dispose();
                    hv_YAxisEndValue = 1;
                }
                //Set default interception point of x- and y- axis
                hv_YAxisPosition.Dispose();
                hv_YAxisPosition = "default";
                hv_XAxisPosition.Dispose();
                hv_XAxisPosition = "default";
                //
                //Set more defaults
                hv_LeftBorder.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_LeftBorder = hv_Width * 0.1;
                }
                hv_RightBorder.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_RightBorder = hv_Width * 0.1;
                }
                hv_UpperBorder.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_UpperBorder = hv_Height * 0.1;
                }
                hv_LowerBorder.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_LowerBorder = hv_Height * 0.1;
                }
                hv_AxesColor.Dispose();
                hv_AxesColor = "white";
                hv_Style.Dispose();
                hv_Style = "line";
                hv_Clip.Dispose();
                hv_Clip = "no";
                hv_XTicks.Dispose();
                hv_XTicks = "min_max_origin";
                hv_YTicks.Dispose();
                hv_YTicks = "min_max_origin";
                hv_XGrid.Dispose();
                hv_XGrid = "none";
                hv_YGrid.Dispose();
                hv_YGrid = "none";
                hv_GridColor.Dispose();
                hv_GridColor = "dim gray";
                hv_YPosition.Dispose();
                hv_YPosition = "left";
                hv_FormatX.Dispose();
                hv_FormatX = "default";
                hv_FormatY.Dispose();
                hv_FormatY = "default";
                //
                //Parse generic parameters
                //
                hv_NumGenParamNames.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumGenParamNames = new HTuple(hv_GenParamName.TupleLength()
                        );
                }
                hv_NumGenParamValues.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumGenParamValues = new HTuple(hv_GenParamValue.TupleLength()
                        );
                }
                if ((int)(new HTuple(hv_NumGenParamNames.TupleNotEqual(hv_NumGenParamValues))) != 0)
                {
                    throw new HalconException("Number of generic parameter names does not match generic parameter values!");
                    #region Dispose()
                    ho_ContourXGrid.Dispose();
                    ho_ContourYGrid.Dispose();
                    ho_XArrow.Dispose();
                    ho_YArrow.Dispose();
                    ho_ContourXTick.Dispose();
                    ho_ContourYTick.Dispose();
                    ho_Contour.Dispose();
                    ho_Cross.Dispose();
                    ho_Filled.Dispose();
                    ho_Stair.Dispose();
                    ho_StairTmp.Dispose();

                    hv_XValues_COPY_INP_TMP.Dispose();
                    hv_YValues_COPY_INP_TMP.Dispose();
                    hv_PreviousWindowHandle.Dispose();
                    hv_ClipRegion.Dispose();
                    hv_Row.Dispose();
                    hv_Column.Dispose();
                    hv_Width.Dispose();
                    hv_Height.Dispose();
                    hv_PartRow1.Dispose();
                    hv_PartColumn1.Dispose();
                    hv_PartRow2.Dispose();
                    hv_PartColumn2.Dispose();
                    hv_Red.Dispose();
                    hv_Green.Dispose();
                    hv_Blue.Dispose();
                    hv_DrawMode.Dispose();
                    hv_OriginStyle.Dispose();
                    hv_XAxisEndValue.Dispose();
                    hv_YAxisEndValue.Dispose();
                    hv_XAxisStartValue.Dispose();
                    hv_YAxisStartValue.Dispose();
                    hv_XValuesAreStrings.Dispose();
                    hv_XTickValues.Dispose();
                    hv_XTicks.Dispose();
                    hv_YAxisPosition.Dispose();
                    hv_XAxisPosition.Dispose();
                    hv_LeftBorder.Dispose();
                    hv_RightBorder.Dispose();
                    hv_UpperBorder.Dispose();
                    hv_LowerBorder.Dispose();
                    hv_AxesColor.Dispose();
                    hv_Style.Dispose();
                    hv_Clip.Dispose();
                    hv_YTicks.Dispose();
                    hv_XGrid.Dispose();
                    hv_YGrid.Dispose();
                    hv_GridColor.Dispose();
                    hv_YPosition.Dispose();
                    hv_FormatX.Dispose();
                    hv_FormatY.Dispose();
                    hv_NumGenParamNames.Dispose();
                    hv_NumGenParamValues.Dispose();
                    hv_GenParamIndex.Dispose();
                    hv_XGridTicks.Dispose();
                    hv_YTickDirection.Dispose();
                    hv_XTickDirection.Dispose();
                    hv_XAxisWidthPx.Dispose();
                    hv_XAxisWidth.Dispose();
                    hv_XScaleFactor.Dispose();
                    hv_YAxisHeightPx.Dispose();
                    hv_YAxisHeight.Dispose();
                    hv_YScaleFactor.Dispose();
                    hv_YAxisOffsetPx.Dispose();
                    hv_XAxisOffsetPx.Dispose();
                    hv_DotStyle.Dispose();
                    hv_XGridValues.Dispose();
                    hv_XGridStart.Dispose();
                    hv_XCoord.Dispose();
                    hv_IndexGrid.Dispose();
                    hv_YGridValues.Dispose();
                    hv_YGridStart.Dispose();
                    hv_YCoord.Dispose();
                    hv_Ascent.Dispose();
                    hv_Descent.Dispose();
                    hv_TextWidthXLabel.Dispose();
                    hv_TextHeightXLabel.Dispose();
                    hv_TextWidthYLabel.Dispose();
                    hv_TextHeightYLabel.Dispose();
                    hv_XTickStart.Dispose();
                    hv_Indices.Dispose();
                    hv_TypeTicks.Dispose();
                    hv_IndexTicks.Dispose();
                    hv_Ascent1.Dispose();
                    hv_Descent1.Dispose();
                    hv_TextWidthXTicks.Dispose();
                    hv_TextHeightXTicks.Dispose();
                    hv_YTickValues.Dispose();
                    hv_YTickStart.Dispose();
                    hv_TextWidthYTicks.Dispose();
                    hv_TextHeightYTicks.Dispose();
                    hv_Num.Dispose();
                    hv_I.Dispose();
                    hv_YSelected.Dispose();
                    hv_Y1Selected.Dispose();
                    hv_X1Selected.Dispose();
                    hv_Index.Dispose();
                    hv_Row1.Dispose();
                    hv_Row2.Dispose();
                    hv_Col1.Dispose();
                    hv_Col2.Dispose();
                    #endregion
                    return;
                }
                //
                for (hv_GenParamIndex = 0; (int)hv_GenParamIndex <= (int)((new HTuple(hv_GenParamName.TupleLength()
                    )) - 1); hv_GenParamIndex = (int)hv_GenParamIndex + 1)
                {

                    #region ParamSet
                    //Set 'axes_color'
                    if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "axes_color"))) != 0)
                    {
                        hv_AxesColor.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_AxesColor = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'style'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "style"))) != 0)
                    {
                        hv_Style.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Style = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'clip'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "clip"))) != 0)
                    {
                        hv_Clip.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Clip = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        if ((int)((new HTuple(hv_Clip.TupleNotEqual("yes"))).TupleAnd(new HTuple(hv_Clip.TupleNotEqual(
                            "no")))) != 0)
                        {
                            throw new HalconException(("Unsupported clipping option: '" + hv_Clip) + "'");
                        }
                        //
                        //Set 'ticks'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "ticks"))) != 0)
                    {
                        hv_XTicks.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XTicks = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        hv_YTicks.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_YTicks = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'ticks_x'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "ticks_x"))) != 0)
                    {
                        hv_XTicks.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XTicks = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'ticks_y'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "ticks_y"))) != 0)
                    {
                        hv_YTicks.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_YTicks = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'grid'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "grid"))) != 0)
                    {
                        hv_XGrid.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XGrid = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        hv_YGrid.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_YGrid = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        hv_XGridTicks.Dispose();
                        hv_XGridTicks = new HTuple(hv_XTicks);
                        //
                        //Set 'grid_x'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "grid_x"))) != 0)
                    {
                        hv_XGrid.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XGrid = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'grid_y'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "grid_y"))) != 0)
                    {
                        hv_YGrid.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_YGrid = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'grid_color'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "grid_color"))) != 0)
                    {
                        hv_GridColor.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_GridColor = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'start_x'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "start_x"))) != 0)
                    {
                        hv_XAxisStartValue.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XAxisStartValue = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'end_x'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "end_x"))) != 0)
                    {
                        hv_XAxisEndValue.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XAxisEndValue = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'start_y'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "start_y"))) != 0)
                    {
                        hv_YAxisStartValue.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_YAxisStartValue = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'end_y'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "end_y"))) != 0)
                    {
                        hv_YAxisEndValue.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_YAxisEndValue = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'axis_location_y' (old name 'origin_x')
                    }
                    else if ((int)((new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "axis_location_y"))).TupleOr(new HTuple(((hv_GenParamName.TupleSelect(
                        hv_GenParamIndex))).TupleEqual("origin_x")))) != 0)
                    {
                        hv_YAxisPosition.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_YAxisPosition = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'axis_location_x' (old name: 'origin_y')
                    }
                    else if ((int)((new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "axis_location_x"))).TupleOr(new HTuple(((hv_GenParamName.TupleSelect(
                        hv_GenParamIndex))).TupleEqual("origin_y")))) != 0)
                    {
                        hv_XAxisPosition.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XAxisPosition = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'margin'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "margin"))) != 0)
                    {
                        hv_LeftBorder.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_LeftBorder = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        hv_RightBorder.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_RightBorder = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        hv_UpperBorder.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_UpperBorder = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        hv_LowerBorder.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_LowerBorder = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'margin_left'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "margin_left"))) != 0)
                    {
                        hv_LeftBorder.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_LeftBorder = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'margin_right'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "margin_right"))) != 0)
                    {
                        hv_RightBorder.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_RightBorder = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'margin_top'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "margin_top"))) != 0)
                    {
                        hv_UpperBorder.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_UpperBorder = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                        //
                        //Set 'margin_bottom'
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "margin_bottom"))) != 0)
                    {
                        hv_LowerBorder.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_LowerBorder = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "format_x"))) != 0)
                    {
                        hv_FormatX.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_FormatX = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "format_y"))) != 0)
                    {
                        hv_FormatY.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_FormatY = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                    }
                    else
                    {
                        throw new HalconException(("Unknown generic parameter: '" + (hv_GenParamName.TupleSelect(
                            hv_GenParamIndex))) + "'");
                    }
                    #endregion 

                }
                //
                //Check consistency of start and end values
                //of the axes.
                if ((int)(new HTuple(hv_XAxisStartValue.TupleGreater(hv_XAxisEndValue))) != 0)
                {
                    throw new HalconException("Value for 'start_x' is greater than value for 'end_x'");
                }
                if ((int)(new HTuple(hv_YAxisStartValue.TupleGreater(hv_YAxisEndValue))) != 0)
                {
                    throw new HalconException("Value for 'start_y' is greater than value for 'end_y'");
                }
                //
                //Set the position of the y-axis.
                if ((int)(new HTuple(hv_YAxisPosition.TupleEqual("default"))) != 0)
                {
                    hv_YAxisPosition.Dispose();
                    hv_YAxisPosition = new HTuple(hv_XAxisStartValue);
                }
                if ((int)(new HTuple(((hv_YAxisPosition.TupleIsString())).TupleEqual(1))) != 0)
                {
                    if ((int)(new HTuple(hv_YAxisPosition.TupleEqual("left"))) != 0)
                    {
                        hv_YAxisPosition.Dispose();
                        hv_YAxisPosition = new HTuple(hv_XAxisStartValue);
                    }
                    else if ((int)(new HTuple(hv_YAxisPosition.TupleEqual("right"))) != 0)
                    {
                        hv_YAxisPosition.Dispose();
                        hv_YAxisPosition = new HTuple(hv_XAxisEndValue);
                    }
                    else if ((int)(new HTuple(hv_YAxisPosition.TupleEqual("origin"))) != 0)
                    {
                        hv_YAxisPosition.Dispose();
                        hv_YAxisPosition = 0;
                    }
                    else
                    {
                        throw new HalconException(("Unsupported axis_location_y: '" + hv_YAxisPosition) + "'");
                    }
                }
                //Set the position of the ticks on the y-axis
                //depending of the location of the y-axis.
                if ((int)(new HTuple((new HTuple(((hv_XAxisStartValue.TupleConcat(hv_XAxisEndValue))).TupleMean()
                    )).TupleGreater(hv_YAxisPosition))) != 0)
                {
                    hv_YTickDirection.Dispose();
                    hv_YTickDirection = "right";
                }
                else
                {
                    hv_YTickDirection.Dispose();
                    hv_YTickDirection = "left";
                }
                //
                //Set the position of the x-axis.
                if ((int)(new HTuple(hv_XAxisPosition.TupleEqual("default"))) != 0)
                {
                    hv_XAxisPosition.Dispose();
                    hv_XAxisPosition = new HTuple(hv_YAxisStartValue);
                }
                if ((int)(new HTuple(((hv_XAxisPosition.TupleIsString())).TupleEqual(1))) != 0)
                {
                    if ((int)(new HTuple(hv_XAxisPosition.TupleEqual("bottom"))) != 0)
                    {
                        hv_XAxisPosition.Dispose();
                        hv_XAxisPosition = new HTuple(hv_YAxisStartValue);
                    }
                    else if ((int)(new HTuple(hv_XAxisPosition.TupleEqual("top"))) != 0)
                    {
                        hv_XAxisPosition.Dispose();
                        hv_XAxisPosition = new HTuple(hv_YAxisEndValue);
                    }
                    else if ((int)(new HTuple(hv_XAxisPosition.TupleEqual("origin"))) != 0)
                    {
                        hv_XAxisPosition.Dispose();
                        hv_XAxisPosition = 0;
                    }
                    else
                    {
                        throw new HalconException(("Unsupported axis_location_x: '" + hv_XAxisPosition) + "'");
                    }
                }
                //Set the position of the ticks on the y-axis
                //depending of the location of the y-axis.
                if ((int)(new HTuple((new HTuple(((hv_YAxisStartValue.TupleConcat(hv_YAxisEndValue))).TupleMean()
                    )).TupleGreater(hv_XAxisPosition))) != 0)
                {
                    hv_XTickDirection.Dispose();
                    hv_XTickDirection = "up";
                }
                else
                {
                    hv_XTickDirection.Dispose();
                    hv_XTickDirection = "down";
                }
                //
                //Calculate basic pixel coordinates and scale factors
                //
                hv_XAxisWidthPx.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_XAxisWidthPx = (hv_Width - hv_LeftBorder) - hv_RightBorder;
                }
                hv_XAxisWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_XAxisWidth = hv_XAxisEndValue - hv_XAxisStartValue;
                }
                if ((int)(new HTuple(hv_XAxisWidth.TupleEqual(0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_XAxisStartValue = hv_XAxisStartValue - 0.5;
                            hv_XAxisStartValue.Dispose();
                            hv_XAxisStartValue = ExpTmpLocalVar_XAxisStartValue;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_XAxisEndValue = hv_XAxisEndValue + 0.5;
                            hv_XAxisEndValue.Dispose();
                            hv_XAxisEndValue = ExpTmpLocalVar_XAxisEndValue;
                        }
                    }
                    hv_XAxisWidth.Dispose();
                    hv_XAxisWidth = 1;
                }
                hv_XScaleFactor.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_XScaleFactor = hv_XAxisWidthPx / (hv_XAxisWidth.TupleReal()
                        );
                }
                hv_YAxisHeightPx.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_YAxisHeightPx = (hv_Height - hv_LowerBorder) - hv_UpperBorder;
                }
                hv_YAxisHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_YAxisHeight = hv_YAxisEndValue - hv_YAxisStartValue;
                }
                if ((int)(new HTuple(hv_YAxisHeight.TupleEqual(0))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_YAxisStartValue = hv_YAxisStartValue - 0.5;
                            hv_YAxisStartValue.Dispose();
                            hv_YAxisStartValue = ExpTmpLocalVar_YAxisStartValue;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_YAxisEndValue = hv_YAxisEndValue + 0.5;
                            hv_YAxisEndValue.Dispose();
                            hv_YAxisEndValue = ExpTmpLocalVar_YAxisEndValue;
                        }
                    }
                    hv_YAxisHeight.Dispose();
                    hv_YAxisHeight = 1;
                }
                hv_YScaleFactor.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_YScaleFactor = hv_YAxisHeightPx / (hv_YAxisHeight.TupleReal());
                }
                hv_YAxisOffsetPx.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_YAxisOffsetPx = (hv_YAxisPosition - hv_XAxisStartValue) * hv_XScaleFactor;
                }
                hv_XAxisOffsetPx.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_XAxisOffsetPx = (hv_XAxisPosition - hv_YAxisStartValue) * hv_YScaleFactor;
                }
                //
                //Display grid lines
                //
                if ((int)(new HTuple(hv_GridColor.TupleNotEqual("none"))) != 0)
                {
                    hv_DotStyle.Dispose();
                    hv_DotStyle = new HTuple();
                    hv_DotStyle[0] = 5;
                    hv_DotStyle[1] = 7;
                    HOperatorSet.SetLineStyle(hv_WindowHandle, hv_DotStyle);
                    //if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_GridColor);
                    }
                    //
                    //Display x grid lines
                    if ((int)(new HTuple(hv_XGrid.TupleNotEqual("none"))) != 0)
                    {
                        if ((int)(new HTuple(hv_XGrid.TupleEqual("min_max_origin"))) != 0)
                        {
                            //Calculate 'min_max_origin' grid line coordinates
                            if ((int)(new HTuple(hv_YAxisPosition.TupleEqual(hv_XAxisStartValue))) != 0)
                            {
                                hv_XGridValues.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_XGridValues = new HTuple();
                                    hv_XGridValues = hv_XGridValues.TupleConcat(hv_XAxisStartValue, hv_XAxisEndValue);
                                }
                            }
                            else
                            {
                                hv_XGridValues.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_XGridValues = new HTuple();
                                    hv_XGridValues = hv_XGridValues.TupleConcat(hv_XAxisStartValue, hv_YAxisPosition, hv_XAxisEndValue);
                                }
                            }
                        }
                        else
                        {
                            //Calculate equidistant grid line coordinates
                            hv_XGridStart.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_XGridStart = (((hv_XAxisStartValue / hv_XGrid)).TupleCeil()
                                    ) * hv_XGrid;
                            }
                            hv_XGridValues.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_XGridValues = HTuple.TupleGenSequence(
                                    hv_XGridStart, hv_XAxisEndValue, hv_XGrid);
                            }
                        }
                        hv_XCoord.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_XCoord = (hv_XGridValues - hv_XAxisStartValue) * hv_XScaleFactor;
                        }
                        //Generate and display grid lines
                        for (hv_IndexGrid = 0; (int)hv_IndexGrid <= (int)((new HTuple(hv_XGridValues.TupleLength()
                            )) - 1); hv_IndexGrid = (int)hv_IndexGrid + 1)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_ContourXGrid.Dispose();
                                HOperatorSet.GenContourPolygonXld(out ho_ContourXGrid, ((hv_Height - hv_LowerBorder)).TupleConcat(
                                    hv_UpperBorder), ((hv_LeftBorder + (hv_XCoord.TupleSelect(hv_IndexGrid)))).TupleConcat(
                                    hv_LeftBorder + (hv_XCoord.TupleSelect(hv_IndexGrid))));
                            }
                            //if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.DispObj(ho_ContourXGrid, hv_WindowHandle);
                            }
                        }
                    }
                    //
                    //Display y grid lines
                    if ((int)(new HTuple(hv_YGrid.TupleNotEqual("none"))) != 0)
                    {
                        if ((int)(new HTuple(hv_YGrid.TupleEqual("min_max_origin"))) != 0)
                        {
                            //Calculate 'min_max_origin' grid line coordinates
                            if ((int)(new HTuple(hv_XAxisPosition.TupleEqual(hv_YAxisStartValue))) != 0)
                            {
                                hv_YGridValues.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_YGridValues = new HTuple();
                                    hv_YGridValues = hv_YGridValues.TupleConcat(hv_YAxisStartValue, hv_YAxisEndValue);
                                }
                            }
                            else
                            {
                                hv_YGridValues.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_YGridValues = new HTuple();
                                    hv_YGridValues = hv_YGridValues.TupleConcat(hv_YAxisStartValue, hv_XAxisPosition, hv_YAxisEndValue);
                                }
                            }
                        }
                        else
                        {
                            //Calculate equidistant grid line coordinates
                            hv_YGridStart.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_YGridStart = (((hv_YAxisStartValue / hv_YGrid)).TupleCeil()
                                    ) * hv_YGrid;
                            }
                            hv_YGridValues.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_YGridValues = HTuple.TupleGenSequence(
                                    hv_YGridStart, hv_YAxisEndValue, hv_YGrid);
                            }
                        }
                        hv_YCoord.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_YCoord = (hv_YGridValues - hv_YAxisStartValue) * hv_YScaleFactor;
                        }
                        //Generate and display grid lines
                        for (hv_IndexGrid = 0; (int)hv_IndexGrid <= (int)((new HTuple(hv_YGridValues.TupleLength()
                            )) - 1); hv_IndexGrid = (int)hv_IndexGrid + 1)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_ContourYGrid.Dispose();
                                HOperatorSet.GenContourPolygonXld(out ho_ContourYGrid, (((hv_Height - hv_LowerBorder) - (hv_YCoord.TupleSelect(
                                    hv_IndexGrid)))).TupleConcat((hv_Height - hv_LowerBorder) - (hv_YCoord.TupleSelect(
                                    hv_IndexGrid))), hv_LeftBorder.TupleConcat(hv_Width - hv_RightBorder));
                            }
                            //if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.DispObj(ho_ContourYGrid, hv_WindowHandle);
                            }
                        }
                    }
                }
                HOperatorSet.SetLineStyle(hv_WindowHandle, new HTuple());
                //
                //
                //Display the coordinate system axes
                if ((int)(new HTuple(hv_AxesColor.TupleNotEqual("none"))) != 0)
                {
                    //Display axes
                    //if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_AxesColor);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_XArrow.Dispose();
                        gen_arrow_contour_xld(out ho_XArrow, (hv_Height - hv_LowerBorder) - hv_XAxisOffsetPx,
                            hv_LeftBorder, (hv_Height - hv_LowerBorder) - hv_XAxisOffsetPx, hv_Width - hv_RightBorder,
                            0, 0);
                    }
                    //if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispObj(ho_XArrow, hv_WindowHandle);
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_YArrow.Dispose();
                        gen_arrow_contour_xld(out ho_YArrow, hv_Height - hv_LowerBorder, hv_LeftBorder + hv_YAxisOffsetPx,
                            hv_UpperBorder, hv_LeftBorder + hv_YAxisOffsetPx, 0, 0);
                    }
                    //if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.DispObj(ho_YArrow, hv_WindowHandle);
                    }
                    //Display labels
                    hv_Ascent.Dispose(); hv_Descent.Dispose(); hv_TextWidthXLabel.Dispose(); hv_TextHeightXLabel.Dispose();
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_XLabel, out hv_Ascent,
                        out hv_Descent, out hv_TextWidthXLabel, out hv_TextHeightXLabel);
                    hv_Ascent.Dispose(); hv_Descent.Dispose(); hv_TextWidthYLabel.Dispose(); hv_TextHeightYLabel.Dispose();
                    HOperatorSet.GetStringExtents(hv_WindowHandle, hv_YLabel, out hv_Ascent,
                        out hv_Descent, out hv_TextWidthYLabel, out hv_TextHeightYLabel);
                    if ((int)(new HTuple(hv_YTickDirection.TupleEqual("right"))) != 0)
                    {
                        if ((int)(new HTuple(hv_XTickDirection.TupleEqual("up"))) != 0)
                        {
                            //if (HDevWindowStack.IsOpen())
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, hv_XLabel, "image",
                                        ((hv_Height - hv_LowerBorder) - hv_TextHeightXLabel) - 3, ((hv_Width - hv_RightBorder) - hv_TextWidthXLabel) - 3,
                                        hv_AxesColor, "box", "false");
                                }
                            }
                            //if (HDevWindowStack.IsOpen())
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, " " + hv_YLabel, "image",
                                        hv_UpperBorder, (hv_LeftBorder + 3) + hv_YAxisOffsetPx, hv_AxesColor,
                                        "box", "false");
                                }
                            }
                        }
                        else
                        {
                            //if (HDevWindowStack.IsOpen())
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, hv_XLabel, "image",
                                        ((hv_Height - hv_LowerBorder) + 3) - hv_XAxisOffsetPx, ((hv_Width - hv_RightBorder) - hv_TextWidthXLabel) - 3,
                                        hv_AxesColor, "box", "false");
                                }
                            }
                            //if (HDevWindowStack.IsOpen())
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, " " + hv_YLabel, "image",
                                        ((hv_Height - hv_LowerBorder) - hv_TextHeightXLabel) - 3, (hv_LeftBorder + 3) + hv_YAxisOffsetPx,
                                        hv_AxesColor, "box", "false");
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((int)(new HTuple(hv_XTickDirection.TupleEqual("up"))) != 0)
                        {
                            //if (HDevWindowStack.IsOpen())
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, hv_XLabel, "image",
                                        ((hv_Height - hv_LowerBorder) - (2 * hv_TextHeightXLabel)) + 3, hv_LeftBorder - 3,
                                        hv_AxesColor, "box", "false");
                                }
                            }
                            //if (HDevWindowStack.IsOpen())
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, " " + hv_YLabel, "image",
                                        hv_UpperBorder, ((hv_Width - hv_RightBorder) - hv_TextWidthYLabel) - 13,
                                        hv_AxesColor, "box", "false");
                                }
                            }
                        }
                        else
                        {
                            //if (HDevWindowStack.IsOpen())
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, hv_XLabel, "image",
                                        ((hv_Height - hv_LowerBorder) + 3) - hv_XAxisOffsetPx, hv_LeftBorder - 3,
                                        hv_AxesColor, "box", "false");
                                }
                            }
                            //if (HDevWindowStack.IsOpen())
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    HOperatorSet.DispText(hv_WindowHandle, " " + hv_YLabel, "image",
                                        ((hv_Height - hv_LowerBorder) - hv_TextHeightXLabel) - 3, ((hv_Width - hv_RightBorder) - (2 * hv_TextWidthYLabel)) - 3,
                                        hv_AxesColor, "box", "false");
                                }
                            }
                        }
                    }
                }
                //
                //Display ticks
                //
                if ((int)(new HTuple(hv_AxesColor.TupleNotEqual("none"))) != 0)
                {
                   // if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(hv_WindowHandle, hv_AxesColor);
                    }
                    if ((int)(new HTuple(hv_XTicks.TupleNotEqual("none"))) != 0)
                    {
                        //
                        //Display x ticks
                        if ((int)(hv_XValuesAreStrings) != 0)
                        {
                            //Display string XValues as categories
                            hv_XTicks.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_XTicks = (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength()
                                    )) / (new HTuple(hv_XTickValues.TupleLength()));
                            }
                            hv_XCoord.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_XCoord = (hv_XValues_COPY_INP_TMP - hv_XAxisStartValue) * hv_XScaleFactor;
                            }
                        }
                        else
                        {
                            //Display tick values
                            if ((int)(new HTuple(hv_XTicks.TupleEqual("min_max_origin"))) != 0)
                            {
                                //Calculate 'min_max_origin' tick coordinates
                                if ((int)(new HTuple(hv_YAxisPosition.TupleEqual(hv_XAxisStartValue))) != 0)
                                {
                                    hv_XTickValues.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_XTickValues = new HTuple();
                                        hv_XTickValues = hv_XTickValues.TupleConcat(hv_XAxisStartValue, hv_XAxisEndValue);
                                    }
                                }
                                else
                                {
                                    hv_XTickValues.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_XTickValues = new HTuple();
                                        hv_XTickValues = hv_XTickValues.TupleConcat(hv_XAxisStartValue, hv_YAxisPosition, hv_XAxisEndValue);
                                    }
                                }
                            }
                            else
                            {
                                //Calculate equidistant tick coordinates
                                hv_XTickStart.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_XTickStart = (((hv_XAxisStartValue / hv_XTicks)).TupleCeil()
                                        ) * hv_XTicks;
                                }
                                hv_XTickValues.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_XTickValues = HTuple.TupleGenSequence(
                                        hv_XTickStart, hv_XAxisEndValue, hv_XTicks);
                                }
                            }
                            //Remove ticks that are smaller than the x-axis start.
                            hv_Indices.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Indices = ((hv_XTickValues.TupleLessElem(
                                    hv_XAxisStartValue))).TupleFind(1);
                            }
                            hv_XCoord.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_XCoord = (hv_XTickValues - hv_XAxisStartValue) * hv_XScaleFactor;
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_XCoord = hv_XCoord.TupleRemove(
                                        hv_Indices);
                                    hv_XCoord.Dispose();
                                    hv_XCoord = ExpTmpLocalVar_XCoord;
                                }
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_XTickValues = hv_XTickValues.TupleRemove(
                                        hv_Indices);
                                    hv_XTickValues.Dispose();
                                    hv_XTickValues = ExpTmpLocalVar_XTickValues;
                                }
                            }
                            //
                            if ((int)(new HTuple(hv_FormatX.TupleEqual("default"))) != 0)
                            {
                                hv_TypeTicks.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_TypeTicks = hv_XTicks.TupleType()
                                        ;
                                }
                                if ((int)(new HTuple(hv_TypeTicks.TupleEqual(4))) != 0)
                                {
                                    //String ('min_max_origin')
                                    //Format depends on actual values
                                    hv_TypeTicks.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_TypeTicks = hv_XTickValues.TupleType()
                                            ;
                                    }
                                }
                                if ((int)(new HTuple(hv_TypeTicks.TupleEqual(1))) != 0)
                                {
                                    //Round to integer
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        {
                                            HTuple
                                              ExpTmpLocalVar_XTickValues = hv_XTickValues.TupleInt()
                                                ;
                                            hv_XTickValues.Dispose();
                                            hv_XTickValues = ExpTmpLocalVar_XTickValues;
                                        }
                                    }
                                }
                                else
                                {
                                    //Use floating point numbers
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        {
                                            HTuple
                                              ExpTmpLocalVar_XTickValues = hv_XTickValues.TupleString(
                                                ".2f");
                                            hv_XTickValues.Dispose();
                                            hv_XTickValues = ExpTmpLocalVar_XTickValues;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_XTickValues = hv_XTickValues.TupleString(
                                            hv_FormatX);
                                        hv_XTickValues.Dispose();
                                        hv_XTickValues = ExpTmpLocalVar_XTickValues;
                                    }
                                }
                            }
                        }
                        //Generate and display ticks
                        for (hv_IndexTicks = 0; (int)hv_IndexTicks <= (int)((new HTuple(hv_XTickValues.TupleLength()
                            )) - 1); hv_IndexTicks = (int)hv_IndexTicks + 1)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Ascent1.Dispose(); hv_Descent1.Dispose(); hv_TextWidthXTicks.Dispose(); hv_TextHeightXTicks.Dispose();
                                HOperatorSet.GetStringExtents(hv_WindowHandle, hv_XTickValues.TupleSelect(
                                    hv_IndexTicks), out hv_Ascent1, out hv_Descent1, out hv_TextWidthXTicks,
                                    out hv_TextHeightXTicks);
                            }
                            if ((int)(new HTuple(hv_XTickDirection.TupleEqual("up"))) != 0)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    ho_ContourXTick.Dispose();
                                    HOperatorSet.GenContourPolygonXld(out ho_ContourXTick, (((hv_Height - hv_LowerBorder) - hv_XAxisOffsetPx)).TupleConcat(
                                        ((hv_Height - hv_LowerBorder) - hv_XAxisOffsetPx) - 5), ((hv_LeftBorder + (hv_XCoord.TupleSelect(
                                        hv_IndexTicks)))).TupleConcat(hv_LeftBorder + (hv_XCoord.TupleSelect(
                                        hv_IndexTicks))));
                                }
                               // if (HDevWindowStack.IsOpen())
                                {
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        HOperatorSet.DispText(hv_WindowHandle, hv_XTickValues.TupleSelect(
                                            hv_IndexTicks), "image", ((hv_Height - hv_LowerBorder) + 2) - hv_XAxisOffsetPx,
                                            hv_LeftBorder + (hv_XCoord.TupleSelect(hv_IndexTicks)), hv_AxesColor,
                                            "box", "false");
                                    }
                                }
                            }
                            else
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    ho_ContourXTick.Dispose();
                                    HOperatorSet.GenContourPolygonXld(out ho_ContourXTick, ((((hv_Height - hv_LowerBorder) - hv_XAxisOffsetPx) + 5)).TupleConcat(
                                        (hv_Height - hv_LowerBorder) - hv_XAxisOffsetPx), ((hv_LeftBorder + (hv_XCoord.TupleSelect(
                                        hv_IndexTicks)))).TupleConcat(hv_LeftBorder + (hv_XCoord.TupleSelect(
                                        hv_IndexTicks))));
                                }
                                //if (HDevWindowStack.IsOpen())
                                {
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        HOperatorSet.DispText(hv_WindowHandle, hv_XTickValues.TupleSelect(
                                            hv_IndexTicks), "image", ((hv_Height - hv_LowerBorder) - (2 * hv_TextHeightXTicks)) - hv_XAxisOffsetPx,
                                            hv_LeftBorder + (hv_XCoord.TupleSelect(hv_IndexTicks)), hv_AxesColor,
                                            "box", "false");
                                    }
                                }
                            }
                            //if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.DispObj(ho_ContourXTick, hv_WindowHandle);
                            }
                        }
                    }
                    //
                    if ((int)(new HTuple(hv_YTicks.TupleNotEqual("none"))) != 0)
                    {
                        //
                        //Display y ticks
                        if ((int)(new HTuple(hv_YTicks.TupleEqual("min_max_origin"))) != 0)
                        {
                            //Calculate 'min_max_origin' tick coordinates
                            if ((int)(new HTuple(hv_XAxisPosition.TupleEqual(hv_YAxisStartValue))) != 0)
                            {
                                hv_YTickValues.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_YTickValues = new HTuple();
                                    hv_YTickValues = hv_YTickValues.TupleConcat(hv_YAxisStartValue, hv_YAxisEndValue);
                                }
                            }
                            else
                            {
                                hv_YTickValues.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_YTickValues = new HTuple();
                                    hv_YTickValues = hv_YTickValues.TupleConcat(hv_YAxisStartValue, hv_XAxisPosition, hv_YAxisEndValue);
                                }
                            }
                        }
                        else
                        {
                            //Calculate equidistant tick coordinates
                            hv_YTickStart.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_YTickStart = (((hv_YAxisStartValue / hv_YTicks)).TupleCeil()
                                    ) * hv_YTicks;
                            }
                            hv_YTickValues.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_YTickValues = HTuple.TupleGenSequence(
                                    hv_YTickStart, hv_YAxisEndValue, hv_YTicks);
                            }
                        }
                        //Remove ticks that are smaller than the y-axis start.
                        hv_Indices.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Indices = ((hv_YTickValues.TupleLessElem(
                                hv_YAxisStartValue))).TupleFind(1);
                        }
                        hv_YCoord.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_YCoord = (hv_YTickValues - hv_YAxisStartValue) * hv_YScaleFactor;
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_YCoord = hv_YCoord.TupleRemove(
                                    hv_Indices);
                                hv_YCoord.Dispose();
                                hv_YCoord = ExpTmpLocalVar_YCoord;
                            }
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_YTickValues = hv_YTickValues.TupleRemove(
                                    hv_Indices);
                                hv_YTickValues.Dispose();
                                hv_YTickValues = ExpTmpLocalVar_YTickValues;
                            }
                        }
                        //
                        if ((int)(new HTuple(hv_FormatY.TupleEqual("default"))) != 0)
                        {
                            hv_TypeTicks.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_TypeTicks = hv_YTicks.TupleType()
                                    ;
                            }
                            if ((int)(new HTuple(hv_TypeTicks.TupleEqual(4))) != 0)
                            {
                                //String ('min_max_origin')
                                //Format depends on actual values
                                hv_TypeTicks.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_TypeTicks = hv_YTickValues.TupleType()
                                        ;
                                }
                            }
                            if ((int)(new HTuple(hv_TypeTicks.TupleEqual(1))) != 0)
                            {
                                //Round to integer
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_YTickValues = hv_YTickValues.TupleInt()
                                            ;
                                        hv_YTickValues.Dispose();
                                        hv_YTickValues = ExpTmpLocalVar_YTickValues;
                                    }
                                }
                            }
                            else
                            {
                                //Use floating point numbers
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    {
                                        HTuple
                                          ExpTmpLocalVar_YTickValues = hv_YTickValues.TupleString(
                                            ".2f");
                                        hv_YTickValues.Dispose();
                                        hv_YTickValues = ExpTmpLocalVar_YTickValues;
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_YTickValues = hv_YTickValues.TupleString(
                                        hv_FormatY);
                                    hv_YTickValues.Dispose();
                                    hv_YTickValues = ExpTmpLocalVar_YTickValues;
                                }
                            }
                        }
                        //Generate and display ticks
                        for (hv_IndexTicks = 0; (int)hv_IndexTicks <= (int)((new HTuple(hv_YTickValues.TupleLength()
                            )) - 1); hv_IndexTicks = (int)hv_IndexTicks + 1)
                        {
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Ascent1.Dispose(); hv_Descent1.Dispose(); hv_TextWidthYTicks.Dispose(); hv_TextHeightYTicks.Dispose();
                                HOperatorSet.GetStringExtents(hv_WindowHandle, hv_YTickValues.TupleSelect(
                                    hv_IndexTicks), out hv_Ascent1, out hv_Descent1, out hv_TextWidthYTicks,
                                    out hv_TextHeightYTicks);
                            }
                            if ((int)(new HTuple(hv_YTickDirection.TupleEqual("right"))) != 0)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    ho_ContourYTick.Dispose();
                                    HOperatorSet.GenContourPolygonXld(out ho_ContourYTick, (((hv_Height - hv_LowerBorder) - (hv_YCoord.TupleSelect(
                                        hv_IndexTicks)))).TupleConcat((hv_Height - hv_LowerBorder) - (hv_YCoord.TupleSelect(
                                        hv_IndexTicks))), ((hv_LeftBorder + hv_YAxisOffsetPx)).TupleConcat(
                                        (hv_LeftBorder + hv_YAxisOffsetPx) + 5));
                                }
                                //if (HDevWindowStack.IsOpen())
                                {
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        HOperatorSet.DispText(hv_WindowHandle, hv_YTickValues.TupleSelect(
                                            hv_IndexTicks), "image", (((hv_Height - hv_LowerBorder) - hv_TextHeightYTicks) + 3) - (hv_YCoord.TupleSelect(
                                            hv_IndexTicks)), ((hv_LeftBorder - hv_TextWidthYTicks) - 2) + hv_YAxisOffsetPx,
                                            hv_AxesColor, "box", "false");
                                    }
                                }
                            }
                            else
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    ho_ContourYTick.Dispose();
                                    HOperatorSet.GenContourPolygonXld(out ho_ContourYTick, (((hv_Height - hv_LowerBorder) - (hv_YCoord.TupleSelect(
                                        hv_IndexTicks)))).TupleConcat((hv_Height - hv_LowerBorder) - (hv_YCoord.TupleSelect(
                                        hv_IndexTicks))), (((hv_LeftBorder + hv_YAxisOffsetPx) - 5)).TupleConcat(
                                        hv_LeftBorder + hv_YAxisOffsetPx));
                                }
                                //if (HDevWindowStack.IsOpen())
                                {
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        HOperatorSet.DispText(hv_WindowHandle, hv_YTickValues.TupleSelect(
                                            hv_IndexTicks), "image", (((hv_Height - hv_LowerBorder) - hv_TextHeightYTicks) + 3) - (hv_YCoord.TupleSelect(
                                            hv_IndexTicks)), (hv_LeftBorder + 2) + hv_YAxisOffsetPx, hv_AxesColor,
                                            "box", "false");
                                    }
                                }
                            }
                            //if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.DispObj(ho_ContourYTick, hv_WindowHandle);
                            }
                        }
                    }
                }
                //
                //Display function plot
                //
                if ((int)(new HTuple(hv_Color.TupleNotEqual("none"))) != 0)
                {
                    if ((int)((new HTuple(hv_XValues_COPY_INP_TMP.TupleNotEqual(new HTuple()))).TupleAnd(
                        new HTuple(hv_YValues_COPY_INP_TMP.TupleNotEqual(new HTuple())))) != 0)
                    {
                        hv_Num.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Num = (new HTuple(hv_YValues_COPY_INP_TMP.TupleLength()
                                )) / (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength()));
                        }
                        //
                        //Iterate over all functions to be displayed
                        HTuple end_val576 = hv_Num - 1;
                        HTuple step_val576 = 1;
                        for (hv_I = 0; hv_I.Continue(end_val576, step_val576); hv_I = hv_I.TupleAdd(step_val576))
                        {
                            //Select y values for current function
                            hv_YSelected.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_YSelected = hv_YValues_COPY_INP_TMP.TupleSelectRange(
                                    hv_I * (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength())), ((hv_I + 1) * (new HTuple(hv_XValues_COPY_INP_TMP.TupleLength()
                                    ))) - 1);
                            }
                            //Set color
                            if ((int)(new HTuple(hv_Color.TupleEqual(new HTuple()))) != 0)
                            {
                                HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
                            }
                            else
                            {
                                //if (HDevWindowStack.IsOpen())
                                {
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        HOperatorSet.SetColor(hv_WindowHandle, hv_Color.TupleSelect(
                                            hv_I % (new HTuple(hv_Color.TupleLength()))));
                                    }
                                }
                            }
                            //
                            //Display in different styles
                            //
                            if ((int)((new HTuple(hv_Style.TupleEqual("line"))).TupleOr(new HTuple(hv_Style.TupleEqual(
                                new HTuple())))) != 0)
                            {
                                //Line
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    ho_Contour.Dispose();
                                    HOperatorSet.GenContourPolygonXld(out ho_Contour, ((hv_Height - hv_LowerBorder) - (hv_YSelected * hv_YScaleFactor)) + (hv_YAxisStartValue * hv_YScaleFactor),
                                        ((hv_XValues_COPY_INP_TMP * hv_XScaleFactor) + hv_LeftBorder) - (hv_XAxisStartValue * hv_XScaleFactor));
                                }
                                //Clip, if necessary
                                if ((int)(new HTuple(hv_Clip.TupleEqual("yes"))) != 0)
                                {
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        HObject ExpTmpOutVar_0;
                                        HOperatorSet.ClipContoursXld(ho_Contour, out ExpTmpOutVar_0, hv_UpperBorder,
                                            hv_LeftBorder, hv_Height - hv_LowerBorder, hv_Width - hv_RightBorder);
                                        ho_Contour.Dispose();
                                        ho_Contour = ExpTmpOutVar_0;
                                    }
                                }
                               // if (HDevWindowStack.IsOpen())
                                {
                                    HOperatorSet.DispObj(ho_Contour, hv_WindowHandle);
                                }
                            }
                            else if ((int)(new HTuple(hv_Style.TupleEqual("cross"))) != 0)
                            {
                                //Cross
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    ho_Cross.Dispose();
                                    HOperatorSet.GenCrossContourXld(out ho_Cross, ((hv_Height - hv_LowerBorder) - (hv_YSelected * hv_YScaleFactor)) + (hv_YAxisStartValue * hv_YScaleFactor),
                                        ((hv_XValues_COPY_INP_TMP * hv_XScaleFactor) + hv_LeftBorder) - (hv_XAxisStartValue * hv_XScaleFactor),
                                        6, 0.785398);
                                }
                                //Clip, if necessary
                                if ((int)(new HTuple(hv_Clip.TupleEqual("yes"))) != 0)
                                {
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        HObject ExpTmpOutVar_0;
                                        HOperatorSet.ClipContoursXld(ho_Cross, out ExpTmpOutVar_0, hv_UpperBorder,
                                            hv_LeftBorder, hv_Height - hv_LowerBorder, hv_Width - hv_RightBorder);
                                        ho_Cross.Dispose();
                                        ho_Cross = ExpTmpOutVar_0;
                                    }
                                }
                               // if (HDevWindowStack.IsOpen())
                                {
                                    HOperatorSet.DispObj(ho_Cross, hv_WindowHandle);
                                }
                            }
                            else if ((int)(new HTuple(hv_Style.TupleEqual("filled"))) != 0)
                            {
                                //Filled
                                hv_Y1Selected.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_Y1Selected = new HTuple();
                                    hv_Y1Selected = hv_Y1Selected.TupleConcat(0 + hv_XAxisPosition);
                                    hv_Y1Selected = hv_Y1Selected.TupleConcat(hv_YSelected);
                                    hv_Y1Selected = hv_Y1Selected.TupleConcat(0 + hv_XAxisPosition);
                                }
                                hv_X1Selected.Dispose();
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    hv_X1Selected = new HTuple();
                                    hv_X1Selected = hv_X1Selected.TupleConcat(hv_XValues_COPY_INP_TMP.TupleMin()
                                        );
                                    hv_X1Selected = hv_X1Selected.TupleConcat(hv_XValues_COPY_INP_TMP);
                                    hv_X1Selected = hv_X1Selected.TupleConcat(hv_XValues_COPY_INP_TMP.TupleMax()
                                        );
                                }
                               // if (HDevWindowStack.IsOpen())
                                {
                                    HOperatorSet.SetDraw(hv_WindowHandle, "fill");
                                }
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    ho_Filled.Dispose();
                                    HOperatorSet.GenRegionPolygonFilled(out ho_Filled, ((hv_Height - hv_LowerBorder) - (hv_Y1Selected * hv_YScaleFactor)) + (hv_YAxisStartValue * hv_YScaleFactor),
                                        ((hv_X1Selected * hv_XScaleFactor) + hv_LeftBorder) - (hv_XAxisStartValue * hv_XScaleFactor));
                                }
                                //Clip, if necessary
                                if ((int)(new HTuple(hv_Clip.TupleEqual("yes"))) != 0)
                                {
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        HObject ExpTmpOutVar_0;
                                        HOperatorSet.ClipRegion(ho_Filled, out ExpTmpOutVar_0, hv_UpperBorder,
                                            hv_LeftBorder, hv_Height - hv_LowerBorder, hv_Width - hv_RightBorder);
                                        ho_Filled.Dispose();
                                        ho_Filled = ExpTmpOutVar_0;
                                    }
                                }
                               // if (HDevWindowStack.IsOpen())
                                {
                                    HOperatorSet.DispObj(ho_Filled, hv_WindowHandle);
                                }
                            }
                            else if ((int)(new HTuple(hv_Style.TupleEqual("step"))) != 0)
                            {
                                ho_Stair.Dispose();
                                HOperatorSet.GenEmptyObj(out ho_Stair);
                                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_XValues_COPY_INP_TMP.TupleLength()
                                    )) - 2); hv_Index = (int)hv_Index + 1)
                                {
                                    hv_Row1.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_Row1 = ((hv_Height - hv_LowerBorder) - ((hv_YSelected.TupleSelect(
                                            hv_Index)) * hv_YScaleFactor)) + (hv_YAxisStartValue * hv_YScaleFactor);
                                    }
                                    hv_Row2.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_Row2 = ((hv_Height - hv_LowerBorder) - ((hv_YSelected.TupleSelect(
                                            hv_Index + 1)) * hv_YScaleFactor)) + (hv_YAxisStartValue * hv_YScaleFactor);
                                    }
                                    hv_Col1.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_Col1 = (((hv_XValues_COPY_INP_TMP.TupleSelect(
                                            hv_Index)) * hv_XScaleFactor) + hv_LeftBorder) - (hv_XAxisStartValue * hv_XScaleFactor);
                                    }
                                    hv_Col2.Dispose();
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        hv_Col2 = (((hv_XValues_COPY_INP_TMP.TupleSelect(
                                            hv_Index + 1)) * hv_XScaleFactor) + hv_LeftBorder) - (hv_XAxisStartValue * hv_XScaleFactor);
                                    }
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        ho_StairTmp.Dispose();
                                        HOperatorSet.GenContourPolygonXld(out ho_StairTmp, ((hv_Row1.TupleConcat(
                                            hv_Row1))).TupleConcat(hv_Row2), ((hv_Col1.TupleConcat(hv_Col2))).TupleConcat(
                                            hv_Col2));
                                    }
                                    {
                                        HObject ExpTmpOutVar_0;
                                        HOperatorSet.ConcatObj(ho_Stair, ho_StairTmp, out ExpTmpOutVar_0);
                                        ho_Stair.Dispose();
                                        ho_Stair = ExpTmpOutVar_0;
                                    }
                                }
                                {
                                    HObject ExpTmpOutVar_0;
                                    HOperatorSet.UnionAdjacentContoursXld(ho_Stair, out ExpTmpOutVar_0,
                                        0.1, 0.1, "attr_keep");
                                    ho_Stair.Dispose();
                                    ho_Stair = ExpTmpOutVar_0;
                                }
                                if ((int)(new HTuple(hv_Clip.TupleEqual("yes"))) != 0)
                                {
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        HObject ExpTmpOutVar_0;
                                        HOperatorSet.ClipRegion(ho_Stair, out ExpTmpOutVar_0, hv_UpperBorder,
                                            hv_LeftBorder, hv_Height - hv_LowerBorder, hv_Width - hv_RightBorder);
                                        ho_Stair.Dispose();
                                        ho_Stair = ExpTmpOutVar_0;
                                    }
                                }
                                //if (HDevWindowStack.IsOpen())
                                {
                                    HOperatorSet.DispObj(ho_Stair, hv_WindowHandle);
                                }
                            }
                            else
                            {
                                throw new HalconException("Unsupported style: " + hv_Style);
                            }
                        }
                    }
                }
                //
                //
                //Reset original display settings
                //if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetPart(hv_WindowHandle, hv_PartRow1, hv_PartColumn1,
                        hv_PartRow2, hv_PartColumn2);
                }
                HDevWindowStack.SetActive(hv_PreviousWindowHandle);
                HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
               // if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
                }
                HOperatorSet.SetLineStyle(hv_WindowHandle, hv_OriginStyle);
                HOperatorSet.SetSystem("clip_region", hv_ClipRegion);
                ho_ContourXGrid.Dispose();
                ho_ContourYGrid.Dispose();
                ho_XArrow.Dispose();
                ho_YArrow.Dispose();
                ho_ContourXTick.Dispose();
                ho_ContourYTick.Dispose();
                ho_Contour.Dispose();
                ho_Cross.Dispose();
                ho_Filled.Dispose();
                ho_Stair.Dispose();
                ho_StairTmp.Dispose();

                hv_XValues_COPY_INP_TMP.Dispose();
                hv_YValues_COPY_INP_TMP.Dispose();
                //hv_PreviousWindowHandle.Dispose();
                hv_ClipRegion.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_PartRow1.Dispose();
                hv_PartColumn1.Dispose();
                hv_PartRow2.Dispose();
                hv_PartColumn2.Dispose();
                hv_Red.Dispose();
                hv_Green.Dispose();
                hv_Blue.Dispose();
                hv_DrawMode.Dispose();
                hv_OriginStyle.Dispose();
                hv_XAxisEndValue.Dispose();
                hv_YAxisEndValue.Dispose();
                hv_XAxisStartValue.Dispose();
                hv_YAxisStartValue.Dispose();
                hv_XValuesAreStrings.Dispose();
                hv_XTickValues.Dispose();
                hv_XTicks.Dispose();
                hv_YAxisPosition.Dispose();
                hv_XAxisPosition.Dispose();
                hv_LeftBorder.Dispose();
                hv_RightBorder.Dispose();
                hv_UpperBorder.Dispose();
                hv_LowerBorder.Dispose();
                hv_AxesColor.Dispose();
                hv_Style.Dispose();
                hv_Clip.Dispose();
                hv_YTicks.Dispose();
                hv_XGrid.Dispose();
                hv_YGrid.Dispose();
                hv_GridColor.Dispose();
                hv_YPosition.Dispose();
                hv_FormatX.Dispose();
                hv_FormatY.Dispose();
                hv_NumGenParamNames.Dispose();
                hv_NumGenParamValues.Dispose();
                hv_GenParamIndex.Dispose();
                hv_XGridTicks.Dispose();
                hv_YTickDirection.Dispose();
                hv_XTickDirection.Dispose();
                hv_XAxisWidthPx.Dispose();
                hv_XAxisWidth.Dispose();
                hv_XScaleFactor.Dispose();
                hv_YAxisHeightPx.Dispose();
                hv_YAxisHeight.Dispose();
                hv_YScaleFactor.Dispose();
                hv_YAxisOffsetPx.Dispose();
                hv_XAxisOffsetPx.Dispose();
                hv_DotStyle.Dispose();
                hv_XGridValues.Dispose();
                hv_XGridStart.Dispose();
                hv_XCoord.Dispose();
                hv_IndexGrid.Dispose();
                hv_YGridValues.Dispose();
                hv_YGridStart.Dispose();
                hv_YCoord.Dispose();
                hv_Ascent.Dispose();
                hv_Descent.Dispose();
                hv_TextWidthXLabel.Dispose();
                hv_TextHeightXLabel.Dispose();
                hv_TextWidthYLabel.Dispose();
                hv_TextHeightYLabel.Dispose();
                hv_XTickStart.Dispose();
                hv_Indices.Dispose();
                hv_TypeTicks.Dispose();
                hv_IndexTicks.Dispose();
                hv_Ascent1.Dispose();
                hv_Descent1.Dispose();
                hv_TextWidthXTicks.Dispose();
                hv_TextHeightXTicks.Dispose();
                hv_YTickValues.Dispose();
                hv_YTickStart.Dispose();
                hv_TextWidthYTicks.Dispose();
                hv_TextHeightYTicks.Dispose();
                hv_Num.Dispose();
                hv_I.Dispose();
                hv_YSelected.Dispose();
                hv_Y1Selected.Dispose();
                hv_X1Selected.Dispose();
                hv_Index.Dispose();
                hv_Row1.Dispose();
                hv_Row2.Dispose();
                hv_Col1.Dispose();
                hv_Col2.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_ContourXGrid.Dispose();
                ho_ContourYGrid.Dispose();
                ho_XArrow.Dispose();
                ho_YArrow.Dispose();
                ho_ContourXTick.Dispose();
                ho_ContourYTick.Dispose();
                ho_Contour.Dispose();
                ho_Cross.Dispose();
                ho_Filled.Dispose();
                ho_Stair.Dispose();
                ho_StairTmp.Dispose();

                hv_XValues_COPY_INP_TMP.Dispose();
                hv_YValues_COPY_INP_TMP.Dispose();
                hv_PreviousWindowHandle.Dispose();
                hv_ClipRegion.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_PartRow1.Dispose();
                hv_PartColumn1.Dispose();
                hv_PartRow2.Dispose();
                hv_PartColumn2.Dispose();
                hv_Red.Dispose();
                hv_Green.Dispose();
                hv_Blue.Dispose();
                hv_DrawMode.Dispose();
                hv_OriginStyle.Dispose();
                hv_XAxisEndValue.Dispose();
                hv_YAxisEndValue.Dispose();
                hv_XAxisStartValue.Dispose();
                hv_YAxisStartValue.Dispose();
                hv_XValuesAreStrings.Dispose();
                hv_XTickValues.Dispose();
                hv_XTicks.Dispose();
                hv_YAxisPosition.Dispose();
                hv_XAxisPosition.Dispose();
                hv_LeftBorder.Dispose();
                hv_RightBorder.Dispose();
                hv_UpperBorder.Dispose();
                hv_LowerBorder.Dispose();
                hv_AxesColor.Dispose();
                hv_Style.Dispose();
                hv_Clip.Dispose();
                hv_YTicks.Dispose();
                hv_XGrid.Dispose();
                hv_YGrid.Dispose();
                hv_GridColor.Dispose();
                hv_YPosition.Dispose();
                hv_FormatX.Dispose();
                hv_FormatY.Dispose();
                hv_NumGenParamNames.Dispose();
                hv_NumGenParamValues.Dispose();
                hv_GenParamIndex.Dispose();
                hv_XGridTicks.Dispose();
                hv_YTickDirection.Dispose();
                hv_XTickDirection.Dispose();
                hv_XAxisWidthPx.Dispose();
                hv_XAxisWidth.Dispose();
                hv_XScaleFactor.Dispose();
                hv_YAxisHeightPx.Dispose();
                hv_YAxisHeight.Dispose();
                hv_YScaleFactor.Dispose();
                hv_YAxisOffsetPx.Dispose();
                hv_XAxisOffsetPx.Dispose();
                hv_DotStyle.Dispose();
                hv_XGridValues.Dispose();
                hv_XGridStart.Dispose();
                hv_XCoord.Dispose();
                hv_IndexGrid.Dispose();
                hv_YGridValues.Dispose();
                hv_YGridStart.Dispose();
                hv_YCoord.Dispose();
                hv_Ascent.Dispose();
                hv_Descent.Dispose();
                hv_TextWidthXLabel.Dispose();
                hv_TextHeightXLabel.Dispose();
                hv_TextWidthYLabel.Dispose();
                hv_TextHeightYLabel.Dispose();
                hv_XTickStart.Dispose();
                hv_Indices.Dispose();
                hv_TypeTicks.Dispose();
                hv_IndexTicks.Dispose();
                hv_Ascent1.Dispose();
                hv_Descent1.Dispose();
                hv_TextWidthXTicks.Dispose();
                hv_TextHeightXTicks.Dispose();
                hv_YTickValues.Dispose();
                hv_YTickStart.Dispose();
                hv_TextWidthYTicks.Dispose();
                hv_TextHeightYTicks.Dispose();
                hv_Num.Dispose();
                hv_I.Dispose();
                hv_YSelected.Dispose();
                hv_Y1Selected.Dispose();
                hv_X1Selected.Dispose();
                hv_Index.Dispose();
                hv_Row1.Dispose();
                hv_Row2.Dispose();
                hv_Col1.Dispose();
                hv_Col2.Dispose();

                throw HDevExpDefaultException;
            }
        }


        // Chapter: Deep Learning / Classification
        // Short Description: Preprocess images for deep-learning-based classification training and inference. 
        public static void preprocess_dl_classifier_images(HObject ho_Images, out HObject ho_ImagesPreprocessed,
            HTuple hv_GenParamName, HTuple hv_GenParamValue, HTuple hv_DLClassifierHandle)
        {




            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_ImagesNew = null, ho_ImageSelected = null;
            HObject ho_ObjectSelected = null, ho_ThreeChannelImage = null;
            HObject ho_SingleChannelImage = null;

            // Local copy input parameter variables 
            HObject ho_Images_COPY_INP_TMP;
            ho_Images_COPY_INP_TMP = new HObject(ho_Images);



            // Local control variables 

            HTuple hv_ContrastNormalization = new HTuple();
            HTuple hv_DomainHandling = new HTuple(), hv_GenParamIndex = new HTuple();
            HTuple hv_ImageWidth = new HTuple(), hv_ImageHeight = new HTuple();
            HTuple hv_ImageRangeMin = new HTuple(), hv_ImageRangeMax = new HTuple();
            HTuple hv_ImageNumChannels = new HTuple(), hv_ImageWidthInput = new HTuple();
            HTuple hv_ImageHeightInput = new HTuple(), hv_EqualWidth = new HTuple();
            HTuple hv_EqualHeight = new HTuple(), hv_Type = new HTuple();
            HTuple hv_NumMatches = new HTuple(), hv_NumImages = new HTuple();
            HTuple hv_ImageIndex = new HTuple(), hv_Min = new HTuple();
            HTuple hv_Max = new HTuple(), hv_Range = new HTuple();
            HTuple hv_Scale = new HTuple(), hv_Shift = new HTuple();
            HTuple hv_EqualByte = new HTuple(), hv_RescaleRange = new HTuple();
            HTuple hv_NumChannels = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImagesPreprocessed);
            HOperatorSet.GenEmptyObj(out ho_ImagesNew);
            HOperatorSet.GenEmptyObj(out ho_ImageSelected);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_ThreeChannelImage);
            HOperatorSet.GenEmptyObj(out ho_SingleChannelImage);
            try
            {
                //This procedure preprocesses the provided images given by Image
                //so that they can be handled by
                //train_dl_classifier_batch and apply_dl_classifier_batch.
                //Note that depending on the images,
                //additional preprocessing steps might be beneficial.
                //
                //Set defaults.
                hv_ContrastNormalization.Dispose();
                hv_ContrastNormalization = "false";
                hv_DomainHandling.Dispose();
                hv_DomainHandling = "full_domain";
                //Set generic parameters.
                for (hv_GenParamIndex = 0; (int)hv_GenParamIndex <= (int)((new HTuple(hv_GenParamName.TupleLength()
                    )) - 1); hv_GenParamIndex = (int)hv_GenParamIndex + 1)
                {
                    if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "contrast_normalization"))) != 0)
                    {
                        //Set 'contrast_normalization'
                        hv_ContrastNormalization.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_ContrastNormalization = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                    }
                    else if ((int)(new HTuple(((hv_GenParamName.TupleSelect(hv_GenParamIndex))).TupleEqual(
                        "domain_handling"))) != 0)
                    {
                        //Set 'domain_handling'
                        hv_DomainHandling.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_DomainHandling = hv_GenParamValue.TupleSelect(
                                hv_GenParamIndex);
                        }
                    }
                    else
                    {
                        throw new HalconException(("Unknown generic parameter: '" + (hv_GenParamName.TupleSelect(
                            hv_GenParamIndex))) + "'");
                    }
                }
                //
                //Get the network's image requirements
                //from the handle of the classifier
                //and use them as preprocessing parameters.
                //
                //Expected input image size:
                hv_ImageWidth.Dispose();
                HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "image_width", out hv_ImageWidth);
                hv_ImageHeight.Dispose();
                HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "image_height", out hv_ImageHeight);
                //Expected gray value range:
                hv_ImageRangeMin.Dispose();
                HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "image_range_min",
                    out hv_ImageRangeMin);
                hv_ImageRangeMax.Dispose();
                HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "image_range_max",
                    out hv_ImageRangeMax);
                //Expected number of channels:
                hv_ImageNumChannels.Dispose();
                HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "image_num_channels",
                    out hv_ImageNumChannels);
                //
                //Preprocess the images.
                //
                if ((int)(new HTuple(hv_DomainHandling.TupleEqual("full_domain"))) != 0)
                {
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.FullDomain(ho_Images_COPY_INP_TMP, out ExpTmpOutVar_0);
                        ho_Images_COPY_INP_TMP.Dispose();
                        ho_Images_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                }
                else if ((int)(new HTuple(hv_DomainHandling.TupleEqual("crop_domain"))) != 0)
                {
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.CropDomain(ho_Images_COPY_INP_TMP, out ExpTmpOutVar_0);
                        ho_Images_COPY_INP_TMP.Dispose();
                        ho_Images_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                }
                else
                {
                    throw new HalconException("Unsupported parameter value for 'domain_handling'");
                }
                //
                //Zoom images only if they have a different size than the specified size
                hv_ImageWidthInput.Dispose(); hv_ImageHeightInput.Dispose();
                HOperatorSet.GetImageSize(ho_Images_COPY_INP_TMP, out hv_ImageWidthInput, out hv_ImageHeightInput);
                hv_EqualWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_EqualWidth = hv_ImageWidth.TupleEqualElem(
                        hv_ImageWidthInput);
                }
                hv_EqualHeight.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_EqualHeight = hv_ImageHeight.TupleEqualElem(
                        hv_ImageHeightInput);
                }
                if ((int)((new HTuple(((hv_EqualWidth.TupleMin())).TupleEqual(0))).TupleOr(
                    new HTuple(((hv_EqualHeight.TupleMin())).TupleEqual(0)))) != 0)
                {
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ZoomImageSize(ho_Images_COPY_INP_TMP, out ExpTmpOutVar_0, hv_ImageWidth,
                            hv_ImageHeight, "constant");
                        ho_Images_COPY_INP_TMP.Dispose();
                        ho_Images_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                }
                if ((int)(new HTuple(hv_ContrastNormalization.TupleEqual("true"))) != 0)
                {
                    //Check the type of the input images.
                    //Contrast normalization works here only for byte, integer and real images.
                    hv_Type.Dispose();
                    HOperatorSet.GetImageType(ho_Images_COPY_INP_TMP, out hv_Type);
                    hv_NumMatches.Dispose();
                    HOperatorSet.TupleRegexpTest(hv_Type, "byte|int|real", out hv_NumMatches);
                    hv_NumImages.Dispose();
                    HOperatorSet.CountObj(ho_Images_COPY_INP_TMP, out hv_NumImages);
                    if ((int)(new HTuple(hv_NumMatches.TupleNotEqual(hv_NumImages))) != 0)
                    {
                        throw new HalconException(new HTuple("In case of contrast normalization, please provide only images of type 'byte', 'int1', 'int2', 'uint2', 'int4', 'int8', or 'real'."));
                    }
                    //
                    //Perform contrast normalization
                    if ((int)(new HTuple(hv_Type.TupleEqual("byte"))) != 0)
                    {
                        //Scale the gray values to [0-255].
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.ScaleImageMax(ho_Images_COPY_INP_TMP, out ExpTmpOutVar_0);
                            ho_Images_COPY_INP_TMP.Dispose();
                            ho_Images_COPY_INP_TMP = ExpTmpOutVar_0;
                        }
                    }
                    else
                    {
                        //Scale the gray values to [ImageRangeMin-ImageRangeMax].
                        //Scaling is performed separately for each image.
                        ho_ImagesNew.Dispose();
                        HOperatorSet.GenEmptyObj(out ho_ImagesNew);
                        HTuple end_val70 = hv_NumImages;
                        HTuple step_val70 = 1;
                        for (hv_ImageIndex = 1; hv_ImageIndex.Continue(end_val70, step_val70); hv_ImageIndex = hv_ImageIndex.TupleAdd(step_val70))
                        {
                            ho_ImageSelected.Dispose();
                            HOperatorSet.SelectObj(ho_Images_COPY_INP_TMP, out ho_ImageSelected,
                                hv_ImageIndex);
                            hv_Min.Dispose(); hv_Max.Dispose(); hv_Range.Dispose();
                            HOperatorSet.MinMaxGray(ho_ImageSelected, ho_ImageSelected, 0, out hv_Min,
                                out hv_Max, out hv_Range);
                            hv_Scale.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Scale = (hv_ImageRangeMax - hv_ImageRangeMin) / (hv_Max - hv_Min);
                            }
                            hv_Shift.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Shift = ((-hv_Scale) * hv_Min) + hv_ImageRangeMin;
                            }
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.ScaleImage(ho_ImageSelected, out ExpTmpOutVar_0, hv_Scale,
                                    hv_Shift);
                                ho_ImageSelected.Dispose();
                                ho_ImageSelected = ExpTmpOutVar_0;
                            }
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.ConcatObj(ho_ImagesNew, ho_ImageSelected, out ExpTmpOutVar_0
                                    );
                                ho_ImagesNew.Dispose();
                                ho_ImagesNew = ExpTmpOutVar_0;
                            }
                        }
                        ho_Images_COPY_INP_TMP.Dispose();
                        ho_Images_COPY_INP_TMP = new HObject(ho_ImagesNew);
                        //Integer image convert to real image
                        if ((int)(new HTuple(hv_Type.TupleNotEqual("real"))) != 0)
                        {
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.ConvertImageType(ho_Images_COPY_INP_TMP, out ExpTmpOutVar_0,
                                    "real");
                                ho_Images_COPY_INP_TMP.Dispose();
                                ho_Images_COPY_INP_TMP = ExpTmpOutVar_0;
                            }
                        }
                    }
                }
                else if ((int)(new HTuple(hv_ContrastNormalization.TupleNotEqual("false"))) != 0)
                {
                    throw new HalconException("Unsupported parameter value for 'contrast_normalization'");
                }
                //Check the type of the input images.
                //If the type is not 'byte',
                //the gray value scaling does not work correctly.
                hv_Type.Dispose();
                HOperatorSet.GetImageType(ho_Images_COPY_INP_TMP, out hv_Type);
                hv_NumMatches.Dispose();
                HOperatorSet.TupleRegexpTest(hv_Type, "byte|real", out hv_NumMatches);
                hv_NumImages.Dispose();
                HOperatorSet.CountObj(ho_Images_COPY_INP_TMP, out hv_NumImages);
                if ((int)(new HTuple(hv_NumMatches.TupleNotEqual(hv_NumImages))) != 0)
                {
                    throw new HalconException("Please provide only images of type 'byte' or 'real'.");
                }
                hv_EqualByte.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_EqualByte = hv_Type.TupleEqualElem(
                        "byte");
                }
                if ((int)(new HTuple(((hv_EqualByte.TupleMax())).TupleEqual(1))) != 0)
                {
                    if ((int)(new HTuple(((hv_EqualByte.TupleMin())).TupleEqual(0))) != 0)
                    {
                        throw new HalconException("Passing mixed type images is not supported.");
                    }
                    //Convert the image type from byte to real,
                    //because the classifier expects 'real' images.
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConvertImageType(ho_Images_COPY_INP_TMP, out ExpTmpOutVar_0,
                            "real");
                        ho_Images_COPY_INP_TMP.Dispose();
                        ho_Images_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                    //Scale/Shift the gray values from [0-255] to the expected range.
                    hv_RescaleRange.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_RescaleRange = (hv_ImageRangeMax - hv_ImageRangeMin) / 255.0;
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ScaleImage(ho_Images_COPY_INP_TMP, out ExpTmpOutVar_0, hv_RescaleRange,
                            hv_ImageRangeMin);
                        ho_Images_COPY_INP_TMP.Dispose();
                        ho_Images_COPY_INP_TMP = ExpTmpOutVar_0;
                    }
                }
                else
                {
                    //For real images it is assumed that the range is already correct
                }

                //Check the number of channels.
                hv_NumImages.Dispose();
                HOperatorSet.CountObj(ho_Images_COPY_INP_TMP, out hv_NumImages);
                HTuple end_val113 = hv_NumImages;
                HTuple step_val113 = 1;
                for (hv_ImageIndex = 1; hv_ImageIndex.Continue(end_val113, step_val113); hv_ImageIndex = hv_ImageIndex.TupleAdd(step_val113))
                {
                    ho_ObjectSelected.Dispose();
                    HOperatorSet.SelectObj(ho_Images_COPY_INP_TMP, out ho_ObjectSelected, hv_ImageIndex);
                    hv_NumChannels.Dispose();
                    HOperatorSet.CountChannels(ho_ObjectSelected, out hv_NumChannels);
                    if ((int)(new HTuple(hv_NumChannels.TupleNotEqual(hv_ImageNumChannels))) != 0)
                    {
                        //
                        if ((int)((new HTuple(hv_NumChannels.TupleEqual(1))).TupleAnd(new HTuple(hv_ImageNumChannels.TupleEqual(
                            3)))) != 0)
                        {
                            //If the image is a grayscale image, but the classifier expects a color image:
                            //convert it to an image with three channels.
                            ho_ThreeChannelImage.Dispose();
                            HOperatorSet.Compose3(ho_ObjectSelected, ho_ObjectSelected, ho_ObjectSelected,
                                out ho_ThreeChannelImage);
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.ReplaceObj(ho_Images_COPY_INP_TMP, ho_ThreeChannelImage,
                                    out ExpTmpOutVar_0, hv_ImageIndex);
                                ho_Images_COPY_INP_TMP.Dispose();
                                ho_Images_COPY_INP_TMP = ExpTmpOutVar_0;
                            }
                        }
                        else if ((int)((new HTuple(hv_NumChannels.TupleEqual(3))).TupleAnd(
                            new HTuple(hv_ImageNumChannels.TupleEqual(1)))) != 0)
                        {
                            //If the image is a color image, but the classifier expects a grayscale image:
                            //convert it to an image with only one channel.
                            ho_SingleChannelImage.Dispose();
                            HOperatorSet.Rgb1ToGray(ho_ObjectSelected, out ho_SingleChannelImage);
                            {
                                HObject ExpTmpOutVar_0;
                                HOperatorSet.ReplaceObj(ho_Images_COPY_INP_TMP, ho_SingleChannelImage,
                                    out ExpTmpOutVar_0, hv_ImageIndex);
                                ho_Images_COPY_INP_TMP.Dispose();
                                ho_Images_COPY_INP_TMP = ExpTmpOutVar_0;
                            }
                        }
                        else
                        {
                            throw new HalconException("Number of channels not supported. Please provide a grayscale or an RGB image.");
                        }
                        //
                    }
                }
                ho_ImagesPreprocessed.Dispose();
                ho_ImagesPreprocessed = new HObject(ho_Images_COPY_INP_TMP);
                ho_Images_COPY_INP_TMP.Dispose();
                ho_ImagesNew.Dispose();
                ho_ImageSelected.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ThreeChannelImage.Dispose();
                ho_SingleChannelImage.Dispose();

                hv_ContrastNormalization.Dispose();
                hv_DomainHandling.Dispose();
                hv_GenParamIndex.Dispose();
                hv_ImageWidth.Dispose();
                hv_ImageHeight.Dispose();
                hv_ImageRangeMin.Dispose();
                hv_ImageRangeMax.Dispose();
                hv_ImageNumChannels.Dispose();
                hv_ImageWidthInput.Dispose();
                hv_ImageHeightInput.Dispose();
                hv_EqualWidth.Dispose();
                hv_EqualHeight.Dispose();
                hv_Type.Dispose();
                hv_NumMatches.Dispose();
                hv_NumImages.Dispose();
                hv_ImageIndex.Dispose();
                hv_Min.Dispose();
                hv_Max.Dispose();
                hv_Range.Dispose();
                hv_Scale.Dispose();
                hv_Shift.Dispose();
                hv_EqualByte.Dispose();
                hv_RescaleRange.Dispose();
                hv_NumChannels.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_Images_COPY_INP_TMP.Dispose();
                ho_ImagesNew.Dispose();
                ho_ImageSelected.Dispose();
                ho_ObjectSelected.Dispose();
                ho_ThreeChannelImage.Dispose();
                ho_SingleChannelImage.Dispose();

                hv_ContrastNormalization.Dispose();
                hv_DomainHandling.Dispose();
                hv_GenParamIndex.Dispose();
                hv_ImageWidth.Dispose();
                hv_ImageHeight.Dispose();
                hv_ImageRangeMin.Dispose();
                hv_ImageRangeMax.Dispose();
                hv_ImageNumChannels.Dispose();
                hv_ImageWidthInput.Dispose();
                hv_ImageHeightInput.Dispose();
                hv_EqualWidth.Dispose();
                hv_EqualHeight.Dispose();
                hv_Type.Dispose();
                hv_NumMatches.Dispose();
                hv_NumImages.Dispose();
                hv_ImageIndex.Dispose();
                hv_Min.Dispose();
                hv_Max.Dispose();
                hv_Range.Dispose();
                hv_Scale.Dispose();
                hv_Shift.Dispose();
                hv_EqualByte.Dispose();
                hv_RescaleRange.Dispose();
                hv_NumChannels.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Deep Learning / Classification
        // Short Description: Read the data set containing the images and their respective ground truth labels.  
        public static void read_dl_classifier_data_set(HTuple hv_ImageDirectory, HTuple hv_LabelSource,
            out HTuple hv_ImageFiles, out HTuple hv_GroundTruthLabels, out HTuple hv_LabelIndices,
            out HTuple hv_UniqueClasses)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_LabelsTmp = new HTuple(), hv_ClassIndex = new HTuple();
            // Initialize local and output iconic variables 
            hv_ImageFiles = new HTuple();
            hv_GroundTruthLabels = new HTuple();
            hv_LabelIndices = new HTuple();
            hv_UniqueClasses = new HTuple();
            try
            {
                //This procedures lists all ImageFiles
                //located in ImageDirectory and its subdirectories,
                //and returns the label of each image in GroundTruthLabels.
                //LabelSource determines how the ground truth labels are extracted.
                //Additionally, indices are assigned to the labels,
                //which can be used for the training instead
                //of the string labels, which is more time efficient.
                //The order of indices corresponds with the returned
                //unique Classes.
                //
                //Check the parameter ImageDirectory.
                if ((int)(((hv_ImageDirectory.TupleIsString())).TupleNot()) != 0)
                {
                    throw new HalconException(("ImageDirectory " + hv_ImageDirectory) + "is not a string.");
                }
                //
                //List all images in the provided directory
                //and its subdirectories ('recursive').
                hv_ImageFiles.Dispose();
                list_image_files(hv_ImageDirectory, ((((((((((((((new HTuple("hobj")).TupleConcat(
                    "ima")).TupleConcat("bmp")).TupleConcat("jpg")).TupleConcat("png")).TupleConcat(
                    "tiff")).TupleConcat("tif")).TupleConcat("gif")).TupleConcat("jpeg")).TupleConcat(
                    "pcx")).TupleConcat("pgm")).TupleConcat("ppm")).TupleConcat("pbm")).TupleConcat(
                    "xwd")).TupleConcat("pnm"), (new HTuple("recursive")).TupleConcat("follow_links"),
                    out hv_ImageFiles);
                if ((int)(new HTuple((new HTuple(hv_ImageFiles.TupleLength())).TupleEqual(0))) != 0)
                {
                    throw new HalconException(("Error: Could not find any image files in folder: \"" + hv_ImageDirectory) + "\"");
                }
                //
                //Get the ground truth labels.
                //Note that when configuring your own LabelSource mode,
                //you might find the procedure parse_filename helpful.
                if ((int)(new HTuple(hv_LabelSource.TupleEqual("last_folder"))) != 0)
                {
                    //The last folder name containing the image
                    //is used as label.
                    hv_GroundTruthLabels.Dispose();
                    HOperatorSet.TupleRegexpMatch(hv_ImageFiles, ".*/([^/]+)/[^/]*$", out hv_GroundTruthLabels);
                }
                else if ((int)(new HTuple(hv_LabelSource.TupleEqual("file_name"))) != 0)
                {
                    //The file name of each image is used as label.
                    hv_GroundTruthLabels.Dispose();
                    HOperatorSet.TupleRegexpMatch(hv_ImageFiles, ".*/([^/]+)[.][^/]*$", out hv_GroundTruthLabels);
                }
                else if ((int)(new HTuple(hv_LabelSource.TupleEqual("file_name_remove_index"))) != 0)
                {
                    //The file name of each image is used as label.
                    //All consecutive digits and underscores
                    //at the end of the file name are removed.
                    hv_LabelsTmp.Dispose();
                    HOperatorSet.TupleRegexpMatch(hv_ImageFiles, ".*/([^/]+)[.][^/]*$", out hv_LabelsTmp);
                    hv_GroundTruthLabels.Dispose();
                    HOperatorSet.TupleRegexpReplace(hv_LabelsTmp, "[0-9_]*$", "", out hv_GroundTruthLabels);
                }
                else if ((int)(new HTuple(hv_LabelSource.TupleEqual(new HTuple()))) != 0)
                {
                    hv_GroundTruthLabels.Dispose();
                    hv_GroundTruthLabels = new HTuple();
                }
                else
                {
                    throw new HalconException("LabelSource not supported.");
                }
                //Get the unique elements of Labels,
                //which represent the classes.
                hv_UniqueClasses.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_UniqueClasses = ((hv_GroundTruthLabels.TupleSort()
                        )).TupleUniq();
                }
                //Assign indices to the labels.
                hv_LabelIndices.Dispose();
                hv_LabelIndices = new HTuple(hv_GroundTruthLabels);
                for (hv_ClassIndex = 0; (int)hv_ClassIndex <= (int)((new HTuple(hv_UniqueClasses.TupleLength()
                    )) - 1); hv_ClassIndex = (int)hv_ClassIndex + 1)
                {
                    if (hv_LabelIndices == null)
                        hv_LabelIndices = new HTuple();
                    hv_LabelIndices[hv_LabelIndices.TupleFind(hv_UniqueClasses.TupleSelect(hv_ClassIndex))] = hv_ClassIndex;
                }

                hv_LabelsTmp.Dispose();
                hv_ClassIndex.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_LabelsTmp.Dispose();
                hv_ClassIndex.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: File / Misc
        // Short Description: This procedure removes a directory recursively. 
        public static void remove_dir_recursively(HTuple hv_DirName)
        {



            // Local control variables 

            HTuple hv_Dirs = new HTuple(), hv_I = new HTuple();
            HTuple hv_Files = new HTuple();
            // Initialize local and output iconic variables 
            try
            {
                //Recursively delete all subdirectories.
                hv_Dirs.Dispose();
                HOperatorSet.ListFiles(hv_DirName, "directories", out hv_Dirs);
                for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Dirs.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        remove_dir_recursively(hv_Dirs.TupleSelect(hv_I));
                    }
                }
                //Delete all files.
                hv_Files.Dispose();
                HOperatorSet.ListFiles(hv_DirName, "files", out hv_Files);
                for (hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_Files.TupleLength())) - 1); hv_I = (int)hv_I + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.DeleteFile(hv_Files.TupleSelect(hv_I));
                    }
                }
                //Remove empty directory.
                HOperatorSet.RemoveDir(hv_DirName);

                hv_Dirs.Dispose();
                hv_I.Dispose();
                hv_Files.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Dirs.Dispose();
                hv_I.Dispose();
                hv_Files.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Deep Learning / Classification
        // Short Description: Select a percentage of the given data. 
        public static void select_percentage_dl_classifier_data(HTuple hv_ImageFiles, HTuple hv_GroundTruthLabels,
            HTuple hv_SelectPercentage, out HTuple hv_ImageFilesOut, out HTuple hv_LabelsOut)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_UniqueClasses = new HTuple(), hv_Ratio = new HTuple();
            HTuple hv_ClassIndex = new HTuple(), hv_Label = new HTuple();
            HTuple hv_LabelIndices = new HTuple(), hv_ImageFilesLabel = new HTuple();
            HTuple hv_IndexEnd = new HTuple();
            // Initialize local and output iconic variables 
            hv_ImageFilesOut = new HTuple();
            hv_LabelsOut = new HTuple();
            try
            {
                //This procedure selects SelectPercentage percentages
                //of the input data set ImageFiles and GroundTruthLabels and returns
                //the result in ImageFilesOut and LabelsOut.
                //The original ratio of class sizes is kept
                //when applying this percentage.
                //
                //Check the input parameters.
                if ((int)(new HTuple((new HTuple(hv_ImageFiles.TupleLength())).TupleLess(1))) != 0)
                {
                    throw new HalconException("ImageFiles must not be empty.");
                }
                if ((int)(new HTuple((new HTuple(hv_ImageFiles.TupleLength())).TupleNotEqual(
                    new HTuple(hv_GroundTruthLabels.TupleLength())))) != 0)
                {
                    throw new HalconException("Please provide a label for every image.");
                }
                if ((int)((new HTuple(hv_SelectPercentage.TupleLess(0))).TupleOr(new HTuple(hv_SelectPercentage.TupleGreater(
                    100)))) != 0)
                {
                    throw new HalconException("UsedPercentage must be between 0 and 100.");
                }
                hv_UniqueClasses.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_UniqueClasses = ((hv_GroundTruthLabels.TupleSort()
                        )).TupleUniq();
                }
                //
                //Select the user-defined percentage of every class.
                if ((int)(new HTuple(hv_SelectPercentage.TupleEqual(100))) != 0)
                {
                    hv_ImageFilesOut.Dispose();
                    hv_ImageFilesOut = new HTuple(hv_ImageFiles);
                    hv_LabelsOut.Dispose();
                    hv_LabelsOut = new HTuple(hv_GroundTruthLabels);
                }
                else
                {
                    hv_Ratio.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Ratio = hv_SelectPercentage * 0.01;
                    }
                    hv_ImageFilesOut.Dispose();
                    hv_ImageFilesOut = new HTuple();
                    hv_LabelsOut.Dispose();
                    hv_LabelsOut = new HTuple();
                    for (hv_ClassIndex = 0; (int)hv_ClassIndex <= (int)((new HTuple(hv_UniqueClasses.TupleLength()
                        )) - 1); hv_ClassIndex = (int)hv_ClassIndex + 1)
                    {
                        //For each class, find the images with this label.
                        hv_Label.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_Label = hv_UniqueClasses.TupleSelect(
                                hv_ClassIndex);
                        }
                        hv_LabelIndices.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_LabelIndices = hv_GroundTruthLabels.TupleFind(
                                hv_Label);
                        }
                        hv_ImageFilesLabel.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_ImageFilesLabel = hv_ImageFiles.TupleSelect(
                                hv_LabelIndices);
                        }
                        //Shuffle the images with this label.
                        {
                            HTuple ExpTmpOutVar_0;
                            tuple_shuffle(hv_ImageFilesLabel, out ExpTmpOutVar_0);
                            hv_ImageFilesLabel.Dispose();
                            hv_ImageFilesLabel = ExpTmpOutVar_0;
                        }
                        //Select images from the class according to the given percentage.
                        hv_IndexEnd.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_IndexEnd = (new HTuple(0)).TupleMax2(
                                ((((((new HTuple(hv_ImageFilesLabel.TupleLength())) * hv_Ratio)).TupleFloor()
                                )).TupleInt()) - 1);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_ImageFilesOut = hv_ImageFilesOut.TupleConcat(
                                    hv_ImageFilesLabel.TupleSelectRange(0, hv_IndexEnd));
                                hv_ImageFilesOut.Dispose();
                                hv_ImageFilesOut = ExpTmpLocalVar_ImageFilesOut;
                            }
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_LabelsOut = hv_LabelsOut.TupleConcat(
                                    HTuple.TupleGenConst(hv_IndexEnd + 1, hv_Label));
                                hv_LabelsOut.Dispose();
                                hv_LabelsOut = ExpTmpLocalVar_LabelsOut;
                            }
                        }
                    }
                }

                hv_UniqueClasses.Dispose();
                hv_Ratio.Dispose();
                hv_ClassIndex.Dispose();
                hv_Label.Dispose();
                hv_LabelIndices.Dispose();
                hv_ImageFilesLabel.Dispose();
                hv_IndexEnd.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_UniqueClasses.Dispose();
                hv_Ratio.Dispose();
                hv_ClassIndex.Dispose();
                hv_Label.Dispose();
                hv_LabelIndices.Dispose();
                hv_ImageFilesLabel.Dispose();
                hv_IndexEnd.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Graphics / Text
        // Short Description: Set font independent of OS 
        public static void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font,
            HTuple hv_Bold, HTuple hv_Slant)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_OS = new HTuple(), hv_Fonts = new HTuple();
            HTuple hv_Style = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_AvailableFonts = new HTuple(), hv_Fdx = new HTuple();
            HTuple hv_Indices = new HTuple();
            HTuple hv_Font_COPY_INP_TMP = new HTuple(hv_Font);
            HTuple hv_Size_COPY_INP_TMP = new HTuple(hv_Size);

            // Initialize local and output iconic variables 
            try
            {
                //This procedure sets the text font of the current window with
                //the specified attributes.
                //
                //Input parameters:
                //WindowHandle: The graphics window for which the font will be set
                //Size: The font size. If Size=-1, the default of 16 is used.
                //Bold: If set to 'true', a bold font is used
                //Slant: If set to 'true', a slanted font is used
                //
                hv_OS.Dispose();
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
                {
                    hv_Size_COPY_INP_TMP.Dispose();
                    hv_Size_COPY_INP_TMP = 16;
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    //Restore previous behaviour
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = ((1.13677 * hv_Size_COPY_INP_TMP)).TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = hv_Size_COPY_INP_TMP.TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Courier";
                    hv_Fonts[1] = "Courier 10 Pitch";
                    hv_Fonts[2] = "Courier New";
                    hv_Fonts[3] = "CourierNew";
                    hv_Fonts[4] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Consolas";
                    hv_Fonts[1] = "Menlo";
                    hv_Fonts[2] = "Courier";
                    hv_Fonts[3] = "Courier 10 Pitch";
                    hv_Fonts[4] = "FreeMono";
                    hv_Fonts[5] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Luxi Sans";
                    hv_Fonts[1] = "DejaVu Sans";
                    hv_Fonts[2] = "FreeSans";
                    hv_Fonts[3] = "Arial";
                    hv_Fonts[4] = "Liberation Sans";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Times New Roman";
                    hv_Fonts[1] = "Luxi Serif";
                    hv_Fonts[2] = "DejaVu Serif";
                    hv_Fonts[3] = "FreeSerif";
                    hv_Fonts[4] = "Utopia";
                    hv_Fonts[5] = "Liberation Serif";
                }
                else
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple(hv_Font_COPY_INP_TMP);
                }
                hv_Style.Dispose();
                hv_Style = "";
                if ((int)(new HTuple(hv_Bold.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Bold";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Bold.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Slant.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Italic";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Slant.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Style.TupleEqual(""))) != 0)
                {
                    hv_Style.Dispose();
                    hv_Style = "Normal";
                }
                hv_AvailableFonts.Dispose();
                HOperatorSet.QueryFont(hv_WindowHandle, out hv_AvailableFonts);
                hv_Font_COPY_INP_TMP.Dispose();
                hv_Font_COPY_INP_TMP = "";
                for (hv_Fdx = 0; (int)hv_Fdx <= (int)((new HTuple(hv_Fonts.TupleLength())) - 1); hv_Fdx = (int)hv_Fdx + 1)
                {
                    hv_Indices.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Indices = hv_AvailableFonts.TupleFind(
                            hv_Fonts.TupleSelect(hv_Fdx));
                    }
                    if ((int)(new HTuple((new HTuple(hv_Indices.TupleLength())).TupleGreater(
                        0))) != 0)
                    {
                        if ((int)(new HTuple(((hv_Indices.TupleSelect(0))).TupleGreaterEqual(0))) != 0)
                        {
                            hv_Font_COPY_INP_TMP.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(
                                    hv_Fdx);
                            }
                            break;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(""))) != 0)
                {
                    throw new HalconException("Wrong value of control parameter Font");
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Font = (((hv_Font_COPY_INP_TMP + "-") + hv_Style) + "-") + hv_Size_COPY_INP_TMP;
                        hv_Font_COPY_INP_TMP.Dispose();
                        hv_Font_COPY_INP_TMP = ExpTmpLocalVar_Font;
                    }
                }
                HOperatorSet.SetFont(hv_WindowHandle, hv_Font_COPY_INP_TMP);

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Deep Learning / Classification
        // Short Description: Split and shuffle the images and ground truth labels into training, validation and test subsets. 
        public static void split_dl_classifier_data_set(HTuple hv_ImageFiles, HTuple hv_GroundTruthLabels,
            HTuple hv_TrainingPercent, HTuple hv_ValidationPercent, out HTuple hv_TrainingImages,
            out HTuple hv_TrainingLabels, out HTuple hv_ValidationImages, out HTuple hv_ValidationLabels,
            out HTuple hv_TestImages, out HTuple hv_TestLabels)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_TrainingRatio = new HTuple(), hv_ValidationRatio = new HTuple();
            HTuple hv_UniqueClasses = new HTuple(), hv_ClassIndex = new HTuple();
            HTuple hv_Class = new HTuple(), hv_ClassIndices = new HTuple();
            HTuple hv_ImageFilesClass = new HTuple(), hv_LabelsClass = new HTuple();
            HTuple hv_IndexTrainingEnd = new HTuple(), hv_IndexValidationEnd = new HTuple();
            HTuple hv_TrainingSequence = new HTuple(), hv_ValidationSequence = new HTuple();
            HTuple hv_TestSequence = new HTuple();
            // Initialize local and output iconic variables 
            hv_TrainingImages = new HTuple();
            hv_TrainingLabels = new HTuple();
            hv_ValidationImages = new HTuple();
            hv_ValidationLabels = new HTuple();
            hv_TestImages = new HTuple();
            hv_TestLabels = new HTuple();
            try
            {
                //This procedure divides the data set (images and ground truth labels)
                //into three disjoint subsets: training, validation, and test.
                //The number of images and labels in each subset is defined
                //by the given percentages TrainingPercent and ValidationPercent.
                //Each subset contains randomly distributed data,
                //whereby the original ratio of class sizes is kept.
                //
                //Check the input parameters.
                if ((int)(new HTuple((new HTuple(hv_ImageFiles.TupleLength())).TupleNotEqual(
                    new HTuple(hv_GroundTruthLabels.TupleLength())))) != 0)
                {
                    throw new HalconException("Please provide a label for every image file.");
                }
                if ((int)(new HTuple(hv_TrainingPercent.TupleLess(0))) != 0)
                {
                    throw new HalconException("TrainingPercent must not be smaller than zero.");
                }
                if ((int)(new HTuple(hv_ValidationPercent.TupleLess(0))) != 0)
                {
                    throw new HalconException("ValidationPercent must not be smaller than zero.");
                }
                if ((int)(new HTuple((new HTuple(hv_ImageFiles.TupleLength())).TupleLess(1))) != 0)
                {
                    throw new HalconException("ImageFiles must not be empty.");
                }
                if ((int)(new HTuple(((hv_TrainingPercent + hv_ValidationPercent)).TupleGreater(
                    100))) != 0)
                {
                    throw new HalconException("The sum of TrainingPercent and ValidationPercent must not be greater than 100.");
                }
                //
                //Set classes and data ratios.
                hv_TrainingRatio.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TrainingRatio = hv_TrainingPercent * 0.01;
                }
                hv_ValidationRatio.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ValidationRatio = hv_ValidationPercent * 0.01;
                }
                //
                //Prepare output tuples.
                hv_TrainingImages.Dispose();
                hv_TrainingImages = new HTuple();
                hv_TrainingLabels.Dispose();
                hv_TrainingLabels = new HTuple();
                hv_ValidationImages.Dispose();
                hv_ValidationImages = new HTuple();
                hv_ValidationLabels.Dispose();
                hv_ValidationLabels = new HTuple();
                hv_TestImages.Dispose();
                hv_TestImages = new HTuple();
                hv_TestLabels.Dispose();
                hv_TestLabels = new HTuple();
                //
                //Loop through all unique classes and add data
                //according to the specified percentages.
                hv_UniqueClasses.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_UniqueClasses = ((hv_GroundTruthLabels.TupleSort()
                        )).TupleUniq();
                }
                for (hv_ClassIndex = 0; (int)hv_ClassIndex <= (int)((new HTuple(hv_UniqueClasses.TupleLength()
                    )) - 1); hv_ClassIndex = (int)hv_ClassIndex + 1)
                {
                    //Select all images and ground truth labels with the class.
                    hv_Class.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Class = hv_UniqueClasses.TupleSelect(
                            hv_ClassIndex);
                    }
                    hv_ClassIndices.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ClassIndices = hv_GroundTruthLabels.TupleFind(
                            hv_Class);
                    }
                    hv_ImageFilesClass.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ImageFilesClass = hv_ImageFiles.TupleSelect(
                            hv_ClassIndices);
                    }
                    hv_LabelsClass.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_LabelsClass = HTuple.TupleGenConst(
                            new HTuple(hv_ImageFilesClass.TupleLength()), hv_Class);
                    }
                    //Shuffle the images in this class.
                    {
                        HTuple ExpTmpOutVar_0;
                        tuple_shuffle(hv_ImageFilesClass, out ExpTmpOutVar_0);
                        hv_ImageFilesClass.Dispose();
                        hv_ImageFilesClass = ExpTmpOutVar_0;
                    }
                    //Determine the boundaries of the respective selection.
                    hv_IndexTrainingEnd.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_IndexTrainingEnd = ((((((new HTuple(hv_ImageFilesClass.TupleLength()
                            )) * hv_TrainingRatio)).TupleFloor())).TupleInt()) - 1;
                    }
                    hv_IndexValidationEnd.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_IndexValidationEnd = ((((((new HTuple(hv_ImageFilesClass.TupleLength()
                            )) * (hv_ValidationRatio + hv_TrainingRatio))).TupleFloor())).TupleInt()) - 1;
                    }
                    //Add the respective images and labels.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_TrainingImages = hv_TrainingImages.TupleConcat(
                                hv_ImageFilesClass.TupleSelectRange(0, hv_IndexTrainingEnd));
                            hv_TrainingImages.Dispose();
                            hv_TrainingImages = ExpTmpLocalVar_TrainingImages;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_TrainingLabels = hv_TrainingLabels.TupleConcat(
                                hv_LabelsClass.TupleSelectRange(0, hv_IndexTrainingEnd));
                            hv_TrainingLabels.Dispose();
                            hv_TrainingLabels = ExpTmpLocalVar_TrainingLabels;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ValidationImages = hv_ValidationImages.TupleConcat(
                                hv_ImageFilesClass.TupleSelectRange(hv_IndexTrainingEnd + 1, hv_IndexValidationEnd));
                            hv_ValidationImages.Dispose();
                            hv_ValidationImages = ExpTmpLocalVar_ValidationImages;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_ValidationLabels = hv_ValidationLabels.TupleConcat(
                                hv_LabelsClass.TupleSelectRange(hv_IndexTrainingEnd + 1, hv_IndexValidationEnd));
                            hv_ValidationLabels.Dispose();
                            hv_ValidationLabels = ExpTmpLocalVar_ValidationLabels;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_TestImages = hv_TestImages.TupleConcat(
                                hv_ImageFilesClass.TupleSelectRange(hv_IndexValidationEnd + 1, (new HTuple(hv_ImageFilesClass.TupleLength()
                                )) - 1));
                            hv_TestImages.Dispose();
                            hv_TestImages = ExpTmpLocalVar_TestImages;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_TestLabels = hv_TestLabels.TupleConcat(
                                hv_LabelsClass.TupleSelectRange(hv_IndexValidationEnd + 1, (new HTuple(hv_ImageFilesClass.TupleLength()
                                )) - 1));
                            hv_TestLabels.Dispose();
                            hv_TestLabels = ExpTmpLocalVar_TestLabels;
                        }
                    }
                }
                //
                //Shuffle the output.
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TrainingSequence.Dispose();
                    tuple_shuffle(HTuple.TupleGenSequence(0, (new HTuple(hv_TrainingImages.TupleLength()
                        )) - 1, 1), out hv_TrainingSequence);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_TrainingImages = hv_TrainingImages.TupleSelect(
                            hv_TrainingSequence);
                        hv_TrainingImages.Dispose();
                        hv_TrainingImages = ExpTmpLocalVar_TrainingImages;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_TrainingLabels = hv_TrainingLabels.TupleSelect(
                            hv_TrainingSequence);
                        hv_TrainingLabels.Dispose();
                        hv_TrainingLabels = ExpTmpLocalVar_TrainingLabels;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ValidationSequence.Dispose();
                    tuple_shuffle(HTuple.TupleGenSequence(0, (new HTuple(hv_ValidationImages.TupleLength()
                        )) - 1, 1), out hv_ValidationSequence);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ValidationImages = hv_ValidationImages.TupleSelect(
                            hv_ValidationSequence);
                        hv_ValidationImages.Dispose();
                        hv_ValidationImages = ExpTmpLocalVar_ValidationImages;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_ValidationLabels = hv_ValidationLabels.TupleSelect(
                            hv_ValidationSequence);
                        hv_ValidationLabels.Dispose();
                        hv_ValidationLabels = ExpTmpLocalVar_ValidationLabels;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TestSequence.Dispose();
                    tuple_shuffle(HTuple.TupleGenSequence(0, (new HTuple(hv_TestImages.TupleLength()
                        )) - 1, 1), out hv_TestSequence);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_TestImages = hv_TestImages.TupleSelect(
                            hv_TestSequence);
                        hv_TestImages.Dispose();
                        hv_TestImages = ExpTmpLocalVar_TestImages;
                    }
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_TestLabels = hv_TestLabels.TupleSelect(
                            hv_TestSequence);
                        hv_TestLabels.Dispose();
                        hv_TestLabels = ExpTmpLocalVar_TestLabels;
                    }
                }

                hv_TrainingRatio.Dispose();
                hv_ValidationRatio.Dispose();
                hv_UniqueClasses.Dispose();
                hv_ClassIndex.Dispose();
                hv_Class.Dispose();
                hv_ClassIndices.Dispose();
                hv_ImageFilesClass.Dispose();
                hv_LabelsClass.Dispose();
                hv_IndexTrainingEnd.Dispose();
                hv_IndexValidationEnd.Dispose();
                hv_TrainingSequence.Dispose();
                hv_ValidationSequence.Dispose();
                hv_TestSequence.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_TrainingRatio.Dispose();
                hv_ValidationRatio.Dispose();
                hv_UniqueClasses.Dispose();
                hv_ClassIndex.Dispose();
                hv_Class.Dispose();
                hv_ClassIndices.Dispose();
                hv_ImageFilesClass.Dispose();
                hv_LabelsClass.Dispose();
                hv_IndexTrainingEnd.Dispose();
                hv_IndexValidationEnd.Dispose();
                hv_TrainingSequence.Dispose();
                hv_ValidationSequence.Dispose();
                hv_TestSequence.Dispose();

                throw HDevExpDefaultException;
            }
        }

        // Chapter: Tuple / Element Order
        // Short Description: Sort the elements of a tuple randomly. 
        public static void tuple_shuffle(HTuple hv_Tuple, out HTuple hv_Shuffled)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_ShuffleIndices = new HTuple();
            // Initialize local and output iconic variables 
            hv_Shuffled = new HTuple();
            try
            {
                //This procedure sorts the input tuple randomly.
                //
                if ((int)(new HTuple((new HTuple(hv_Tuple.TupleLength())).TupleGreater(0))) != 0)
                {
                    //Create a tuple of random numbers,
                    //sort this tuple, and return the indices
                    //of this sorted tuple.
                    hv_ShuffleIndices.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_ShuffleIndices = (HTuple.TupleRand(
                            new HTuple(hv_Tuple.TupleLength()))).TupleSortIndex();
                    }
                    //Assign the elements of Tuple
                    //to these random positions.
                    hv_Shuffled.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Shuffled = hv_Tuple.TupleSelect(
                            hv_ShuffleIndices);
                    }
                }
                else
                {
                    //If the input tuple is empty,
                    //an empty tuple should be returned.
                    hv_Shuffled.Dispose();
                    hv_Shuffled = new HTuple();
                }

                hv_ShuffleIndices.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_ShuffleIndices.Dispose();

                throw HDevExpDefaultException;
            }
        }

        #endregion

    }

}
