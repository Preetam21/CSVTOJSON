using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace ConsoleApp3
{
    class areachart//This class hold the properties 
    {
        public string IndicatorName { get; set; }
        public string CountryCode { get; set; }
        public double value { get; set; }
        public string year { get; set; }
    }
    class Program//This class have method, which contain logic for converting file from csv to json
    {
        public static void csvtojsonpart2()
        {
            StreamReader streamreader = new StreamReader(new FileStream(@"C:\Users\Training\Downloads\Indicators.csv", FileMode.Open, FileAccess.Read));
            StreamWriter streamWriter = new StreamWriter(new FileStream(@"C:\Users\Training\Desktop\contentpart2.json", FileMode.OpenOrCreate, FileAccess.Write));
            string[] headers = streamreader.ReadLine().Split(',');
            string line;
            List<areachart> area = new List<areachart>();//creating list of type areachart
            while ((line = streamreader.ReadLine()) != null)
            {
                string[] values = line.Split(',');//values hold the data separated by comma 
                if (values[1] == "IND" && (values[2] == "Urban population growth (annual %)"))
                {
                    double temp;
                    double.TryParse(values[5], out temp);
                    area.Add(new areachart() { year = values[4], IndicatorName = values[2], value = temp });
                }
            }
            var data = from multiSeries in area
            group new { multiSeries.IndicatorName, multiSeries.value } by multiSeries.year into yeargroup
            select yeargroup;//This query  is used for grouping the data by year
            streamWriter.Write("[");
            foreach (var variable in data)
            {
                streamWriter.Write("{" + "\"year\":" + "\"" + variable.Key + "\",");
                string temp = "";
                foreach (var item in variable)
                {
                    temp = temp + "\t\"" + item.IndicatorName + "\":" + item.value + ",";
                }
                temp = temp.Remove(temp.Length - 1);
                streamWriter.WriteLine(temp);
                var res = variable.Key == "2014" ? "}" : "},";
                streamWriter.WriteLine(res);
            }
            streamWriter.Write("]");
            streamWriter.Flush();}
        static void Main(string[] args)
        {csvtojsonpart2();}}}//calling CsvToJson
