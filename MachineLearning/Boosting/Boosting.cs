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
        private static string target ="safe_loans";

        private static List<string> features = new List<string>
        {
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
            if (!CheckPoint2()) return;

        }
        private static void LoadData()
        {
            DataSet = CsvReader.Read(@"..\..\..\ML_Data",
                test_data, train_data);
        }
        public static Tuple<double, int> Intermediate_Node_Weighted_Mistakes(List<string> labels_In_Node,
            List<double> data_Weights)
        {
            var total_weight_positive = 0d;
            var total_weight_negative = 0d;
            for (var i = 0; i < labels_In_Node.Count; i++)
            //(var label in labels_In_Node)
            {
                var label = Convert.ToInt32(labels_In_Node[i]);

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
            var example_lables = new List<string> { "-1", "-1", "1", "1", "1" };
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

        private static bool CheckPoint2()
        {
            var example_data_weights = init_weights(Train.Rows.Count, 1.5d);
            if (best_splitting_feature(Train, features, target, example_data_weights) == "term_36months")
            {
                Console.WriteLine("Check Point 2 Test passed!");
                return true;
            }
            else
            {
                Console.WriteLine("Check Point 2 Test failed...try again!");
                return false;
            }
        }

        private static List<double> init_weights(int count, double init_weight)
        {
            var weights = new List<double>();
            for(var i = 0; i < count; i++)
            {
                weights.Add(init_weight);
            }
            return weights;
        }

        public static string best_splitting_feature(DataTable data, List<string> features, string target,
            List<double> data_weights)
        {
            var best_feature = string.Empty;
            var best_error = double.MaxValue;

            for (var i = 0; i < features.Count; i++)
            {
                var left_target = new List<string>();
                var right_target = new List<string>();
                var left_weights = new List<double>();
                var right_weights = new List<double>();
                //var feature = features[i.
                for (var j = 0; j < data.Rows.Count; j++)
                {
                    if (data.Rows[j][i].ToString().Equals((Convert.ToInt32(false)).ToString()))
                    {
                        var row = data.Rows[j];
                        left_target.Add(row[target].ToString());
                        left_weights.Add(data_weights[j]);
                    }
                    else
                    {
                        var row = data.Rows[j];
                        right_target.Add(row[target].ToString());
                        right_weights.Add(data_weights[j]);
                    }
                }
                //Calculate the weight of mistakes for left and right sides
                var left_weighted_mistakes = Intermediate_Node_Weighted_Mistakes(left_target, left_weights);
                var right_weighted_mistakes = Intermediate_Node_Weighted_Mistakes(right_target, right_weights);
                //  Compute weighted error by computing
                //( [weight of mistakes (left)] + [weight of mistakes (right)] ) / [total weight of all data points]
                var error = (left_weighted_mistakes.Item1 + right_weighted_mistakes.Item1) / Sum(data_weights);

                //If this is the best error we have found so far, store the feature and the error
                if (error < best_error)
                {
                    best_feature = features[i];
                    best_error = error;
                }
            }
            return best_feature;
        }

        private static double Sum(List<double> data)
        {
            var total_weights = 0d;
            foreach (var d in data)
            {
                total_weights += d;
            }
            return total_weights;
        }
    }
}