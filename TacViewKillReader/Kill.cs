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

        public bool wasPlayer;
        public bool squadronkill;

        public Kill(string killerAircraft, string killerPilot, string killerCountry, string destroyedType, string destroyedName, string destroyedCountry, bool squadronkill, bool wasPlayer)
        {
            this.killerAircraft = killerAircraft;
            this.killerPilot = killerPilot;
            this.killerCountry = killerCountry;
                                    
            this.destroyedType = destroyedType;
            this.destroyedName = destroyedName;
            this.destroyedCountry = destroyedCountry;

            this.squadronkill = squadronkill;
            this.wasPlayer = wasPlayer;
        }
    }
}
