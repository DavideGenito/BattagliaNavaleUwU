using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public class Bot
    {
        public StatoCampo[,] Campo { get; private set; }
        public IGenerator Generator { get; private set; }
        public Bot(StatoCampo[,] campo, BattagliaNavale.Models.IGenerator? generator = null)
        {
            Campo = campo;
            if (generator == null)
            {
                Generator = new RandomGenerator();
            }
        }
        public Tentativo FaiMossa(IGenerator tentativo)
        {
            

        }
    }
}
