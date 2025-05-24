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

namespace BattagliaNavale.ViewModels
{
    public partial class CronologiaPartiteViewModel : ObservableObject
    {
        public ObservableCollection<PartitaStatistiche> ListaPartite { get; set; } = [];

        public CronologiaPartiteViewModel() { }

        [RelayCommand]
        public void OnAppearing() 
        {
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
    }
}
