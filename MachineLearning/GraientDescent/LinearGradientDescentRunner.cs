using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    class LinearGradientDescentRunner
    {
        public static List<string[]> Sales { get; private set; }
        public static List<string[]> Train_data { get; private set; }
        public static List<string[]> Test_data { get; private set; }

        internal static void Run()
        {
            Console.WriteLine("Linear Gradient Descent");
            LoadData();
            //Console.WriteLine("Convert to Numpy array");
            //RunExample();
            Console.WriteLine("Gradient Descent");
            RunQuiz();
            Console.ReadLine();
        }

        private static void LoadData()
        {
            Sales = CsvReader.Read(@"C:\machineLearning\regssion\gradientDescent\kc_house_data.csv", true);
            Train_data = CsvReader.Read(@"C:\machineLearning\regssion\gradientDescent\kc_house_train_data.csv", true);
            Test_data = CsvReader.Read(@"C:\machineLearning\regssion\gradientDescent\kc_house_test_data.csv", true);
        }

        private static void RunQuiz()
        {
            Console.WriteLine("Run Linear Regssion");
            double[] model1Predictions = RunLinearRegssion();
            Console.WriteLine("Run Multiple Regssion");
            double[] model2Predictions = RunMultipleRegssion();
            Console.WriteLine($"Linear Regssion Prediction[0]={model1Predictions[0]}");
            Console.WriteLine($"Multiple Regssion Prediction[0]={model2Predictions[0]}");
            Console.WriteLine($"Linear Regssion RSS[0]=" +
                $"{GraientDescent.ComputeRss(double.Parse(Test_data[0][2]), model1Predictions[0])}");
            Console.WriteLine($"Multiple Regssion RSS[0]=" +
                $"{GraientDescent.ComputeRss(double.Parse(Test_data[0][2]), model2Predictions[0])}");
            Console.WriteLine($"Linear Regssion RSS=" +
    $"{GraientDescent.ComputeRss(Test_data, 2, model1Predictions)}");
            Console.WriteLine($"Multiple Regssion RSS=" +
                $"{GraientDescent.ComputeRss(Test_data, 2, model2Predictions)}");

        }

        private static double[] RunMultipleRegssion()
        {
            var initialWeights2 = new[] { -100000.0, 1.0, 1.0 };
            var stepSize2 = 4e-12;
            var tolerance2 = 1e9;
            var features2 = new List<int> { 5, 19 };
            var weights2 = TrainModel(features2, initialWeights2, stepSize2, tolerance2);
            var model2Predictions = RunTestData(features2, weights2);
            return model2Predictions;
        }

        private static double[] RunLinearRegssion()
        {
            var initialWeights = new[] { -47000.0, 1.0 };
            var stepSize = 7e-12;
            var tolerance = 2.5e7;
            var features = new List<int> { 5 };
            var weights = TrainModel(features, initialWeights, stepSize, tolerance);
            var model1Predictions = RunTestData(features, weights);
            return model1Predictions;
        }

        private static double[] RunTestData(List<int> features, double[] weights)
        {
            //Console.WriteLine("Run on test data");
            var featuresMatirxOutputArray = GraientDescent.Get_Numpy_Data(Test_data, features, 2);
            return GraientDescent.PredictionOutput(featuresMatirxOutputArray.Item1, weights);
        }

        private static double[] TrainModel(List<int> features, double[] initialWeights,
            double stepSize, double tolerance)
        {
            Console.WriteLine("Run on train data");
            var featuresMatirxOutputArray = GraientDescent.Get_Numpy_Data(Train_data, features, 2);
            var weights = GraientDescent.RegssionGraientDescent(featuresMatirxOutputArray.Item1,
                featuresMatirxOutputArray.Item2,
                initialWeights, stepSize, tolerance);
            return weights;
        }

        private static void RunExample()
        {
            var featuresMatirxOutputArray = GraientDescent.Get_Numpy_Data(Sales, new List<int> { 5 }, 2);
            var features = featuresMatirxOutputArray.Item1;
            var outputs = featuresMatirxOutputArray.Item2;
            foreach (var featurevalue in features[0])
            {
                Console.WriteLine(featurevalue);
            }
            if (Math.Abs(features[0][0] - 1.0) > 0)
            {
                throw new Exception("feature[0] wrong!");
            }
            if (Math.Abs(features[0][1] - 1180.0) > 0)
            {
                throw new Exception("feature[0] wrong!");
            }
            Console.WriteLine(outputs[0]);
            if (Math.Abs(outputs[0] - 221900.0) > 0)
            {
                throw new Exception("Output wrong!");
            }
            Console.WriteLine("Predicting oupt give regssion weights");
            var weights = new[] { 1.0, 1.0 };
            var predictedValue = GraientDescent.PredictionOutput(
                new List<double[]> { features[0], features[1] },
                weights);
            Console.WriteLine(predictedValue[0]);
            if (Math.Abs(predictedValue[0] - 1181.0) > 0)
            {
                throw new Exception("Predicted value wrong");
            }
            Console.WriteLine(predictedValue[1]);
            if (Math.Abs(predictedValue[1] - 2571.0) > 0)
            {
                throw new Exception("Predicted value wrong");
            }
            Console.WriteLine("Computing the derivative");
            weights = new[] { 0.0, 0.0 };
            var testPredictions = GraientDescent.PredictionOutput(features, weights);
            var errors = GraientDescent.ComputeErrors(testPredictions, outputs);
            var derivative = GraientDescent.FeatureDerivative(GraientDescent.FilterColumns(features, 0), errors);
            Console.WriteLine(derivative);
            var sumOutputs = Numpy.Sum(outputs) * -2;
            Console.WriteLine(sumOutputs);
            if (Math.Abs(sumOutputs - derivative) > 0)
            {
                throw new Exception("derivative wrong");
            }

        }
    }
}
