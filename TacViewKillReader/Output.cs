﻿using ClosedXML.Excel;
using System.Collections.Generic;
using System.Linq;

namespace TacViewKillReader
{
    public class Output
    {
        public void WriteExcelFile(List<destroyedCounter> killed, List<destroyedCounter> mia, string savePath)
        {
            using (var workbook = new XLWorkbook())
            {
                WriteKillsToExcel(killed, workbook, "KIA");
                WriteKillsToExcel(mia, workbook, "MIA and Accidents");

                workbook.SaveAs(savePath + ".xlsx"); //savePath
            }
        }

        public void WriteKillsToExcel(List<destroyedCounter> destroyed, XLWorkbook workbook, string worksheetName)
        {
            List<destroyedCounter> aircraft, tanks, sam, other;
            GetFilterLists(destroyed, out aircraft,  out tanks, out sam, out other);
            var aircraftByCountry = FilterListByCountry(aircraft);
            var tanksByCountry = FilterListByCountry(tanks);
            var samByCountry = FilterListByCountry(sam);
            var otherByCountry = FilterListByCountry(other);


            var worksheet = workbook.Worksheets.Add(worksheetName);
            WriteHeader(worksheet);
            int counter = 2;

            counter = MakeDataEntry(aircraftByCountry, worksheet, counter);
            counter++;

            counter = MakeDataEntry(tanksByCountry, worksheet, counter);
            counter++;

            counter = MakeDataEntry(samByCountry, worksheet, counter);
            counter++;

            counter = MakeDataEntry(otherByCountry, worksheet, counter);
        }

        private static void WriteHeader(IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = "Name";
            worksheet.Cell("A1").Style.Font.Bold = true;

            worksheet.Cell("B1").Value = "Losses";
            worksheet.Cell("B1").Style.Font.Bold = true;

            worksheet.Cell("C1").Value = "Country";
            worksheet.Cell("C1").Style.Font.Bold = true;

            worksheet.Cell("D1").Value = "Type";
            worksheet.Cell("D1").Style.Font.Bold = true;

            worksheet.Cell("E1").Value = "Player losses";
            worksheet.Cell("E1").Style.Font.Bold = true;

            worksheet.Cell("F1").Value = "Killed by players";
            worksheet.Cell("F1").Style.Font.Bold = true;
        }

        private static int MakeDataEntry(List<List<destroyedCounter>> typeList, IXLWorksheet worksheet, int counter)
        {
            for (int i = 0; i < typeList.Count; i++)
            {
                int casualties = 0;
                int playerCasualties = 0;
                int killedByPlayers = 0;

                foreach (var entry in typeList[i])
                {
                    worksheet.Cell("A" + counter).Value = entry.name;
                    worksheet.Cell("B" + counter).Value = entry.destroyedInMission;
                    worksheet.Cell("C" + counter).Value = entry.country;
                    worksheet.Cell("D" + counter).Value = entry.type;
                    worksheet.Cell("E" + counter).Value = entry.playerLosses;
                    worksheet.Cell("F" + counter).Value = entry.destroyedByPlayers;
                    counter++;
                    casualties += entry.destroyedInMission;
                    playerCasualties += entry.playerLosses;
                    killedByPlayers += entry.destroyedByPlayers;
                }

                worksheet.Cell("A" + counter).Value = "Gesamt";
                worksheet.Cell("A" + counter).Style.Font.Bold = true;

                worksheet.Cell("B" + counter).Value = casualties;
                worksheet.Cell("B" + counter).Style.Font.Bold = true;
                worksheet.Cell("E" + counter).Value = playerCasualties;
                worksheet.Cell("E" + counter).Style.Font.Bold = true;
                worksheet.Cell("F" + counter).Value = killedByPlayers;
                worksheet.Cell("F" + counter).Style.Font.Bold = true;

                counter++;
                counter++;
            }

            return counter;
        }


        //public void CreateFile(List<destroyedCounter> destroyed, string filePath)
        //{
        //    var path = filePath;
        //    File.WriteAllText(path, GetKillText(destroyed));
        //}

        //private string GetKillText (List<destroyedCounter> destroyed)
        //{
        //    string text = "";

        //    List<destroyedCounter> aircraft, tanks, sam, other;
        //    GetFilterLists(destroyed, out aircraft, out tanks, out sam, out other);
        //    var aircraftByCountries = FilterListByCountry(aircraft);
        //    var tanksByCountries = FilterListByCountry(tanks);
        //    var samByCountries = FilterListByCountry(sam);
        //    var otherByCountries = FilterListByCountry(other);

        //    foreach (var item in aircraftByCountries)
        //    {
        //        foreach (var entry in item)
        //        {
        //            text += entry.name + "," + entry.counter + "," + entry.type + "," + entry.country + ";\n";
        //        }
        //    }

        //    foreach (var item in tanksByCountries)
        //    {
        //        foreach (var entry in item)
        //        {
        //            text += entry.name + "," + entry.counter + "," + entry.type + "," + entry.country + ";\n";
        //        }
        //    }

        //    foreach (var item in samByCountries)
        //    {
        //        foreach (var entry in item)
        //        {
        //            text += entry.name + "," + entry.counter + "," + entry.type + "," + entry.country + ";\n";
        //        }
        //    }

        //    foreach (var item in otherByCountries)
        //    {
        //        foreach (var entry in item)
        //        {
        //            text += entry.name + "," + entry.counter + "," + entry.type + "," + entry.country + ";\n";
        //        }
        //    }

        //    return text;
        //}

        private static void GetFilterLists(List<destroyedCounter> destroyed, out List<destroyedCounter> aircraft, out List<destroyedCounter> tanks, out List<destroyedCounter> sam, out List<destroyedCounter> other)
        {
            aircraft = new List<destroyedCounter>();
            tanks = new List<destroyedCounter>();
            sam = new List<destroyedCounter>();
            other = new List<destroyedCounter>();
            foreach (var entry in destroyed)
            {
                if (entry.type == "Aircraft" || entry.type == "Helicopter")
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
