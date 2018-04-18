using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace MachineLearning
{
    internal class CsvReader
    {
        public static List<string[]> Read(string fileName, bool hasHead = false)
        {
            var data = new List<string[]>();
            using(var reader = new StreamReader(fileName))
            {
                var readLine = string.Empty;
                if (hasHead)
                    readLine = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    readLine = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(readLine))
                    {
                        continue;
                    }
                    data.Add(readLine.Split(','));
                }
            }
            return data;
        }

        public static DataSet Read(string directoryName, params string[] fileNames)
        {
            OleDbConnection conn = new OleDbConnection
           ("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " +
             directoryName +
             "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"");
            DataSet ds = new DataSet("Temp");
            conn.Open();
            foreach(string fileName in fileNames)
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter
                       ("SELECT * FROM " + fileName, conn);
                adapter.Fill(ds, fileName);
            }

            conn.Close();
            return ds;
        }
    }


}
