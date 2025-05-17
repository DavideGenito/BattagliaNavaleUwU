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
<<<<<<< HEAD
        public int Contatore { get; set; }
        public IGenerator Generator { get; set; }
        public StatoCampo[,] Campo {  get; private set; }
        public Player(StatoCampo[,] campo, IGenerator? generator = null)
=======
       

        public StatoCampo[,] CampoPlayer {  get; private set; }
        public Player(StatoCampo[,] campo)
>>>>>>> origin/filomena
        {
            Campo = campo;
            Contatore = 12;

            if (generator != null)
            {
                Generator = generator;
            }
            else
            {
                Generator = new RandomGenerator();
            }
        }
<<<<<<< HEAD

        public int[] FaiMossa(int x, int y)
        {
            if (x > Campo.GetLength(0) || x < 0) throw new ArgumentOutOfRangeException("immetti una x valida");
            if (y > Campo.GetLength(1) || y < 0) throw new ArgumentOutOfRangeException("immetti una y valida");

            return new int[] { x, y };
=======
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
>>>>>>> origin/filomena
        }
    }
}
