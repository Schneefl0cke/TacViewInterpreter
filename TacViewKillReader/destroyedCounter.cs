using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacViewKillReader
{
    /// <summary>
    /// for combining files
    /// </summary>
    public class destroyedCounter
    {
        public int destroyedInMission;
        public int destroyedByPlayers;
        public int playerLosses;
        public string name;
        public string type;
        public string country;

        public destroyedCounter(string name, string type, string country)
        {
            destroyedInMission = 1;
            destroyedByPlayers = 0;
            playerLosses = 0;
            this.name = name;
            this.type = type;
            this.country = country;
        }
    }
}
