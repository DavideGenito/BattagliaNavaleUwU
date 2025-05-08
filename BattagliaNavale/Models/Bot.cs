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
        public Tentativo FaiMossaBot(IGenerator x,IGenerator y)
        {
            Tentativo tentativoDaRitornare= new Tentativo();
            x.GeneraMossaX(CampoBot.GetLength(1));
            y.GeneraMossaY(CampoBot.GetLength(0));
            x = int.Parse(GeneraMossaX(CampoBot.GetLength(1)));
            if (CampoBot[x,y]==StatoCampo.ACQUA)
            {

            }

        }
    }
}
