namespace DeepLearning_MNIST
{
    partial class ClassifierParamsSetForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassifierParamsSetForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownValidationPercent = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxLearningRateStepRatio = new System.Windows.Forms.TextBox();
            this.textBoxInitialLearningRate = new System.Windows.Forms.TextBox();
            this.numericUpDownTrainingPercent = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNumEpochs = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBatchSize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLearningRateSENE = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValidationPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrainingPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumEpochs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBatchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLearningRateSENE)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownValidationPercent);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxLearningRateStepRatio);
            this.groupBox1.Controls.Add(this.textBoxInitialLearningRate);
            this.groupBox1.Controls.Add(this.numericUpDownTrainingPercent);
            this.groupBox1.Controls.Add(this.numericUpDownNumEpochs);
            this.groupBox1.Controls.Add(this.numericUpDownBatchSize);
            this.groupBox1.Controls.Add(this.numericUpDownLearningRateSENE);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 301);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "训练参数设置";
            // 
            // numericUpDownValidationPercent
            // 
            this.numericUpDownValidationPercent.Location = new System.Drawing.Point(278, 258);
            this.numericUpDownValidationPercent.Name = "numericUpDownValidationPercent";
            this.numericUpDownValidationPercent.Size = new System.Drawing.Size(154, 25);
            this.numericUpDownValidationPercent.TabIndex = 18;
            this.numericUpDownValidationPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownValidationPercent.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownValidationPercent.ValueChanged += new System.EventHandler(this.numericUpDownValidationPercent_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(111, 263);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "ValidationPercent:";
            // 
            // textBoxLearningRateStepRatio
            // 
            this.textBoxLearningRateStepRatio.Location = new System.Drawing.Point(278, 147);
            this.textBoxLearningRateStepRatio.Name = "textBoxLearningRateStepRatio";
            this.textBoxLearningRateStepRatio.Size = new System.Drawing.Size(151, 25);
            this.textBoxLearningRateStepRatio.TabIndex = 16;
            this.textBoxLearningRateStepRatio.Text = "0.1";
            this.textBoxLearningRateStepRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxLearningRateStepRatio.TextChanged += new System.EventHandler(this.textBoxLearningRateStepRatio_TextChanged);
            // 
            // textBoxInitialLearningRate
            // 
            this.textBoxInitialLearningRate.Location = new System.Drawing.Point(278, 184);
            this.textBoxInitialLearningRate.Name = "textBoxInitialLearningRate";
            this.textBoxInitialLearningRate.Size = new System.Drawing.Size(151, 25);
            this.textBoxInitialLearningRate.TabIndex = 15;
            this.textBoxInitialLearningRate.Text = "0.001";
            this.textBoxInitialLearningRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxInitialLearningRate.TextChanged += new System.EventHandler(this.textBoxInitialLearningRate_TextChanged);
            // 
            // numericUpDownTrainingPercent
            // 
            this.numericUpDownTrainingPercent.Location = new System.Drawing.Point(278, 221);
            this.numericUpDownTrainingPercent.Name = "numericUpDownTrainingPercent";
            this.numericUpDownTrainingPercent.Size = new System.Drawing.Size(154, 25);
            this.numericUpDownTrainingPercent.TabIndex = 14;
            this.numericUpDownTrainingPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTrainingPercent.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownTrainingPercent.ValueChanged += new System.EventHandler(this.numericUpDownTrainingPercent_ValueChanged);
            // 
            // numericUpDownNumEpochs
            // 
            this.numericUpDownNumEpochs.Location = new System.Drawing.Point(278, 73);
            this.numericUpDownNumEpochs.Name = "numericUpDownNumEpochs";
            this.numericUpDownNumEpochs.Size = new System.Drawing.Size(154, 25);
            this.numericUpDownNumEpochs.TabIndex = 12;
            this.numericUpDownNumEpochs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownNumEpochs.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownNumEpochs.ValueChanged += new System.EventHandler(this.numericUpDownNumEpochs_ValueChanged);
            // 
            // numericUpDownBatchSize
            // 
            this.numericUpDownBatchSize.Location = new System.Drawing.Point(278, 36);
            this.numericUpDownBatchSize.Name = "numericUpDownBatchSize";
            this.numericUpDownBatchSize.Size = new System.Drawing.Size(154, 25);
            this.numericUpDownBatchSize.TabIndex = 11;
            this.numericUpDownBatchSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownBatchSize.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUpDownBatchSize.ValueChanged += new System.EventHandler(this.numericUpDownBatchSize_ValueChanged);
            // 
            // numericUpDownLearningRateSENE
            // 
            this.numericUpDownLearningRateSENE.Location = new System.Drawing.Point(278, 110);
            this.numericUpDownLearningRateSENE.Name = "numericUpDownLearningRateSENE";
            this.numericUpDownLearningRateSENE.Size = new System.Drawing.Size(154, 25);
            this.numericUpDownLearningRateSENE.TabIndex = 10;
            this.numericUpDownLearningRateSENE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownLearningRateSENE.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownLearningRateSENE.ValueChanged += new System.EventHandler(this.numericUpDownLearningRateSENE_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(127, 226);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "TrainingPercent:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(95, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "InitialLearningRate:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "LearningRateStepRatio:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "LearningRateStepEveryNthEpoch:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "NumEpochs:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "BatchSize:";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(352, 319);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(92, 40);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(237, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ClassifierParamsSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 366);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClassifierParamsSetForm";
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.ClassifierParamsSetForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValidationPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTrainingPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumEpochs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBatchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLearningRateSENE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownTrainingPercent;
        private System.Windows.Forms.NumericUpDown numericUpDownNumEpochs;
        private System.Windows.Forms.NumericUpDown numericUpDownBatchSize;
        private System.Windows.Forms.NumericUpDown numericUpDownLearningRateSENE;
        private System.Windows.Forms.TextBox textBoxInitialLearningRate;
        private System.Windows.Forms.TextBox textBoxLearningRateStepRatio;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numericUpDownValidationPercent;
        private System.Windows.Forms.Label label8;
    }
}