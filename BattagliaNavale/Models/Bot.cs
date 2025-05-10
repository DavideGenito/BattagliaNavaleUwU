using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public class Bot
    {
        public StatoCampo[,] ZonaDiAttacco {  get; private set; }
        public StatoCampo[,] CampoBot { get; private set; }
        public IGenerator Generator { get; private set; }
        public Bot(StatoCampo[,] campo, BattagliaNavale.Models.IGenerator? generator = null)
        {
            CampoBot = campo;
            if (generator == null)
            {
                Generator = new RandomGenerator();
            }
        }
        public Tentativo FaiMossaBot()
        {
            Tentativo tentativoDaRitornare= new Tentativo();
            int? ultimaX = null;
            int? ultimaY = null;
            int x = Generator.GeneraMossaX(CampoBot.GetLength(1));
            int y = Generator.GeneraMossaX(CampoBot.GetLength(0));           
            if (CampoBot[x,y]==StatoCampo.ACQUA)
            {
                tentativoDaRitornare=Tentativo.ACQUA;
            }
            else
            {
                if (CampoBot[x, y] == StatoCampo.NAVECOLPITA)
                {
                    tentativoDaRitornare = Tentativo.COLPITA;
                    ultimaX = x;
                    ultimaY = y;

                }                
                
            }
            return tentativoDaRitornare;

        }
    }
}
