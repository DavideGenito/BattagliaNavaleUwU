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
        public Tuple<Risultato, int[]> VerificaVincitore(int playerX, int playerY)
        {
            bool mossaGiusta = false;
            int[] mossaBot = {};
            while (!mossaGiusta)
            {
                mossaBot = Bot.FaiMossa();
                if (Giocatore.Campo[mossaBot[0], mossaBot[1]] == StatoCampo.NAVE)
                {
                    mossaGiusta = true;
                    Giocatore.Campo[mossaBot[0], mossaBot[1]] = StatoCampo.NAVE_COLPITA;
                    Bot.Contatore--;
                    if (Bot.Contatore == 0) return Tuple.Create(Risultato.VINTO_BOT, mossaBot);
                    Bot.ultimaMossa = new int[] { mossaBot[0], mossaBot[1], 0 };
                }
                else if (Giocatore.Campo[mossaBot[0], mossaBot[1]] == StatoCampo.ACQUA)
                {
                    mossaGiusta = true;
                    Giocatore.Campo[mossaBot[0], mossaBot[1]] = StatoCampo.ACQUA_COLPITA;
                    if (Bot.ultimaMossa != null)
                    {
                        if (Bot.ultimaMossa[2] == 3) Bot.ultimaMossa = null;
                        else Bot.ultimaMossa[2]++;
                    }
                }
            }

            int[] mossaPlayer = Giocatore.FaiMossa(playerX, playerY);

            if (Bot.Campo[mossaPlayer[0], mossaPlayer[1]] == StatoCampo.NAVE)
            {
                Bot.Campo[mossaPlayer[0], mossaPlayer[1]] = StatoCampo.NAVE_COLPITA;
                Giocatore.Contatore--;
                if(Giocatore.Contatore == 0) return Tuple.Create(Risultato.VINTO_PLAYER, mossaBot);
            }
            else if (Bot.Campo[mossaPlayer[0], mossaPlayer[1]] == StatoCampo.ACQUA)
            {
                Bot.Campo[mossaPlayer[0], mossaPlayer[1]] = StatoCampo.ACQUA_COLPITA;
            }

            return Tuple.Create(Risultato.SOSPESO, mossaBot);
        }
    }
}
