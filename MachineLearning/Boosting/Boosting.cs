using System;
using System.Collections.Generic;
using System.Data;

namespace MachineLearning.Boosting
{
    public class Boosting
    {
        private const string test_data = "C2_W5_binary_features_test_data.csv";
        private const string train_data = "C2_W5_binary_features_train_data.csv";
        private static DataSet DataSet { get; set; }
        private static DataTable Test { get { return DataSet.Tables[test_data]; } }
        private static DataTable Train { get { return DataSet.Tables[train_data]; } }
        private List<string> features = new List<string>
        {
            "safe_loans",
            "grade_A",
            "grade_B",
            "grade_C",
            "grade_D",
            "grade_E",
            "grade_F",
            "grade_G",
            "term_36months",
            "term_60months",
            "home_ownership_MORTGAGE",
            "home_ownership_OTHER",
            "home_ownership_OWN",
            "home_ownership_RENT",
            "emp_length_1year",
            "emp_length_10years",
            "emp_length_2_years",
            "emp_length_3_years",
            "emp_length_4_years",
            "emp_length_5_years",
            "emp_length_6_years",
            "emp_length_7_years",
            "emp_length_8_years",
            "emp_length_9_years",
            "emp_length_1_year",
            "emp_length_na",
        };
        internal static void Run()
        {
            LoadData();

            if (!CheckPoint1()) return;

        }
        private static void LoadData()
        {
            DataSet = CsvReader.Read(@"C:\ML_Data",
                test_data, train_data);
        }
        public static Tuple<double,int> Intermediate_Node_Weighted_Mistakes(List<int> labels_In_Node, 
            List<double> data_Weights)
        {
            var total_weight_positive = 0d;
            var total_weight_negative = 0d;
            for(var i = 0; i< labels_In_Node.Count;i++)
                //(var label in labels_In_Node)
            {
                var label = labels_In_Node[i];

                if (label == 1)
                    total_weight_positive += data_Weights[i];
                else
                    total_weight_negative += data_Weights[i];
            }

            var weighted_mistakes_all_positive = total_weight_negative;
            var weighted_mistakes_all_negative = total_weight_positive;
            if (weighted_mistakes_all_positive < weighted_mistakes_all_negative)
                return new Tuple<double, int>(weighted_mistakes_all_positive, +1);
            else
                return new Tuple<double, int>(weighted_mistakes_all_negative, -1);
        }

        private static bool CheckPoint1()
        {
            var example_lables = new List<int> { -1, -1, 1, 1, 1 };
            var example_data_weights = new List<double>
            {
                1.0,2.0,0.5,1.0,1.0
            };
            if (Intermediate_Node_Weighted_Mistakes(example_lables,
                example_data_weights).Equals(new Tuple<double, int>(2.5, -1)))
            {
                Console.WriteLine("Check Point 1 Test passed!");
                return true;
            }
            else
            {
                Console.WriteLine("Check Point 1 Test failed...try again!");
                return false;
            }
        }

        public void best_splitting_feature(List<string[]> data, List<string> features, string target, 
            List<double> data_weights)
        {
            var best_feature = string.Empty;
            var best_error = 0d;
            for(var i =0;i< features.Count; i++)
            {
                var feature = features[i];
                var left_split_index = data.FindIndex(v => v[i] == 
                    (Convert.ToInt32(false)).ToString());
                var right_split_index = data.FindIndex(v => v[i] == 
                    (Convert.ToInt32(true)).ToString());


                //var left_data_weights = 


            }
    }
}
