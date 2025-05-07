using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public class Player
    {
        public StatoCampo[,] CampoPlayer {  get; private set; }
        public Player(StatoCampo[,] campo)
        {
            CampoPlayer = campo;
        }

        public Tentativo FaiMossa(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }
}
