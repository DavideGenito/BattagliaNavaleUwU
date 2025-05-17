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

<<<<<<< HEAD
        public Risultato VerificaVincitore(int playerX, int playerY)
        {
            bool mossaGiusta = false;
            while (!mossaGiusta)
            {
                int[] mossaBot = Bot.FaiMossa();
                if (Giocatore.Campo[mossaBot[0], mossaBot[1]] == StatoCampo.NAVE)
                {
                    mossaGiusta = true;
                    Giocatore.Campo[mossaBot[0], mossaBot[1]] = StatoCampo.NAVE_COLPITA;
                    Bot.Contatore--;
                    if (Bot.Contatore == 0) return Risultato.VINTO_BOT;
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
                if(Giocatore.Contatore == 0) return Risultato.VINTO_PLAYER;
            }
            else if (Bot.Campo[mossaPlayer[0], mossaPlayer[1]] == StatoCampo.ACQUA)
            {
                Bot.Campo[mossaPlayer[0], mossaPlayer[1]] = StatoCampo.ACQUA_COLPITA;
            }
        
            return Risultato.SOSPESO;
=======
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
>>>>>>> origin/filomena
        }
    }
}
