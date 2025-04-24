using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public class Bot
    {
        public StatoTentativo[,] Campo { get; private set; }
        public bool[,] CampoFeedback { get; private set; }
        public Bot(StatoTentativo[,] campo, bool[,] campoFeedback)
        {
            Campo = campo;
            CampoFeedback = campoFeedback;
        }
        public void Strategia()
        {
            throw new System.NotImplementedException();
        }
    }
}
