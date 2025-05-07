using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public class GameManager
    {
        public Player Giocatore { get; private set; }
        public Bot Bot { get; private set; }

        public GameManager(Player giocatore, Bot bot)
        {
            Giocatore = giocatore;
            Bot = bot;
        }

        public void VerificaVincitore(Tentativo tentativoBot, Tentativo tentativoPlayer)
        {
            throw new System.NotImplementedException();
        }
    }
}
