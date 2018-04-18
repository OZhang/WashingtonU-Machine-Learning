using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearning.PolynomialRegression
{
    class PolynomialRegression
    {
        internal static void Polynomial_SFrame(ref DataTable data, string feature, int degree)
        {            
            if (degree <= 1) return;
            var featureColumn = data.Columns[feature];
            var power_1Column = data.Columns.Add("power_1");
            for(var i = 0; i < data.Rows.Count; i++)
            {
                data.Rows[i][power_1Column] = data.Rows[i][featureColumn];
            }

            for (var i = 2; i < degree + 1; i++)
            {
                data.Columns.Add($"power_{i}");
            }
        }
    }
}
