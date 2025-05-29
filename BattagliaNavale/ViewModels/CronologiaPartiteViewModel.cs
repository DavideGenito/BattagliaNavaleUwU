using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattagliaNavale.Models;
using BattagliaNavale.Infrastucture;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace BattagliaNavale.ViewModels
{
    public partial class CronologiaPartiteViewModel : ObservableObject
    {
        public ObservableCollection<PartitaStatistiche> ListaPartite { get; set; } = [];

        [ObservableProperty]
        private string nomeUtente = "Utente Sconosciuto";

        public CronologiaPartiteViewModel()
        {
            CaricaNomeUtente();
        }

        private void CaricaNomeUtente()
        {
            string nomeUtenteSalvato = Preferences.Get("NomeUtente", "Utente Sconosciuto");
            NomeUtente = nomeUtenteSalvato;
        }

        [RelayCommand]
        public void OnAppearing() 
        {
            CaricaNomeUtente();

            ListaPartite.Clear();

            var partite = PreferencesUtilities.GetFields();

            foreach ( var field in partite )
            {
                ListaPartite.Add(field);
            }
        }

        [RelayCommand]
        public void CancellaPartita(int id)
        {
            PreferencesUtilities.DeleteOrder(id);

            ListaPartite.Clear();

            var updatedOrders = PreferencesUtilities.GetFields();

            foreach (var order in updatedOrders)
            {
                ListaPartite.Add(order);
            }
        }

        [RelayCommand]
        private async void CambiaNome()
        {
            string nuovoNome = await Application.Current.MainPage.DisplayPromptAsync(
                "Cambia Nome",
                "Inserisci il nuovo nome utente:",
                initialValue: nomeUtente);

            if (!string.IsNullOrWhiteSpace(nuovoNome))
            {
                Preferences.Set("NomeUtente", nuovoNome);

                NomeUtente = nuovoNome;
            }
        }
    }
}
