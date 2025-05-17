using BattagliaNavale.Models;
using BattagliaNavale.ViewModels;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

namespace BattagliaNavale.Views;

public partial class Gioco : ContentPage
{
    const int LUNGHEZZA = 10;
    public Gioco(StatoCampo[,] campoLogico, List<Tuple<int, int, bool>> barchePosizione)
    {
        InitializeComponent();

        BindingContext = new ViewModels.GiocoViewModel(campoLogico);

        for (int i = 0; i < LUNGHEZZA; i++)
        {
            grigliaBot.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grigliaBot.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grigliaGiocatore.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grigliaGiocatore.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        for (int i = 0; i < LUNGHEZZA; i++)
        {
            for (int j = 0; j < LUNGHEZZA; j++)
            {
                Button bottone = new Button();
                Grid.SetColumn(bottone, j);
                Grid.SetRow(bottone, i);
                bottone.BorderWidth = 0.5;
                bottone.BackgroundColor = Colors.LightBlue;
                bottone.BorderColor = Colors.CadetBlue;
                bottone.CornerRadius = 0;
                grigliaBot.Children.Add(bottone);
                Microsoft.Maui.Controls.Shapes.Rectangle cella = new Microsoft.Maui.Controls.Shapes.Rectangle();
                Grid.SetColumn(cella, j);
                Grid.SetRow(cella, i);
                cella.Fill = new SolidColorBrush(Colors.LightBlue);
                cella.Stroke = new SolidColorBrush(Colors.CadetBlue);
                cella.StrokeThickness = 0.5;
                grigliaGiocatore.Children.Add(cella);
            }
        }

        for(int i = 0; i < barchePosizione.Count; i++)
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

            Image barca = new Image
            {
                Source = source,
                ZIndex = 1,
                Rotation = barchePosizione[i].Item3 ? 90 : 0,
                Scale = barchePosizione[i].Item3 ? scale : 1.0
            };

            Grid.SetRow(barca, barchePosizione[i].Item1);
            Grid.SetColumn(barca, barchePosizione[i].Item2);
            if(barchePosizione[i].Item3)
            {
                Grid.SetColumnSpan(barca, lunghezza);
            }
            else
            {
                Grid.SetRowSpan(barca, lunghezza);
            }

            grigliaGiocatore.Children.Add(barca);
        }
    }
}