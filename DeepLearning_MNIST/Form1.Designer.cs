namespace DeepLearning_MNIST
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.btnPreprocessImages = new System.Windows.Forms.Button();
            this.btnTrainClassifier = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.textBoxInformation = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRandomTest = new System.Windows.Forms.Button();
            this.btnChooseTestImage = new System.Windows.Forms.Button();
            this.btnReadClassifier = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnComputeConfusionMatrix = new System.Windows.Forms.Button();
            this.btnSaveClassifier = new System.Windows.Forms.Button();
            this.btnClassifierParamsSet = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnChooseFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReadInformation = new System.Windows.Forms.Button();
            this.btnClearText = new System.Windows.Forms.Button();
            this.btnShowParams = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(2, 3);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(640, 480);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(640, 480);
            // 
            // btnPreprocessImages
            // 
            this.btnPreprocessImages.Location = new System.Drawing.Point(135, 39);
            this.btnPreprocessImages.Name = "btnPreprocessImages";
            this.btnPreprocessImages.Size = new System.Drawing.Size(100, 40);
            this.btnPreprocessImages.TabIndex = 1;
            this.btnPreprocessImages.Text = "预处理图像";
            this.btnPreprocessImages.UseVisualStyleBackColor = true;
            this.btnPreprocessImages.Click += new System.EventHandler(this.btnPreprocessImages_Click);
            // 
            // btnTrainClassifier
            // 
            this.btnTrainClassifier.Location = new System.Drawing.Point(90, 39);
            this.btnTrainClassifier.Name = "btnTrainClassifier";
            this.btnTrainClassifier.Size = new System.Drawing.Size(100, 40);
            this.btnTrainClassifier.TabIndex = 2;
            this.btnTrainClassifier.Text = "训练分类器";
            this.btnTrainClassifier.UseVisualStyleBackColor = true;
            this.btnTrainClassifier.Click += new System.EventHandler(this.btnTrainClassifier_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(199, 21);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(88, 35);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // textBoxInformation
            // 
            this.textBoxInformation.Location = new System.Drawing.Point(651, 27);
            this.textBoxInformation.Multiline = true;
            this.textBoxInformation.Name = "textBoxInformation";
            this.textBoxInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInformation.Size = new System.Drawing.Size(285, 410);
            this.textBoxInformation.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnRandomTest);
            this.groupBox1.Controls.Add(this.btnChooseTestImage);
            this.groupBox1.Controls.Add(this.btnReadClassifier);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Location = new System.Drawing.Point(643, 492);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "3、图像识别测试";
            // 
            // btnRandomTest
            // 
            this.btnRandomTest.Location = new System.Drawing.Point(107, 60);
            this.btnRandomTest.Name = "btnRandomTest";
            this.btnRandomTest.Size = new System.Drawing.Size(180, 35);
            this.btnRandomTest.TabIndex = 6;
            this.btnRandomTest.Text = "随机选择图片测试";
            this.btnRandomTest.UseVisualStyleBackColor = true;
            this.btnRandomTest.Click += new System.EventHandler(this.btnRandomTest_Click);
            // 
            // btnChooseTestImage
            // 
            this.btnChooseTestImage.Location = new System.Drawing.Point(107, 21);
            this.btnChooseTestImage.Name = "btnChooseTestImage";
            this.btnChooseTestImage.Size = new System.Drawing.Size(88, 35);
            this.btnChooseTestImage.TabIndex = 5;
            this.btnChooseTestImage.Text = "选择图片";
            this.btnChooseTestImage.UseVisualStyleBackColor = true;
            this.btnChooseTestImage.Click += new System.EventHandler(this.btnChooseTestImage_Click);
            // 
            // btnReadClassifier
            // 
            this.btnReadClassifier.Location = new System.Drawing.Point(3, 39);
            this.btnReadClassifier.Name = "btnReadClassifier";
            this.btnReadClassifier.Size = new System.Drawing.Size(101, 40);
            this.btnReadClassifier.TabIndex = 4;
            this.btnReadClassifier.Text = "读取分类器";
            this.btnReadClassifier.UseVisualStyleBackColor = true;
            this.btnReadClassifier.Click += new System.EventHandler(this.btnReadClassifier_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.btnComputeConfusionMatrix);
            this.groupBox2.Controls.Add(this.btnSaveClassifier);
            this.groupBox2.Controls.Add(this.btnClassifierParamsSet);
            this.groupBox2.Controls.Add(this.btnTrainClassifier);
            this.groupBox2.Location = new System.Drawing.Point(252, 492);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(385, 100);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2、训练分类器";
            // 
            // btnComputeConfusionMatrix
            // 
            this.btnComputeConfusionMatrix.Location = new System.Drawing.Point(298, 39);
            this.btnComputeConfusionMatrix.Name = "btnComputeConfusionMatrix";
            this.btnComputeConfusionMatrix.Size = new System.Drawing.Size(80, 40);
            this.btnComputeConfusionMatrix.TabIndex = 5;
            this.btnComputeConfusionMatrix.Text = "误差分析";
            this.btnComputeConfusionMatrix.UseVisualStyleBackColor = true;
            this.btnComputeConfusionMatrix.Click += new System.EventHandler(this.btnComputeConfusionMatrix_Click);
            // 
            // btnSaveClassifier
            // 
            this.btnSaveClassifier.Location = new System.Drawing.Point(194, 39);
            this.btnSaveClassifier.Name = "btnSaveClassifier";
            this.btnSaveClassifier.Size = new System.Drawing.Size(100, 40);
            this.btnSaveClassifier.TabIndex = 4;
            this.btnSaveClassifier.Text = "保存分类器";
            this.btnSaveClassifier.UseVisualStyleBackColor = true;
            this.btnSaveClassifier.Click += new System.EventHandler(this.btnSaveClassifier_Click);
            // 
            // btnClassifierParamsSet
            // 
            this.btnClassifierParamsSet.Location = new System.Drawing.Point(6, 39);
            this.btnClassifierParamsSet.Name = "btnClassifierParamsSet";
            this.btnClassifierParamsSet.Size = new System.Drawing.Size(80, 40);
            this.btnClassifierParamsSet.TabIndex = 3;
            this.btnClassifierParamsSet.Text = "参数设置";
            this.btnClassifierParamsSet.UseVisualStyleBackColor = true;
            this.btnClassifierParamsSet.Click += new System.EventHandler(this.btnClassifierParamsSet_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.btnChooseFolder);
            this.groupBox3.Controls.Add(this.btnPreprocessImages);
            this.groupBox3.Location = new System.Drawing.Point(5, 492);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(241, 100);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "1、预处理图像";
            // 
            // btnChooseFolder
            // 
            this.btnChooseFolder.Location = new System.Drawing.Point(7, 39);
            this.btnChooseFolder.Name = "btnChooseFolder";
            this.btnChooseFolder.Size = new System.Drawing.Size(122, 40);
            this.btnChooseFolder.TabIndex = 2;
            this.btnChooseFolder.Text = "选择图像文件夹";
            this.btnChooseFolder.UseVisualStyleBackColor = true;
            this.btnChooseFolder.Click += new System.EventHandler(this.btnChooseFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(648, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "提示信息：";
            // 
            // btnReadInformation
            // 
            this.btnReadInformation.Location = new System.Drawing.Point(651, 443);
            this.btnReadInformation.Name = "btnReadInformation";
            this.btnReadInformation.Size = new System.Drawing.Size(90, 35);
            this.btnReadInformation.TabIndex = 9;
            this.btnReadInformation.Text = "查看说明";
            this.btnReadInformation.UseVisualStyleBackColor = true;
            this.btnReadInformation.Click += new System.EventHandler(this.btnReadInformation_Click);
            // 
            // btnClearText
            // 
            this.btnClearText.Location = new System.Drawing.Point(846, 442);
            this.btnClearText.Name = "btnClearText";
            this.btnClearText.Size = new System.Drawing.Size(90, 35);
            this.btnClearText.TabIndex = 10;
            this.btnClearText.Text = "清空信息";
            this.btnClearText.UseVisualStyleBackColor = true;
            this.btnClearText.Click += new System.EventHandler(this.btnClearText_Click);
            // 
            // btnShowParams
            // 
            this.btnShowParams.Location = new System.Drawing.Point(750, 442);
            this.btnShowParams.Name = "btnShowParams";
            this.btnShowParams.Size = new System.Drawing.Size(90, 35);
            this.btnShowParams.TabIndex = 11;
            this.btnShowParams.Text = "查看参数";
            this.btnShowParams.UseVisualStyleBackColor = true;
            this.btnShowParams.Click += new System.EventHandler(this.btnShowParams_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(942, 598);
            this.Controls.Add(this.btnShowParams);
            this.Controls.Add(this.btnClearText);
            this.Controls.Add(this.btnReadInformation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxInformation);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.hWindowControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DeepLearningDemo之MNIST数据集";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Button btnPreprocessImages;
        private System.Windows.Forms.Button btnTrainClassifier;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox textBoxInformation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnChooseTestImage;
        private System.Windows.Forms.Button btnReadClassifier;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSaveClassifier;
        private System.Windows.Forms.Button btnClassifierParamsSet;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnChooseFolder;
        private System.Windows.Forms.Button btnRandomTest;
        private System.Windows.Forms.Button btnComputeConfusionMatrix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReadInformation;
        private System.Windows.Forms.Button btnClearText;
        private System.Windows.Forms.Button btnShowParams;
    }
}

