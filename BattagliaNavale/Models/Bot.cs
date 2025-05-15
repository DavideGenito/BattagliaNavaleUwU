namespace BattagliaNavale.Models
{
    public class Bot
    {
        public int Contatore { get; set; }
        public IGenerator Generator;
        public StatoCampo[,] Campo { get; private set; }
        public int[]? ultimaMossa = null;
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
                while (true)
                {
                    int orientamento = ultimaMossa[2];
                    if (orientamento == 0 && ultimaMossa[1] + 1 < Campo.GetLength(1)
                        || orientamento == 2 && ultimaMossa[1] - 1 >= 0
                        || orientamento == 1 && ultimaMossa[0] + 1 < Campo.GetLength(0)
                        || orientamento == 3 && ultimaMossa[0] - 1 >= 0)
                    {
                        switch (orientamento)
                        {
                            case 0:
                                return new int[] { ultimaMossa[0], ultimaMossa[1] + 1 };

                            case 1:
                                return new int[] { ultimaMossa[0] + 1, ultimaMossa[1] };

                            case 2:
                                return new int[] { ultimaMossa[0], ultimaMossa[1] - 1 };

                            case 3:
                                return new int[] { ultimaMossa[0] - 1, ultimaMossa[1] };

                        }
                    }
                    ultimaMossa[2]++;
                    if (ultimaMossa[2] > 3) ultimaMossa = null;
                }
            }
            else
            {
                int x = Generator.GeneraMossaX(Campo.GetLength(0));
                int y = Generator.GeneraMossaY(Campo.GetLength(1));

                return new int[] { x, y };
            }    
        }

        private void PosizionaBarca(int grand)
        {
            bool errore = true;
            while (errore)
            {
                errore = false;
                int barcaX = Generator.GeneraMossaX(Campo.GetLength(0));
                int barcaY = Generator.GeneraMossaY(Campo.GetLength(1));
                bool orrizontale = Generator.GeneraOrientamentoBarca();

                if (barcaX + grand < Campo.GetLength(0) && barcaY + grand < Campo.GetLength(1))
                {
                    for (int i = 0; i < grand; i++)
                    {
                        if (orrizontale)
                        {
                            if (Campo[barcaX + i, barcaY] == StatoCampo.NAVE)
                            {
                                errore = true;
                                break;
                            }
                            Campo[barcaX + i, barcaY] = StatoCampo.NAVE;
                        }
                        else
                        {
                            if (Campo[barcaX, barcaY + i] == StatoCampo.NAVE)
                            {
                                errore = true;
                                break;
                            }
                            Campo[barcaX, barcaY + i] = StatoCampo.NAVE;
                        }
                    }
                }
            }
        }
    }
}
