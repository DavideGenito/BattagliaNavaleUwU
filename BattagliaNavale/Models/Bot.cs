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
        public Bot(StatoCampo[,] campo)
        {
            Campo = campo;
        }
        public Tentativo FaiMossa(BattagliaNavale.Models.IGenerator? generator = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
