using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.PolynomialRegression
{
    class PolynomialRegressionRunner
    {
        private const string Sales_Data = "kc_house_data.csv";
        private const string set_1_data = "wk3_kc_house_set_1_data.csv";
        private const string set_2_data = "wk3_kc_house_set_2_data.csv";
        private const string set_3_data = "wk3_kc_house_set_3_data.csv";
        private const string set_4_data = "wk3_kc_house_set_4_data.csv";
        private const string test_data = "wk3_kc_house_test_data.csv";
        private const string valid_data = "wk3_kc_house_valid_data.csv";
        private static DataSet DataSet { get; set; }
        private static DataTable Sales { get { return DataSet.Tables[Sales_Data]; } }
        private static DataTable Set1 { get { return DataSet.Tables[set_1_data]; } }
        private static DataTable Set2 { get { return DataSet.Tables[set_2_data]; } }
        private static DataTable Set3 { get { return DataSet.Tables[set_3_data]; } }
        private static DataTable Set4 { get { return DataSet.Tables[set_4_data]; } }
        private static DataTable Test { get { return DataSet.Tables[test_data]; } }
        private static DataTable Valid { get { return DataSet.Tables[valid_data]; } }

        internal static void Run()
        {
            LoadData();
            //var result = SimpleLinearRegression.LinearRegression(Sales, "sqft_living", "price");

        }

        private static void LoadData()
        {
            DataSet = CsvReader.Read(@"C:\machineLearning\regssion\polynomial",
                Sales_Data, set_1_data, set_2_data,
                set_3_data, set_4_data,
                test_data, valid_data);
        }
    }
}
