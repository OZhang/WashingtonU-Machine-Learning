using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    class GraientDescent
    {
        public static double[] RegssionGraientDescent(List<double[]> featuresMatrix, double[] output,
            double[] initialWeights, double stepSize, double tolerance)
        {
            var converged = false;
            var weights = initialWeights;
            var iterate = 0;
            while (!converged)
            {
                iterate++;
                if (iterate % 1000 == 0)
                {
                    Console.WriteLine("Iterate:" + iterate);
                    DisplayWeights(weights);
                }
                var predictions = PredictionOutput(featuresMatrix, weights);
                var errors = ComputeErrors(predictions, output);
                var gradientSumSquares = 0.0;
                for (var i = 0; i < weights.Length; i++)
                {
                    var derivative = FeatureDerivative(FilterColumns(featuresMatrix, i), errors);
                    gradientSumSquares += derivative * derivative;
                    weights[i] -= stepSize * derivative;
                }
                var gradientMagnitude = Math.Sqrt(gradientSumSquares);
                if (gradientMagnitude < tolerance)
                    converged = true;
            }
            DisplayWeights(weights);
            return weights;
        }

        internal static void DisplayWeights(double[] weights)
        {
            for (var i = 0; i < weights.Length; i++)
            {
                var weight = weights[i];
                Console.WriteLine($"Weight[{i}]:{weight}");
            }
        }

        internal static List<double[]> FilterColumns(List<double[]> features, params int[] columns)
        {
            var result = new List<double[]>();
            foreach (var feature in features)
            {
                var values = new double[columns.Length];
                for (var i = 0; i < columns.Length; i++)
                {
                    values[i] = feature[columns[i]];
                }

                result.Add(values);
            }
            return result;
        }

        internal static double[] ComputeErrors(double[] testPredictions, double[] exampleOutputs)
        {
            return testPredictions.Select((t, i) => t - exampleOutputs[i]).ToArray();
        }

        internal static Tuple<List<double[]>, double[]> Get_Numpy_Data(List<string[]> dataSframe, IReadOnlyList<int> features, int output)
        {
            var featuresMatrix = new List<double[]>();
            var outputArray = new List<double>();
            foreach(var data in dataSframe)
            {
                var featureData = new double[features.Count + 1];
                //w0*1
                featureData[0] = 1;
                for(var i = 0; i < features.Count; i++)
                {
                    featureData[i + 1] = double.Parse(data[features[i]]);
                }
                featuresMatrix.Add(featureData);
                outputArray.Add(double.Parse(data[output]));
            }

            return new Tuple<List<double[]>, double[]>(featuresMatrix, outputArray.ToArray());
        }

        internal static double[] PredictionOutput(List<double[]> features, double[] weights)
        {
            return Numpy.Dot(features, weights);
        }
        internal static double FeatureDerivative(List<double[]> features, double[] errofs)
        {
            return Numpy.Dot(errofs, features).Sum(value => 2 * value);
        }

        internal static double ComputeRss(double y, double prediction)
        {
            return (y - prediction) * (y - prediction);
        }
        internal static double ComputeRss(List<string[]> y,int output, double[] predictions)
        {
            double rss = 0;
            for (var i = 0; i< y.Count; i++)
            {
                rss += ComputeRss(double.Parse(y[i][output]), predictions[i]);
            }
            return rss;
        }
    }
}
