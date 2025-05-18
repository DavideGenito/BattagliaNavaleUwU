using BattagliaNavale.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BattagliaNavale.ViewModels
{
    public partial class GiocoViewModel : ObservableObject
    {
        public ObservableCollection<ObservableCollection<StatoCampo>> CampoBot { get; private set; }
        public ObservableCollection<ObservableCollection<StatoCampo>> CampoGiocatore { get; private set; }

        public ObservableCollection<(int x, int y, bool colpito)> ColpiPlayer { get; } = new();
        public ObservableCollection<(int x, int y, bool colpito)> ColpiBot { get; } = new();

        private GameManager gameManager;

        [ObservableProperty]
        private int? selezioneX;

        [ObservableProperty]
        private int? selezioneY;

        [ObservableProperty]
        private Risultato messaggioRisultato;

        public GiocoViewModel(StatoCampo[,] campoGiocatore)
        {
            var player = new Player(campoGiocatore);
            var bot = new Bot(new StatoCampo[10, 10]);
            gameManager = new GameManager(player, bot);

            CampoBot = ConvertiCampo(bot.Campo);
            CampoGiocatore = ConvertiCampo(player.Campo);
        }

        private ObservableCollection<ObservableCollection<StatoCampo>> ConvertiCampo(StatoCampo[,] campo)
        {
            var grid = new ObservableCollection<ObservableCollection<StatoCampo>>();
            for (int i = 0; i < campo.GetLength(0); i++)
            {
                var row = new ObservableCollection<StatoCampo>();
                for (int j = 0; j < campo.GetLength(1); j++)
                    row.Add(campo[i, j]);
                grid.Add(row);
            }
            return grid;
        }

        [RelayCommand]
        public void SelezionaCella(Tuple<int, int> cella)
        {
            SelezioneX = cella.Item1;
            SelezioneY = cella.Item2;
        }

        [RelayCommand]
        private void ConfermaColpo()
        {
            // Verifica se la cella è già stata colpita
            foreach (var colpo in ColpiPlayer)
            {
                if (colpo.x == SelezioneX.Value && colpo.y == SelezioneY.Value)
                {
                    return;
                }
            }

            var risultato = gameManager.VerificaVincitore(SelezioneX.Value, SelezioneY.Value);

            var statoPlayer = gameManager.Bot.Campo[SelezioneX.Value, SelezioneY.Value];
            bool colpitoPlayer = statoPlayer == StatoCampo.NAVE_COLPITA;
            ColpiPlayer.Add((SelezioneX.Value, SelezioneY.Value, colpitoPlayer));

            var mossaBot = risultato.Item2;
            var statoBot = gameManager.Giocatore.Campo[mossaBot[0], mossaBot[1]];
            bool colpitoBot = statoBot == StatoCampo.NAVE_COLPITA;
            ColpiBot.Add((mossaBot[0], mossaBot[1], colpitoBot));

            MessaggioRisultato = risultato.Item1;
            AggiornaGriglie();

            OnPropertyChanged(nameof(ColpiPlayer));
            OnPropertyChanged(nameof(ColpiBot));

            SelezioneX = null;
            SelezioneY = null;
        }

        private void AggiornaGriglie()
        {
            CampoBot = ConvertiCampo(gameManager.Bot.Campo);
            CampoGiocatore = ConvertiCampo(gameManager.Giocatore.Campo);
            OnPropertyChanged(nameof(CampoBot));
            OnPropertyChanged(nameof(CampoGiocatore));
        }
    }
}