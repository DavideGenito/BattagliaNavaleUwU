using BattagliaNavale.Models;
using BattagliaNavale.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattagliaNavale.ViewModels
{
    public partial class PosizionaNaviViewModel : ObservableObject
    {
        private const int GrigliaDimensione = 10;

        private readonly INavigation _navigation;

        public ObservableCollection<ObservableCollection<StatoCampo>> Campo { get; set; }

        [ObservableProperty]
        private int naveCorrente = 0;

        [ObservableProperty]
        private bool orientamentoOrizzontale = true;

        public List<int> NaviDisponibili { get; } = new() { 2, 3, 3, 4 };

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
        }

        [RelayCommand]
        private void SelezionaNave(int index)
        {
            NaveCorrente = index;
        }

        [RelayCommand]
        private void RuotaNave()
        {
            OrientamentoOrizzontale = !OrientamentoOrizzontale;
        }

        [RelayCommand]
        private void PosizionaNave(Point spostamento)
        {
            if (NaveCorrente >= NaviDisponibili.Count) return;

            int lunghezza = NaviDisponibili[NaveCorrente];
            int x = Convert.ToInt32(spostamento.X);
            int y = Convert.ToInt32(spostamento.Y);

            bool spazioDisponibile = true;

            for (int i = 0; i < lunghezza; i++)
            {
                int posX = OrientamentoOrizzontale ? x + i : x;
                int posY = OrientamentoOrizzontale ? y : y + i;

                if (posX >= GrigliaDimensione || posY >= GrigliaDimensione || campoLogico[posX, posY] == StatoCampo.NAVE)
                {
                    spazioDisponibile = false;
                    break;
                }
            }

            if (!spazioDisponibile) return;

            for (int i = 0; i < lunghezza; i++)
            {
                int posX = OrientamentoOrizzontale ? x + i : x;
                int posY = OrientamentoOrizzontale ? y : y + i;

                campoLogico[posX, posY] = StatoCampo.NAVE;
                Campo[posX][posY] = StatoCampo.NAVE;
            }

            NaveCorrente++;
        }

        [RelayCommand]
        private async Task ConfermaPosizionamentoAsync()
        {
            if (NaveCorrente < NaviDisponibili.Count)
            {
                // Mostra messaggio: "Devi ancora posizionare tutte le navi!"
                return;
            }

            await _navigation.PushAsync(new Gioco());
        }
    }
}
