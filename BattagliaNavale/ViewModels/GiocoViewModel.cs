using BattagliaNavale.Models;
using BattagliaNavale.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace BattagliaNavale.ViewModels
{

    public partial class GiocoViewModel : ObservableObject
    {
        public ObservableCollection<ObservableCollection<StatoCampo>> CampoBot { get; set; }
        public ObservableCollection<ObservableCollection<StatoCampo>> CampoGiocatore { get; set; }

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
                for (int j = 0; j < campo.GetLength(1); j++) row.Add(campo[i, j]);
                grid.Add(row);
            }
            return grid;
        }

        [RelayCommand]
        public void SelezionaCella((int x, int y) cella)
        {
            SelezioneX = cella.x;
            SelezioneY = cella.y;
        }

        [RelayCommand]
        private void ConfermaColpo()
        {
            if (SelezioneX == null || SelezioneY == null) return;

            var stato = gameManager.Bot.Campo[SelezioneX.Value, SelezioneY.Value];
            bool colpito = stato == StatoCampo.NAVE ? true : false;

            ColpiPlayer.Add((SelezioneX.Value, SelezioneY.Value, colpito));

            var risultato = gameManager.VerificaVincitore(SelezioneX.Value, SelezioneY.Value);
            AggiornaGriglie();

            messaggioRisultato = risultato.Item1;

            var statoBot = gameManager.Giocatore.Campo[risultato.Item2[0], risultato.Item2[1]];
            bool colpitoBot = stato == StatoCampo.NAVE ? true : false;
            ColpiBot.Add((risultato.Item2[0], risultato.Item2[1], colpitoBot));

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
