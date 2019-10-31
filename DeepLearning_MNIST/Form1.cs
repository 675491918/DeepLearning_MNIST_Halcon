using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using System.IO;

namespace DeepLearning_MNIST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 变量声明
        //创建对象
        MNIST mnist = new MNIST();

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            this.mnist.hv_HWindowControl = this.hWindowControl1;
            groupBox2.Enabled = false;
            groupBox1.Enabled = false;
            HOperatorSet.DispText(this.hWindowControl1.HalconWindow, "请先选择图像目录", "window", "center", "center", "white", "box", "false");
            textBoxInformation.Text = "请先选择图像目录";
            btnChooseFolder.Focus();
        }

        private void btnPreprocessImages_Click(object sender, EventArgs e)
        {
            if (this.mnist.mnist_Preprocess.path_Images == null)
            {
                MessageBox.Show("请先选择图像目录");
                return;
            }

            this.mnist.mnist_Preprocess.path_Train_Images = this.mnist.mnist_Preprocess.path_Images + @"\Train_images";
            if (!IsPathAvailable(this.mnist.mnist_Preprocess.path_Train_Images))
            {
                return;
            }

            this.mnist.mnist_Preprocess.path_Preprocessed_Images = mnist.mnist_Preprocess.path_Images + @"\Preprocessed_images";
            if (Directory.Exists(this.mnist.mnist_Preprocess.path_Preprocessed_Images))
            {
                if (MessageBox.Show("正在新建预处理图像保存目录，但该目录已存在，是否删除并继续?", "消息确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    //删除已有文件夹及其子文件夹和文件
                    HalconTools.remove_dir_recursively(this.mnist.mnist_Preprocess.path_Preprocessed_Images);
                }
                else { return; }
            }

            mnist.PreprocessImages();
        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "请选择文件路径";
            fbd.SelectedPath = Application.StartupPath;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                this.mnist.mnist_Preprocess.path_Images = fbd.SelectedPath;
                if (fbd.SelectedPath.Contains("Train") || fbd.SelectedPath.Contains("Test") || fbd.SelectedPath.Contains("Preprocessed"))
                {
                    this.mnist.mnist_Preprocess.path_Images = Path.GetDirectoryName(fbd.SelectedPath);
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("已选择目录：\r\n");
                sb.Append(mnist.mnist_Preprocess.GetImagesPaths());
                this.textBoxInformation.Text = sb.ToString();
                HOperatorSet.ClearWindow(this.hWindowControl1.HalconWindow);
                groupBox2.Enabled = true;
                groupBox1.Enabled = true;
            }
        }

        private void btnClassifierParamsSet_Click(object sender, EventArgs e)
        {
            ClassifierParamsSetForm cpsf = new ClassifierParamsSetForm();

            cpsf._BatchSize = this.mnist.mnist_Train.hv_BatchSize;
            cpsf._NumEpochs = this.mnist.mnist_Train.hv_NumEpochs;
            cpsf._LearningRateStepEveryNthEpoch = this.mnist.mnist_Train.hv_LearningRateStepEveryNthEpoch;
            cpsf._LearningRateStepRatio = this.mnist.mnist_Train.hv_LearningRateStepRatio;
            cpsf._InitialLearningRate = this.mnist.mnist_Train.hv_InitialLearningRate;
            cpsf._TrainingPercent = this.mnist.mnist_Train.hv_TrainingPercent;
            cpsf._ValidationPercent = this.mnist.mnist_Train.hv_ValidationPercent;

            if (cpsf.ShowDialog() == DialogResult.OK)
            {
                this.mnist.mnist_Train.hv_BatchSize = cpsf._BatchSize;
                this.mnist.mnist_Train.hv_NumEpochs = cpsf._NumEpochs;
                this.mnist.mnist_Train.hv_LearningRateStepEveryNthEpoch = cpsf._LearningRateStepEveryNthEpoch;
                this.mnist.mnist_Train.hv_LearningRateStepRatio = cpsf._LearningRateStepRatio;
                this.mnist.mnist_Train.hv_InitialLearningRate = cpsf._InitialLearningRate;
                this.mnist.mnist_Train.hv_TrainingPercent = cpsf._TrainingPercent;
                this.mnist.mnist_Train.hv_ValidationPercent = cpsf._ValidationPercent;
            }
            StringBuilder sb = new StringBuilder();

            sb.Append("当前训练参数设置：\r\n");
            sb.Append(mnist.mnist_Train.TrainClassifierParams());
            this.textBoxInformation.Text = sb.ToString();
        }

        private void btnTrainClassifier_Click(object sender, EventArgs e)
        {
            //获取预处理后图像的文件夹名称
            mnist.mnist_Train.hv_PreprocessedFolder = mnist.mnist_Preprocess.path_Images + @"\Preprocessed_images";
            if (!IsPathAvailable(mnist.mnist_Train.hv_PreprocessedFolder))
            {
                return;
            }


            StringBuilder sb = new StringBuilder();

            sb.Append("训练进行中...\r\n\r\n").Append("当前训练参数设置：\r\n");
            sb.Append(mnist.mnist_Train.TrainClassifierParams());
            this.textBoxInformation.Text = sb.ToString();
            
            mnist.Trainclassifier();
        }

        private void btnSaveClassifier_Click(object sender, EventArgs e)
        {
            if (this.mnist.mnist_Train.hv_Train_DLClassifierHandle == null)
            {
                MessageBox.Show("无可保存的分类器", "错误信息");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath;
            sfd.FileName = Path.GetFileName(mnist.mnist_Train.hv_Trained_DlClassifierName);
            sfd.Filter = "(分类器文件*.hdl)|*.hdl";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (!sfd.FileName.Contains(".hdl"))
                {
                    sfd.FileName = sfd.FileName + ".hdl";
                }
                HOperatorSet.WriteDlClassifier(this.mnist.mnist_Train.hv_Train_DLClassifierHandle, sfd.FileName);
                this.textBoxInformation.Text = "分类器已保存：\r\n" + sfd.FileName;

            }
        }

        private void btnReadClassifier_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Multiselect = false;
            ofd.Filter = "classifier文件(*.hdl)|*.hdl";
            ofd.Title = "选择测试分类器";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                mnist.mnist_Test.hv_Test_DlClassifierName = ofd.FileName;
                this.textBoxInformation.Text = "已选择分类器：\r\n" + mnist.mnist_Test.hv_Test_DlClassifierName.ToString();
            }
        }

        private void btnRandomTest_Click(object sender, EventArgs e)
        {
            mnist.mnist_Test.path_Test_Images = mnist.mnist_Preprocess.path_Images + @"\Test_images";
            if (mnist.mnist_Preprocess.path_Images.Contains("Test"))
            {
                mnist.mnist_Test.path_Test_Images = mnist.mnist_Preprocess.path_Images;
            }
            //判断图像文件夹是否存在或内容是否为空
            if (!IsPathAvailable(mnist.mnist_Test.path_Test_Images))
            {
                return;
            }

            mnist.mnist_Test.RandomGetImage();
            textBoxInformation.Text = "已选择测试图像：\r\n" + mnist.mnist_Test.ho_TestImageName;

            mnist.Testclassifier();
        }

        private void btnChooseTestImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = this.mnist.mnist_Preprocess.path_Images;
            ofd.Multiselect = false;
            ofd.Filter = "图像文件(*.bmp;*.png;*.jpg;*.jpeg;*.tif)|*.bmp;*.png;*.jpg;*.jpeg;*.tif";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                HOperatorSet.ReadImage(out this.mnist.mnist_Test.ho_TestImage, ofd.FileName);
                mnist.DispImageAdaptively(ref this.hWindowControl1, this.mnist.mnist_Test.ho_TestImage);
                textBoxInformation.Text = "已选择测试图像：\r\n" + ofd.FileName;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (mnist.mnist_Test.ho_TestImage == null)
            {
                MessageBox.Show("请选择新图像", "错误提示");
                return;
            }

            mnist.Testclassifier();
        }

        private void btnReadInformation_Click(object sender, EventArgs e)
        {
            this.textBoxInformation.Text = mnist.InformationText();
        }

        private void btnClearText_Click(object sender, EventArgs e)
        {
            this.textBoxInformation.Text = "";
        }

        private void btnComputeConfusionMatrix_Click(object sender, EventArgs e)
        {
            if (this.mnist.mnist_Train.hv_Train_DLClassifierHandle == null)
            {
                MessageBox.Show("请先训练分类器");
                return;
            }
            mnist.mnist_Train.ComputeConfusionMatrix(this.hWindowControl1.HalconWindow);

            StringBuilder sb = new StringBuilder();
            sb.Append("当前训练参数设置：\r\n");
            sb.Append(mnist.mnist_Train.TrainClassifierParams());
            this.textBoxInformation.Text = sb.ToString();
        }

        private void btnShowParams_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("当前训练参数设置：\r\n");
            sb.Append(mnist.mnist_Train.TrainClassifierParams());

            sb.Append("\r\n当前分类器：\r\n");
            if (mnist.mnist_Train.hv_Trained_DlClassifierName != null)
            {
                sb.Append(mnist.mnist_Train.hv_Trained_DlClassifierName.ToString() + "\r\n");
            }
            else if (mnist.mnist_Test.hv_Test_DlClassifierName != null)
            {
                sb.Append(mnist.mnist_Test.hv_Test_DlClassifierName.ToString() + "\r\n");
            }
            else
            {
                sb.Append("暂无可用分类器，需要训练或读取\r\n");
            }


            sb.Append("\r\n当前图像文件目录：\r\n");
            sb.Append(mnist.mnist_Preprocess.GetImagesPaths());

            this.textBoxInformation.Text = sb.ToString();
        }

        /// <summary>
        /// 判断路径是否存在或内容是否为空
        /// </summary>
        public bool IsPathAvailable(string Path)
        {
            if (!Directory.Exists(Path))
            {
                MessageBox.Show(Path+"路径不存在", "错误提示");
                return false;
            }
            if (Directory.GetDirectories(Path).Length < 1 && Directory.GetFiles(Path).Length < 1)
            {
                MessageBox.Show(Path+"路径内容为空", "错误提示");
                return false;
            }
            return true;
        }

    }
}
