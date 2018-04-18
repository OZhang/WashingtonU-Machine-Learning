using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.SimpleLinearRegression
{
    class SimpleLinearRegressionRunner
    {
        private const string Sales_Data = "kc_house_data.csv";
        private const string test_data = "kc_house_test_data.csv";
        private const string train_data = "kc_house_train_data.csv";
        private static DataSet DataSet { get; set; }
        private static DataTable Sales { get { return DataSet.Tables[Sales_Data]; } }
        private static DataTable Test { get { return DataSet.Tables[test_data]; } }
        private static DataTable Train { get { return DataSet.Tables[train_data]; } }
        const string sqft_living = "sqft_living";
        const string bedrooms = "bedrooms";
        const string price = "price";

        internal static void Run()
        {
            LoadData();
            var model1 = RunSquarefeet();
            var model2 = RunBedrooms();

            var rss_model1 = SimpleLinearRegression.GetResidualSumOfSquares(Test, sqft_living, price, model1.Item2, model1.Item1);
            var rss_model2 = SimpleLinearRegression.GetResidualSumOfSquares(Test, bedrooms, price, model2.Item2, model2.Item1);
            Console.WriteLine($"The RSS of predicting Prices based on Square Feet is :{rss_model1}");
            Console.WriteLine($"The RSS of predicting Prices based on bedrooms is :{rss_model2}");
        }

        private static Tuple<double, double> RunSquarefeet()
        {
            var result = SimpleLinearRegression.LinearRegression(Train, sqft_living, price);
            var intercept = Math.Round(result.Item2, 2);
            var slope = Math.Round(result.Item1, 2);
            Console.WriteLine($"Intercept:{intercept}");
            Console.WriteLine($"Slope:{slope}");
            if (intercept != -47116.08)
                throw new Exception("Intercept wrong");
            if (slope != 281.96)
                throw new Exception("Slope wrong");

            var estimated_price = SimpleLinearRegression.GetRegressionPredictions(2650, result.Item2, result.Item1);
            Console.WriteLine($"The estimated price for a house with 2650 squarefeet is :{estimated_price}");
            if (Math.Round(estimated_price, 2) != 700074.85)
                throw new Exception("estimated_price wrong");

            var rss = SimpleLinearRegression.GetResidualSumOfSquares(
                Train, sqft_living, price, result.Item2, result.Item1);
            Console.WriteLine($"The RSS of predicting Prices based on Square Feet is :{rss}");
            if (Math.Round(rss / 1e15, 5) != 1.20192)
                throw new Exception("RSS wrong");

            var squarefeet = SimpleLinearRegression.InverseRegressionPredictions(800000.0, result.Item2, result.Item1);
            Console.WriteLine($"The estimated squarefeet for a house worth 800,000 is {squarefeet,0:00}");
            if (Math.Round(squarefeet, 0) != 3004)
                throw new Exception("estimated squarefeet wrong");
            return result;
        }

        private static Tuple<double,double> RunBedrooms()
        {
            var result = SimpleLinearRegression.LinearRegression(Train, bedrooms, price);
            var intercept = Math.Round(result.Item2, 2);
            var slope = Math.Round(result.Item1, 2);
            Console.WriteLine($"Intercept:{intercept}");
            Console.WriteLine($"Slope:{slope}");
            //if (intercept != 7388.31)
            //    throw new Exception("Intercept wrong");
            //if (slope != 157886.93)
            //    throw new Exception("Slope wrong");
            return result;
        }

        private static void LoadData()
        {
            DataSet = CsvReader.Read(@"C:\machineLearning\regssion\SimpleLinearRegression",
                Sales_Data, 
                test_data, train_data);
        }
    }
}
