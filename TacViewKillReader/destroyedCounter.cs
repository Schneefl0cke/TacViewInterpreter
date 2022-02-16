using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacViewKillReader
{
    public class destroyedCounter
    {
        public int counter;
        public string name;
        public string type;
        public string country;

        public destroyedCounter(string name, string type, string country, int counter = 1)
        {
            this.name = name;
            this.type = type;
            this.country = country;
            this.counter = counter;
        }
    }
}
