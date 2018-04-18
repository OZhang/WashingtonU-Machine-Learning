using MachineLearning.PolynomialRegression;
using MachineLearning.SimpleLinearRegression;
using System;
using System.Collections.Generic;

namespace MachineLearning
{
    class Program
    {

        static void Main(string[] args)
        {
            //Console.WriteLine("Linear Gradient Descent");
            //LinearGradientDescentRunner.Run();

            //MachineLearning.PolynomialRegression.PolynomialRegressionRunner.Run();

            //SimpleLinearRegressionRunner.Run();

            Boosting.Boosting.Run();

            Console.ReadLine();
        }


    }
}
