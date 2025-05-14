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

        public void VerificaVincitore()
        {
            int[] mossaBot = Bot.FaiMossa();
            if (Giocatore.CampoPlayer[mossaBot[0], mossaBot[1]] == StatoCampo.NAVE)
            {
                Giocatore.CampoPlayer[mossaBot[0], mossaBot[1]] = StatoCampo.NAVE_COLPITA;
                Bot.Contatore--;
                if()
                Bot.ultimaMossa = new int[] { mossaBot[0], mossaBot[1], 0 };
            }
            else
            {
                if (Bot.ultimaMossa != null)
                {
                    if (Bot.ultimaMossa[2] == 3) Bot.ultimaMossa = null;
                    else Bot.ultimaMossa[2]++;
                }
            }
        }
    }
}
