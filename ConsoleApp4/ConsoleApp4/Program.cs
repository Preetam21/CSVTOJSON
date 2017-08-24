
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    public class Jsonthird
    {
        public string year { get; set; }
        public string IndicatorName { get; set; }
        public string Countrycode { get; set; }
        public string countryname { get; set; }
        public double value { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader streamReader = new StreamReader(new FileStream(@"C:\Users\Training\Downloads\Indicators.csv", FileMode.Open, FileAccess.Read));
            StreamWriter thirdJson = new StreamWriter(new FileStream(@"C:\Users\Training\Desktop\content3.json", FileMode.OpenOrCreate, FileAccess.Write));
            string[] countrycode = { "AFG", "ARM", "AZE", "BHR", "BGD", "BTN", "BRN", "KHM", "CHN", "CXR", "CCK", "IOT", "GEO", "HKG", "IND", "IDN", "IRN", "IRQ", "ISR", "JPN", "JOR", "KAZ", "KWT", "KGZ", "LAO", "LBN", "MAC", "MYS", "MDV", "MNG", "MMR", "NPL", "PRK", "OMN", "PAK", "PHL", "QAT", "SAU", "SGP", "KOR", "LKA", "SYR", "TWN", "TJK", "THA", "TUR", "TKM", "ARE", "UZB", "VNM"};
            List<Jsonthird> stackedbar = new List<Jsonthird>();
            List<Jsonthird> sub = new List<Jsonthird>();
            string[] data = streamReader.ReadLine().Split(',');
            while (!streamReader.EndOfStream)
            {
                string[] data1 = streamReader.ReadLine().Split(',');
                for (int i = 0; i < countrycode.Length; i++)
                {
                    if ((countrycode[i] == data1[1]) && (data1[2] == "Urban population" || data1[2] == "Rural population"))
                    {
                        double temp;
                        double.TryParse(data1[5], out temp);
                        stackedbar.Add(new Jsonthird() { year = data1[4], IndicatorName = data1[2], value = temp, Countrycode = data1[1], countryname = data1[0] });
                    }
                }
            }var Data = from m in stackedbar group new { m.value, m.IndicatorName } by m.countryname into NewG from n in (from m in NewG group new { m.value } by m.IndicatorName into xyz select new { xyz.Key, sum = xyz.Sum(o => o.value) }) group n by NewG.Key;
            thirdJson.WriteLine("[" + "\n");
            foreach (var i in Data)
            {
               thirdJson.Write("{" + "\"" + "CountryCode" + "\"" + ":" + "\"" + i.Key + "\"" + "" + "," + "\n");
                foreach (var j in i)
                {
                    var data1 = j.Key == "Urban population" ? "\"" + "Urban" + "\"" + ":" + j.sum : "\"" + "Rural" + "\"" + ":" + j.sum + ",";
                    thirdJson.WriteLine(data1);
                }
                var res = i.Key == "Vietnam" ? "}" : "},";
                thirdJson.WriteLine(res);
            }
            thirdJson.WriteLine("]");
            thirdJson.Flush();
        }
    }
}


