using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Google.Crypto.Tink.Signature;

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
            Tentativo tentativoDaRitornare = new Tentativo();
            if (CampoPlayer[x, y] == StatoCampo.ACQUA)
            {
                tentativoDaRitornare=Tentativo.ACQUA;
            }
            else
            {
                if (CampoPlayer[x, y] == StatoCampo.NAVE)
                {
                    tentativoDaRitornare = Tentativo.COLPITA;
                }
               
            }
            return tentativoDaRitornare;
        }
    }
}
