using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.Models
{
    public class GameManager
    {
        public Player Player { get; private set; }
        public Bot Bot { get; private set; }

        public GameManager(Player giocatore, Bot bot)
        {
            Player = giocatore;
            Bot = bot;
        }

        public Tentativo VerificaVincitore()
        {
            int x=0; 
            int y=0;
            int i = 12;
            int contatoreBot = 12;
            int contatorePlayer = 12;
            while(i!=0)
            {
                if (Bot.FaiMossaBot() == Tentativo.COLPITA)
                {
                    contatoreBot--;
                }
                else
                {
                    if (Player.FaiMossa(x, y) == Tentativo.COLPITA)
                    {
                        contatorePlayer--;
                    }

                }
            }
            if (contatorePlayer < contatoreBot) return Tentativo.VINTO;
            return Tentativo.PERSO;            
        }
    }
}
