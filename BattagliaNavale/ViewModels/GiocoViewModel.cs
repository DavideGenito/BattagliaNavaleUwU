<<<<<<< HEAD
﻿using BattagliaNavale.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
=======
﻿using CommunityToolkit.Mvvm.ComponentModel;
>>>>>>> origin/filomena
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.ViewModels
{
<<<<<<< HEAD
    public partial class GiocoViewModel : ObservableObject
    {
        public ObservableCollection<ObservableCollection<StatoCampo>> CampoBot { get; set; }
        public ObservableCollection<ObservableCollection<StatoCampo>> CampoGiocatore { get; set; }

        private GameManager gameManager;

        [ObservableProperty]
        private int? selezioneX;

        [ObservableProperty]
        private int? selezioneY;

        [ObservableProperty]
        private string messaggioRisultato;

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
        private void SelezionaCella((int x, int y) cella)
        {
            SelezioneX = cella.x;
            SelezioneY = cella.y;
        }

        [RelayCommand]
        private void ConfermaColpo()
        {
            if (SelezioneX == null || SelezioneY == null) return;

            var risultato = gameManager.VerificaVincitore(SelezioneX.Value, SelezioneY.Value);
            AggiornaGriglie();

            switch (risultato)
            {
                case Risultato.VINTO_BOT:
                    MessaggioRisultato = "Hai perso!";
                    break;
                case Risultato.VINTO_PLAYER:
                    MessaggioRisultato = "Hai vinto!";
                    break;
                default:
                    MessaggioRisultato = "Colpo effettuato!";
                    break;
            }

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
=======
    public partial class GiocoViewModel:ObservableObject
    {

>>>>>>> origin/filomena
    }
}
