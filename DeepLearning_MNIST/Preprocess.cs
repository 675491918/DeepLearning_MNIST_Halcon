using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.IO;
using System.Windows.Forms;

namespace DeepLearning_MNIST
{
    public class Preprocess : HalconTools
    {
        public string path_Images = null;
        public string path_Train_Images;
        public HTuple path_Preprocessed_Images = new HTuple();
        public HTuple hv_Pretrained_DlClassifierName = new HTuple();
        

        private System.Threading.Thread _PreprocessThread = null;

        HTuple hv_ImageFiles = new HTuple();
        HTuple hv_UniqueClasses = new HTuple();
        HTuple hv_GroundTruthLabels = new HTuple();
        HTuple hv_LabelsIndices = new HTuple();
        HTuple hv_ObjectFilesOut = new HTuple();
        HTuple hv_WindowHandle = new HTuple();
        HWindowControl hv_HWindowControl = new HWindowControl();


        public Preprocess()
        { }


        public void PreprocessDlClassifierImagesForTrain(HWindowControl hv_HWindowControl,HTuple hv_Pretrained_DlClassifierName)
        {
            this.hv_HWindowControl = hv_HWindowControl;
            this.hv_WindowHandle = this.hv_HWindowControl.HalconWindow;
            this.hv_Pretrained_DlClassifierName = hv_Pretrained_DlClassifierName;


            hv_ImageFiles.Dispose(); hv_GroundTruthLabels.Dispose(); hv_LabelsIndices.Dispose(); hv_UniqueClasses.Dispose();

            //读取训练数据集，图像Lable为图像所在目录名
            read_dl_classifier_data_set(path_Train_Images, "last_folder", out hv_ImageFiles, out hv_GroundTruthLabels, out hv_LabelsIndices, out hv_UniqueClasses);

            //Create the directories for writing the preprocessed images.
            CreatePreprocessedImagesFolder();

            //Prepare the new image names.
            HTuple hv_BaseNames = new HTuple(); HTuple hv_Extensions = new HTuple(); HTuple hv_Directories = new HTuple();

            hv_BaseNames.Dispose(); hv_Extensions.Dispose(); hv_Directories.Dispose();
            parse_filename(hv_ImageFiles, out hv_BaseNames, out hv_Extensions, out hv_Directories);

            hv_ObjectFilesOut.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_ObjectFilesOut = (((path_Preprocessed_Images + "/") + hv_GroundTruthLabels) + "/") + hv_BaseNames;
            }
            //
            System.Threading.ThreadStart ts = new System.Threading.ThreadStart(ImagePreprocessThreading);
            this._PreprocessThread = new System.Threading.Thread(ts);
            this._PreprocessThread.IsBackground = true;//设置线程为后台线程
            this._PreprocessThread.Start();

        }

        /// <summary>
        /// 创建预处理图像保存目录
        /// </summary>
        public void CreatePreprocessedImagesFolder()
        {

            HOperatorSet.MakeDir(this.path_Preprocessed_Images); 

            HTuple hv_path = new HTuple();
            for (HTuple hv_I = 0; (int)hv_I <= (int)((new HTuple(hv_UniqueClasses.TupleLength())) - 1); hv_I = (int)hv_I + 1)
            {
                hv_path.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_path = (path_Preprocessed_Images + "/") + (hv_UniqueClasses.TupleSelect(hv_I));
                }
                HOperatorSet.MakeDir(hv_path);
            }
        }

        /// <summary>
        /// 批量处理并显示图像线程
        /// </summary>
        private void ImagePreprocessThreading()
        {
            //Loop through all images,
            //preprocess and then write them.
            HObject ho_Image = new HObject(); HObject ho_ImagePreprocessed = new HObject();
            for (HTuple hv_ImageIndex = 0; (int)hv_ImageIndex <= (hv_ImageFiles.TupleLength() - 1); hv_ImageIndex = (int)hv_ImageIndex + 1)
            {
                //显示原图像
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Image.Dispose();
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_ImageIndex));
                    DispImageAdaptively(ref hv_HWindowControl, ho_Image);
                    HOperatorSet.DispText(hv_HWindowControl.HalconWindow, "ImagesPreprocessing:"+hv_ImageIndex, "window", "top", "left", "red", "box", "true");
                    Console.WriteLine(hv_ImageIndex.ToString());
                }
                ho_ImagePreprocessed.Dispose();

                //预处理图像
                PreprocessImage(this.hv_Pretrained_DlClassifierName,ho_Image, out ho_ImagePreprocessed);

                //保存预处理后图像
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.WriteObject(ho_ImagePreprocessed, hv_ObjectFilesOut.TupleSelect(hv_ImageIndex));
                }
            }
            HOperatorSet.DispText(this.hv_WindowHandle, "Preprocessing over,Click to continue...", "window", "bottom", "right", "red", "box", "false");

        }

        /// <summary>
        /// 预处理图像用于训练或测试
        /// </summary>
        public void PreprocessImage(string hv_Pretrained_DlClassifierName, HObject ho_Image, out HObject ho_ImagePreprocessed)
        {
            HTuple hv_DLClassifierHandle = new HTuple();
            HOperatorSet.ReadDlClassifier(hv_Pretrained_DlClassifierName, out hv_DLClassifierHandle);
            preprocess_dl_classifier_images(ho_Image, out ho_ImagePreprocessed, new HTuple(), new HTuple(), hv_DLClassifierHandle);

        }

        public string GetImagesPaths()
        {
            StringBuilder sb = new StringBuilder();
            if (this.path_Images != null)
            {
                sb.Append("图像主目录：\r\n").Append(this.path_Images);
                sb.Append("\r\n训练图像文件目录：\r\n").Append(this.path_Images + @"\Train_images");
                sb.Append("\r\n测试图像文件目录：\r\n").Append(this.path_Images + @"\Test_images");
                sb.Append("\r\n预处理图像文件目录：\r\n").Append(this.path_Images + @"\Preprocessed_images");
            }
            else
            {
                sb.Append("未选择图像目录，需要点击左下角按钮选择\r\n");
            }
            return sb.ToString();
        }


    }
}
