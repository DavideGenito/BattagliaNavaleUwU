using Microsoft.Maui.Controls;
using BattagliaNavale.Models;
using BattagliaNavale.ViewModels;
using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;

namespace BattagliaNavale.Views
{
    public partial class Gioco : ContentPage
    {
        const int LUNGHEZZA = 10;

        public Gioco(StatoCampo[,] campoLogico, List<Tuple<int, int, bool>> barchePosizione)
        {
            InitializeComponent();
            BindingContext = new GiocoViewModel(campoLogico);

            CreaGriglie();
            PosizionaBarche(barchePosizione);
        }

        private void CreaGriglie()
        {
            // Crea righe e colonne per le griglie
            for (int i = 0; i < LUNGHEZZA; i++)
            {
                grigliaBot.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grigliaBot.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                grigliaGiocatore.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grigliaGiocatore.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            // Crea bottoni per la griglia del bot
            for (int i = 0; i < LUNGHEZZA; i++)
            {
                for (int j = 0; j < LUNGHEZZA; j++)
                {
                    var bottone = new Button
                    {
                        BorderWidth = 0.5,
                        BackgroundColor = Colors.LightBlue,
                        BorderColor = Colors.CadetBlue,
                        CornerRadius = 0,
                        CommandParameter = new Tuple<int, int>(i, j)
                    };

                    bottone.AutomationId = $"btn_{i}_{j}";

                    bottone.Command = ((GiocoViewModel)BindingContext).SelezionaCellaCommand;

                    Grid.SetRow(bottone, i);
                    Grid.SetColumn(bottone, j);
                    grigliaBot.Children.Add(bottone);

                    var cella = new Microsoft.Maui.Controls.Shapes.Rectangle
                    {
                        Fill = new SolidColorBrush(Colors.LightBlue),
                        Stroke = new SolidColorBrush(Colors.CadetBlue),
                        StrokeThickness = 0.5
                    };
                    Grid.SetRow(cella, i);
                    Grid.SetColumn(cella, j);
                    grigliaGiocatore.Children.Add(cella);
                }
            }
        }

        private void PosizionaBarche(List<Tuple<int, int, bool>> barchePosizione)
        {
            for (int i = 0; i < barchePosizione.Count; i++)
            {
                int lunghezza = 2;
                string source = "";
                double scale = 1.0;
                switch (i)
                {
                    case 0:
                        lunghezza = 2;
                        source = "../Resources/Images/barca3.png";
                        scale = 2.0;
                        break;
                    case 1:
                        lunghezza = 3;
                        source = "../Resources/Images/barca1.png";
                        scale = 3.0;
                        break;
                    case 2:
                        lunghezza = 3;
                        source = "../Resources/Images/barca4.png";
                        scale = 2.7;
                        break;
                    case 3:
                        lunghezza = 4;
                        source = "../Resources/Images/barca2.png";
                        scale = 3.7;
                        break;
                }

                var barca = new Image
                {
                    Source = ImageSource.FromFile(source),
                    ZIndex = 1,
                    Rotation = barchePosizione[i].Item3 ? 90 : 0,
                    Scale = barchePosizione[i].Item3 ? scale : 1.0
                };

                Grid.SetRow(barca, barchePosizione[i].Item1);
                Grid.SetColumn(barca, barchePosizione[i].Item2);
                if (barchePosizione[i].Item3)
                    Grid.SetColumnSpan(barca, lunghezza);
                else
                    Grid.SetRowSpan(barca, lunghezza);

                grigliaGiocatore.Children.Add(barca);
            }
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            var vm = (GiocoViewModel)BindingContext;

            if (vm.ColpiPlayer.Count == 0 || vm.ColpiBot.Count == 0)
            {
                return;
            }

            var colpoPlayer = vm.ColpiPlayer.LastOrDefault();

            MostraFeedbackColpo(grigliaBot, colpoPlayer.x, colpoPlayer.y, colpoPlayer.colpito);

            DisabilitaBottone(colpoPlayer.x, colpoPlayer.y);

            var colpoBot = vm.ColpiBot.LastOrDefault();

            MostraFeedbackColpo(grigliaGiocatore, colpoBot.x, colpoBot.y, colpoBot.colpito);

            CheckRisultato(vm.MessaggioRisultato);
        }

        private void MostraFeedbackColpo(Grid griglia, int riga, int colonna, bool colpito)
        {

            var imgFeedback = new Image
            {
                Source = colpito
                    ? ImageSource.FromFile("../Resources/Images/fire.gif")
                    : ImageSource.FromFile("../Resources/Images/croce_img.png"),
                ZIndex = 10,
                IsAnimationPlaying = colpito,
                WidthRequest = 30,
                HeightRequest = 30,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Grid.SetRow(imgFeedback, riga);
            Grid.SetColumn(imgFeedback, colonna);
            griglia.Children.Add(imgFeedback);

            
        }

        private void DisabilitaBottone(int riga, int colonna)
        {
            foreach (var child in grigliaBot.Children)
            {
                if (child is Button button &&
                    Grid.GetRow(button) == riga &&
                    Grid.GetColumn(button) == colonna)
                {
                    button.IsEnabled = false;
                    break;
                }
            }
        }

        private void CheckRisultato(Risultato risultato)
        {
            if (risultato == Risultato.VINTO_PLAYER)
            {
                DisplayAlert("Hai vinto!", "Hai distrutto tutte le navi del bot!", "OK");
                Esci();
            }
            else if (risultato == Risultato.VINTO_BOT)
            {
                DisplayAlert("Hai perso!", "Il bot ha distrutto tutte le tue navi!", "OK");
                Esci();
            }
        }

        [RelayCommand]
        public async Task Esci()
        {
            await Navigation.PopAsync();
        }
    }
}