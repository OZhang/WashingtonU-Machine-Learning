using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.SimpleLinearRegression
{
    class SimpleLinearRegression
    {
        internal static double InverseRegressionPredictions(double price,
            double intercept, double slope)
        {
            return (price - intercept) / slope;
        }
        internal static double GetResidualSumOfSquares(DataTable data, string feature,
            string output, double intercept, double slope)
        {
            var featureColumn = data.Columns[feature];
            var outputColumn = data.Columns[output];
            var sumOfRss = 0.0;
            for (var i = 0; i < data.Rows.Count; i++)
            {
                var prediction = intercept + 
                    slope * double.Parse(data.Rows[i][feature].ToString());
                var residual = prediction - double.Parse(data.Rows[i][outputColumn].ToString());
                sumOfRss += residual * residual;
            }

            return sumOfRss;
        }

        internal static double GetRegressionPredictions(double squarefeet, double intercept, double slope)
        {
            return intercept + squarefeet * slope;
        }

        internal static Tuple<double, double> LinearRegression(
            DataTable data, string feature, string output)
        {
            var featureColumn = data.Columns[feature];
            var outputColumn = data.Columns[output];
            var numerator =
                Mean(data, (datarow) =>
                {
                    return double.Parse(datarow[featureColumn].ToString()) * double.Parse(datarow[outputColumn].ToString());
                }) -
                Mean(data, (datarow) =>
                {
                    return double.Parse(datarow[featureColumn].ToString());
                }) *
                Mean(data, (datarow) =>
                {
                    return double.Parse(datarow[outputColumn].ToString());
                });
            var denominator =
                Mean(data, (datarow) =>
                {
                    var featureValue = double.Parse(datarow[featureColumn].ToString());
                    return featureValue * featureValue;
                }) -
                Mean(data, (datarow) =>
                {
                    return double.Parse(datarow[featureColumn].ToString());
                }) *
                Mean(data, (datarow) =>
                {
                    return double.Parse(datarow[featureColumn].ToString());
                });
            var slope = numerator / denominator;
            var intercept =
                Mean(data, (datarow) =>
                {
                    return double.Parse(datarow[outputColumn].ToString());
                }) - slope *
                Mean(data, (datarow) =>
                {
                    return double.Parse(datarow[featureColumn].ToString());
                });
            return new Tuple<double, double>(slope, intercept);
        }

        private static double Mean(DataTable data, Func<DataRow, double> myMethodName)
        {
            var sum = 0.0;
            for (var i = 0; i < data.Rows.Count; i++)
            {
                sum+= myMethodName(data.Rows[i]);
            }

            return sum / data.Rows.Count;
        }

    }
}
