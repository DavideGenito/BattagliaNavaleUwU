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
    public partial class PosizionaNaviViewModel : ObservableObject
    {
        private const int GrigliaDimensione = 10;
        private readonly INavigation _navigation;

        public ObservableCollection<ObservableCollection<StatoCampo>> Campo { get; set; }

        [ObservableProperty]
        private int naveCorrente = 0;

        public List<int> NaviDisponibili { get; } = new() { 2, 3, 3, 4 };

        // Posizioni e orientamenti temporanei delle navi
        private List<Tuple<int, int, bool>> NaviPosizionate = new()
        {
            new(0, 0, true),
            new(0, 1, true),
            new(0, 2, true),
            new(0, 3, true),
        };

        public int PrimaNaveRow => NaviPosizionate[0].Item1;
        public int PrimaNaveColumn => NaviPosizionate[0].Item2;

        [ObservableProperty]
        private int primaNaveRowSpan = 2;
        [ObservableProperty]
        private int primaNaveColumnSpan = 1;

        public double PrimaNaveRotation { get; set; } = 0;
        public int PrimaNaveTranslationX { get; set; } = 0;

        public int SecondaNaveRow => NaviPosizionate[1].Item1;
        public int SecondaNaveColumn => NaviPosizionate[1].Item2;
        public double SecondaNaveRotation { get; set; } = 0;
        public int SecondaNaveTranslationX { get; set; } = 0;

        [ObservableProperty]
        private int secondaNaveRowSpan = 3;
        [ObservableProperty]
        private int secondaNaveColumnSpan = 1;

        public int TerzaNaveRow => NaviPosizionate[2].Item1;
        public int TerzaNaveColumn => NaviPosizionate[2].Item2;
        public double TerzaNaveRotation { get; set; } = 0;
        public int TerzaNaveTranslationX { get; set; } = 0;

        [ObservableProperty]
        private int terzaNaveRowSpan = 3;
        [ObservableProperty]
        private int terzaNaveColumnSpan = 1;

        public int QuartaNaveRow => NaviPosizionate[3].Item1;
        public int QuartaNaveColumn => NaviPosizionate[3].Item2;
        public double QuartaNaveRotation { get; set; } = 0;
        public int QuartaNaveTranslationX { get; set; } = 0;

        [ObservableProperty]
        private int quartaNaveRowSpan = 4;
        [ObservableProperty]
        private int quartaNaveColumnSpan = 1;

        private StatoCampo[,] campoLogico;

        public PosizionaNaviViewModel(INavigation navigation)
        {
            campoLogico = new StatoCampo[GrigliaDimensione, GrigliaDimensione];
            Campo = new ObservableCollection<ObservableCollection<StatoCampo>>();

            for (int i = 0; i < GrigliaDimensione; i++)
            {
                Campo.Add(new ObservableCollection<StatoCampo>());
                for (int j = 0; j < GrigliaDimensione; j++)
                {
                    Campo[i].Add(StatoCampo.ACQUA);
                }
            }

            _navigation = navigation;
            AggiornaGrigliaTemporanea();
        }

        [RelayCommand]
        private void SelezionaNave(int index)
        {
            NaveCorrente = index;
            NotificaPosizioniNavi();
            AggiornaGrigliaTemporanea();
        }

        [RelayCommand]
        private void RuotaNave()
        {
            var (x, y, orientamento) = NaviPosizionate[NaveCorrente];

            if (PuoPosizionarsi(x, y, NaviDisponibili[NaveCorrente], !orientamento))
            {
                NaviPosizionate[NaveCorrente] = new Tuple<int, int, bool>(x, y, !orientamento);
                if(orientamento)
                {
                    switch (NaveCorrente)
                    {
                        case 0:
                            PrimaNaveRotation = 90;
                            PrimaNaveTranslationX = 80;
                            break;
                        case 1:
                            SecondaNaveRotation = 90;
                            SecondaNaveTranslationX = 120;
                            break;
                        case 2:
                            TerzaNaveRotation = 90;
                            TerzaNaveTranslationX = 120;
                            break;
                        case 3:
                            QuartaNaveRotation = 90;
                            QuartaNaveTranslationX = 160;
                            break;
                    }
                }
                else
                {
                    switch (NaveCorrente)
                    {
                        case 0:
                            PrimaNaveRotation = 0;
                            PrimaNaveTranslationX = 0;
                            break;
                        case 1:
                            SecondaNaveRotation = 0;
                            SecondaNaveTranslationX = 0;
                            break;
                        case 2:
                            TerzaNaveRotation = 0;
                            TerzaNaveTranslationX = 0;
                            break;
                        case 3:
                            QuartaNaveRotation = 0;
                            QuartaNaveTranslationX = 0;
                            break;
                    }
                }
                NotificaPosizioniNavi();
                AggiornaGrigliaTemporanea();
            }
        }

        [RelayCommand]
        private void SpostaNave(Point direzione)
        {
            var (x, y, orientamento) = NaviPosizionate[NaveCorrente];
            int nuovaX = x + (int)direzione.X;
            int nuovaY = y + (int)direzione.Y;

            if (PuoPosizionarsi(nuovaX, nuovaY, NaviDisponibili[NaveCorrente], orientamento))
            {
                NaviPosizionate[NaveCorrente] = new Tuple<int, int, bool>(nuovaX, nuovaY, orientamento);
                NotificaPosizioniNavi();
                AggiornaGrigliaTemporanea();
            }
        }

        [RelayCommand]
        private async Task ConfermaPosizionamentoAsync()
        {
            await _navigation.PushAsync(new Gioco(campoLogico));
        }

        private void AggiornaGrigliaTemporanea()
        {
            for (int i = 0; i < GrigliaDimensione; i++)
            {
                for (int j = 0; j < GrigliaDimensione; j++)
                {
                    Campo[i][j] = StatoCampo.ACQUA;
                    campoLogico[i, j] = StatoCampo.ACQUA;
                }
            }
                
            for(int i = 0; i < NaviPosizionate.Count; i++)
            {
                var (x, y, orientamento) = NaviPosizionate[i];
                int lunghezza = NaviDisponibili[i];
                for (int j = 0; j < lunghezza; j++)
                {
                    int posX = orientamento ? x + j : x;
                    int posY = orientamento ? y : y + j;
                    if (posX < GrigliaDimensione && posY < GrigliaDimensione)
                    {
                        Campo[posX][posY] = StatoCampo.NAVE;
                        campoLogico[posX, posY] = StatoCampo.NAVE;
                    }
                }
            }
        }

        private bool PuoPosizionarsi(int x, int y, int lunghezza, bool orientamento)
        {
            // Controllo che la nave stia dentro la griglia
            if (orientamento)
            {
                if (x < 0 || x + lunghezza > GrigliaDimensione || y < 0 || y >= GrigliaDimensione)
                    return false;
            }
            else
            {
                if (y < 0 || y + lunghezza > GrigliaDimensione || x < 0 || x >= GrigliaDimensione)
                    return false;
            }

            // Controllo sovrapposizione con altre navi (escludendo la nave corrente)
            for (int i = 0; i < lunghezza; i++)
            {
                int posX = orientamento ? x + i : x;
                int posY = orientamento ? y : y + i;

                // Trova l'indice della nave corrente
                for (int n = 0; n < NaviPosizionate.Count; n++)
                {
                    if (n == NaveCorrente) continue;
                    var (nx, ny, norientamento) = NaviPosizionate[n];
                    int nlunghezza = NaviDisponibili[n];
                    for (int j = 0; j < nlunghezza; j++)
                    {
                        int nposX = norientamento ? nx + j : nx;
                        int nposY = norientamento ? ny : ny + j;
                        if (posX == nposX && posY == nposY)
                            return false;
                    }
                }
            }

            return true;
        }



        private void NotificaPosizioniNavi()
        {
            OnPropertyChanged(nameof(PrimaNaveRow));
            OnPropertyChanged(nameof(PrimaNaveColumn));
            OnPropertyChanged(nameof(PrimaNaveRotation));
            OnPropertyChanged(nameof(PrimaNaveTranslationX));
            primaNaveRowSpan = NaviPosizionate[0].Item3 ? NaviDisponibili[0] : 1;
            primaNaveColumnSpan = NaviPosizionate[0].Item3 ? 1 : NaviDisponibili[0];

            OnPropertyChanged(nameof(SecondaNaveRow));
            OnPropertyChanged(nameof(SecondaNaveColumn));
            OnPropertyChanged(nameof(SecondaNaveRotation));
            OnPropertyChanged(nameof(SecondaNaveTranslationX));
            secondaNaveRowSpan = NaviPosizionate[1].Item3 ? NaviDisponibili[1] : 1;
            secondaNaveColumnSpan = NaviPosizionate[1].Item3 ? 1 : NaviDisponibili[1];
            
            OnPropertyChanged(nameof(TerzaNaveRow));
            OnPropertyChanged(nameof(TerzaNaveColumn));
            OnPropertyChanged(nameof(TerzaNaveRotation));
            OnPropertyChanged(nameof(TerzaNaveTranslationX));
            terzaNaveRowSpan = NaviPosizionate[2].Item3 ? NaviDisponibili[2] : 1;
            terzaNaveColumnSpan = NaviPosizionate[2].Item3 ? 1 : NaviDisponibili[2];
            
            OnPropertyChanged(nameof(QuartaNaveRow));
            OnPropertyChanged(nameof(QuartaNaveColumn));
            OnPropertyChanged(nameof(QuartaNaveRotation));
            OnPropertyChanged(nameof(QuartaNaveTranslationX));
            quartaNaveRowSpan = NaviPosizionate[3].Item3 ? NaviDisponibili[3] : 1;
            quartaNaveColumnSpan = NaviPosizionate[3].Item3 ? 1 : NaviDisponibili[3];
        }

    }
}
