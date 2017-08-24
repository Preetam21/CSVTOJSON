using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace ConsoleApp2
{class multiSeries
    {public string IndicatorName { get; set; }
     public string CountryCode { get; set; }
     public double value { get; set; }
     public string year { get; set; }}
    class Program
    {public static void CsvToJson()
        { StreamReader streamreader = new StreamReader(new FileStream(@"C:\Users\Training\Downloads\Indicators.csv", FileMode.Open, FileAccess.Read));
         StreamWriter streamWriter = new StreamWriter(new FileStream(@"C:\Users\Training\Desktop\content.json", FileMode.OpenOrCreate, FileAccess.Write));
         string[] headers = streamreader.ReadLine().Split(',');
         string line;
         List<multiSeries> multi = new List<multiSeries>();
         while ((line = streamreader.ReadLine()) != null)
            {string[] values = line.Split(',');
                if (values[1] == "IND" && (values[2] == "Urban population (% of total)" || values[2] == "Rural population (% of total population)"))
                {
                    double temp;
                    double.TryParse(values[5], out temp);
                    multi.Add(new multiSeries() { year = values[4], IndicatorName = values[2], value =temp });}}
                var data = from multiSeries in multi
                           group new { multiSeries.IndicatorName, multiSeries.value } by multiSeries.year into yeargroup
                           select yeargroup;
                streamWriter.Write("{\n"+"\"India\""+":"+"[");
                foreach (var v in data)
                {
                streamWriter.Write("{"+"\"year\":"+ "\""+ v.Key + "\",");
                 string temp = "";
                 foreach (var item in v)
                    {
                    temp = temp + "\t\"" + item.IndicatorName + "\":" + item.value + ",";}
                    temp = temp.Remove(temp.Length - 1);
                streamWriter.WriteLine(temp);
                var res = v.Key == "2014" ? "}" : "},";
                streamWriter.WriteLine(res);
                }
                streamWriter.Write("]}");
                streamWriter.Flush();}
        static void Main(string[] args)
        {CsvToJson();}}}