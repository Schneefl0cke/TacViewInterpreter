﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacViewKillReader
{
    public class Kill
    {
        public string killerAircraft;
        public string killerPilot;
        public string killerCountry;

        public string destroyedType;
        public string destroyedName;
        public string destroyedCountry;

        public Kill(string killerAircraft, string killerPilot, string killerCountry, string destroyedType, string destroyedName, string destroyedCountry)
        {
            this.killerAircraft = killerAircraft;
            this.killerPilot = killerPilot;
            this.killerCountry = killerCountry;
                                    
            this.destroyedType = destroyedType;
            this.destroyedName = destroyedName;
            this.destroyedCountry = destroyedCountry;
        }
    }
}