using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace DeepLearning_MNIST
{
    public class Test : HalconTools
    {
        //测试图像文件路径
        public string path_Test_Images;
        //测试图像变量
        public HObject ho_TestImage = new HObject();
        //测试图像名称变量
        public string ho_TestImageName = null;
        //预处理图像变量
        public HObject ho_Preprocessed_TestImage = new HObject();
        //分类器
        public HTuple hv_Test_DlClassifierName = new HTuple();
        //用于测试的分类器句柄
        public HTuple hv_Test_DLClassifierHandle = new HTuple();
        //运行模式 CPU / GPU
        HTuple hv_Runtime = new HTuple();

        public Test()
        {
            hv_Test_DlClassifierName = null;
            ho_TestImage = null;
            ho_Preprocessed_TestImage = null;
        }


        public void TestImage(ref HWindowControl hv_HWindowControl)
        {
            if (hv_Test_DlClassifierName == null)
            {
                Console.WriteLine("错误提示：无可用分类器");
                return;
            }
            if (ho_Preprocessed_TestImage == null)
            {
                Console.WriteLine("图像未进行预处理");
                return;
            }

            HObject ho_ImagePreprocessed = new HObject();
            HTuple hv_DLClassifierResultHandle = new HTuple();
            HTuple hv_Inference_PredictedClass = new HTuple();

            HTuple hv_Exception = new HTuple();
            HTuple hv_Text = new HTuple();

            hv_Test_DLClassifierHandle.Dispose();
            HOperatorSet.ReadDlClassifier(hv_Test_DlClassifierName, out hv_Test_DLClassifierHandle);

            //If it is not possible to accumulate more than one image at a time the batch size should be set to 1.
            HOperatorSet.SetDlClassifierParam(hv_Test_DLClassifierHandle, "batch_size", 1);

            //Set the runtime to 'cpu' to perform the inference on the CPU, if this is possible on the current hardware.
            //(see the system requirements in the Installation Guide)
            try
            {
                HOperatorSet.SetDlClassifierParam(hv_Test_DLClassifierHandle, "runtime", "cpu");
                hv_Runtime.Dispose();
                hv_Runtime = "cpu";
            }
            // catch (Exception) 
            catch (HalconException HDevExpDefaultException1)
            {
                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                //Keep the 'gpu' runtime if switching to 'cpu' failed.
                hv_Runtime.Dispose();
                hv_Runtime = "gpu";
            }
            //This initializes the runtime environment immediately.
            HOperatorSet.SetDlClassifierParam(hv_Test_DLClassifierHandle, "runtime_init", "immediately");
            //
            //dev_disp_inference_text(hv_Runtime, hv_WindowHandle);
            // stop(...); only in hdevelop
            ho_ImagePreprocessed.Dispose();

            preprocess_dl_classifier_images(this.ho_TestImage, out ho_ImagePreprocessed, new HTuple(),
                new HTuple(), hv_Test_DLClassifierHandle);

            hv_DLClassifierResultHandle.Dispose();
            HOperatorSet.ApplyDlClassifier(ho_ImagePreprocessed, hv_Test_DLClassifierHandle,
                out hv_DLClassifierResultHandle);
            hv_Inference_PredictedClass.Dispose();
            HOperatorSet.GetDlClassifierResult(hv_DLClassifierResultHandle, "all", "predicted_classes",
                out hv_Inference_PredictedClass);
            //
            DispImageAdaptively(ref hv_HWindowControl, this.ho_TestImage);
            hv_Text.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_Text = "Predicted class: " + hv_Inference_PredictedClass;
            }
            HOperatorSet.DispText(hv_HWindowControl.HalconWindow, hv_Text, "window", "top", "left", "red", "box", "true");

            HOperatorSet.DispText(hv_HWindowControl.HalconWindow, "Click to continue", "window", "bottom", "right", "black", new HTuple(), new HTuple());
            this.ho_TestImage = null;
        }

        public void RandomGetImage()
        {
            HTuple hv_TestImageFiles = new HTuple();


            hv_TestImageFiles.Dispose();
            HOperatorSet.ListFiles(this.path_Test_Images, "files", out hv_TestImageFiles);

            //Read / acquire images in a loop and classify them.
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_TestImageName = hv_TestImageFiles.TupleSelect((new HTuple(HTuple.TupleRand(1) * (new HTuple(hv_TestImageFiles.TupleLength())))).TupleFloor());
            }
            HOperatorSet.ReadImage(out this.ho_TestImage, ho_TestImageName);
        }

        private void DispTestInformation()
        {
            HTuple hv_Text = new HTuple();
            // Initialize local and output iconic variables 

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
        }

    }
}
