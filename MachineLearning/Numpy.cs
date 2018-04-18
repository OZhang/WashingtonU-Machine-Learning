using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning
{
    class Numpy
    {
        public static double Sum(double[] outputs)
        {
            return outputs.Sum();
        }

        public static double[] Dot(List<double[]> matrix1, double[] matrix2)
        {
            var result = new List<double>();
            for(var i = 0; i< matrix1.Count; i++)
            {
                var prediction = 0.0;
                var features = matrix1[i];
                for(var j = 0; j < features.Length; j++)
                {
                    prediction += features[j] * matrix2[j];
                }

                result.Add(prediction);
            }

            return result.ToArray();
        }

        public static double[] Dot(double[] matrix1, List<double[]> matrix2)
        {
            var result = new List<double>();
            for(var i = 0; i < matrix2.Count; i++)
            {
                result.Add(matrix1[i] * matrix2[i][0]);
            }
            return result.ToArray();
        }
    }
}
