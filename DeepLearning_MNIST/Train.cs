using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using System.IO;
using System.Threading;

namespace DeepLearning_MNIST
{
    public class Train : HalconTools
    {
        //常用训练参数
        public HTuple hv_BatchSize = new HTuple();
        public HTuple hv_NumEpochs = new HTuple();
        public HTuple hv_LearningRateStepEveryNthEpoch = new HTuple();
        public HTuple hv_LearningRateStepRatio = new HTuple();
        public HTuple hv_InitialLearningRate = new HTuple();
        public HTuple hv_PlotEveryNthEpoch = new HTuple();
        public HTuple hv_TrainingPercent = new HTuple();
        public HTuple hv_ValidationPercent = new HTuple();
        public HTuple hv_Pretrained_DlClassifierName = new HTuple();
        

        //训练后的分类器名称
        public HTuple hv_Trained_DlClassifierName = new HTuple();
        //训练分类器句柄
        public HTuple hv_Train_DLClassifierHandle = new HTuple();
        //预处理后图像文件夹
        public HTuple hv_PreprocessedFolder = new HTuple();
        //训练线程
        public Thread _TrainingThread = null;
        //窗口句柄
        HTuple hv_WindowHandle = new HTuple();

        HTuple hv_ImageFiles = new HTuple(), hv_GroundTruthLabels = new HTuple();
        HTuple hv_UniqueClasses = new HTuple();
        HTuple hv_I = new HTuple(), hv_path = new HTuple(), hv_BaseNames = new HTuple();
        HTuple hv_Extensions = new HTuple(), hv_Directories = new HTuple();
        HTuple hv_ObjectFilesOut = new HTuple(), hv_ImageIndex = new HTuple();
        HTuple hv_Labels = new HTuple();
        HTuple hv_LabelsIndices = new HTuple(), hv_Classes = new HTuple();

        HTuple hv_TrainingImages = new HTuple(), hv_TrainingLabels = new HTuple();
        HTuple hv_ValidationImages = new HTuple(), hv_ValidationLabels = new HTuple();
        HTuple hv_TestImages = new HTuple(), hv_TestLabels = new HTuple();
        HTuple hv_Exception = new HTuple();

        HTuple hv_PredictedClassesValidation = new HTuple();
        HTuple hv_ConfusionMatrix = new HTuple(), hv_Runtime = new HTuple();
        HTuple hv_TestImageFiles = new HTuple(), hv_Index = new HTuple();
        HTuple hv_ImageFile = new HTuple(), hv_DLClassifierResultHandle = new HTuple();
        HTuple hv_PredictedClass = new HTuple(), hv_Text = new HTuple();
        HTuple hv_RemovePreprocessingAfterExample = new HTuple();


        public Train()
        {
            this.hv_Trained_DlClassifierName = null;
            this.hv_Train_DLClassifierHandle = null;
            this.hv_BatchSize = 32;
            this.hv_NumEpochs = 24;
            this.hv_LearningRateStepEveryNthEpoch = 6;
            this.hv_LearningRateStepRatio = 0.5;
            this.hv_InitialLearningRate = 0.001;
            this.hv_PlotEveryNthEpoch = 1;
            this.hv_TrainingPercent = 60;
            this.hv_ValidationPercent = 20;
            
        }

        public void TrainProcess(HTuple hv_WindowHandle,string pretrained_DlClassifierName)
        {
            this.hv_WindowHandle = hv_WindowHandle;
            //This example shows how to train a deep learning fruit classifier, along with a short overview of the necessary steps.
            //
            //Initialization.
            //dev_open_window_fit_size(0, 0, hv_WindowWidth, hv_WindowHeight, -1, -1, out hv_WindowHandle);
            set_display_font(hv_WindowHandle, 16, "mono", "true", "false");
            //
            //Some procedures use a random number generator. Set the seed for reproducibility.
            HOperatorSet.SetSystem("seed_rand", 42);
            //
            HOperatorSet.ClearWindow(hv_WindowHandle);

            //
            //** TRAINING **
            //
            //Read one of the pretrained networks.
            HOperatorSet.ReadDlClassifier(pretrained_DlClassifierName, out this.hv_Train_DLClassifierHandle);

            //2) Split data into training, validation, and test set.
            //
            //Read the data, i.e., the paths of the images and their respective ground truth labels.
            hv_ImageFiles.Dispose(); hv_Labels.Dispose(); hv_LabelsIndices.Dispose(); hv_Classes.Dispose();
            read_dl_classifier_data_set(this.hv_PreprocessedFolder, "last_folder", out hv_ImageFiles,
                out hv_Labels, out hv_LabelsIndices, out hv_Classes);
            //
            //Split the data into three subsets,
            //Default for training 80%, validation 20%, and testing 0%.
            hv_TrainingImages.Dispose(); hv_TrainingLabels.Dispose(); hv_ValidationImages.Dispose(); hv_ValidationLabels.Dispose(); hv_TestImages.Dispose(); hv_TestLabels.Dispose();
            split_dl_classifier_data_set(hv_ImageFiles, hv_Labels, this.hv_TrainingPercent,
               this.hv_ValidationPercent, out hv_TrainingImages, out hv_TrainingLabels, out hv_ValidationImages,
                out hv_ValidationLabels, out hv_TestImages, out hv_TestLabels);
            //
            //Set training hyper-parameters.
            //In order to retrain the neural network, we have to specify
            //the class names of our classification problem.
            HOperatorSet.SetDlClassifierParam(this.hv_Train_DLClassifierHandle, "classes", hv_Classes);
            //Set the batch size.
            HOperatorSet.SetDlClassifierParam(this.hv_Train_DLClassifierHandle, "batch_size", this.hv_BatchSize);
            hv_RemovePreprocessingAfterExample.Dispose();
            hv_RemovePreprocessingAfterExample = 0;
            //Try to initialize the runtime environment.
            try
            {
                HOperatorSet.SetDlClassifierParam(this.hv_Train_DLClassifierHandle, "runtime_init",
                    "immediately");
            }
            // catch (Exception) 
            catch (HalconException HDevExpDefaultException1)
            {
                HDevExpDefaultException1.ToHTuple(out hv_Exception);
                HOperatorSet.DispText(hv_WindowHandle, "Failed to initialize the runtime environment.", "window", "bottom", "right", "red", "box", "false");
                //dev_disp_error_text(hv_Exception);
                if ((int)(hv_RemovePreprocessingAfterExample.TupleAnd(new HTuple(((hv_Exception.TupleSelect(
                    0))).TupleNotEqual(4104)))) != 0)
                {
                    remove_dir_recursively(hv_PreprocessedFolder);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.DispText(hv_WindowHandle, ("Preprocessed data in folder \"" + hv_PreprocessedFolder) + "\" have been deleted.",
                            "window", "top", "right", "red", new HTuple(), new HTuple());
                    }
                }
                return;
            }
            //For this data set, an initial learning rate of 0.001 has proven to yield good results.
            HOperatorSet.SetDlClassifierParam(this.hv_Train_DLClassifierHandle, "learning_rate", this.hv_InitialLearningRate);

            // stop(...); only in hdevelop
            //
            //Train the classifier.
            HOperatorSet.DispText(hv_WindowHandle, "Training has started...",
                "window", "top", "left", "black", new HTuple(), new HTuple());

            Console.WriteLine("开始训练过程...");

            _TrainingThread = new Thread(new ThreadStart(TrainingThread));
            _TrainingThread.IsBackground = true;
            _TrainingThread.Start();
            //
            //In this example, we reduce the learning rate by a factor of 1/10 every 5th epoch.
            //We iterate 50 times over the full training set.


            //ComputeConfusionMatrix(hv_WindowHandle);
        }

        public void ComputeConfusionMatrix(HTuple hv_WindowHandle)
        {
            HOperatorSet.ClearWindow(hv_WindowHandle);
            hv_Train_DLClassifierHandle.Dispose();
            HOperatorSet.ReadDlClassifier(this.hv_Trained_DlClassifierName, out hv_Train_DLClassifierHandle);
            //
            //Compute the confusion matrix for the validation data set.
            hv_PredictedClassesValidation.Dispose();
            get_predicted_classes(hv_ValidationImages, hv_Train_DLClassifierHandle, out hv_PredictedClassesValidation);
            hv_ConfusionMatrix.Dispose();
            gen_confusion_matrix(hv_ValidationLabels, hv_PredictedClassesValidation, new HTuple(),
                new HTuple(), hv_WindowHandle, out hv_ConfusionMatrix);

            HOperatorSet.DispText(hv_WindowHandle, "Validation data", "window",
                "top", "left", "gray", "box", "false");
            HOperatorSet.DispText(hv_WindowHandle, "Click to continue...",
                "window", "bottom", "right", "black", new HTuple(), new HTuple());

        }

        private void TrainingThread()
        {
            train_classifier(this.hv_Train_DLClassifierHandle, this.hv_Trained_DlClassifierName, this.hv_NumEpochs, hv_TrainingImages,
                hv_TrainingLabels, hv_ValidationImages, hv_ValidationLabels, this.hv_LearningRateStepEveryNthEpoch,
                this.hv_LearningRateStepRatio, this.hv_PlotEveryNthEpoch, this.hv_WindowHandle);
            HOperatorSet.DispText(this.hv_WindowHandle, "Training over, Click to continue...",
                "window", "bottom", "right", "black", new HTuple(), new HTuple());
        }

        public string TrainClassifierParams()
        {
            string[] classifierParamsLabels = { "BatchSize", "NumEpochs", "InitialLearningRate", "LearningRateStepRatio", "LearningRateStepEveryNthEpoch", "TrainingPercent", "ValidationPercent" };
            string[] classifierParamsValues = { this.hv_BatchSize.ToString(), this.hv_NumEpochs.ToString(), this.hv_InitialLearningRate.ToString(), this.hv_LearningRateStepRatio.ToString(), this.hv_LearningRateStepEveryNthEpoch.ToString(), this.hv_TrainingPercent.ToString(), this.hv_ValidationPercent.ToString() };

            StringBuilder classifierParams = new StringBuilder();
            for (int i = 0; i < classifierParamsLabels.Length; i++)
            {
                classifierParams.Append(classifierParamsLabels[i] + "：" + classifierParamsValues[i] + "\r\n");
            }
            return classifierParams.ToString();

        }

        private void train_classifier(HTuple hv_DLClassifierHandle, HTuple hv_FileName,
            HTuple hv_NumEpochs, HTuple hv_TrainingImages, HTuple hv_TrainingLabels, HTuple hv_ValidationImages,
            HTuple hv_ValidationLabels, HTuple hv_LearningRateStepEveryNthEpoch, HTuple hv_LearningRateStepRatio,
            HTuple hv_PlotEveryNthEpoch, HTuple hv_WindowHandle)
        {

            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_BatchImages = null;

            // Local control variables 

            HTuple hv_TrainingErrors = new HTuple(), hv_ValidationErrors = new HTuple();
            HTuple hv_LearningRates = new HTuple(), hv_Epochs = new HTuple();
            HTuple hv_LossByIteration = new HTuple(), hv_BatchSize = new HTuple();
            HTuple hv_MinValidationError = new HTuple(), hv_NumBatchesInEpoch = new HTuple();
            HTuple hv_NumTotalIterations = new HTuple(), hv_PlottedIterations = new HTuple();
            HTuple hv_TrainSequence = new HTuple(), hv_SelectPercentageTrainingImages = new HTuple();
            HTuple hv_TrainingImagesSelected = new HTuple(), hv_TrainingLabelsSelected = new HTuple();
            HTuple hv_Epoch = new HTuple(), hv_Iteration = new HTuple();
            HTuple hv_BatchStart = new HTuple(), hv_BatchEnd = new HTuple();
            HTuple hv_BatchIndices = new HTuple(), hv_BatchImageFiles = new HTuple();
            HTuple hv_BatchLabels = new HTuple(), hv_GenParamName = new HTuple();
            HTuple hv_GenParamValue = new HTuple(), hv_DLClassifierTrainResultHandle = new HTuple();
            HTuple hv_Loss = new HTuple(), hv_CurrentIteration = new HTuple();
            HTuple hv_TrainingDLClassifierResultIDs = new HTuple();
            HTuple hv_ValidationDLClassifierResultIDs = new HTuple();
            HTuple hv_TrainingTop1Error = new HTuple(), hv_ValidationTop1Error = new HTuple();
            HTuple hv_LearningRate = new HTuple();

            HTupleVector hvec_TrainingPredictedLabels = new HTupleVector(1);
            HTupleVector hvec_TrainingConfidences = new HTupleVector(1);
            HTupleVector hvec_ValidationPredictedLabels = new HTupleVector(1);
            HTupleVector hvec_ValidationConfidences = new HTupleVector(1);
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_BatchImages);
            try
            {
                //For the plot during training,
                //we need to concatenate some intermediate results.
                hv_TrainingErrors.Dispose();
                hv_TrainingErrors = new HTuple();
                hv_ValidationErrors.Dispose();
                hv_ValidationErrors = new HTuple();
                hv_LearningRates.Dispose();
                hv_LearningRates = new HTuple();
                hv_Epochs.Dispose();
                hv_Epochs = new HTuple();
                hv_LossByIteration.Dispose();
                hv_LossByIteration = new HTuple();
                hv_BatchSize.Dispose();
                HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "batch_size", out hv_BatchSize);
                hv_MinValidationError.Dispose();
                hv_MinValidationError = 1;
                //
                //Create a tuple that includes all the iterations
                //where the plot should be computed (including the last ieration).
                hv_NumBatchesInEpoch.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumBatchesInEpoch = (((((new HTuple(hv_TrainingImages.TupleLength()
                        )) / (hv_BatchSize.TupleReal()))).TupleFloor())).TupleInt();
                }
                hv_NumTotalIterations.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_NumTotalIterations = (hv_NumBatchesInEpoch * hv_NumEpochs) - 1;
                }
                hv_PlottedIterations.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_PlottedIterations = ((((hv_NumBatchesInEpoch * HTuple.TupleGenSequence(
                        0, hv_NumEpochs - 1, hv_PlotEveryNthEpoch))).TupleConcat(hv_NumTotalIterations))).TupleRound()
                        ;
                }
                //
                //TrainSequence is used for easier indexing of the training data.
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_TrainSequence.Dispose();
                    HOperatorSet.TupleGenSequence(0, (new HTuple(hv_TrainingImages.TupleLength()
                        )) - 1, 1, out hv_TrainSequence);
                }
                //
                //Select a subset of the training data set
                //in order to obtain a fast approximation
                //of the training error during training (plotting).
                hv_SelectPercentageTrainingImages.Dispose();
                hv_SelectPercentageTrainingImages = 100;
                hv_TrainingImagesSelected.Dispose(); hv_TrainingLabelsSelected.Dispose();
                select_percentage_dl_classifier_data(hv_TrainingImages, hv_TrainingLabels,
                    hv_SelectPercentageTrainingImages, out hv_TrainingImagesSelected, out hv_TrainingLabelsSelected);
                //
                HTuple end_val25 = hv_NumEpochs - 1;
                HTuple step_val25 = 1;
                #region
                for (hv_Epoch = 0; hv_Epoch.Continue(end_val25, step_val25); hv_Epoch = hv_Epoch.TupleAdd(step_val25))
                {
                    //In order to get randomness in each epoch,
                    //the training set is shuffled every epoch.
                    {
                        HTuple ExpTmpOutVar_0;
                        tuple_shuffle(hv_TrainSequence, out ExpTmpOutVar_0);
                        hv_TrainSequence.Dispose();
                        hv_TrainSequence = ExpTmpOutVar_0;
                    }
                    HTuple end_val29 = hv_NumBatchesInEpoch - 1;
                    HTuple step_val29 = 1;
                    for (hv_Iteration = 0; hv_Iteration.Continue(end_val29, step_val29); hv_Iteration = hv_Iteration.TupleAdd(step_val29))
                    {
                        //Select a batch from the training data set.
                        hv_BatchStart.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_BatchStart = hv_Iteration * hv_BatchSize;
                        }
                        hv_BatchEnd.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_BatchEnd = hv_BatchStart + (hv_BatchSize - 1);
                        }
                        hv_BatchIndices.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_BatchIndices = hv_TrainSequence.TupleSelectRange(
                                hv_BatchStart, hv_BatchEnd);
                        }
                        hv_BatchImageFiles.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_BatchImageFiles = hv_TrainingImages.TupleSelect(
                                hv_BatchIndices);
                        }
                        hv_BatchLabels.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_BatchLabels = hv_TrainingLabels.TupleSelect(
                                hv_BatchIndices);
                        }
                        //
                        //Read the image of the current batch.
                        ho_BatchImages.Dispose();
                        HOperatorSet.ReadImage(out ho_BatchImages, hv_BatchImageFiles);
                        //Augment the images to get a better variety of training images.
                        hv_GenParamName.Dispose();
                        hv_GenParamName = "mirror";
                        hv_GenParamValue.Dispose();
                        hv_GenParamValue = "rc";
                        {
                            HObject ExpTmpOutVar_0;
                            augment_images(ho_BatchImages, out ExpTmpOutVar_0, hv_GenParamName, hv_GenParamValue);
                            ho_BatchImages.Dispose();
                            ho_BatchImages = ExpTmpOutVar_0;
                        }
                        //
                        //Train the network with these images and ground truth labels.
                        hv_DLClassifierTrainResultHandle.Dispose();
                        HOperatorSet.TrainDlClassifierBatch(ho_BatchImages, hv_DLClassifierHandle,
                            hv_BatchLabels, out hv_DLClassifierTrainResultHandle);
                        //You can access the current value of the loss function,
                        //which should decrease during the training.
                        hv_Loss.Dispose();
                        HOperatorSet.GetDlClassifierTrainResult(hv_DLClassifierTrainResultHandle,
                            "loss", out hv_Loss);
                        //Store the loss in a tuple .
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            {
                                HTuple
                                  ExpTmpLocalVar_LossByIteration = hv_LossByIteration.TupleConcat(
                                    hv_Loss);
                                hv_LossByIteration.Dispose();
                                hv_LossByIteration = ExpTmpLocalVar_LossByIteration;
                            }
                        }
                        //
                        //In regular intervals, we want to evaluate
                        //how well our classifier performs.
                        hv_CurrentIteration.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_CurrentIteration = ((hv_Iteration + (hv_NumBatchesInEpoch * hv_Epoch))).TupleInt()
                                ;
                        }
                        if ((int)(((hv_CurrentIteration.TupleEqualElem(hv_PlottedIterations))).TupleSum()
                            ) != 0)
                        {
                            //Plot the progress regularly.
                            //Evaluate the current classifier on the training and validation set.
                            hv_TrainingDLClassifierResultIDs.Dispose(); hvec_TrainingPredictedLabels.Dispose(); hvec_TrainingConfidences.Dispose();
                            apply_dl_classifier_batchwise(hv_TrainingImagesSelected, hv_DLClassifierHandle,
                                out hv_TrainingDLClassifierResultIDs, out hvec_TrainingPredictedLabels,
                                out hvec_TrainingConfidences);
                            hv_ValidationDLClassifierResultIDs.Dispose(); hvec_ValidationPredictedLabels.Dispose(); hvec_ValidationConfidences.Dispose();
                            apply_dl_classifier_batchwise(hv_ValidationImages, hv_DLClassifierHandle,
                                out hv_ValidationDLClassifierResultIDs, out hvec_ValidationPredictedLabels,
                                out hvec_ValidationConfidences);
                            //Evaluate the top-1 error on each dataset.
                            hv_TrainingTop1Error.Dispose();
                            evaluate_dl_classifier(hv_TrainingLabelsSelected, hv_DLClassifierHandle,
                                hv_TrainingDLClassifierResultIDs, "top1_error", "global", out hv_TrainingTop1Error);
                            hv_ValidationTop1Error.Dispose();
                            evaluate_dl_classifier(hv_ValidationLabels, hv_DLClassifierHandle, hv_ValidationDLClassifierResultIDs,
                                "top1_error", "global", out hv_ValidationTop1Error);
                            //Concatenate the values for the plot.
                            hv_LearningRate.Dispose();
                            HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "learning_rate",
                                out hv_LearningRate);
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_TrainingErrors = hv_TrainingErrors.TupleConcat(
                                        hv_TrainingTop1Error);
                                    hv_TrainingErrors.Dispose();
                                    hv_TrainingErrors = ExpTmpLocalVar_TrainingErrors;
                                }
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_ValidationErrors = hv_ValidationErrors.TupleConcat(
                                        hv_ValidationTop1Error);
                                    hv_ValidationErrors.Dispose();
                                    hv_ValidationErrors = ExpTmpLocalVar_ValidationErrors;
                                }
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_LearningRates = hv_LearningRates.TupleConcat(
                                        hv_LearningRate);
                                    hv_LearningRates.Dispose();
                                    hv_LearningRates = ExpTmpLocalVar_LearningRates;
                                }
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                {
                                    HTuple
                                      ExpTmpLocalVar_Epochs = hv_Epochs.TupleConcat(
                                        (hv_PlottedIterations.TupleSelect(new HTuple(hv_Epochs.TupleLength()
                                        ))) / (hv_NumBatchesInEpoch.TupleReal()));
                                    hv_Epochs.Dispose();
                                    hv_Epochs = ExpTmpLocalVar_Epochs;
                                }
                            }

                            //Plot validation and error against the epochs in order to
                            //observe the progress of the training.
                            plot_dl_classifier_training_progress(hv_TrainingErrors, hv_ValidationErrors,hv_LearningRates, hv_Epochs, hv_NumEpochs, hv_WindowHandle);
                            //如果上面绘制坐标系到窗体过程报错，搞不定的话就用下面这句代替，直接显示训练数据到窗体
                            //disp_training_progress_datas(hv_TrainingErrors, hv_ValidationErrors, hv_LearningRates, hv_Epochs, hv_NumEpochs, hv_WindowHandle);


                            if ((int)(new HTuple(hv_ValidationTop1Error.TupleLessEqual(hv_MinValidationError))) != 0)
                            {
                                HOperatorSet.WriteDlClassifier(hv_DLClassifierHandle, hv_FileName);
                                hv_MinValidationError.Dispose();
                                hv_MinValidationError = new HTuple(hv_ValidationTop1Error);
                            }
                        }
                    }
                    //Reduce the learning rate every nth epoch.
                    if ((int)(new HTuple((((hv_Epoch + 1) % hv_LearningRateStepEveryNthEpoch)).TupleEqual(
                        0))) != 0)
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            HOperatorSet.SetDlClassifierParam(hv_DLClassifierHandle, "learning_rate",
                                hv_LearningRate * hv_LearningRateStepRatio);
                        }
                        hv_LearningRate.Dispose();
                        HOperatorSet.GetDlClassifierParam(hv_DLClassifierHandle, "learning_rate",
                            out hv_LearningRate);
                    }
                }
                #endregion
                // stop(...); only in hdevelop
                ho_BatchImages.Dispose();

                hv_TrainingErrors.Dispose();
                hv_ValidationErrors.Dispose();
                hv_LearningRates.Dispose();
                hv_Epochs.Dispose();
                hv_LossByIteration.Dispose();
                hv_BatchSize.Dispose();
                hv_MinValidationError.Dispose();
                hv_NumBatchesInEpoch.Dispose();
                hv_NumTotalIterations.Dispose();
                hv_PlottedIterations.Dispose();
                hv_TrainSequence.Dispose();
                hv_SelectPercentageTrainingImages.Dispose();
                hv_TrainingImagesSelected.Dispose();
                hv_TrainingLabelsSelected.Dispose();
                hv_Epoch.Dispose();
                hv_Iteration.Dispose();
                hv_BatchStart.Dispose();
                hv_BatchEnd.Dispose();
                hv_BatchIndices.Dispose();
                hv_BatchImageFiles.Dispose();
                hv_BatchLabels.Dispose();
                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();
                hv_DLClassifierTrainResultHandle.Dispose();
                hv_Loss.Dispose();
                hv_CurrentIteration.Dispose();
                hv_TrainingDLClassifierResultIDs.Dispose();
                hv_ValidationDLClassifierResultIDs.Dispose();
                hv_TrainingTop1Error.Dispose();
                hv_ValidationTop1Error.Dispose();
                hv_LearningRate.Dispose();
                hvec_TrainingPredictedLabels.Dispose();
                hvec_TrainingConfidences.Dispose();
                hvec_ValidationPredictedLabels.Dispose();
                hvec_ValidationConfidences.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_BatchImages.Dispose();

                hv_TrainingErrors.Dispose();
                hv_ValidationErrors.Dispose();
                hv_LearningRates.Dispose();
                hv_Epochs.Dispose();
                hv_LossByIteration.Dispose();
                hv_BatchSize.Dispose();
                hv_MinValidationError.Dispose();
                hv_NumBatchesInEpoch.Dispose();
                hv_NumTotalIterations.Dispose();
                hv_PlottedIterations.Dispose();
                hv_TrainSequence.Dispose();
                hv_SelectPercentageTrainingImages.Dispose();
                hv_TrainingImagesSelected.Dispose();
                hv_TrainingLabelsSelected.Dispose();
                hv_Epoch.Dispose();
                hv_Iteration.Dispose();
                hv_BatchStart.Dispose();
                hv_BatchEnd.Dispose();
                hv_BatchIndices.Dispose();
                hv_BatchImageFiles.Dispose();
                hv_BatchLabels.Dispose();
                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();
                hv_DLClassifierTrainResultHandle.Dispose();
                hv_Loss.Dispose();
                hv_CurrentIteration.Dispose();
                hv_TrainingDLClassifierResultIDs.Dispose();
                hv_ValidationDLClassifierResultIDs.Dispose();
                hv_TrainingTop1Error.Dispose();
                hv_ValidationTop1Error.Dispose();
                hv_LearningRate.Dispose();
                hvec_TrainingPredictedLabels.Dispose();
                hvec_TrainingConfidences.Dispose();
                hvec_ValidationPredictedLabels.Dispose();
                hvec_ValidationConfidences.Dispose();

                throw HDevExpDefaultException;
            }

        }

        private void disp_training_progress_datas(HTuple hv_TrainingErrors, HTuple hv_ValidationErrors, HTuple hv_LearningRates, HTuple hv_Epochs, HTuple hv_NumEpochs, HTuple hv_WindowHandle)
        {
            HOperatorSet.ClearWindow(hv_WindowHandle);

            if (hv_Epochs.TupleMax()< hv_NumEpochs)
            {
                HOperatorSet.DispText(hv_WindowHandle, "Training...","window", "top", "left", "black", new HTuple(), new HTuple());
            }
            HOperatorSet.DispText(hv_WindowHandle, "hv_Epoch", "window", 80, 30, "white", "box", "false");
            HOperatorSet.DispText(hv_WindowHandle, "LearningRate", "window", 80, 150, "white", "box", "false");
            HOperatorSet.DispText(hv_WindowHandle, "TrainingError", "window", 80, 300, "white", "box", "false");
            HOperatorSet.DispText(hv_WindowHandle, "ValidationError", "window", 80, 460, "white", "box", "false");

            HOperatorSet.DispText(hv_WindowHandle, hv_Epochs.TupleString(".0f"), "window", 100, 30, "white", "box", "false");
            HOperatorSet.DispText(hv_WindowHandle, hv_LearningRates.TupleString(".8f"), "window", 100, 150, "white", "box", "false");
            HOperatorSet.DispText(hv_WindowHandle, (hv_TrainingErrors * 100).TupleString(".2f") + "%", "window", 100, 300, "white", "box", "false");
            HOperatorSet.DispText(hv_WindowHandle, (hv_ValidationErrors * 100).TupleString(".2f") + "%", "window", 100, 460, "white", "box", "false");
        }
    }
}
