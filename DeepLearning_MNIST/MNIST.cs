using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.Windows.Forms;
using System.IO;

namespace DeepLearning_MNIST
{
    class MNIST : HalconTools
    {

        #region

        public Preprocess mnist_Preprocess = new Preprocess();
        public Train mnist_Train = new Train();
        public Test mnist_Test = new Test();

        //窗口句柄
        public HWindowControl hv_HWindowControl = new HWindowControl();

        //分类模型
        // HALCON provides pretrained neural networks. These neural networks are good starting points to train a custom classifier for image classification. They have been pretrained on a large image dataset. The provided pretrained neural networks are:
        // "pretrained_dl_classifier_compact.hdl"
        // This neural network is designed to be memory and runtime efficient.This network does not contain any fully connected layer. The network architecture allows changes concerning the image dimensions, but requires a minimum 'image_width' and 'image_height' of 15 pixels.
        // "pretrained_dl_classifier_enhanced.hdl"
        // This neural network has more hidden layers than 'pretrained_dl_classifier_compact.hdl' and is therefore assumed to be better suited for more complex classification tasks. But this comes at the cost of being more time and memory demanding.
        // "pretrained_dl_classifier_resnet50.hdl"
        // As the neural network 'pretrained_dl_classifier_enhanced.hdl', this classifier is suited for more complex tasks. But its structure differs, bringing the advantage of making the training more stable and being internally more robust.
        public string hv_Pretrained_DlClassifierName = "pretrained_dl_classifier_compact.hdl";
        #endregion

        public MNIST()
        { }

        public void PreprocessImages()
        {
            mnist_Preprocess.PreprocessDlClassifierImagesForTrain(this.hv_HWindowControl, hv_Pretrained_DlClassifierName);

        }

        public void Trainclassifier()
        {
            //分类器默认保存名称为"classifier+图像主文件夹名称.hdl"
            mnist_Train.hv_Trained_DlClassifierName = "classifier_" + Path.GetFileNameWithoutExtension(mnist_Preprocess.path_Images) + ".hdl";

            //训练分类器
            mnist_Train.TrainProcess(this.hv_HWindowControl.HalconWindow, hv_Pretrained_DlClassifierName);

        }

        public void Testclassifier()
        {
            GetDlClassifierName();
            
            mnist_Preprocess.PreprocessImage(this.hv_Pretrained_DlClassifierName,mnist_Test.ho_TestImage,out mnist_Test.ho_Preprocessed_TestImage);
            mnist_Test.TestImage(ref hv_HWindowControl);
        }

        public string InformationText()
        {
            string strReadFilePath = "Readme.txt";
            StreamReader srReadFile = new StreamReader(strReadFilePath, Encoding.Default);
            StringBuilder Introduction = new StringBuilder();
            // 读取流直至文件末尾结束
            while (!srReadFile.EndOfStream)
            {
                Introduction.Append(srReadFile.ReadLine() + "\r\n");
            }
            // 关闭读取流文件
            srReadFile.Close();

            return Introduction.ToString();
        }

        public void GetDlClassifierName()
        {
            if (mnist_Test.hv_Test_DlClassifierName == null)
            {
                if (mnist_Train.hv_Trained_DlClassifierName == null)
                {
                    MessageBox.Show("无可用分类器，请先训练或读取", "错误提示");
                    return;
                }
                mnist_Test.hv_Test_DlClassifierName = mnist_Train.hv_Trained_DlClassifierName;
            }
        }

    }
}
