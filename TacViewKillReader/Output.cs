using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacViewKillReader
{
    public class Output
    {
        public void ConvertToExcelFormat(List<destroyedCounter> destroyed, string savePath)
        {
            List<destroyedCounter> aircraft, tanks, sam, other;
            GetFilterLists(destroyed, out aircraft, out tanks, out sam, out other);
            var aircraftByCountry = FilterListByCountry(aircraft);
            var tanksByCountry = FilterListByCountry(tanks);
            var samByCountry = FilterListByCountry(sam);
            var otherByCountry = FilterListByCountry(other);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Casualties");
                WriteHeader(worksheet);
                int counter = 2;

                counter = MakeDataEntry(aircraftByCountry, worksheet, counter);
                counter++;

                counter = MakeDataEntry(tanksByCountry, worksheet, counter);
                counter++;

                counter = MakeDataEntry(samByCountry, worksheet, counter);
                counter++;

                counter = MakeDataEntry(otherByCountry, worksheet, counter);

                workbook.SaveAs(savePath + ".xlsx"); //savePath
            }
        }

        private static void WriteHeader(IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = "Name";
            worksheet.Cell("B1").Value = "Casualties";
            worksheet.Cell("C1").Value = "Country";
            worksheet.Cell("D1").Value = "Type";
        }

        private static int MakeDataEntry(List<List<destroyedCounter>> typeList, IXLWorksheet worksheet, int counter)
        {
            for (int i = 0; i < typeList.Count; i++)
            {
                int casualties = 0;
                foreach (var entry in typeList[i])
                {
                    worksheet.Cell("A" + counter).Value = entry.name;
                    worksheet.Cell("B" + counter).Value = entry.counter;
                    worksheet.Cell("C" + counter).Value = entry.country;
                    worksheet.Cell("D" + counter).Value = entry.type;
                    counter++;
                    casualties += entry.counter;
                }
                worksheet.Cell("A" + counter).Value = "Gesamt";
                worksheet.Cell("B" + counter).Value = casualties;
                counter++;
                counter++;
            }

            return counter;
        }


        public void CreateFile(List<destroyedCounter> destroyed, string filePath)
        {
            var path = filePath;
            string text = "";

            List<destroyedCounter> aircraft, tanks, sam, other;
            GetFilterLists(destroyed, out aircraft, out tanks, out sam, out other);
            var aircraftByCountries = FilterListByCountry(aircraft);
            var tanksByCountries = FilterListByCountry(tanks);
            var samByCountries = FilterListByCountry(sam);
            var otherByCountries = FilterListByCountry(other);

            foreach (var item in aircraftByCountries)
            {
                foreach (var entry in item)
                {
                    text += entry.name + "," + entry.counter + "," + entry.type + "," + entry.country + ";\n";
                }
            }

            foreach (var item in tanksByCountries)
            {
                foreach (var entry in item)
                {
                    text += entry.name + "," + entry.counter + "," + entry.type + "," + entry.country + ";\n";
                }
            }

            foreach (var item in samByCountries)
            {
                foreach (var entry in item)
                {
                    text += entry.name + "," + entry.counter + "," + entry.type + "," + entry.country + ";\n";
                }
            }

            foreach (var item in otherByCountries)
            {
                foreach (var entry in item)
                {
                    text += entry.name + "," + entry.counter + "," + entry.type + "," + entry.country + ";\n";
                }
            }
            

            File.WriteAllText(path, text);
        }

        private static void GetFilterLists(List<destroyedCounter> destroyed, out List<destroyedCounter> aircraft, out List<destroyedCounter> tanks, out List<destroyedCounter> sam, out List<destroyedCounter> other)
        {
            aircraft = new List<destroyedCounter>();
            tanks = new List<destroyedCounter>();
            sam = new List<destroyedCounter>();
            other = new List<destroyedCounter>();
            foreach (var entry in destroyed)
            {
                if (entry.type == "Aircraft")
                {
                    aircraft.Add(entry);
                }
                else if (entry.type == "Tank")
                {
                    tanks.Add(entry);
                }
                else if (entry.type == "SAM/AAA")
                {
                    sam.Add(entry);
                }
                else
                {
                    other.Add(entry);
                }
            }
        }

        private List<List<destroyedCounter>> FilterListByCountry(List<destroyedCounter> destroyed)
        {
            var listsWithStuff = new List<List<destroyedCounter>>();

            //kriege alle Länder
            var countries = new List<string>();

            foreach (var entry in destroyed)
            {
                var match = countries.FirstOrDefault(x => x == entry.country);
                if (match == null)
                {
                    countries.Add(entry.country);
                }
            }

            foreach (var country in countries)
            {
                var entries = destroyed.FindAll(x => x.country == country);
                listsWithStuff.Add(entries);
            }

            return listsWithStuff;
        }
    }
}
