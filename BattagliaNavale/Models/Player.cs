using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public class Player
    {
        public int Contatore { get; set; }
        public IGenerator Generator { get; set; }
        public StatoCampo[,] Campo {  get; private set; }
        public Player(StatoCampo[,] campo, IGenerator? generator = null)
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
        public int[] FaiMossa(int x, int y)
        {
            if (x > Campo.GetLength(0) - 1 || x < 0) throw new ArgumentOutOfRangeException("immetti una x valida");
            if (y > Campo.GetLength(1) - 1 || y < 0) throw new ArgumentOutOfRangeException("immetti una y valida");

            return new int[] { x, y };
        }
    }
}
