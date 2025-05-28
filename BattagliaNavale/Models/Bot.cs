
namespace BattagliaNavale.Models
{
    public class Bot
    {
        public int Contatore { get; set; }
        public IGenerator Generator;
        public StatoCampo[,] Campo { get; private set; }

        public int[]? ultimaMossa = null;

        public List<Tuple<int, int, bool>> BarchePosizione { get; private set; } = new List<Tuple<int, int, bool>> { };


        public Bot(StatoCampo[,] campo, IGenerator? generator = null)
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

            PosizionaBarca(2);
            PosizionaBarca(3);
            PosizionaBarca(3);
            PosizionaBarca(4);
        }

        public int[] FaiMossa()
        {
            if (ultimaMossa != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    int direzione = (ultimaMossa[2] + i) % 4;
                    int newX = ultimaMossa[0];
                    int newY = ultimaMossa[1];

                    switch (direzione)
                    {
                        case 0:
                            newY++;
                            break;
                        case 1: 
                            newX++;
                            break;
                        case 2: 
                            newY--;
                            break;
                        case 3: 
                            newX--;
                            break;
                    }

                    if (newX >= 0 && newX < Campo.GetLength(0) &&
                        newY >= 0 && newY < Campo.GetLength(1) &&
                        Campo[newX, newY] != StatoCampo.NAVE_COLPITA &&
                        Campo[newX, newY] != StatoCampo.ACQUA_COLPITA)
                    {
                        ultimaMossa[2] = direzione;
                        return new int[] { newX, newY };
                    }
                }

                ultimaMossa = null;
            }

            int maxAttempts = 100;
            int attempts = 0;
            while (attempts < maxAttempts)
            {
                int x = Generator.GeneraMossaX(Campo.GetLength(0));
                int y = Generator.GeneraMossaY(Campo.GetLength(1));

                if (Campo[x, y] != StatoCampo.NAVE_COLPITA &&
                    Campo[x, y] != StatoCampo.ACQUA_COLPITA)
                {
                    return new int[] { x, y };
                }
                attempts++;
            }

            for (int i = 0; i < Campo.GetLength(0); i++)
            {
                for (int j = 0; j < Campo.GetLength(1); j++)
                {
                    if (Campo[i, j] != StatoCampo.NAVE_COLPITA &&
                        Campo[i, j] != StatoCampo.ACQUA_COLPITA)
                    {
                        return new int[] { i, j };
                    }
                }
            }

            return new int[] { 0, 0 };
        }

        private void PosizionaBarca(int lunghezza)
        {
            bool errore;
            do
            {
                errore = false;
                int barcaX = Generator.GeneraMossaX(Campo.GetLength(0));
                int barcaY = Generator.GeneraMossaY(Campo.GetLength(1));
                bool orizzontale = Generator.GeneraOrientamentoBarca();

                if (orizzontale)
                {
                    if (barcaX + lunghezza > Campo.GetLength(0)) { errore = true; continue; }
                }
                else
                {
                    if (barcaY + lunghezza > Campo.GetLength(1)) { errore = true; continue; }
                }

                for (int i = 0; i < lunghezza; i++)
                {
                    int x = orizzontale ? barcaX + i : barcaX;
                    int y = orizzontale ? barcaY : barcaY + i;
                    if (Campo[x, y] == StatoCampo.NAVE)
                    {
                        errore = true;
                        break;
                    }
                }

                if (!errore)
                {
                    for (int i = 0; i < lunghezza; i++)
                    {
                        int x = orizzontale ? barcaX + i : barcaX;
                        int y = orizzontale ? barcaY : barcaY + i;
                        Campo[x, y] = StatoCampo.NAVE;
                    }

                    BarchePosizione.Add(Tuple.Create(barcaX, barcaY, !orizzontale));
                }
            } while (errore);
        }
    }
}