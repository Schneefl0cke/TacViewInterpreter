using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TacViewKillReader
{
    public class Analyzer
    {
        List<Kill> KillList = new List<Kill>();
        List<Kill> MiaList = new List<Kill>();

        public List<destroyedCounter> Killed = new List<destroyedCounter>();
        public List<destroyedCounter> Mia = new List<destroyedCounter>();


        //Analyzing of multiple files will be reworked!
        //public List<destroyedCounter> AnalyzeMultipleFiles(List<string> paths)
        //{
        //    var kills = CombineMissionResults_MultipleMissions(paths);
        //    var combinedKills = CombineKills_MultipleMissions(kills);

        //    return combinedKills;
        //}

        //public List<destroyedCounter> CombineMissionResults_MultipleMissions(List<string> paths)
        //{
        //    var kills = new List<destroyedCounter>();
        //    foreach (var path in paths)
        //    {
        //        var content = File.ReadAllText(path);
        //        var lines = content.Split(';');

        //        foreach (var line in lines)
        //        {
        //            var entries = line.Split(',');

        //            if (entries.Length == 4)
        //            {
        //                var dName = entries[0].Trim();
        //                var dCounter = Convert.ToInt32(entries[1].Trim());
        //                var dType = entries[2].Trim();
        //                var dCountry = entries[3].Trim();

        //                kills.Add(new destroyedCounter(dName, dType, dCountry, dCounter));
        //            }
        //        }
        //    }
        //    return kills;
        //}

        //public List<destroyedCounter> CombineKills_MultipleMissions(List<destroyedCounter> kills)
        //{
        //    var combinedKills = new List<destroyedCounter>();
        //    foreach (var kill in kills)
        //    {
        //        var destroyedEntry = combinedKills.FirstOrDefault(x => x.name == kill.name && x.country == kill.country);
        //        if (destroyedEntry == null)
        //        {
        //            combinedKills.Add(kill);
        //        }
        //        else
        //        {
        //            destroyedEntry.counter += kill.counter;
        //        }
        //    }
        //    return combinedKills;
        //}

        public void AnalyzeSingleMission(string path)
        {
            KillList = new List<Kill>();
            MiaList = new List<Kill>();

            using (XmlReader reader = XmlReader.Create(path))
            {
                ReadLosses(reader);
                FilterKills();
            }
        }

        public void ReadLosses(XmlReader reader)
        {
            reader.ReadToFollowing("Event");

            do
            {
                string killerAircraft = "";
                string killerPilot = "";
                string killerCountry = "";

                reader.MoveToFirstAttribute();

                //Primary
                reader.ReadToFollowing("Type");
                var destroyedType = reader.ReadElementContentAsString().Trim();
                reader.ReadToFollowing("Name");
                var destroyedName = reader.ReadElementContentAsString().Trim();
                reader.ReadToFollowing("Country");
                var destroyedCountry = reader.ReadElementContentAsString().Trim();

                reader.ReadToFollowing("Action");
                var data = reader.ReadElementContentAsString();
                if (data == "HasBeenDestroyed" && destroyedType != "Missile" && destroyedType != "Parachutist")
                {
                    reader.MoveToContent();
                    if (reader.LocalName == "SecondaryObject")
                    {
                        reader.ReadToFollowing("Name");
                        killerAircraft = reader.ReadElementContentAsString().Trim();
                        reader.ReadToFollowing("Pilot");
                        killerPilot = reader.ReadElementContentAsString().Trim();
                        reader.ReadToFollowing("Country");
                        killerCountry = reader.ReadElementContentAsString().Trim();

                        KillList.Add(new Kill(killerAircraft, killerPilot, killerCountry, destroyedType, destroyedName, destroyedCountry));
                    }
                    else
                    {
                        MiaList.Add(new Kill("", "", "", destroyedType, destroyedName, destroyedCountry));
                    }
                }
            }
            while (reader.ReadToFollowing("Event"));
        }

        public void FilterKills()
        {
            foreach (var kill in KillList)
            {
                var destroyedEntry = Killed.FirstOrDefault(x => x.name == kill.destroyedName && x.country == kill.destroyedCountry);

                if (destroyedEntry == null)
                {
                    Killed.Add(new destroyedCounter(kill.destroyedName, kill.destroyedType, kill.destroyedCountry));
                }
                else
                {
                    destroyedEntry.counter++;
                }
            }
        }

        public void FilterMia()
        {
            foreach (var mia in MiaList)
            {
                var destroyedEntry = Mia.FirstOrDefault(x => x.name == mia.destroyedName && x.country == mia.destroyedCountry);

                if (destroyedEntry == null)
                {
                    Mia.Add(new destroyedCounter(mia.destroyedName, mia.destroyedType, mia.destroyedCountry));
                }
                else
                {
                    destroyedEntry.counter++;
                }
            }
        }
    }
}
