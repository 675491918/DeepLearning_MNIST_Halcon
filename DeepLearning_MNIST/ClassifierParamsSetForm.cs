using HalconDotNet;
using System;
using System.Windows.Forms;

namespace DeepLearning_MNIST
{
    public partial class ClassifierParamsSetForm : Form
    {
        public ClassifierParamsSetForm()
        {
            InitializeComponent();

        }


        public HTuple _BatchSize = new HTuple();
        public HTuple _NumEpochs = new HTuple();
        public HTuple _InitialLearningRate = new HTuple();
        public HTuple _LearningRateStepEveryNthEpoch = new HTuple();
        public HTuple _LearningRateStepRatio = new HTuple();
        public HTuple _TrainingPercent = new HTuple();
        public HTuple _ValidationPercent = new HTuple();


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void ClassifierParamsSetForm_Load(object sender, EventArgs e)
        {
            numericUpDownBatchSize.Value = _BatchSize;
            numericUpDownNumEpochs.Value = _NumEpochs;
            numericUpDownLearningRateSENE.Value = _LearningRateStepEveryNthEpoch;
            textBoxLearningRateStepRatio.Text = _LearningRateStepRatio.ToString();
            textBoxInitialLearningRate.Text = _InitialLearningRate.ToString();
            numericUpDownTrainingPercent.Value = _TrainingPercent;
            numericUpDownValidationPercent.Value = _ValidationPercent;
        }

        private void numericUpDownBatchSize_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numericUpDownBatchSize.Value) > 100 || Convert.ToInt32(numericUpDownBatchSize.Value) < 1)
            {
                return;
            }
            this._BatchSize = Convert.ToInt32(numericUpDownBatchSize.Value);
            btnConfirm.Focus();
        }

        private void numericUpDownNumEpochs_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numericUpDownNumEpochs.Value) > 100 || Convert.ToInt32(numericUpDownNumEpochs.Value) < 1)
            {
                return;
            }
            this._NumEpochs = Convert.ToInt32(numericUpDownNumEpochs.Value);
            btnConfirm.Focus();
        }

        private void numericUpDownLearningRateSENE_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numericUpDownLearningRateSENE.Value) > 100 || Convert.ToInt32(numericUpDownLearningRateSENE.Value) < 1)
            {
                return;
            }
            this._LearningRateStepEveryNthEpoch = Convert.ToInt32(numericUpDownLearningRateSENE.Value);
            btnConfirm.Focus();
        }

        private void textBoxLearningRateStepRatio_TextChanged(object sender, EventArgs e)
        {
            this._LearningRateStepRatio = Convert.ToDouble(textBoxLearningRateStepRatio.Text);
            btnConfirm.Focus();
        }

        private void textBoxInitialLearningRate_TextChanged(object sender, EventArgs e)
        {
            this._InitialLearningRate = Convert.ToDouble(textBoxInitialLearningRate.Text);
            btnConfirm.Focus();
        }


        private void numericUpDownTrainingPercent_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numericUpDownTrainingPercent.Value) > 100 || Convert.ToInt32(numericUpDownTrainingPercent.Value) < 1)
            {
                return;
            }
            this._TrainingPercent = Convert.ToInt32(numericUpDownTrainingPercent.Value);
            btnConfirm.Focus();
        }

        private void numericUpDownValidationPercent_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numericUpDownValidationPercent.Value) > 100 || Convert.ToInt32(numericUpDownValidationPercent.Value) < 1)
            {
                return;
            }
            this._ValidationPercent = Convert.ToInt32(numericUpDownValidationPercent.Value);
            btnConfirm.Focus();
        }

    }
}
